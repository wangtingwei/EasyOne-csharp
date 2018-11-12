<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Survey.QuestionDetail" Title="Untitled Page" Codebehind="QuestionDetail.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td style="width: 60%">
                <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
            </td>
            <td style="width: 40%; text-align: right">
                <asp:Label ID="LblTitle" runat="server"></asp:Label>&nbsp;</td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <pe:ExtendedGridView ID="EgvQuestionDetail" runat="server" SerialText="���" AutoGenerateColumns="False"
        OnRowDataBound="EgvQuestionDetail_RowDataBound" AllowPaging="True">
        <Columns>
            <pe:TemplateField HeaderText="���">
                <HeaderStyle Width="4%" />
            </pe:TemplateField>
            <pe:TemplateField HeaderText="����">
                <ItemStyle HorizontalAlign="Left" />
                <HeaderStyle Width="45%" />
                <ItemTemplate>
                    <pe:ExtendedAnchor IsChecked="true" runat="server" OperateCode="SurveyResultPreview"
                        href='<%# "AnswerList.aspx?SurveyID="+RequestString("SurveyID")+"&QuestionID="+Eval("QuestionId") %>'>
                        <%# Eval("QuestionContent") %>
                    </pe:ExtendedAnchor>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="��������">
                <HeaderStyle Width="10%" />
            </pe:TemplateField>
            <pe:TemplateField HeaderText="�����ʾ�">
                <HeaderStyle Width="16%" />
            </pe:TemplateField>
            <pe:TemplateField HeaderText="����������">
                <HeaderStyle Width="25%" />
                <ItemTemplate>
                    <pe:ExtendedAnchor IsChecked="true" runat="server" OperateCode="SurveyResultPreview"
                        href='<%# "AnswerList.aspx?SurveyID="+RequestString("SurveyID")+"&QuestionID="+Eval("QuestionId") %>'>�鿴����Ļش�����</pe:ExtendedAnchor>
                </ItemTemplate>
            </pe:TemplateField>
        </Columns>
    </pe:ExtendedGridView>
    <asp:HiddenField ID="HdnSurveyId" runat="server" />
</asp:Content>
