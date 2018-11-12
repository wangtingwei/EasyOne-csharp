<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.AD.ADZoneExport" Title="����λ����" Codebehind="ADZoneExport.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td colspan="2" class="spacingtitle">
                <b>����λ����</b>
            </td>
        </tr>
        <tr class="tdbg">
            <td align="center">
                <table border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td>
                            <asp:ListBox ID="LstZoneID" runat="server" Width="450px" Height="300px" SelectionMode="Multiple"
                                DataTextField="ZoneName" DataValueField="zoneId"></asp:ListBox>
                        </td>
                        <td align="left">
                            &nbsp;<asp:Button ID="BtnSelectAll" runat="server" Text="ѡ������" OnClick="BtnSelectAll_Click" />
                            <br />
                            <br />
                            &nbsp;<asp:Button ID="BtnUnSelectAll" runat="server" Text="ȡ��ѡ��" OnClick="BtnunSelectAll_Click" />
                            <br />
                            <br />
                            <br />
                            <b>��ʾ����ס��Ctrl����Shift�������Զ�ѡ</b></td>
                    </tr>
                    <tr height="30">
                        <td colspan="2">
                            Ŀ�����ݿ⣺
                            <asp:TextBox ID="TxtExportMdb" runat="server">../../Temp/ADZone.mdb</asp:TextBox>
                            <asp:CheckBox ID="ChkFormatConn" runat="server" Text="�����Ŀ�����ݿ�" Checked="True" />
                        </td>
                    </tr>
                    <tr height="50">
                        <td colspan="2" align="center">
                            <asp:Button ID="EBtnSubmit" runat="server" Text="ִ�е�������" OnClick="EBtnSubmit_Click" />&nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
