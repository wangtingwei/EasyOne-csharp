<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Template.FrontPageTemplateConfig"
    Title="动态页面模板配置" Codebehind="FrontPageTemplateConfig.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="server">

    <script id="ShowTab" type="text/javascript">
       var tID=0;
       var arrTabTitle = new Array("TabTitle0","TabTitle1","TabTitle2","TabTitle3","<%= TabTitle4.ClientID %>","TabTitle5","TabTitle6","TabTitle7","TabTitle8");
       var arrTrs0 = new Array("Tab0");
       var arrTrs1 = new Array("Tab1");
       var arrTrs2 = new Array("Tab2");
       var arrTrs3 = new Array("Tab3");
       var arrTrs4 = new Array("Tab4");
       var arrTrs5 = new Array("Tab5");
       var arrTrs6 = new Array("Tab6");
       var arrTrs7 = new Array("Tab7");
       var arrTrs8 = new Array("Tab8");
       var arrTab = new Array(arrTrs0,arrTrs1,arrTrs2,arrTrs3,arrTrs4,arrTrs5,arrTrs6,arrTrs7,arrTrs8);
       
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
        <tr align="center">
            <td id="TabTitle0" class="titlemouseover" onclick="ShowTabs(0)">
                前台功能页
            </td>
            <td id="TabTitle2" class="tabtitle" onclick="ShowTabs(2)">
                好友/短消息
            </td>
            <td id="TabTitle3" class="tabtitle" onclick="ShowTabs(3)">
                充值管理
            </td>
            <td id="TabTitle4" class="tabtitle" onclick="ShowTabs(4)" runat="server">
                信息管理
            </td>
            <td id="TabTitle6" class="tabtitle" onclick="ShowTabs(6)">
                帐户管理
            </td>
            <td id="TabTitle7" class="tabtitle" onclick="ShowTabs(7)">
                在线支付
            </td>
            <td id="TabTitle8" class="tabtitle" onclick="ShowTabs(8)">
                评论/RSS/WAP
            </td>
            <td id="TabTitle5" class="tabtitle" style="<%= IsShow()%>" onclick="ShowTabs(5)">
                商店管理
            </td>
            <td id="TabTitle1" class="tabtitle" style="<%= IsShow()%>" onclick="ShowTabs(1)">
                订购流程
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <%--前台功能页--%>
        <tbody id="Tab0">
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>动态页默认模板：</strong><br />
                    动态页不指定将使用此模板
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateDynamicPageDefault" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelDynamicPageDefault" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>用户登录页模板：</strong><br />
                    User/Login.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateLogin" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelLogin" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td style="width: 30%" class="tdbgleft">
                    <strong>用户注册页模板：</strong><br />
                    User/Register.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateRegister" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelRegister" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>注册认证页模板：</strong><br />
                    User/RegisterCheck.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateRegisterCheck" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelRegisterCheck" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>成功信息提示页模板：</strong><br />
                    Prompt/ShowSuccess.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateShowSuccess" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelShowSuccess" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>错误信息提示页模板：</strong><br />
                    Prompt/ShowError.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateShowError" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelShowError" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>框架内成功信息提示页模板：</strong><br />
                    Prompt/ShowSuccess.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateShowUserSuccess" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelShowUserSuccess" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>框架内错误信息提示页模板：</strong><br />
                    Prompt/ShowError.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateShowUserError" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelShowUserError" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>注册我的企业（第一步）模板：</strong><br />
                    User/Company/RegCompany.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateRegCompany" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelRegCompany" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>注册我的企业（第二步）模板：</strong><br />
                    User/Company/RegCompany2.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateRegCompany2" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelRegCompany2" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>投票结果页模板：</strong><br />
                    Vote.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateVote" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelVote" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>在线用户页模板：</strong><br />
                    Analytics/ShowOnline.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateShowOnline" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelShowOnline" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>在线播放页模板：</strong><br />
                    ShowDownloadUrl.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateShowDownloadUrl" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelShowDownloadUrl" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>专题首页模板：</strong><br />
                    Special.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateSpecial" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelSpecial" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>全站搜索结果页模板：</strong><br />
                    Search.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateSearch" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelSearch" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>下载错误信息页模板：</strong><br />
                    ShowDownloadError.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateShowDownloadError" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelShowDownloadError" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>作者列表页模板：</strong><br />
                    ShowAuthorList.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateShowAuthorList" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelShowAuthorList" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>作者详细信息页模板：</strong><br />
                    ShowAuthor.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateShowAuthor" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelShowAuthor" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>来源列表页模板：</strong><br />
                    ShowCopyFromList.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateShowCopyFromList" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelShowCopyFromList" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>来源详细信息页模板：</strong><br />
                    ShowCopyFrom.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateShowCopyFrom" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelShowCopyFrom" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>签写留言页模板：</strong><br />
                    GuestWrite.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateGuestWrite" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelGuestWrite" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>匿名用户查询订单页模板：</strong><br />
                    OrderForm.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateOrderForm" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelOrderForm" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
        </tbody>
        <%--订购流程--%>
        <tbody id="Tab1" style="display: none">
            <tr class="tdbg">
                <td style="width: 30%" class="tdbgleft">
                    <strong>购物车模板：</strong><br />
                    Shop/ShoppingCart.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateShoppingCart" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelShoppingCart" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>快速登陆注册模板：</strong><br />
                    Shop/FastRegister.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateFastRegister" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelFastRegister" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>收银台模板：</strong><br />
                    Shop/Payment.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplatePayment" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelPayment" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>订单预览页模板：</strong><br />
                    Shop/Preview.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplatePreview" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelPreview" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>订购成功模板：</strong><br />
                    Shop/OrderSuccess.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateOrderSuccess" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelOrderSuccess" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
        </tbody>
        <%--好友/短消息--%>
        <tbody id="Tab2" style="display: none">
            <tr class="tdbg">
                <td style="width: 30%" class="tdbgleft">
                    <strong>创建好友组模板：</strong><br />
                    User/UserFriend/FriendGroup.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateFriendGroup" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelFriendGroup" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>好友组管理模板：</strong><br />
                    User/UserFriend/FriendGroupManage.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateFriendGroupManage" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelFriendGroupManage" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>添加好友模板：</strong><br />
                    User/UserFriend/Friend.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateFriend" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelFriend" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>好友管理模板：</strong><br />
                    User/UserFriend/FriendManage.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateFriendManage" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelFriendManage" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td style="width: 30%" class="tdbgleft">
                    <strong>写短消息模板：</strong><br />
                    User/Message/Message.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateMessage" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelMessage" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>读短消息模板：</strong><br />
                    User/Message/MessageRead.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateMessageRead" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelMessageRead" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>短消息管理模板：</strong><br />
                    User/Message/MessageManager.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateMessageManager" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelMessageManager" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>弹出未读短消息模板：</strong><br />
                    User/Message/PopupMessageRead.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplatePopupMessageRead" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelPopupMessageRead" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
        </tbody>
        <%--充值管理--%>
        <tbody id="Tab3" style="display: none">
            <tr class="tdbg">
                <td style="width: 30%" class="tdbgleft">
                    <strong>充值管理模板：</strong><br />
                    User/Shop/CardList.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateCardList" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelCardList" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>充值卡充值模板：</strong><br />
                    User/Shop/Recharge.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateRecharge" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelRecharge" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>
                        <pe:ShowPointName ID="ShowPointName1" runat="server"></pe:ShowPointName>兑换模板：</strong><br />
                    User/Shop/Recharge.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateExchangePoint" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelExchangePoint" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>有效期兑换模板：</strong><br />
                    User/Info/ExchangeValidDate.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateExchangeValidDate" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelExchangeValidDate" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>有效期日志模板：</strong><br />
                    User/Info/ValidLogDetail.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateValidLog" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelValidLog" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>
                        <pe:ShowPointName ID="ShowPointName2" runat="server"></pe:ShowPointName>日志详细页模板：</strong><br />
                    User/Info/PointLog.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateValidLogDetail" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelValidLogDetail" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>
                        <pe:ShowPointName ID="ShowPointName3" runat="server"></pe:ShowPointName>日志模板：</strong><br />
                    User/Info/PointLog.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplatePointLog" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelPointLog" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>
                        <pe:ShowPointName ID="ShowPointName4" runat="server"></pe:ShowPointName>日志详细页模板：</strong><br />
                    User/Info/PointLogDetail.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplatePointLogDetail" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelPointLogDetail" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>赠送<pe:ShowPointName ID="ShowPointName5" runat="server"></pe:ShowPointName>页模板：</strong><br />
                    User/Info/DonatePoint.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateDonatePoint" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelDonatePoint" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>购买<pe:ShowPointName ID="ShowPointName6" runat="server"></pe:ShowPointName>页模板：</strong><br />
                    User/Info/BuyPoint.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateBuyPoint" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="Label4" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
        </tbody>
        <%--商店管理--%>
        <tbody id="Tab5" style="display: none">
            <tr class="tdbg">
                <td style="width: 30%" class="tdbgleft">
                    <strong>商品添加模板：</strong><br />
                    User/Shop/Product.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateProduct" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelProduct" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>商品管理模板：</strong><br />
                    User/Shop/ProductManage.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateProductManage" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelProductManage" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>我的订单模板：</strong><br />
                    User/Shop/OrderList.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateOrderList" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelOrderList" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>显示订单模板：</strong><br />
                    User/Shop/ShowOrder.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateShowOrder" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelShowOrder" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>余额支付模板：</strong><br />
                    User/Shop/AddPayment.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateAddPayment" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelAddPayment" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>下载软件模板：</strong><br />
                    User/Shop/DownList.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateDownList" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelDownList" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>显示下载软件信息模板：</strong><br />
                    User/Shop/ShowDownload.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateShowDownload" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LblShowDownload" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>收货人地址管理模板：</strong><br />
                    User/Shop/AddressManager.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateAddressManager" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelAddressManager" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>优惠券管理模板：</strong><br />
                    User/Shop/CouponList.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateCouponList" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelCouponList" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>订单反馈模板：</strong><br />
                    User/Shop/OrderFeedback.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateOrderFeedback" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelOrderFeedback" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>合并订单页模板：</strong><br />
                    User/Shop/MergeOrder.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateMergeOrder" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelMergeOrder" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>批发商品模板：</strong><br />
                    User/Shop/Wholesale.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateWholesale" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelWholesale" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>被投诉记录模板：</strong><br />
                    User/Crm/ComplainList.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateComplainList" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelComplainList" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>我的对账单模板：</strong><br />
                    User/Shop/Bill.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateBill" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelBill" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>汇款确认：</strong><br />
                    User/Shop/ConfirmRemittance.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateConfirmRemittance" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="Label1" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>显示厂商详细信息模板：</strong><br />
                    Shop/ShowProducer.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateShowProducer" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelShowProducer" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>显示厂商列表模板：</strong><br />
                    Shop/ShowProducerList.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateShowProducerList" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelShowProducerList" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>显示品牌详细信息模板：</strong><br />
                    Shop/ShowTrademark.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateShowTrademark" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelShowTrademark" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>显示品牌列表模板：</strong><br />
                    Shop/ShowTrademarkList.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateShowTrademarkList" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelShowTrademarkList" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>商品比较模板：</strong><br />
                    Shop/ShowCompare.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateShowCompare" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelShowCompare" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
        </tbody>
        <%--信息管理--%>
        <tbody id="Tab4" style="display: none">
            <tr class="tdbg">
                <td style="width: 30%" class="tdbgleft">
                    <strong>内容添加模板：</strong><br />
                    User/Contents/Content.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateContent" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelContent" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td style="width: 30%" class="tdbgleft">
                    <strong>内容添加引导页模板：</strong><br />
                    User/Contents/NavContent.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateNavContent" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelNavContent" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>信息管理模板：</strong><br />
                    User/Contents/ContentManage.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateContentManage" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelContentManage" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>节点树模板：</strong><br />
                    User/Contents/NodeTree.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateNodeTree" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelNodeTree" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>签收模板：</strong><br />
                    User/Contents/Signin.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateSignin" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelSignin" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>匿名投稿步骤一：</strong><br />
                    User/Contents/AnonymousContent.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateAnonymousContent" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="Label2" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>匿名投稿步骤二：</strong><br />
                    User/Contents/AnonymousContent2.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateAnonymousContent2" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="Label3" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>更多作者弹出窗口模板：</strong><br />
                    AuthorList.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateAuthorList" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelAuthorList" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>更多来源弹出窗口模板：</strong><br />
                    SourceList.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateSourceList" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelSourceList" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>更多关键字弹出窗口模板：</strong><br />
                    KeyWordList.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateKeyWordList" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelKeyWordList" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>下载服务器选择弹出窗口模板：</strong><br />
                    DownServerList.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateDownServerList" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelDownServerList" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
        </tbody>
        <%--帐户管理--%>
        <tbody id="Tab6" style="display: none">
            <tr class="tdbg">
                <td style="width: 30%" class="tdbgleft">
                    <strong>用户中心首页模板：</strong><br />
                    User/Default.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateDefault" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelDefault" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>用户中心信息管理首页模板：</strong><br />
                    User/Contents/Index.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateIndex" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelIndex" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>修改用户信息模板：</strong><br />
                    User/Info/ModifyInfo.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateModifyInfo" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelModifyInfo" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>修改用户密码模板：</strong><br />
                    User/Info/Password.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplatePassword" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelPassword" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>修改支付密码模板：</strong><br />
                    User/Info/PayPassword.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplatePayPassword" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelPayPassword" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>用户收藏夹模板：</strong><br />
                    User/Contents/Favorite.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateFavorite" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelFavorite" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>找回密码页模板：</strong><br />
                    User/GetPassword.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateGetPassword" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelGetPassword" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>注册支付密码页模板：</strong><br />
                    User/RegisterPayPassword.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateRegisterPayPassword" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="Label5" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>资金明细模板：</strong><br />
                    User/Shop/Bankroll.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateBankroll" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelBankroll" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>支付记录模板：</strong><br />
                    User/Info/PaymentLog.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplatePaymentLog" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelPaymentLog" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
        </tbody>
        <%--在线支付--%>
        <tbody id="Tab7" style="display: none">
            <tr class="tdbg">
                <td style="width: 30%" class="tdbgleft">
                    <strong>在线支付第一步模板：</strong><br />
                    PayOnline/SelectPayPlatform.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateSelectPayPlatform" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelSelectPayPlatform" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>在线支付第二步模板：</strong><br />
                    PayOnline/PayOnline.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplatePayOnline" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelPayOnline" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>在线支付第三步模板：</strong><br />
                    PayOnline/PayOnlineStep.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplatePayOnlineStep" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelPayOnlineStep" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
        </tbody>
        <%--评论/RSS/WAP--%>
        <tbody id="Tab8" style="display: none">
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>用户评论管理页模板：</strong><br />
                    User/Contents/CommentManage.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateCommentManage" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelCommentManage" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>用户评论修改模板：</strong><br />
                    User/Contents/CommentModify.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateCommentModify" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelCommentModify" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>用户评论树：</strong><br />
                    User/Contents/CommentTree.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateCommentTree" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelCommentTree" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td style="width: 30%" class="tdbgleft">
                    <strong>评论发表页：</strong><br />
                    Comment/AddComment.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateAddComment" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelAddComment" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td style="width: 30%" class="tdbgleft">
                    <strong>内容评论PK模板：</strong><br />
                    Comment/CommentPKZoneManage.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateCommentPKZoneManage" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelCommentPKZoneManage" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td style="width: 30%" class="tdbgleft">
                    <strong>内容评论引用页模板：</strong><br />
                    Comment/CommentExcerpt.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateCommentExcerpt" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelCommentExcerpt" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td style="width: 30%" class="tdbgleft">
                    <strong>RSS索引页模板：</strong><br />
                    Rss/Rss.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateRssIndex" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelRssIndex" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td style="width: 30%" class="tdbgleft">
                    <strong>WAP索引页模板：</strong><br />
                    Wap/Wap.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateWapIndex" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelWapIndex" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td style="width: 30%" class="tdbgleft">
                    <strong>WAP内容页模板：</strong><br />
                    Wap/WapItem.aspx
                </td>
                <td>
                    <pe:TemplateSelectControl ID="TemplateWapItem" Width="300px" runat="server"></pe:TemplateSelectControl>&nbsp;&nbsp;&nbsp;<asp:Label
                        ID="LabelWapItem" runat="server" ForeColor="red" Text=""></asp:Label>
                </td>
            </tr>
        </tbody>
        <tr class="tdbgbottom">
            <td colspan="2">
                <asp:Button ID="BtnSubmit" runat="server" Text="保存设置" OnClick="BtnSubmit_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
