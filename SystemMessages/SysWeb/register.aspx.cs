using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SysWeb
{
    public partial class register : System.Web.UI.Page
    {
        static BadWordsFilter bf = new BadWordsFilter();
        static TrieFilter tf = new TrieFilter();
        private static readonly int _pipeId = Convert.ToInt32(ConfigurationManager.AppSettings["PipeId"]);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lengthcc();
            }
        }

        private void lengthcc() 
        {
            
        }

        private void HTMLTOSTRING() 
        {
            string stest = "<response><result>5959790|13031190350|null|null|2016-12-23 13:06:39|%BF%C9%D2%D4#####5959791|13031190350|null|null|2016-12-23 13:07:04|%BA%C3%B5%C4</result></response>";
            //string stest2 = "<response><result>5959796|13031190350|null|null|2016-12-23 13:12:05|%CA%C7%B5%C4</result></response>";
            string result = HttpUtility.UrlDecode(stest, Encoding.GetEncoding("GBK"));
            string strTest = Regex.Match(result, @"(?<=<result>)(.*?)(?=</result>)").Value;
            string[] arry = Regex.Split(strTest, "#####", RegexOptions.IgnoreCase);
            string sqlinsert = " innert into tb_Sms_up (mobile,msg,C_time,PipeId,CreatorId) ";
            string sqlsb = "";
            for (int i = 0; i < arry.Length; i++)
            {
                var strs = arry[i].Split('|');
                if (arry.Length - 1 == i)
                {
                    sqlsb += " select '" + arry[1] + "','" + arry[5] + "','" + arry[4] + "'," + _pipeId + "," + 1;
                }
                else
                {
                    sqlsb += " select '" + arry[1] + "','" + arry[5] + "','" + arry[4] + "'," + _pipeId + "," + "1  union all";
                }
            }
            
        }

        private void reg() { 
            Dictionary<string,string> dict=new Dictionary<string,string>();
            dict.Add("regcode","ZXHD-CRM-0100-ZMVCBP");
            dict.Add("pwd", MD5Helper.MD5Encrypt32bit("79268835"));//
            dict.Add("key","077be2c9df20b1c08a3d7df3d60f837d");//
            dict.Add("CNAME","和合益生（北京）诊所有限公司");//
            dict.Add("ENAME","heheyisheng");//
            dict.Add("CSNAME","和合益生");//
            dict.Add("ESNAME","hhys");//
            dict.Add("ENTERPRISETYPEID","01");//
            dict.Add("ADDR","北京市朝阳区北四环中路 房地首华大厦东北侧裙楼二层");//
            dict.Add("LINKTEL","010-53380976");//
            dict.Add("LINKMAN","");//
            dict.Add("EMAIL", "26197494@qq.com");//
            dict.Add("FAX","");//
            dict.Add("POSTCODE","100101");//
            dict.Add("MOBILETEL","13661019024");//
            string result = NetHelper.RequestPostUrl("http://sms.pica.com:8082/zqhdServer/reg.jsp", dict);
            LogHelper.Info(result);
        }

        private void reg2() {
            StringBuilder sb = new StringBuilder();
            sb.Append("regcode=ZXHD-CRM-0100-ZMVCBP");
            sb.Append("&pwd=" + MD5Helper.MD5Encrypt32bit("79268835"));//b83f586a65e82faa11976a79c627630d
            sb.Append("&key=077be2c9df20b1c08a3d7df3d60f837d");
            sb.Append("&CNAME=和合益生（北京）诊所有限公司");
            sb.Append("&ENAME=heheyisheng");
            sb.Append("&CSNAME=和合益生");
            sb.Append("&ESNAME=hhys");
            sb.Append("&ENTERPRISETYPEID=01");
            sb.Append("&ADDR=北京市朝阳区北四环中路 房地首华大厦东北侧裙楼二层");
            sb.Append("&LINKTEL=010-53380976");
            sb.Append("&LINKMAN=");
            sb.Append("&EMAIL=26197494@qq.com");
            sb.Append("&FAX=");
            sb.Append("&POSTCODE=100101");
            sb.Append("&MOBILETEL=13661019024");
            string result = NetHelper.RequestGetUrl("http://sms.pica.com:8082/zqhdServer/reg.jsp?"+sb.ToString());
            LogHelper.Info(result);
        }

        private void sendSMS() {
            StringBuilder sb = new StringBuilder();
            sb.Append("regcode=ZXHD-CRM-0100-ZMVCBP");
            sb.Append("&pwd=" + MD5Helper.MD5Encrypt32bit("79268835"));
            sb.Append("&phone=13031190350");
            sb.Append("&CONTENT=" + HttpUtility.UrlEncode("【和合益生】min测试56", Encoding.GetEncoding("GBK")));
            sb.Append("&extnum=&level=1&schtime=null&reportflag=1&url=&smstype=0&key=077be2c9df20b1c08a3d7df3d60f837d");
            string result = NetHelper.RequestGetUrl("http://sms.pica.com:8082/zqhdServer/sendSMS.jsp?" + sb.ToString());
            if (!string.IsNullOrWhiteSpace(result))
            {
                string strTest = Regex.Match(result, @"(?<=<result>)(.*?)(?=</result>)").Value;
            }
            LogHelper.Info(result);
        }

        private void receive() {
            StringBuilder sb = new StringBuilder();
            sb.Append("regcode=ZXHD-CRM-0100-ZMVCBP");
            sb.Append("&pwd=" + MD5Helper.MD5Encrypt32bit("79268835"));//b83f586a65e82faa11976a79c627630d
            sb.Append("&key=077be2c9df20b1c08a3d7df3d60f837d");
            string result = NetHelper.RequestGetUrl("http://sms.pica.com:8082/zqhdServer/recvSMS.jsp?" + sb.ToString());
            LogHelper.Info(result);
        }
        private void report() {
            StringBuilder sb = new StringBuilder();
            sb.Append("regcode=ZXHD-CRM-0100-ZMVCBP");
            sb.Append("&pwd=" + MD5Helper.MD5Encrypt32bit("79268835"));//b83f586a65e82faa11976a79c627630d
            sb.Append("&key=077be2c9df20b1c08a3d7df3d60f837d");
            string result = NetHelper.RequestGetUrl("http://sms.pica.com:8082/zqhdServer/getreport.jsp?" + sb.ToString());
            LogHelper.Info(result);
        }
        


        private void Valid() 
        {
            string text = "今天一百周年习近平一下";
            KeysFilter(text);
        }

        public bool KeysFilter(string text)
        {
            var result = false;
            var readKey = string.Empty;
            ReadBadWord();
            for (int i = 0; i < 100; i++)
            {
                result = bf.HasBadWord(text, ref readKey);
                if (result) {
                    break; 
                }
            }
            for (int i = 0; i < 100; i++)
            {
                string tttt = tf.FindOne(text);
               if (!string.IsNullOrEmpty(tttt))
               {
                   var aaa = "";
               }
            }
            return true;
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
                        bf.AddKey(key);
                        tf.AddKey(key);
                    }
                    key = sw.ReadLine();
                }
            }
        }
    }
}