<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Contents.SpecialGuide"
    MasterPageFile="~/Admin/Guide.master" Title="专题管理" Codebehind="SpecialGuide.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphTitle" runat="Server">
    专题管理<span class="refresh" onclick="location.reload();"></span>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphSubMenu" runat="Server">
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
    <script language="javascript" type="text/javascript">
//<!--
    function going(type,id,specialId,Iseshop)
    {
        if(type=="addSpecialCategory")
        {
            var url = "SpecialCategory.aspx";
              JumpToMainRight(url);
        }
        if(type=="sortSpecialCategory")
        {
            var url = "SpecialCategoryOrder.aspx";
              JumpToMainRight(url);
        }
        if(type=="modifySpecialCategory")
        {
            var url = "SpecialCategory.aspx?Action=Modify&SpecialCategoryID=" + specialId;
              JumpToMainRight(url);
        }
        if(type=="deleteSpecialCategory")
        {
            var isConfirm = confirm('删除专题类别将删除该类别所有相关数据，确定要删除此专题类别吗？');
            if(isConfirm){
                var url ='<%= AppendSecurityCode("SpecialCategoryManage.aspx?Action=Delete")%>' + "&SpecialCategoryID=" + specialId;
                  JumpToMainRight(url);
            }
        }
        if(type=="setNode")
        {
            var url = "Special.aspx?Action=Modify&SpecialID=" + specialId;
           
              JumpToMainRight(url);
        }
        if(type=="copyNode")
        {
            var url = "Special.aspx?Action=Copy&SpecialID=" + specialId;
           
              JumpToMainRight(url);
        }
        if(type=="clear")
        {
            var isConfirm = confirm('确定要清空此专题下的所有信息吗？');
            if(isConfirm){
                var url ='<%= AppendSecurityCode("SpecialManage.aspx?Action=Clear")%>' + "&SpecialID=" + specialId;
              
                  JumpToMainRight(url);
            }
        }
        if(type=="delete")
        {
            var isConfirm = confirm('删除专题将删除该专题所有相关数据，确定要删除此专题吗？');
            if(isConfirm){
                var url ='<%= AppendSecurityCode("SpecialManage.aspx?Action=Delete")%>' + "&SpecialID=" + specialId;
                 JumpToMainRight(url);
            }
        }
        if(type=="addSpecial")
        {
            var url = "Special.aspx?Action=Add&SpecialCategoryID=" + specialId;
             JumpToMainRight(url);
        }
        if(type=="sortSpecial")
        {
            var url = "SpecialOrder.aspx?SpecialCategoryID=" + specialId;
             JumpToMainRight(url);
        }
    }
    
    function Reflash_main_right(t)
    {
        ReloadMainRight();
    }
//-->
    </script>
</asp:Content>
