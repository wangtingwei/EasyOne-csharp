<%@ Control Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Controls.WebPart.Orders" Codebehind="Orders.ascx.cs" %>
<%@ Import Namespace="EasyOne.Components" %>
<pe:ExtendedGridView ID="EgvOrders" runat="server" CheckBoxFieldHeaderWidth="3%"
    IsHoldState="True" SerialText="" AutoGenerateColumns="False" OnRowDataBound="EgvOrders_RowDataBound">
    <Columns>
        <asp:TemplateField HeaderText="�������">
            <itemtemplate>
            <asp:HyperLink ID="HlnkOrderNum" runat="server" Text='<%#Eval("OrderNum")%>' />
</itemtemplate>
            <HeaderStyle Wrap="False" />
        </asp:TemplateField>
         <asp:TemplateField HeaderText="�ͻ���" SortExpression="ClientName">
                <ItemTemplate>
                    <a href='<%#string.Format(BasePath + SiteConfig.SiteOption.ManageDir+"/Crm/ClientShow.aspx?ClientID={0}",Eval("ClientID")) %>'>
                        <%#Eval("ClientName")%>
                    </a>
                
</ItemTemplate>
                <HeaderStyle Wrap="False" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="�û���">
                <ItemTemplate>
                    <a href='<%#string.Format(BasePath + SiteConfig.SiteOption.ManageDir+"/User/UserShow.aspx?UserName={0}",Server.UrlEncode(Convert.ToString(Eval("UserName")))) %>'>
                        <%#Eval("UserName")%>
                    </a>
                
</ItemTemplate>
                <HeaderStyle Wrap="False" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="�µ�ʱ��" SortExpression="InputTime">
                <ItemTemplate>
                    <%# Eval("InputTime", "{0:yyyy-MM-dd HH:mm:ss}")%>
                
</ItemTemplate>
                <HeaderStyle Wrap="False" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="����״̬">
                <ItemTemplate>
                    <pe:ExtendedLabel HtmlEncode="false" ID="LblOrderStatus" runat="server" />
                
</ItemTemplate>
                <HeaderStyle Wrap="False" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="����״̬">
                <ItemTemplate>
                    <pe:ExtendedLabel HtmlEncode="false" ID="LblPayStatus" runat="server" />
                
</ItemTemplate>
                <HeaderStyle Wrap="False" />
            </asp:TemplateField>
    </Columns>
</pe:ExtendedGridView>
