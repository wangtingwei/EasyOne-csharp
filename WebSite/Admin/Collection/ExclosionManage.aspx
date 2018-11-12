<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Collection.ExclosionManage"
    MasterPageFile="~/Admin/MasterPage.master" Title="采集排除管理" Codebehind="ExclosionManage.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <pe:ExtendedGridView ID="EgvExclosion" runat="server" AllowPaging="True" AutoGenerateColumns="False"
        DataKeyNames="ExclosionID" DataSourceID="OdsExclosion" ItemName="记录" ItemUnit="条"
        CheckBoxFieldHeaderWidth="3%" SerialText="" AutoGenerateCheckBoxColumn="True" OnRowDataBound="EgvExclosion_RowDataBound"
        RowDblclickBoundField="ExclosionID" RowDblclickUrl="Exclosion.aspx?Action=Modify&ExclosionID={$Field}">
        <Columns>
            <pe:BoundField DataField="ExclosionID" HeaderText="ID" SortExpression="ID">
                <HeaderStyle Width="5%" />
            </pe:BoundField>
            <pe:BoundField DataField="ExclosionName" HeaderText="排除名称" SortExpression="ExclosionName">
                <HeaderStyle Width="30%" />
            </pe:BoundField>
            <pe:TemplateField HeaderText="排除类型" SortExpression="ExclosionType">
                <HeaderStyle Width="10%" />
                <ItemTemplate>
                    <asp:Label ID="LblExclosionType" runat="server" Text=""></asp:Label>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <a href="Exclosion.aspx?Action=Modify&ExclosionID=<%#Eval("ExclosionID")%>">
                        修改</a> <a href="ExclosionManage.aspx?Action=Delete&ExclosionID=<%#Eval("ExclosionID")%>"
                            onclick="return confirm('是否删除该采集排除？');">删除</a>
                </ItemTemplate>
                <HeaderStyle Width="55%" />
            </pe:TemplateField>
        </Columns>
    </pe:ExtendedGridView>
    <br />
        <input id="ChkAll" onclick="javascript:CheckAll(this);" type="checkbox" />选中本页显示的所有项目
    &nbsp;&nbsp;<asp:Button ID="BtnBatchDelete" Text="批量删除选定采集排除项目" OnClientClick="return batchconfirm('确实要删除选中的采集排除么？')"
        runat="server" OnClick="BtnBatchDelete_Click" />&nbsp;&nbsp;
    <br />
    <asp:ObjectDataSource ID="OdsExclosion" runat="server" SelectMethod="GetList" SelectCountMethod="GetCountNumber"
        TypeName="EasyOne.Collection.CollectionExclosion" EnablePaging="True" StartRowIndexParameterName="startRowIndexId"
        MaximumRowsParameterName="maxNumberRows" OldValuesParameterFormatString="original_{0}">
    </asp:ObjectDataSource>
</asp:Content>

