<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    ValidateRequest="false" Inherits="EasyOne.WebSite.Admin.Accessories.SiteInfo"
    Title="网站信息配置" Codebehind="SiteInfo.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="server">
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td colspan="2" class="spacingtitle">
                <strong>网站信息配置</strong>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" style="width: 40%">
                <strong>网站名称：</strong><br />
            </td>
            <td>
                <asp:TextBox ID="TxtSiteName" runat="server" MaxLength="50" Columns="50"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>网站标题：</strong><br />
            </td>
            <td>
                <asp:TextBox ID="TxtSiteTitle" runat="server" MaxLength="50" Columns="50"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>网站地址：</strong><br />
            </td>
            <td>
                <asp:TextBox ID="TxtSiteUrl" runat="server" MaxLength="255" Columns="50"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>LOGO地址：</strong><br />
            </td>
            <td>
                <asp:TextBox ID="TxtLogoUrl" runat="server" MaxLength="255" Columns="50"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>Banner地址：</strong><br />
            </td>
            <td>
                <asp:TextBox ID="TxtBannerUrl" runat="server" MaxLength="255" Columns="50"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>站长姓名：</strong><br />
            </td>
            <td>
                <asp:TextBox ID="TxtWebmaster" runat="server" MaxLength="20" Columns="50"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>站长信箱：</strong></td>
            <td>
                <asp:TextBox ID="TxtWebmasterEmail" runat="server" MaxLength="100" Columns="50">
                </asp:TextBox>
                <pe:EmailValidator ID="Vmail" ControlToValidate="TxtWebmasterEmail" ErrorMessage="错误的电子邮件格式！"
                    runat="server"></pe:EmailValidator></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>版权信息：</strong><br />
                支持HTML标记</td>
            <td>
                <asp:TextBox ID="TxtCopyright" TextMode="MultiLine" runat="server" Height="80px"
                    Width="400px"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>网站META关键词：</strong><br />
                针对搜索引擎设置的关键词</td>
            <td>
                <asp:TextBox ID="TxtMeta_Keywords" TextMode="MultiLine" runat="server" Height="80px"
                    Width="400px"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>网站META网页描述：</strong><br />
                针对搜索引擎设置的网页描述</td>
            <td>
                <asp:TextBox ID="TxtMeta_Description" TextMode="MultiLine" runat="server" Height="80px"
                    Width="400px"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbgbottom">
            <td colspan="2">
                <asp:Button ID="BtnSubmit" runat="server" Text="保存设置" OnClick="BtnSubmit_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
