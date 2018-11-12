<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Collection.CollectionMain"
    MasterPageFile="~/Admin/MasterPage.master" Title="采集主程序" ValidateRequest="false" Codebehind="CollectionMain.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server"><pe:ExtendedGridView ID="EgvItemRules" runat="server" AllowPaging="True" AutoGenerateColumns="False"
        DataKeyNames="ItemID" DataSourceID="OdsCollectionItemRules" ItemName="项目" ItemUnit="个"
        CheckBoxFieldHeaderWidth="3%" SerialText="" AutoGenerateCheckBoxColumn="True"  OnRowDataBound="EgvItemRules_RowDataBound" EmptyDataText ="没有任何测试通过的采集项目！" >
        <Columns>
            <pe:BoundField DataField="ItemID" HeaderText="ID" SortExpression="ID">
                <HeaderStyle Width="5%" />
            </pe:BoundField>
            <pe:BoundField DataField="ItemName" HeaderText="项目名称" SortExpression="ItemName">
                <HeaderStyle Width="10%" />
            </pe:BoundField>
            <pe:TemplateField HeaderText="采集网站名称" SortExpression="UrlName">
                <HeaderStyle Width="15%" />
                <ItemTemplate>
                    <a href="<%#Eval("Url")%>" target='_blank'><%#Eval("UrlName")%></a>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:BoundField DataField="NodeName" HeaderText="所属栏目" SortExpression="NodeName">
                <HeaderStyle Width="10%" />
            </pe:BoundField>
            <pe:BoundField DataField="ModelName" HeaderText="所属模型" SortExpression="ModelName">
                <HeaderStyle Width="10%" />
            </pe:BoundField>
            <pe:BoundField DataField="NewsCollecDate" HeaderText="上次采集时间" SortExpression="NewsCollecDate">
                <HeaderStyle Width="20%" />
            </pe:BoundField> 
            <pe:TemplateField HeaderText="成功记录" SortExpression="NodeID">
                <HeaderStyle Width="15%" />
                <ItemTemplate>
                    <pe:ExtendedLabel HtmlEncode="false"
 ID="LblSuccessRecord" runat="server" Text="Label"></pe:ExtendedLabel>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="失败记录" SortExpression="NodeID">
                <HeaderStyle Width="15%" />
                <ItemTemplate>
                    <pe:ExtendedLabel HtmlEncode="false"
 ID="LblFailureRecord" runat="server" Text="Label"></pe:ExtendedLabel>
                </ItemTemplate>
            </pe:TemplateField>  
        </Columns>
    </pe:ExtendedGridView> <br />&nbsp;&nbsp;<input type='checkbox' name='ChkIsTitle' value='True' id='ChkIsTitle' checked ="checked"  /> 不采集相同标题的文章 &nbsp;&nbsp;&nbsp;&nbsp; <asp:Button ID="BtnCollec" Text=" 开始采集 " OnClientClick="return confirm('确实要采集选中的采集项目么？')"
        runat="server" OnClick="BtnCollec_Click"  /> <br /><asp:ObjectDataSource ID="OdsCollectionItemRules" runat="server" SelectMethod="GetCollectionList" SelectCountMethod="GetCountNumber"
        TypeName="EasyOne.Collection.CollectionItem" EnablePaging="True" StartRowIndexParameterName="startRowIndexId"
        MaximumRowsParameterName="maxNumberRows" OldValuesParameterFormatString="original_{0}">
    </asp:ObjectDataSource>
</asp:Content>
