<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.CommentUI.AddComment"
    Title="发表评论" ValidateRequest="false" Codebehind="AddComment.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>发表评论</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="border">
                <tr>
                    <td class="spacingtitle" colspan="2" align="center">
                        <asp:Label ID="LblCommentTitle" runat="server" Text="发表评论"></asp:Label>
                    </td>
                </tr>
                <tr class="tdbg">
                    <td class="tdbgleft" align="right" style="width: 150px">
                        <strong>评分：&nbsp;</strong></td>
                    <td class="tdbg" align="left">
                        <asp:RadioButtonList ID="RadlScore" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1">1分</asp:ListItem>
                            <asp:ListItem Value="2">2分</asp:ListItem>
                            <asp:ListItem Selected="True" Value="3">3分</asp:ListItem>
                            <asp:ListItem Value="4">4分</asp:ListItem>
                            <asp:ListItem Value="5">5分</asp:ListItem>
                        </asp:RadioButtonList>
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
                        <asp:HiddenField ID="HdnGeneralId" runat="server" />
                        <asp:HiddenField ID="HdnNodeId" runat="server" />
                        <asp:Button ID="BtnSubmit" runat="server" Text="发表" OnClick="BtnSubmit_Click" />
                    </td>
                </tr>
                <tr style="background-color: #f5f5f5;">
                    <td colspan="2">
                        <div>
                            * 请遵守<a href="/Reg/bbs.htm"><span style="color: Red">《互联网电子公告服务管理规定》</span></a>及中华人民共和国其他各项有关法律法规。<br />
                            * 严禁发表危害国家安全、损害国家利益、破坏民族团结、破坏国家宗教政策、破坏社会稳定、侮辱、诽谤、教唆、淫秽等内容的评论 。<br />
                            * 用户需对自己在使用本站服务过程中的行为承担法律责任（直接或间接导致的）。<br />
                            * 本站管理员有权保留或删除评论内容。<br />
                            * 评论内容只代表网友个人观点，与本网站立场无关。
                            <br />
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
