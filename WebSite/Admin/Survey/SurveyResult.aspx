<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Survey.SurveyResult" Title="调查问卷管理" Codebehind="SurveyResult.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" runat="server" SiteMapProvider="AdminMapProvider" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <pe:ExtendedGridView ID="EgvSurvey" runat="server" AllowPaging="True" AutoGenerateColumns="False"
        DataSourceID="OdsSurvey" SerialText="" DataKeyNames="SurveyId" EmptyDataText="找不到任何问卷！"
        OnRowDataBound="EgvSurvey_RowDataBound">
        <Columns>
            <pe:BoundField DataField="SurveyName" HeaderText="问卷名称">
                <itemstyle horizontalalign="Left" />
            </pe:BoundField>
            <pe:TemplateField HeaderText="创建日期/截止日期">
                <HeaderStyle Width="18%" />
            </pe:TemplateField>
            <pe:TemplateField HeaderText="结果分析管理">
                <HeaderStyle Width="35%" />
                <ItemTemplate>
                    <pe:ExtendedAnchor IsChecked="true" IsVisible="true" runat="server" OperateCode="SurveyResultPreview"
                        id="LnkSurveyCountData" href='<%# "ShowCountData.aspx?SurveyID="+Eval("SurveyId")+"&SurveyName="+ Server.UrlEncode(Convert.ToString(Eval("SurveyName"))) %>'
                        Disabled='<%# (int)Eval("IsOpen") != 1 %>'>调查结果</pe:ExtendedAnchor>
                    <pe:ExtendedAnchor IsChecked="true" runat="server" OperateCode="SurveyResultPreview"
                        href='<%# Eval("SurveyId","ShowCountData3.aspx?SurveyID={0}") %>'>列表式查看</pe:ExtendedAnchor>
                    <pe:ExtendedAnchor IsChecked="true" IsVisible="true" runat="server" id="LnkSurveyCountDataPie"
                        OperateCode="SurveyResultPreview" href='<%# "ShowCountData.aspx?ShowType=Pie&SurveyID="+Eval("SurveyId")+"&SurveyName="+Server.UrlEncode(Convert.ToString(Eval("SurveyName"))) %>'
                        Disabled='<%# (int)Eval("IsOpen") != 1 %>'>饼形图</pe:ExtendedAnchor>
                    <br />
                    <pe:ExtendedAnchor IsChecked="true" runat="server" OperateCode="SurveyResultPreview"
                        href='<%# string.Format("QuestionDetail.aspx?SurveyID={0}&SurveyName={1}",Eval("SurveyId"),Server.UrlEncode(Convert.ToString(Eval("SurveyName")))) %>'>回答详情</pe:ExtendedAnchor>
                    <pe:ExtendedAnchor IsChecked="true" runat="server" OperateCode="SurveyResultPreview"
                        href='<%# Eval("SurveyId","ShowCountData2.aspx?SurveyID={0}") %>'>卡片式查看</pe:ExtendedAnchor>
                    <pe:ExtendedAnchor id="LnkSurveyCountDataBar" IsChecked="true" runat="server" OperateCode="SurveyResultPreview"
                        IsVisible="true" href='<%# "ShowCountData.aspx?ShowType=Bar&SurveyID="+Eval("SurveyId")+"&SurveyName="+Server.UrlEncode(Convert.ToString(Eval("SurveyName"))) %>'
                        Disabled='<%# (int)Eval("IsOpen") != 1 %>'>柱形图</pe:ExtendedAnchor>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="报表管理">
                <HeaderStyle Width="14%" />
                <ItemTemplate>
                    <pe:ExtendedAnchor id="LnkReport" IsVisible="true" IsChecked="true" runat="server"
                        OperateCode="SurveyResultPreview" href='<%# "SurveyReport.aspx?SurveyID="+Eval("SurveyId") %>'
                        Disabled='<%# (int)Eval("IsOpen") != 1 %>'>报表</pe:ExtendedAnchor>
                    <pe:ExtendedAnchor id="LnkSurveyResultPie" IsVisible="true" IsChecked="true" runat="server"
                        OperateCode="SurveyResultPreview" href='<%# "SurveyReport.aspx?ShowType=Pie&SurveyID="+Eval("SurveyId") %>'
                        Disabled='<%# (int)Eval("IsOpen") != 1 %>'>饼形图</pe:ExtendedAnchor>
                    <pe:ExtendedAnchor id="LnkSurveyResultBar" IsVisible="true" IsChecked="true" runat="server"
                        OperateCode="SurveyResultPreview" href='<%# "SurveyReport.aspx?ShowType=Bar&SurveyID="+Eval("SurveyId") %>'
                        Disabled='<%# (int)Eval("IsOpen") != 1 %>'>柱形图</pe:ExtendedAnchor>
                </ItemTemplate>
            </pe:TemplateField>
            <%--            <pe:TemplateField HeaderText="HTML页操作">
                <headerstyle width="12%" />
                <itemtemplate>
<asp:HyperLink id="LnkCreate" runat="server" NavigateUrl='<%# "SurveyFormCreate.aspx?SurveyID="+Eval("SurveyId") %>'>创建</asp:HyperLink> <asp:HyperLink id="LnkEdit" runat="server" Enabled='<%# (int)Eval("IsOpen") == 0 %>' NavigateUrl='<%# "SurveyFormEdit.aspx?SurveyID="+Eval("SurveyId") %>'>编辑</asp:HyperLink> <asp:HyperLink id="LnkPreview" runat="server" Enabled='<%# (int)Eval("IsOpen") ==0 %>' NavigateUrl='<%# Eval("FileName")==null?"":"~/Survey/"+Eval("FileName").ToString() %>'>预览</asp:HyperLink> 
</itemtemplate>
            </pe:TemplateField>--%>
        </Columns>
    </pe:ExtendedGridView>
    <asp:ObjectDataSource ID="OdsSurvey" runat="server" DeleteMethod="Delete" EnablePaging="True"
        MaximumRowsParameterName="maxNumberRows" SelectCountMethod="GetTotalOfSurvey"
        SelectMethod="GetList" StartRowIndexParameterName="startRowIndexId" TypeName="EasyOne.Survey.SurveyManager">
        <DeleteParameters>
            <asp:Parameter Name="surveyId" Type="String" />
        </DeleteParameters>
        <SelectParameters>
            <asp:QueryStringParameter Name="searchType" QueryStringField="SearchType" Type="Int32" />
            <asp:QueryStringParameter Name="keyword" QueryStringField="Keyword" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <%--    <br />
    <asp:Button ID="BtnDelete" runat="server" OnClick="BtnDelete_Click" OnClientClick="return confirm('删除后将删除该问卷页面和所有题目，确定要删除此问卷吗？')"
        Text="删除选定的问卷" />--%>
</asp:Content>
