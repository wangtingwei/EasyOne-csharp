<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPage.master"
    Inherits="EasyOne.WebSite.Admin.Accessories.MessageBatchDel" Codebehind="MessageBatchDel.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td colspan="2" class="spacingtitle">
                <b>批量删除操作</b>
            </td>
        </tr>
        <tr class="tdbg">
            <td align="right" style="height: 28px; width: 45%;">
                <strong>批量删除会员（发件人）短消息：<br />
                </strong>可以用英文状态下的逗号将用户名隔开实现多会员同时删除</td>
            <td style="height: 28px; width: 511px;">
                <asp:TextBox ID="TxtSender" runat="server"></asp:TextBox>
                <asp:Button ID="BtnDelSender" runat="server" Text="删除" OnClientClick="return confirm('确定要删除吗？');"
                    OnClick="BtnDelSender_Click" /></td>
        </tr>
        <tr class="tdbg">
            <td align="right" style="height: 28px; width: 45%;">
                <strong>批量删除指定日期范围内的短消息：<br />
                </strong>默认为删除已读信息</td>
            <td style="height: 28px; width: 511px;">
                <asp:DropDownList ID="DropDelDate" runat="server">
                    <asp:ListItem Value="1">一天前</asp:ListItem>
                    <asp:ListItem Value="3">三天前</asp:ListItem>
                    <asp:ListItem Value="7">一星期前</asp:ListItem>
                    <asp:ListItem Value="30">一个月前</asp:ListItem>
                    <asp:ListItem Value="60">两个月前</asp:ListItem>
                    <asp:ListItem Value="180">半年前</asp:ListItem>
                    <asp:ListItem Value="0">所有短消息</asp:ListItem>
                </asp:DropDownList>
                <asp:Button ID="BtnDelDate" runat="server" Text="删除" OnClientClick="return confirm('确定要删除吗？');"
                    OnClick="BtnDelDate_Click" /></td>
        </tr>
    </table>
</asp:Content>
