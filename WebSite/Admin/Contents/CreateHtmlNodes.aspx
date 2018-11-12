<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    EnableEventValidation="false" Inherits="EasyOne.WebSite.Admin.Contents.CreateHtmlNodes"
    ValidateRequest="false" Title="生成栏目" Codebehind="CreateHtmlNodes.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td class="spacingtitle" colspan="2">
                生成栏目列表页
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>选择栏目：</strong>
            </td>
            <td>
                <asp:ListBox ID="LstNodes" runat="server" Width="500px" Height="400px" SelectionMode="Multiple"
                    DataTextField="NodeName" DataValueField="NodeId" ToolTip="按住“Ctrl”或“Shift”键可以多选，按住“Ctrl”可取消选择">
                </asp:ListBox>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>是否同时生成子栏目页：</strong>
            </td>
            <td>
                <asp:RadioButtonList ID="RdlIsCreateChild" RepeatDirection="horizontal" RepeatLayout="flow"
                    runat="server">
                    <asp:ListItem Selected="true" Text="是" Value="true"></asp:ListItem>
                    <asp:ListItem Text="否" Value="false"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr class="tdbg">
            <td colspan="2" align="center">
                <asp:Button ID="CreateCategoryListById" runat="server" Text="生成选定栏目的列表页" OnClick="CreateCategoryListById_Click" />
                &nbsp;&nbsp;
                <asp:Button ID="CreateNeedCreateHtmlList" runat="server" Text="生成所有待发布栏目的列表页" OnClick="CreateNeedCreateHtmlList_Click" />
                &nbsp;&nbsp;
                <asp:Button ID="CreateAllCategoryList" runat="server" Text="生成所有栏目的列表页" OnClick="CreateAllCategoryList_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
