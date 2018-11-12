<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Accessories.BankManage" Codebehind="BankManage.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server"
        AdditionalNode="���������˻�" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <pe:ExtendedGridView ID="EgvBankList" runat="server" AutoGenerateColumns="false"
        AllowPaging="false" DataSourceID="OdsBankList" DataKeyNames="BankId" OnRowCommand="EgvBankList_RowCommand"
        ItemName="�˻�" ItemUnit="��" OnRowDataBound="EgvBankList_RowDataBound" RowDblclickBoundField="BankId"
        RowDblclickUrl="Bank.aspx?Action=Modify&amp;ID={$Field}">
        <Columns>
            <pe:BoundField DataField="BankId" HeaderText="ID" SortExpression="BankId">
                <HeaderStyle Width="5%" />
            </pe:BoundField>
            <pe:BoundField DataField="BankShortName" HeaderText="�˻�����" SortExpression="BankShortName">
                <HeaderStyle Width="13%" />
            </pe:BoundField>
            <pe:BoundField DataField="BankName" HeaderText="������" SortExpression="BankName">
                <HeaderStyle Width="15%" />
            </pe:BoundField>
            <pe:BoundField DataField="HolderName" HeaderText="����" SortExpression="HolderName">
                <HeaderStyle Width="12%" />
            </pe:BoundField>
            <pe:TemplateField HeaderText="�˻�/����" SortExpression="Disabled">
                <ItemStyle HorizontalAlign="Left" />
                <ItemTemplate>
                    �˻���<%# Eval("Accounts") %><br />
                    ���ţ�<%# Eval("CardNum") %>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="�Ƿ�Ĭ��" SortExpression="Disabled">
                <HeaderStyle Width="8%" />
                <ItemTemplate>
                    <%# (bool)Eval("IsDefault") == true ? "<font color=green>��</font>" : "<font color=red>��</font>"%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="������" SortExpression="Disabled">
                <HeaderStyle Width="6%" />
                <ItemTemplate>
                    <%# (bool)Eval("IsDisabled") == true ? "<font color=red>��</font>" : "<font color=green>��</font>"%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="�������">
                <HeaderStyle Width="9%" />
                <ItemTemplate>
                    <asp:LinkButton ID="LbtnDefault" runat="server" CommandName="Default" CommandArgument='<%# Eval("BankID") %>'>Ĭ��</asp:LinkButton>
                    <asp:LinkButton ID="LbtnDisabled" runat="server" CommandName='<%# (bool)Eval("IsDisabled") == true ? "Enabled" : "Disabled"%>'
                        CommandArgument='<%# Eval("BankID") %>'><%# (bool)Eval("IsDisabled") == true ? "����" : "����"%></asp:LinkButton><br />
                    <a href='<%# Eval("BankID","Bank.aspx?Action=Modify&ID={0}") %>'>�޸�</a>
                    <asp:LinkButton ID="LbtnDel" CommandName="Del" CommandArgument='<%# Eval("BankID") %>'
                        OnClientClick='<%# (bool)Eval("IsDefault") == true ? "" : "return confirm(\"ȷ��Ҫɾ���˼�¼��\");"%>'
                        runat="server">ɾ��</asp:LinkButton>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="�������" SortExpression="OrderID">
                <HeaderStyle Width="8%" />
                <ItemTemplate>
                    <asp:DropDownList ID="DropOrderId" runat="server">
                    </asp:DropDownList>
                </ItemTemplate>
            </pe:TemplateField>
        </Columns>
    </pe:ExtendedGridView>
    <br />
    <asp:Button ID="BtnSaveSort" runat="server" OnClick="BtnSaveSort_Click" Text="��������" /><br />
    ˵���������á�ĳ�����˻�����������Ϣʱ��������ʾ�������˻��������ʽ���ϸ������Ի���ʾ��<br />
    <asp:ObjectDataSource ID="OdsbankList" runat="server" SelectCountMethod="Count" SelectMethod="GetList"
        TypeName="EasyOne.Accessories.Bank" EnablePaging="true" StartRowIndexParameterName="startRowIndexId"
        MaximumRowsParameterName="maxNumberRows"></asp:ObjectDataSource>
</asp:Content>
