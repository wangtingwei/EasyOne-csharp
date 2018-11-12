<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.User.BankrollItemList" Title="资金明细记录管理" Codebehind="BankrollItemList.aspx.cs" %>

<%@ Import Namespace="EasyOne.Accessories" %>
<%@ Import Namespace="EasyOne.Model.Accessories" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <pe:ExtendedGridView ID="EgdvBankrollItem" runat="server" DataSourceID="OdsBankrollItem"
        AutoGenerateColumns="False" ShowFooter="True" EmptyDataText="没有任何符合条件的资金记录！"
        ItemName="资金明细" AllowPaging="True" OnDataBound="EgdvBankrollItem_DataBound" OnRowDataBound="EgdvBankrollItem_RowDataBound"
        SerialText="" DataKeyNames="ItemID" OnRowCommand="EgdvBankrollItem_RowCommand" CheckBoxFieldHeaderWidth="3%" IsHoldState="True"
        RowDblclickBoundField="ItemId" RowDblclickUrl="BankrollItemDetail.aspx?BankrollItemID={$Field}">
        <Columns>
            <asp:BoundField DataField="DateAndTime" HeaderText="交易时间" SortExpression="DateAndTime"
                DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HtmlEncode="False">
                <HeaderStyle Width="12%" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="客户名称" SortExpression="ClientName">
                <ItemTemplate>
<A href='../Crm/ClientShow.aspx?ClientID=<%# Eval("ClientId") %>'><%# Eval("ClientName")%></A>
</ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="用户名" SortExpression="UserName">
                <HeaderStyle Width="8%" />
                <ItemTemplate>
                    <a href='../User/UserShow.aspx?UserName=<%#Server.UrlEncode(Eval("UserName").ToString()) %>'>
                        <%# Eval("UserName") %>
                    </a>
                
</ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="交易方式" SortExpression="MoneyType">
                <HeaderStyle Width="8%" />
                <ItemTemplate>
                    <%#  BankrollItem.GetMoneyType(Eval("MoneyType")) %>
                
</ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="币种" SortExpression="CurrencyType">
                <HeaderStyle Width="5%" />
                <ItemTemplate>
                    <%# BankrollItem.GetCurrencyType(Eval("CurrencyType")) %>
                
</ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="收入金额">
                <ItemStyle HorizontalAlign="Right" />
                <HeaderStyle Width="10%" />
                <ItemTemplate>
                    <%# (decimal)Eval("Money")>0?Eval("Money","{0:N2}"):"" %>
                
</ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="支出金额">
                <ItemStyle HorizontalAlign="Right" />
                <HeaderStyle Width="10%" />
                <ItemTemplate>
                    <%#  (decimal)Eval("Money")>0?"":Math.Abs((decimal)Eval("Money")).ToString("N2") %>
                
</ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="银行名称" SortExpression="UserName">
                <HeaderStyle Width="8%" />
                <ItemTemplate>
                    <%# (int)Eval("MoneyType")!=3?Eval("Bank"):PayPlatform.GetPayPlatformById((int)Eval("eBankID")).PayPlatformName%>
                
</ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="备注/说明">
                <itemstyle horizontalalign="Left" />
                <itemtemplate>
<asp:Label runat="server" Text='' id="LblRemark"></asp:Label>
</itemtemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="确认">
                <HeaderStyle Width="4%" />
                <ItemTemplate>
                    <%#(int)Eval("Status") == 0 ? "<font color=red>×</font>" : "√"%>
                
</ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="操作">
                <HeaderStyle Width="9%" />
                <ItemTemplate>
<asp:LinkButton id="LBtnDel" runat="server" Enabled='<%# (int)Eval("Status")==0 %>' CommandName="Delete" CommandArgument='<%# Eval("ItemId") %>'>删除</asp:LinkButton> <asp:LinkButton id="LBtnConfirm" runat="server" Enabled='<%# (int)Eval("Status")==0 %>' CommandName="Confirm" CommandArgument='<%# Eval("ItemId") %>'>确认</asp:LinkButton> <A href='<%#string.Format("BankrollItemDetail.aspx?BankrollItemID={0}",Eval("ItemId"))%>'>查看</A> 
</ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </pe:ExtendedGridView>
    <asp:ObjectDataSource ID="OdsBankrollItem" runat="server" SelectMethod="GetList" DeleteMethod="Delete"
        TypeName="EasyOne.Accessories.BankrollItem" SelectCountMethod="GetTotalOfBankrollItem"
        EnablePaging="True">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="0" Name="searchType" QueryStringField="SearchType"
                Type="Int32" />
            <asp:QueryStringParameter DefaultValue="0" Name="field" QueryStringField="Field"
                Type="Int32" />
            <asp:QueryStringParameter DefaultValue="" Name="keyword" QueryStringField="Keyword"
                Type="String" />
        </SelectParameters>
        <DeleteParameters>
            <asp:Parameter Name="itemId" Type="Int32" />
        </DeleteParameters>
    </asp:ObjectDataSource>
    <br />
    <span>注意：没确认的资金将不会计入合计当中。</span>
</asp:Content>
