<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Contents.CategoryManage" Title="节点管理" Codebehind="CategoryManage.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="border">
        <tr class="tdbg" align="center">
            <td colspan="2" class="spacingtitle">
                节点通用操作
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" style="width:15%">
                <asp:HyperLink ID="HypCategory" NavigateUrl="~/Admin/Contents/Category.aspx" runat="server">添加栏目节点</asp:HyperLink>
            </td>
            <td>
                为方便归类管理，界面中将信息归类成“基本信息”、“栏目选项”、“模板选项”、“收费设置”、“前台样式”、“上传选项”、“生成选项”、“权限设置”和“自设内容”等书签式管理选项，以方便按快捷分类设置信息选项。在填写好相关信息后，单击页面底部“添加”按钮保存所添加的栏目节点。
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <asp:HyperLink ID="HypSingle" NavigateUrl="~/Admin/Contents/Single.aspx" runat="server">添加单页节点</asp:HyperLink>
            </td>
            <td>
                当网站中需要显示“联系方式”、“公司简介”、“版权声明”等无分类需求的单个信息页面时，可以添加单页节点的方式来实现。
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <asp:HyperLink ID="HypOutLink" NavigateUrl="~/Admin/Contents/OutLink.aspx" runat="server">添加外部连接</asp:HyperLink>
            </td>
            <td>
                外部链接是指在网站中添加节点的链接地址为网站外部的地址。如在网站顶部导航中，显示“动易论坛”文字，链接地址为http://bbs.EasyOne.net/，以新窗口打开方式打开网址，则可添加外部链接节点的方式来实现。
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <asp:HyperLink ID="HypUnitCategory" NavigateUrl="~/Admin/Contents/CategoryUnite.aspx" runat="server">合并栏目</asp:HyperLink>
            </td>
            <td>
                本操作是将一个节点内所有信息转移合并到目标节点中去，原节点名及其下属节点将被删除。
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <asp:HyperLink ID="HypMoveCategory" NavigateUrl="~/Admin/Contents/CategoryMove.aspx" runat="server">移动栏目</asp:HyperLink> 
            </td>
            <td>
                本操作是将一个节点中的所有信息转移到目标节点中去，原指定节点包括其下属节点不会被删除。
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <asp:HyperLink ID="HypCategoryBatchSet" NavigateUrl="~/Admin/Contents/CategoryBatchSet.aspx" runat="server">批量设置</asp:HyperLink>  
            </td>
            <td>
                本操作是批量设置节点属性，包括对节点选项、前台样式、权限设置的批量设置。利用本功能可以对节点所需的相同属性进行快捷批量设置。
            </td>
        </tr>
         <tr class="tdbg">
            <td class="tdbgleft">
                <asp:HyperLink ID="HypResetCategory" NavigateUrl="~/Admin/Contents/CategoryReset.aspx" runat="server">复位所有栏目</asp:HyperLink>
            </td>
            <td>
                本操作是将所有栏目节点及其子栏目节点都复位为一级节点。
            </td>
        </tr>
         <tr class="tdbg">
           <td class="tdbgleft">
                <asp:HyperLink ID="HypRepairCategory" NavigateUrl ="~/Admin/Contents/CategoryPatch.aspx" runat="server">修复栏目结构</asp:HyperLink>
            </td>
            <td>
               本操作是修复节点出现排序错误或串位的情况。
            </td>
        </tr>
    </table>
</asp:Content>
