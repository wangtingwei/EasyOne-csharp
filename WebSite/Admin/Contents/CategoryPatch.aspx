<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Contents.CategoryPatch" Title="修复节点" Codebehind="CategoryPatch.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td colspan="2" class="spacingtitle">
                修复节点
            </td>
        </tr>
        <tr>
            <td class="tdbg" style="width: 100%;">
                <br />
                当节点出现排序错误或串位的情况时，使用此功能可以修复。本操作相当安全，不会给系统带来任何负面影响。<br />
                <br />
                修复过程中请勿刷新页面！<br />
                <br />
                <br />
            </td>
        </tr>
        <tr>
            <td class="tdbg" style="width: 100%;" align="center">
                <asp:Button ID="EBtnPatch" Text="开始修复" OnClick="EBtnPatch_Click" OnClientClick="BOX_show('RegUser');" runat="server" />
                <asp:Button ID="BtnCancel" runat="server" Text="取消" OnClick="BtnCancel_Click" />
            </td>
        </tr>
    </table>
    <div id="BOX_overlay" style="display:none;">
        <div id="RegUser">
            <div>
                <label><font color="#FF0000">数据正在更新中……</font></label><br />
                <img alt="" src="<%=BasePath %>admin/Images/progressbar.gif" />
            </div>
        </div>
    </div>
<script src="<%=BasePath %>admin/JS/ModalPopup.js" type="text/javascript"></script>
</asp:Content>
