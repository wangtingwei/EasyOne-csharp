<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPage.master" Inherits="EasyOne.WebSite.Admin.Contents.ContentBatchMove" Codebehind="ContentBatchMove.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="border">
        <tr class="tdbg" align="center">
            <td colspan="2" class="spacingtitle">
                移动内容
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>选定的内容ID：</strong>
            </td>
            <td>
                <asp:TextBox ID="TxtGeneralId" runat="server" Width="230px"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>移动到目标节点：</strong></td>
            <td>
                <asp:DropDownList ID="DropNode" DataValueField="NodeId" DataTextField="NodeName"
                    runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="tdbgleft" id="commonfooter" colspan="2">
                <asp:Button ID="EBtnBacthMove" Text="执行批处理" OnClick="EBtnBacthMove_Click" runat="server" />&nbsp;&nbsp;
                <asp:Button ID="BtnCancel" runat="server" Text="取消" OnClick="BtnCancel_Click" /></td>
        </tr>
    </table>
</asp:Content>
