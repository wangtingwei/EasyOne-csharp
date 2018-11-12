<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Contents.CreateHtmlSingle"
    Title="无标题页" Codebehind="CreateHtmlSingle.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="server">
    <table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td class="spacingtitle">
                生成单页节点
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbg" align="center">
                <asp:ListBox ID="LstSinglePage" runat="server" Width="500px" Height="400px" SelectionMode="Multiple"
                    ToolTip="按住“Ctrl”或“Shift”键可以多选，按住“Ctrl”可取消选择"></asp:ListBox>
                <br />
                <asp:Button ID="CreateSinglePage" runat="server" Text="生成选定单页节点" OnClick="CreateSinglePage_Click" />
                &nbsp;&nbsp;
                <asp:Button ID="CreateAllSinglePage" runat="server" Text="生成所有单页节点" OnClick="CreateAllSinglePage_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
