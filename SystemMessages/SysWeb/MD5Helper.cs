using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace SysWeb
{
    public class MD5Helper
    {
        #region MD5加密

        /// <summary>
        /// 返回MD5　32位加密后的值
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string MD5Encrypt32bit(string str)
        {
            byte[] b = System.Text.Encoding.Default.GetBytes(str);
            b = new System.Security.Cryptography.MD5CryptoServiceProvider().ComputeHash(b);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < b.Length; i++)
            {
                sb.Append(b[i].ToString("x").PadLeft(2, '0'));

            }
            return sb.ToString();
        }

        /// <summary>
        /// 返回MD5 16位加密后的值
        /// </summary>
        /// <param name="ConvertString"></param>
        /// <returns></returns>
        public static string MD5Encrypt16bit(string str)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(str), 4, 8));
            t2 = t2.Replace("-", "");
            t2 = t2.ToLower();//若欲转换为小写
            return t2;
        }

        #endregion
    }
}