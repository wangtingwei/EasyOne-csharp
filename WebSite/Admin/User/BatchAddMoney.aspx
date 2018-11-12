<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPage.master"
    Inherits="EasyOne.WebSite.Admin.User.BatchAddMoney" Codebehind="BatchAddMoney.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td colspan="2" class="spacingtitle">
                <b>
                    <asp:Label ID="LblMsg" runat="server" Text="批量添加奖金"></asp:Label></b>
            </td>
        </tr>
        <tr class="tdbg">
            <td style="width: 15%;" class="tdbgleft">
                <strong>选择会员：</strong></td>
            <td>
                <pe:SelectUser ID="SelectUser" runat="server" />
            </td>
        </tr>
        <tr class="tdbg">
            <td style="width: 15%;" class="tdbgleft">
                <strong>金额：</strong></td>
            <td align="left">
                <asp:TextBox ID="TxtMoney" runat="server" Columns="7" MaxLength ="7"></asp:TextBox>
                元
                <pe:RequiredFieldValidator ID="ValrMoney" ControlToValidate="TxtMoney" runat="server"
                    ErrorMessage="奖励金额不能为空" Display="Dynamic"></pe:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="ValeMoney" runat="server" ControlToValidate="TxtMoney"
                    ErrorMessage="只能输入货币字符" ValidationExpression="^-?[0-9]+(\.?[0-9]{1,2})?" Display="Dynamic"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr class="tdbg">
            <td style="width: 15%;" class="tdbgleft">
                <strong>原因：</strong></td>
            <td align="left">
                <asp:TextBox ID="TxtReason" runat="server" Height="50px" Width="300px" TextMode="MultiLine"></asp:TextBox>
                <pe:RequiredFieldValidator ID="ValrMRemark" ControlToValidate="TxtReason" runat="server"
                    ErrorMessage="奖励原因不能为空"></pe:RequiredFieldValidator>
            </td>
        </tr>
        <tr class="tdbg">
            <td style="width: 15%" class="tdbgleft">
                <strong>内部记录：</strong></td>
            <td>
                <asp:TextBox ID="TxtMemo" runat="server" Width="400px" Columns="50" Height="60px"
                    Rows="4" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg">
            <td>
            </td>
            <td align="left">
                <asp:CheckBox ID="ChkSaveItem" runat="server" Checked="True" />
                <strong>为每个会员记录明细记录（推荐）</strong>
            </td>
        </tr>
        <tr class="tdbg">
            <td style="height: 40px;" colspan="2" align="center">
                <asp:Button ID="EBtnSubmit" Text="执行批量操作" OnClick="EBtnSubmit_Click" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
