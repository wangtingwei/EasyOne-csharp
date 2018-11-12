<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/Guide.master"
    Inherits="EasyOne.WebSite.Admin.Contents.SpecialCategoryGuide"
    Title="专题类别管理" Codebehind="SpecialCategoryGuide.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphTitle" runat="Server">
    专题类别管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphSubMenu" runat="Server">
    <div class="guide">
        <ul>
            <li><a href="../Contents/SpecialCategoryManage.aspx" target="main_right">专题类别管理</a></li>
            <li><a href="../Contents/SpecialCategory.aspx" target="main_right">添加专题类别</a></li>
            <li><a href="../Contents/SpecialCategoryOrder.aspx" target="main_right">专题类别排序</a></li>
        </ul>
    </div>
</asp:Content>
