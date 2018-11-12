<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Controls.CommentManage" Codebehind="CommentManage.ascx.cs" %>
 <!-- 显示评论树开始 -->
    &nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="BtnAllComment" runat="server" Text="所有评论" OnClick="BtnAllComment_Click" />&nbsp;
    <asp:Button ID="BtnAuditedComment" runat="server" Text="已审核评论" OnClick="BtnAuditedComment_Click" />&nbsp;
    <asp:Button ID="BtnUNAuditedComment" runat="server" Text="未审核评论" OnClick="BtnUNAuditedComment_Click" />
    <br />
    <br />

    <script src="<%=Path%>/JS/jsPopup.js" type="text/javascript"></script>

    <div id="bbs_title">标题：<asp:Label
                    ID="LblTitle" runat="server" Text="Label"></asp:Label>
    </div>
    <asp:Repeater ID="RptCommentList" runat="server" OnItemDataBound="RptCommentContent_ItemDataBound">
        <HeaderTemplate>
        </HeaderTemplate>
        <ItemTemplate>
            <!--中部详细内容-->
            <div id="bbs_center">
                <div id="bbs_left">
                    <div style="margin-top: 15px;">
                        <asp:Image ID="ImgUserFace" runat="server" /><br />
                        <strong> 
                            <asp:Label ID="LblUserName" runat="server" Text='<%#Eval("UserName") %>'></asp:Label>
                        </strong>
                    </div>
                    <div style="margin-left: 35px; text-align: left;">
                        <asp:Label ID="LblCommentContent" runat="server" Text=""></asp:Label><br />
                        <pe:ExtendedLabel ID="LblUserExp" runat="server"></pe:ExtendedLabel>
                        <asp:Label ID="LblUserRegTime" runat="server" ></asp:Label>
                    </div>
                </div>
                <div id="bbs_right">
                    <div id="bbs_text" >
                        <table border='0' cellpadding='0' cellspacing='3' width='100%' id="bbs_center_line" >
                            <tr>
                                <td>
                                    <a href='../Accessories/MessageSend.aspx?UserName=<%#Server.UrlEncode(Eval("UserName").ToString())%>'>
                                        <img alt="发送短信" src='<%=Path%>/Images/Comment/message.gif' align="Absmiddle" style="border: 0px;" /></a>
                                    <a href='../User/UserShow.aspx?UserId=<%#Eval("UserID")%>'>
                                        <img alt="用户信息" src='<%=Path%>/Images/Comment/profile.gif' align="Absmiddle" style="border: 0px;" /></a>
                                    <a href='mailto:<%#Eval("Email")%>'>
                                        <img alt="邮箱" src='<%=Path%>/Images/Comment/email.gif' align="Absmiddle" style="border: 0px;" /></a>
                                    &nbsp;&nbsp;[支持：<asp:Label ID="LblSustain" runat="server" Text="Label" />人 反对：<asp:Label
                                        ID="LblOppose" runat="server" Text="Label" />人 中立：<asp:Label ID="LblNeutralismNetizen"
                                            runat="server" Text="Label" />人]
                                </td>
                                <td align='right'>
                                    <pe:ExtendedLabel HtmlEncode="false" ID="LblNum" runat="server"></pe:ExtendedLabel>
                                </td>
                            </tr>
                        </table>
                        <asp:Label ID="LblContent" runat="server"></asp:Label>
                        <pe:ExtendedLabel ID="LblReply" runat="server"></pe:ExtendedLabel>
                    </div>
                </div>
                <div class="clearbox2"></div>
            </div>
            <!--中部详细内容end-->
            <div id="bbs_bot">
                <div id="bbs_botleft">
                    <img src='<%=Path%>/Images/Comment/ip.gif' alt="评论发布时间" align="Absmiddle" />
                    <%#Eval("UpdateDateTime")%>
                </div>
                <div id="bbs_botright">
                    <div style="padding-right: 4px; float: right;">
                        操作：
                        <pe:ExtendedLabel HtmlEncode="false" ID="LblPKZone" runat="server" Text=""></pe:ExtendedLabel>
                        <pe:ExtendedLabel HtmlEncode="false" ID="LblPKAgree" runat="server" Text=""></pe:ExtendedLabel>
                        <pe:ExtendedLabel HtmlEncode="false" ID="LblPKOppose" runat="server" Text=""></pe:ExtendedLabel>
                        <pe:ExtendedLabel HtmlEncode="false" ID="LblExcerpt" runat="server" Text=""></pe:ExtendedLabel>
                        <pe:ExtendedLabel HtmlEncode="false" ID="LblRestore" runat="server" Text=""></pe:ExtendedLabel>
                        <pe:ExtendedLabel HtmlEncode="false" ID="LblDelete" runat="server" Text=""></pe:ExtendedLabel>
                        <pe:ExtendedLabel HtmlEncode="false" ID="LblAuditing" runat="server" Text=""></pe:ExtendedLabel>
                        <pe:ExtendedLabel HtmlEncode="false" ID="LblIsElite" runat="server" Text=""></pe:ExtendedLabel>
                    </div>
                </div>
            </div>
            <div id="Agree<%#Eval("CommentID")%>" style="position: absolute; width: 200px; border: solid 1px black;
                background-color: white; display: none;">
                <table border='0' cellpadding='0' cellspacing='1' width='100%'>
                    <tr>
                        <td align="left">
                            &nbsp;&nbsp;<a href="<%=m_ViewFile%>?Action=AddPKZone&CommentID=<%#Eval("CommentID")%>&GeneralId=<%#Eval("GeneralId")%>&Title=<%#Server.UrlEncode(Request["Title"])%>&RadlPosition=1&ItemContent=<%#Server.UrlEncode("精彩一针见血")%>">精彩一针见血</a></td>
                    </tr>
                    <tr>
                        <td align="left">
                            &nbsp;&nbsp;<a href="<%=m_ViewFile%>?Action=AddPKZone&CommentID=<%#Eval("CommentID")%>&GeneralId=<%#Eval("GeneralId")%>&Title=<%#Server.UrlEncode(Request["Title"])%>&RadlPosition=1&ItemContent=<%#Server.UrlEncode("观点独到")%>">观点独到</a></td>
                    </tr>
                    <tr>
                        <td align="left">
                            &nbsp;&nbsp;<a href="<%=m_ViewFile%>?Action=AddPKZone&CommentID=<%#Eval("CommentID")%>&GeneralId=<%#Eval("GeneralId")%>&Title=<%#Server.UrlEncode(Request["Title"])%>&RadlPosition=1&ItemContent=<%#Server.UrlEncode("说的很对")%>">说的很对</a></td>
                    </tr>
                    <tr>
                        <td align="left">
                            &nbsp;&nbsp;<a href="<%=m_ViewFile%>?Action=AddPKZone&CommentID=<%#Eval("CommentID")%>&GeneralId=<%#Eval("GeneralId")%>&Title=<%#Server.UrlEncode(Request["Title"])%>&RadlPosition=1&ItemContent=<%#Server.UrlEncode("你说的有道理")%>">你说的有道理</a></td>
                    </tr>
                </table>
            </div>
            <div id="Oppose<%#Eval("CommentID")%>" style="position: absolute; width: 200px; border: solid 1px black;
                background-color: white; display: none;">
                <table border='0' cellpadding='0' cellspacing='1' width='100%'>
                    <tr>
                        <td align="left">
                            &nbsp;&nbsp;<a href="<%=m_ViewFile%>?Action=AddPKZone&CommentID=<%#Eval("CommentID")%>&GeneralId=<%#Eval("GeneralId")%>&Title=<%#Server.UrlEncode(Request["Title"])%>&RadlPosition=-1&ItemContent=<%#Server.UrlEncode("乱七八糟说什么")%>">乱七八糟说什么</a></td>
                    </tr>
                    <tr>
                        <td align="left">
                            &nbsp;&nbsp;<a href="<%=m_ViewFile%>?Action=AddPKZone&CommentID=<%#Eval("CommentID")%>&GeneralId=<%#Eval("GeneralId")%>&Title=<%#Server.UrlEncode(Request["Title"])%>&RadlPosition=-1&ItemContent=<%#Server.UrlEncode("你说的没道理")%>">你说的没道理</a></td>
                    </tr>
                    <tr>
                        <td align="left">
                            &nbsp;&nbsp;<a href="<%=m_ViewFile%>?Action=AddPKZone&CommentID=<%#Eval("CommentID")%>&GeneralId=<%#Eval("GeneralId")%>&Title=<%#Server.UrlEncode(Request["Title"])%>&RadlPosition=-1&ItemContent=<%#Server.UrlEncode("简直是胡说八道")%>">简直是胡说八道</a></td>
                    </tr>
                </table>
            </div>
        </ItemTemplate>
        <FooterTemplate>
        </FooterTemplate>
    </asp:Repeater>
    <!-- 显示评论树结束 -->
    <center>
        <pe:AspNetPager ID="Pager" runat="server" OnPageChanged="Pager_PageChanged">
        </pe:AspNetPager>
    </center>
    <asp:HiddenField ID="HdnListType" runat="server" Value="0" />
<script type="text/javascript">
function addFavorite()
{
    if (document.all)
    {
       window.external.addFavorite(document.location.href,document.title);
    }
    else 
    {
        try
        {
            if (window.sidebar)
            {
                if(!window.sidebar.addPanel(document.location.href,document.title, ""))
                {
                    alert("对不起，您的浏览器不支持收藏此链接！");
                }
            }
        }
        catch(err)
        {
            alert("对不起，您的浏览器不支持收藏此链接！");
        }
    }
}
</script>