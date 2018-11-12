<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.User.Contents.CommentManage" Codebehind="CommentManage.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>用户评论管理</title>
</head>
<body>
    <form id="form1" runat="server">
         <div id="NotAssignment" style="display: none;" runat ="server">
            <div>
                <table cellpadding="2" cellspacing="1" border="0" width="100%" class="border" align="center">
                    <tr class="tdbg">
                        <td valign="top" style="height: 100px;" align ="center">
                            <br />
                            <br />
                            没有评论信息！
                            <br />
                            <br /><br />
                        </td>
                    </tr>
                    <tr align="center" class="tdbg">
                        <td>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div>
            <asp:Repeater ID="RptCommentList" runat="server" OnItemDataBound="RptCommentManage_ItemDataBound">
                <HeaderTemplate>
                </HeaderTemplate>
                <ItemTemplate>
                    <pe:ExtendedLabel HtmlEncode="false" ID="LblRestoreBottom" runat="server" Text="" Visible="false" />
                    <pe:ExtendedLiteral HtmlEncode="false" ID="LblCommentHead" runat="server" Text="" Visible="false" />
                    <tr class="tdbg" onmouseout="this.className='tdbg'" onmouseover="this.className='tdbgmouseover'">
                        <td style="width: 30px;" align="center">
                            <span id="CommentID">
                                <%#Eval("CommentID")%>
                            </span>
                        </td>
                        <td>
                            <pe:ExtendedLabel ID="LblContent" runat="server" Text="" /></td>
                        <td style="width: 70px;" align="center">
                            <%#Eval("Score")%>
                        </td>
                        <td style="width: 200px;" align="center">
                            <pe:ExtendedLabel HtmlEncode="false" ID="LblUserRegTime" runat="server" Text="" />
                        </td>
                        <td style="width: 60px;" align="center">
                            <pe:ExtendedLabel HtmlEncode="false" ID="LblStatus" runat="server" Text="" />
                        </td>
                        <td style="width: 150px;" align="center">
                            <pe:ExtendedLabel HtmlEncode="false" ID="LblManage" runat="server" Text="" />
                        </td>
                    </tr>
                    <pe:ExtendedLabel  ID="LblRestore" runat="server" Text="" Visible="false" />
                </ItemTemplate>
                <FooterTemplate>
                </FooterTemplate>
            </asp:Repeater>
            <pe:ExtendedLabel HtmlEncode="false" ID="LblCommontBottom2" runat="server" Text="</table></td></tr></table>" />
            <br />
            <center>
                <pe:AspNetPager ID="Pager" runat="server" OnPageChanged="Pager_PageChanged">
                </pe:AspNetPager>
            </center>
        </div>
    </form>
</body>
</html>
