<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Controls.ProductView" Codebehind="ProductView.ascx.cs" %>
<table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="border">
    <tr class="tdbg">
        <td class="tdbgleft" style="width: 15%;" align="right">
            <strong>��Ʒ���</strong></td>
        <td class="tdbg" align="left">
            <asp:Label ID="LblProductKind" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr class="tdbg">
        <td class="tdbgleft" style="width: 15%;" align="right">
            <strong>��Ʒ���ʣ�</strong></td>
        <td class="tdbg" align="left">
            <asp:Label ID="LblCharacter" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr class="tdbg">
        <td class="tdbgleft" style="width: 15%;" align="right">
            <strong>��Ʒ��λ��</strong></td>
        <td class="tdbg" align="left">
            <asp:Label ID="LblUnit" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr class="tdbg">
        <td class="tdbgleft" style="width: 15%;" align="right">
            <strong>��Ʒ���ԣ�</strong></td>
        <td class="tdbg" align="left">
            <asp:Label ID="LblProperties" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr class="tdbg">
        <td class="tdbgleft" style="width: 15%;" align="right">
            <strong>��汨��������</strong></td>
        <td class="tdbg" align="left">
            <asp:Label ID="LblStocksProject" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr class="tdbg">
        <td class="tdbgleft" style="width: 15%;" align="right">
            <strong>��棺</strong></td>
        <td class="tdbg" align="left">
            <pe:ExtendedGridView ID="EgvStocks" runat="server" AllowPaging="False" AutoGenerateColumns="False"
                AutoGenerateCheckBoxColumn="False" DataKeyNames="Id" ItemName="����" ItemUnit="��">
                <Columns>
                    <pe:BoundField DataField="PropertyValue" NullDisplayText="��һ����Ʒ" HeaderText="������Ʒ"
                        ReadOnly="True" />
                    <pe:BoundField DataField="Stocks" HeaderText="�������" ReadOnly="True" />
                    <pe:BoundField DataField="AlarmNum" HeaderText="��汨������" ReadOnly="True" />
                    <pe:BoundField DataField="BuyTimes" HeaderText="�������" ReadOnly="True" />
                </Columns>
            </pe:ExtendedGridView>
        </td>
    </tr>
    <tr class="tdbg">
        <td class="tdbgleft" style="width: 15%;" align="right">
            <strong>˰�����ã�</strong></td>
        <td class="tdbg" align="left">
            <asp:Label ID="LblIncludeTax" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr class="tdbg">
        <td class="tdbgleft" style="width: 15%;" align="right">
            <strong>��Ʒ˰�ʣ�</strong></td>
        <td class="tdbg" align="left">
            <asp:Label ID="LblTaxRate" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr class="tdbg">
        <td class="tdbgleft" style="width: 15%;" align="right">
            <strong>������</strong></td>
        <td class="tdbg" align="left">
            <asp:Label ID="LblWeight" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr class="tdbg">
        <td class="tdbgleft" style="width: 15%;" align="right">
            <strong>�������ޣ�</strong></td>
        <td class="tdbg" align="left">
            <asp:Label ID="LblServiceTerm" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr class="tdbg">
        <td class="tdbgleft" style="width: 15%;" align="right">
            <strong>��Ʒ���ͣ�</strong></td>
        <td class="tdbg" align="left">
            <asp:Label ID="LblProductType" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr class="tdbg">
        <td class="tdbgleft" style="width: 15%;" align="right">
            <strong>��Ʒ�۸�</strong></td>
        <td class="tdbg" align="left">
            <%--ԭʼ���ۼۣ�<asp:Label ID="LblPrice_Original" runat="server" Text=""></asp:Label>--%>
            ��ǰ���ۼۣ�<asp:Label ID="LblPrice" runat="server" Text=""></asp:Label>
            �г��ο��ۣ�<asp:Label ID="LblPrice_Market" runat="server" Text=""></asp:Label>
            ��Ա�۸�<asp:Label ID="LblPrice_Member" runat="server" Text=""></asp:Label>
            �����̼۸�<asp:Label ID="LblPrice_Agent" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr class="tdbg">
        <td class="tdbgleft" style="width: 15%;" align="right">
            <strong>�� �� �̣�</strong></td>
        <td class="tdbg" align="left">
            <asp:Label ID="LblProducer" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr class="tdbg">
        <td class="tdbgleft" style="width: 15%;" align="right">
            <strong>Ʒ��/�̱꣺</strong></td>
        <td class="tdbg" align="left">
            <asp:Label ID="LblTrademark" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr class="tdbg">
        <td class="tdbgleft" style="width: 15%;" align="right">
            <strong>���ص�ַ��</strong></td>
        <td class="tdbg" align="left">
            <asp:Label ID="LblDownloadUrl" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr class="tdbg">
        <td class="tdbgleft" style="width: 15%;" align="right">
            <strong>����˵����</strong></td>
        <td class="tdbg" align="left">
            <asp:Label ID="LblDownloadUrlRemark" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr class="tdbg">
        <td class="tdbgleft" style="width: 15%;" align="right">
            <strong>����������</strong></td>
        <td class="tdbg" align="left">
            <asp:Label ID="LblSalePromotionType" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr class="tdbg">
        <td class="tdbgleft" style="width: 15%;" align="right">
            <strong>���͵�ȯ��</strong></td>
        <td class="tdbg" align="left">
            <asp:Label ID="LblPresentPoint" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr class="tdbg">
        <td class="tdbgleft" style="width: 15%;" align="right">
            <strong>������֣�</strong></td>
        <td class="tdbg" align="left">
            <asp:Label ID="LblPresentExp" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr class="tdbg">
        <td class="tdbgleft" style="width: 15%;" align="right">
            <strong>�����ֽ�ȯ��</strong></td>
        <td class="tdbg" align="left">
            <asp:Label ID="LblPresentMoney" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr class="tdbg">
        <td class="tdbgleft" style="width: 15%;" align="right">
            <strong>���۲�����</strong></td>
        <td class="tdbg" align="left">
            <asp:Label ID="LblEnableSale" runat="server" Text=""></asp:Label></td>
    </tr>
</table>
