<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.User.Info.PointLogDetail" Codebehind="PointLogDetail.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>点券明细记录详情</title>
</head>
<body>
    <pe:UserNavigation Tab="user" ID="UserCenterNavigation" runat="server" />
    <pe:ExtendedSiteMapPath ID="YourPosition" SiteMapProvider="UserMapProvider" runat="server" />
    <form id="form1" runat="server">
        <div>
        <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
            <tr align='center'>
                <td colspan='2' class='spacingtitle'>
                    <b>查看<pe:ShowPointName ID="ShowPointName1" runat="server"></pe:ShowPointName>明细记录详情</b>
                </td>
            </tr>
            <tr class="tdbg">
                <td style="width: 15%" class="tdbgleft" align="right">
                    消费时间：</td>
                <td>
                    <asp:Label ID="LblLogTime" runat="server"></asp:Label></td>
            </tr>
            <tr class="tdbg">
                <td style="width: 15%" class="tdbgleft" align="right">
                    IP地址：</td>
                <td>
                    <asp:Label ID="LblIP" runat="server"></asp:Label></td>
            </tr>
            <tr class="tdbg">
                <td style="width: 15%" class="tdbgleft" align="right">
                    <pe:ShowPointName ID="ShowPointName2" runat="server"></pe:ShowPointName>数：</td>
                <td>
                    <asp:Label ID="LblIncomePayOut" runat="server"></asp:Label></td>
            </tr>
            <tr class="tdbg">
                <td style="width: 15%" class="tdbgleft" align="right">
                    重复次数：</td>
                <td>
                    <asp:Label ID="LblTimes" runat="server"></asp:Label></td>
            </tr>
            <tr class="tdbg">
                <td style="width: 15%" class="tdbgleft" align="right">
                    备注/说明：</td>
                <td>
                    <asp:Label ID="LblRemark" runat="server"></asp:Label></td>
            </tr>
            <tr class="tdbg">
                <td colspan="2" align="center">
                    <input type="button" value="返回" class="inputbutton" onclick="javascript:window.location.href='PointLog.aspx'" />
                </td>
            </tr>
        </table>
        </div>
    </form>
</body>
</html>
