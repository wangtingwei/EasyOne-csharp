<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Accessories.BankManage" Codebehind="BankManage.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server"
        AdditionalNode="所有银行账户" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <pe:ExtendedGridView ID="EgvBankList" runat="server" AutoGenerateColumns="false"
        AllowPaging="false" DataSourceID="OdsBankList" DataKeyNames="BankId" OnRowCommand="EgvBankList_RowCommand"
        ItemName="账户" ItemUnit="个" OnRowDataBound="EgvBankList_RowDataBound" RowDblclickBoundField="BankId"
        RowDblclickUrl="Bank.aspx?Action=Modify&amp;ID={$Field}">
        <Columns>
            <pe:BoundField DataField="BankId" HeaderText="ID" SortExpression="BankId">
                <HeaderStyle Width="5%" />
            </pe:BoundField>
            <pe:BoundField DataField="BankShortName" HeaderText="账户名称" SortExpression="BankShortName">
                <HeaderStyle Width="13%" />
            </pe:BoundField>
            <pe:BoundField DataField="BankName" HeaderText="开户行" SortExpression="BankName">
                <HeaderStyle Width="15%" />
            </pe:BoundField>
            <pe:BoundField DataField="HolderName" HeaderText="户名" SortExpression="HolderName">
                <HeaderStyle Width="12%" />
            </pe:BoundField>
            <pe:TemplateField HeaderText="账户/卡号" SortExpression="Disabled">
                <ItemStyle HorizontalAlign="Left" />
                <ItemTemplate>
                    账户：<%# Eval("Accounts") %><br />
                    卡号：<%# Eval("CardNum") %>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="是否默认" SortExpression="Disabled">
                <HeaderStyle Width="8%" />
                <ItemTemplate>
                    <%# (bool)Eval("IsDefault") == true ? "<font color=green>√</font>" : "<font color=red>×</font>"%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="已启用" SortExpression="Disabled">
                <HeaderStyle Width="6%" />
                <ItemTemplate>
                    <%# (bool)Eval("IsDisabled") == true ? "<font color=red>×</font>" : "<font color=green>√</font>"%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="常规操作">
                <HeaderStyle Width="9%" />
                <ItemTemplate>
                    <asp:LinkButton ID="LbtnDefault" runat="server" CommandName="Default" CommandArgument='<%# Eval("BankID") %>'>默认</asp:LinkButton>
                    <asp:LinkButton ID="LbtnDisabled" runat="server" CommandName='<%# (bool)Eval("IsDisabled") == true ? "Enabled" : "Disabled"%>'
                        CommandArgument='<%# Eval("BankID") %>'><%# (bool)Eval("IsDisabled") == true ? "启用" : "禁用"%></asp:LinkButton><br />
                    <a href='<%# Eval("BankID","Bank.aspx?Action=Modify&ID={0}") %>'>修改</a>
                    <asp:LinkButton ID="LbtnDel" CommandName="Del" CommandArgument='<%# Eval("BankID") %>'
                        OnClientClick='<%# (bool)Eval("IsDefault") == true ? "" : "return confirm(\"确定要删除此记录吗？\");"%>'
                        runat="server">删除</asp:LinkButton>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="排序操作" SortExpression="OrderID">
                <HeaderStyle Width="8%" />
                <ItemTemplate>
                    <asp:DropDownList ID="DropOrderId" runat="server">
                    </asp:DropDownList>
                </ItemTemplate>
            </pe:TemplateField>
        </Columns>
    </pe:ExtendedGridView>
    <br />
    <asp:Button ID="BtnSaveSort" runat="server" OnClick="BtnSaveSort_Click" Text="保存排序" /><br />
    说明：“禁用”某银行账户后，输入汇款信息时将不再显示此银行账户，但在资金明细情况中仍会显示。<br />
    <asp:ObjectDataSource ID="OdsbankList" runat="server" SelectCountMethod="Count" SelectMethod="GetList"
        TypeName="EasyOne.Accessories.Bank" EnablePaging="true" StartRowIndexParameterName="startRowIndexId"
        MaximumRowsParameterName="maxNumberRows"></asp:ObjectDataSource>
</asp:Content>
