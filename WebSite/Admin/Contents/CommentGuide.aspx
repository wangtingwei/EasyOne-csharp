<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/Guide.master"
    Inherits="EasyOne.WebSite.Admin.Contents.CommentGuide" Codebehind="CommentGuide.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphTitle" runat="Server">
    网站节点
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphSubMenu" runat="Server">
  <script language="javascript" type="text/javascript">
<!--
    function going(type,id,specialId,Iseshop)
    {
        
    }
    
     function DoContextMenu(event)
{
    event.returnValue = false;
}

    function Reflash_main_right(t)
    {
        var url = "CommentManage.aspx";
        JumpToMainRight(url);
    }
//-->
    </script>
   <table style="width:100%;height:100%;"  oncontextmenu="DoContextMenu(event)">
   <tr><td valign ="top">
    <div style="text-align: left" >
        <table >
            <tr>
                <td align="left">
                    <pe:XLoadTree ID="XLoadNodeTree" RootIcon="WebSite"  runat="server">
                    </pe:XLoadTree>
                </td>
            </tr>
        </table>
    </div>
    </td></tr>
    </table>
</asp:Content>
