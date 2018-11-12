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
    <pe:ExtendedGridView ID="EgvQuestionDetail" runat="server" SerialText="序号" AutoGenerateColumns="False"
        OnRowDataBound="EgvQuestionDetail_RowDataBound" AllowPaging="True">
        <Columns>
            <pe:TemplateField HeaderText="序号">
                <HeaderStyle Width="4%" />
            </pe:TemplateField>
            <pe:TemplateField HeaderText="问题">
                <ItemStyle HorizontalAlign="Left" />
                <HeaderStyle Width="45%" />
                <ItemTemplate>
                    <pe:ExtendedAnchor IsChecked="true" runat="server" OperateCode="SurveyResultPreview"
                        href='<%# "AnswerList.aspx?SurveyID="+RequestString("SurveyID")+"&QuestionID="+Eval("QuestionId") %>'>
                        <%# Eval("QuestionContent") %>
                    </pe:ExtendedAnchor>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="问题类型">
                <HeaderStyle Width="10%" />
            </pe:TemplateField>
            <pe:TemplateField HeaderText="所属问卷">
                <HeaderStyle Width="16%" />
            </pe:TemplateField>
            <pe:TemplateField HeaderText="常规管理操作">
                <HeaderStyle Width="25%" />
                <ItemTemplate>
                    <pe:ExtendedAnchor IsChecked="true" runat="server" OperateCode="SurveyResultPreview"
                        href='<%# "AnswerList.aspx?SurveyID="+RequestString("SurveyID")+"&QuestionID="+Eval("QuestionId") %>'>查看问题的回答详情</pe:ExtendedAnchor>
                </ItemTemplate>
            </pe:TemplateField>
        </Columns>
    </pe:ExtendedGridView>
    <asp:HiddenField ID="HdnSurveyId" runat="server" />
</asp:Content>
