<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Survey.ShowCountData" Title="�鿴ͳ�ƽ��" Codebehind="ShowCountData.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td style="width: 60%">
                <pe:ExtendedSiteMapPath ID="SmpNavigator" runat="server" SiteMapProvider="AdminMapProvider" />
            </td>
            <td style="width: 40%; text-align: right">
                <asp:Label ID="LblSurveyName" runat="server"></asp:Label>&nbsp;</td>
        </tr>
    </table>
    <table border="0" cellpadding="2" cellspacing="1" class="border tdbg" style="width: 100%">
        <tr>
            <td style="width: 100px; height: 23px; text-align: right">
                <strong>�鿴�����</strong></td>
            <td align="left" style="height: 23px">
                &nbsp;<a href='<%="ShowCountData.aspx?SurveyID="+HdnSurveyId.Value+"&SurveyName="+Server.UrlEncode(Convert.ToString(RequestString("SurveyName"))) %>'>����������</a>
                | <a href='<%="ShowCountData.aspx?ShowType=Pie&SurveyID="+HdnSurveyId.Value+"&SurveyName="+Server.UrlEncode(Convert.ToString(RequestString("SurveyName"))) %>'>
                    ����ͼ��ʾ</a> | <a href='<%="ShowCountData.aspx?ShowType=Bar&SurveyID="+HdnSurveyId.Value+"&SurveyName="+Server.UrlEncode(Convert.ToString(RequestString("SurveyName"))) %>'>
                        ����ͼ��ʾ</a> | <a href="ShowCountData3.aspx?SurveyID=<%=HdnSurveyId.Value%>">�б�ʽ�鿴</a>
                | <a href="ShowCountData2.aspx?SurveyID=<%=HdnSurveyId.Value%>">��Ƭʽ�鿴</a> | <a href='<%="QuestionDetail.aspx?SurveyID="+HdnSurveyId.Value+"&SurveyName="+Server.UrlEncode(Convert.ToString(RequestString("SurveyName"))) %>'>
                    �鿴����Ļش�����</a>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <asp:Repeater ID="RptShowCountData" runat="server" DataSourceID="OdsShowCountData"
        OnItemDataBound="RptShowCountData_ItemDataBound">
        <ItemTemplate>
            <asp:Label ID="LblQuestionContent" runat="server" Text="LblQuestionContent" />
            <asp:HyperLink ID="LnkListAnswer" runat="server">LnkListAnswer</asp:HyperLink><br />
            <asp:PlaceHolder ID="PlhQuestion" runat="server" />
            <br />
        </ItemTemplate>
    </asp:Repeater>
    &nbsp; &nbsp;&nbsp; &nbsp;
    <asp:ObjectDataSource ID="OdsShowCountData" runat="server" SelectMethod="GetFieldList"
        TypeName="EasyOne.Survey.SurveyField">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="0" Name="surveyId" QueryStringField="SurveyID"
                Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:HiddenField ID="HdnSurveyId" runat="server" />
    <asp:HiddenField ID="HdnShowType" runat="server" />
    <div style="width: 100%; text-align: center">
        <input id="btnReturn" type="button" class="inputbutton" value="������һҳ" onclick="JavaScript:history.go(-1);" /></div>
</asp:Content>
