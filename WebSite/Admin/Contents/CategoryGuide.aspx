<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/Guide.master"
    Inherits="EasyOne.WebSite.Admin.Contents.CategoryGuide"
    Title="网站节点管理向导" Codebehind="CategoryGuide.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphTitle" runat="Server">
    网站节点管理<span class="refresh" onclick="location.reload();"></span>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphSubMenu" runat="Server">
    <table style="width: 100%; height: 100%;" oncontextmenu="DoContextMenu(event)">
        <tr>
            <td valign="top">
                <div style="text-align: left">
                    <table>
                        <tr>
                            <td align="left">
                                <pe:XLoadTree ID="XLoadNodeTree" RootIcon="WebSite" runat="server">
                                </pe:XLoadTree>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <script type="text/javascript">
    function DoContextMenu(event)
{
    event.returnValue = false;
}
    </script>
</asp:Content>
