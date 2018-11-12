<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Contents.CommentReplyModify"
    MasterPageFile="~/Admin/MasterPage.master" Title="管理员评论回复" ValidateRequest="false" Codebehind="CommentReplyModify.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <!-- 显示评论树开始 -->
    <table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td colspan="2" class="spacingtitle">
                <asp:Label ID="LblTitle" runat="server" Text="管理员评论回复" />
            </td>
        </tr>
        <tr class="tdbg" runat="server" id="trUser">
            <td class="tdbgleft" align="right" style="width: 150px;">
                <strong>用户名：&nbsp;</strong></td>
            <td class="tdbg" align="left">
                <asp:Label ID="LblUserName" runat="server" Text="匿名发表" />
            </td>
        </tr>
        <tr class="tdbg" runat="server" id="trEmail">
            <td class="tdbgleft" align="right" style="width: 150px;">
                <strong>邮件：&nbsp;</strong></td>
            <td class="tdbg" align="left">
                <asp:Label ID="LblEmail" runat="server" Text="" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right" style="width: 150px">
                <strong>评分：&nbsp;</strong></td>
            <td class="tdbg" align="left">
                <asp:Label ID="LblScore" runat="server" Text="" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right" style="width: 150px">
                <strong>评论回复：&nbsp;</strong></td>
            <td class="tdbg" align="left">
                <asp:TextBox ID="TxtCommentReply" runat="server" Height="118px" TextMode="MultiLine"
                    Width="355px"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg">
            <td colspan="2" align="center">
                <asp:Button ID="BtnSubmit" runat="server" Text="保存" OnClick="BtnSubmit_Click" />
                &nbsp;&nbsp;
                <asp:Button ID="BtnReset" runat="server" Text="取消" OnClick="BtnReset_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
