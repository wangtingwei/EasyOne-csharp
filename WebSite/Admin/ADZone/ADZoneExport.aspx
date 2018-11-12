<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.AD.ADZoneExport" Title="广告版位导出" Codebehind="ADZoneExport.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td colspan="2" class="spacingtitle">
                <b>广告版位导出</b>
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
                            &nbsp;<asp:Button ID="BtnSelectAll" runat="server" Text="选定所有" OnClick="BtnSelectAll_Click" />
                            <br />
                            <br />
                            &nbsp;<asp:Button ID="BtnUnSelectAll" runat="server" Text="取消选定" OnClick="BtnunSelectAll_Click" />
                            <br />
                            <br />
                            <br />
                            <b>提示：按住“Ctrl”或“Shift”键可以多选</b></td>
                    </tr>
                    <tr height="30">
                        <td colspan="2">
                            目标数据库：
                            <asp:TextBox ID="TxtExportMdb" runat="server">../../Temp/ADZone.mdb</asp:TextBox>
                            <asp:CheckBox ID="ChkFormatConn" runat="server" Text="先清空目标数据库" Checked="True" />
                        </td>
                    </tr>
                    <tr height="50">
                        <td colspan="2" align="center">
                            <asp:Button ID="EBtnSubmit" runat="server" Text="执行导出操作" OnClick="EBtnSubmit_Click" />&nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
