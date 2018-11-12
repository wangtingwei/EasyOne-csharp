<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.User.Info.PaymentLog" Codebehind="PaymentLog.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>在线支付记录查询</title>
</head>
<body>
    <pe:UserNavigation ID="UserCenterNavigation" Tab="shop" runat="server" />
    <pe:ExtendedSiteMapPath ID="YourPosition" SiteMapProvider="UserMapProvider" runat="server" />
    <form id="form1" runat="server">
        <div>
            <pe:ExtendedGridView ID="EgvPayment" runat="server" AutoGenerateColumns="False" CheckBoxFieldHeaderWidth="3%"
                DataSourceID="OdsPayment" SerialText="" OnRowDataBound="EgvPayment_RowDataBound"
                AllowPaging="True">
                <Columns>
                    <pe:BoundField DataField="PaymentNum" HeaderText="支付序号" SortExpression="PaymentNum">
                        <HeaderStyle Width="16%" />
                    </pe:BoundField>
                    <pe:TemplateField HeaderText="支付平台" SortExpression="PlatformId">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="LblPlatform"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="10%" />
                    </pe:TemplateField>
                    <pe:BoundField DataField="PayTime" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HeaderText="交易时间"
                        HtmlEncode="False" SortExpression="PayTime">
                        <HeaderStyle Width="16%" />
                    </pe:BoundField>
                    <pe:BoundField DataField="MoneyPay" DataFormatString="{0:N2}" HeaderText="汇款金额" SortExpression="MoneyPay"
                        HtmlEncode="False">
                        <ItemStyle HorizontalAlign="right" />
                        <HeaderStyle Width="10%" />
                    </pe:BoundField>
                    <pe:BoundField DataField="MoneyTrue" DataFormatString="{0:N2}" HeaderText="实际转账金额"
                        SortExpression="MoneyTrue" HtmlEncode="False">
                        <ItemStyle HorizontalAlign="right" />
                        <HeaderStyle Width="14%" />
                    </pe:BoundField>
                    <pe:TemplateField HeaderText="交易状态" SortExpression="Status">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="LblStatus"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="10%" />
                    </pe:TemplateField>
                    <pe:BoundField DataField="PlatformInfo" HeaderText="银行信息" SortExpression="PlatformInfo">
                        <HeaderStyle Width="10%" />
                    </pe:BoundField>
                    <pe:BoundField DataField="Remark" HeaderText="备注" SortExpression="Remark" />
                </Columns>
            </pe:ExtendedGridView>
        </div>
        <asp:ObjectDataSource ID="OdsPayment" runat="server" SelectMethod="GetListByUserName" TypeName="EasyOne.Accessories.PaymentLog"
            EnablePaging="True" MaximumRowsParameterName="maxNumberRows" SelectCountMethod="GetTotalOfPaymentLogByUserName"
            StartRowIndexParameterName="startRowIndexId">
            <SelectParameters>
                <asp:ControlParameter ControlID="HdnUserName" DefaultValue="" Name="userName" PropertyName="Value"
                    Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:HiddenField ID="HdnUserName" runat="server" />
    </form>
</body>
</html>
