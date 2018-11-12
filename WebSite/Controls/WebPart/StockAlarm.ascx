<%@ Control Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Controls.WebPart.StockAlarm" Codebehind="StockAlarm.ascx.cs" %>
<pe:ExtendedGridView ID="EgvStockAlarm" runat="server" 
    AutoGenerateColumns="False" CheckBoxFieldHeaderWidth="3%" IsHoldState="True" 
    SerialText="" ondatabound="EgvStockAlarm_DataBound" 
    onrowdatabound="EgvStockAlarm_RowDataBound">
    <Columns>
        <asp:TemplateField HeaderText="商品名称">
        <ItemTemplate>
            <asp:HyperLink ID="HlnkProductName" runat="server" />
        </ItemTemplate>
            <ItemStyle HorizontalAlign="Left" />
        </asp:TemplateField>
        <asp:BoundField DataField="Stocks" HeaderText="库存" >
            <ItemStyle Font-Bold="True" />
        </asp:BoundField>
        <asp:BoundField DataField="AlarmNum" HeaderText="库存报警下限" />
        <asp:BoundField DataField="OrderNum" HeaderText="订购数量" />
    </Columns>
</pe:ExtendedGridView>
