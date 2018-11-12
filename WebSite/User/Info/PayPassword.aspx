<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.User.Info.PayPassword" Codebehind="PayPassword.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>修改预付款支付密码</title>
</head>
<body>
    <pe:UserNavigation Tab="user" ID="UserCenterNavigation" runat="server" />
    <pe:ExtendedSiteMapPath ID="YourPosition" SiteMapProvider="UserMapProvider" runat="server" />
    <form id="form1" runat="server"><asp:ScriptManager ID="ScriptManagePayPassword" runat="server" />
        <table width="100%" cellpadding="2" cellspacing="1" class="border"> 
                 <tr align="center" class="spacingtitle" valign="top">
                <td colspan="2">
                    <strong>修改预付款支付密码</strong>
                </td>
            </tr>
           
            </table>
            <table width="100%" cellpadding="2" cellspacing="1" class="border" id="TblPayPassword" runat="server">
             <tr class="tdbg">
                <td class="tdbgleft" align="right" style="width:50%">
                    用 户 名：
                </td>
                <td>
                    <asp:Label ID="LblUserName" runat="server" Text="" />
                </td>
            </tr>
            <tr class="tdbg" id="trOldPassword" runat="server">
                <td class="tdbgleft" align="right" style="width:50%">
                    旧 密 码：
                </td> 
                <td>
                    <asp:TextBox ID="TxtOldPassword" runat="server" TextMode="Password" />
                    <pe:RequiredFieldValidator ID="ValrReason" ControlToValidate="TxtOldPassword" runat="server"
                        ErrorMessage="旧密码不能为空！" />
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft" align="right">
                    新 密 码：
                </td>
                <td>
                    <asp:TextBox ID="TxtPasswords" runat="server" TextMode="Password" MaxLength="20" />
                    <pe:RequiredFieldValidator ID="ValrPassword" ControlToValidate="TxtPasswords" runat="server"
                        ErrorMessage="新密码不能为空！" Display="Dynamic" />
                    <asp:RegularExpressionValidator ID="RegularExpressionPassword" runat="server" ControlToValidate="TxtPasswords"
                        ErrorMessage="请输入新密码(至少6位)！" ValidationExpression="^.{6,}$" Display="Dynamic" />
                    <ajaxToolkit:PasswordStrength ID="PasswordStrength2" runat="server" TargetControlID="TxtPasswords"
                        StrengthIndicatorType="BarIndicator" BarIndicatorCssClass="BarIndicator_TxtUserPassword"
                        BarBorderCssClass="BarBorder_TxtUserPassword" PreferredPasswordLength="8" MinimumNumericCharacters="1"
                        MinimumSymbolCharacters="1" RequiresUpperAndLowerCaseCharacters="true" DisplayPosition="RightSide" />
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft" align="right">
                    确认密码：
                </td>
                <td>
                    <asp:TextBox ID="TxtConfirmPassword" runat="server" TextMode="Password" MaxLength="20" />
                    <pe:RequiredFieldValidator ID="RequiredFieldValidatorTxtConfirmPassword" ControlToValidate="TxtConfirmPassword"
                        runat="server" ErrorMessage="确认密码不能为空！" />
                </td>
            </tr>
            <tr class="tdbgbottom">
                <td colspan="2">
                    <pe:ExtendedButton ID="EBtnSubmit" Text="修改" IsChecked="false" OnClick="EBtnSubmit_Click"
                        runat="server" />
                </td>
            </tr>
        </table>
        <table width="100%">
        <tr class="tdbg">
        <td align="center">
            <pe:ExtendedLabel HtmlEncode="false" ID="LblMessage" runat="server"></pe:ExtendedLabel></td>
        </tr>
        </table>
    </form>
</body>
</html>
