<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Controls.ShowMessageList" Codebehind="ShowMessageList.ascx.cs" %>
<pe:ExtendedGridView ID="GdvMessageList" runat="server" DataKeyNames="MessageId"
    AllowPaging="True" ItemName="����Ϣ" AutoGenerateColumns="False" AutoGenerateCheckBoxColumn="True"
    OnRowDataBound="GdvMessageList_RowDataBound" OnRowCommand="GdvMessageList_RowCommand"
    OnPageIndexChanging="GdvMessageList_PageIndexChanging" DataSourceID="OdsMessageList"
    CheckBoxFieldHeaderWidth="3%" SerialText="" OnDataBound="GdvMessageList_DataBound">
    <Columns>
        <pe:TemplateField HeaderText="����Ϣ����" ShowHeader="False">
            <ItemStyle HorizontalAlign="Left" />
            <ItemTemplate>
                <asp:HyperLink runat="server" ID="HlnkMessage" Text='<%# Eval("Title") %>' NavigateUrl='<%# Eval("MessageID","~/Admin/Accessories/MessageRead.aspx?MessageID={0}") %>'></asp:HyperLink></ItemTemplate>
        </pe:TemplateField>
        <pe:BoundField DataField="Incept" HeaderText="�ռ���"  />
        <pe:BoundField DataField="Sender" HeaderText="������"  />
        <pe:TemplateField HeaderText="��������" SortExpression="SendTime">
            <ItemTemplate>
                <%# Eval("SendTime", "{0:yyyy-MM-dd HH:mm:ss}")%>
            </ItemTemplate>
        </pe:TemplateField>
        <pe:TemplateField HeaderText="��С" SortExpression="Content">
            <ItemStyle HorizontalAlign="Right" />
            <ItemTemplate>
                <%# Eval("Content").ToString().Length +" Byte"%>
            </ItemTemplate>
        </pe:TemplateField>
        <pe:TemplateField HeaderText="�Ѷ�">
            <ItemTemplate>
                <%# Convert.ToInt32(Eval("IsRead")) == 0 ? "<font color=red>��</font>" : "��"%>
            </ItemTemplate>
        </pe:TemplateField>
        <pe:TemplateField HeaderText="����">
            <ItemTemplate>
                <asp:LinkButton ID="LbtnDelete" runat="server" CausesValidation="False" CommandArgument='<%# Eval("MessageID") %>'
                    CommandName="Del" OnClientClick='return confirm("�Ƿ�ɾ���˶���Ϣ?")'>ɾ��</asp:LinkButton>
            </ItemTemplate>
        </pe:TemplateField>
    </Columns>
</pe:ExtendedGridView>
<br />
<input id="ChkAll" onclick="javascript:CheckAll(this);" type="checkbox" />ѡ�б�ҳ��ʾ�����ж���Ϣ
<asp:Button ID="BtnDelete" runat="server" Text="ɾ��ѡ���Ķ���Ϣ" OnClientClick='return confirm("�Ƿ�ɾ����ѡ���Ķ���Ϣ?")'
    OnClick="BtnDelete_Click" CausesValidation="False" />
<asp:Button ID="BtnDeleteAll" runat="server" Text="Button" OnClick="BtnDeleteAll_Click"
    OnClientClick="return confirm('ȷ��Ҫ��ն���Ϣ��')" />&nbsp;<br />
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
