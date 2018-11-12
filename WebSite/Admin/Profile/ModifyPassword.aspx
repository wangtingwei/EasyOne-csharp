<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPage.master" Inherits="EasyOne.WebSite.Admin.Profile.ModifyPassword" Codebehind="ModifyPassword.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <asp:ScriptManager ID="SmgeRegion" runat="server" EnablePartialRendering="true" />
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td colspan="2" class="spacingtitle">
                <strong>修改密码</strong>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" style="width: 50%" align="right">
                <strong>原 密 码：</strong>
            </td>
            <td>
                <asp:TextBox ID="TxtOldPassword" runat="server" TextMode="Password" />
                <pe:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="TxtOldPassword"
                    runat="server" ErrorMessage="原密码不能为空！" Display="Dynamic" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right">
                <strong>新 密 码：</strong>
            </td>
            <td>
                <asp:TextBox ID="TxtPassword" runat="server" TextMode="Password" />
                <pe:RequiredFieldValidator ID="ValrUserPassword" ControlToValidate="TxtPassword"
                    runat="server" ErrorMessage="密码不能为空！" Display="Dynamic" />
                <asp:Label ID="LabTip" runat="server" />
                <ajaxToolkit:PasswordStrength ID="PasswordStrength2" runat="server" TargetControlID="TxtPassword"
                    StrengthIndicatorType="BarIndicator" BarIndicatorCssClass="BarIndicator_TxtUserPassword"
                    BarBorderCssClass="BarBorder_TxtUserPassword" PreferredPasswordLength="8" MinimumNumericCharacters="1"
                    MinimumSymbolCharacters="1" RequiresUpperAndLowerCaseCharacters="true" DisplayPosition="RightSide" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right">
                <strong>确认密码：</strong>
            </td>
            <td>
                <asp:TextBox ID="TxtPassword2" runat="server" TextMode="Password" />
                <asp:CompareValidator ID="CompareValidator1" ControlToValidate="TxtPassword2" ControlToCompare="TxtPassword"
                    ErrorMessage="两次输入的密码不一致！" runat="server" />
            </td>
        </tr>
        <tr class="tdbgbottom">
            <td colspan="2">
                <asp:Button ID="BtnSubmit" runat="server" Text="修改" OnClick="BtnSubmit_Click" />
                &nbsp;&nbsp;
                <asp:Button ID="BtnCancle" runat="server" Text="取消" OnClick="BtnCancle_Click" ValidationGroup="BtnCancel" />
            </td>
        </tr>
    </table>
</asp:Content>
