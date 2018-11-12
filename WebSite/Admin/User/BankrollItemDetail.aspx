<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.User.BankrollItemDetail"
    Title="查看资金明细记录详情" Codebehind="BankrollItemDetail.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="server">
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td colspan="2" class="spacingtitle">
                <b>查看资金明细记录详情</b>
            </td>
        </tr>
        <tr class="tdbg">
            <td style="width: 15%" class="tdbgleft" align="right">
                交易时间：</td>
            <td>
                <asp:Label ID="LblDateAndTime" runat="server"></asp:Label></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right">
                客户名称：</td>
            <td>
                <asp:Label ID="LblClientName" runat="server"></asp:Label></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right">
                用户名：</td>
            <td>
                <asp:Label ID="LblUserName" runat="server"></asp:Label></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right">
                交易方式：</td>
            <td>
                <asp:Label ID="LblMoneyType" runat="server"></asp:Label></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right">
                币种：</td>
            <td>
                <asp:Label ID="LblCurrencyType" runat="server"></asp:Label></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right">
                收入金额：</td>
            <td>
                <asp:Label ID="LblIncomeMoney" runat="server" Text="0"></asp:Label>
                元</td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right">
                支出金额：</td>
            <td>
                <asp:Label ID="LblPaymentMoney" runat="server" Text="0"></asp:Label>
                元</td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right">
                银行名称：</td>
            <td>
                <asp:Label ID="LblBank" runat="server"></asp:Label></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right">
                资金明细状态：</td>
            <td>
                <asp:Label ID="LblStatus" runat="server"></asp:Label></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right">
                IP地址：</td>
            <td>
                <asp:Label ID="LblIP" runat="server"></asp:Label></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right">
                录入者：</td>
            <td>
                <asp:Label ID="LblInputer" runat="server"></asp:Label></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right">
                录入时间：</td>
            <td>
                <asp:Label ID="LblLogTime" runat="server"></asp:Label></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right">
                备注：</td>
            <td>
                <asp:Label ID="LblRemark" runat="server"></asp:Label></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right">
                内部记录：</td>
            <td>
                <asp:Label ID="LblMemo" runat="server"></asp:Label></td>
        </tr>
        <tr class="tdbgbottom">
            <td colspan="2">
            <pe:ExtendedButton runat="server" ID="BtnConfirm" OperateCode="BankrollItemList" Visible="false"
             IsChecked="true" Text="确认" onclick="BtnConfirm_Click" />&nbsp;
             <pe:ExtendedButton runat="server" ID="BtnDelete" OperateCode="BankrollItemList"  Visible="false"
              IsChecked="true" Text="删除" onclick="BtnDelete_Click" onclientclick="return confirm('是否确定要删除这条汇款通知记录？')" />&nbsp;
            <input type="button" value="返回" class="inputbutton" onclick="javascript:history.back();" /></td>
        </tr>
    </table>
</asp:Content>
