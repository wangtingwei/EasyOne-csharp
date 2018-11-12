<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Controls.AddressList" Codebehind="AddressList.ascx.cs" %>
<div>
    <pe:ExtendedGridView ID="EgvAddress" AutoGenerateColumns="False"
        runat="server" OnRowDataBound="EgvAddress_RowDataBound" CheckBoxFieldHeaderWidth="3%" IsHoldState="True" SerialText="">
        <Columns>
            <asp:TemplateField HeaderText="收货人"></asp:TemplateField>
            <asp:TemplateField HeaderText="国家">
            </asp:TemplateField>
            <asp:TemplateField HeaderText="省份">
            </asp:TemplateField>
            <asp:TemplateField HeaderText="城市">
            </asp:TemplateField>
            <asp:TemplateField HeaderText="地区(县)">
            </asp:TemplateField>
            <asp:TemplateField HeaderText="邮政编码">
            </asp:TemplateField>
            <asp:TemplateField HeaderText="街道地址">
                <ItemTemplate>
                    <asp:Label ID="LblAddress" ToolTip='<%#Eval("Address")%>' runat="server" Text="查看"></asp:Label>
                
</ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </pe:ExtendedGridView>
</div>
