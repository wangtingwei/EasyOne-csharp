<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.CommentUI.CommentPKZoneManage" Codebehind="CommentPKZoneManage.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>动易网络科技</title>
</head>
<body>
    <form id="form1" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" width="100%" class="border" style="background-color: White;
            height: 100%;">
            <tr>
                <td valign="top">
                    <table border="0" cellpadding="1" cellspacing="1" width="100%">
                        <tr>
                            <td align="left" colspan="2" style="height: 20px">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 120px; color: Green; font-size: 14px" align="right">
                                PK 主题：&nbsp;</td>
                            <td align="left">
                                <span style="color: #3B78AF; font-size: 20px"><strong>
                                    <pe:ExtendedLabel HtmlEncode="false" ID="LblTitle" runat="server" Text="" />
                                </strong></span>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 120px; color: #E9BD17; font-size: 14px" align="right">
                                PK 对象：&nbsp;</td>
                            <td align="left">
                                <span style="font-size: 14px">
                                    <asp:Label ID="LblUserName" runat="server" Text="Label" />
                                    <asp:Label ID="LblTime" runat="server" Text="" />
                                </span>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 120px; color: Red; font-size: 14px" align="right">
                                PK 言论：&nbsp;</td>
                            <td align="left">
                                <span style="font-size: 14px">——
                                    <asp:Label ID="LblContent" runat="server" Text="" />
                                </span>
                            </td>
                        </tr>
                        <tr>
                            <td style="color: #000000" align="center" colspan="2">
                                本评论只代表网友个人观点，不代表本站观点</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="sustainfilter" align="right">
                                <strong>支持方：<asp:Label ID="LblSustain" runat="server" Text="Label" />人</strong>
                            </td>
                            <td style="width: 10%; background-color: #D4EAF7;" align="center">
                            </td>
                            <td class="opposefilter" align="left">
                                <strong>反对方：<asp:Label ID="LblOppose" runat="server" Text="Label" />人</strong>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td style="width: 45%; background-color: #37B6ED; font-size: 14px" align="left">
                                <img alt="" src="<%=Path%>Images/Comment/pk_asq.gif" />
                            </td>
                            <td style="width: 5%; background-color: #37B6ED;">
                            </td>
                            <td style="width: 1%; background-color: #FFFFFF;">
                            </td>
                            <td style="width: 5%; background-color: #D8D8D8;">
                            </td>
                            <td style="width: 44%; background-color: #D8D8D8; font-size: 14px" align="right">
                                <img alt="" src="<%=Path%>Images/Comment/pk_bsq.gif" />
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
                                            <a href="#" onclick="javascript:publishPk(1)">
                                                <img alt="支持，我有话要说" src="<%=Path%>Images/Comment/pk_post_a.gif" style="border: 0px" /></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Repeater ID="RptAgreeNetizen" runat="server" OnItemDataBound="RptAgreeNetizen_ItemDataBound">
                                                <HeaderTemplate>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <!-- 循环赞同辩论开始-->
                                                    <table border="0" cellpadding="5" cellspacing="1" style="width: 100%; background-color: #92D7F4;">
                                                        <tr>
                                                            <td style="width: 100%; background-color: #C4E9FB;">
                                                                <asp:Label ID="LblAgreeNetizen" runat="server" Text="Label" />
                                                                辩论观点：
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 100%; background-color: #FFFFFF;">
                                                                <asp:Label ID="LblAgreeNetizenContent" runat="server" Text="Label" />
                                                                <div style="border-top: 1px dashed #1A89DA; height: 1px; overflow: hidden;">
                                                                </div>
                                                                时间：<asp:Label ID="LblAgreeNetizenTime" runat="server" Text="" />
                                                                来自：<asp:Label ID="LblAgreeNetizenIp" runat="server" Text="" />
                                                                <asp:Label ID="LblAgreeDelete" runat="server" Text="" />
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
                            <td style="width: 5%; background-color: #37B6ED;">
                            </td>
                            <td style="width: 1%; background-color: #FFFFFF;">
                            </td>
                            <td style="width: 5%; background-color: #D8D8D8;">
                            </td>
                            <!-- 反对辩论开始-->
                            <td style="width: 44%; font-size: 14px" align="left" valign="top">
                                <table border="0" cellpadding="2" cellspacing="0" width="100%">
                                    <tr>
                                        <td align="right">
                                            <a href="#" onclick="javascript:publishPk(-1)">
                                                <img alt="反对，我有话要说" src="<%=Path%>Images/Comment/pk_post_b.gif" style="border: 0px;" /></a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Repeater ID="RptOpposeNetizen" runat="server" OnItemDataBound="RptOpposeNetizen_ItemDataBound">
                                                <HeaderTemplate>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <!-- 循环反对辩论开始-->
                                                    <table border="0" cellpadding="5" cellspacing="1" style="width: 100%; background-color: #C4C4C4;">
                                                        <tr>
                                                            <td style="width: 100%; background-color: #D8D8D8;">
                                                                <asp:Label ID="LblOpposeNetizen" runat="server" Text="Label" />
                                                                辩论观点：
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 100%; background-color: #FFFFFF;">
                                                                <asp:Label ID="LblOpposeNetizenContent" runat="server" Text="Label" />
                                                                <div style="border-top: 1px dashed #1A89DA; height: 1px; overflow: hidden;">
                                                                </div>
                                                                时间：<asp:Label ID="LblOpposeNetizenTime" runat="server" Text="" />
                                                                来自：<asp:Label ID="LblOpposeNetizenIp" runat="server" Text="" />
                                                                <asp:Label ID="LblOpposeDelete" runat="server" Text="" />
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
                    <table border="0" cellpadding="2" cellspacing="2" width="100%">
                        <tr>
                            <td class="sustainfilter2" align="left">
                                <strong>支持方：<asp:Label ID="LblSustain2" runat="server" Text="Label" />人</strong>
                            </td>
                            <td class="opposefilter2" align="right">
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
                            <td align="left" style="width: 100%; background-color: #37B6ED;">
                                <img alt="" src="<%=Path%>Images/Comment/pk_csq.gif" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 100%;">
                                <a href="#" onclick="javascript:publishPk(0)">
                                    <img alt="我中立，我有话要说" src="<%=Path%>Images/Comment/pk_post_c.gif" style="border: 0px" /></a>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 100%;">
                                <asp:Repeater ID="RptNeutralismNetizen" runat="server" OnItemDataBound="RptNeutralismNetizen_ItemDataBound">
                                    <HeaderTemplate>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <!-- 循环中立辩论开始-->
                                        <table border="0" cellpadding="5" cellspacing="1" style="width: 100%; background-color: #92D7F4;">
                                            <tr>
                                                <td style="width: 100%; background-color: #C4E9FB;">
                                                    <asp:Label ID="LblNeutralismNetizen" runat="server" Text="Label" />
                                                    辩论观点：
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100%; background-color: #FFFFFF;">
                                                    <asp:Label ID="LblNeutralismNetizenContent" runat="server" Text="Label" />
                                                    <div style="border-top: 1px dashed #1A89DA; height: 1px; overflow: hidden;">
                                                    </div>
                                                    时间：<asp:Label ID="LblNeutralismNetizenTime" runat="server" Text="" />
                                                    来自：<asp:Label ID="LblNeutralismNetizenIp" runat="server" Text="" />
                                                    <asp:Label ID="LblNeutralismDelete" runat="server" Text="" />
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
        <asp:Panel ID="Panel1" runat="server" Height="150px" Width="400px" Style="display: none"
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
                    <td align="right" class="tdbgleft" style="height: 17px">
                        标题：&nbsp;
                    </td>
                    <td align="left" style="height: 17px">
                        <input type="text" name="ItemTitle" value="" id="TxtItemTitle" maxlength="5" style="width: 250px;"
                            class="inputtext" />
                    </td>
                </tr>
                <tr class="tdbg">
                    <td align="right" class="tdbgleft">
                        辩论：&nbsp;
                    </td>
                    <td align="left">
                        <textarea name="ItemContent" rows="" cols="" style="width: 300px; height: 200px"></textarea>
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
                        <asp:Button ID="OkButton" runat="server" Text=" 确定 " OnClick="OkButton_Click" />&nbsp;&nbsp;<asp:Button
                            ID="CancelButton" runat="server" Text=" 取消 " />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:HiddenField ID="HdnPKZone" runat="server" />
        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="HdnPKZone"
            PopupControlID="Panel1" CancelControlID="CancelButton" BackgroundCssClass="modalBackground"
            DropShadow="true" Drag="true" />
        <!-- 发表辩论结束-->

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

    </form>
</body>
</html>
