<%@ Page Language="C#" MasterPageFile="~/Admin/Guide.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Accessories.FilesGuide"
    Title="�ϴ��ļ�������" Codebehind="FilesGuide.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphTitle" runat="Server">
    �ϴ��ļ�Ŀ¼
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphSubMenu" runat="Server">
    <pe:FileTreeView ID="TrvUploadDir" RootNodeName="��Ŀ¼" DirectoriesXmlUrl="UploadDirectoriesXML.aspx?Dir="
        NodeNavigateUrl="FileManage.aspx?Dir=" RootAction="FileManage.aspx" runat="server">
    </pe:FileTreeView>
</asp:Content>
