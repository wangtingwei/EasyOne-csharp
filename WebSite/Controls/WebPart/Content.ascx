<%@ Control Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Controls.WebPart.Content" Codebehind="Content.ascx.cs" %>
<pe:ExtendedGridView ID="EgvContent" runat="server" SerialText="" AutoGenerateCheckBoxColumn="False"
    AutoGenerateColumns="False" AllowPaging="False" DataKeyNames="GeneralId" OnRowDataBound="EgvContent_RowDataBound">
    <Columns>
        <pe:BoundField DataField="GeneralId" HeaderText="ID" SortExpression="GeneralId">
            <HeaderStyle Width="5%" />
        </pe:BoundField>
        <pe:TemplateField HeaderText="标题" SortExpression="Title">
            <ItemStyle HorizontalAlign="left" />
            <ItemTemplate>
                <asp:HyperLink ID="LnkNodeLink" runat="server"></asp:HyperLink>
                <asp:HyperLink ID="HypTitle" runat="server"></asp:HyperLink>
            </ItemTemplate>
        </pe:TemplateField>
        <pe:BoundField DataField="Inputer" HeaderText="录入者" SortExpression="Inputer">
            <HeaderStyle Width="15%" />
        </pe:BoundField>
        <%--<pe:BoundField DataField="Hits" HeaderText="点击数" SortExpression="Hits">
            <headerstyle width="15%" />
        </pe:BoundField>--%>
        <pe:TemplateField HeaderText="操作" SortExpression="Disabled">
            <HeaderStyle Width="15%" />
            <ItemTemplate>
                <asp:HyperLink ID="ContentModify" runat="server"></asp:HyperLink>
            </ItemTemplate>
        </pe:TemplateField>
    </Columns>
</pe:ExtendedGridView>
