<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Contents.ContentSignIn" Codebehind="ContentSignIn.aspx.cs" %>

<%@ Import Namespace="EasyOne.Contents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <asp:ScriptManager ID="ScriptManageContent" runat="server">
    </asp:ScriptManager>
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
                    <asp:RadioButtonList ID="RadlListType" runat="server" RepeatDirection="Horizontal"
                        AutoPostBack="true" RepeatLayout="Flow" OnSelectedIndexChanged="RadlListType_SelectedIndexChanged">
                        <asp:ListItem Value="0">所有签收文档</asp:ListItem>
                        <asp:ListItem Value="1">公众文档</asp:ListItem>
                        <asp:ListItem Value="2">专属文档</asp:ListItem>
                    </asp:RadioButtonList>&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="DrpSearchType" runat="server">
                        <asp:ListItem Value="Title" Text="内容标题" />
                        <asp:ListItem Value="Inputer" Text="录入者" />
                    </asp:DropDownList>
                    <asp:TextBox ID="TxtSearchKeyword" runat="server" />
                    <asp:Button ID="BtnSearch" runat="server" Text="搜索" OnClick="BtnSearch_Click" />
                </td>
            </tr>
        </table>
    </div>
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
    <pe:ExtendedGridView ID="EgvContentSignin" runat="server" DataSourceID="OdsContents"
        SerialText="" AutoGenerateCheckBoxColumn="True" AutoGenerateColumns="False" AllowPaging="True"
        OnRowDataBound="EgvContentSignin_RowDataBound" OnRowCommand="EgvContentSignin_RowCommand"
        DataKeyNames="GeneralId">
        <Columns>
            <pe:BoundField DataField="GeneralId" HeaderText="ID" SortExpression="GeneralId">
                <HeaderStyle Width="5%" />
            </pe:BoundField>
            <pe:TemplateField HeaderText="标题" SortExpression="Title">
                <HeaderStyle Width="30%" />
                <ItemStyle HorizontalAlign="Left" />
                <ItemTemplate>
                    <pe:LinkImage ID="LinkImageModel" runat="server">
                        <pe:ExtendedHyperLink ID="LnkNodeLink" runat="server" />
                        <asp:HyperLink ID="HypTitle" runat="server" />
                        <pe:ExtendedLabel HtmlEncode="false" ID="LblSigninStatus" runat="Server"></pe:ExtendedLabel>
                    </pe:LinkImage>
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
                    <%# ContentManage.GetStatusShow(Eval("Status").ToString())%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="已生成" SortExpression="Status">
                <HeaderStyle Width="8%" />
                <ItemTemplate>
                    <pe:ExtendedLabel HtmlEncode="false" ID="LblIsCreateHtml" runat="server"></pe:ExtendedLabel>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="常规管理操作" SortExpression="Disabled">
                <HeaderStyle Width="15%" />
                <ItemTemplate>
                    <asp:HyperLink ID="EahContentModify" NavigateUrl=''
                        runat="server">修改</asp:HyperLink>
                    <asp:LinkButton ID="ELbtnDelete" Text="删除" OnClientClick="if(!this.disabled) return confirm('确实要删除此信息吗？删除后你还可以从回收站中还原！')"
                        runat="server" CommandArgument='<%# Eval("GeneralId")%>' CommandName="DeleteContent" />
                </ItemTemplate>
            </pe:TemplateField>
        </Columns>
    </pe:ExtendedGridView>
    <asp:HiddenField ID="HdnListType" runat="server" Value="0" />
    <asp:HiddenField ID="HdnSortType" runat="server" Value="-1" />
    <asp:HiddenField ID="HdnStatus" runat="server" Value="100" />
    <asp:HiddenField ID="HdnSearchType" runat="server" Value="" />
    <asp:HiddenField ID="HdnSearchKeyword" runat="server" Value="" />
    <asp:ObjectDataSource ID="OdsContents" runat="server" SelectMethod="GetCommonModelInfoListBySignInType"
        TypeName="EasyOne.Contents.ContentManage" EnablePaging="True" MaximumRowsParameterName="maxNumberRows"
        StartRowIndexParameterName="startRowIndexId" SelectCountMethod="GetTotalOfCommonModelInfoBySignInType">
        <SelectParameters>
            <asp:QueryStringParameter Name="nodeId" QueryStringField="NodeID" Type="Int32" />
            <asp:ControlParameter ControlID="HdnListType" Type="Int32" Name="signInType" PropertyName="Value" />
            <asp:ControlParameter ControlID="HdnSortType" Type="Int32" Name="sortType" PropertyName="Value" />
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
</asp:Content>
