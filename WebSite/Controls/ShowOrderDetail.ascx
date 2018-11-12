<%@ Control Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Controls.ShowOrderDetail" Codebehind="ShowOrderDetail.ascx.cs" %>

<table border="0" cellpadding="2" cellspacing="1" class="border" width="100%">
    <tr align="center">
        <td class="title">
            <b>�� �� �� Ϣ</b>��������ţ�<asp:Label ID="LblOrderNum" runat="server"></asp:Label>��
        </td>
    </tr>
    <tr>
        <td style="height: 25px">
            <table border="0" cellpadding="2" cellspacing="0" width="100%">
                <tr class="tdbg">
                    <td style="width: 18%">
                        �ͻ����ƣ�<asp:HyperLink ID="HlkClientName" runat="server"></asp:HyperLink>
                    </td>
                    <td style="width: 20%">
                        �� �� ����<asp:HyperLink ID="HlkUserName" runat="server"></asp:HyperLink>
                    </td>
                    <td style="width: 18%">
                        �� �� �̣�
                        <asp:HyperLink ID="HlkAgentName" runat="server"></asp:HyperLink></td>
                    <td style="width: 18%">
                        �������ڣ�<asp:Label ID="LblBeginDate" runat="server"></asp:Label></td>
                    <td style="color: #000000">
                        �µ�ʱ�䣺<asp:Label ID="LblInputTime" runat="server"></asp:Label></td>
                </tr>
                <tr class="tdbg" style="color: #000000">
                    <td>
                        ��Ҫ��Ʊ��<pe:ExtendedLabel HtmlEncode="false" ID="LblNeedInvoice" runat="server"></pe:ExtendedLabel></td>
                    <td>
                        �ѿ���Ʊ��<pe:ExtendedLabel HtmlEncode="false" ID="LblInvoiced" runat="server"></pe:ExtendedLabel></td>
                    <td>
                        ����״̬��<pe:ExtendedLabel HtmlEncode="false" ID="LblStatus" runat="server"></pe:ExtendedLabel></td>
                    <td>
                        ���������<pe:ExtendedLabel HtmlEncode="false" ID="LblMoneyTotal" runat="server"></pe:ExtendedLabel></td>
                    <td>
                        ����״̬��<pe:ExtendedLabel HtmlEncode="false" ID="LblDeliverStatus" runat="server"></pe:ExtendedLabel></td>
                </tr>
            </table>
        </td>
    </tr>
    <tr align="center">
        <td style="height: 24%">
            <table border="0" cellpadding="2" cellspacing="1" width="100%">
                <tr class="tdbg">
                    <td align="right" class="tdbgleft" style="width: 12%">
                        �ջ���������</td>
                    <td style="width: 38%" align="left">
                        <asp:Label ID="LblContacterName" runat="server"></asp:Label></td>
                    <td align="right" class="tdbgleft" style="width: 12%">
                        ��ϵ�绰��</td>
                    <td style="width: 38%" align="left">
                        <asp:Label ID="LblPhone" runat="server"></asp:Label></td>
                </tr>
                <tr class="tdbg" valign="top">
                    <td align="right" class="tdbgleft">
                        �ջ��˵�ַ��</td>
                    <td align="left">
                        <asp:Label ID="LblAddress" runat="server"></asp:Label></td>
                    <td align="right" class="tdbgleft">
                        �������룺</td>
                    <td align="left">
                        <asp:Label ID="LblZipCode" runat="server"></asp:Label></td>
                </tr>
                <tr class="tdbg">
                    <td align="right" class="tdbgleft">
                        �ջ������䣺</td>
                    <td align="left">
                        <asp:Label ID="LblEmail" runat="server"></asp:Label></td>
                    <td align="right" class="tdbgleft">
                        �ջ����ֻ���</td>
                    <td align="left">
                        <asp:Label ID="LblMobile" runat="server"></asp:Label></td>
                </tr>
                <tr class="tdbg">
                    <td align="right" class="tdbgleft" >
                        ���ʽ��</td>
                    <td align="left">
                        <asp:Label ID="LblPaymentType" runat="server"></asp:Label></td>
                    <td align="right" class="tdbgleft">
                        �ͻ���ʽ��</td>
                    <td align="left">
                        <asp:Label ID="LblDeliverType" runat="server"></asp:Label></td>
                </tr>
                <tr class="tdbg" valign="top">
                    <td align="right" class="tdbgleft">
                        ��Ʊ��Ϣ��</td>
                    <td align="left">
                        <asp:Label ID="LblInvoiceContent" runat="server"></asp:Label></td>
                    <td align="right" class="tdbgleft">
                        ��ע/���ԣ�</td>
                    <td align="left">
                        <asp:Label ID="LblRemark" runat="server"></asp:Label></td>
                </tr>
                <tr class="tdbg" valign="top">
                    <td align="right" class="tdbgleft">
                        ȱ������</td>
                    <td align="left">
                        <asp:Label ID="LblOutOfStockProject" runat="server"></asp:Label>
                    </td>
                    <td align="right" class="tdbgleft">
                         <asp:Literal ID="LtrMemoTitle" Text="�ڲ���¼��" runat="server" Visible="false"></asp:Literal>
                     </td>
                    <td align="left">
                        <asp:Label ID="LblMemo" runat="server" Visible="false"></asp:Label>
                    </td>
                </tr>
                <tr class="tdbg" runat="server" id="ShowFunctionary" visible="false">
                    <td align="right" class="tdbgleft">
                        �������ͣ�
                        </td>
                    <td align="left">
                        <asp:Label ID="LblOrderType" runat="server"></asp:Label>
                        </td>
                    <td align="right" class="tdbgleft" >
                        ����Ա��</td>
                    <td align="left">
                        <asp:Label ID="LblFunctionary" runat="server"></asp:Label></td>
                </tr>
                <tr class="tdbg">
                   <td align="right" class="tdbgleft">
                        Ҫ���ͻ�ʱ�䣺</td>
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
                                ͼƬ
                            </td>
                            <td>
                                �� Ʒ �� ��</td>
                            <td style="width: 5%;">
                                ��λ</td>
                            <td style="width: 6%;">
                                ����</td>
                            <td style="width: 8%;">
                                �г���</td>
                            <td style="width: 8%;">
                                ʵ��</td>
                            <td style="width: 8%;">
                                ָ����</td>
                            <td style="width: 8%;">
                                ���</td>
                            <td style="width: 8%;">
                                ��������</td>
                            <td style="width: 10%;">
                                ��ע</td>
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
                            �ϼƣ�
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
                            ʵ�ʽ�
                        </td>
                        <td align="right">
                            <%=m_TotalMoney.ToString("N2")%>
                        </td>
                        <td align='left' colspan='2'>
                            �Ѹ��<%=m_MoneyReceipt%>
                        </td>
                    </tr>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </td>
    </tr>
</table>
