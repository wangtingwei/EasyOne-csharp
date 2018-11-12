<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.User.ExchangeValid"
    MasterPageFile="~/Admin/MasterPage.master" Title="会员有效期兑换" Codebehind="ExchangeValid.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <div style="text-align: center">
        <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
            <tr align="center" class="title">
                <td colspan="2">
                    <asp:Label ID="LblTitle" runat="server" Text=" 会员有效期兑换 " Font-Bold="True"></asp:Label></td>
            </tr>
            <tr class="tdbg">
                <td align="right" style="width: 100%;" colspan="2">
                    <pe:ShowUserInfo ID="showUserInfo" runat="server" />
                </td>
            </tr>
            <tr class="tdbg">
                <td align="right" style="width: 15%;" class="tdbgleft">
                    <asp:Label ID="LblValid" Text="设置有效期：" runat="server"></asp:Label>
                </td>
                <td align="left" style="width: 586px">
                    <asp:RadioButton ID="RadValidType" runat="server" Text="指定期限：" GroupName="ValidType"
                        Checked="true" />
                    &nbsp;<asp:TextBox ID="TxtValidNum" runat="server" Width="30px" MaxLength="4"></asp:TextBox>
                    <asp:DropDownList ID="DropValidUnit" runat="server">
                        <asp:ListItem Selected="True" Value="1">天</asp:ListItem>
                        <asp:ListItem Value="2">月</asp:ListItem>
                        <asp:ListItem Value="3">年</asp:ListItem>
                    </asp:DropDownList>
                    <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;若目前会员尚未到期，则追加相应天数
                    <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;若目前会员已经过了有效期，则有效期从续费之日起重新计数。<br />
                    <asp:RadioButton ID="RadValidType2" GroupName="ValidType" runat="server" Text="改为无限期" />
                </td>
            </tr>
            <tr class="tdbg">
                <td align="right" style="width: 15%;" class="tdbgleft">
                    同时减去资金：</td>
                <td align="left">
                    <asp:TextBox ID="TxtMoney" runat="server"></asp:TextBox>元
                    <pe:RequiredFieldValidator ID="ValrMoney" ControlToValidate="TxtMoney" runat="server"
                        ErrorMessage="减去资金不能为空" Display="Dynamic"></pe:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="ValeMoney" runat="server" ControlToValidate="TxtMoney"
                        ErrorMessage="只能输入货币字符，而且不能为负数" ValidationExpression="\d+(\.\d\d)?" Display="Dynamic"></asp:RegularExpressionValidator>
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
