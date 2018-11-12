<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.User.RegCompany" Codebehind="RegCompany.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>注册企业</title>
</head>
<body>
    <pe:UserNavigation ID="UserCenterNavigation" Tab="user" runat="server" />
    <pe:ExtendedSiteMapPath ID="YourPosition" SiteMapProvider="UserMapProvider" runat="server" />
    <form id="form1" runat="server">
        <div style="text-align: center">
            <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
                <tr align="center" class="title">
                    <td colspan="2">
                        <asp:Label ID="LblTitle" runat="server" Text=" 注册我的企业（第一步） " Font-Bold="True"></asp:Label></td>
                </tr>
                <tr class="tdbg">
                    <td align="right" style="width: 40%;" class="tdbgleft">
                        请输入要注册的企业完整名称：</td>
                    <td align="left">
                        <asp:TextBox ID="TxtCompanyName" MaxLength="200" Width="220px" runat="server"></asp:TextBox>
                        <pe:RequiredFieldValidator ID="ValrCompanyName" runat="server" Display="dynamic"
                            ControlToValidate="TxtCompanyName" ErrorMessage="企业名称不能为空！"></pe:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="ValeCompanyName" Display="Dynamic" ControlToValidate="TxtCompanyName"
                            ValidationExpression="^[\w\W\u4e00-\u9fa5]{6,100}$" SetFocusOnError="true" runat="server"
                            ErrorMessage="企业名称的长度不能小于6位大于100位"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr align="center" class="tdbg">
                    <td colspan="2">
                        <asp:Button ID="BtnSubmit" runat="server" Text="下一步" PostBackUrl="~/User/Company/RegCompany2.aspx" />&nbsp;&nbsp;&nbsp;&nbsp;
                        <input type="button" onclick="javascript:history.go(-1)" value="取消" class="inputbutton" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
