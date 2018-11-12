<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.User.RegisterPayPassword" Codebehind="RegisterPayPassword.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>设置支付密码</title>
    <style type="text/css">
        .style1
        {
            width: 92px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style=" margin-left:200px;">
    <br/>
            <table id="Table2" style="border-collapse: collapse; width: 69%;" 
                cellspacing="1" cellpadding="2" border="0">
                <tr>
                    <td class="style1">
                        支付密码：
                    </td>
                    <td>
                        <asp:TextBox ID="TxtPassword" runat="server" Width="150" TextMode="Password"></asp:TextBox>
                         <pe:RequiredFieldValidator ID="RegLogOnPassword" runat="server" 
                            ValidationGroup="valsLogin" ControlToValidate="TxtPassword" 
                            SetFocusOnError="false"  ErrorMessage="密码不能为空" Display="Dynamic"></pe:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidatorPassword" runat="server" ControlToValidate="TxtPassword" SetFocusOnError="false"  ValidationExpression="[\S]{6,}" ErrorMessage="密码至少6位" Display="Dynamic"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        确认密码：
                    </td>
                    <td>
                         <asp:TextBox ID="TxtPwdConfirm" runat="server" Width="150" TextMode="Password"></asp:TextBox>
                         <pe:RequiredFieldValidator ID="ReqTxtPwdConfirm" runat="server" ControlToValidate="TxtPwdConfirm" ValidationGroup="valsLogin" SetFocusOnError="false"  ErrorMessage="确认密码不能为空" Display="Dynamic"></pe:RequiredFieldValidator>
                         <asp:CompareValidator ID="ValCompPassword" runat="server" ControlToValidate="TxtPwdConfirm" ValidationGroup="valsLogin"  ControlToCompare="TxtPassword" Operator="Equal" SetFocusOnError="false"  ErrorMessage="两次密码输入不一致"></asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                  <td colspan="2" style="margin-left:50px;"><br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                      <asp:Button ID="BtnSetPayPassword" runat="server" Text="确定" 
                          ValidationGroup="valsLogin" onclick="BtnSetPayPassword_Click"/></td>
                </tr>
            </table>
    </div>
    </form>
</body>
</html>
