<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.User.CardShow" Title="查看充值卡信息" Codebehind="CardShow.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table border="0" cellpadding="2" cellspacing="1" class="border" width="100%">
        <tr align="center">
            <td class="spacingtitle" colspan="2">
                <strong>查看充值卡信息</strong></td>
        </tr>
        <tr class="tdbg">
            <td style="text-align: right;">
                <b>充值卡类型：</b></td>
            <td style="width: 50%" id="TdCardType" runat="server">
            </td>
        </tr>
        <tr class="tdbg">
            <td style="text-align: right">
                <b>所属商品：</b></td>
            <td id="TdProductName" runat="server">
            </td>
        </tr>
        <tr class="tdbg">
            <td style="text-align: right">
                <b>充值卡卡号：</b></td>
            <td id="TdCardNum" runat="server">
            </td>
        </tr>
        <tr class="tdbg">
            <td style="text-align: right">
                <b>充值卡密码：</b></td>
            <td id="TdPassword" runat="server">
            </td>
        </tr>
        <tr class="tdbg">
            <td style="text-align: right">
                <b>充值卡面值：</b></td>
            <td id="TdMoney" runat="server">
            </td>
        </tr>
        <tr class="tdbg">
            <td style="text-align: right">
                <b>充值卡点数：</b></td>
            <td id="TdValidNum" runat="server">
            </td>
        </tr>
        <tr class="tdbg">
            <td style="text-align: right">
                <b>充值截止日期：</b></td>
            <td id="TdEndDate" runat="server">
            </td>
        </tr>
        <tr class="tdbg">
            <td style="text-align: right">
                <b>充值卡生成时间：</b></td>
            <td id="TdCreateTime" runat="server">
            </td>
        </tr>
        <tr class="tdbg">
            <td style="text-align: right">
                <b>充值卡状态：</b></td>
            <td id="TdStatus" runat="server">
            </td>
        </tr>
        <tr class="tdbg">
            <td style="text-align: right">
                <b>使用者：</b></td>
            <td id="TdUserName" runat="server">
            </td>
        </tr>
        <tr class="tdbg">
            <td style="text-align: right">
                <b>充值时间：</b></td>
            <td id="TdUseTime" runat="server">
            </td>
        </tr>
        <tr class="tdbg">
            <td style="text-align: right">
                <strong>代理商：</strong></td>
            <td id="TdAgentName" runat="server">
            </td>
        </tr>
    </table>
    <div style="width: 100%; text-align: center">
        <br />
        <input id="Button2" type="button" class="inputbutton" value="返回" onclick="javascript:history.go(-1)" /></div>
</asp:Content>
