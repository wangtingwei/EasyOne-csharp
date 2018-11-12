<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.User.ExchangePoint"
    MasterPageFile="~/Admin/MasterPage.master" Title="会员点券兑换" Codebehind="ExchangePoint.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <div style="text-align: center">
        <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
            <tr align="center" class="title">
                <td colspan="2">
                    <asp:Label ID="LblTitle" runat="server" Text=" 会员点券兑换 " Font-Bold="True"></asp:Label></td>
            </tr>
            <tr class="tdbg">
                <td align="right" style="width: 100%;" colspan="2">
                    <pe:ShowUserInfo ID="showUserInfo" runat="server" />
                </td>
            </tr>
            <tr class="tdbg">
                <td align="right" style="width: 15%;" class="tdbgleft">
                    增加<%=m_PointName%>：</td>
                <td align="left">
                    <asp:TextBox ID="TxtPoint" runat="server" Text="100" MaxLength="6" Width="50px"></asp:TextBox>
                    <pe:RequiredFieldValidator ID="ValrPoint" ControlToValidate="TxtPoint" runat="server"
                        ErrorMessage="增加点券不能为空" Display="Dynamic"></pe:RequiredFieldValidator>
                    <asp:RangeValidator ID="ValgTxtPoint" ControlToValidate="TxtPoint" runat="server"
                        ErrorMessage="点券范围在1—100000之间" Type="Double" Display="Dynamic" MaximumValue="100000"
                        MinimumValue="1"></asp:RangeValidator>
                </td>
            </tr>
            <tr class="tdbg">
                <td align="right" style="width: 15%;" class="tdbgleft">
                    同时减去资金：</td>
                <td align="left">
                    <asp:TextBox ID="TxtMoney" runat="server" MaxLength="6" Width="50px"></asp:TextBox>元
                    <pe:RequiredFieldValidator ID="ValrMoney" ControlToValidate="TxtMoney" runat="server"
                        ErrorMessage="减去资金不能为空" Display="Dynamic"></pe:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="ValeMoney" runat="server" ControlToValidate="TxtMoney"
                        ErrorMessage="只能输入货币字符" ValidationExpression="^-?[0-9]+(\.?[0-9]{1,4})?" Display="Dynamic"></asp:RegularExpressionValidator>
                    <asp:RangeValidator ID="ValrMoney2" ControlToValidate="TxtMoney" runat="server" ErrorMessage="资金范围在0—100000之间"
                        Type="Double" Display="Dynamic" MaximumValue="100000" MinimumValue="0"></asp:RangeValidator>
                </td>
            </tr>
            <tr class="tdbg">
                <td style="width: 15%" class="tdbgleft" align="right">
                </td>
                <td align="left">
                    <asp:CheckBox ID="ChkIsSendMessage" runat="server" />同时发送手机短信通知会员
                </td>
            </tr>
            <tr align="center" class="tdbg">
                <td style="height: 30px;" colspan="2">
                    <asp:Button ID="EBtnSubmit" Text="执行兑换" OnClick="EBtnSubmit_Click" runat="server" />
                </td>
            </tr>
            <asp:HiddenField ID="HdnUsersId" runat="server" />
        </table>
    </div>
</asp:Content>
