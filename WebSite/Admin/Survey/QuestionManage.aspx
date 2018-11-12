<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Survey.QuestionManage" Codebehind="QuestionManage.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <pe:ExtendedGridView ID="EgvQuestion" runat="server" AutoGenerateCheckBoxColumn="True"
        ItemName="����" ItemUnit="��" AutoGenerateColumns="False" SerialText="" DataSourceID="OdsQuetion"
        OnRowDataBound="EgvQuestion_RowDataBound" DataKeyNames="QuestionId">
        <Columns>
            <pe:TemplateField HeaderText="����" SortExpression="Disabled">
                <HeaderStyle Width="55%" />
                <ItemTemplate>
                    <%# Eval("QuestionContent").ToString().Length <= 30 ? Eval("QuestionContent") : Eval("QuestionContent").ToString().Substring(0, 30) + "..."%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="��������">
                <HeaderStyle Width="15%" />
            </pe:TemplateField>
            <pe:TemplateField HeaderText="�������">
                <HeaderStyle Width="20%" />
                <ItemTemplate>
                    <pe:ExtendedAnchor id="LnkModify" IsChecked="true" runat="server" OperateCode="SurveyCreate"
                        href='<%# "Question.aspx?Action=Modify&SurveyID="+surveyId+"&QuestionID="+ Eval("QuestionID") %>'
                        Disabled='<%# (int)isOpen == 1 %>'>�޸�</pe:ExtendedAnchor>
                    <pe:ExtendedAnchor id="LnkDelete" IsChecked="true" runat="server" OperateCode="SurveyCreate"
                        onclick="return confirm('ȷ��Ҫɾ����������');" href='<%# AppendSecurityCode("QuestionManage.aspx?Action=Delete&SurveyID="+surveyId+"&QuestionID="+ Eval("QuestionID")) %>'
                        Disabled='<%# (int)isOpen == 1 %>'>ɾ��</pe:ExtendedAnchor>
                    <pe:ExtendedAnchor IsChecked="true" runat="server" OperateCode="SurveyCreate" href='<%# string.Format("QuestionPreview.aspx?SurveyID={0}&QuestionID={1}&QuestionType={2}&IsOpen={3}",surveyId,Eval("QuestionID"),Eval("QuestionType"),isOpen) %>'>Ԥ��</pe:ExtendedAnchor>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="����" SortExpression="Disabled">
                <ItemTemplate>
                    <asp:DropDownList ID="DropOrderId" runat="server">
                    </asp:DropDownList>
                </ItemTemplate>
            </pe:TemplateField>
        </Columns>
    </pe:ExtendedGridView>
    ע�⣺�ʾ�ֻ���ڷǡ����á���״̬�²ſ��Զ���������������и��ġ�<br />
    <div style="text-align: center">
        <pe:ExtendedButton IsChecked="true" OperateCode="SurveyCreate" ID="BtnAddQuestion"
            runat="server" OnClick="BtnAddQuestion_Click" Text="�������" />
        <pe:ExtendedButton IsChecked="true" OperateCode="SurveyCreate" ID="BtnDel" runat="server"
            Text="����ɾ��ѡ��������" CausesValidation="False" OnClientClick="return confirm('ȷʵҪɾ��ѡ�е����⣿');"
            OnClick="BtnDel_Click" />
        <pe:ExtendedButton IsChecked="true" OperateCode="SurveyCreate" ID="BtnSetOrderId"
            runat="server" OnClick="BtnOrderId_Click" Text="��������" />
    </div>
    <asp:ObjectDataSource ID="OdsQuetion" runat="server" SelectMethod="GetFieldList"
        MaximumRowsParameterName="maxNumberRows" StartRowIndexParameterName="startRowIndexId"
        TypeName="EasyOne.Survey.SurveyField">
        <SelectParameters>
            <asp:QueryStringParameter Name="surveyId" QueryStringField="surveyId" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
