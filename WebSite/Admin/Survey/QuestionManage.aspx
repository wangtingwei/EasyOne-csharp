<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Survey.QuestionManage" Codebehind="QuestionManage.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <pe:ExtendedGridView ID="EgvQuestion" runat="server" AutoGenerateCheckBoxColumn="True"
        ItemName="问题" ItemUnit="个" AutoGenerateColumns="False" SerialText="" DataSourceID="OdsQuetion"
        OnRowDataBound="EgvQuestion_RowDataBound" DataKeyNames="QuestionId">
        <Columns>
            <pe:TemplateField HeaderText="问题" SortExpression="Disabled">
                <HeaderStyle Width="55%" />
                <ItemTemplate>
                    <%# Eval("QuestionContent").ToString().Length <= 30 ? Eval("QuestionContent") : Eval("QuestionContent").ToString().Substring(0, 30) + "..."%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="问题类型">
                <HeaderStyle Width="15%" />
            </pe:TemplateField>
            <pe:TemplateField HeaderText="常规操作">
                <HeaderStyle Width="20%" />
                <ItemTemplate>
                    <pe:ExtendedAnchor id="LnkModify" IsChecked="true" runat="server" OperateCode="SurveyCreate"
                        href='<%# "Question.aspx?Action=Modify&SurveyID="+surveyId+"&QuestionID="+ Eval("QuestionID") %>'
                        Disabled='<%# (int)isOpen == 1 %>'>修改</pe:ExtendedAnchor>
                    <pe:ExtendedAnchor id="LnkDelete" IsChecked="true" runat="server" OperateCode="SurveyCreate"
                        onclick="return confirm('确定要删除此问题吗？');" href='<%# AppendSecurityCode("QuestionManage.aspx?Action=Delete&SurveyID="+surveyId+"&QuestionID="+ Eval("QuestionID")) %>'
                        Disabled='<%# (int)isOpen == 1 %>'>删除</pe:ExtendedAnchor>
                    <pe:ExtendedAnchor IsChecked="true" runat="server" OperateCode="SurveyCreate" href='<%# string.Format("QuestionPreview.aspx?SurveyID={0}&QuestionID={1}&QuestionType={2}&IsOpen={3}",surveyId,Eval("QuestionID"),Eval("QuestionType"),isOpen) %>'>预览</pe:ExtendedAnchor>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="排序" SortExpression="Disabled">
                <ItemTemplate>
                    <asp:DropDownList ID="DropOrderId" runat="server">
                    </asp:DropDownList>
                </ItemTemplate>
            </pe:TemplateField>
        </Columns>
    </pe:ExtendedGridView>
    注意：问卷只有在非“启用”的状态下才可以对它所属的问题进行更改。<br />
    <div style="text-align: center">
        <pe:ExtendedButton IsChecked="true" OperateCode="SurveyCreate" ID="BtnAddQuestion"
            runat="server" OnClick="BtnAddQuestion_Click" Text="添加问题" />
        <pe:ExtendedButton IsChecked="true" OperateCode="SurveyCreate" ID="BtnDel" runat="server"
            Text="批量删除选定的问题" CausesValidation="False" OnClientClick="return confirm('确实要删除选中的问题？');"
            OnClick="BtnDel_Click" />
        <pe:ExtendedButton IsChecked="true" OperateCode="SurveyCreate" ID="BtnSetOrderId"
            runat="server" OnClick="BtnOrderId_Click" Text="保存排序" />
    </div>
    <asp:ObjectDataSource ID="OdsQuetion" runat="server" SelectMethod="GetFieldList"
        MaximumRowsParameterName="maxNumberRows" StartRowIndexParameterName="startRowIndexId"
        TypeName="EasyOne.Survey.SurveyField">
        <SelectParameters>
            <asp:QueryStringParameter Name="surveyId" QueryStringField="surveyId" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
