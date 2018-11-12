<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Controls.AddressList" Codebehind="AddressList.ascx.cs" %>
<div>
    <pe:ExtendedGridView ID="EgvAddress" AutoGenerateColumns="False"
        runat="server" OnRowDataBound="EgvAddress_RowDataBound" CheckBoxFieldHeaderWidth="3%" IsHoldState="True" SerialText="">
        <Columns>
            <asp:TemplateField HeaderText="�ջ���"></asp:TemplateField>
            <asp:TemplateField HeaderText="����">
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ʡ��">
            </asp:TemplateField>
            <asp:TemplateField HeaderText="����">
            </asp:TemplateField>
            <asp:TemplateField HeaderText="����(��)">
            </asp:TemplateField>
            <asp:TemplateField HeaderText="��������">
            </asp:TemplateField>
            <asp:TemplateField HeaderText="�ֵ���ַ">
                <ItemTemplate>
                    <asp:Label ID="LblAddress" ToolTip='<%#Eval("Address")%>' runat="server" Text="�鿴"></asp:Label>
                
</ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </pe:ExtendedGridView>
</div>
