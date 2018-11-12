<%@ Page Language="C#" MasterPageFile="~/Admin/Guide.master" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Contents.SpecialTree"
    Title="专题树" Codebehind="SpecialTree.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphTitle" runat="Server">
    <pe:ExtendedLabel HtmlEncode="false" ID="LblNavigationLink" runat="server" Text=""></pe:ExtendedLabel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphSubMenu" runat="Server">
    <div style="text-align: left">
        <table>
            <tr>
                <td align="left">
                    <pe:XLoadTree ID="XLoadNodeTree" runat="server">
                    </pe:XLoadTree>
                </td>
            </tr>
        </table>
    </div>

    <script language="javascript" type="text/javascript">
    <!--
    function going(type,id,specialId,Iseshop)
    {
        if(type=="modifySpecialCategory")
        {
            var url = "SpecialCategory.aspx?Action=Modify&SpecialCategoryID=" + specialId;
            JumpToMainRight(url);
        }
        if(type=="setNode")
        {
            var url = "Special.aspx?Action=Modify&SpecialID=" + specialId;
           JumpToMainRight(url);
        }
        
    }
    
    function Reflash_main_right(t)
    {
        var url = "ContentManage.aspx";
        JumpToMainRight(url);
    }
    //-->
    </script>

</asp:Content>
