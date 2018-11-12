<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.User.CardsManage" Title="充值卡管理" Codebehind="CardsManage.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <pe:ExtendedGridView ID="EgvCards" runat="server" AllowPaging="True" AutoGenerateColumns="False"
        AutoGenerateCheckBoxColumn="True" DataKeyNames="CardId" ItemName="充值卡" ItemUnit="个"
        DataSourceID="OdsCards" OnRowDataBound="EgvCards_RowDataBound" OnRowDeleting="EgvCards_RowDeleting"
        CheckBoxFieldHeaderWidth="3%" SerialText="" OnDataBound="EgvCards_DataBound"
        RowDblclickBoundField="CardId" 
        RowDblclickUrl="CardModify.aspx?CardId={$Field}">
        <Columns>
            <pe:TemplateField HeaderText="类型" SortExpression="CardType" />
            <pe:TemplateField HeaderText="卡号">
                <ItemTemplate>
                    <a href='<%# Eval("CardId", "CardShow.aspx?CardId={0}") %>'>
                        <%# Eval("CardNum") %>
                    </a>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:BoundField HeaderText="面值" DataField="Money" DataFormatString="{0:N0}元" HtmlEncode="False"
                SortExpression="Money" />
            <pe:TemplateField HeaderText="点数" SortExpression="ValidNum" />
            <pe:BoundField HeaderText="截止日期" DataField="EndDate" DataFormatString="{0:yyyy-MM-dd}"
                HtmlEncode="False" SortExpression="EndDate" />
            <pe:TemplateField HeaderText="所属商品" SortExpression="ProductName" />
            <pe:TemplateField HeaderText="状态" />
            <pe:TemplateField HeaderText="使用者">
                <ItemTemplate>
                    <a href='<%#Server.UrlEncode( Eval("UserName", "UserShow.aspx?UserName={0}")) %>'>
                        <%# Eval("UserName") %>
                    </a>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:BoundField HeaderText="充值时间" DataField="UseTime" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"
                HtmlEncode="False" SortExpression="UseTime" />
            <pe:TemplateField HeaderText="代理商">
                <ItemTemplate>
                    <a href='<%# Eval("AgentName", "CardsManage.aspx?AgentName={0}") %>'>
                        <%# Eval("AgentName") %>
                    </a>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <%--<a href='<%# Eval("CardId", "CardModify.aspx?CardId={0}") %>'></a>--%>
                    <%--  <asp:LinkButton  ID="LBtnModify" runat="server" CommandName="Modify" Text="修改"/>--%>
                    <asp:HyperLink Text="修改" runat="server" ID="HlnkModify" NavigateUrl='<%# Eval("CardId", "CardModify.aspx?CardId={0}") %>' />
                    <asp:LinkButton ID="LBtnDelete" runat="server" OnClientClick="return confirm('确定要删除此充值卡吗？')"
                        CommandName="Delete">删除</asp:LinkButton>
                </ItemTemplate>
            </pe:TemplateField>
        </Columns>
    </pe:ExtendedGridView>
    <asp:ObjectDataSource ID="OdsCards" runat="server" SelectMethod="GetCardList" TypeName="EasyOne.Accessories.Cards"
        EnablePaging="True" MaximumRowsParameterName="maxiNumRows" SelectCountMethod="GetTotalofCards"
        StartRowIndexParameterName="startRowIndexId" DeleteMethod="DelCard">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="-1" Name="cardType" QueryStringField="CardType"
                Type="String" />
            <asp:QueryStringParameter DefaultValue="-1" Name="cardStatus" QueryStringField="CardStatus"
                Type="String" />
            <asp:QueryStringParameter DefaultValue="1" Name="field" QueryStringField="Field"
                Type="String" />
            <asp:QueryStringParameter DefaultValue="" Name="keyword" QueryStringField="Keyword"
                Type="String" />
            <asp:QueryStringParameter Name="agentName" QueryStringField="AgentName" Type="String" />
        </SelectParameters>
        <DeleteParameters>
            <asp:ControlParameter ControlID="EgvCards" Name="cardId" PropertyName="SelectedValue"
                Type="Int32" />
        </DeleteParameters>
    </asp:ObjectDataSource>
    <br />
    <asp:Button ID="BtnDel" runat="server" OnClick="BtnDel_Click" Text="删除选中的充值卡" OnClientClick="return confirm('确定要删除指定的充值卡吗？')" />
    <asp:Button ID="BtnExportExcel" runat="server" OnClick="BtnExportExcel_Click" Text="导出到EXCEL" />
    <br />
    <br />
    <table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="border">
        <tr>
            <td class="spacingtitle" colspan="2" align="center">
                <strong>相关说明</strong></td>
        </tr>
        <tr class="tdbg">
            <td>
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                <ajaxToolkit:TabContainer runat="server" ID="Tabs" Height="150px">
                    <ajaxToolkit:TabPanel runat="Server" ID="TabPanel" HeaderText="充值卡说明">
                        <ContentTemplate>
                            <ul>
                                <li>通过商店销售的充值卡有四种状态：未售出、已售出、已使用、已失效</li>
                                <li><span style="color: blue">不通过商店销售</span>的充值卡有三种状态：未使用、已使用、已失效</li>
                                <li>已经售出或已经使用过的充值卡将不能修改和删除</li>
                            </ul>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                </ajaxToolkit:TabContainer>
            </td>
        </tr>
    </table>
</asp:Content>
