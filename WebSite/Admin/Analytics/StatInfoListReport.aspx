<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Analytics.StatInfoListReport" Title="网站统计管理" Codebehind="StatInfoListReport.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td style="width: 60%">
                <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
            </td>
            <td style="width: 40%; text-align: right">
                开始统计日期：<asp:Label ID="LblStartDate" runat="server" ForeColor="Blue"></asp:Label>&nbsp;</td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table border="0" cellpadding="2" cellspacing="1" width="100%" class="border">
        <tr class="title" align="center">
            <td align="center" style="width: 20%">
                统计项</td>
            <td align="center" style="width: 30%">
                统计数据</td>
            <td align="center" style="width: 20%">
                统计项</td>
            <td align="center" style="width: 30%">
                统计数据</td>
        </tr>
        <tbody>
            <tr class="tdbg">
                <td align="center">
                    总统计天数</td>
                <td align="center" id="TblcStatDayNum" runat="server">
                </td>
                <td align="center">
                    最高月访量</td>
                <td align="center" id="TdlcMonthMaxNum" runat="server">
                </td>
            </tr>
            <tr class="tdbg">
                <td align="center">
                    总访问量</td>
                <td align="center" id="TdlcTotalNum" runat="server">
                </td>
                <td align="center">
                    最高月访量月份</td>
                <td align="center" id="TdlcMonthMaxDate" runat="server">
                </td>
            </tr>
            <tr class="tdbg">
                <td align="center">
                    总访问人数</td>
                <td align="center" id="TdlcCountNum" runat="server">
                </td>
                <td align="center">
                    最高日访量</td>
                <td align="center" id="TdlcDayMaxNum" runat="server">
                </td>
            </tr>
            <tr class="tdbg">
                <td align="center">
                    总浏览量</td>
                <td align="center" id="TdlcTotalView" runat="server">
                </td>
                <td align="center">
                    最高日访量日期</td>
                <td align="center" id="TdlcDayMaxDate" runat="server">
                </td>
            </tr>
            <tr class="tdbg">
                <td align="center">
                    平均日访量</td>
                <td align="center" id="TdlcAveDayNum" runat="server">
                </td>
                <td align="center">
                    最高时访量</td>
                <td align="center" id="TdlcHourMaxNum" runat="server">
                </td>
            </tr>
            <tr class="tdbg">
                <td align="center">
                    今日访问量</td>
                <td align="center" id="TdlcDayNum" runat="server">
                </td>
                <td align="center">
                    最高时访量时间</td>
                <td align="center" id="TdlcHourMaxTime" runat="server">
                </td>
            </tr>
            <tr class="tdbg">
                <td align="center">
                    预计今日访问量</td>
                <td align="center" id="TdlcPreDayNum" runat="server">
                </td>
                <td align="center">
                </td>
                <td align="center">
                </td>
            </tr>
            <tr class="tdbgleft">
                <td colspan="4">
                </td>
            </tr>
            <tr class="tdbg">
                <td align="center">
                    国内访问人数</td>
                <td align="center" id="TdlcChinaNum" runat="server">
                </td>
                <td align="center">
                    国外访问人数</td>
                <td align="center" id="TdlcOtherNum" runat="server">
                </td>
            </tr>
            <tr class="tdbg">
                <td align="center">
                    常用操作系统</td>
                <td align="center" id="TdlcSystem" runat="server">
                </td>
                <td align="center">
                    常用浏览器</td>
                <td align="center" id="TdlcBrowser" runat="server">
                </td>
            </tr>
            <tr class="tdbg">
                <td align="center">
                    访问最多的地址</td>
                <td align="center" id="TdlcMaxAreNum" runat="server">
                </td>
                <td align="center">
                    访问最多的网站</td>
                <td align="center" id="TdlcMaxWebNum" runat="server">
                </td>
            </tr>
            <tr class="tdbg">
                <td align="center">
                    常用屏幕分辨率</td>
                <td align="center" id="TdlcMaxScrNum" runat="server">
                </td>
                <td align="center">
                    常用屏幕显示颜色</td>
                <td align="center" id="TdlcMaxColorNum" runat="server">
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
