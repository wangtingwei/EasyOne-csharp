<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.User.Contents.CommentModify" Codebehind="CommentModify.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>用户评论管理</title>
</head>
<body>
    <form id="form1" runat="server">
        <!-- 显示评论树开始 -->
        <table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="border">
            <tr align="center">
                <td colspan="2" class="spacingtitle">
                    <asp:Label ID="LblCommentTitle" runat="server" Text="评论修改" />
                </td>
            </tr>
            <asp:Label ID="LblUserInfo" runat="server" Text="评论修改" Visible="false" /><tr class="tdbg"
                runat="server" id="trUser">
                <td class="tdbgleft" align="right" style="width: 150px;">
                    <strong>用户名：&nbsp;</strong></td>
                <td class="tdbg" align="left">
                    <asp:Label ID="LblUserName" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg" runat="server" id="trEmail">
                <td class="tdbgleft" align="right" style="width: 150px;">
                    <strong>邮件：&nbsp;</strong></td>
                <td class="tdbg" align="left">
                    <asp:TextBox ID="TxtEmail" runat="server" Style="width: 120px" />
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
                    <strong>评论内容：&nbsp;</strong></td>
                <td class="tdbg" align="left">
                    <asp:TextBox ID="TxtCommentContent" runat="server" Height="118px" TextMode="MultiLine"
                        Width="355px"></asp:TextBox>
                    <pe:RequiredFieldValidator ID="ValrCommentContent" ControlToValidate="TxtCommentContent"
                        runat="server" ErrorMessage="评论回复不能为空！" Display="Dynamic"></pe:RequiredFieldValidator>
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
                <td class="tdbgleft" align="right" style="width: 150px">
                    <strong>评论是否隐藏：&nbsp;<br />
                        <span style="color: #336600">(隐藏只有评论<br />
                            人才能看见)</span></strong></td>
                <td class="tdbg" align="left">
                    <asp:CheckBox ID="ChkReplyIsPrivate" runat="server" />
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
    </form>
</body>
</html>
