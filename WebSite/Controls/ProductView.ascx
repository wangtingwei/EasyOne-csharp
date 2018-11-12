<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Controls.ProductView" Codebehind="ProductView.ascx.cs" %>
<table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="border">
    <tr class="tdbg">
        <td class="tdbgleft" style="width: 15%;" align="right">
            <strong>商品类别：</strong></td>
        <td class="tdbg" align="left">
            <asp:Label ID="LblProductKind" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr class="tdbg">
        <td class="tdbgleft" style="width: 15%;" align="right">
            <strong>商品性质：</strong></td>
        <td class="tdbg" align="left">
            <asp:Label ID="LblCharacter" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr class="tdbg">
        <td class="tdbgleft" style="width: 15%;" align="right">
            <strong>商品单位：</strong></td>
        <td class="tdbg" align="left">
            <asp:Label ID="LblUnit" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr class="tdbg">
        <td class="tdbgleft" style="width: 15%;" align="right">
            <strong>商品属性：</strong></td>
        <td class="tdbg" align="left">
            <asp:Label ID="LblProperties" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr class="tdbg">
        <td class="tdbgleft" style="width: 15%;" align="right">
            <strong>库存报警方案：</strong></td>
        <td class="tdbg" align="left">
            <asp:Label ID="LblStocksProject" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr class="tdbg">
        <td class="tdbgleft" style="width: 15%;" align="right">
            <strong>库存：</strong></td>
        <td class="tdbg" align="left">
            <pe:ExtendedGridView ID="EgvStocks" runat="server" AllowPaging="False" AutoGenerateColumns="False"
                AutoGenerateCheckBoxColumn="False" DataKeyNames="Id" ItemName="属性" ItemUnit="个">
                <Columns>
                    <pe:BoundField DataField="PropertyValue" NullDisplayText="单一型商品" HeaderText="属性商品"
                        ReadOnly="True" />
                    <pe:BoundField DataField="Stocks" HeaderText="库存数量" ReadOnly="True" />
                    <pe:BoundField DataField="AlarmNum" HeaderText="库存报警下限" ReadOnly="True" />
                    <pe:BoundField DataField="BuyTimes" HeaderText="购买次数" ReadOnly="True" />
                </Columns>
            </pe:ExtendedGridView>
        </td>
    </tr>
    <tr class="tdbg">
        <td class="tdbgleft" style="width: 15%;" align="right">
            <strong>税率设置：</strong></td>
        <td class="tdbg" align="left">
            <asp:Label ID="LblIncludeTax" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr class="tdbg">
        <td class="tdbgleft" style="width: 15%;" align="right">
            <strong>商品税率：</strong></td>
        <td class="tdbg" align="left">
            <asp:Label ID="LblTaxRate" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr class="tdbg">
        <td class="tdbgleft" style="width: 15%;" align="right">
            <strong>重量：</strong></td>
        <td class="tdbg" align="left">
            <asp:Label ID="LblWeight" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr class="tdbg">
        <td class="tdbgleft" style="width: 15%;" align="right">
            <strong>服务期限：</strong></td>
        <td class="tdbg" align="left">
            <asp:Label ID="LblServiceTerm" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr class="tdbg">
        <td class="tdbgleft" style="width: 15%;" align="right">
            <strong>商品类型：</strong></td>
        <td class="tdbg" align="left">
            <asp:Label ID="LblProductType" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr class="tdbg">
        <td class="tdbgleft" style="width: 15%;" align="right">
            <strong>商品价格：</strong></td>
        <td class="tdbg" align="left">
            <%--原始零售价：<asp:Label ID="LblPrice_Original" runat="server" Text=""></asp:Label>--%>
            当前零售价：<asp:Label ID="LblPrice" runat="server" Text=""></asp:Label>
            市场参考价：<asp:Label ID="LblPrice_Market" runat="server" Text=""></asp:Label>
            会员价格：<asp:Label ID="LblPrice_Member" runat="server" Text=""></asp:Label>
            代理商价格：<asp:Label ID="LblPrice_Agent" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr class="tdbg">
        <td class="tdbgleft" style="width: 15%;" align="right">
            <strong>生 产 商：</strong></td>
        <td class="tdbg" align="left">
            <asp:Label ID="LblProducer" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr class="tdbg">
        <td class="tdbgleft" style="width: 15%;" align="right">
            <strong>品牌/商标：</strong></td>
        <td class="tdbg" align="left">
            <asp:Label ID="LblTrademark" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr class="tdbg">
        <td class="tdbgleft" style="width: 15%;" align="right">
            <strong>下载地址：</strong></td>
        <td class="tdbg" align="left">
            <asp:Label ID="LblDownloadUrl" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr class="tdbg">
        <td class="tdbgleft" style="width: 15%;" align="right">
            <strong>下载说明：</strong></td>
        <td class="tdbg" align="left">
            <asp:Label ID="LblDownloadUrlRemark" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr class="tdbg">
        <td class="tdbgleft" style="width: 15%;" align="right">
            <strong>促销方案：</strong></td>
        <td class="tdbg" align="left">
            <asp:Label ID="LblSalePromotionType" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr class="tdbg">
        <td class="tdbgleft" style="width: 15%;" align="right">
            <strong>赠送点券：</strong></td>
        <td class="tdbg" align="left">
            <asp:Label ID="LblPresentPoint" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr class="tdbg">
        <td class="tdbgleft" style="width: 15%;" align="right">
            <strong>购物积分：</strong></td>
        <td class="tdbg" align="left">
            <asp:Label ID="LblPresentExp" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr class="tdbg">
        <td class="tdbgleft" style="width: 15%;" align="right">
            <strong>赠送现金券：</strong></td>
        <td class="tdbg" align="left">
            <asp:Label ID="LblPresentMoney" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr class="tdbg">
        <td class="tdbgleft" style="width: 15%;" align="right">
            <strong>销售操作：</strong></td>
        <td class="tdbg" align="left">
            <asp:Label ID="LblEnableSale" runat="server" Text=""></asp:Label></td>
    </tr>
</table>
