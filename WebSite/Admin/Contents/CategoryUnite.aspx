<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Contents.CategoryUnite" Title="节点合并" Codebehind="CategoryUnite.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td class="spacingtitle">
                节点合并
            </td>
        </tr>
        <tr class="tdbg">
            <td>
                <br />
                &nbsp;&nbsp;将节点
                <asp:DropDownList ID="DropFromNode" DataValueField="NodeId" DataTextField="NodeName"
                    runat="server" Width="225px">
                </asp:DropDownList>
                &nbsp;&nbsp;合并到&nbsp;&nbsp;
                <asp:DropDownList ID="DropToNode" DataValueField="NodeId" DataTextField="NodeName"
                    runat="server" Width="225px">
                </asp:DropDownList>
                <br />
            </td>
        </tr>
        <tr class="tdbg">
            <td colspan="4" style="width: 786px; height: 72px;" align="center">
                <asp:Button ID="EBtnUnite" Text="合并节点" OnClick="EBtnUnite_Click" runat="server" />
                <asp:Button ID="BtnCancel" runat="server" Text="取消" OnClick="BtnCancel_Click" />
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td valign="top">
                <strong><span style="color: Blue">注意事项：</span></strong>
            </td>
            <td>
              
                1、所有操作不可逆，请慎重操作。
                <br />
                2、不能在同一个节点内进行操作，不能将一个节点合并到其下属节点中。目标节点中不能含有子节点。<br />
                3、合并后您所指定的节点（或者包括其下属节点）将被删除，所有内容信息将转移到目标节点中。
            </td>
        </tr>
    </table>
</asp:Content>
