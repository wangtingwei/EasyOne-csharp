<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.User.CardShow" Title="�鿴��ֵ����Ϣ" Codebehind="CardShow.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table border="0" cellpadding="2" cellspacing="1" class="border" width="100%">
        <tr align="center">
            <td class="spacingtitle" colspan="2">
                <strong>�鿴��ֵ����Ϣ</strong></td>
        </tr>
        <tr class="tdbg">
            <td style="text-align: right;">
                <b>��ֵ�����ͣ�</b></td>
            <td style="width: 50%" id="TdCardType" runat="server">
            </td>
        </tr>
        <tr class="tdbg">
            <td style="text-align: right">
                <b>������Ʒ��</b></td>
            <td id="TdProductName" runat="server">
            </td>
        </tr>
        <tr class="tdbg">
            <td style="text-align: right">
                <b>��ֵ�����ţ�</b></td>
            <td id="TdCardNum" runat="server">
            </td>
        </tr>
        <tr class="tdbg">
            <td style="text-align: right">
                <b>��ֵ�����룺</b></td>
            <td id="TdPassword" runat="server">
            </td>
        </tr>
        <tr class="tdbg">
            <td style="text-align: right">
                <b>��ֵ����ֵ��</b></td>
            <td id="TdMoney" runat="server">
            </td>
        </tr>
        <tr class="tdbg">
            <td style="text-align: right">
                <b>��ֵ��������</b></td>
            <td id="TdValidNum" runat="server">
            </td>
        </tr>
        <tr class="tdbg">
            <td style="text-align: right">
                <b>��ֵ��ֹ���ڣ�</b></td>
            <td id="TdEndDate" runat="server">
            </td>
        </tr>
        <tr class="tdbg">
            <td style="text-align: right">
                <b>��ֵ������ʱ�䣺</b></td>
            <td id="TdCreateTime" runat="server">
            </td>
        </tr>
        <tr class="tdbg">
            <td style="text-align: right">
                <b>��ֵ��״̬��</b></td>
            <td id="TdStatus" runat="server">
            </td>
        </tr>
        <tr class="tdbg">
            <td style="text-align: right">
                <b>ʹ���ߣ�</b></td>
            <td id="TdUserName" runat="server">
            </td>
        </tr>
        <tr class="tdbg">
            <td style="text-align: right">
                <b>��ֵʱ�䣺</b></td>
            <td id="TdUseTime" runat="server">
            </td>
        </tr>
        <tr class="tdbg">
            <td style="text-align: right">
                <strong>�����̣�</strong></td>
            <td id="TdAgentName" runat="server">
            </td>
        </tr>
    </table>
    <div style="width: 100%; text-align: center">
        <br />
        <input id="Button2" type="button" class="inputbutton" value="����" onclick="javascript:history.go(-1)" /></div>
</asp:Content>
