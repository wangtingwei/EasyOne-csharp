<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Contents.ContentManageUI" Title="内容管理" Codebehind="ContentManage.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <asp:ScriptManager ID="ScriptManageContent" runat="server" />
    <pe:ContentManageNavigation ID="Cmn" runat="server" />
    <div style="padding-top: 5px;">
        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td style="width: 80px" align="left" class="tdbg">
                    <b>内容选项：</b>
                </td>
                <td class="tdbg">
                    <asp:RadioButtonList ID="RadlContent" runat="server" RepeatDirection="Horizontal"
                        AutoPostBack="true" RepeatLayout="Flow" OnSelectedIndexChanged="RadlContent_SelectedIndexChanged">
                        <asp:ListItem Value="100">所有内容</asp:ListItem>
                        <asp:ListItem Value="-1">草稿</asp:ListItem>
                        <asp:ListItem Value="101">待审核</asp:ListItem>
                        <asp:ListItem Value="99">已审核</asp:ListItem>
                        <asp:ListItem Value="-2">退稿</asp:ListItem>
                    </asp:RadioButtonList>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="DrpSearchType" runat="server">
                        <asp:ListItem Value="ID" Text="ID" />
                        <asp:ListItem Value="Title" Text="内容标题" />
                        <asp:ListItem Value="Inputer" Text="录入者" />
                    </asp:DropDownList>
                    <asp:TextBox ID="TxtSearchKeyword" runat="server" />
                    <asp:Button ID="BtnSearch" runat="server" Text="搜索" OnClick="BtnSearch_Click" />
                    <pe:ExtendedLabel ID="LblContentAdvancedSearch" runat="server" HtmlEncode="false" Text="<a href='ContentAdvancedSearch.aspx'>高级搜索</a>" Visible ="false" ></pe:ExtendedLabel>
                </td>
            </tr>
        </table>
    </div>
    <div style="padding-top: 15px;">
        <table style="width: 100%; margin: 0 auto;" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
                </td>
                <td align="right">
                    排序方式：
                    <asp:DropDownList ID="DropRescentQuery" AutoPostBack="true" OnSelectedIndexChanged="DropSelectedIndex_Changed"
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
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <pe:ExtendedGridView ID="EgvContent" runat="server" DataSourceID="OdsContents" SerialText=""
        AutoGenerateCheckBoxColumn="True" AutoGenerateColumns="False" AllowPaging="True"
        OnRowDataBound="EgvContent_RowDataBound" OnRowCommand="EgvContent_RowCommand"
        DataKeyNames="GeneralId" CheckBoxFieldHeaderWidth="3%" IsHoldState="True">
        <Columns>
            <asp:BoundField DataField="GeneralId" HeaderText="ID" 
                SortExpression="GeneralId">
                <HeaderStyle Width="5%" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="标题" SortExpression="Title">
                <ItemTemplate>
                    <pe:LinkImage ID="LinkImageModel" runat="server">
                        <pe:ExtendedHyperLink ID="LnkNodeLink" runat="server" />
                        <asp:HyperLink ID="HypTitle" runat="server" />
                    </pe:LinkImage>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:BoundField DataField="Inputer" HeaderText="录入者" SortExpression="Inputer">
                <HeaderStyle Width="6%" />
            </asp:BoundField>
            <asp:BoundField DataField="Hits" HeaderText="点击数" SortExpression="Hits">
                <HeaderStyle Width="6%" />
            </asp:BoundField>
            <asp:BoundField DataField="EliteLevel" HeaderText="推荐级别" 
                SortExpression="EliteLevel">
                <HeaderStyle Width="8%" />
            </asp:BoundField>
            <asp:BoundField DataField="Priority" HeaderText="优先级" SortExpression="Priority">
                <HeaderStyle Width="6%" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="状态" SortExpression="Status">
                <HeaderStyle Width="15%" />
                <ItemTemplate>
                    <asp:Label ID="lStatus" Text='<%# GetStatusShow(Eval("Status").ToString())%>' runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="已生成" SortExpression="Status">
                <ItemTemplate>
                    <pe:ExtendedLabel ID="LblIsCreateHtml" runat="server" HtmlEncode="false">
                    &nbsp;
                    </pe:ExtendedLabel>
                </ItemTemplate>
                <HeaderStyle Width="6%" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="常规管理操作" SortExpression="Disabled">
                <ItemTemplate>
                    <asp:HyperLink ID="ContentModify" runat="server" />
                    <asp:LinkButton ID="ContentDelete" runat="server" />
                </ItemTemplate>
                <HeaderStyle Width="12%" />
            </asp:TemplateField>
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
    <input id="ChkAll" onclick="javascript:CheckAll(this);" type="checkbox" /><label
        for="ChkAll">选中本页显示的所有项目</label>
    &nbsp;&nbsp;
    <asp:Button ID="EBtnBatchDelete" Text="批量删除" OnClientClick="return batchconfirm('确定要删除选中的项目吗？本操作把选中的信息移到回收站中。必要时您可从回收站中恢复！');"
        OnClick="EBtnBatchDelete_Click" CausesValidation="False" runat="server" />&nbsp;&nbsp;
    <asp:Button ID="EBtnBatchSet" Text="批量设置" runat="server" OnClick="EBtnBatchSet_Click" />
    <asp:Button ID="EBtnBatchMove" Text="批量移动" runat="server" OnClick="EBtnBatchMove_Click" />
    <asp:Button ID="EBtnPass" Text="审核通过" runat="server" OnClick="EBtnPass_Click" />
    <asp:Button ID="EBtnCancelPass" Text="取消审核" runat="server" OnClick="EBtnCancelPass_Click" />
    <asp:Button ID="BatchSpecialSet" Text="添加到专题" runat="server" OnClick="EBtnBatchSpecialSet_Click" />
    <asp:Button ID="BatchNodeSet" Text="添加到其他节点" runat="server" OnClick="BatchNodeSet_Click" />
    <asp:Button ID="BtnArchiving" Text="归档内容" runat="server" OnClick="BtnArchiving_Click" />
</asp:Content>
