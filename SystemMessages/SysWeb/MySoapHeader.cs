using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SysWeb
{
    public class MySoapHeader : System.Web.Services.Protocols.SoapHeader
    {
        public string loginName { get; set; }
        public string loginPwd { get; set; }

        public MySoapHeader(){}

        public MySoapHeader(string loginName,string loginPwd)
        {
            this.loginName = loginName;
            this.loginPwd = loginPwd;
        }

        public bool CheckLogin() 
        {
            if (string.IsNullOrWhiteSpace(loginName) || string.IsNullOrWhiteSpace(loginPwd)) return false;
            Dictionary<string,string> list=XmlHelper.GetNode();
            if (list==null || list.Count==0){
                return true;
            }
            else
            {
                foreach (KeyValuePair<string,string> kvp in list)
                {
                    if (kvp.Key == loginName.ToLower() && kvp.Value==loginPwd.ToLower())
                    {
                        return true;
                    }
                }
                return false;
            }
            //if (loginName.ToUpper() == _adminName.ToUpper() && loginPwd.ToUpper() == _adminPwd.ToUpper()) return true;
            
        }

    }
}