<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Collection.CollectionMain"
    MasterPageFile="~/Admin/MasterPage.master" Title="�ɼ�������" ValidateRequest="false" Codebehind="CollectionMain.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server"><pe:ExtendedGridView ID="EgvItemRules" runat="server" AllowPaging="True" AutoGenerateColumns="False"
        DataKeyNames="ItemID" DataSourceID="OdsCollectionItemRules" ItemName="��Ŀ" ItemUnit="��"
        CheckBoxFieldHeaderWidth="3%" SerialText="" AutoGenerateCheckBoxColumn="True"  OnRowDataBound="EgvItemRules_RowDataBound" EmptyDataText ="û���κβ���ͨ���Ĳɼ���Ŀ��" >
        <Columns>
            <pe:BoundField DataField="ItemID" HeaderText="ID" SortExpression="ID">
                <HeaderStyle Width="5%" />
            </pe:BoundField>
            <pe:BoundField DataField="ItemName" HeaderText="��Ŀ����" SortExpression="ItemName">
                <HeaderStyle Width="10%" />
            </pe:BoundField>
            <pe:TemplateField HeaderText="�ɼ���վ����" SortExpression="UrlName">
                <HeaderStyle Width="15%" />
                <ItemTemplate>
                    <a href="<%#Eval("Url")%>" target='_blank'><%#Eval("UrlName")%></a>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:BoundField DataField="NodeName" HeaderText="������Ŀ" SortExpression="NodeName">
                <HeaderStyle Width="10%" />
            </pe:BoundField>
            <pe:BoundField DataField="ModelName" HeaderText="����ģ��" SortExpression="ModelName">
                <HeaderStyle Width="10%" />
            </pe:BoundField>
            <pe:BoundField DataField="NewsCollecDate" HeaderText="�ϴβɼ�ʱ��" SortExpression="NewsCollecDate">
                <HeaderStyle Width="20%" />
            </pe:BoundField> 
            <pe:TemplateField HeaderText="�ɹ���¼" SortExpression="NodeID">
                <HeaderStyle Width="15%" />
                <ItemTemplate>
                    <pe:ExtendedLabel HtmlEncode="false"
 ID="LblSuccessRecord" runat="server" Text="Label"></pe:ExtendedLabel>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="ʧ�ܼ�¼" SortExpression="NodeID">
                <HeaderStyle Width="15%" />
                <ItemTemplate>
                    <pe:ExtendedLabel HtmlEncode="false"
 ID="LblFailureRecord" runat="server" Text="Label"></pe:ExtendedLabel>
                </ItemTemplate>
            </pe:TemplateField>  
        </Columns>
    </pe:ExtendedGridView> <br />&nbsp;&nbsp;<input type='checkbox' name='ChkIsTitle' value='True' id='ChkIsTitle' checked ="checked"  /> ���ɼ���ͬ��������� &nbsp;&nbsp;&nbsp;&nbsp; <asp:Button ID="BtnCollec" Text=" ��ʼ�ɼ� " OnClientClick="return confirm('ȷʵҪ�ɼ�ѡ�еĲɼ���Ŀô��')"
        runat="server" OnClick="BtnCollec_Click"  /> <br /><asp:ObjectDataSource ID="OdsCollectionItemRules" runat="server" SelectMethod="GetCollectionList" SelectCountMethod="GetCountNumber"
        TypeName="EasyOne.Collection.CollectionItem" EnablePaging="True" StartRowIndexParameterName="startRowIndexId"
        MaximumRowsParameterName="maxNumberRows" OldValuesParameterFormatString="original_{0}">
    </asp:ObjectDataSource>
</asp:Content>
