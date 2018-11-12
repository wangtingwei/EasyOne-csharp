<%@ Control Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Controls.WebPart.MyMessage" Codebehind="MyMessage.ascx.cs" %>
<pe:ExtendedGridView ID="GdvMessageList" runat="server" DataKeyNames="MessageId"
    AllowPaging="False" ItemName="短消息" ItemUnit="条" AutoGenerateColumns="False" AutoGenerateCheckBoxColumn="False">
    <Columns>
        <pe:BoundField DataField="Sender" HeaderText="发件人" >
        <HeaderStyle Width="20%" />
        </pe:BoundField>
        <pe:TemplateField HeaderText="短消息主题" ShowHeader="False">
        <ItemStyle HorizontalAlign="Left" />
            <ItemTemplate>
                <pe:ExtendedAnchor IsChecked="false" runat="server" href=<%# "javascript:OpenLink('Accessories/MessageGuide.aspx','" +  Eval("MessageID","Accessories/MessageRead.aspx?MessageID={0}").ToString() +"')" %>>
                    <%# Eval("Title") %>
                </pe:ExtendedAnchor>
            </ItemTemplate>
        </pe:TemplateField>
        <pe:TemplateField HeaderText="发送日期" SortExpression="SendTime">
            <HeaderStyle Width="40%" />
            <ItemTemplate>
                <%# Eval("SendTime", "{0:yyyy-MM-dd HH:mm:ss}")%>
            </ItemTemplate>
        </pe:TemplateField>
        <pe:TemplateField HeaderText="已读">
        <HeaderStyle Width="10%" />
            <ItemTemplate>
                <%# Convert.ToInt32(Eval("IsRead")) == 0 ? "<font color=red>×</font>" : "√"%>
            </ItemTemplate>
        </pe:TemplateField>
    </Columns>
</pe:ExtendedGridView>
