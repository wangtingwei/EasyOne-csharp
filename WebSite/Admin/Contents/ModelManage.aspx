<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Contents.ContentModelManage"
    MasterPageFile="~/Admin/MasterPage.master" Title="ģ�͹���" Codebehind="ModelManage.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <pe:ExtendedGridView ID="EgvModel" runat="server" AutoGenerateColumns="False" DataKeyNames="ModelID"
        DataSourceID="OdsModel" ItemName="ģ��" ItemUnit="��" SerialText="" OnRowCommand="EgvModel_RowCommand"
        RowDblclickBoundField="ModelID" RowDblclickUrl="Model.aspx?Action=Modify&amp;ModelID={$Field}">
        <Columns>
            <pe:BoundField DataField="ModelID" HeaderText="ID">
                <HeaderStyle Width="5%" />
            </pe:BoundField>
            <pe:TemplateField HeaderText="ͼ��">
                <HeaderStyle Width="5%" />
                <ItemTemplate>
                    <asp:Image ID="ImgItemIcon" runat="server" ImageUrl='<%#"~/Images/ModelIcon/" + (string.IsNullOrEmpty(Eval("ItemIcon").ToString())?"Default.gif":Eval("ItemIcon").ToString()) %>' />
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="ģ������">
                <HeaderStyle Width="15%" />
                <ItemTemplate>
                    <a href='Model.aspx?Action=Modify&ModelID=<%#Eval("ModelID") %>'>
                        <%# Eval("ModelName").ToString().Length <= 10 ? Eval("ModelName") : Eval("ModelName").ToString().Substring(0, 10) + ".."%>
                    </a>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="ģ������">
                <ItemTemplate>
                    <%# Eval("Description").ToString()%>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
            </pe:TemplateField>
            <pe:TemplateField HeaderText="��Ŀ����">
                <HeaderStyle Width="10%" />
                <ItemTemplate>
                    <%# Eval("ItemName").ToString().Length <= 10 ? Eval("ItemName") : Eval("ItemName").ToString().Substring(0, 10) + ".."%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:BoundField DataField="TableName" HeaderText="����" SortExpression="TableName">
                <HeaderStyle Width="10%" />
                <ItemStyle HorizontalAlign="Left" />
            </pe:BoundField>
            <pe:TemplateField HeaderText="״̬">
                <HeaderStyle Width="5%" />
                <ItemTemplate>
                    <%# (bool)Eval("Disabled") ? "<span style='color:Red'>����</span>" : "����"%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="�������">
                <HeaderStyle Width="26%" />
                <ItemTemplate>
                    <a id="EahModelModify" href='<%# "Model.aspx?Action=Modify&ModelID=" + Eval("ModelID")%>'
                        runat="server">�޸�</a> <a id="ELbtnDisabled" text='<%# (bool)Eval("Disabled") ? "����" : "����"%>'
                            runat="server" commandname='<%# (bool)Eval("Disabled") ? "Enabled" : "Disabled"%>'
                            commandargument='<%# Eval("ModelID")%>' /><a id="EahModelDelete" href='<%# AppendSecurityCode("ModelManage.aspx?Action=Delete&ModelID=" + Eval("ModelID"))%>'
                                onclick="return confirm('�Ƿ�ɾ����ģ�ͣ�');" runat="server">ɾ��</a> <a id="EahFieldManage"
                                    href='<%# "~/Admin/CommonModel/FieldManage.aspx?ModelType=1&ModelID=" + Eval("ModelID")+"&ModelName="+Server.UrlEncode(Eval("ModelName").ToString())%>'
                                    runat="server">�ֶ��б�</a> <a id="EahModelTemplate" href='<%# AppendSecurityCode("~/Admin/CommonModel/ModelTemplate.aspx?Action=AddModelToFieldTemplate&ModelType=1&ModelID=" + Eval("ModelID")+"&ModelName="+Server.UrlEncode(Eval("ModelName").ToString()))%>'
                                        runat="server">��Ϊģ��ģ��</a>
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
