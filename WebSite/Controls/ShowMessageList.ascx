<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Controls.ShowMessageList" Codebehind="ShowMessageList.ascx.cs" %>
<pe:ExtendedGridView ID="GdvMessageList" runat="server" DataKeyNames="MessageId"
    AllowPaging="True" ItemName="短消息" AutoGenerateColumns="False" AutoGenerateCheckBoxColumn="True"
    OnRowDataBound="GdvMessageList_RowDataBound" OnRowCommand="GdvMessageList_RowCommand"
    OnPageIndexChanging="GdvMessageList_PageIndexChanging" DataSourceID="OdsMessageList"
    CheckBoxFieldHeaderWidth="3%" SerialText="" OnDataBound="GdvMessageList_DataBound">
    <Columns>
        <pe:TemplateField HeaderText="短消息主题" ShowHeader="False">
            <ItemStyle HorizontalAlign="Left" />
            <ItemTemplate>
                <asp:HyperLink runat="server" ID="HlnkMessage" Text='<%# Eval("Title") %>' NavigateUrl='<%# Eval("MessageID","~/Admin/Accessories/MessageRead.aspx?MessageID={0}") %>'></asp:HyperLink></ItemTemplate>
        </pe:TemplateField>
        <pe:BoundField DataField="Incept" HeaderText="收件人"  />
        <pe:BoundField DataField="Sender" HeaderText="发件人"  />
        <pe:TemplateField HeaderText="发送日期" SortExpression="SendTime">
            <ItemTemplate>
                <%# Eval("SendTime", "{0:yyyy-MM-dd HH:mm:ss}")%>
            </ItemTemplate>
        </pe:TemplateField>
        <pe:TemplateField HeaderText="大小" SortExpression="Content">
            <ItemStyle HorizontalAlign="Right" />
            <ItemTemplate>
                <%# Eval("Content").ToString().Length +" Byte"%>
            </ItemTemplate>
        </pe:TemplateField>
        <pe:TemplateField HeaderText="已读">
            <ItemTemplate>
                <%# Convert.ToInt32(Eval("IsRead")) == 0 ? "<font color=red>×</font>" : "√"%>
            </ItemTemplate>
        </pe:TemplateField>
        <pe:TemplateField HeaderText="操作">
            <ItemTemplate>
                <asp:LinkButton ID="LbtnDelete" runat="server" CausesValidation="False" CommandArgument='<%# Eval("MessageID") %>'
                    CommandName="Del" OnClientClick='return confirm("是否删除此短消息?")'>删除</asp:LinkButton>
            </ItemTemplate>
        </pe:TemplateField>
    </Columns>
</pe:ExtendedGridView>
<br />
<input id="ChkAll" onclick="javascript:CheckAll(this);" type="checkbox" />选中本页显示的所有短消息
<asp:Button ID="BtnDelete" runat="server" Text="删除选定的短消息" OnClientClick='return confirm("是否删除所选定的短消息?")'
    OnClick="BtnDelete_Click" CausesValidation="False" />
<asp:Button ID="BtnDeleteAll" runat="server" Text="Button" OnClick="BtnDeleteAll_Click"
    OnClientClick="return confirm('确定要清空短消息吗？')" />&nbsp;<br />
<asp:HiddenField ID="HdnSearchField" runat="server" />
<asp:HiddenField ID="HdnKeyword" runat="server" />
<asp:ObjectDataSource ID="OdsMessageList" runat="server" SelectMethod="GetMessageList"
    SelectCountMethod="Count" TypeName="EasyOne.Accessories.Message" EnablePaging="True"
    StartRowIndexParameterName="startRowIndexId" MaximumRowsParameterName="maxNumberRows">
    <SelectParameters>
        <asp:ControlParameter ControlID="HdnSearchField" DefaultValue="0" Name="searchField"
            PropertyName="Value" />
        <asp:ControlParameter ControlID="HdnKeyword" DefaultValue="" Name="keyword" PropertyName="Value" />
    </SelectParameters>
</asp:ObjectDataSource>
<asp:HiddenField ID="HdnManageType" runat="server" Value="0" />
