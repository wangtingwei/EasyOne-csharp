<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Survey.SurveyManage" Title="调查问卷管理" Codebehind="SurveyManage.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" runat="server" SiteMapProvider="AdminMapProvider" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <pe:ExtendedGridView ID="EgvSurvey" runat="server" AllowPaging="True" AutoGenerateColumns="False"
        DataSourceID="OdsSurvey" SerialText="" AutoGenerateCheckBoxColumn="True" DataKeyNames="SurveyId"
        EmptyDataText="找不到任何问卷！" OnRowDataBound="EgvSurvey_RowDataBound" OnRowCommand="EgvSurvey_RowCommand"
        RowDblclickBoundField="SurveyId" 
        RowDblclickUrl="Survey.aspx?Action=Modify&amp;SurveyId={$Field}">
        <Columns>
            <pe:TemplateField HeaderText="问卷名称">
                <itemstyle horizontalalign="Left" />
                <ItemTemplate>
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='SurveyCode.aspx?Keyword=<%#Eval("SurveyName")%>' Text='<%#Eval("SurveyName")%>'></asp:HyperLink>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="创建日期/截止日期">
                <HeaderStyle Width="18%" />
            </pe:TemplateField>
            <pe:TemplateField HeaderText="问卷状态">
                <HeaderStyle Width="12%" />
            </pe:TemplateField>
            <pe:TemplateField HeaderText="常规管理操作">
                <HeaderStyle Width="23%" />
                <ItemTemplate>
                    <pe:ExtendedAnchor IsChecked="true" id="LnkModify" runat="server" OperateCode="SurveyManage"
                        href='<%# Eval("SurveyId","Survey.aspx?Action=Modify&SurveyID={0}") %>'>修改</pe:ExtendedAnchor>
                    <pe:ExtendedLinkButton IsChecked="true" IsVisible="true" OperateCode="SurveyQuestionnaireManage"
                        ID="LbtnDelete" runat="server" OnClientClick="if(!disabled)return confirm('删除后将删除该问卷页面和所有题目，确定要删除此问卷吗？')"
                        CausesValidation="False" CommandName="Del" CommandArgument='<%#Eval("SurveyId")%>'
                        Enabled='<%# (int)Eval("IsOpen") != 1 %>'>删除</pe:ExtendedLinkButton>
                    <pe:ExtendedLinkButton IsChecked="true" IsVisible="true" OperateCode="SurveyQuestionnaireManage"
                        ID="LbtnOpenUse" runat="server" CausesValidation="False" Enabled='<%#(string)Eval("FileName")==null || (string)Eval("FileName")=="" ? false:true %>'
                        CommandName='<%#"SetState" + Eval("IsOpen")%>' CommandArgument='<%#Eval("SurveyId")%>'><%#Convert.ToInt32(Eval("IsOpen")) == 1 ? "禁用" : "启用"%></pe:ExtendedLinkButton>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="题目操作">
                <HeaderStyle Width="9%" />
                <ItemTemplate>
                    <pe:ExtendedAnchor IsVisible="true" OperateCode="SurveyQuestionnaireManage" IsChecked="true" id="LnkAdd"
                        runat="server" Disabled='<%# (int)Eval("IsOpen") == 1 %>' href='<%# Eval("SurveyId","Question.aspx?Action=Add&SurveyID={0}") %>'>添加</pe:ExtendedAnchor>
                    <pe:ExtendedAnchor IsChecked="true" runat="server" OperateCode="SurveyQuestionnaireManage"
                        href='<%# Eval("SurveyId","QuestionManage.aspx?SurveyID={0}") %>' id="LnkList">列表</pe:ExtendedAnchor>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="HTML页操作">
                <HeaderStyle Width="12%" />
                <ItemTemplate>
                    <pe:ExtendedAnchor runat="server" id="LnkCreate" IsChecked="true" OperateCode="SurveyCreate"
                        href='<%# Eval("SurveyId", "SurveyFormCreate.aspx?SurveyID={0}") %>'>创建</pe:ExtendedAnchor>
                    <pe:ExtendedAnchor id="LnkEdit" runat="server" IsVisible="true" IsChecked="true"
                        OperateCode="SurveyTemplateManage" Disabled='<%# (int)Eval("IsOpen") != 2 %>'
                        href='<%# Eval("SurveyId","SurveyFormEdit.aspx?SurveyID={0}") %>'>编辑</pe:ExtendedAnchor>
                    <pe:ExtendedAnchor OperateCode="SurveyQuestionnaireManage" id="LnkPreview" runat="server" IsChecked="true" target="_blank"
                        IsVisible="true" ToolTip='<%#(string)Eval("FileName")==null || (string)Eval("FileName")=="" ? "问卷未创建":"文件名" + (string)Eval("FileName") %>'
                        Disabled='<%# (int)Eval("IsOpen") ==0 %>' href='<%# Eval("FileName")==null || (string)Eval("FileName")=="" ? "":"~/Survey/"+Eval("FileName").ToString() %>'>预览</pe:ExtendedAnchor>
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
    注意：更改过问卷题目列表的，只有重新“创建”页面才能看到更改后的效果。<br />
    <pe:ExtendedButton IsChecked="true" OperateCode="SurveyQuestionnaireManage" ID="BtnDelete" runat="server"
        OnClick="BtnDelete_Click" OnClientClick="return confirm('删除后将删除该问卷页面和所有题目，确定要删除此问卷吗？')"
        Text="删除选定的问卷" />
</asp:Content>
