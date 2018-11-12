<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.User.CardsManage" Title="��ֵ������" Codebehind="CardsManage.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <pe:ExtendedGridView ID="EgvCards" runat="server" AllowPaging="True" AutoGenerateColumns="False"
        AutoGenerateCheckBoxColumn="True" DataKeyNames="CardId" ItemName="��ֵ��" ItemUnit="��"
        DataSourceID="OdsCards" OnRowDataBound="EgvCards_RowDataBound" OnRowDeleting="EgvCards_RowDeleting"
        CheckBoxFieldHeaderWidth="3%" SerialText="" OnDataBound="EgvCards_DataBound"
        RowDblclickBoundField="CardId" 
        RowDblclickUrl="CardModify.aspx?CardId={$Field}">
        <Columns>
            <pe:TemplateField HeaderText="����" SortExpression="CardType" />
            <pe:TemplateField HeaderText="����">
                <ItemTemplate>
                    <a href='<%# Eval("CardId", "CardShow.aspx?CardId={0}") %>'>
                        <%# Eval("CardNum") %>
                    </a>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:BoundField HeaderText="��ֵ" DataField="Money" DataFormatString="{0:N0}Ԫ" HtmlEncode="False"
                SortExpression="Money" />
            <pe:TemplateField HeaderText="����" SortExpression="ValidNum" />
            <pe:BoundField HeaderText="��ֹ����" DataField="EndDate" DataFormatString="{0:yyyy-MM-dd}"
                HtmlEncode="False" SortExpression="EndDate" />
            <pe:TemplateField HeaderText="������Ʒ" SortExpression="ProductName" />
            <pe:TemplateField HeaderText="״̬" />
            <pe:TemplateField HeaderText="ʹ����">
                <ItemTemplate>
                    <a href='<%#Server.UrlEncode( Eval("UserName", "UserShow.aspx?UserName={0}")) %>'>
                        <%# Eval("UserName") %>
                    </a>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:BoundField HeaderText="��ֵʱ��" DataField="UseTime" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"
                HtmlEncode="False" SortExpression="UseTime" />
            <pe:TemplateField HeaderText="������">
                <ItemTemplate>
                    <a href='<%# Eval("AgentName", "CardsManage.aspx?AgentName={0}") %>'>
                        <%# Eval("AgentName") %>
                    </a>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="����">
                <ItemTemplate>
                    <%--<a href='<%# Eval("CardId", "CardModify.aspx?CardId={0}") %>'></a>--%>
                    <%--  <asp:LinkButton  ID="LBtnModify" runat="server" CommandName="Modify" Text="�޸�"/>--%>
                    <asp:HyperLink Text="�޸�" runat="server" ID="HlnkModify" NavigateUrl='<%# Eval("CardId", "CardModify.aspx?CardId={0}") %>' />
                    <asp:LinkButton ID="LBtnDelete" runat="server" OnClientClick="return confirm('ȷ��Ҫɾ���˳�ֵ����')"
                        CommandName="Delete">ɾ��</asp:LinkButton>
                </ItemTemplate>
            </pe:TemplateField>
        </Columns>
    </pe:ExtendedGridView>
    <asp:ObjectDataSource ID="OdsCards" runat="server" SelectMethod="GetCardList" TypeName="EasyOne.Accessories.Cards"
        EnablePaging="True" MaximumRowsParameterName="maxiNumRows" SelectCountMethod="GetTotalofCards"
        StartRowIndexParameterName="startRowIndexId" DeleteMethod="DelCard">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="-1" Name="cardType" QueryStringField="CardType"
                Type="String" />
            <asp:QueryStringParameter DefaultValue="-1" Name="cardStatus" QueryStringField="CardStatus"
                Type="String" />
            <asp:QueryStringParameter DefaultValue="1" Name="field" QueryStringField="Field"
                Type="String" />
            <asp:QueryStringParameter DefaultValue="" Name="keyword" QueryStringField="Keyword"
                Type="String" />
            <asp:QueryStringParameter Name="agentName" QueryStringField="AgentName" Type="String" />
        </SelectParameters>
        <DeleteParameters>
            <asp:ControlParameter ControlID="EgvCards" Name="cardId" PropertyName="SelectedValue"
                Type="Int32" />
        </DeleteParameters>
    </asp:ObjectDataSource>
    <br />
    <asp:Button ID="BtnDel" runat="server" OnClick="BtnDel_Click" Text="ɾ��ѡ�еĳ�ֵ��" OnClientClick="return confirm('ȷ��Ҫɾ��ָ���ĳ�ֵ����')" />
    <asp:Button ID="BtnExportExcel" runat="server" OnClick="BtnExportExcel_Click" Text="������EXCEL" />
    <br />
    <br />
    <table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="border">
        <tr>
            <td class="spacingtitle" colspan="2" align="center">
                <strong>���˵��</strong></td>
        </tr>
        <tr class="tdbg">
            <td>
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                <ajaxToolkit:TabContainer runat="server" ID="Tabs" Height="150px">
                    <ajaxToolkit:TabPanel runat="Server" ID="TabPanel" HeaderText="��ֵ��˵��">
                        <ContentTemplate>
                            <ul>
                                <li>ͨ���̵����۵ĳ�ֵ��������״̬��δ�۳������۳�����ʹ�á���ʧЧ</li>
                                <li><span style="color: blue">��ͨ���̵�����</span>�ĳ�ֵ��������״̬��δʹ�á���ʹ�á���ʧЧ</li>
                                <li>�Ѿ��۳����Ѿ�ʹ�ù��ĳ�ֵ���������޸ĺ�ɾ��</li>
                            </ul>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                </ajaxToolkit:TabContainer>
            </td>
        </tr>
    </table>
</asp:Content>
