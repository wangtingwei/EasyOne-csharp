<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" Inherits="EasyOne.WebSite.Admin.Survey.SurveyFormEdit" Codebehind="SurveyFormEdit.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
                <tr align="center">
                    <td class="spacingtitle">
                        <b>编辑页面</b></td>
                </tr>
                <tr class="tdbg" id="showAlgebra">
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <table width="100%">
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <textarea name="SurveyContent" id="SurveyContent" rows="30" cols="117" class="txt_main"
                                                    runat="server"></textarea>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr class="tdbg" id="showeditor" style="display: none">
                    <td valign="top">
                        <table>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <textarea name="SurveyEditContent" id="SurveyEditContent" rows="30" cols="117" style="display: none">
</textarea>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr class="tdbg">
                    <td>
                        <table>
                            <tr>
                                <td>
                                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="BtnCodeMode" runat="server" Text="代码模式" />
                                    <asp:Button ID="BtnEditMode" runat="server" Text="编辑模式" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr class="tdbg">
                    <td align="left">
                        &nbsp;&nbsp;&nbsp;&nbsp; 问卷文件名：&nbsp;&nbsp;&nbsp;&nbsp;<asp:Literal ID="LtrFileName"
                            runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr class="tdbg">
                    <td align="center">
                        <asp:HiddenField ID="HdnSurveyId" runat="server" />
                        <pe:ExtendedButton IsChecked="true" OperateCode="SurveyTemplateManage" ID="BtnSave"
                            runat="server" Text="保存修改" OnClick="BtnSave_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
