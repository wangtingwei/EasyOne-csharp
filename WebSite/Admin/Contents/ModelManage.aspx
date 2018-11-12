<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Contents.ContentModelManage"
    MasterPageFile="~/Admin/MasterPage.master" Title="模型管理" Codebehind="ModelManage.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <pe:ExtendedGridView ID="EgvModel" runat="server" AutoGenerateColumns="False" DataKeyNames="ModelID"
        DataSourceID="OdsModel" ItemName="模型" ItemUnit="个" SerialText="" OnRowCommand="EgvModel_RowCommand"
        RowDblclickBoundField="ModelID" RowDblclickUrl="Model.aspx?Action=Modify&amp;ModelID={$Field}">
        <Columns>
            <pe:BoundField DataField="ModelID" HeaderText="ID">
                <HeaderStyle Width="5%" />
            </pe:BoundField>
            <pe:TemplateField HeaderText="图标">
                <HeaderStyle Width="5%" />
                <ItemTemplate>
                    <asp:Image ID="ImgItemIcon" runat="server" ImageUrl='<%#"~/Images/ModelIcon/" + (string.IsNullOrEmpty(Eval("ItemIcon").ToString())?"Default.gif":Eval("ItemIcon").ToString()) %>' />
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="模型名称">
                <HeaderStyle Width="15%" />
                <ItemTemplate>
                    <a href='Model.aspx?Action=Modify&ModelID=<%#Eval("ModelID") %>'>
                        <%# Eval("ModelName").ToString().Length <= 10 ? Eval("ModelName") : Eval("ModelName").ToString().Substring(0, 10) + ".."%>
                    </a>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="模型描述">
                <ItemTemplate>
                    <%# Eval("Description").ToString()%>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
            </pe:TemplateField>
            <pe:TemplateField HeaderText="项目名称">
                <HeaderStyle Width="10%" />
                <ItemTemplate>
                    <%# Eval("ItemName").ToString().Length <= 10 ? Eval("ItemName") : Eval("ItemName").ToString().Substring(0, 10) + ".."%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:BoundField DataField="TableName" HeaderText="表名" SortExpression="TableName">
                <HeaderStyle Width="10%" />
                <ItemStyle HorizontalAlign="Left" />
            </pe:BoundField>
            <pe:TemplateField HeaderText="状态">
                <HeaderStyle Width="5%" />
                <ItemTemplate>
                    <%# (bool)Eval("Disabled") ? "<span style='color:Red'>禁用</span>" : "正常"%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="管理操作">
                <HeaderStyle Width="26%" />
                <ItemTemplate>
                    <a id="EahModelModify" href='<%# "Model.aspx?Action=Modify&ModelID=" + Eval("ModelID")%>'
                        runat="server">修改</a> <a id="ELbtnDisabled" text='<%# (bool)Eval("Disabled") ? "启用" : "禁用"%>'
                            runat="server" commandname='<%# (bool)Eval("Disabled") ? "Enabled" : "Disabled"%>'
                            commandargument='<%# Eval("ModelID")%>' /><a id="EahModelDelete" href='<%# AppendSecurityCode("ModelManage.aspx?Action=Delete&ModelID=" + Eval("ModelID"))%>'
                                onclick="return confirm('是否删除该模型？');" runat="server">删除</a> <a id="EahFieldManage"
                                    href='<%# "~/Admin/CommonModel/FieldManage.aspx?ModelType=1&ModelID=" + Eval("ModelID")+"&ModelName="+Server.UrlEncode(Eval("ModelName").ToString())%>'
                                    runat="server">字段列表</a> <a id="EahModelTemplate" href='<%# AppendSecurityCode("~/Admin/CommonModel/ModelTemplate.aspx?Action=AddModelToFieldTemplate&ModelType=1&ModelID=" + Eval("ModelID")+"&ModelName="+Server.UrlEncode(Eval("ModelName").ToString()))%>'
                                        runat="server">存为模型模板</a>
                </ItemTemplate>
            </pe:TemplateField>
        </Columns>
    </pe:ExtendedGridView>
    <br />
    <asp:ObjectDataSource ID="OdsModel" runat="server" SelectMethod="ContentModelList"
        TypeName="EasyOne.CommonModel.ModelManager" EnablePaging="False">
        <SelectParameters>
            <asp:Parameter DefaultValue="0" Name="showType" Type="int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
