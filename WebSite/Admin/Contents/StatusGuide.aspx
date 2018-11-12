<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/Guide.master"
    Inherits="EasyOne.WebSite.Admin.Contents.StatusGuide" Title="流程操作码管理" Codebehind="StatusGuide.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphTitle" runat="Server">
    稿件状态码管理
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphSubMenu" runat="Server">
<div class="guideexpand">
        稿件状态码管理</div>
    <div class="guide">
        <ul>
            <li><a ID="EahStatusManage"  href="../Contents/StatusManage.aspx" runat="server" target="main_right">稿件状态码管理</a></li>
            <li><a ID="EahStatusAdd"  href="../Contents/Status.aspx" runat="server" target="main_right">添加稿件状态码</a></li>
        </ul>
    </div>
</asp:Content>
