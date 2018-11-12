<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Contents.ContentManageUI" Title="���ݹ���" Codebehind="ContentManage.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <asp:ScriptManager ID="ScriptManageContent" runat="server" />
    <pe:ContentManageNavigation ID="Cmn" runat="server" />
    <div style="padding-top: 5px;">
        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td style="width: 80px" align="left" class="tdbg">
                    <b>����ѡ�</b>
                </td>
                <td class="tdbg">
                    <asp:RadioButtonList ID="RadlContent" runat="server" RepeatDirection="Horizontal"
                        AutoPostBack="true" RepeatLayout="Flow" OnSelectedIndexChanged="RadlContent_SelectedIndexChanged">
                        <asp:ListItem Value="100">��������</asp:ListItem>
                        <asp:ListItem Value="-1">�ݸ�</asp:ListItem>
                        <asp:ListItem Value="101">�����</asp:ListItem>
                        <asp:ListItem Value="99">�����</asp:ListItem>
                        <asp:ListItem Value="-2">�˸�</asp:ListItem>
                    </asp:RadioButtonList>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="DrpSearchType" runat="server">
                        <asp:ListItem Value="ID" Text="ID" />
                        <asp:ListItem Value="Title" Text="���ݱ���" />
                        <asp:ListItem Value="Inputer" Text="¼����" />
                    </asp:DropDownList>
                    <asp:TextBox ID="TxtSearchKeyword" runat="server" />
                    <asp:Button ID="BtnSearch" runat="server" Text="����" OnClick="BtnSearch_Click" />
                    <pe:ExtendedLabel ID="LblContentAdvancedSearch" runat="server" HtmlEncode="false" Text="<a href='ContentAdvancedSearch.aspx'>�߼�����</a>" Visible ="false" ></pe:ExtendedLabel>
                </td>
            </tr>
        </table>
    </div>
    <div style="padding-top: 15px;">
        <table style="width: 100%; margin: 0 auto;" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
                </td>
                <td align="right">
                    ����ʽ��
                    <asp:DropDownList ID="DropRescentQuery" AutoPostBack="true" OnSelectedIndexChanged="DropSelectedIndex_Changed"
                        runat="server">
                        <asp:ListItem Value="-1">��ID����</asp:ListItem>
                        <asp:ListItem Value="-2">��ID����</asp:ListItem>
                        <asp:ListItem Value="1">���Ƽ�������</asp:ListItem>
                        <asp:ListItem Value="2">���Ƽ���������</asp:ListItem>
                        <asp:ListItem Value="3">�����ȼ�����</asp:ListItem>
                        <asp:ListItem Value="4">�����ȼ�������</asp:ListItem>
                        <asp:ListItem Value="5">���յ��������</asp:ListItem>
                        <asp:ListItem Value="6">���յ��������</asp:ListItem>
                        <asp:ListItem Value="7">���ܵ��������</asp:ListItem>
                        <asp:ListItem Value="8">���ܵ��������</asp:ListItem>
                        <asp:ListItem Value="9">���µ��������</asp:ListItem>
                        <asp:ListItem Value="10">���µ��������</asp:ListItem>
                        <asp:ListItem Value="11">���ܵ��������</asp:ListItem>
                        <asp:ListItem Value="12">���ܵ��������</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <pe:ExtendedGridView ID="EgvContent" runat="server" DataSourceID="OdsContents" SerialText=""
        AutoGenerateCheckBoxColumn="True" AutoGenerateColumns="False" AllowPaging="True"
        OnRowDataBound="EgvContent_RowDataBound" OnRowCommand="EgvContent_RowCommand"
        DataKeyNames="GeneralId" CheckBoxFieldHeaderWidth="3%" IsHoldState="True">
        <Columns>
            <asp:BoundField DataField="GeneralId" HeaderText="ID" 
                SortExpression="GeneralId">
                <HeaderStyle Width="5%" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="����" SortExpression="Title">
                <ItemTemplate>
                    <pe:LinkImage ID="LinkImageModel" runat="server">
                        <pe:ExtendedHyperLink ID="LnkNodeLink" runat="server" />
                        <asp:HyperLink ID="HypTitle" runat="server" />
                    </pe:LinkImage>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:BoundField DataField="Inputer" HeaderText="¼����" SortExpression="Inputer">
                <HeaderStyle Width="6%" />
            </asp:BoundField>
            <asp:BoundField DataField="Hits" HeaderText="�����" SortExpression="Hits">
                <HeaderStyle Width="6%" />
            </asp:BoundField>
            <asp:BoundField DataField="EliteLevel" HeaderText="�Ƽ�����" 
                SortExpression="EliteLevel">
                <HeaderStyle Width="8%" />
            </asp:BoundField>
            <asp:BoundField DataField="Priority" HeaderText="���ȼ�" SortExpression="Priority">
                <HeaderStyle Width="6%" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="״̬" SortExpression="Status">
                <HeaderStyle Width="15%" />
                <ItemTemplate>
                    <asp:Label ID="lStatus" Text='<%# GetStatusShow(Eval("Status").ToString())%>' runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="������" SortExpression="Status">
                <ItemTemplate>
                    <pe:ExtendedLabel ID="LblIsCreateHtml" runat="server" HtmlEncode="false">
                    &nbsp;
                    </pe:ExtendedLabel>
                </ItemTemplate>
                <HeaderStyle Width="6%" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="����������" SortExpression="Disabled">
                <ItemTemplate>
                    <asp:HyperLink ID="ContentModify" runat="server" />
                    <asp:LinkButton ID="ContentDelete" runat="server" />
                </ItemTemplate>
                <HeaderStyle Width="12%" />
            </asp:TemplateField>
        </Columns>
    </pe:ExtendedGridView>
    <asp:HiddenField ID="HdnListType" runat="server" Value="-1" />
    <asp:HiddenField ID="HdnStatus" runat="server" Value="100" />
    <asp:HiddenField ID="HdnSearchType" runat="server" Value="" />
    <asp:HiddenField ID="HdnSearchKeyword" runat="server" Value="" />
    <asp:ObjectDataSource ID="OdsContents" runat="server" SelectMethod="GetSearchContentList"
        TypeName="EasyOne.Contents.ContentManage" EnablePaging="True" MaximumRowsParameterName="maxNumberRows"
        StartRowIndexParameterName="startRowIndexId" SelectCountMethod="GetTotalOfCommonModelInfo">
        <SelectParameters>
            <asp:QueryStringParameter Name="nodeId" QueryStringField="NodeID" Type="Int32" />
            <asp:ControlParameter ControlID="HdnListType" Type="Int32" Name="sortType" PropertyName="Value" />
            <asp:ControlParameter ControlID="HdnStatus" Type="Int32" Name="status" PropertyName="Value" />
            <asp:ControlParameter ControlID="HdnSearchType" Type="String" Name="searchType" PropertyName="Value" />
            <asp:ControlParameter ControlID="HdnSearchKeyword" Type="String" Name="keyword" PropertyName="Value" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <br />
    <input id="ChkAll" onclick="javascript:CheckAll(this);" type="checkbox" /><label
        for="ChkAll">ѡ�б�ҳ��ʾ��������Ŀ</label>
    &nbsp;&nbsp;
    <asp:Button ID="EBtnBatchDelete" Text="����ɾ��" OnClientClick="return batchconfirm('ȷ��Ҫɾ��ѡ�е���Ŀ�𣿱�������ѡ�е���Ϣ�Ƶ�����վ�С���Ҫʱ���ɴӻ���վ�лָ���');"
        OnClick="EBtnBatchDelete_Click" CausesValidation="False" runat="server" />&nbsp;&nbsp;
    <asp:Button ID="EBtnBatchSet" Text="��������" runat="server" OnClick="EBtnBatchSet_Click" />
    <asp:Button ID="EBtnBatchMove" Text="�����ƶ�" runat="server" OnClick="EBtnBatchMove_Click" />
    <asp:Button ID="EBtnPass" Text="���ͨ��" runat="server" OnClick="EBtnPass_Click" />
    <asp:Button ID="EBtnCancelPass" Text="ȡ�����" runat="server" OnClick="EBtnCancelPass_Click" />
    <asp:Button ID="BatchSpecialSet" Text="��ӵ�ר��" runat="server" OnClick="EBtnBatchSpecialSet_Click" />
    <asp:Button ID="BatchNodeSet" Text="��ӵ������ڵ�" runat="server" OnClick="BatchNodeSet_Click" />
    <asp:Button ID="BtnArchiving" Text="�鵵����" runat="server" OnClick="BtnArchiving_Click" />
</asp:Content>
