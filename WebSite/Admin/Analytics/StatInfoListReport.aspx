<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Analytics.StatInfoListReport" Title="��վͳ�ƹ���" Codebehind="StatInfoListReport.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td style="width: 60%">
                <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
            </td>
            <td style="width: 40%; text-align: right">
                ��ʼͳ�����ڣ�<asp:Label ID="LblStartDate" runat="server" ForeColor="Blue"></asp:Label>&nbsp;</td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table border="0" cellpadding="2" cellspacing="1" width="100%" class="border">
        <tr class="title" align="center">
            <td align="center" style="width: 20%">
                ͳ����</td>
            <td align="center" style="width: 30%">
                ͳ������</td>
            <td align="center" style="width: 20%">
                ͳ����</td>
            <td align="center" style="width: 30%">
                ͳ������</td>
        </tr>
        <tbody>
            <tr class="tdbg">
                <td align="center">
                    ��ͳ������</td>
                <td align="center" id="TblcStatDayNum" runat="server">
                </td>
                <td align="center">
                    ����·���</td>
                <td align="center" id="TdlcMonthMaxNum" runat="server">
                </td>
            </tr>
            <tr class="tdbg">
                <td align="center">
                    �ܷ�����</td>
                <td align="center" id="TdlcTotalNum" runat="server">
                </td>
                <td align="center">
                    ����·����·�</td>
                <td align="center" id="TdlcMonthMaxDate" runat="server">
                </td>
            </tr>
            <tr class="tdbg">
                <td align="center">
                    �ܷ�������</td>
                <td align="center" id="TdlcCountNum" runat="server">
                </td>
                <td align="center">
                    ����շ���</td>
                <td align="center" id="TdlcDayMaxNum" runat="server">
                </td>
            </tr>
            <tr class="tdbg">
                <td align="center">
                    �������</td>
                <td align="center" id="TdlcTotalView" runat="server">
                </td>
                <td align="center">
                    ����շ�������</td>
                <td align="center" id="TdlcDayMaxDate" runat="server">
                </td>
            </tr>
            <tr class="tdbg">
                <td align="center">
                    ƽ���շ���</td>
                <td align="center" id="TdlcAveDayNum" runat="server">
                </td>
                <td align="center">
                    ���ʱ����</td>
                <td align="center" id="TdlcHourMaxNum" runat="server">
                </td>
            </tr>
            <tr class="tdbg">
                <td align="center">
                    ���շ�����</td>
                <td align="center" id="TdlcDayNum" runat="server">
                </td>
                <td align="center">
                    ���ʱ����ʱ��</td>
                <td align="center" id="TdlcHourMaxTime" runat="server">
                </td>
            </tr>
            <tr class="tdbg">
                <td align="center">
                    Ԥ�ƽ��շ�����</td>
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
                    ���ڷ�������</td>
                <td align="center" id="TdlcChinaNum" runat="server">
                </td>
                <td align="center">
                    �����������</td>
                <td align="center" id="TdlcOtherNum" runat="server">
                </td>
            </tr>
            <tr class="tdbg">
                <td align="center">
                    ���ò���ϵͳ</td>
                <td align="center" id="TdlcSystem" runat="server">
                </td>
                <td align="center">
                    ���������</td>
                <td align="center" id="TdlcBrowser" runat="server">
                </td>
            </tr>
            <tr class="tdbg">
                <td align="center">
                    �������ĵ�ַ</td>
                <td align="center" id="TdlcMaxAreNum" runat="server">
                </td>
                <td align="center">
                    ����������վ</td>
                <td align="center" id="TdlcMaxWebNum" runat="server">
                </td>
            </tr>
            <tr class="tdbg">
                <td align="center">
                    ������Ļ�ֱ���</td>
                <td align="center" id="TdlcMaxScrNum" runat="server">
                </td>
                <td align="center">
                    ������Ļ��ʾ��ɫ</td>
                <td align="center" id="TdlcMaxColorNum" runat="server">
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
