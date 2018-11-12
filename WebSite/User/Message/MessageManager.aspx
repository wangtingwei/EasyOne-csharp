<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.User.MessageManager" Codebehind="MessageManager.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>短消息管理</title>
</head>
<body>
    <pe:UserNavigation Tab="message" ID="UserCenterNavigation" runat="server" />
    <pe:ExtendedSiteMapPath ID="YourPosition" SiteMapProvider="UserMapProvider" runat="server" />
    <form id="form1" runat="server">
        <pe:ShowMessageList ID="ShowUserMessageList" runat="server" />
        <br />
        <table width="100%" cellpadding="5" cellspacing="0" class="border">
            <tr class="tdbg">
                <td align="right" style="width: 130px;">
                    <strong>短消息搜索：</strong>
                </td>
                <td>
                    <asp:DropDownList ID="DropManageType" runat="server">
                        <asp:ListItem Value="0">收件箱</asp:ListItem>
                        <asp:ListItem Value="1">草稿箱</asp:ListItem>
                        <asp:ListItem Value="2">已发送</asp:ListItem>
                        <asp:ListItem Value="3">废件箱</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;
                    <asp:DropDownList ID="DropSearch" runat="server">
                        <asp:ListItem Value="0">搜索类型</asp:ListItem>
                        <asp:ListItem Value="1">短消息主题</asp:ListItem>
                        <asp:ListItem Value="2">短消息内容</asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="TxtKeyword" runat="server">关键字</asp:TextBox>
                    <pe:ExtendedButton IsChecked="false" OperateCode="None" ID="BtnSubmit" runat="server"
                        Text="搜索" />
                    <pe:RequiredFieldValidator ID="ValrKeyword" runat="server" ControlToValidate="TxtKeyWord"
                        ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">请输入搜索的关键字！</pe:RequiredFieldValidator></td>
            </tr>
        </table>
    </form>
</body>
</html>
