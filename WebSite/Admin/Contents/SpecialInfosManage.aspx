<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Contents.SpecialInfosManage"
    Title="内容管理" Codebehind="SpecialInfosManage.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <asp:ScriptManager ID="ScriptManageContent" runat="server">
    </asp:ScriptManager>
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <pe:ExtendedGridView ID="EgvSpecialInfos" runat="server" DataSourceID="OdsSpecialInfos"
        SerialText="" AutoGenerateCheckBoxColumn="True" AutoGenerateColumns="False" AllowPaging="True"
        OnRowCommand="EgvSpecialInfos_RowCommand" DataKeyNames="SpecialInfoId" OnRowDataBound="EgvSpecialInfos_RowDataBound">
        <Columns>
            <pe:BoundField DataField="SpecialInfoId" HeaderText="ID">
                <HeaderStyle Width="5%" />
            </pe:BoundField>
            <pe:TemplateField HeaderText="标题" SortExpression="Title">
                <HeaderStyle Width="30%" />
                <ItemStyle HorizontalAlign="Left" />
                <ItemTemplate>
                    <pe:LinkImage ID="LinkImageModel" runat="server">
                    <asp:HyperLink ID="HypTitle" runat="server" /></pe:LinkImage>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:BoundField DataField="Inputer" HeaderText="录入者" SortExpression="Inputer">
                <HeaderStyle Width="8%" />
            </pe:BoundField>
            <pe:BoundField DataField="Hits" HeaderText="点击数" SortExpression="Hits">
                <HeaderStyle Width="8%" />
            </pe:BoundField>
            <pe:BoundField DataField="EliteLevel" HeaderText="推荐级别" SortExpression="EliteLevel">
                <HeaderStyle Width="10%" />
            </pe:BoundField>
            <pe:BoundField DataField="Priority" HeaderText="优先级" SortExpression="Priority">
                <HeaderStyle Width="8%" />
            </pe:BoundField>
            <pe:TemplateField HeaderText="状态" SortExpression="Status">
                <HeaderStyle Width="8%" />
                <ItemTemplate>
                    <%# GetStatusShow(Eval("Status").ToString())%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="已生成">
                <HeaderStyle Width="8%" />
                <ItemTemplate>
                    <pe:ExtendedLabel HtmlEncode="false" ID="LblIsCreateHtml" runat="Server"></pe:ExtendedLabel>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="常规管理操作" SortExpression="Disabled">
                <HeaderStyle Width="15%" />
                <ItemTemplate>
                    <asp:LinkButton runat="server" ID="DeleteSpecialInfoById" CommandName="DeleteSpecialInfoById"
                        CommandArgument='<%# Eval("SpecialInfoID")%>' OnClientClick="if(!this.disabled) return confirm('确定要删除该专题信息吗？');">删除</asp:LinkButton>
                </ItemTemplate>
            </pe:TemplateField>
        </Columns>
    </pe:ExtendedGridView>
    <asp:HiddenField ID="HdnListType" runat="server" Value="-1" />
    <asp:HiddenField ID="HdnStatus" runat="server" Value="100" />
    <asp:ObjectDataSource ID="OdsSpecialInfos" runat="server" SelectMethod="GetCommonModelInfoListBySpecialIdOrSpecialCategoryId"
        TypeName="EasyOne.Contents.ContentManage" EnablePaging="True" MaximumRowsParameterName="maxNumberRows"
        StartRowIndexParameterName="startRowIndexId" SelectCountMethod="GetTotalOfCommonModelInfoBySpecialIdOrSpecialCategoryId">
        <SelectParameters>
            <asp:QueryStringParameter Name="specialId" QueryStringField="SpecialId" Type="Int32" />
            <asp:QueryStringParameter Name="specialCategoryId" QueryStringField="SpecialCategoryId"
                Type="Int32" />
            <asp:ControlParameter ControlID="HdnListType" Type="Int32" Name="sortType" PropertyName="Value" />
            <asp:ControlParameter ControlID="HdnStatus" Type="Int32" Name="status" PropertyName="Value" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <br />
    <input id="ChkAll" onclick="javascript:CheckAll(this);" type="checkbox" />选中本页显示的所有项目
    &nbsp;&nbsp;
    <asp:Button ID="EBtnDelete" Text="从所属专题中删除" OnClick="EBtnDelete_Click" runat="server"
        OnClientClick="return batchconfirm('是否将选中的专题信息从所属专题中删除？');" CausesValidation="False" />
    <asp:Button ID="EBtnAddToSpecial" Text="添加到其它专题" OnClick="EBtnAddToSpecial_Click"
        runat="server" />
    <asp:Button ID="EBtnMoveToOtherSpecial" Text="移动到另一专题中" OnClick="EBtnMoveToOtherSpecial_Click"
        runat="server" />
</asp:Content>
