<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="True"
    EnableEventValidation="false" Inherits="EasyOne.WebSite.Admin.Contents.ContentView" Codebehind="ContentView.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" CurrentNode="查看内容"
        runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">

    <script language="JavaScript" type="text/JavaScript">
   var tID=0;
   var arrTabTitle = new Array("TabTitle0","TabTitle1","<%= TabTitle5.ClientID %>","<%= TabTitle6.ClientID %>");
   var arrTrs0 = new Array(<%= arrTrs0 %>);
   var arrTrs1 = new Array(<%= arrTrs1 %>);
   var arrTrs2 = new Array("TabsCharge");
   var arrTrs3 = new Array("TabsSign");

   var arrTab = new Array(arrTrs0,arrTrs1,arrTrs2,arrTrs3);
   function ShowTabs(ID)
   {
       if(ID!=tID)
       {
           document.getElementById(arrTabTitle[tID].toString()).className = "tabtitle";
           document.getElementById(arrTabTitle[ID].toString()).className = "titlemouseover";
           
           for (var i = 0; i < arrTab.length; i++) 
           {
                var tab = arrTab[i];
                if(i==ID)
                {
                    for (var j = 0; j < tab.length; j++) 
                    {
                       document.getElementById(tab[j].toString()).style.display = "";
                    }
                }
                else
                {
                    for (var j = 0; j < tab.length; j++) 
                    {
                       document.getElementById(tab[j].toString()).style.display = "none";
                    }
                }
           }
           
           tID=ID;
        }
    }
    </script>

    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td class="spacingtitle" colspan="7" align="center">
                查看内容
            </td>
        </tr>
        <tr>
            <td>
                <br />
            </td>
        </tr>
        <tr align="center">
            <td id="TabTitle0" class="titlemouseover" onclick="ShowTabs(0)">
                基本信息</td>
            <td id="TabTitle1" class="tabtitle" onclick="ShowTabs(1)">
                属性信息</td>
            <td id="TabTitle5" class="tabtitle" runat="server" onclick="ShowTabs(2)">
                收费选项</td>
            <td id="TabTitle6" runat="server" class="tabtitle" onclick="ShowTabs(3)">
                签收选项</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>	
    <table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="border">
        <asp:Repeater ID="RptContent" runat="server" OnItemDataBound="RptContent_ItemDataBound">
            <ItemTemplate>
                <tr id="Tab" runat="server" class="tdbg">
                    <td class="tdbgleft" style="width: 15%;" align="right">
                        <strong>
                            <%# Eval("FieldAlias")%>
                            ：&nbsp;</strong></td>
                    <td class="tdbg" align="left" style="min-height:100%;word-break: break-all;white-space: normal;white-space: normal;word-wrap: break-word;line-break: strict;">
                        <asp:Literal ID="LitContentText" runat="server"></asp:Literal><asp:Panel ID="PnlContent"
                            runat="server">
                        </asp:Panel>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
        <tbody id="TabsCharge" style="display: none">
            <pe:ContentCharge ID="ContentCharge" runat="server" />
        </tbody>
        <tbody id="TabsSign" style="display: none">
            <tr class="tdbg">
                <td class="tdbgleft" align="right" style="width: 150px;">
                    <strong>文档类型：&nbsp;</strong>
                </td>
                <td class="tdbg" align="left">
                    <asp:Label ID="LblSigninType" runat="server"></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft" align="right" style="width: 150px;">
                    <strong>优先级：&nbsp;</strong><br />
                </td>
                <td class="tdbg" align="left">
                    <asp:Label ID="LblPriority" runat="server"></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft" align="right" style="width: 150px;">
                    <strong>签收截止日期：&nbsp;</strong><br />
                </td>
                <td class="tdbg" align="left">
                    <asp:Label ID="LblEndTime" runat="server"></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft" align="right" style="width: 150px;">
                    <strong>签收状态：&nbsp;</strong><br />
                </td>
                <td class="tdbg" align="left">
                    <asp:Label ID="LblStatus" runat="server"></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft" align="right" style="width: 150px;">
                    <strong>签收记录：&nbsp;</strong>
                </td>
                <td class="tdbg" align="left">
                    <table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="border">
                        <tr class="tdbgleft">
                            <td align="center">
                                签收用户</td>
                            <td align="center">
                                是否签收</td>
                            <td align="center">
                                签收时间</td>
                            <td align="center">
                                签收IP</td>
                        </tr>
                        <asp:Repeater ID="RptSigninLog" runat="server">
                            <ItemTemplate>
                                <tr class="tdbg">
                                    <td align="left">
                                        <%#Eval("UserName") %>
                                    </td>
                                    <td align="center">
                                        <%#(bool)Eval("IsSignin") ? "<span style=\"color:blue\"><b>√</b></span>" : "<span style=\"color:Red\"><b>×</b></span>"%>
                                    </td>
                                    <td align="center">
                                        <%#(bool)Eval("IsSignin") ? Eval("SigninTime").ToString() : "" %>
                                    </td>
                                    <td align="center">
                                        <%#Eval("IP") %>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </td>
            </tr>
        </tbody>
    </table>
    <br />
    <pe:ExtendedNodeButton ID="EBtnModify" Text="修改/审核" IsChecked="false" NodeId='<%# Eval("NodeID")%>'
        OperateCode="ContentManage" OnClick="EBtnModify_Click" runat="server" />
    <pe:ExtendedNodeButton ID="EBtnDelete" Text="删除" IsChecked="false" NodeId='<%# Eval("NodeID")%>'
        OperateCode="ContentManage" OnClientClick="return confirm('确定要删除该信息吗？')" OnClick="EBtnDelete_Click"
        runat="server" />
    <pe:ExtendedNodeButton ID="EBtnCheck" Text="审核通过" IsChecked="false" NodeId='<%# Eval("NodeID")%>'
        OperateCode="ContentManage" OnClientClick="return confirm('确定要审核通过该信息吗？')" OnClick="EBtnCheck_Click"
        runat="server" />
    <div id="Div1" style="display: none;">
        <pe:ExtendedNodeButton ID="EBtnMove" Text=" 移动 " IsChecked="true" NodeId='<%# Eval("NodeID")%>'
            OperateCode="ContentManage" OnClick="EBtnMove_Click" runat="server" />
        <pe:ExtendedNodeButton ID="EBtnBack" Text="直接退稿" IsChecked="true" NodeId='<%# Eval("NodeID")%>'
            OperateCode="ContentManage" OnClick="EBtnBack_Click" runat="server" />
        <pe:ExtendedNodeButton ID="EBtnCPass" Text="取消审核" IsChecked="true" NodeId='<%# Eval("NodeID")%>'
            OperateCode="ContentManage" OnClick="EBtnCPass_Click" runat="server" />
        <pe:ExtendedNodeButton ID="EBtnTop" Text="设为固顶" IsChecked="true" NodeId='<%# Eval("NodeID")%>'
            OperateCode="ContentManage" OnClick="EBtnTop_Click" runat="server" />
        <pe:ExtendedNodeButton ID="EBtnEltiy" Text="设为推荐" IsChecked="true" NodeId='<%# Eval("NodeID")%>'
            OperateCode="ContentManage" OnClick="EBtnEltiy_Click" runat="server" />
    </div>
    <br />
    <br />
    <table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="border">
        <tr>
            <td class="tdbg">
                <li>上一篇：<asp:HyperLink ID="LnkGetPrevInfo" runat="server"></asp:HyperLink></li>
                <li>下一篇：<asp:HyperLink ID="LnkGetNextInfo" runat="server"></asp:HyperLink></li>
            </td>
        </tr>
    </table>
    <br />
    <!-- 显示评论树开始 -->
    &nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="BtnAllComment" runat="server" Text="所有评论" OnClick="BtnAllComment_Click" />&nbsp;
    <asp:Button ID="BtnAuditedComment" runat="server" Text="已审核评论" OnClick="BtnAuditedComment_Click" />&nbsp;
    <asp:Button ID="BtnUNAuditedComment" runat="server" Text="未审核评论" OnClick="BtnUNAuditedComment_Click" />
    <br />
    <br />

    <script src="<%=Path%>/JS/jsPopup.js" type="text/javascript"></script>

    <div id="bbs_title">
        <span id="bbs_title_right"><a style="cursor: hand" onclick="javascript:window.print()"
            onmousemove="status='打印本页';" onmouseout="status='';">打印</a> <a style="cursor: hand"
                onclick="window.external.AddFavorite(document.location.href,document.title)"
                onmousemove="status='收藏本页';" onmouseout="status='';">收藏</a> </span>标题：<asp:Label
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
                        <pe:ExtendedLabel HtmlEncode="false" ID="LblUserFace" runat="server" Text=""></pe:ExtendedLabel><br />
                        <strong>
                            <%#Eval("UserName") %>
                        </strong>
                    </div>
                    <div style="margin-left: 35px; text-align: left;">
                        <pe:ExtendedLabel HtmlEncode="false" ID="LblCommentContent" runat="server" Text=""></pe:ExtendedLabel>
                    </div>
                </div>
                <div id="bbs_right">
                    <div id="bbs_text">
                        <table border="0" cellpadding="0" cellspacing="3" width="100%" id="bbs_center_line">
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
                                <td align="right">
                                    <pe:ExtendedLabel HtmlEncode="false" ID="LblNum" runat="server" Text="Label"></pe:ExtendedLabel>
                                </td>
                            </tr>
                        </table>
                        <pe:ExtendedLabel HtmlEncode="false" ID="LblContent" runat="server" Text="Label"></pe:ExtendedLabel>
                    </div>
                </div>
                <div class="clearbox2">
                </div>
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
                <table border="0" cellpadding="0" cellspacing="1" width="100%">
                    <tr>
                        <td align="left">
                            &nbsp;&nbsp;<a href="ContentView.aspx?Action=AddPKZone&CommentID=<%#Eval("CommentID")%>&GeneralId=<%#Eval("GeneralId")%>&Title=<%#Server.UrlEncode(Request["Title"])%>&RadlPosition=1&ItemContent=<%#Server.UrlEncode("精彩一针见血")%>">精彩一针见血</a></td>
                    </tr>
                    <tr>
                        <td align="left">
                            &nbsp;&nbsp;<a href="ContentView.aspx?Action=AddPKZone&CommentID=<%#Eval("CommentID")%>&GeneralId=<%#Eval("GeneralId")%>&Title=<%#Server.UrlEncode(Request["Title"])%>&RadlPosition=1&ItemContent=<%#Server.UrlEncode("观点独到")%>">观点独到</a></td>
                    </tr>
                    <tr>
                        <td align="left">
                            &nbsp;&nbsp;<a href="ContentView.aspx?Action=AddPKZone&CommentID=<%#Eval("CommentID")%>&GeneralId=<%#Eval("GeneralId")%>&Title=<%#Server.UrlEncode(Request["Title"])%>&RadlPosition=1&ItemContent=<%#Server.UrlEncode("说的很对")%>">说的很对</a></td>
                    </tr>
                    <tr>
                        <td align="left">
                            &nbsp;&nbsp;<a href="ContentView.aspx?Action=AddPKZone&CommentID=<%#Eval("CommentID")%>&GeneralId=<%#Eval("GeneralId")%>&Title=<%#Server.UrlEncode(Request["Title"])%>&RadlPosition=1&ItemContent=<%#Server.UrlEncode("你说的有道理")%>">你说的有道理</a></td>
                    </tr>
                </table>
            </div>
            <div id="Oppose<%#Eval("CommentID")%>" style="position: absolute; width: 200px; border: solid 1px black;
                background-color: white; display: none;">
                <table border="0" cellpadding="0" cellspacing="1" width="100%">
                    <tr>
                        <td align="left">
                            &nbsp;&nbsp;<a href="ContentView.aspx?Action=AddPKZone&CommentID=<%#Eval("CommentID")%>&GeneralId=<%#Eval("GeneralId")%>&Title=<%#Server.UrlEncode(Request["Title"])%>&RadlPosition=-1&ItemContent=<%#Server.UrlEncode("乱七八糟说些什么")%>">乱七八糟说些什么</a></td>
                    </tr>
                    <tr>
                        <td align="left">
                            &nbsp;&nbsp;<a href="ContentView.aspx?Action=AddPKZone&CommentID=<%#Eval("CommentID")%>&GeneralId=<%#Eval("GeneralId")%>&Title=<%#Server.UrlEncode(Request["Title"])%>&RadlPosition=-1&ItemContent=<%#Server.UrlEncode("你说的没道理")%>">你说的没道理</a></td>
                    </tr>
                    <tr>
                        <td align="left">
                            &nbsp;&nbsp;<a href="ContentView.aspx?Action=AddPKZone&CommentID=<%#Eval("CommentID")%>&GeneralId=<%#Eval("GeneralId")%>&Title=<%#Server.UrlEncode(Request["Title"])%>&RadlPosition=-1&ItemContent=<%#Server.UrlEncode("简直是胡说八道")%>"">简直是胡说八道</a></td>
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
</asp:Content>
