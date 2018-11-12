<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Survey.SurveyManage" Title="�����ʾ����" Codebehind="SurveyManage.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" runat="server" SiteMapProvider="AdminMapProvider" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <pe:ExtendedGridView ID="EgvSurvey" runat="server" AllowPaging="True" AutoGenerateColumns="False"
        DataSourceID="OdsSurvey" SerialText="" AutoGenerateCheckBoxColumn="True" DataKeyNames="SurveyId"
        EmptyDataText="�Ҳ����κ��ʾ�" OnRowDataBound="EgvSurvey_RowDataBound" OnRowCommand="EgvSurvey_RowCommand"
        RowDblclickBoundField="SurveyId" 
        RowDblclickUrl="Survey.aspx?Action=Modify&amp;SurveyId={$Field}">
        <Columns>
            <pe:TemplateField HeaderText="�ʾ�����">
                <itemstyle horizontalalign="Left" />
                <ItemTemplate>
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='SurveyCode.aspx?Keyword=<%#Eval("SurveyName")%>' Text='<%#Eval("SurveyName")%>'></asp:HyperLink>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="��������/��ֹ����">
                <HeaderStyle Width="18%" />
            </pe:TemplateField>
            <pe:TemplateField HeaderText="�ʾ�״̬">
                <HeaderStyle Width="12%" />
            </pe:TemplateField>
            <pe:TemplateField HeaderText="����������">
                <HeaderStyle Width="23%" />
                <ItemTemplate>
                    <pe:ExtendedAnchor IsChecked="true" id="LnkModify" runat="server" OperateCode="SurveyManage"
                        href='<%# Eval("SurveyId","Survey.aspx?Action=Modify&SurveyID={0}") %>'>�޸�</pe:ExtendedAnchor>
                    <pe:ExtendedLinkButton IsChecked="true" IsVisible="true" OperateCode="SurveyQuestionnaireManage"
                        ID="LbtnDelete" runat="server" OnClientClick="if(!disabled)return confirm('ɾ����ɾ�����ʾ�ҳ���������Ŀ��ȷ��Ҫɾ�����ʾ���')"
                        CausesValidation="False" CommandName="Del" CommandArgument='<%#Eval("SurveyId")%>'
                        Enabled='<%# (int)Eval("IsOpen") != 1 %>'>ɾ��</pe:ExtendedLinkButton>
                    <pe:ExtendedLinkButton IsChecked="true" IsVisible="true" OperateCode="SurveyQuestionnaireManage"
                        ID="LbtnOpenUse" runat="server" CausesValidation="False" Enabled='<%#(string)Eval("FileName")==null || (string)Eval("FileName")=="" ? false:true %>'
                        CommandName='<%#"SetState" + Eval("IsOpen")%>' CommandArgument='<%#Eval("SurveyId")%>'><%#Convert.ToInt32(Eval("IsOpen")) == 1 ? "����" : "����"%></pe:ExtendedLinkButton>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="��Ŀ����">
                <HeaderStyle Width="9%" />
                <ItemTemplate>
                    <pe:ExtendedAnchor IsVisible="true" OperateCode="SurveyQuestionnaireManage" IsChecked="true" id="LnkAdd"
                        runat="server" Disabled='<%# (int)Eval("IsOpen") == 1 %>' href='<%# Eval("SurveyId","Question.aspx?Action=Add&SurveyID={0}") %>'>���</pe:ExtendedAnchor>
                    <pe:ExtendedAnchor IsChecked="true" runat="server" OperateCode="SurveyQuestionnaireManage"
                        href='<%# Eval("SurveyId","QuestionManage.aspx?SurveyID={0}") %>' id="LnkList">�б�</pe:ExtendedAnchor>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="HTMLҳ����">
                <HeaderStyle Width="12%" />
                <ItemTemplate>
                    <pe:ExtendedAnchor runat="server" id="LnkCreate" IsChecked="true" OperateCode="SurveyCreate"
                        href='<%# Eval("SurveyId", "SurveyFormCreate.aspx?SurveyID={0}") %>'>����</pe:ExtendedAnchor>
                    <pe:ExtendedAnchor id="LnkEdit" runat="server" IsVisible="true" IsChecked="true"
                        OperateCode="SurveyTemplateManage" Disabled='<%# (int)Eval("IsOpen") != 2 %>'
                        href='<%# Eval("SurveyId","SurveyFormEdit.aspx?SurveyID={0}") %>'>�༭</pe:ExtendedAnchor>
                    <pe:ExtendedAnchor OperateCode="SurveyQuestionnaireManage" id="LnkPreview" runat="server" IsChecked="true" target="_blank"
                        IsVisible="true" ToolTip='<%#(string)Eval("FileName")==null || (string)Eval("FileName")=="" ? "�ʾ�δ����":"�ļ���" + (string)Eval("FileName") %>'
                        Disabled='<%# (int)Eval("IsOpen") ==0 %>' href='<%# Eval("FileName")==null || (string)Eval("FileName")=="" ? "":"~/Survey/"+Eval("FileName").ToString() %>'>Ԥ��</pe:ExtendedAnchor>
                </ItemTemplate>
            </pe:TemplateField>
        </Columns>
    </pe:ExtendedGridView>
    <asp:ObjectDataSource ID="OdsSurvey" runat="server" EnablePaging="True" MaximumRowsParameterName="maxNumberRows"
        SelectCountMethod="GetTotalOfSurvey" SelectMethod="GetList" StartRowIndexParameterName="startRowIndexId"
        TypeName="EasyOne.Survey.SurveyManager">
        <SelectParameters>
            <asp:QueryStringParameter Name="searchType" QueryStringField="SearchType" Type="Int32" />
            <asp:QueryStringParameter Name="keyword" QueryStringField="Keyword" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    ע�⣺���Ĺ��ʾ���Ŀ�б�ģ�ֻ�����¡�������ҳ����ܿ������ĺ��Ч����<br />
    <pe:ExtendedButton IsChecked="true" OperateCode="SurveyQuestionnaireManage" ID="BtnDelete" runat="server"
        OnClick="BtnDelete_Click" OnClientClick="return confirm('ɾ����ɾ�����ʾ�ҳ���������Ŀ��ȷ��Ҫɾ�����ʾ���')"
        Text="ɾ��ѡ�����ʾ�" />
</asp:Content>
