<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Accessories.DownServers" Title="下载服务器管理" Codebehind="DownServer.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td colspan="2" class="spacingtitle">
                <strong>
                    <pe:AlternateLiteral ID="AltrTitle" Text="添加服务器" AlternateText="修改服务器" runat="Server" />
                </strong>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="left" style="width: 40%">
                <strong>服务器名称：</strong><br />
                在此输入在前台显示的镜像服务器名，如广东下载、上海下载等。
            </td>
            <td class="tdbg" style="text-align: left; width: 60%;">
                <asp:TextBox ID="TxtServerName" runat="server" Width="290px"></asp:TextBox>
                <pe:RequiredFieldValidator ID="ValrServerName" runat="server" ErrorMessage="下载服务器名称不能为空"
                    ControlToValidate="TxtServerName"></pe:RequiredFieldValidator></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="left" style="width: 40%">
                <strong>服务器LOGO：</strong><br />
                输入服务器LOGO的绝对地址，如http://www.EasyOne.net/Soft/Images/ServerLogo.gif
            </td>
            <td class="tdbg" style="text-align: left; width: 60%;">
                <asp:TextBox ID="TxtServerLogo" runat="server" Width="290px"></asp:TextBox></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="left" style="width: 40%; height: 49px;">
                <strong>服务器地址：</strong><br />
                请认真输入正确的服务器地址。<br />
                如http://www.EasyOne.net/这样的地址
            </td>
            <td class="tdbg" style="text-align: left; width: 60%; height: 49px;">
                <asp:TextBox ID="TxtServerUrl" runat="server" Width="290px"></asp:TextBox>
                <pe:RequiredFieldValidator ID="ValrServerUrl" runat="server" ErrorMessage="下载服务器地址不能为空"
                    ControlToValidate="TxtServerUrl"></pe:RequiredFieldValidator></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="left" style="width: 40%">
                <strong>显示方式：</strong>
            </td>
            <td class="tdbg" style="text-align: left; width: 60%;">
                <asp:DropDownList ID="DropShowType" runat="server">
                    <asp:ListItem Value="0">显示名称</asp:ListItem>
                    <asp:ListItem Value="1">显示LOGO</asp:ListItem>
                </asp:DropDownList></td>
        </tr>
        <tr class="tdbg">
            <td style="text-align: center" colspan="2">
                <br />
                <asp:Button ID="EBtnSubmit" Text="保存" OnClick="EBtnSubmit_Click" runat="server" />&nbsp;&nbsp;
                <input name="BtnCancel" type="button" class="inputbutton" onclick="Redirect('DownServerManage.aspx')"
                    value=" 取消 " />
            </td>
        </tr>
    </table>
</asp:Content>
