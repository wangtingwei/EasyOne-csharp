<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.AD.ShowJSCode" Title="����λ����" Codebehind="ShowJSCode.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr class="title">
            <td colspan="2" align="center">
                <strong>��λJS���ô���</strong></td>
        </tr>
        <tr class="tdbgleft">
            <td align="center">
            <span style="color:Green">���÷�����������Ĵ�����뵽��ҳ��Ԥ���Ĺ��λ��</span></td>
        </tr>
        <tr class="tdbg">
            <td align="center" style="height: 27px">
                <asp:TextBox ID="TxtZoneCode" runat="server" Height="102px" TextMode="MultiLine"
                    Width="578px"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg">
            <td align="center">
                <a href="ADZoneManage.aspx">����</a></td>
        </tr>
    </table>
</asp:Content>
