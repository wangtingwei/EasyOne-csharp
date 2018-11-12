<%@ Control Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Controls.ShowOrderDetail" Codebehind="ShowOrderDetail.ascx.cs" %>

<table border="0" cellpadding="2" cellspacing="1" class="border" width="100%">
    <tr align="center">
        <td class="title">
            <b>订 单 信 息</b>（订单编号：<asp:Label ID="LblOrderNum" runat="server"></asp:Label>）
        </td>
    </tr>
    <tr>
        <td style="height: 25px">
            <table border="0" cellpadding="2" cellspacing="0" width="100%">
                <tr class="tdbg">
                    <td style="width: 18%">
                        客户名称：<asp:HyperLink ID="HlkClientName" runat="server"></asp:HyperLink>
                    </td>
                    <td style="width: 20%">
                        用 户 名：<asp:HyperLink ID="HlkUserName" runat="server"></asp:HyperLink>
                    </td>
                    <td style="width: 18%">
                        代 理 商：
                        <asp:HyperLink ID="HlkAgentName" runat="server"></asp:HyperLink></td>
                    <td style="width: 18%">
                        购买日期：<asp:Label ID="LblBeginDate" runat="server"></asp:Label></td>
                    <td style="color: #000000">
                        下单时间：<asp:Label ID="LblInputTime" runat="server"></asp:Label></td>
                </tr>
                <tr class="tdbg" style="color: #000000">
                    <td>
                        需要发票：<pe:ExtendedLabel HtmlEncode="false" ID="LblNeedInvoice" runat="server"></pe:ExtendedLabel></td>
                    <td>
                        已开发票：<pe:ExtendedLabel HtmlEncode="false" ID="LblInvoiced" runat="server"></pe:ExtendedLabel></td>
                    <td>
                        订单状态：<pe:ExtendedLabel HtmlEncode="false" ID="LblStatus" runat="server"></pe:ExtendedLabel></td>
                    <td>
                        付款情况：<pe:ExtendedLabel HtmlEncode="false" ID="LblMoneyTotal" runat="server"></pe:ExtendedLabel></td>
                    <td>
                        物流状态：<pe:ExtendedLabel HtmlEncode="false" ID="LblDeliverStatus" runat="server"></pe:ExtendedLabel></td>
                </tr>
            </table>
        </td>
    </tr>
    <tr align="center">
        <td style="height: 24%">
            <table border="0" cellpadding="2" cellspacing="1" width="100%">
                <tr class="tdbg">
                    <td align="right" class="tdbgleft" style="width: 12%">
                        收货人姓名：</td>
                    <td style="width: 38%" align="left">
                        <asp:Label ID="LblContacterName" runat="server"></asp:Label></td>
                    <td align="right" class="tdbgleft" style="width: 12%">
                        联系电话：</td>
                    <td style="width: 38%" align="left">
                        <asp:Label ID="LblPhone" runat="server"></asp:Label></td>
                </tr>
                <tr class="tdbg" valign="top">
                    <td align="right" class="tdbgleft">
                        收货人地址：</td>
                    <td align="left">
                        <asp:Label ID="LblAddress" runat="server"></asp:Label></td>
                    <td align="right" class="tdbgleft">
                        邮政编码：</td>
                    <td align="left">
                        <asp:Label ID="LblZipCode" runat="server"></asp:Label></td>
                </tr>
                <tr class="tdbg">
                    <td align="right" class="tdbgleft">
                        收货人邮箱：</td>
                    <td align="left">
                        <asp:Label ID="LblEmail" runat="server"></asp:Label></td>
                    <td align="right" class="tdbgleft">
                        收货人手机：</td>
                    <td align="left">
                        <asp:Label ID="LblMobile" runat="server"></asp:Label></td>
                </tr>
                <tr class="tdbg">
                    <td align="right" class="tdbgleft" >
                        付款方式：</td>
                    <td align="left">
                        <asp:Label ID="LblPaymentType" runat="server"></asp:Label></td>
                    <td align="right" class="tdbgleft">
                        送货方式：</td>
                    <td align="left">
                        <asp:Label ID="LblDeliverType" runat="server"></asp:Label></td>
                </tr>
                <tr class="tdbg" valign="top">
                    <td align="right" class="tdbgleft">
                        发票信息：</td>
                    <td align="left">
                        <asp:Label ID="LblInvoiceContent" runat="server"></asp:Label></td>
                    <td align="right" class="tdbgleft">
                        备注/留言：</td>
                    <td align="left">
                        <asp:Label ID="LblRemark" runat="server"></asp:Label></td>
                </tr>
                <tr class="tdbg" valign="top">
                    <td align="right" class="tdbgleft">
                        缺货处理：</td>
                    <td align="left">
                        <asp:Label ID="LblOutOfStockProject" runat="server"></asp:Label>
                    </td>
                    <td align="right" class="tdbgleft">
                         <asp:Literal ID="LtrMemoTitle" Text="内部记录：" runat="server" Visible="false"></asp:Literal>
                     </td>
                    <td align="left">
                        <asp:Label ID="LblMemo" runat="server" Visible="false"></asp:Label>
                    </td>
                </tr>
                <tr class="tdbg" runat="server" id="ShowFunctionary" visible="false">
                    <td align="right" class="tdbgleft">
                        订单类型：
                        </td>
                    <td align="left">
                        <asp:Label ID="LblOrderType" runat="server"></asp:Label>
                        </td>
                    <td align="right" class="tdbgleft" >
                        跟单员：</td>
                    <td align="left">
                        <asp:Label ID="LblFunctionary" runat="server"></asp:Label></td>
                </tr>
                <tr class="tdbg">
                   <td align="right" class="tdbgleft">
                        要求送货时间：</td>
                    <td align="left" colspan="3">
                        <asp:Label ID="LblDeliverTime" runat="server"></asp:Label>
                    </td>
             
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Repeater ID="RptOrderItem" runat="server" OnItemDataBound="RptOrderItem_ItemDataBound">
                <HeaderTemplate>
                    <table border='0' cellpadding='0' cellspacing='1' width='100%'>
                        <tr align='center' class='tdbgleft'>
                            <td id="ProductImageTitle" runat="server">
                                图片
                            </td>
                            <td>
                                商 品 名 称</td>
                            <td style="width: 5%;">
                                单位</td>
                            <td style="width: 6%;">
                                数量</td>
                            <td style="width: 8%;">
                                市场价</td>
                            <td style="width: 8%;">
                                实价</td>
                            <td style="width: 8%;">
                                指定价</td>
                            <td style="width: 8%;">
                                金额</td>
                            <td style="width: 8%;">
                                服务期限</td>
                            <td style="width: 10%;">
                                备注</td>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr align='center' class='tdbg'>
                        <td id="ProductImage" runat="server">
                             <pe:ExtendedImage id="extendedImage" runat="server"></pe:ExtendedImage>
                        </td>
                        <td align="left">
                            <asp:HyperLink ID="LnkProduct" Text='<%#GetProductName(Convert.ToString(Eval("ProductName")),(Eval("Property")==null?"":Eval("Property").ToString()),Convert.ToInt32(Eval("SaleType"))) %>' runat="server"></asp:HyperLink>
                        </td>
                        <td>
                            <asp:Literal ID="LtrUnit" runat="server" Text='<%#Eval("Unit")%>'></asp:Literal>
                        </td>
                        <td>
                            <asp:Literal ID="LtrAmount" runat="server" Text='<%#Eval("Amount") %>'></asp:Literal>
                        </td>
                        <td align="right">
                            <asp:Literal ID="LtrPrice_Original" runat="server" Text='<%#Decimal.Round(Convert.ToDecimal(Eval("PriceMarket")),2) %>'></asp:Literal>
                        </td>
                        <td align="right">
                            <asp:Literal ID="LtrPrice" runat="server" Text='<%#Decimal.Round(Convert.ToDecimal(Eval("Price")),2) %>'></asp:Literal>
                        </td>
                        <td align="right">
                            <asp:Literal ID="LtrTruePrice" runat="server" Text='<%#Decimal.Round(Convert.ToDecimal(Eval("TruePrice")),2) %>'></asp:Literal>
                        </td>
                        <td align="right">
                            <asp:Literal ID="LtrTotalMoney" runat="server" Text='<%#Decimal.Round(Convert.ToDecimal((int)Eval("Amount")*(decimal)Eval("TruePrice")),2) %>'></asp:Literal>
                        </td>
                        <td>
                            <pe:ExtendedLiteral HtmlEncode="false" ID="LtrServiceTerm" runat="server"></pe:ExtendedLiteral>
                        </td>
                        <td>
                            <asp:Label ID="LblItemRemark" runat="server"></asp:Label>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    <tr class='tdbg'>
                        <td align="right" colspan='7'>
                            合计：
                        </td>
                        <td align="right">
                            <%=m_SubTotal.ToString("N2")%>
                        </td>
                        <td colspan='2'>
                        </td>
                    </tr>
                    <tr class='tdbg'>
                        <td align='left' colspan='5'>
                            <%=m_SumInfo%>
                        </td>
                        <td align='right' colspan='2'>
                            实际金额：
                        </td>
                        <td align="right">
                            <%=m_TotalMoney.ToString("N2")%>
                        </td>
                        <td align='left' colspan='2'>
                            已付款：<%=m_MoneyReceipt%>
                        </td>
                    </tr>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </td>
    </tr>
</table>
