<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.User.Info.Favorites" Codebehind="Favorite.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <pe:UserNavigation Tab="content" ID="UserCenterNavigation" runat="server" />
    <pe:ExtendedSiteMapPath ID="YourPosition" SiteMapProvider="UserMapProvider" runat="server" />
    <form id="form1" runat="server">
        <div>
            <pe:ExtendedGridView ID="EgvFavorite" DataKeyNames="InfoId" runat="server" AllowPaging="True"
                AutoGenerateColumns="False" AutoGenerateCheckBoxColumn="true" ItemName="收藏" SerialText=""
                CheckBoxFieldHeaderWidth="3%" DataSourceID="OdsFavorite">
                <Columns>
                    <pe:BoundField DataField="InfoId" HeaderStyle-Width="6%" HeaderText="ID" SortExpression="InfoId" />
                    <pe:TemplateField>
                        <HeaderTemplate>
                            标题</HeaderTemplate>
                        <ItemTemplate>
                            <a href='<%# BasePath + "Item/" +  Eval("InfoId").ToString() + ".aspx" %>' target="_blank">
                                <%# Eval("Title") %>
                            </a>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" />
                    </pe:TemplateField>
                    <pe:BoundField DataField="FavoriteTime" HeaderStyle-Width="18%" HeaderText="收藏时间"
                        SortExpression="FavoriteTime" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HtmlEncode="false" />
                    <pe:TemplateField HeaderText="操作">
                        <HeaderStyle Width="10%" />
                        <ItemTemplate>
                            <pe:ExtendedNodeAnchor ID="EahContentView" IsChecked="false" href='<%# "Favorite.aspx?Action=Delete&Id=" + Eval("InfoId")%>'
                                runat="server">取消收藏</pe:ExtendedNodeAnchor>
                        </ItemTemplate>
                    </pe:TemplateField>
                </Columns>
            </pe:ExtendedGridView>
            <div style="padding-top: 5px;">
                <table width="100%" cellpadding="5" cellspacing="0" class="border">
                    <tr class="tdbg">
                        <td>
                            <input id="ChkAll" onclick="javascript:CheckAll(this);" type="checkbox" /><label
                                for="ChkAll">选中本页显示的所有项目</label>
                            <asp:Button ID="BtnBatchDelete" runat="server" Text="取消选中的收藏" OnClick="BtnBatchDelete_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <asp:ObjectDataSource ID="OdsFavorite" runat="server" SelectMethod="GetList" SelectCountMethod="GetTotalOfFavorite"
                TypeName="EasyOne.Accessories.Favorite">
                <SelectParameters>
                    <asp:Parameter Name="startRowIndexId" Type="Int32" />
                    <asp:Parameter Name="maxNumberRows" Type="Int32" />
                    <asp:Parameter Name="userId" Type="int32" />
                    <asp:QueryStringParameter DefaultValue="0" Name="nodeId" QueryStringField="NodeId"
                        Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </div>
    </form>
</body>
</html>
