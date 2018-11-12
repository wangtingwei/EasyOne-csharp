<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.User.BankrollItemList" Title="�ʽ���ϸ��¼����" Codebehind="BankrollItemList.aspx.cs" %>

<%@ Import Namespace="EasyOne.Accessories" %>
<%@ Import Namespace="EasyOne.Model.Accessories" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <pe:ExtendedGridView ID="EgdvBankrollItem" runat="server" DataSourceID="OdsBankrollItem"
        AutoGenerateColumns="False" ShowFooter="True" EmptyDataText="û���κη����������ʽ��¼��"
        ItemName="�ʽ���ϸ" AllowPaging="True" OnDataBound="EgdvBankrollItem_DataBound" OnRowDataBound="EgdvBankrollItem_RowDataBound"
        SerialText="" DataKeyNames="ItemID" OnRowCommand="EgdvBankrollItem_RowCommand" CheckBoxFieldHeaderWidth="3%" IsHoldState="True"
        RowDblclickBoundField="ItemId" RowDblclickUrl="BankrollItemDetail.aspx?BankrollItemID={$Field}">
        <Columns>
            <asp:BoundField DataField="DateAndTime" HeaderText="����ʱ��" SortExpression="DateAndTime"
                DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HtmlEncode="False">
                <HeaderStyle Width="12%" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="�ͻ�����" SortExpression="ClientName">
                <ItemTemplate>
<A href='../Crm/ClientShow.aspx?ClientID=<%# Eval("ClientId") %>'><%# Eval("ClientName")%></A>
</ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="�û���" SortExpression="UserName">
                <HeaderStyle Width="8%" />
                <ItemTemplate>
                    <a href='../User/UserShow.aspx?UserName=<%#Server.UrlEncode(Eval("UserName").ToString()) %>'>
                        <%# Eval("UserName") %>
                    </a>
                
</ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="���׷�ʽ" SortExpression="MoneyType">
                <HeaderStyle Width="8%" />
                <ItemTemplate>
                    <%#  BankrollItem.GetMoneyType(Eval("MoneyType")) %>
                
</ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="����" SortExpression="CurrencyType">
                <HeaderStyle Width="5%" />
                <ItemTemplate>
                    <%# BankrollItem.GetCurrencyType(Eval("CurrencyType")) %>
                
</ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="������">
                <ItemStyle HorizontalAlign="Right" />
                <HeaderStyle Width="10%" />
                <ItemTemplate>
                    <%# (decimal)Eval("Money")>0?Eval("Money","{0:N2}"):"" %>
                
</ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="֧�����">
                <ItemStyle HorizontalAlign="Right" />
                <HeaderStyle Width="10%" />
                <ItemTemplate>
                    <%#  (decimal)Eval("Money")>0?"":Math.Abs((decimal)Eval("Money")).ToString("N2") %>
                
</ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="��������" SortExpression="UserName">
                <HeaderStyle Width="8%" />
                <ItemTemplate>
                    <%# (int)Eval("MoneyType")!=3?Eval("Bank"):PayPlatform.GetPayPlatformById((int)Eval("eBankID")).PayPlatformName%>
                
</ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="��ע/˵��">
                <itemstyle horizontalalign="Left" />
                <itemtemplate>
<asp:Label runat="server" Text='' id="LblRemark"></asp:Label>
</itemtemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ȷ��">
                <HeaderStyle Width="4%" />
                <ItemTemplate>
                    <%#(int)Eval("Status") == 0 ? "<font color=red>��</font>" : "��"%>
                
</ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="����">
                <HeaderStyle Width="9%" />
                <ItemTemplate>
<asp:LinkButton id="LBtnDel" runat="server" Enabled='<%# (int)Eval("Status")==0 %>' CommandName="Delete" CommandArgument='<%# Eval("ItemId") %>'>ɾ��</asp:LinkButton> <asp:LinkButton id="LBtnConfirm" runat="server" Enabled='<%# (int)Eval("Status")==0 %>' CommandName="Confirm" CommandArgument='<%# Eval("ItemId") %>'>ȷ��</asp:LinkButton> <A href='<%#string.Format("BankrollItemDetail.aspx?BankrollItemID={0}",Eval("ItemId"))%>'>�鿴</A> 
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
    <span>ע�⣺ûȷ�ϵ��ʽ𽫲������ϼƵ��С�</span>
</asp:Content>
