<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.User.Info.Bankroll" Codebehind="Bankroll.aspx.cs" %>

<%@ Import Namespace="EasyOne.Accessories" %>
<%@ Import Namespace="EasyOne.Model.Accessories" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>资金明细查询</title>
</head>
<body>
    <pe:UserNavigation ID="UserCenterNavigation" Tab="shop" runat="server" />
    <pe:ExtendedSiteMapPath ID="YourPosition" SiteMapProvider="UserMapProvider" runat="server" />
    <form id="form1" runat="server">
        <div>
            <pe:ExtendedGridView ID="EgvBankroll" runat="server" AutoGenerateColumns="False"
                CheckBoxFieldHeaderWidth="3%" DataSourceID="OdsBankroll" EmptyDataText="没有任何符合条件的资金记录！"
                SerialText="" DataKeyNames="ItemId" AllowPaging="True" OnDataBound="EgvBankroll_DataBound"
                OnRowDataBound="EgvBankroll_RowDataBound" ShowFooter="True" IsHoldState="True">
                <Columns>
                    <asp:BoundField DataField="DateAndTime" HeaderText="交易时间" SortExpression="DateAndTime"
                        DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HtmlEncode="False">
                        <HeaderStyle Width="16%" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="交易方式" SortExpression="MoneyType">
                        <HeaderStyle Width="10%" />
                        <ItemTemplate>
                            <%#  BankrollItem.GetMoneyType(Eval("MoneyType")) %>
                        
</ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="币种" SortExpression="CurrencyType">
                        <HeaderStyle Width="10%" />
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
                    <asp:TemplateField HeaderText="银行名称" SortExpression="Bank">
                        <ItemTemplate>
                            <%# (int)Eval("MoneyType") == 3 ? PayPlatform.GetPayPlatformById((int)Eval("EBankId")).PayPlatformName : (Eval("Bank")== null ? "" : Eval("Bank").ToString())%>
                        
</ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="备注/说明" SortExpression="Remark">
                        <itemstyle horizontalalign="Left" />
                        <itemtemplate>
<asp:Label runat="server" Text='' id="LblRemark"></asp:Label>
</itemtemplate>
                    </asp:TemplateField>
                </Columns>
            </pe:ExtendedGridView>
        </div>
        <br />
        <span>注意：没确认的资金将不会计入合计当中。</span>
        <asp:ObjectDataSource ID="OdsBankroll" runat="server" SelectMethod="GetList" TypeName="EasyOne.Accessories.BankrollItem"
            EnablePaging="True" SelectCountMethod="GetTotalOfBankrollItem">
            <SelectParameters>
                <asp:Parameter DefaultValue="0" Name="startRowIndex" Type="Int32" />
                <asp:Parameter DefaultValue="10" Name="maximumRows" Type="Int32" />
                <asp:Parameter DefaultValue="10" Name="searchType" Type="Int32" />
                <asp:QueryStringParameter DefaultValue="6" Name="field" QueryStringField="ShowType"
                    Type="Int32" />
                <asp:ControlParameter ControlID="HdnUserName" DefaultValue="" Name="keyword" PropertyName="Value"
                    Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:HiddenField ID="HdnUserName" runat="server" />
    </form>
</body>
</html>
