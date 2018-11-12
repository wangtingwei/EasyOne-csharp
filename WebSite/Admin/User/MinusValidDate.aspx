<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.User.MinusValidDate"
    MasterPageFile="~/Admin/MasterPage.master" Title="扣除用户有效期" Codebehind="MinusValidDate.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <div style="text-align: center">
        <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
            <tr align="center" class="title">
                <td colspan="2">
                    <asp:Label ID="LblTitle" runat="server" Text=" 扣除用户有效期 " Font-Bold="True"></asp:Label></td>
            </tr>
            <tr class="tdbg">
                <td align="right" style="width: 100%;" colspan="2">
                    <pe:ShowUserInfo ID="showUserInfo" runat="server" />
                </td>
            </tr>
            <tr class="tdbg">
                <td align="right" style="width: 15%;" class="tdbgleft">
                    <asp:Label ID="LblValid" Text="扣除有效期：" runat="server"></asp:Label>
                </td>
                <td align="left" style="width: 586px">
                    <asp:RadioButton ID="RadValidType" runat="server" Text="指定期限：" GroupName="ValidType"
                        Checked="true" />
                    &nbsp;<asp:TextBox ID="TxtValidNum" runat="server" Width="30px" MaxLength = "4" ></asp:TextBox>
                    <asp:DropDownList ID="DropValidUnit" runat="server">
                        <asp:ListItem Selected="True" Value="1">天</asp:ListItem>
                        <asp:ListItem Value="2">月</asp:ListItem>
                        <asp:ListItem Value="3">年</asp:ListItem>
                    </asp:DropDownList>
                    <br />
                    <asp:RadioButton ID="RadValidType2" GroupName="ValidType" runat="server" Text="有效期归零" />（当某个会员的有效期是“无限期”时，可以通过归零重设其有效期期限）
                </td>
            </tr>
            <tr class="tdbg">
                <td align="right" style="width: 15%;" class="tdbgleft">
                    请输入扣除原因：</td>
                <td align="left">
                    <asp:TextBox ID="TxtReason" runat="server" Height="50px" Width="300px" TextMode="MultiLine"></asp:TextBox>
                    <pe:RequiredFieldValidator ID="ValrReason" ControlToValidate="TxtReason" runat="server"
                        ErrorMessage="扣除有效期原因不能为空"></pe:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="tdbg">
                <td style="width: 15%" class="tdbgleft" align="right">
                    内部记录：</td>
                <td align="left">
                    <asp:TextBox ID="TxtMemo" runat="server" Width="400px" Columns="50" Height="60px"
                        Rows="4" TextMode="MultiLine"></asp:TextBox>
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
                    <asp:Button ID="EBtnSubmit" Text="扣除有效期" OnClick="EBtnSubmit_Click" runat="server" />&nbsp;&nbsp;
                </td>
            </tr>
            <asp:HiddenField ID="HdnUsersId" runat="server" />
        </table>
    </div>
</asp:Content>
