<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Accessories.WordFilterManage"
    MasterPageFile="~/Admin/MasterPage.master" Title="字符过滤管理"
    ValidateRequest="false" Codebehind="WordFilterManage.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <pe:ExtendedGridView ID="EgvWordFilter" runat="server" AllowPaging="True" AutoGenerateColumns="False"
        AutoGenerateCheckBoxColumn="True" DataKeyNames="ItemID" DataSourceID="OdsWordFilter"
        ItemName="项目" ItemUnit="个" OnRowDataBound="EgvWordFilter_RowDataBound"
        RowDblclickBoundField="ItemID" RowDblclickUrl="WordFilter.aspx?Action=Modify&ItemID={$Field}">
        <Columns>
            <pe:BoundField DataField="ItemID" HeaderText="ID">
                <HeaderStyle Width="6%" />
            </pe:BoundField>
            <pe:BoundField DataField="SourceWord" HeaderText="替换目标" SortExpression="SourceWord">
                <HeaderStyle Width="35%" />
                <ItemStyle HorizontalAlign="Left" />
            </pe:BoundField>
            <pe:BoundField DataField="TargetWord" HeaderText="替换内容" SortExpression="TargetWord">
                <ItemStyle HorizontalAlign="Left" />
            </pe:BoundField>
            <pe:BoundField DataField="Priority" HeaderText="优先级" SortExpression="Priority">
                <HeaderStyle Width="6%" />
            </pe:BoundField>
            <pe:TemplateField HeaderText="状态" SortExpression="IsEnabled">
                <HeaderStyle Width="6%" />
                <ItemTemplate>
                    <%# (bool)Eval("IsEnabled") == true ? "<b>√</b>" : "<span style=\"color:red;\"><b>×</b></span>"%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="操作">
                <HeaderStyle Width="12%" />
                <ItemTemplate>
                    <a id="EahWordFilterEnabled" href='<%# AppendSecurityCode("WordFilterManage.aspx?Action=runWordFilter&ItemID=" + Eval("ItemID"))%>'
                        runat="server">启用</a> <a id="EahWordFilterDisable" href='<%# AppendSecurityCode("WordFilterManage.aspx?Action=disWordFilter&ItemID=" + Eval("ItemID"))%>'
                            runat="server">禁用</a> <a id="EahWordFilterModify" href='<%# "WordFilter.aspx?Action=Modify&ItemID=" + Eval("ItemID")%>'
                                runat="server">修改</a> <a id="EahWordFilterDelete" href='<%# AppendSecurityCode("WordFilterManage.aspx?Action=Delete&ItemID=" + Eval("ItemID"))%>'
                                    onclick="return confirm('是否删除该字符过滤？');" runat="server">删除</a>
                </ItemTemplate>
            </pe:TemplateField>
        </Columns>
    </pe:ExtendedGridView>
    <asp:ObjectDataSource ID="OdsWordFilter" runat="server" SelectMethod="GetWordFilterList"
        SelectCountMethod="GetCountNumber" TypeName="EasyOne.Accessories.WordReplace"
        DeleteMethod="Delete" EnablePaging="True" StartRowIndexParameterName="startRowIndexId"
        MaximumRowsParameterName="maxNumberRows" OldValuesParameterFormatString="original_{0}">
        <DeleteParameters>
            <asp:Parameter Name="id" Type="String" />
        </DeleteParameters>
        <SelectParameters>
            <asp:QueryStringParameter Name="keyword" QueryStringField="Keyword" Type="string" />
            <asp:QueryStringParameter Name="listType" QueryStringField="ListType" Type="int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <br />
    <input id="ChkAll" onclick="javascript:CheckAll(this);" type="checkbox" />选中本页显示的所有字符过滤
    &nbsp;&nbsp;
    <asp:Button ID="EBtnBatchDelete" Text="删除选中的字符过滤" OnClientClick="return batchconfirm('是否要删除字符过滤？');"
        OnClick="EBtnBatchDelete_Click" CausesValidation="False" runat="server" />
    <asp:Button ID="EBtnBatchEnable" Text="启用选中的字符过滤" OnClientClick="return batchconfirm('是否要启用字符过滤？');"
        OnClick="EBtnBatchEnable_Click" CausesValidation="False" runat="server" />
    <asp:Button ID="EBtnBatchDisable" Text="禁用选中的字符过滤" OnClientClick="return batchconfirm('是否要禁用字符过滤？');"
        OnClick="EBtnBatchDisable_Click" CausesValidation="False" runat="server" />
</asp:Content>
