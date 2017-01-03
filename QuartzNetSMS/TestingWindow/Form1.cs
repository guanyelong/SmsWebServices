using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace TestingWindow
{
    public partial class Form1 : Form
    {
        private static readonly string _regcode = ConfigurationManager.AppSettings["regcode"];
        private static readonly string _pwd = MD5Helper.MD5Encrypt32bit(ConfigurationManager.AppSettings["pwd"]);
        private static readonly string _mobileKey = ConfigurationManager.AppSettings["mobileKey"];
        private static readonly string _mobileReport = ConfigurationManager.AppSettings["mobileReportPath"];
        private static readonly string _mobileSendPath = ConfigurationManager.AppSettings["mobileSendPath"];
        private static readonly string _mobileReceive = ConfigurationManager.AppSettings["mobileReceivePath"];
        private static readonly string _dbSource = ConfigurationManager.AppSettings["dbSource"];
        private static readonly string conStr = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
        private static readonly string _webservice = ConfigurationManager.AppSettings["webservice"];
        private static readonly string _adminName = ConfigurationManager.AppSettings["adminName"];
        private static readonly int _adminID = Convert.ToInt32(ConfigurationManager.AppSettings["adminID"]);
        public Form1()
        {
            InitializeComponent();
        }
        private void btnTesting_Click(object sender, EventArgs e)
        {
            
            TestingFramework();
            TestingMobile();
            TestingIP();
            TestingWeb();
            if (!string.IsNullOrWhiteSpace(this.txtMoblie.Text) && !string.IsNullOrWhiteSpace(this.txtUser.Text) && !string.IsNullOrWhiteSpace(this.txtPwd.Text))
            {
                TestingPhone();
            }
        }

        private void TestingFramework()
        {
            lblFramework.Text = System.Environment.Version.ToString();
        }

        private void TestingMobile()
        {
            string report_result = NetHelper.RequestGetUrl(_mobileReport);
            string send_result = NetHelper.RequestGetUrl(_mobileSendPath);
            string receive_result = NetHelper.RequestGetUrl(_mobileReceive);
            string path = AppDomain.CurrentDomain.BaseDirectory + "Images\\";
            if (!string.IsNullOrWhiteSpace(send_result))
                ShowImage(this.picSMS, path + "ok.png");
            else
            {
                ShowImage(this.picSMS, path + "no.png");
                lblerror_send.Text = "请检查配置文件 key=mobileSendPath 中的地址是否正确！";
            }
                
            if (!string.IsNullOrWhiteSpace(report_result))
                ShowImage(this.picReceive, path + "ok.png");
            else
            {
                ShowImage(this.picReceive, path + "no.png");
                lblerror_receive.Text = "请检查配置文件 key=mobileReceivePath 中的地址是否正确！";
            }
                
            if (!string.IsNullOrWhiteSpace(receive_result))
                ShowImage(this.picReport, path + "ok.png");
            else 
            {
                ShowImage(this.picReport, path + "no.png");
                lblerror_report.Text = "请检查配置文件 key=mobileReportPath 中的地址是否正确！";
            }
                
        }

        private void TestingIP()
        {
            Ping pingSender = new Ping();
            string path = AppDomain.CurrentDomain.BaseDirectory + "Images\\";
            PingReply reply = pingSender.Send(_dbSource, 10);//第一个参数为ip地址，第二个参数为ping的时间 
            if (reply.Status == IPStatus.Success)
            {
                //ping的通 
                this.picwait.Visible = true;
                //Thread TD = new Thread(connct);
                //TD.Start(); 
                ThreadStart ts = new ThreadStart(connshow);
                Thread thread = new Thread(ts);
                thread.Name = "connshow";
                thread.Start();
            }
            else
            {
                //ping不通 
                ShowImage(this.picDB, path + "no.png");
                lblerror_db.Text = "请检查配置文件 key=dbSource中的地址是否正确！";
            }
        }

        private void connshow()
        {
            //异步外的方法。这样窗体不会假死
            connct();
        }

        delegate void SetTextCallBack();
        private void connct() 
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "Images\\";
            SqlConnection mySqlConnection = new SqlConnection(conStr);
            
            try
            {
                //打开数据库
                mySqlConnection.Open();
                //mySqlConnection.Close();
                if (this.picwait.InvokeRequired)
                {
                    SetTextCallBack stcb = new SetTextCallBack(connct);
                    this.Invoke(stcb);
                }
                else
                {
                    ShowImage(picDB, path + "ok.png");
                    picwait.Visible = false;
                }
                
            }
            catch (Exception ex)
            {
                if (this.picwait.InvokeRequired)
                {
                    SetTextCallBack stcb = new SetTextCallBack(connct);
                    this.Invoke(stcb);
                }
                else
                {
                    picwait.Visible = false;
                    mySqlConnection.Close();
                    ShowImage(picDB, path + "no.png");
                    lblerror_db.Text = "数据库连接失败！请检查配置文件 name=conStr中的数据库连接字符串是否正确！";
                }
                
            }
        }

        private void TestingWeb() 
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "Images\\";
            try
            {
                SmsMsg.SmsMsg smsg = new SmsMsg.SmsMsg();
                string result = smsg.TestingWebService();
                if (result == "ok")
                {
                    ShowImage(this.picweb, path + "ok.png");
                }
                else
                {
                    ShowImage(this.picweb, path + "no.png");
                    lblerror_web.Text = "请检查配置文件 key=webservice 中的地址是否正确！";
                }
            }
            catch (Exception)
            {
                ShowImage(this.picweb, path + "no.png");
                lblerror_web.Text = "请检查配置文件 key=webservice 中的地址是否正确！";
            }
            

            //TcpClient client = new TcpClient();
            //try
            //{
            //    string hosts = _webservice.Substring(_webservice.IndexOf(":") + 3);
            //    string host = hosts.Substring(0, hosts.IndexOf(":"));
            //    string ports = _webservice.Substring(_webservice.LastIndexOf(":") + 1);
            //    int port = Convert.ToInt32(ports.Substring(0, ports.IndexOf("/")));
            //    var ar = client.BeginConnect(host, port, null, null);
            //    ar.AsyncWaitHandle.WaitOne(2);
            //    if (client.Connected)
            //    {
            //        ShowImage(this.picweb, path + "ok.png");
            //    }
            //    else
            //    {
            //        ShowImage(this.picweb, path + "no.png");
            //        lblerror_web.Text = "请检查配置文件 key=webservice 中的地址是否正确！";
            //    }
            //}
            //catch (Exception)
            //{
            //    ShowImage(this.picweb, path + "no.png");
            //    lblerror_web.Text = "请检查配置文件 key=webservice 中的地址是否正确！";
            //}
        }

        private void TestingPhone() 
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "Images\\";
            try
            {
                SmsMsg.MySoapHeader header = new SmsMsg.MySoapHeader();
                header.loginName = this.txtUser.Text;
                header.loginPwd =MD5Helper.MD5Encrypt32bit(this.txtPwd.Text);
                SmsMsg.SmsMsg smg = new SmsMsg.SmsMsg();
                smg.MySoapHeaderValue = header;
                bool authen=smg.TestingAuthentication();
                if (authen)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("?regcode=" + _regcode);
                    sb.Append("&pwd=" + _pwd);
                    sb.Append("&phone=" + this.txtMoblie.Text);
                    sb.Append("&CONTENT=" + HttpUtility.UrlEncode("【和合益生】此条为接口测试短信，若收到则测试成功！", Encoding.GetEncoding("GBK")));
                    sb.Append("&extnum=&level=1&schtime=null&reportflag=1&url=&smstype=0&key=" + _mobileKey);
                    string result = NetHelper.RequestGetUrl(_mobileSendPath + sb.ToString());
                    if (!string.IsNullOrWhiteSpace(result))
                    {
                        string strTest = Regex.Match(result, @"(?<=<result>)(.*?)(?=</result>)").Value;
                        if (strTest == "0")
                        {
                            ShowImage(picmobile, path + "ok.png");
                            LogHelper.Info("当前操作员使用账号" + this.txtUser.Text + "发送一条测试短信。");
                        }
                        else
                        {
                            ShowImage(picmobile, path + "no.png");
                            lblerror_mobile.Text = "短信发送到手机端失败！检查配置key=mobileSendPath是否正确，错误代码：" + strTest;
                        }
                    }
                    else
                    {
                        ShowImage(picmobile, path + "no.png");
                        lblerror_mobile.Text = "调用短信接口失败！";
                    }
                }
                else
                {
                    ShowImage(picmobile, path + "no.png");
                    lblerror_mobile.Text = "调用WebService身份认证失败";
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                ShowImage(picmobile, path + "no.png");
                lblerror_mobile.Text = "系统异常，错误原因：" + ex.Message;
            }
            
            //SmsMsg.SmsMsg sm = new SmsMsg.SmsMsg();
            //var msg = sm.SmsSend(this.txtMoblie.Text, "此条为接口测试短信，若收到则测试成功！", _adminID, _adminName, null, null, 0, null);
            //string path = AppDomain.CurrentDomain.BaseDirectory + "Images\\";
            //JavaScriptSerializer jss = new JavaScriptSerializer();
            //SmsError error = jss.Deserialize<SmsError>(msg);
            //if (error != null && error.errorNum == "000")
            //{
            //    StringBuilder sb = new StringBuilder();
            //    sb.Append("regcode="+_regcode);
            //    sb.Append("&pwd=" + _pwd);
            //    sb.Append("&phone="+this.txtMoblie.Text);
            //    sb.Append("&CONTENT=" + HttpUtility.UrlEncode("【和合益生】此条为接口测试短信，若收到则测试成功！", Encoding.GetEncoding("GBK")));
            //    sb.Append("&extnum=&level=1&schtime=null&reportflag=1&url=&smstype=0&key=" + _mobileKey);
            //    string result = NetHelper.RequestGetUrl(_mobileSendPath + sb.ToString());
            //    if (!string.IsNullOrWhiteSpace(result))
            //    {
            //        string strTest = Regex.Match(result, @"(?<=<result>)(.*?)(?=</result>)").Value;
            //        if (strTest == "0") 
            //        {
            //            ShowImage(picmobile, path + "ok.png");
            //        }
            //        else
            //        {
            //            ShowImage(picmobile, path + "no.png");
            //            lblerror_mobile.Text = "短信发送到手机端失败！错误代码：" + strTest;
            //        }
            //    }
            //}
            //else
            //{
            //    ShowImage(picmobile, path + "no.png");
            //    lblerror_mobile.Text = "短信数据插入数据库失败！错误代码：" + error.errorNum + ",错误原因：" + error.errosMsg;
            //}
        }

        public void ShowImage(PictureBox pic, string path)
        {
            Image img = Image.FromFile(path); //加载图片  
            MemoryStream mstr = new MemoryStream();
            img.Save(mstr, ImageFormat.Png);
            // 保存这个对象              
            pic.Image = Image.FromStream(mstr); //显示  
            img.Dispose();//释放占用
        }

    }
    public class SmsError
    {
        public string errorNum { get; set; }
        public string errosMsg { get; set; }
    }
}
