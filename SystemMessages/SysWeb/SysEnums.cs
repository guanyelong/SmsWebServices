using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SysWeb
{
    public enum SendFlag
    {
        未发送=0,
        已发送=1
    }
    public enum SendType 
    {
        一般短信=0,
        定时短信=1
    }
    public enum Platform 
    {
        客服系统=0,
        网站系统=1,
        APP=2
    }
}