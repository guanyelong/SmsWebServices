using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SysWeb
{
    public class SmsInfo
    {
        /// <summary>
        /// 手机号码
        /// </summary>
        public string mobile { get; set; }
        /// <summary>
        /// 短信内容
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 发送消息人ID
        /// </summary>
        public int? creatorId { get; set; }
        /// <summary>
        /// 发送消息人名字
        /// </summary>
        public string creator { get; set; }
        /// <summary>
        /// 短信类别:0一般短信；1定时短信
        /// </summary>
        public int? stype { get; set; }
        /// <summary>
        /// 短信计划发送时间
        /// </summary>
        public DateTime? sendTime { get; set; }
        /// <summary>
        /// 短信下发计划使用的通道号（可能有多个短信通道）
        /// </summary>
        public int? pipeId { get; set; }
        /// <summary>
        /// 短信优先级，数值越高，优先级别越高
        /// </summary>
        public int? levelNum { get; set; }
        /// <summary>
        /// 所属平台
        /// </summary>
        public int? sysPlatform { get; set; }
    }

    public class SmsError {
        public string errorNum { get; set; }
        public string errosMsg { get; set; }
    }
}