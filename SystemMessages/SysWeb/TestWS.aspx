<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestWS.aspx.cs" Inherits="SysWeb.TestWS" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style>
        .align-center {
            margin: 0 auto; /* 居中 这个是必须的，，其它的属性非必须 */
            width: 500px; /* 给个宽度 顶到浏览器的两边就看不出居中效果了 */
            /*background: red; /* 背景色 */*/
            text-align: center; /* 文字等内容居中 */
        }
        div span {
            width:100px;
            padding-right:10px;
        }
    </style>
</head>
<body>
    <div class="align-center">
        <form id="form1" runat="server">

            <div>
                <span>手机号码</span><asp:TextBox ID="txtMobile" runat="server"></asp:TextBox>
            </div>
            <div>
                <span>身份账号</span><asp:TextBox ID="txtUser" runat="server"></asp:TextBox>
            </div>
            <div>
                <span>身份密码</span><asp:TextBox ID="txtPwd" runat="server"></asp:TextBox>
            </div>
            <div>
                <span></span><asp:Button Text="验证" ID="btnValidate" runat="server" OnClick="btnValidate_Click" />
            </div>
            <div>
                <span>
                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                </span>
            </div>

        </form>
    </div>
</body>
</html>
