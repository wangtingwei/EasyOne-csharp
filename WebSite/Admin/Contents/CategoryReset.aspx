<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Contents.CategoryReset" Title="复位所有节点" Codebehind="CategoryReset.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td colspan="2" class="spacingtitle">
                复位所有节点
            </td>
        </tr>
        <tr>
            <td class="tdbg" style="width: 100%">
                <span style="color: #ff0000"><strong>注意：</strong></span><br />
                如果选择复位所有节点，则所有节点都将作为一级节点，这时您需要重新对各个节点进行归属的基本设置。不要轻易使用该功能，仅在做出了错误的设置而无法复原节点之间的关系和排序的时候使用。<br />
                <br />
                如果复位时存在着同名节点，则系统会自动将目录名进行重命名。
                <br />
                <br />
                复位成功后，请记得一定要重新生成所有HTML的内容。
            </td>
        </tr>
        <tr>
            <td class="tdbg" style="width: 100%" align="center">
                <asp:Button ID="EBtnReset" Text="复位所有节点" OnClick="EBtnReset_Click" OnClientClick="BOX_show('RegUser');" runat="server" />
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
