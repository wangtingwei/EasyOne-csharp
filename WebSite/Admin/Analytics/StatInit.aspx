<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Analytics.StatInit" Title="���ݳ�ʼ��" Codebehind="StatInit.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table width="100%" border="0" cellspacing="1" cellpadding="2" class="border">
        <tr align="center">
            <td class="spacingtitle">
                <strong>���ݳ�ʼ��</strong>
            </td>
        </tr>
        <tr class="tdbg">
            <td style="height: 150px">
                <p style="text-align: center">
                    <strong><span style="color: #ff0000">�����ô˹��ܣ���Ϊһ��������޷��ָ���</span></strong>
                    <br />
                    �˲�����������ݿ��е�����ͳ�����ݣ�����ϵͳ��ʼ��ʱ����Ҫ����վ�ķ���ͳ�����ݽ�������ͳ��ʱʹ�á�
                </p>
                <p style="text-align: center">
                    <asp:Button ID="BtnInit" runat="server" Text="ͳ�����ݳ�ʼ��" OnClick="BtnInit_Click" OnClientClick="return confirm('ȷʵҪ��ʼ����һ��������޷��ָ���')" />
                    &nbsp;</p>
            </td>
        </tr>
    </table>
</asp:Content>
