using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SysWeb
{
    public partial class TestWS : System.Web.UI.Page
    {
        private static readonly string _autograph = ConfigurationManager.AppSettings["autograph"];
        private static readonly int _pipeId = Convert.ToInt32(ConfigurationManager.AppSettings["PipeId"]);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }
        }

        protected void btnValidate_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(this.txtMobile.Text)) {
                    lblMsg.Text = "请输入手机号码！";
                    return;
                }
                if (string.IsNullOrWhiteSpace(this.txtUser.Text)) { 
                    lblMsg.Text = "请输入身份账号！";
                    return;
                }
                if (string.IsNullOrWhiteSpace(this.txtPwd.Text)) { 
                    lblMsg.Text = "请输入身份密码！";
                    return;
                }
                Dictionary<string, string> list = XmlHelper.GetNode();
                bool result = false;
                if (list!=null)
                {

                    foreach (KeyValuePair<string, string> kvp in list)
                    {
                        if (kvp.Key == this.txtUser.Text.ToLower() && kvp.Value == MD5Helper.MD5Encrypt32bit(this.txtPwd.Text.ToLower()))
                        {
                            result = true;
                            LogHelper.Info("当前操作员使用账号" + kvp.Key + "发送一条测试短信。");
                            string insertSql = "insert into tb_Sms_Down (mobile,msg,creatorId,creator,c_time,sendFlag,errMsg,sendTime,stype,pipeId,levelNum,sysPlatform) "
                + "Values('{0}','{1}{2}',{3},'{4}','{5}',{6},'{7}','{8}',{9},{10},{11},{12})";
                            string sql = string.Format(insertSql, this.txtMobile.Text, _autograph, "若接收到此短信表示测试推送服务部署成功！", 0, 0, DateTime.Now, (int)SendFlag.未发送, "", DateTime.Now.AddSeconds(4), (int)SendType.一般短信, _pipeId, 0, (int)Platform.网站系统);
                            int num = SQLHelp.ExecuteNonQuery(sql, CommandType.Text, null);
                            if (num > 0) lblMsg.Text = "数据插入成功！若1分钟收不到短信信息，则推送服务失败。";
                            else lblMsg.Text = "插入数据异常！";
                            break;
                        }
                    }
                }
                if (!result)
                {
                    lblMsg.Text = "身份认证输入错误！";
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
            
        }
    }
}