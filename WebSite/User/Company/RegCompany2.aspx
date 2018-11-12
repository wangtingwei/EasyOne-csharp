<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.User.RegCompany2" Codebehind="RegCompany2.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>注册企业</title>
</head>
<body>
    <pe:UserNavigation ID="UserCenterNavigation" Tab="user" runat="server" />
    <pe:ExtendedSiteMapPath ID="YourPosition" SiteMapProvider="UserMapProvider" runat="server" />
    <form id="form1" runat="server">
        <asp:ScriptManager ID="SmgeRegion" runat="server" EnablePartialRendering="true">
        </asp:ScriptManager>
        <div style="text-align: center">
            <asp:Panel ID="PnlSame" runat="server" Visible="false">
                <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
                    <tr align="center" class="title">
                        <td colspan="4">
                            <asp:Label ID="LblTitle" runat="server" Text=" 注册我的企业（第二步） " Font-Bold="True"></asp:Label></td>
                    </tr>
                    <tr align="left" class="tdbg">
                        <td colspan="4">
                            已经存在与<asp:Label ID="LblName" runat="server"></asp:Label>相同或相近的企业（详见下表）。您要注册的企业是否就在其中？<br />
                            如果是，请在对应企业下面点击[加入此企业]按钮向此企业创建人发送请求，并等待他的通过。企业创建人在审核您的申请时，可以查看您的有关信息。申请通过后，您将成为我们的企业会员，享受更多服务！
                            <br />
                            如果不是，请返回上一步输入其他企业名称。
                        </td>
                    </tr>
                    <tr class="tdbg">
                        <td align="right" style="width: 20%;" class="tdbgleft">
                            企业名称：</td>
                        <td align="left" style="width: 30%;">
                            <asp:Label ID="LblCompanyName" runat="server"></asp:Label>
                        </td>
                        <td align="right" style="width: 20%;" class="tdbgleft">
                            联系地址：</td>
                        <td align="left" style="width: 30%;">
                            <asp:Label ID="LblAddress" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr class="tdbg">
                        <td align="right" class="tdbgleft">
                            国家/地区：</td>
                        <td align="left">
                            <asp:Label ID="LblCountry" runat="server"></asp:Label>
                        </td>
                        <td align="right" class="tdbgleft">
                            省/市：</td>
                        <td align="left">
                            <asp:Label ID="LblProvince" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr class="tdbg">
                        <td align="right" class="tdbgleft">
                            市/县/区：</td>
                        <td align="left">
                            <asp:Label ID="LblCity" runat="server"></asp:Label>
                        </td>
                        <td align="right" class="tdbgleft">
                            邮政编码：</td>
                        <td align="left">
                            <asp:Label ID="LblZipCode" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr align="center" class="tdbg">
                        <td colspan="4">
                            <asp:Button ID="BtnRegister" runat="server" Text="加入此企业" OnClick="BtnRegister_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                            <input type="button" onclick="javascript:history.go(-1)" value="返回上一步" class="inputbutton" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="PnlDifferent" Visible="false" runat="server">
                <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
                    <tr align="center" class="title">
                        <td colspan="4">
                            <asp:Label ID="Label1" runat="server" Text=" 注册我的企业（第二步） " Font-Bold="True"></asp:Label></td>
                    </tr>
                    <tr align="left" class="tdbg">
                        <td colspan="4">
                            您输入的企业名称还没有其他人注册，赶紧填写详细信息完成注册吧！注册成功后，您将成为这家企业的创建人，同时将拥有这家企业及其管理权限（如审核批准其他人的申请）。
                        </td>
                    </tr>
                    <pe:Company ID="Company1" runat="server" />
                    <tr align="center" class="tdbg">
                        <td colspan="4">
                            <asp:Button ID="BtnAppend" runat="server" Text="注册企业" OnClick="BtnAppend_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                            <input type="button" onclick="javascript:history.go(-1)" value="返回上一步" class="inputbutton" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
