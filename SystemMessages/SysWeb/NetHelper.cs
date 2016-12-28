using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace SysWeb
{
    public class NetHelper
    {
        #region Get方式请求
        /// <summary>
        /// Get方式请求
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string RequestGetUrl(string url)
        {
            try
            {
                WebRequest request = WebRequest.Create(url);
                WebResponse response = request.GetResponse();
                Stream resStream = response.GetResponseStream();
                StreamReader sr = new StreamReader(resStream, System.Text.Encoding.GetEncoding("GBK"));
                String aaa = "";
                aaa = sr.ReadToEnd();
                resStream.Close();
                sr.Close();
                return aaa;
            }
            catch (Exception e)
            {
                LogHelper.Error(e);
                return "";
                
            }

        }
        #endregion

        #region RequestPostUrl post方式提交数据
        public static string RequestPostUrl(string url, string content)//post方式向页面提交 
        {
            byte[] bs = Encoding.UTF8.GetBytes(content);
            string resultStream = null;
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
            req.ContentLength = bs.Length;

            req.Timeout = 20000;
            //设置发送内容
            try
            {
                using (Stream reqStream = req.GetRequestStream())
                {
                    reqStream.Write(bs, 0, bs.Length);
                    reqStream.Close();
                    reqStream.Dispose();
                }
            }
            catch
            {

            }
            try
            {
                WebResponse wr = req.GetResponse();
                using (wr)
                {

                    //在这里对接收到的页面内容进行处理
                    Stream stream = wr.GetResponseStream();
                    StreamReader sr = new StreamReader(stream, Encoding.UTF8);
                    resultStream = @sr.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                resultStream = "0";

            }
            return resultStream;

        }

        /// <summary>  
        /// 指定Post地址使用Get 方式获取全部字符串  
        /// </summary>  
        /// <param name="url">请求后台地址</param>  
        /// <returns></returns>  
        public static string RequestPostUrl(string url, Dictionary<string, string> dic)
        {
            try
            {
                string result = "";
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
                #region 添加Post 参数
                StringBuilder builder = new StringBuilder();
                int i = 0;
                foreach (var item in dic)
                {
                    if (i > 0)
                        builder.Append("&");
                    builder.AppendFormat("{0}={1}", item.Key, item.Value);
                    i++;
                }
                byte[] data = Encoding.UTF8.GetBytes(builder.ToString());
                req.ContentLength = data.Length;
                using (Stream reqStream = req.GetRequestStream())
                {
                    reqStream.Write(data, 0, data.Length);
                    reqStream.Close();
                }
                #endregion
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                Stream stream = resp.GetResponseStream();
                //获取响应内容  
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    result = reader.ReadToEnd();
                }
                return result;
            }
            catch (Exception ex)
            {
                LogHelper.Error("RequestPostUrl", ex);
                return string.Empty;
            }
            
        }
        #endregion
    }
}