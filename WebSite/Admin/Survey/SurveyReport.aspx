<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Survey.SurveyReport" Codebehind="SurveyReport.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table width="100%" border="0" cellpadding="2" cellspacing="1">
                <tr>
                    <td align="center">
                        <br />
                        <%--<asp:Literal ID="LtrTitle" runat="server"></asp:Literal>--%>
                        <asp:Label ID="LblTitle" runat="server" Font-Size="Larger"></asp:Label>
                    </td>
                </tr>
            </table>
            <br />
            <table width="100%" border="0" class="border" cellpadding="2" cellspacing="1">
                <tr class="tdbg">
                    <td style="width: 30%">
                        问卷名：
                    </td>
                    <td style="width: 70%">
                        <asp:Label ID="LblSurveyName" runat="server" Text="Lbl" Font-Size="Larger"></asp:Label>
                    </td>
                </tr>
                <tr class="tdbg">
                    <td style="width: 30%">
                        问卷说明：
                    </td>
                    <td style="width: 70%">
                        <asp:Label ID="LblDescription" runat="server" Text="Lbl" Font-Size="Larger"></asp:Label>
                    </td>
                </tr>
                <tr class="tdbg">
                    <td style="width: 30%">
                        创建日期/结束日期：
                    </td>
                    <td style="width: 70%">
                        <asp:Label ID="LblDate" runat="server" Text="Lbl" Font-Size="Larger"></asp:Label>
                    </td>
                </tr>
                <tr class="tdbg">
                    <td style="width: 30%">
                        问卷记录数：
                    </td>
                    <td style="width: 70%">
                        <asp:Label ID="LblSurveyNumber" runat="server" Text="Lbl" Font-Size="Larger"></asp:Label>
                    </td>
                </tr>
                <tr class="tdbg">
                    <td style="width: 30%">
                        问卷题目：
                    </td>
                    <td style="width: 70%">
                        <asp:Repeater ID="RptSurveyQuestion" runat="server" OnItemDataBound="RptSurveyQuestion_ItemDataBound"
                            DataSourceID="OdsSurveyQuestion" OnItemCommand="RptSurveyQuestion_ItemCommand">
                            <ItemTemplate>
                                <br />
                                <asp:Label ID="LblQuestion" runat="server" Text="Label"></asp:Label><br />
                                <asp:Label ID="LblOptions" runat="server" Text="Label"></asp:Label>
                            </ItemTemplate>
                        </asp:Repeater>
                        <asp:ObjectDataSource ID="OdsSurveyQuestion" runat="server" SelectMethod="GetFieldList"
                            TypeName="EasyOne.Survey.SurveyField">
                            <SelectParameters>
                                <asp:QueryStringParameter DefaultValue="0" Name="surveyId" QueryStringField="SurveyID"
                                    Type="Int32" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </td>
                </tr>
                <tr class="tdbg">
                    <td style="width: 30%">
                        数据统计：
                    </td>
                    <td style="width: 70%">
                        <asp:Repeater ID="RptSurveyVote" runat="server" DataSourceID="OdsQuestionContent"
                            OnItemDataBound="RptSurveyVote_ItemDataBound">
                            <ItemTemplate>
                                <asp:Label ID="LblQuestionContent" runat="server" Text="LblQuestionContent" />
                                <asp:HyperLink ID="LnkListAnswer" runat="server"></asp:HyperLink><br />
                                <asp:PlaceHolder ID="PlhQuestion" runat="server" />
                                <br />
                            </ItemTemplate>
                        </asp:Repeater>
                        <asp:ObjectDataSource ID="OdsQuestionContent" runat="server" SelectMethod="GetFieldList"
                            TypeName="EasyOne.Survey.SurveyField">
                            <SelectParameters>
                                <asp:QueryStringParameter Type="Int32" DefaultValue="0" Name="surveyId" QueryStringField="SurveyID">
                                </asp:QueryStringParameter>
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </td>
                </tr>
            </table>
            <br />
            <table width="100%" border="0" cellpadding="2" cellspacing="1">
                <tr>
                    <td align="center" style="height: 24px">
                        <pe:ExtendedButton IsChecked="true" OperateCode="SurveyResultPreview" ID="BtnReport"
                            runat="server" Text="打 印… " UseSubmitBehavior="False" />&nbsp;
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
