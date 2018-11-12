<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Prompt.ShowError" Codebehind="ShowError.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>错误提示信息</title>
</head>
<body id="MasterPagebody">
    <form id="form1" runat="server">
        <div>
            <br />
            <br />
            <table cellpadding="2" cellspacing="1" border="0" width="400" class="border" align="center">
                <tr align="center" class="title">
                    <td>
                        <strong>错误信息</strong></td>
                </tr>
                <tr class="tdbg">
                    <td valign="top" height="100">
                        <br />
                        <b>产生错误的可能原因：</b><br />
                        <pe:ExtendedLiteral HtmlEncode="false" ID="LtrErrorMessage" runat="server"></pe:ExtendedLiteral></td>
                </tr>
                <tr align="center" class="tdbg">
                    <td>
                        <asp:HyperLink ID="LnkReturnUrl" runat="server"><< 返回上一页</asp:HyperLink>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
