<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.User.BankrollItemListSearch" Title="资金明细查询" Codebehind="BankrollItemListSearch.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td colspan="2" class="spacingtitle">
                <b>资金明细复杂查询</b>
            </td>
        </tr>
        <tr class="tdbg">
            <td style="width: 120px; text-align: right" class="tdbgleft">
                ID范围：</td>
            <td>
                起始ID
                <asp:TextBox ID="TxtBeginId" runat="server" Width="78px"></asp:TextBox>&nbsp;<asp:CompareValidator
                    ID="ValcStartID" runat="server" ControlToValidate="TxtBeginId" Display="Dynamic"
                    ErrorMessage="请输入正确ID号！" Operator="GreaterThan" SetFocusOnError="True" Type="Integer" ValueToCompare="0"></asp:CompareValidator>终止ID<asp:TextBox
                    ID="TxtEndId" runat="server" Width="78px"></asp:TextBox>
                <asp:CompareValidator ID="ValcEndID" runat="server" ControlToValidate="TxtEndId"
                    Display="Dynamic" ErrorMessage="请输入正确ID号！" Operator="GreaterThan" SetFocusOnError="True"
                    Type="Integer" ValueToCompare="0"></asp:CompareValidator></td>
        </tr>
        <tr class="tdbg">
            <td style="width: 120px; text-align: right" class="tdbgleft">
                日期范围：</td>
            <td>
                起始日期
                <pe:DatePicker ID="DpkBegin" runat="server" Width="70px"></pe:DatePicker>
                &nbsp; &nbsp; &nbsp;结束日期
                <pe:DatePicker ID="DpkEnd" runat="server" Width="70px"></pe:DatePicker>
                <asp:CompareValidator ID="ValcBegin" runat="server" ControlToValidate="DpkBegin"
                    Display="Dynamic" ErrorMessage="起始日期格式有错误！" Operator="DataTypeCheck" SetFocusOnError="True"
                    Type="Date"></asp:CompareValidator>
                <asp:CompareValidator ID="ValcEnd" runat="server" ControlToValidate="DpkEnd" Display="Dynamic"
                    ErrorMessage="结束日期格式有错误！" Operator="DataTypeCheck" SetFocusOnError="True" Type="Date"></asp:CompareValidator></td>
        </tr>
        <tr class="tdbg">
            <td style="width: 120px; text-align: right" class="tdbgleft">
                客户名称：</td>
            <td>
                <asp:TextBox ID="TxtClientName" runat="server"></asp:TextBox></td>
        </tr>
        <tr class="tdbg">
            <td style="width: 120px; text-align: right" class="tdbgleft">
                用户名称：</td>
            <td>
                <asp:TextBox ID="TxtUserName" runat="server"></asp:TextBox></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" style="width: 120px; text-align: right">
                银行名称：</td>
            <td>
                <asp:TextBox ID="TxtBank" runat="server"></asp:TextBox></td>
        </tr>
        <tr class="tdbg" style="height: 40px; text-align: center">
            <td colspan="6">
                <asp:Button ID="BtnSearch" runat="server" Text="查询" OnClick="BtnSearch_Click" />
                &nbsp;
                <asp:Button ID="BtnExportExcel" runat="server" Text="导出到EXCEL" OnClick="BtnExportExcel_Click" /></td>
        </tr>
    </table>
</asp:Content>
