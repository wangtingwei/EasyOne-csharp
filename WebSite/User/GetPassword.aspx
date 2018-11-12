<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.GetPassword" Codebehind="GetPassword.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>获取密码</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:PlaceHolder ID="PnlStep1" runat="server" Visible="false">
            <asp:TextBox ID="TxtUserName" runat="server"></asp:TextBox><pe:RequiredFieldValidator ID="ValrTxtUserName" runat="server" ErrorMessage="请输入用户名！" ControlToValidate="TxtUserName" Display="dynamic" SetFocusOnError="True"></pe:RequiredFieldValidator>
            <asp:Button ID="BtnStep1" runat="server" Text="下一步" OnClick="BtnStep1_Click" />
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="PnlSendToEmail" runat="server" Visible="false">
            <asp:RadioButtonList ID="RadPwdType" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Text="登陆密码" Value="0" Selected="True"></asp:ListItem>
                <asp:ListItem Text="预付款支付密码" Value="1"></asp:ListItem>
             </asp:RadioButtonList>
            <asp:Button ID="btnSendEmail" runat="server" Text="发送新密码至邮箱" OnClick="btnSendEmail_Click"  />
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="PnlStep2" runat="server" Visible="false">
            <asp:Literal ID="LitQuestion" runat="server"></asp:Literal>
            <asp:TextBox ID="TxtAnswer" runat="server"></asp:TextBox>
            <asp:TextBox ID="TxtValidateCode" runat="server"></asp:TextBox><pe:ValidateCode ID="VcodeLogOn" runat="server" RefreshLinkToolTip="看不清楚，换一个" />
            <pe:RequiredFieldValidator ID="ValrValidateCode" runat="server" ErrorMessage="请输入验证码！" ControlToValidate="TxtValidateCode" Display="dynamic" SetFocusOnError="True"></pe:RequiredFieldValidator>
             <asp:RadioButtonList ID="RadListPwdType" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Text="登陆密码" Value="0" Selected="True"></asp:ListItem>
                <asp:ListItem Text="预付款支付密码" Value="1"></asp:ListItem>
             </asp:RadioButtonList>
            <asp:Button ID="BtnStep2" runat="server" Text="完成" OnClick="BtnStep2_Click" />
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="PnlStep3" runat="server" Visible="false">
            <asp:TextBox ID="TxtPassword" TextMode="Password" runat="server"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidatorPassword" runat="server" ControlToValidate="TxtPassword" SetFocusOnError="True" Display="Dynamic" ValidationGroup="valsPassword" ValidationExpression="[\S]{6,}" ErrorMessage="密码至少6位"></asp:RegularExpressionValidator>
            <asp:TextBox ID="TxtConfirmPassword" runat="server" TextMode="Password"></asp:TextBox>
            <asp:CompareValidator ID="CompareValTxtConfirmPassword" ControlToValidate="TxtConfirmPassword" ValidationGroup="valsPassword" ControlToCompare="TxtPassword" Display="Dynamic" Type="String" Operator="Equal" runat="server" ErrorMessage="两次密码输入不一致！"></asp:CompareValidator>
            <asp:Button ID="BtnStep3" runat="server" Text="修改密码" ValidationGroup="valsPassword" OnClick="BtnStep3_Click" />
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="PalChangePayPassword" runat="server" Visible="false">
            <asp:TextBox ID="txtNewPayPassword" TextMode="Password" runat="server"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegNewPayPassword" ValidationGroup="valsPayPassword" runat="server" ControlToValidate="txtNewPayPassword" SetFocusOnError="True" Display="Dynamic" ValidationExpression="[\S]{6,}" ErrorMessage="密码至少6位"></asp:RegularExpressionValidator>
            <asp:TextBox ID="txtConfirmNewPayPassword" runat="server" TextMode="Password"></asp:TextBox><asp:CompareValidator ID="CompNewPayPassword" ValidationGroup="valsPayPassword" ControlToValidate="txtNewPayPassword" ControlToCompare="txtConfirmNewPayPassword" Display="Dynamic" Type="String" Operator="Equal" runat="server" ErrorMessage="两次密码输入不一致！"></asp:CompareValidator>
            <asp:Button ID="btnChangePayPassword" runat="server" Text="修改预付款支付密码" ValidationGroup="valsPayPassword" OnClick="btnChangePayPassword_Click" />
        </asp:PlaceHolder>
    </form>
</body>
</html>
