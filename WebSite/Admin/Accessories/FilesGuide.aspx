<%@ Page Language="C#" MasterPageFile="~/Admin/Guide.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Accessories.FilesGuide"
    Title="上传文件管理向导" Codebehind="FilesGuide.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphTitle" runat="Server">
    上传文件目录
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphSubMenu" runat="Server">
    <pe:FileTreeView ID="TrvUploadDir" RootNodeName="根目录" DirectoriesXmlUrl="UploadDirectoriesXML.aspx?Dir="
        NodeNavigateUrl="FileManage.aspx?Dir=" RootAction="FileManage.aspx" runat="server">
    </pe:FileTreeView>
</asp:Content>
