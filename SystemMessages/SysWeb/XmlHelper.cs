using System.Xml;
using System.Data;
using System.IO;
using System.Web;
using System.Collections.Generic;

namespace SysWeb
{
    /// <summary>
    /// Xml的操作公共类
    /// </summary>    
    public class XmlHelper
    {
        public static Dictionary<string,string> GetNode() 
        {
            XmlDocument xml = new XmlDocument();
            Dictionary<string,string> list = new Dictionary<string,string>();
            try
            {
                xml.Load(HttpContext.Current.Server.MapPath("~/UserAuthentication.xml"));
                //指定一个节点
                XmlNode root = xml.SelectSingleNode("/root");
                //获取节点下所有直接子节点
                if (root.HasChildNodes)
                {
                    XmlNodeList childlist = root.ChildNodes;
                    foreach (XmlNode xn in childlist)
                    {
                        XmlElement xe = (XmlElement)xn;
                        if (xe.HasChildNodes)
                        {
                            XmlNodeList nls = xe.ChildNodes;
                            var user = string.Empty;
                            var pwd = string.Empty;
                            foreach (XmlNode node in nls)
                            {
                                XmlElement item = (XmlElement)node;//转换类型
                                if (item != null && item.Name.ToLower() == "user")
                                {
                                    user = item.InnerText.ToLower();
                                }
                                if (item != null && item.Name.ToLower() == "pwd")
                                {
                                    pwd = item.InnerText.ToLower();
                                }
                            }
                            if (!string.IsNullOrWhiteSpace(user) && !string.IsNullOrWhiteSpace(pwd)) list.Add(user, pwd);
                        }
                    }
                }
                return list;
            }
            catch (System.Exception ex)
            {
                LogHelper.Error(ex);
                return null;
            }
        }
    }
}
