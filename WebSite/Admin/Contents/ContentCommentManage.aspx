<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPage.master"
    Inherits="EasyOne.WebSite.Admin.Contents.ContentCommentManage" Codebehind="ContentCommentManage.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <div style="text-align: left">
        <br />
        <asp:LinkButton ID="LbtnAllComment" runat="server" Text="&nbsp;&nbsp;<b><span style='color:White'>所有评论</span></b>"
            Width="111" Height="28" OnClick="LbtnAllComment_Click"></asp:LinkButton>
        &nbsp;
        <asp:LinkButton ID="LbtnAuditedComment" runat="server" Text="&nbsp;&nbsp;<b><span style='color:White'>已审核评论</span></b>"
            Width="111" Height="28" OnClick="LbtnAuditedComment_Click"></asp:LinkButton>
        &nbsp;
        <asp:LinkButton ID="LbtnUNAuditedComment" runat="server" Text="&nbsp;&nbsp;<b><span style='color:White'>未审核评论</span></b>"
            Width="111" Height="28" OnClick="LbtnUNAuditedComment_Click"></asp:LinkButton>
        <br />
        <br />
        <!-- 显示评论树开始 -->
        <div id="bbs_tit">
            <span style="background-image: url(../Images/Comment/border.gif); line-height: 25px;
                float: right; font-weight: normal;"><a href='javascript:window.print()' class="print">
                    打印</a> <a style="cursor: hand" class="print" onclick="window.external.AddFavorite(document.location.href,document.title)"
                        onmousemove="status='收藏本页';" onmouseout="status='';">收藏</a> </span>标题：<asp:Label
                            ID="LblTitle" runat="server" Text="Label"></asp:Label></div>
        <asp:Repeater ID="RptModelList" runat="server" DataSourceID="OdsContentCommentManageList"
            OnItemDataBound="RptCommentContent_ItemDataBound">
            <HeaderTemplate>
            </HeaderTemplate>
            <ItemTemplate>
                <!--中部详细内容-->
                <div id="bbs_center">
                    <div id="bbs_left">
                        <div style="margin-top: 15px;">
                            <asp:Label ID="LblUserFace" runat="server" Text="Label"></asp:Label>
                        </div>
                        <div>
                            <table cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td colspan="2">
                                        <strong>【<%#Eval("UserName") %>】</strong></td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <strong>发表信息数：&nbsp;</strong></td>
                                    <td>
                                        <%#Eval("PassedItems") %>
                                    </td>
                                </tr>
                                <tr runat="server" id="UserExp">
                                    <td align="right">
                                        <strong>用户积分：&nbsp;</strong></td>
                                    <td>
                                        <%#Eval("UserExp")%>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <strong>管理员回复：&nbsp;</strong></td>
                                    <td>
                                        <%#Eval("ReplyAdmin") %>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <strong>注册时间：&nbsp;</strong></td>
                                    <td>
                                        <%#Eval("UserRegTime", "{0:yyyy-MM-dd}")%>
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
                                        <a href='../Accessories/MessageSend.aspx?UserName=<%#Eval("UserName").ToString()%>'>
                                            <img alt="发送短信" src="../Images/Comment/message.gif" style="border: 0px" /></a>
                                        <a href='../User/UserShow.aspx?UserId=<%#Eval("UserID")%>'>
                                            <img alt="用户信息" src="../Images/Comment/profile.gif" style="border: 0px" /></a>
                                        <a href='mailto:<%#Eval("Email")%>'>
                                            <img alt="邮箱" src="../Images/Comment/email.gif" style="border: 0px" /></a>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="LblNum" runat="server" Text="Label"></asp:Label>
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
                        <img src="../Images/Comment/ip.gif" alt="评论发布时间" />
                        <%#Eval("UpdateDateTime")%>
                    </div>
                    <div id="bbs_botright">
                        <div style="padding: 4px; float: right;">
                        </div>
                        <div style="padding: 4px; float: right;">
                            操作： <a href="#">引用</a> <a href="#">回复</a> <a href='ContentCommentManage.aspx?Action=Delete&CommentID=<%#Eval("CommentID")%>&GeneralId=<%#Eval("GeneralId")%>&Title=<%#Server.UrlEncode(Request["Title"])%>'
                                onclick="return confirm('确定要删除此评论吗？');">删除</a>
                            <asp:Label ID="LblAuditing" runat="server" Text="Label"></asp:Label>
                            <asp:Label ID="LblIsElite" runat="server" Text="Label"></asp:Label>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
            <FooterTemplate>
            </FooterTemplate>
        </asp:Repeater>
        <!-- 显示评论树结束 -->
        <asp:ObjectDataSource ID="OdsContentCommentManageList" runat="server" SelectMethod="GetList"
            TypeName="EasyOne.Contents.Comment">
            <SelectParameters>
                <asp:Parameter DefaultValue="0" Name="startRowIndexId" Type="Int32" />
                <asp:Parameter DefaultValue="0" Name="maxNumberRows" Type="Int32" />
                <asp:QueryStringParameter DefaultValue="0" Name="generalId" QueryStringField="GeneralID"
                    Type="Int32" />
                <asp:QueryStringParameter DefaultValue="0" Name="type" QueryStringField="ListType"
                    Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>
</asp:Content>
