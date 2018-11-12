<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Controls.ShoppingCartUI" Codebehind="ShoppingCart.ascx.cs" %>
<asp:Repeater ID="RptShoppingCart" runat="server" OnItemDataBound="RptShoppingCart_ItemDataBound">
    <HeaderTemplate>
        <table class="border" cellspacing="1" width="100%">
            <tr class="title" align="center">
             <td id="ProductImageTitle" runat="server">
                  ͼƬ
                </td>
                <td style="width: 10%">
                    ��Ʒ����</td>
                <td style="width: 8%"> 
                    ��λ</td>
                <td style="width: 13%">
                    ����</td>
                 <td style="width: 10%" id="tdProductTypeTitle" runat="server">
                    ��Ʒ���</td>
                 <td style="width: 10%" id="tdSaleTypeTitle" runat="server">
                    ��������</td>
                 <td style="width: 12%" id="tdMarkPriceTitle" runat="server">
                    �г���</td>
                <td style="width: 12%">
                    ʵ��</td>
                <td style="width: 12%">
                    ���</td>
            </tr>
    </HeaderTemplate>
    <ItemTemplate>
        <tr class="tdbg" align="center">
            <asp:HiddenField ID="HdfProdctId" runat="server" Value='<%#Eval("ProductId") %>' />
            <asp:HiddenField ID="HdfTableName" runat="server" Value='<%#Eval("TableName") %>' />
            <td id="ProductImage" runat="server">
                            <pe:ExtendedImage id="extendedImage" runat="server"></pe:ExtendedImage>
            </td>
            <td align="left">
                 <pe:ExtendedLiteral ID="LitProductName" HtmlEncode="false" runat="server"></pe:ExtendedLiteral>
                <asp:Literal ID="LitProperty" runat="server"></asp:Literal>
                </td>
                 
            <td>
                <asp:Literal ID="LitProductUnit" runat="server"></asp:Literal></td>
            <td>
                <asp:Literal ID="LitProductAmount" runat="server" Text='<%#Eval("Quantity") %>'></asp:Literal></td>
            <td id="tdProductType" runat="server">
                <asp:Literal ID="LitProductType" runat="server"></asp:Literal></td>
            <td id="tdSaleType" runat="server">
                <asp:Literal ID="LitSaleType" runat="server"></asp:Literal></td>
            <td align="right" id="tdMarkPrice" runat="server">
                <asp:Literal ID="LitPriceMarket" runat="server"></asp:Literal></td>
            <td align="right">
                <asp:Literal ID="LitTruePrice" runat="server"></asp:Literal></td>
            <td align="right">
                <asp:Literal ID="LitSubTotal" runat="server"></asp:Literal></td>
        </tr>
        <asp:Panel ID="PresentInfomation" runat="server" Visible="false">
            <tr class="tdbg" align="center">
                <asp:HiddenField ID="HdnPresentId" runat="server" />
                <td id="presentImage" runat="server">
                    <pe:ExtendedImage id="extendedPresentImage" runat="server"></pe:ExtendedImage>
                </td>
                <td align="left">
                    <pe:ExtendedLiteral ID="LitPresentName" runat="server"></pe:ExtendedLiteral></td>
                <td>
                    <asp:Literal ID="LitPresentUnit" runat="server"></asp:Literal></td>
                <td>
                    <asp:Literal ID="LitPresentNum" runat="server"></asp:Literal></td>
                <td id="tdPresentType" runat="server">
                    <asp:Literal ID="LitPresentType" runat="server"></asp:Literal></td>
                <td id="tdPresentSaleType" runat="server">
                    <asp:Literal ID="LitPresentSaleType" runat="server"></asp:Literal></td>
                <td align="right" id="tdPresentMarkPrice" runat="server">
                    <asp:Literal ID="LitPresentPriceOriginal" runat="server"></asp:Literal></td>
                <td align="right">
                    <asp:Literal ID="LitPresentTruePrice" runat="server"></asp:Literal></td>
                <td align="right">
                    <asp:Literal ID="LitPresentSubtotal" runat="server"></asp:Literal></td>
            </tr>
        </asp:Panel>
    </ItemTemplate>
    <FooterTemplate>
       
         <asp:PlaceHolder ID="PlhPresentInfo" runat="server" Visible="false">
            <tr align='center' class='tdbg'>
                <td id="footerPresentImage" runat="server">
                  <pe:ExtendedImage id="presentImage" runat="server"></pe:ExtendedImage>
                </td>
                <td align='left'>
                    <pe:ExtendedLabel ID="LblProductName" runat="server"/><span style="color:Red">����ֵ������</span>
                </td>
                <td><asp:Label ID="LblUnit" runat="server"></asp:Label></td>
                <td>1</td>
                <td id="footerPresentType" runat="server"><span style="color:Red">������Ʒ</span></td>
                <td id="footerPresentSaleType" runat="server"><span style="color:Red">��ֵ����</span></td>
                <td align='right' id="footerPresentMarkPrice" runat="server"><asp:Label ID="LblPresentPriceMarket" runat="server"></asp:Label></td>
                <td align='right' id="footerPresentTruePrice" runat="server"><asp:Label ID="LblPresentPrice" runat="server"></asp:Label></td>
                <td align='right' id="footerPresentSalePrice" runat="server"><asp:Label ID="LblPresentPrice1" runat="server"></asp:Label></td>
            </tr>
         </asp:PlaceHolder>
        <%--<%=presentList2 %>--%>
        <tr class="tdbg">
            <td runat="server" id="footerTdThemeImage"></td>
            <td runat="server" id="footerTdProductType"></td>
            <td runat="server" id="footerTdSaleType"></td>
            <td runat="server" id="footerTdMarkPrice"></td>
            <td colspan="4" align="right">
                <b>�ϼƣ�</b></td>
            <td align="right"><%=string.Format("{0:N2}",total)%></td>
        </tr>
        <asp:PlaceHolder ID="PlhMoneyInfo" runat="server" Visible="false" >
            <tr> 
                 <td align='left' colspan="2"> <span style="color:green">�˷�</span>�� <asp:Label ID="LblDeliverCharge" runat="server"></asp:Label>Ԫ
                     <span style="color:green">�˷�˰��</span>��<asp:Label ID="LblTaxRate" runat="server"></asp:Label>%
                     <span style="color:green">�˷Ѽ۸�˰</span>��<asp:Label ID="LblIncludeTax" runat="server"></asp:Label><br />
                     <pe:ExtendedLabel HtmlEncode="false" ID="LblCoupon" runat="server" Visible="false"></pe:ExtendedLabel>
                     <asp:Label ID="LblTotalMoney" runat="server"></asp:Label>
                </td>
                <td runat="server" id="footerTdMoneyInfoProductType" runat="server"></td>
                <td runat="server" id="footerTdMoneyInfoSaleType" runat="server"></td>
                <td runat="server" id="footerTdMoneyInfoMarkPrice" runat="server"></td>
                <td></td>
                <td align='right' colspan="3"><b>ʵ�ʽ�</b><asp:Label ID="LblTrueTotalMoney" runat="server"></asp:Label>Ԫ</td>
            </tr>      
        </asp:PlaceHolder> 
