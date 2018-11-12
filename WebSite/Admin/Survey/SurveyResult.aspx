<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Survey.SurveyResult" Title="�����ʾ����" Codebehind="SurveyResult.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" runat="server" SiteMapProvider="AdminMapProvider" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <pe:ExtendedGridView ID="EgvSurvey" runat="server" AllowPaging="True" AutoGenerateColumns="False"
        DataSourceID="OdsSurvey" SerialText="" DataKeyNames="SurveyId" EmptyDataText="�Ҳ����κ��ʾ�"
        OnRowDataBound="EgvSurvey_RowDataBound">
        <Columns>
            <pe:BoundField DataField="SurveyName" HeaderText="�ʾ�����">
                <itemstyle horizontalalign="Left" />
            </pe:BoundField>
            <pe:TemplateField HeaderText="��������/��ֹ����">
                <HeaderStyle Width="18%" />
            </pe:TemplateField>
            <pe:TemplateField HeaderText="�����������">
                <HeaderStyle Width="35%" />
                <ItemTemplate>
                    <pe:ExtendedAnchor IsChecked="true" IsVisible="true" runat="server" OperateCode="SurveyResultPreview"
                        id="LnkSurveyCountData" href='<%# "ShowCountData.aspx?SurveyID="+Eval("SurveyId")+"&SurveyName="+ Server.UrlEncode(Convert.ToString(Eval("SurveyName"))) %>'
                        Disabled='<%# (int)Eval("IsOpen") != 1 %>'>������</pe:ExtendedAnchor>
                    <pe:ExtendedAnchor IsChecked="true" runat="server" OperateCode="SurveyResultPreview"
                        href='<%# Eval("SurveyId","ShowCountData3.aspx?SurveyID={0}") %>'>�б�ʽ�鿴</pe:ExtendedAnchor>
                    <pe:ExtendedAnchor IsChecked="true" IsVisible="true" runat="server" id="LnkSurveyCountDataPie"
                        OperateCode="SurveyResultPreview" href='<%# "ShowCountData.aspx?ShowType=Pie&SurveyID="+Eval("SurveyId")+"&SurveyName="+Server.UrlEncode(Convert.ToString(Eval("SurveyName"))) %>'
                        Disabled='<%# (int)Eval("IsOpen") != 1 %>'>����ͼ</pe:ExtendedAnchor>
                    <br />
                    <pe:ExtendedAnchor IsChecked="true" runat="server" OperateCode="SurveyResultPreview"
                        href='<%# string.Format("QuestionDetail.aspx?SurveyID={0}&SurveyName={1}",Eval("SurveyId"),Server.UrlEncode(Convert.ToString(Eval("SurveyName")))) %>'>�ش�����</pe:ExtendedAnchor>
                    <pe:ExtendedAnchor IsChecked="true" runat="server" OperateCode="SurveyResultPreview"
                        href='<%# Eval("SurveyId","ShowCountData2.aspx?SurveyID={0}") %>'>��Ƭʽ�鿴</pe:ExtendedAnchor>
                    <pe:ExtendedAnchor id="LnkSurveyCountDataBar" IsChecked="true" runat="server" OperateCode="SurveyResultPreview"
                        IsVisible="true" href='<%# "ShowCountData.aspx?ShowType=Bar&SurveyID="+Eval("SurveyId")+"&SurveyName="+Server.UrlEncode(Convert.ToString(Eval("SurveyName"))) %>'
                        Disabled='<%# (int)Eval("IsOpen") != 1 %>'>����ͼ</pe:ExtendedAnchor>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="�������">
                <HeaderStyle Width="14%" />
                <ItemTemplate>
                    <pe:ExtendedAnchor id="LnkReport" IsVisible="true" IsChecked="true" runat="server"
                        OperateCode="SurveyResultPreview" href='<%# "SurveyReport.aspx?SurveyID="+Eval("SurveyId") %>'
                        Disabled='<%# (int)Eval("IsOpen") != 1 %>'>����</pe:ExtendedAnchor>
                    <pe:ExtendedAnchor id="LnkSurveyResultPie" IsVisible="true" IsChecked="true" runat="server"
                        OperateCode="SurveyResultPreview" href='<%# "SurveyReport.aspx?ShowType=Pie&SurveyID="+Eval("SurveyId") %>'
                        Disabled='<%# (int)Eval("IsOpen") != 1 %>'>����ͼ</pe:ExtendedAnchor>
                    <pe:ExtendedAnchor id="LnkSurveyResultBar" IsVisible="true" IsChecked="true" runat="server"
                        OperateCode="SurveyResultPreview" href='<%# "SurveyReport.aspx?ShowType=Bar&SurveyID="+Eval("SurveyId") %>'
                        Disabled='<%# (int)Eval("IsOpen") != 1 %>'>����ͼ</pe:ExtendedAnchor>
                </ItemTemplate>
            </pe:TemplateField>
            <%--            <pe:TemplateField HeaderText="HTMLҳ����">
                <headerstyle width="12%" />
                <itemtemplate>
<asp:HyperLink id="LnkCreate" runat="server" NavigateUrl='<%# "SurveyFormCreate.aspx?SurveyID="+Eval("SurveyId") %>'>����</asp:HyperLink> <asp:HyperLink id="LnkEdit" runat="server" Enabled='<%# (int)Eval("IsOpen") == 0 %>' NavigateUrl='<%# "SurveyFormEdit.aspx?SurveyID="+Eval("SurveyId") %>'>�༭</asp:HyperLink> <asp:HyperLink id="LnkPreview" runat="server" Enabled='<%# (int)Eval("IsOpen") ==0 %>' NavigateUrl='<%# Eval("FileName")==null?"":"~/Survey/"+Eval("FileName").ToString() %>'>Ԥ��</asp:HyperLink> 
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
    <asp:Button ID="BtnDelete" runat="server" OnClick="BtnDelete_Click" OnClientClick="return confirm('ɾ����ɾ�����ʾ�ҳ���������Ŀ��ȷ��Ҫɾ�����ʾ���')"
        Text="ɾ��ѡ�����ʾ�" />--%>
</asp:Content>
