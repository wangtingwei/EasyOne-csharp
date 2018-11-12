<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.CommonModel.ModelTemplateManage"
    Title="模型模板管理" Codebehind="ModelTemplateManage.aspx.cs" %>

<asp:Content ID="Content3" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CphContent" runat="Server">
    <pe:ExtendedGridView ID="EgvModelTemplate" runat="server" AutoGenerateColumns="False"
        DataKeyNames="TemplateID" DataSourceID="OdsModelTemplate" ItemName="模型模板" ItemUnit="个"
        AutoGenerateCheckBoxColumn="True" AllowPaging="True" SerialText="" CheckBoxFieldHeaderWidth="3%">
        <Columns>
            <pe:TemplateField HeaderText="模型模板名称" SortExpression="TemplateName">
                <HeaderStyle Width="25%" />
                <ItemTemplate>
                    <a href='ModelTemplate.aspx?Action=Modify&TemplateID=<%#Eval("TemplateID") %>'>
                        <%# Eval("TemplateName").ToString().Length <= 50 ? Eval("TemplateName") : Eval("TemplateName").ToString().Substring(0, 50) + ".."%>
                    </a>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="模型模板描述" SortExpression="TemplateDescription">
                <HeaderStyle Width="40%" />
                <ItemTemplate>
                    <%# Eval("TemplateDescription").ToString().Length <= 100 ? Eval("TemplateDescription") : Eval("TemplateDescription").ToString().Substring(0, 100) + ".."%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="管理操作">
                <HeaderStyle Width="30%" />
                <ItemTemplate>
                    <a id="EahModelTemplateModify" href='<%# "ModelTemplate.aspx?Action=Modify&TemplateID=" + Eval("TemplateID")+"&ModelType="+RequestInt32("ModelType").ToString()%>'
                        runat="server">修改</a> | <a id="EahModelTemplateDelete" href='<%# AppendSecurityCode("ModelTemplateManage.aspx?Action=Delete&ModelType="+RequestInt32("ModelType").ToString()+"&TemplateID=" + Eval("TemplateID"))%>'
                            onclick="return confirm('是否删除该模型模板？');" runat="server">删除</a> | <a id="EahTemplateFieldManage"
                                href='<%# "TemplateFieldManage.aspx?TemplateID=" + Eval("TemplateID")+ "&ModelType="+RequestInt32("ModelType").ToString()+ "&TemplateName="+Server.HtmlEncode(Eval("TemplateName").ToString())%>'
                                runat="server">字段列表</a>
                </ItemTemplate>
            </pe:TemplateField>
        </Columns>
    </pe:ExtendedGridView>
    <br />
    <input id="ChkAll" onclick="javascript:CheckAll(this);" type="checkbox" />选中本页显示的所有模型模板
    &nbsp;&nbsp;
    <asp:Button ID="EBtnDelete" Text="删除选中的模型模板" OnClientClick="return batchconfirm('是否要删除模型模板？');"
        OnClick="EBtnDelete_Click" runat="server" />
    <asp:ObjectDataSource ID="OdsModelTemplate" runat="server" SelectMethod="GetModelTemplateInfoList"
        TypeName="EasyOne.CommonModel.ModelTemplate" StartRowIndexParameterName="startRowIndexId"
        MaximumRowsParameterName="maxNumberRows" OldValuesParameterFormatString="original_{0}"
        EnablePaging="True" SelectCountMethod="GetCountNumber">
        <SelectParameters>
            <asp:Parameter DefaultValue="0" Name="startRowIndexId" Type="Int32" />
            <asp:Parameter DefaultValue="0" Name="maxNumberRows" Type="Int32" />
            <asp:QueryStringParameter DefaultValue="0" Name="type" QueryStringField="ModelType"
                Type="Object" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
