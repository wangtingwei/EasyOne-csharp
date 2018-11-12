<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    EnableEventValidation="false" Inherits="EasyOne.WebSite.Admin.Contents.CreateHtmlSpecial"
    ValidateRequest="false" Title="生成专题" Codebehind="CreateHtmlSpecial.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td class="spacingtitle">
                生成专题列表页
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbg" align="center">
                <asp:ListBox ID="LstSpecials" runat="server" Width="500px" Height="400px" SelectionMode="Multiple"
                    DataTextField="SpecialName" DataValueField="SpecialId" ToolTip="按住“Ctrl”或“Shift”键可以多选，按住“Ctrl”可取消选择">
                </asp:ListBox>
                <br />
                <asp:Button ID="CreateSpecialListById" runat="server" Text="生成选定专题的列表页" OnClick="CreateSpecialListById_Click" />
                &nbsp;&nbsp;
                <asp:Button ID="CreateNeedCreateHtmlList" runat="server" Text="生成所有待发布专题列表页" OnClick="CreateNeedCreateHtmlList_Click" />
                &nbsp;&nbsp;
                <asp:Button ID="CreateAllSpecialList" runat="server" Text="生成所有专题的列表页" OnClick="CreateAllSpecialList_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
