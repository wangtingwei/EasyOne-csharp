<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.CommonModel.ModelTemplateExport"
    Title="模型模板导出" Codebehind="ModelTemplateExport.aspx.cs" %>

<asp:Content ID="Content3" ContentPlaceHolderID="CphNavigation" runat="server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CphContent" runat="Server">
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td colspan="2" class="spacingtitle">
                <b>模型模板导出</b>
            </td>
        </tr>
        <tr class="tdbg">
            <td align="center">
                <table border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td>
                            <asp:ListBox ID="LstTemplateID" runat="server" Width="450px" Height="300px" SelectionMode="Multiple"
                                DataTextField="TemplateName" DataValueField="TemplateID"></asp:ListBox>
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
                    <tr style="height: 30px">
                        <td colspan="2">
                            目标数据库：
                            <asp:TextBox ID="TxtExportMdb" runat="server">../../Temp/ModelTemplate.mdb</asp:TextBox>
                            <asp:CheckBox ID="ChkFormatConn" runat="server" Text="先清空目标数据库" Checked="True" />
                        </td>
                    </tr>
                    <tr style="height: 50px">
                        <td colspan="2" align="center">
                            <asp:Button ID="BtnSubmit" runat="server" Text="执行导出操作" OnClick="Submit_Click" />&nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
