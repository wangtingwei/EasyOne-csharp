<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.User.Info.CardList" Codebehind="CardList.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>充值卡管理</title>
</head>
<body>
    <pe:UserNavigation ID="UserCenterNavigation" Tab="charge" runat="server" />
    <pe:ExtendedSiteMapPath ID="YourPosition" SiteMapProvider="UserMapProvider" runat="server" />
    <form id="form1" runat="server">
        <div>
            <table border="0" cellpadding="0" cellspacing="1" class="border" style="width: 100%">
                <tr>
                    <td align="center" class="spacingtitle" valign="middle">
                        获取虚拟充值卡</td>
                </tr>
                <tr>
                    <td align="center" class="tdbg">
                        <asp:Table ID="TbCardList" runat="server" Width="100%" BorderWidth="0" HorizontalAlign="Center"
                            CellPadding="2" CellSpacing="1" CssClass="border" GridLines="Both">
                        </asp:Table>
                    </td>
                </tr>
                <tr>
                    <td class="tdbg" colspan="">
                        <span style="color: #ff0000">
                            <br />
                            注意：</span><br />
                        这里只显示了还未使用的充值卡的卡号及密码。为了安全起见，请您尽快使用！<br />
                        <br />
                        如果您购买的是本站的充值卡，可以直接点击“充值卡充值”链接进行充值。<br />
                        如果您购买的是其他公司的卡，请尽快去相关公司或网站的充值入口进行充值。<br />
                        <br />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
