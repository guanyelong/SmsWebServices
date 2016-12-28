using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace SysWeb
{
    /// <summary>
    /// SmsMsg 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class SmsMsg : System.Web.Services.WebService
    {
        private static readonly string _conStr = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
        private static readonly int _commandTimeOut = Convert.ToInt32(ConfigurationManager.AppSettings["TimeOut"]);
        private static readonly int _BatchSize = Convert.ToInt32(ConfigurationManager.AppSettings["BatchSize"]);
        private static readonly string _tableName = ConfigurationManager.AppSettings["tableName"];
        private static readonly int _pipeId = Convert.ToInt32(ConfigurationManager.AppSettings["PipeId"]);
        private static readonly string _autograph = ConfigurationManager.AppSettings["autograph"];
        private static readonly string _adminName = ConfigurationManager.AppSettings["adminName"];
        private static readonly string _adminPwd = ConfigurationManager.AppSettings["adminPwd"];
        public MySoapHeader myHeader = new MySoapHeader();
        //static  BadWordsFilter bf = new BadWordsFilter();
        static TrieFilter tf = new TrieFilter();
        [System.Web.Services.Protocols.SoapHeader("myHeader")]
        [WebMethod]
        public string SmsSend(string mobiles, string msg, int? creatorId, string creator, DateTime? sendTime, int? sysPlatform = (int)Platform.网站系统, int? stype = (int)SendType.一般短信, int? levelNum = 0)
        {
            SmsError se = new SmsError();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            if (!myHeader.CheckLogin())
            {
                se.errorNum = "010";
                se.errosMsg = "身份认证不通过！";
                var emsg = serializer.Serialize(se);
                return emsg;
            }
            
            string filterstr = string.Empty;
            if (KeysFilter(msg,ref filterstr))
            {
                se.errorNum = "007";
                se.errosMsg = "内容包括非法字符！非法字符：" + filterstr;
                var emsg = serializer.Serialize(se);
                return emsg;
            }
            try
            {
                string error = string.Empty;
                SmsInfo info = new SmsInfo()
                {
                    creator = creator,
                    creatorId = creatorId,
                    levelNum = levelNum,
                    mobile = mobiles,
                    msg = msg,
                    pipeId = _pipeId,
                    sendTime = sendTime,
                    stype = stype,
                    sysPlatform = sysPlatform
                };
                if (validate(info, ref se))
                {
                    string[] arrys = info.mobile.Replace('；', ';').Replace(',', ';').Split(';');
                    DataTable dt = CreateTable();
                    dt = CreateRow(dt, arrys, info);
                    bool result = innserMsg(dt, _tableName, ref se);
                    var emsg = serializer.Serialize(se);
                    LogHelper.Info("系统异常error :" + emsg);
                    return emsg;
                }
                else
                {
                    var emsg = serializer.Serialize(se);
                    LogHelper.Info("参数丢失error :" + emsg);
                    return emsg;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                se = new SmsError() { errorNum = "006", errosMsg = ex.Message };
                var emsg = serializer.Serialize(se);
                return emsg;
            }
        }
        /// <summary>
        /// 参数验证和填充表数据
        /// </summary>
        /// <param name="info"></param>
        /// <param name="se"></param>
        /// <returns></returns>
        public bool validate(SmsInfo info, ref SmsError se)
        {
            //SmsError se = new SmsError();
            if (string.IsNullOrWhiteSpace(info.mobile))
            {
                se.errosMsg = "客户手机号码为空！";
                se.errorNum = "001";
                return false;
            }
            if (!string.IsNullOrWhiteSpace(info.mobile))
            {
                string[] arrys = info.mobile.Replace('；', ';').Replace(',', ';').Split(';');
                if (arrys.Length > 100)
                {
                    se.errosMsg = "手机号最多选中100个！";
                    se.errorNum = "009";
                    return false;
                }
            }
            if (string.IsNullOrWhiteSpace(info.msg))
            {
                se.errosMsg = "发送消息内容为空！";
                se.errorNum = "002";
                return false;
            }
            if (!string.IsNullOrWhiteSpace(info.msg) && info.msg.Length>=70)
            {
                se.errosMsg = "发送消息内容长度小于70个字符！";
                se.errorNum = "008";
                return false;
            }
            if (info.creatorId==null)
            {
                info.creatorId = 0;
                //se.errosMsg = "发送消息人ID值为空！";
                //se.errorNum = "003";
                //return false;
            }
            if (string.IsNullOrWhiteSpace(info.creator))
            {
                info.creator = "0";
                //se.errosMsg = "发送消息人名字为空！";
                //se.errorNum = "004";
                //return false;
            }
            if (info.stype==null)
            {
                info.stype = (int)SendType.一般短信;//默认为一般短信
            }
            if (info.sendTime==null)
            {
                info.sendTime = DateTime.Now;
            }
            if (info.levelNum==null)
            {
                info.levelNum = 0;
            }
            if (info.sysPlatform==null)
            {
                info.sysPlatform = (int)Platform.网站系统;
            }
            info.msg = _autograph + info.msg;
            return true;
        }
        /// <summary>
        /// 创建Table属性
        /// </summary>
        /// <returns></returns>
        public DataTable CreateTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("mobile", typeof(String));
            dt.Columns.Add("msg", typeof(String));
            dt.Columns.Add("creatorId", typeof(Int32));
            dt.Columns.Add("creator", typeof(String));
            dt.Columns.Add("c_time", typeof(DateTime));
            dt.Columns.Add("sendFlag", typeof(Int32));
            dt.Columns.Add("sendTime", typeof(DateTime));
            dt.Columns.Add("stype", typeof(Int32));
            dt.Columns.Add("pipeId", typeof(Int32));
            dt.Columns.Add("levelNum", typeof(Int32));
            dt.Columns.Add("sysPlatform", typeof(Int32));
            return dt;
        }
        /// <summary>
        /// 创建DataRow并赋值
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="moblie"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public DataTable CreateRow(DataTable dt, string[] moblie, SmsInfo info)
        {
            for (int i = 0; i < moblie.Length; i++)
            {
                DataRow dr = dt.NewRow();
                dr["mobile"] = moblie[i];
                dr["msg"] = info.msg;
                dr["creatorId"] = info.creatorId;
                dr["creator"] = info.creator;
                dr["c_time"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                dr["sendFlag"] = ((int)SendFlag.未发送).ToString();
                dr["sendTime"] = info.sendTime;
                dr["stype"] = info.stype;
                dr["pipeId"] = info.pipeId;
                dr["levelNum"] = info.levelNum;
                dr["sysPlatform"] = info.sysPlatform;
                dt.Rows.Add(dr);
            }
            return dt;
        }
        /// <summary>
        /// 大批量插入数据(_BatchSize/每批次) 。
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="tableName"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool innserMsg(DataTable dt, string tableName, ref SmsError msg)
        {
            using (SqlConnection conn = new SqlConnection(_conStr))
            {
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        //SqlBulkCopy bulkCopy = new SqlBulkCopy(conn, SqlBulkCopyOptions.CheckConstraints, transaction);
                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(conn, SqlBulkCopyOptions.Default, transaction))
                        {
                            bulkCopy.BatchSize = _BatchSize;
                            bulkCopy.BulkCopyTimeout = _commandTimeOut;
                            bulkCopy.DestinationTableName = tableName;
                            foreach (DataColumn col in dt.Columns)
                            {
                                bulkCopy.ColumnMappings.Add(col.ColumnName, col.ColumnName);
                            }
                            bulkCopy.WriteToServer(dt);
                            transaction.Commit();
                            conn.Close();
                            conn.Dispose();
                            msg.errosMsg = "success";
                            msg.errorNum = "000";
                            return true;
                        }
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Error(ex);
                        msg.errosMsg = ex.Message;
                        msg.errorNum = "005";
                        transaction.Rollback();
                        conn.Close();
                        conn.Dispose();
                        return false;
                    }
                }
            }
        }

        public bool KeysFilter(string text,ref string readKey)
        {
            bool result = false;
            ReadBadWord();
            for (int i = 0; i < 100; i++)
            {
                string keyOne = tf.FindOne(text);
                if (!string.IsNullOrWhiteSpace(keyOne))
                {
                    readKey = keyOne;
                    result = true;
                    break;
                }
            }
            return result;
        }
        static void ReadBadWord()
        {
            string sevicePath = HttpContext.Current.Server.MapPath("~/BadWord.txt");
            using (StreamReader sw = new StreamReader(sevicePath, Encoding.UTF8))
            //using (StreamReader sw = new StreamReader(File.OpenRead("BadWord.txt")))
            {
                string key = sw.ReadLine();
                while (key != null)
                {
                    if (key != string.Empty)
                    {
                        tf.AddKey(key);
                    }
                    key = sw.ReadLine();
                }
            }
        }
        /// <summary>
        /// 测试调用端口是否成功
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string TestingWebService() 
        {
            return "ok";
        }
        /// <summary>
        /// 测试端口身份认证是否成功
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        [System.Web.Services.Protocols.SoapHeader("myHeader")]
        public bool TestingAuthentication() 
        {
            if (!myHeader.CheckLogin())
            {
                return false;
            }
            return true;
        }
    }
}
