<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Collection.ItemManage"
    MasterPageFile="~/Admin/MasterPage.master" Title="采集项目管理" ValidateRequest="false" Codebehind="ItemManage.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
 <pe:ExtendedGridView ID="EgvItemRules" runat="server" AllowPaging="True" AutoGenerateColumns="False"
        DataKeyNames="ItemID" DataSourceID="OdsCollectionItemRules" ItemName="项目" ItemUnit="个"
        CheckBoxFieldHeaderWidth="3%" SerialText="" AutoGenerateCheckBoxColumn="True"  
        RowDblclickBoundField="ItemID" RowDblclickUrl="ConfigStep2.aspx?Action=Modify&ItemID={$Field}">
        <Columns>
            <pe:BoundField DataField="ItemID" HeaderText="ID" SortExpression="ID">
                <HeaderStyle Width="4%" />
            </pe:BoundField>
            <pe:BoundField DataField="ItemName" HeaderText="项目名称" SortExpression="ItemName">
                <HeaderStyle Width="17%" />
            </pe:BoundField>
            <pe:TemplateField HeaderText="采集网站名称" SortExpression="UrlName">
                <HeaderStyle Width="18%" />
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
            <pe:TemplateField HeaderText="可采集" SortExpression="UrlName">
                <HeaderStyle Width="7%" />
                <ItemTemplate>
                    <%# (bool)Eval("Detection") == true ? "<b>√</b>" : "<span style=\"color:red;\"><b>×</b></span>"%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="操作">
                <HeaderStyle Width="24%" />
                <ItemTemplate>
                    <a href="ConfigStep1.aspx?Action=Modify&ItemID=<%#Eval("ItemID")%>&ModelId=<%#Eval("ModelID")%>&NodeId=<%#Eval("NodeID")%>"> 修改项目</a> |
                    <a href="ConfigStep2.aspx?Action=Modify&ItemID=<%#Eval("ItemID")%>"> 修改列表</a> |
                    <a href="ConfigStep3.aspx?Action=Modify&ItemID=<%#Eval("ItemID")%>"> 修改字段</a> <br />
                    <a href="ItemManage.aspx?Action=Detection&ItemID=<%#Eval("ItemID")%>"> 测试项目</a> | 
                    <a href="ItemManage.aspx?Action=Copy&ItemID=<%#Eval("ItemID")%>"> 复制项目</a> | 
                    <a href="ItemManage.aspx?Action=Delete&ItemID=<%#Eval("ItemID")%>" onclick="return confirm('是否删除该采集项目？');">删除项目</a>
                </ItemTemplate>
            </pe:TemplateField>
        </Columns>
    </pe:ExtendedGridView>
    <br />
    <input id="ChkAll" onclick="javascript:CheckAll(this);" type="checkbox" />选中本页显示的所有项目
    &nbsp;&nbsp;
    <asp:Button ID="BtnBatchDelete" Text="批量删除选定采集项目" OnClientClick="return batchconfirm('确实要删除选中的采集项目么？')"
        runat="server" OnClick="BtnBatchDelete_Click" />&nbsp;&nbsp;
    <br />
    <asp:ObjectDataSource ID="OdsCollectionItemRules" runat="server" SelectMethod="GetList" SelectCountMethod="GetCountNumber"
        TypeName="EasyOne.Collection.CollectionItem" EnablePaging="True" StartRowIndexParameterName="startRowIndexId"
        MaximumRowsParameterName="maxNumberRows" OldValuesParameterFormatString="original_{0}">
        <DeleteParameters>
            <asp:Parameter Name="id" Type="String" />
        </DeleteParameters>
    </asp:ObjectDataSource>
    <span style="color:blue;">注意：</span>如果修改了采集项目，采集项目将自动转换为不可运行，需要再操作所属项目的测试项目将其变为可运行。
</asp:Content>