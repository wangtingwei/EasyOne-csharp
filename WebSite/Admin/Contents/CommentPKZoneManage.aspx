<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Contents.CommentPKZoneManage" Codebehind="CommentPKZoneManage.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>动易网络科技</title>
</head>
<body>
    <form id="form1" runat="server">
        <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
        <br />
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td valign="top" valign="middle">
                    <table border="0" cellpadding="1" cellspacing="1" width="100%" class="border">
                        <tr>
                            <td class="title" align="center" colspan="2">
                                本评论只代表网友个人观点，不代表本站观点</td>
                        </tr>
                        <tr>
                            <td align="right" class="tdbgleft">
                                PK 主题：&nbsp;</td>
                            <td align="left" class="tdbg">
                                <span><strong>
                                    <asp:Label ID="LblTitle" runat="server" Text="Label" />
                                </strong></span>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdbgleft" align="right">
                                PK 对象：&nbsp;</td>
                            <td align="left" class="tdbg">
                                <span style="font-size: 14px">
                                    <asp:Label ID="LblUserName" runat="server" Text="Label" />
                                    <asp:Label ID="LblTime" runat="server" Text="Label" />
                                </span>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdbgleft" align="right">
                                PK 言论：&nbsp;</td>
                            <td align="left" class="tdbg">
                                <span style="font-size: 14px">——
                                    <asp:Label ID="LblContent" runat="server" Text="Label" />
                                </span>
                            </td>
                        </tr>
                    </table>
                    <br />
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr class="title">
                            <td align="right">
                                <strong>支持方：<asp:Label ID="LblSustain" runat="server" Text="Label" />人</strong>
                            </td>
                            <td align="center">
                            </td>
                            <td align="left">
                                <strong>反对方：<asp:Label ID="LblOppose" runat="server" Text="Label" />人</strong>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <!-- 主辩论开始-->
            <tr>
                <td valign="top">
                    <table border="0" cellpadding="2" cellspacing="0" width="100%">
                        <tr>
                            <!-- 赞同辩论开始-->
                            <td style="width: 45%; font-size: 14px" align="left" valign="top">
                                <table border="0" cellpadding="2" cellspacing="0" width="100%">
                                    <tr>
                                        <td align="right">
                                            <input type="button" class="inputbutton" value=" 支持，我有话要说 " onclick="javascript:publishPk(1)" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Repeater ID="RptAgreeNetizen" runat="server" OnItemDataBound="RptAgreeNetizen_ItemDataBound">
                                                <HeaderTemplate>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <!-- 循环赞同辩论开始-->
                                                    <table border="0" cellpadding="5" cellspacing="1" style="width: 100%;" class="border">
                                                        <tr>
                                                            <td style="width: 100%;" class="title">
                                                                <asp:Label ID="LblAgreeNetizen" runat="server" Text="Label" />
                                                                发表评论：
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 100%;" class="tdbg">
                                                                <asp:Label ID="LblAgreeNetizenContent" runat="server" Text="Label" />
                                                                <div class="commontbrokenline">
                                                                </div>
                                                                时间：<asp:Label ID="LblAgreeNetizenTime" runat="server" Text="" />
                                                                来自：<asp:Label ID="LblAgreeNetizenIp" runat="server" Text="" />
                                                                <pe:ExtendedLabel HtmlEncode="false" ID="LblAgreeDelete" runat="server" Text="" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <br />
                                                    <!-- 循环赞同辩论结束-->
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <pe:AspNetPager ID="AgreePager" runat="server" OnPageChanged="AgreePager_PageChanged">
                                            </pe:AspNetPager>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <!-- 赞同辩论结束-->
                            <td style="width: 3%;">
                            </td>
                            <!-- 反对辩论开始-->
                            <td style="width: 44%; font-size: 14px" align="left" valign="top">
                                <table border="0" cellpadding="2" cellspacing="0" width="100%">
                                    <tr>
                                        <td align="right">
                                            <input type="button" class="inputbutton" value=" 反对，我有话要说 " onclick="javascript:publishPk(-1)" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Repeater ID="RptOpposeNetizen" runat="server" OnItemDataBound="RptOpposeNetizen_ItemDataBound">
                                                <HeaderTemplate>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <!-- 循环反对辩论开始-->
                                                    <table border="0" cellpadding="5" cellspacing="1" style="width: 100%;" class="border">
                                                        <tr>
                                                            <td style="width: 100%;" class="title">
                                                                <asp:Label ID="LblOpposeNetizen" runat="server" Text="Label" />
                                                                发表评论：
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 100%;" class="tdbg">
                                                                <asp:Label ID="LblOpposeNetizenContent" runat="server" Text="Label" />
                                                                <div class="commontbrokenline">
                                                                </div>
                                                                时间：<asp:Label ID="LblOpposeNetizenTime" runat="server" Text="" />
                                                                来自：<asp:Label ID="LblOpposeNetizenIp" runat="server" Text="" />
                                                                <pe:ExtendedLabel HtmlEncode="false" ID="LblOpposeDelete" runat="server" Text="" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <br />
                                                    <!-- 循环反对辩论结束-->
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <pe:AspNetPager ID="OpposePager" runat="server" OnPageChanged="OpposePager_PageChanged">
                                            </pe:AspNetPager>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <!-- 反对辩论结束-->
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr class="title">
                            <td align="left">
                                <strong>支持方：<asp:Label ID="LblSustain2" runat="server" Text="Label" />人</strong>
                            </td>
                            <td align="right">
                                <strong>反对方：<asp:Label ID="LblOppose2" runat="server" Text="Label" />人</strong>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <!-- 中立辩论开始-->
            <tr>
                <td valign="top">
                    <table border="0" cellpadding="2" cellspacing="2" width="100%">
                        <tr>
                            <td align="right" style="width: 100%;">
                                <input type="button" class="inputbutton" value=" 我中立，我有话要说 " onclick="javascript:publishPk(0)" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%;">
                                <asp:Repeater ID="RptNeutralismNetizen" runat="server" OnItemDataBound="RptNeutralismNetizen_ItemDataBound">
                                    <HeaderTemplate>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <!-- 循环中立辩论开始-->
                                        <table border="0" cellpadding="5" cellspacing="1" style="width: 100%;" class="border">
                                            <tr>
                                                <td style="width: 100%;" class="title">
                                                    <asp:Label ID="LblNeutralismNetizen" runat="server" Text="Label" />
                                                    发表评论：
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100%;" class="tdbg">
                                                    <asp:Label ID="LblNeutralismNetizenContent" runat="server" Text="Label" />
                                                    <div class="commontbrokenline">
                                                    </div>
                                                    时间：<asp:Label ID="LblNeutralismNetizenTime" runat="server" Text="" />
                                                    来自：<asp:Label ID="LblNeutralismNetizenIp" runat="server" Text="" />
                                                    <pe:ExtendedLabel HtmlEncode="false" ID="LblNeutralismDelete" runat="server" Text="" />
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                        <!-- 循环中立辩论结束-->
                                    </ItemTemplate>
                                    <FooterTemplate>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="neutralismfilter" align="right">
                    <strong>中立方：<asp:Label ID="LblNeutralismNetizen" runat="server" Text="Label" />人</strong>&nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td align="center">
                    <pe:AspNetPager ID="NeutralismPager" runat="server" OnPageChanged="NeutralismPager_PageChanged">
                    </pe:AspNetPager>
                </td>
            </tr>
            <!-- 中立辩论结束-->
            <!-- 主辩论结束-->
        </table>
        <!-- 发表辩论开始-->
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <asp:Panel ID="Panel1" runat="server" Height="295px" Width="400px" Style="display: none"
            CssClass="modalPopup">
            <table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="border">
                <tr>
                    <td class="title" align="center" colspan="2">
                        <b>
                            <asp:Label ID="LblItemTitle" runat="server" Text="Label" /></b>
                    </td>
                </tr>
                <tr class="tdbg">
                    <td align="right" style="width: 120px;" class="tdbgleft">
                        您的立场：&nbsp;
                    </td>
                    <td>
                        <input type="radio" name="RadlPosition" value="1" />
                        支持
                        <input type="radio" name="RadlPosition" value="-1" />
                        反对
                        <input type="radio" name="RadlPosition" value="0" checked="checked" />
                        中立
                    </td>
                </tr>
                <tr class="tdbg">
                    <td align="right" class="tdbgleft">
                        引用：&nbsp;
                    </td>
                    <td>
                        <asp:Label ID="LblItemContent" runat="server" Text="Label" />
                    </td>
                </tr>
                <tr class="tdbg">
                    <td align="right" class="tdbgleft">
                        评论标题：&nbsp;
                    </td>
                    <td>
                        <input type="text" name="ItemTitle" value="" style="width: 250px;" />
                    </td>
                </tr>
                <tr class="tdbg">
                    <td align="right" class="tdbgleft">
                        评论：&nbsp;
                    </td>
                    <td align="left">
                        <textarea name="ItemContent" rows='' cols='' style="width: 300px; height: 150px"></textarea>
                    </td>
                </tr>
                <tr class="tdbg">
                    <td align="right" class="tdbgleft">
                        当前：&nbsp;
                    </td>
                    <td align="left">
                        <asp:Label ID="LblState" runat="server" Text="匿名发表" />
                    </td>
                </tr>
                <tr class="tdbg">
                    <td align="center" colspan="2">
                        <asp:Button ID="OkButton" runat="server" Text="确定" OnClick="OkButton_Click" />&nbsp;&nbsp;<asp:Button
                            ID="CancelButton" runat="server" Text="取消" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:HiddenField ID="HdnPKZone" runat="server" />
        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="HdnPKZone"
            PopupControlID="Panel1" CancelControlID="CancelButton" BackgroundCssClass="modalBackground"
            DropShadow="true" Drag="true" />
        <!-- 发表辩论结束-->
    </form>

    <script language="javascript" type="text/javascript">
    function publishPk(type)
    {
        switch(type){
        case 1:
            document.form1.RadlPosition[0].checked = true;
            break;
        case -1:
            document.form1.RadlPosition[1].checked = true;
            break;
        case 0:
            document.form1.RadlPosition[2].checked = true;
            break;
        }
        var popup = $find('ModalPopupExtender1');	
        popup.show();
    }
    </script>

</body>
</html>
