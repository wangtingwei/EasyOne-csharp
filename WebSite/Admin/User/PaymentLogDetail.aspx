<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.User.PaymentLogDetail"
    Title="查看在线支付记录详情" Codebehind="PaymentLogDetail.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="server">
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td colspan="2" class="spacingtitle">
                <b>查看在线支付记录详情</b>
            </td>
        </tr>
        <tr class="tdbg">
            <td style="width: 15%" class="tdbgleft" align="right">
                支付序号：</td>
            <td>
                <asp:Label ID="LblPaymentNum" runat="server"></asp:Label></td>
        </tr>
        <tr class="tdbg">
            <td style="width: 15%" class="tdbgleft" align="right">
                用户名：</td>
            <td>
                <asp:Label ID="LblUserName" runat="server"></asp:Label></td>
        </tr>
        <tr class="tdbg">
            <td style="width: 15%" class="tdbgleft" align="right">
                支付平台：</td>
            <td>
                <asp:Label ID="LblPlatform" runat="server"></asp:Label></td>
        </tr>
        <tr class="tdbg">
            <td style="width: 15%" class="tdbgleft" align="right">
                交易时间：</td>
            <td>
                <asp:Label ID="LblPayTime" runat="server"></asp:Label></td>
        </tr>
        <tr class="tdbg">
            <td style="width: 15%" class="tdbgleft" align="right">
                汇款金额：</td>
            <td>
                <asp:Label ID="LblMoneyPay" runat="server"></asp:Label></td>
        </tr>
        <tr class="tdbg">
            <td style="width: 15%" class="tdbgleft" align="right">
                实际转账金额：</td>
            <td>
                <asp:Label ID="LblMoneyTrue" runat="server"></asp:Label>
                元</td>
        </tr>
        <tr class="tdbg">
            <td style="width: 15%" class="tdbgleft" align="right">
                交易状态：</td>
            <td>
                <asp:Label ID="LblStatus" runat="server"></asp:Label></td>
        </tr>
        <tr class="tdbg">
            <td style="width: 15%" class="tdbgleft" align="right">
                银行信息：</td>
            <td>
                <asp:Label ID="LblPlatformInfo" runat="server"></asp:Label></td>
        </tr>
        <tr class="tdbg">
            <td style="width: 15%" class="tdbgleft" align="right">
                备注：</td>
            <td>
                <asp:Label ID="LblRemark" runat="server"></asp:Label></td>
        </tr>
        <tr class="tdbgbottom">
            <td colspan="2">
                <asp:Button ID="BtnReturn" runat="server" Text="返回" OnClick="BtnReturn_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