<%--        <%= priceInfomation%>--%>
        <%=presentExpInfomation%>
        <%--            <asp:Panel ID="Notes" runat="server" Visible="false">
             <tr>
                    <td colspan="8" align="right"><%=presentList%></td>
                    <td></td>
                </tr>
                <tr>
                    <td colspan="9" align="left"><b> <%= priceInfomation%></b></td>
                </tr>
             </asp:Panel>--%>
        </table>
    </FooterTemplate>
</asp:Repeater>
<div id="Note" runat="server" style="text-align: left" visible="false">
    <b>�������
        <asp:Label ID="LblPrice" runat="server" Visible="false" ForeColor="red"></asp:Label>
        Ԫ��ֵ����������Ʒ�е���һ� </b>
</div>
<asp:Repeater ID="RptPresentList" runat="server" Visible="false" OnItemDataBound="RptPresentList_ItemDataBound">
    <HeaderTemplate>
        <table width="100%" class="border" cellpadding="2" cellspacing="1">
            <tr class="title">
                <td style="width:3px;"></td>
                <td id="changePresentHeaderImage" runat="server">
                    ͼƬ
               </td>
                <td>
                    ��Ʒ����</td>
                <td>
                    ��λ</td>
                <td>
                    ����</td>
                <td runat="server" id="changePresentHeaderProductType" runat="server">
                    ��Ʒ���</td>
                <td runat="server" id="changePresentHeaderSaleType" runat="server">
                    ��������</td>
                <td runat="server" id="changePresentHeaderMarkPrice" runat="server">
                    �г���</td>
                <td>
                    ʵ��</td>
                <td>
                    ���</td>
            </tr>
    </HeaderTemplate>
    <ItemTemplate>
        <tr>
             <td style="width:5px;"><input type="radio" value='<%#Eval("PresentId").ToString()%>' name="RdbPresentId" id="RdbPresentId"></td>
             <td id="changePresentImage" runat="server" align="center">
                <pe:ExtendedImage id="changePresentListImage" runat="server"></pe:ExtendedImage>
            </td>
             <td style="text-align:center;">
                <%--                <asp:HiddenField ID="HdfChangePresentId" runat="server" Value='<%#Eval("ProductId") %>' />
                <asp:HiddenField ID="HdfChangePresentTableName" runat="server" Value='<%#Eval("TableName") %>' />--%>
                <asp:Literal ID="LitChangePresentName" runat="server" Text='<%#Eval("PresentName") %>'></asp:Literal></td>
            <td align="center">
                <asp:Literal ID="LitChangePresentUnit" runat="server" Text='<%#Eval("Unit") %>'></asp:Literal></td>
            <td align="center">
                <asp:Literal ID="LitChangePresentAmount" runat="server" Text='1'></asp:Literal></td>
            <td runat="server" id="changePresentType" runat="server" align="center">
                <asp:Literal ID="LitChangePresentType" runat="server" Text='������Ʒ'></asp:Literal></td>
            <td runat="server" id="changeSaleType" runat="server" align="center">
                <asp:Literal ID="LitChangePresentSaleType" runat="server" Text='��ֵ����'></asp:Literal></td>
            <td runat="server" id="changeMarkPrice" runat="server" align="center">
                <asp:Literal ID="LitChangePresentPriceMarket" runat="server"></asp:Literal></td>
            <td align="center">
                <asp:Literal ID="LitChangePresentTruePrice" runat="server" ></asp:Literal></td>
            <td align="center">
                <asp:Literal ID="LitChangePresentSubTotal" runat="server" ></asp:Literal></td>
        </tr>
    </ItemTemplate>
    <FooterTemplate>
        </table>
    </FooterTemplate>
</asp:Repeater>
