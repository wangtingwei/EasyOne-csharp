<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Contents.CommentExcerpt"
    MasterPageFile="~/Admin/MasterPage.master" Title="评论引用管理" ValidateRequest="false" Codebehind="CommentExcerpt.aspx.cs" %>

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
                <pe:ExtendedLabel  HtmlEncode="false" ID="LblUserFace" runat="server" Text=""></pe:ExtendedLabel>
            </div>
            <div>
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td colspan="2">
                            <strong>【<pe:ExtendedLabel  HtmlEncode="false"  ID="LblUserName" runat="server" Text=""></pe:ExtendedLabel>】</strong></td>
                    </tr>
                    <tr>
                        <td align="right">
                            <strong>发表信息数：&nbsp;</strong></td>
                        <td>
                            <pe:ExtendedLabel  HtmlEncode="false"  ID="LblPassedItems" runat="server" Text=""></pe:ExtendedLabel>
                        </td>
                    </tr>
                    <tr runat="server" id="UserExp">
                        <td align="right">
                            <strong>用户积分：&nbsp;</strong></td>
                        <td>
                            <pe:ExtendedLabel  HtmlEncode="false"  ID="LblUserExp" runat="server" Text=""></pe:ExtendedLabel>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <strong>注册时间：&nbsp;</strong></td>
                        <td>
                            <pe:ExtendedLabel  HtmlEncode="false"  ID="LblUserRegTime" runat="server" Text=""></pe:ExtendedLabel>
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
                            <pe:ExtendedLabel  HtmlEncode="false"  ID="LblMessage" runat="server" Text="LblMessage"></pe:ExtendedLabel>
                            <pe:ExtendedLabel  HtmlEncode="false"  ID="LblUserShow" runat="server" Text="LblUserShow"></pe:ExtendedLabel>
                            <pe:ExtendedLabel  HtmlEncode="false"  ID="LblEmail" runat="server" Text="LblEmail"></pe:ExtendedLabel>
                        </td>
                        <td align="right">
                        </td>
                    </tr>
                </table>
                <pe:ExtendedLabel  HtmlEncode="false"  ID="LblContent" runat="server" Text=""></pe:ExtendedLabel>
            </div>
        </div>
    </div>
    <!--中部详细内容end-->
    <div id="bbs_bot">
        <div id="bbs_botleft">
            <pe:ExtendedLabel  HtmlEncode="false"  ID="lblCommentTime" runat="server" Text=""></pe:ExtendedLabel>
            <pe:ExtendedLabel  HtmlEncode="false"  ID="LblUpdateDateTime" runat="server" Text=""></pe:ExtendedLabel>
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
                <asp:Label ID="LblCommentTitle" runat="server" Text="评论引用"></asp:Label>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right" style="width: 150px">
                <strong>评分：&nbsp;</strong></td>
            <td class="tdbg" align="left">
                <pe:ScoreControl ID="ScoreControl" runat="server" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right" style="width: 150px; height: 42px;">
                <strong>观点：&nbsp;</strong></td>
            <td class="tdbg" align="left" style="height: 42px">
                <asp:RadioButtonList ID="RadlPosition" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="1">赞同</asp:ListItem>
                    <asp:ListItem Value="0" Selected="True">中立</asp:ListItem>
                    <asp:ListItem Value="-1">反对</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right" style="width: 150px; height: 42px;">
                <strong>评论标题：&nbsp;</strong></td>
            <td class="tdbg" align="left" style="height: 42px">
                <asp:TextBox ID="TxtCommentTitle" runat="server" Style="width: 250px" />
                <pe:RequiredFieldValidator ID="ValrCommentTitle" runat="server" ErrorMessage="评论标题不能为空！"
                    ControlToValidate="TxtCommentTitle" Display="Dynamic" SetFocusOnError="True"></pe:RequiredFieldValidator>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right" style="width: 150px">
                <strong>评论：&nbsp;</strong></td>
            <td class="tdbg" align="left">
                <asp:TextBox ID="TxtCommentRestore" runat="server" Height="118px" TextMode="MultiLine"
                    Width="355px"></asp:TextBox>
                <pe:RequiredFieldValidator ID="ValrCommentRestore" ControlToValidate="TxtCommentRestore"
                    runat="server" ErrorMessage="评论引用不能为空！" Display="Dynamic"></pe:RequiredFieldValidator>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right" style="width: 150px">
                <strong>评论是否隐藏：&nbsp;<br />
                    <span style="color: #336600">(隐藏只有评论<br />
                        人才能看见)</span></strong></td>
            <td class="tdbg" align="left">
                <asp:CheckBox ID="ChkIsPrivate" runat="server" />
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
