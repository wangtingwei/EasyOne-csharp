<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.User.MinusPoint"
    MasterPageFile="~/Admin/MasterPage.master" Title="扣除点券" Codebehind="MinusPoint.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <div style="text-align: center">
        <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
            <tr align="center" class="spacingtitle">
                <td colspan="2">
                    <asp:Label ID="LblTitle" runat="server" Text="扣除点券" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr class="tdbg">
                <td align="right" style="width: 100%;" colspan="2">
                    <pe:ShowUserInfo ID="showUserInfo" runat="server" />
                </td>
            </tr>
            <tr class="tdbg">
                <td align="right" style="width: 15%;" class="tdbgleft">
                    扣除<pe:ShowPointName ID="ShowPointName" runat="server"></pe:ShowPointName>数：</td>
                <td align="left">
                    <asp:TextBox ID="TxtPoint" runat="server" Text="100"  Columns="8" MaxLength="8"></asp:TextBox>
                    <pe:RequiredFieldValidator ID="ValrPoint" ControlToValidate="TxtPoint" runat="server"
                        ErrorMessage="扣除点券数不能为空" Display="Dynamic"></pe:RequiredFieldValidator>                    
                    <asp:RegularExpressionValidator ID="ValgPoint" runat="server" ControlToValidate="TxtPoint"
                    ErrorMessage="只能输入正整数" ValidationExpression="^([0-9])(\d{0,})(\d{0,})$" Display="Dynamic" />
                </td>
            </tr>
            <tr class="tdbg">
                <td align="right" style="width: 15%;" class="tdbgleft">
                    扣除<pe:ShowPointName ID="ShowPointName1" runat="server"></pe:ShowPointName>原因：</td>
                <td align="left">
                    <asp:TextBox ID="TxtReason" runat="server" Height="50px" Width="300px" TextMode="MultiLine"
                        MaxLength="255"></asp:TextBox>
                    <pe:RequiredFieldValidator ID="ValrReason" ControlToValidate="TxtReason" runat="server"
                        ErrorMessage="扣除点券原因不能为空"></pe:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="tdbg">
                <td style="width: 15%" class="tdbgleft" align="right">
                    内部记录：</td>
                <td align="left">
                    <asp:TextBox ID="TxtMemo" runat="server" Width="400px" Columns="50" Height="60px"
                        Rows="4" TextMode="MultiLine" MaxLength="255"></asp:TextBox>
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
                    <asp:Button ID="EBtnSubmit" Text="执行扣除" OnClick="EBtnSubmit_Click" runat="server" />&nbsp;&nbsp;
                </td>
            </tr>
            <asp:HiddenField ID="HdnUsersId" runat="server" />
        </table>
    </div>
</asp:Content>
