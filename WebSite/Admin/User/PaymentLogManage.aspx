<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.User.PaymentLogManage" Title="在线支付记录管理" Codebehind="PaymentLogManage.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server"
        AdditionalNode="所有在线支付记录" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <pe:ExtendedGridView ID="GdvPaymentLogList" runat="server" AutoGenerateCheckBoxColumn="True"
        DataSourceID="OdsPaymentLog" DataKeyNames="PaymentLogId" OnRowCommand="GdvPaymentLogList_RowCommand"
        AutoGenerateColumns="False" AllowPaging="True" OnRowDataBound="GdvPaymentLogList_RowDataBound"
        HorizontalAlign="Center" CheckBoxFieldHeaderWidth="3%" SerialText=""
         RowDblclickBoundField="PaymentLogID" RowDblclickUrl="PaymentLogDetail.aspx?PaymentLogID={$Field}">
        <Columns>
            <pe:BoundField DataField="PaymentNum" HeaderText="支付序号" SortExpression="PaymentNum"
                >
                <HeaderStyle Width="14%" />
            </pe:BoundField>
            <pe:BoundField DataField="UserName" HeaderText="用户名" SortExpression="UserName" >
                <HeaderStyle Width="8%" />
            </pe:BoundField>
            <pe:TemplateField HeaderText="支付平台">
                <ItemTemplate>
                    <asp:Label ID="LblPlatform" runat="server" />
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="交易时间" SortExpression="PayTime">
                <HeaderStyle Width="16%" />
                <ItemTemplate>
                    <%# Eval("PayTime", "{0:yyyy-MM-dd HH:mm:ss}")%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="汇款金额" SortExpression="MoneyPay">
                <HeaderStyle Width="12%" />
                <ItemStyle HorizontalAlign="right" />
                <ItemTemplate>
                    <%# Eval("MoneyPay", "{0:N2}")%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="实际转账金额" SortExpression="MoneyTrue">
                <HeaderStyle Width="12%" />
                <ItemStyle HorizontalAlign="right" />
                <ItemTemplate>
                    <%# Eval("MoneyTrue", "{0:N2}")%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="交易状态">
                <HeaderStyle Width="8%" />
                <ItemTemplate>
                    <asp:Label ID="LblStatus" runat="server" />
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="常规操作">
                <HeaderStyle Width="12%" />
                <ItemTemplate>
                    <pe:ExtendedLinkButton IsChecked="true" OperateCode="PaymentLogManage" ID="LbtnDelete"
                        OnClientClick="return confirm('确定要删除此记录吗？');" runat="server" CommandName="Delete"
                        CommandArgument='<%# Eval("PaymentLogID") %>' Text="删除" Visible='<%# (int)Eval("Status") == 1 ? true : false%>' />
                    <a href='<%#string.Format("PaymentLogDetail.aspx?PaymentLogID={0}",Eval("PaymentLogID"))%>'>
                        查看</a>
                    <pe:ExtendedLinkButton IsChecked="true" OperateCode="PaymentLogManage" ID="LbtnStatus"
                        runat="server" CommandName="Status" CommandArgument='<%# Eval("PaymentLogID") %>'
                        Visible='<%# (int)Eval("Status") == 1 ? true : false%>' Text="成功" />
                </ItemTemplate>
            </pe:TemplateField>
        </Columns>
    </pe:ExtendedGridView>
    <asp:ObjectDataSource ID="OdsPaymentLog" runat="server" SelectCountMethod="GetTotalOfPaymentLog"
        SelectMethod="GetList" TypeName="EasyOne.Accessories.PaymentLog" DeleteMethod="Delete"
        EnablePaging="true" StartRowIndexParameterName="startRowIndexId" MaximumRowsParameterName="maxNumberRows">
        <DeleteParameters>
            <asp:Parameter Name="paymentLogId" Type="String" />
        </DeleteParameters>
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="0" Name="searchType" QueryStringField="SearchType"
                Type="String" />
            <asp:QueryStringParameter DefaultValue="0" Name="field" QueryStringField="Field"
                Type="String" />
            <asp:QueryStringParameter DefaultValue="" Name="keyword" QueryStringField="KeyWord"
                Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <br />
    <pe:ExtendedButton IsChecked="true" OperateCode="PaymentLogManage" ID="BtnDelete"
        runat="server" Text="删除选中的记录" OnClick="BtnDelete_Click" OnClientClick="return batchconfirm('确定要删除记录吗？');" />
    <br />
    <br />
    对于一些交易未成功的支付记录，可以删除一定时间段前的记录以加快速度。
    <br />
    <table width="100%" cellpadding="5" cellspacing="0" class="border">
        <tr class="tdbg">
            <td align="right" style="width: 10%;">
                时间范围：
            </td>
            <td align="left" style="width: 55%;">
                <asp:RadioButtonList ID="RadlDatepartType" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="0" Selected="True">10天前</asp:ListItem>
                    <asp:ListItem Value="1">1个月前</asp:ListItem>
                    <asp:ListItem Value="2">2个月前</asp:ListItem>
                    <asp:ListItem Value="3">3个月前</asp:ListItem>
                    <asp:ListItem Value="4">6个月前</asp:ListItem>
                    <asp:ListItem Value="5">1年前</asp:ListItem>
                </asp:RadioButtonList></td>
            <td align="left">
                <asp:Button ID="BtnBatchDelete" runat="server" OnClientClick="return confirm('确实要删除有关记录吗？')"
                    Text="删除" OnClick="BtnBatchDelete_Click" CausesValidation="False" /></td>
        </tr>
    </table>
</asp:Content>
