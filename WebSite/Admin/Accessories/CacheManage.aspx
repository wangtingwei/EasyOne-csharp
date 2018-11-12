<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Accessories.CacheManage"
    Title="缓存管理" Codebehind="CacheManage.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="server">
    <pe:ExtendedGridView ID="EgvCache" runat="server" DataKeyNames="CacheName" AllowPaging="true"
        DataSourceID="OdsCacheList" AutoGenerateColumns="false" OnRowCommand="EgvCache_RowCommand"
        OnRowDataBound="EgvCache_RowDataBound" RowDblclickBoundField="CacheName" RowDblclickUrl="CacheShow.aspx?CacheKey={$Field}">
        <Columns>
            <pe:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="缓存名称">
                <ItemTemplate>
                    <asp:Label ID="LblCacheKey" runat="server" Text=""></asp:Label>
                </ItemTemplate>
                <HeaderStyle Width="30%" />
            </pe:TemplateField>
            <pe:BoundField DataField="CacheValue" ItemStyle-HorizontalAlign="Left" HeaderText="缓存内容" />
            <pe:TemplateField HeaderText="缓存说明">
                <ItemTemplate>
                    <asp:Label ID="LblCacheIntro" runat="server" Text=""></asp:Label>
                </ItemTemplate>
                <HeaderStyle Width="20%" />
            </pe:TemplateField>
            <pe:TemplateField HeaderText="操作">
                <HeaderStyle Width="10%" />
                <ItemTemplate>
                    <asp:LinkButton ID="LbtnDelete" runat="server" CommandArgument='<%#Eval("CacheName") %>'
                        CommandName="Del" OnClientClick="return confirm('确定要删除此缓存吗？')">删除</asp:LinkButton>
                    <asp:HyperLink ID="LnkCacheShow" NavigateUrl='<%#String.Format("CacheShow.aspx?CacheKey={0}",Server.UrlEncode(Convert.ToString(Eval("CacheName")))) %>'
                        runat="server">查看</asp:HyperLink>
                </ItemTemplate>
            </pe:TemplateField>
        </Columns>
    </pe:ExtendedGridView>
    <br />
    <div style="text-align: center;">
        <asp:Button ID="BtnDelete" runat="server" Text="清除所有缓存" OnClick="BtnDelete_Click" /> <asp:Button ID="BtnDeleteNode" runat="server" Text="清除节点设置缓存" OnClick="BtnDeleteNode_Click" /> 
        <asp:Button ID="BtnDeleteModel" runat="server" Text="清除模型缓存" OnClick="BtnDeleteModel_Click" /> <asp:Button ID="BtnDeleteLabel" runat="server" Text="清除模板标签缓存" OnClick="BtnDeleteLabel_Click" />
        <asp:Button ID="BtnDeletePageCategory" runat="server" Text="清除节点页缓存" OnClick="BtnDeletePageCategory_Click" /></div>
    <asp:ObjectDataSource ID="OdsCacheList" runat="server" SelectMethod="AcquireCurrentCacheList"
        TypeName="EasyOne.Components.SiteCache" >
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="0" Name="cacheType" QueryStringField="CacheType"
                Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
