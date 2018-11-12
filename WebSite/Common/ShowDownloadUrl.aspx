<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.ShowDownloadUrl" StylesheetTheme="" EnableTheming="false" Codebehind="ShowDownloadUrl.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="ShowDownloadUrl" runat="server">
    <div id="content">
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <pe:ExtendedLiteral HtmlEncode="false" ID="LitPlayer" runat="server"></pe:ExtendedLiteral>
                </td>
            </tr>
        </table>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
