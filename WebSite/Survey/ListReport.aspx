<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Survey.ListReport" Codebehind="ListReport.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>问卷调查</title>
</head>
<body>
    <form id="form1" runat="server">
        <table width="90%" border="0" align="center" class="border tdbg" cellpadding="0" cellspacing="0">
            <tr>
                <td align="center"><asp:Label ID="LblSurveyName" runat="server" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td  style="padding:8px; border-left:#999999 solid 1px;border-right:#999999 solid 1px;">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                  <tr>
                    <td>
                        <table border="0" cellpadding="2" cellspacing="1" class="border tdbg" style="width: 100%;">
                            <tr>
                                <td style="width: 100px; height: 23px; text-align: right">
                                    <strong>查看结果：</strong></td>
                                <td align="left" style="height: 23px">
                                      <a href='<%="ListReport.aspx?ShowType=0&SurveyID="+RequestInt32("SurveyID") %>'>列表显示</a>
                                    | <a href='<%="ListReport.aspx?ShowType=1&SurveyID="+RequestInt32("SurveyID") %>'>饼形图显示</a> 
                                    | <a href='<%="ListReport.aspx?ShowType=2&SurveyID="+RequestInt32("SurveyID") %>'>柱形图显示</a> 
                                    <%--| <a href='<%="ListReport.aspx?ShowType=3&SurveyID="+RequestInt32("SurveyID") %>'>列表式查看</a>
                                    | <a href='<%="ListReport.aspx?ShowType=4&SurveyID="+RequestInt32("SurveyID") %>'>卡片式查看</a> 
                                    | <a href='<%="ListReport.aspx?ShowType=5&SurveyID="+RequestInt32("SurveyID") %>'>查看问题的回答详情</a>--%>
                                </td>
                            </tr>
                        </table>
                        
                        <div id="DivShowData" runat="server" visible="true">
                            <asp:Repeater ID="RptShowCountData" runat="server" DataSourceID="OdsShowCountData"
                                OnItemDataBound="RptShowCountData_ItemDataBound">
                                <ItemTemplate>
                                    <asp:Label ID="LblQuestionContent" runat="server" Text="LblQuestionContent" /><br />
                                    <asp:PlaceHolder ID="PlhQuestion" runat="server"></asp:PlaceHolder>
                                    <%--<asp:Repeater ID="RptAnswerList" Visible="false" runat="server" OnItemDataBound="RptAnswerList_ItemDataBound" >
                                    <ItemTemplate>
                                        <asp:Literal ID="LtrAnswerList" runat="server"></asp:Literal>
                                    </ItemTemplate>
                                    </asp:Repeater>
                                    <pe:AspNetPager ID="Pager" Visible="false" runat="server" OnPageChanged="Pager_PageChanged"></pe:AspNetPager>--%>
                                </ItemTemplate>
                            </asp:Repeater>
                            <asp:ObjectDataSource ID="OdsShowCountData" runat="server" SelectMethod="GetFieldList"
                                TypeName="EasyOne.Survey.SurveyField">
                                <SelectParameters>
                                    <asp:QueryStringParameter DefaultValue="0" Name="surveyId" QueryStringField="SurveyID"
                                        Type="Int32" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </div>
                        
                        <asp:HiddenField ID="HdnSurveyId" runat="server" />
                        <div style="width: 100%; text-align: center">
                            <br />
                            <input id="BtnReturn" type="button" runat="server" class="inputbutton" value="返回" onclick="javaScript:history.go(-1);" />
                            <input id="BtnClose" type="button" class="inputbutton" value="关闭" onclick="javascript:window.close();" />
                        </div>
                    </td>
                  </tr>
                </table>
                </td>
                </tr>
            <tr>
                <td style="background:#0149a8;"></td>
            </tr>
        </table>
    </form>
</body>
</html>
