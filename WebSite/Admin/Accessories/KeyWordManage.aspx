<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Accessories.KeyWordManage"
    MasterPageFile="~/Admin/MasterPage.master" Title="�ؼ��ֹ���" Codebehind="KeyWordManage.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server"
        />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <pe:ExtendedGridView ID="EgvKeyWord" runat="server" AllowPaging="True" AutoGenerateColumns="False"
        AutoGenerateCheckBoxColumn="True" DataKeyNames="KeywordID" DataSourceID="OdsKeyWord"
        ItemName="��Ŀ" ItemUnit="��" SerialText="" RowDblclickBoundField="KeywordID" RowDblclickUrl="Keyword.aspx?Action=Modify&KeywordID={$Field}">
        <Columns>
            <pe:BoundField DataField="KeywordID" HeaderText="ID">
                <HeaderStyle Width="6%" />
            </pe:BoundField>
            <pe:BoundField DataField="KeywordText" HeaderText="�ؼ�������" SortExpression="KeywordText">
            </pe:BoundField>
            <pe:TemplateField HeaderText="�ؼ������" SortExpression="KeywordID">
                <HeaderStyle Width="9%" />
                <ItemTemplate>
                    <%# (int)Eval("KeywordType") != 0 ? "�����ؼ���" : "����ؼ���"%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:BoundField DataField="Priority" HeaderText="���ȼ�" SortExpression="Priority">
                <HeaderStyle Width="6%" />
            </pe:BoundField>
            <pe:BoundField DataField="Hits" HeaderText="��ѯ����" SortExpression="Hits">
                <HeaderStyle Width="8%" />
            </pe:BoundField>
            <pe:BoundField DataField="QuoteTimes" HeaderText="���ô���" SortExpression="QuoteTimes">
                <HeaderStyle Width="8%" />
            </pe:BoundField>
            <pe:BoundField DataField="LastUseTime" HeaderText="������ʱ��" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"
                SortExpression="LastUseTime" HtmlEncode="False">
                <HeaderStyle Width="17%" />
            </pe:BoundField>
            <pe:TemplateField HeaderText="����">
                <HeaderStyle Width="9%" />
                <ItemTemplate>
                    <pe:ExtendedAnchor ID="EahKeywordModify" IsChecked="true" OperateCode="KeyWordManage"
                        href='<%# "Keyword.aspx?Action=Modify&KeywordID=" + Eval("KeywordID")%>' runat="server">�޸�</pe:ExtendedAnchor>
                    <pe:ExtendedAnchor ID="EahKeywordDelete" IsChecked="true" OperateCode="KeyWordManage"
                        href='<%# AppendSecurityCode("KeywordManage.aspx?Action=Delete&KeywordID=" + Eval("KeywordID"))%>'
                        onclick="return confirm('�Ƿ�ɾ���ùؼ��֣�');" runat="server">ɾ��</pe:ExtendedAnchor>
                </ItemTemplate>
            </pe:TemplateField>
        </Columns>
    </pe:ExtendedGridView>
    <asp:ObjectDataSource ID="OdsKeyWord" runat="server" SelectMethod="GetList" SelectCountMethod="GetTotalOfKeyword"
        TypeName="EasyOne.Accessories.Keywords" DeleteMethod="Delete" EnablePaging="True"
        StartRowIndexParameterName="startRowIndexId" MaximumRowsParameterName="maxNumberRows"
        OldValuesParameterFormatString="original_{0}">
        <DeleteParameters>
            <asp:Parameter Name="id" Type="String" />
        </DeleteParameters>
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="" Name="searchType" QueryStringField="SearchType"
                Type="Int32" />
            <asp:QueryStringParameter DefaultValue="" Name="keyword" QueryStringField="KeyWord"
                Type="String" />
            <asp:QueryStringParameter DefaultValue="0" Name="listType" QueryStringField="ListType"
                Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:HiddenField ID="HdnlistType" runat="server" Value="0" />
    <br />
    <input id="ChkAll" onclick="javascript:CheckAll(this);" type="checkbox" />ѡ�б�ҳ��ʾ�����йؼ���
    &nbsp;&nbsp;
    <asp:Button ID="EBtnBatchDelete" Text="ɾ��ѡ�еĹؼ���" OnClientClick="return batchconfirm('�Ƿ�Ҫɾ���ؼ��֣�');"
        OnClick="EBtnBatchDelete_Click" CausesValidation="False" runat="server" />
    <br />
    <br />
</asp:Content>
