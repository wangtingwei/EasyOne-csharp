<%@ Page Language="C#" MasterPageFile="~/Admin/Guide.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Contents.NodeTree" Title="½ÚµãÊ÷" Codebehind="NodeTree.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphTitle" runat="Server">
    <pe:ExtendedLabel ID="LblNavigationLink" HtmlEncode="false" runat="server" Text=""></pe:ExtendedLabel>
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
    
    <script language="javascript" type="text/javascript">
<!--
function DoContextMenu(event)
{
    event.returnValue = false;
}
    function going(type,id,nodeId,Iseshop,filePath)
    {
        var contentFilePath = "Content.aspx";
        
        if (filePath != "")
        {
            contentFilePath = filePath;
        }
        if(Iseshop=="True")
        {
            contentFilePath="../Shop/"+filePath;
        }
        switch (type)
        {   
            case "manageinfo":            
                var url = contentFilePath + "?NodeId=" + nodeId + "&ModelID=" + id;
                JumpToMainRight(url);
                break;
            case "addcontent":            
                var url = contentFilePath + "?Action=add&NodeId=" + nodeId + "&ModelID=" + id;
                JumpToMainRight(url);
                break;
            case "setNode":
                var url = "Category.aspx?Action=Modify&NodeID=" + nodeId;
                JumpToMainRight(url);
                break;
            case "htmlManage":
                var url = "ContentHtml.aspx?NodeID=" + nodeId;
                JumpToMainRight(url);
                break;
            case "signin":
                 var url = "ContentSignin.aspx?NodeID=" + nodeId;
                JumpToMainRight(url);
                break;
            case "recycle":
                var url = "ContentRecycle.aspx?NodeID=" + nodeId;
                JumpToMainRight(url);
                break;
        }
    }

    function Reflash_main_right(t)
    {
        var url = "SpecialInfosManage.aspx";
        JumpToMainRight(url);
    }
//-->
    </script>

</asp:Content>
