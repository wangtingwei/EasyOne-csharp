<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Contents.CommentRestore"
    MasterPageFile="~/Admin/MasterPage.master" Title="评论回复管理" ValidateRequest="false" Codebehind="CommentRestore.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <!-- 显示评论树开始 -->
    <div id="bbs_title">
        <span id="bbs_title_right"><a style="cursor: hand" onclick="javascript:window.print()"
            onmousemove="status='打印本页';" onmouseout="status='';">打印</a> <a style="cursor: hand"
                onclick="window.external.AddFavorite(document.location.href,document.title)"
                onmousemove="status='收藏本页';" onmouseout="status='';">收藏</a> </span>标题：<asp:Label
                    ID="LblTitle" runat="server" Text="Label"></asp:Label>
    </div>
    <!--中部详细内容-->
    <div id="bbs_center">
        <div id="bbs_left">
            <div style="margin-top: 15px;">
                <asp:Image ID="ImgUserFace" runat="server" />
            </div>
            <div>
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td colspan="2">
                            <strong>【<asp:Label ID="LblUserName" runat="server" Text="Label"></asp:Label>】</strong></td>
                    </tr>
                    <tr>
                        <td align="right">
                            <strong>发表信息数：&nbsp;</strong></td>
                        <td>
                            <asp:Label ID="LblPassedItems" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr runat="server" id="UserExp">
                        <td align="right">
                            <strong>用户积分：&nbsp;</strong></td>
                        <td>
                            <asp:Label ID="LblUserExp" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <strong>注册时间：&nbsp;</strong></td>
                        <td>
                            <asp:Label ID="LblUserRegTime" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div id="bbs_right">
            <div id="bbs_text">
                <table border="0" cellpadding="0" cellspacing="1" width="100%" style="border-bottom: #6595D6 1px solid;">
                    <tr>
                        <td>
                            <pe:ExtendedLabel HtmlEncode="false" ID="LblMessage" runat="server" Text="LblMessage"></pe:ExtendedLabel>
                            <pe:ExtendedLabel HtmlEncode="false" ID="LblUserShow" runat="server" Text="LblUserShow"></pe:ExtendedLabel>
                            <pe:ExtendedLabel ID="LblEmail" runat="server" Text="LblEmail"></pe:ExtendedLabel>
                        </td>
                        <td align="right">
                        </td>
                    </tr>
                </table>
                <asp:Label ID="LblContent" runat="server" Text="Label"></asp:Label>
            </div>
        </div>
    </div>
    <!--中部详细内容end-->
    <div id="bbs_bot">
        <div id="bbs_botleft">
            <img src="<%=Path%>Images/Comment/ip.gif" alt="评论发布时间" />
            <asp:Label ID="LblUpdateDateTime" runat="server" Text="Label"></asp:Label>
        </div>
        <div id="bbs_botright">
            <div style="padding: 4px; float: right;">
            </div>
            <div style="padding: 4px; float: right;">
            </div>
        </div>
    </div>
    <!-- 显示评论树结束 -->
    <br />
    <table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td colspan="2" class="spacingtitle">
                <asp:Label ID="LblCommentTitle" runat="server" Text="评论回复"></asp:Label>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right" style="width: 150px">
                <strong>评论回复：&nbsp;</strong></td>
            <td class="tdbg" align="left">
                <asp:TextBox ID="TxtCommentRestore" runat="server" Height="118px" TextMode="MultiLine"
                    Width="355px"></asp:TextBox>
                <pe:RequiredFieldValidator ID="ValrCommentRestore" ControlToValidate="TxtCommentRestore"
                    runat="server" ErrorMessage="评论回复不能为空！" Display="Dynamic"></pe:RequiredFieldValidator>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right" style="width: 150px"> </td>
            <td class="tdbg" align="left">
                <asp:CheckBox ID="ChkReplyIsPrivate" runat="server" />回复是否隐藏<span style="color: #336600">(隐藏只有评论人才能看见)</span>
            </td>
        </tr>
        <tr class="tdbg">
            <td colspan="2" align="center">
                <asp:Button ID="BtnSubmit" runat="server" Text="保存" OnClick="BtnSubmit_Click" />
                &nbsp;&nbsp;
                <asp:Button ID="BtnReset" runat="server" Text="取消" OnClick="BtnReset_Click" ValidationGroup="BtnReset" />
            </td>
        </tr>
    </table>
</asp:Content>
