<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Accessories.InsideLinkManage"
    MasterPageFile="~/Admin/MasterPage.master" Title="站内链接管理"
    ValidateRequest="false" Codebehind="InsideLinkManage.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server"
        AdditionalNode="所有站内链接" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <pe:ExtendedGridView ID="EgvInsideLink" runat="server" AllowPaging="True" AutoGenerateColumns="False"
        AutoGenerateCheckBoxColumn="True" DataKeyNames="ItemID" DataSourceID="OdsInsideLink"
        ItemName="项目" ItemUnit="个" OnRowDataBound="EgvInsideLink_RowDataBound"
        RowDblclickBoundField="ItemID" RowDblclickUrl="InsideLink.aspx?Action=Modify&ItemID={$Field}">
        <Columns>
            <pe:BoundField DataField="ItemID" HeaderText="ID">
                <HeaderStyle Width="8%" />
            </pe:BoundField>
            <pe:BoundField DataField="SourceWord" HeaderText="链接目标" SortExpression="SourceWord">
                <HeaderStyle Width="20%" />
            </pe:BoundField>
            <pe:TemplateField HeaderText="链接地址" SortExpression="TargetWord">
                <ItemTemplate>
                    <a href='<%# (string)Eval("TargetWord") %>' target="_blank">
                        <%# (string)Eval("TargetWord") %>
                    </a>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:BoundField DataField="Priority" HeaderText="优先级" SortExpression="Priority">
                <HeaderStyle Width="8%" />
            </pe:BoundField>
            <pe:BoundField DataField="ReplaceTimes" HeaderText="替换次数" SortExpression="ReplaceTimes">
                <HeaderStyle Width="10%" />
            </pe:BoundField>
            <pe:TemplateField HeaderText="打开方式" SortExpression="OpenType">
                <HeaderStyle Width="10%" />
                <ItemTemplate>
                    <%# (bool)Eval("OpenType") ? "原窗体" : "新窗体"%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="状态" SortExpression="IsEnabled">
                <HeaderStyle Width="5%" />
                <ItemTemplate>
                    <%# (bool)Eval("IsEnabled") == true ? "<b>√</b>" : "<span style=\"color:red;\"><b>×</b></span>"%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="操作">
                <HeaderStyle Width="10%" />
                <ItemTemplate>
                    <pe:ExtendedAnchor ID="EahInsideLinkEnabled" IsChecked="true" OperateCode="InsideLinkManage"
                        href='<%# AppendSecurityCode("InsideLinkManage.aspx?Action=runInsideLink&ItemID=" + Eval("ItemID"))%>'
                        runat="server">启用</pe:ExtendedAnchor>
                    <pe:ExtendedAnchor ID="EahInsideLinkDisable" IsChecked="true" OperateCode="InsideLinkManage"
                        href='<%# AppendSecurityCode("InsideLinkManage.aspx?Action=disInsideLink&ItemID=" + Eval("ItemID"))%>'
                        runat="server">禁用</pe:ExtendedAnchor>
                    <pe:ExtendedAnchor ID="EahInsideLinkModify" IsChecked="true" OperateCode="InsideLinkManage"
                        href='<%# "InsideLink.aspx?Action=Modify&ItemID=" + Eval("ItemID")%>' runat="server">修改</pe:ExtendedAnchor>
                    <pe:ExtendedAnchor ID="EahInsideLinkDelete" IsChecked="true" OperateCode="InsideLinkManage"
                        href='<%# AppendSecurityCode("InsideLinkManage.aspx?Action=Delete&ItemID=" + Eval("ItemID"))%>'
                        onclick="return confirm('是否删除该站内链接？');" runat="server">删除</pe:ExtendedAnchor>
                </ItemTemplate>
            </pe:TemplateField>
        </Columns>
    </pe:ExtendedGridView>
    <asp:ObjectDataSource ID="OdsInsideLink" runat="server" SelectMethod="GetInsideList"
        TypeName="EasyOne.Accessories.WordReplace" DeleteMethod="Delete" EnablePaging="True"
        StartRowIndexParameterName="startRowIndexId" MaximumRowsParameterName="maxNumberRows"
        OldValuesParameterFormatString="original_{0}">
        <DeleteParameters>
            <asp:Parameter Name="id" Type="String" />
        </DeleteParameters>
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="" Name="keyword" QueryStringField="KeyWord"
                Type="String" />
            <asp:QueryStringParameter DefaultValue="0" Name="listType" QueryStringField="ListType"
                Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:HiddenField ID="HdnlistType" runat="server" Value="0" />
    <br />
    <input id="ChkAll" onclick="javascript:CheckAll(this);" type="checkbox" /><label
        for="ChkAll">选中本页显示的所有站内链接</label>
    &nbsp;&nbsp;
    <pe:ExtendedButton ID="EBtnBatchDelete" Text="删除选中的站内链接" IsChecked="true" OperateCode="InsideLinkManage"
        OnClientClick="return batchconfirm('是否要删除站内链接？');" OnClick="EBtnBatchDelete_Click"
        CausesValidation="False" runat="server" />
    <pe:ExtendedButton ID="EBtnBatchEnable" Text="启用选中的站内链接" IsChecked="true" OperateCode="InsideLinkManage"
        OnClientClick="return batchconfirm('是否要启用站内链接？');" OnClick="EBtnBatchEnable_Click"
        CausesValidation="False" runat="server" />
    <pe:ExtendedButton ID="EBtnBatchDisable" Text="禁用选中的站内链接" IsChecked="true" OperateCode="InsideLinkManage"
        OnClientClick="return batchconfirm('是否要禁用站内链接？');" OnClick="EBtnBatchDisable_Click"
        CausesValidation="False" runat="server" />
    <br />
    <br />
    <br />
</asp:Content>
