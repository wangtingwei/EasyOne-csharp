<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Accessories.ShowCheckTitleMessage" Codebehind="ShowCheckTitleMessage.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>检测标题d</title>
</head>
<body>
  <center>
    <form id="form1" runat="server">
      
            <table width="100%" border="0" cellpadding="2" cellspacing="0" class="border">
                <tr class="title" style="height: 22">
                    <td align="center">
                        <b>检测结果</b></td>
                </tr>
                <tr class="tdbg">
                    <td style="height: 241px;">
                        <pe:ExtendedLabel HtmlEncode="false" ID="LblCheckTitleMessage" runat="server" Text="Label"></pe:ExtendedLabel>
                    </td>
                </tr>
                <tr class="title">
                    <td align="center">
                        <input type="button" class="inputbutton" name="BtnCloseWindow" onclick="javascript:window.close();"
                            value="关闭窗口" />
                    </td>
                </tr>
            </table>
    </form>
    </center>
</body>
</html>
