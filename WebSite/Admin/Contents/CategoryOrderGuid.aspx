<%@ Page Language="C#" MasterPageFile="~/Admin/Guide.master" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Contents.CategoryOrderGuid"
    Title="无标题页" Codebehind="CategoryOrderGuid.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphTitle" runat="server">
    网站节点排序<span class="refresh" onclick="location.reload();"></span>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphSubMenu" runat="server">
    <table style="width: 100%; height: 100%;" oncontextmenu="DoContextMenu(event)">
        <tr>
            <td valign="top">
                <div style="text-align: left">
                    <table>
                        <tr>
                            <td align="left">
                                <pe:XLoadTree ID="XLoadNodeTree" RootIcon="WebSite"  runat="server">
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
