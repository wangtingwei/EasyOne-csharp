<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Contents.ContentArchivingManage" Codebehind="ContentArchivingManage.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <asp:ScriptManager ID="ScriptManageContent" runat="server">
    </asp:ScriptManager>
    <pe:ContentManageNavigation ID="Cmn" runat="server" />
    <br />
    <table style="width: 100%; margin: 0 auto;" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
            </td>
            <td align="right">
                排序方式：
                <asp:DropDownList ID="DropRescentQuery" AutoPostBack="true" OnSelectedIndexChanged="DropRescentQuerySelectedIndex_Changed"
                    runat="server">
                    <asp:ListItem Value="-1">按ID降序</asp:ListItem>
                    <asp:ListItem Value="-2">按ID升序</asp:ListItem>
                    <asp:ListItem Value="1">按推荐级别降序</asp:ListItem>
                    <asp:ListItem Value="2">按推荐级别升序</asp:ListItem>
                    <asp:ListItem Value="3">按优先级别降序</asp:ListItem>
                    <asp:ListItem Value="4">按优先级别升序</asp:ListItem>
                    <asp:ListItem Value="5">按日点击数降序</asp:ListItem>
                    <asp:ListItem Value="6">按日点击数升序</asp:ListItem>
                    <asp:ListItem Value="7">按周点击数降序</asp:ListItem>
                    <asp:ListItem Value="8">按周点击数升序</asp:ListItem>
                    <asp:ListItem Value="9">按月点击数降序</asp:ListItem>
                    <asp:ListItem Value="10">按月点击数升序</asp:ListItem>
                    <asp:ListItem Value="11">按总点击数降序</asp:ListItem>
                    <asp:ListItem Value="12">按总点击数升序</asp:ListItem>
                </asp:DropDownList>
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <pe:ExtendedGridView ID="EgvContent" runat="server" DataSourceID="OdsContents" SerialText=""
        AutoGenerateCheckBoxColumn="True" AutoGenerateColumns="False" AllowPaging="True"
        OnRowDataBound="EgvContent_RowDataBound" OnRowCommand="EgvContent_RowCommand"
        DataKeyNames="GeneralId" CheckBoxFieldHeaderWidth="3%">
        <Columns>
            <pe:BoundField DataField="GeneralId" HeaderText="ID" SortExpression="GeneralId">
                <HeaderStyle Width="5%" />
            </pe:BoundField>
            <pe:TemplateField HeaderText="标题" SortExpression="Title">
                <ItemStyle HorizontalAlign="Left" />
                <ItemTemplate>
                    <pe:LinkImage ID="LinkImageModel" runat="server">
                        <pe:ExtendedHyperLink ID="LnkNodeLink" runat="server" />
                        <asp:HyperLink ID="HypTitle" runat="server" />
                    </pe:LinkImage>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:BoundField DataField="Inputer" HeaderText="录入者" SortExpression="Inputer">
                <HeaderStyle Width="6%" />
            </pe:BoundField>
            <pe:BoundField DataField="Hits" HeaderText="点击数" SortExpression="Hits">
                <HeaderStyle Width="6%" />
            </pe:BoundField>
            <pe:BoundField DataField="EliteLevel" HeaderText="推荐级别" SortExpression="EliteLevel">
                <HeaderStyle Width="8%" />
            </pe:BoundField>
            <pe:BoundField DataField="Priority" HeaderText="优先级" SortExpression="Priority">
                <HeaderStyle Width="6%" />
            </pe:BoundField>
            <pe:TemplateField HeaderText="状态" SortExpression="Status">
                <HeaderStyle Width="15%" />
                <ItemTemplate>
                    <%# GetStatusShow(Eval("Status").ToString())%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="已生成" SortExpression="Status">
                <HeaderStyle Width="6%" />
                <ItemTemplate>
                    <pe:ExtendedLabel ID="LblIsCreateHtml" HtmlEncode="false" runat="server"></pe:ExtendedLabel>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="常规管理操作" SortExpression="Disabled">
                <HeaderStyle Width="12%" />
                <ItemTemplate>
                    <asp:LinkButton ID="CancelArchiving" runat="server" />
                    <asp:LinkButton ID="ContentDelete" runat="server" />
                </ItemTemplate>
            </pe:TemplateField>
        </Columns>
    </pe:ExtendedGridView>
    <asp:HiddenField ID="HdnListType" runat="server" Value="-1" />
    <asp:HiddenField ID="HdnStatus" runat="server" Value="100" />
    <asp:HiddenField ID="HdnSearchType" runat="server" Value="" />
    <asp:HiddenField ID="HdnSearchKeyword" runat="server" Value="" />
    <asp:ObjectDataSource ID="OdsContents" runat="server" SelectMethod="GetSearchContentList"
        TypeName="EasyOne.Contents.ContentManage" EnablePaging="True" MaximumRowsParameterName="maxNumberRows"
        StartRowIndexParameterName="startRowIndexId" SelectCountMethod="GetTotalOfCommonModelInfo">
        <SelectParameters>
            <asp:QueryStringParameter Name="nodeId" QueryStringField="NodeID" Type="Int32" />
            <asp:ControlParameter ControlID="HdnListType" Type="Int32" Name="sortType" PropertyName="Value" />
            <asp:ControlParameter ControlID="HdnStatus" Type="Int32" Name="status" PropertyName="Value" />
            <asp:ControlParameter ControlID="HdnSearchType" Type="String" Name="searchType" PropertyName="Value" />
            <asp:ControlParameter ControlID="HdnSearchKeyword" Type="String" Name="keyword" PropertyName="Value" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <br />
    <input id="ChkAll" onclick="javascript:CheckAll(this);" type="checkbox" />
    <label for="ChkAll">
        选中本页显示的所有项目</label>
    &nbsp;&nbsp;
    <asp:Button ID="EBtnBatchDelete" Text="批量删除" OnClientClick="return batchconfirm('确定要删除选中的项目吗？本操作把选中的信息移到回收站中。必要时您可从回收站中恢复！');"
        OnClick="EBtnBatchDelete_Click" CausesValidation="False" runat="server" />&nbsp;&nbsp;
    <asp:Button ID="BtnCancel" Text="取消归档" runat="server"  OnClientClick="return batchconfirm('确定要取消归档选中的项目吗？');" OnClick="BtnCancel_Click" />
    &nbsp;&nbsp;
    <asp:Button ID="BtnEmpty" Text="取消所有归档" runat="server" OnClick="BtnEmpty_Click" OnClientClick="return confirm('确定要清空归档吗？');"/>
</asp:Content>

