<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Controls.WebPart.SigninContent" Codebehind="SigninContent.ascx.cs" %>
<pe:ExtendedGridView ID="EgvContent" runat="server" SerialText="" AutoGenerateCheckBoxColumn="False"
    AutoGenerateColumns="False" AllowPaging="False" DataKeyNames="GeneralId">
    <Columns>
        <pe:BoundField DataField="GeneralId" HeaderText="ID" SortExpression="GeneralId">
            <ItemStyle Width="5%" />
        </pe:BoundField>
        <pe:TemplateField HeaderText="标题" SortExpression="Title">
            <ItemStyle HorizontalAlign="Left" Width="67%" />
            <ItemTemplate>
                <asp:Label ID="LblNodeLink" runat="server"></asp:Label><a href='<%# "javascript:OpenLink(\"Contents/NodeTree.aspx\"," + "\"Contents/ContentView.aspx?" + ((bool)Eval("IsEshop")?"IsEshop=true&":"") + "GeneralID=" + Eval("GeneralId") +"\")"%>'><%# Eval("Title").ToString().Length <= 20 ? Eval("Title") : Eval("Title").ToString().Substring(0, 20) + ".."%></a>
            </ItemTemplate>
        </pe:TemplateField>
        <pe:BoundField DataField="Inputer" HeaderText="录入者" SortExpression="Inputer">
            <ItemStyle Width="10%" />
        </pe:BoundField>
        <pe:BoundField DataField="Hits" HeaderText="点击数" SortExpression="Hits">
            <ItemStyle Width="10%" HorizontalAlign="Center" />
        </pe:BoundField>
        <pe:TemplateField HeaderText="操作" SortExpression="Disabled">
            <ItemStyle Width="8%" HorizontalAlign="Center" />
            <ItemTemplate>
                <a href='<%# "javascript:OpenLink(\"Contents/NodeTree.aspx\"," + "\"Contents/Content.aspx?Action=Modify&GeneralID=" + Eval("GeneralID") +"\")"%>'>修改</a>
            </ItemTemplate>
        </pe:TemplateField>
    </Columns>
</pe:ExtendedGridView>
