<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" Inherits="EasyOne.WebSite.Admin.Survey.SurveyFormCreate" Codebehind="SurveyFormCreate.aspx.cs" %>

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
                        <b>创建问卷</b></td>
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
                                    &nbsp;&nbsp;&nbsp;&nbsp; 页面类型：</td>
                                <td>
                                    <asp:RadioButtonList ID="RadlPageType" RepeatDirection="Horizontal" AutoPostBack="true" 
                                        OnTextChanged="RadlPageType_TextChanged" runat="server">
                                        <asp:ListItem Value="0" Selected="True">静态页</asp:ListItem>
                                        <asp:ListItem Value="1">动态页</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr class="tdbg">
                    <td align="left">
                        &nbsp;&nbsp;&nbsp;&nbsp; 问卷文件名：&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:TextBox ID="TxtFileName" runat="server"></asp:TextBox>
                        <pe:RequiredFieldValidator ID="ValrFileName" runat="server" ErrorMessage="问卷文件名不能为空！"
                            SetFocusOnError="true" Display="dynamic" ControlToValidate="TxtFileName"></pe:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="ValeFileName" ValidationExpression="\w+.(html|aspx)"
                            runat="server" ErrorMessage="请输入指定格式的文件名！" ControlToValidate="TxtFileName" SetFocusOnError="true"
                            Display="dynamic"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr class="tdbg">
                    <td align="center">
                        <asp:HiddenField ID="HdnSurveyId" runat="server" />
                        <asp:HiddenField ID="HdnShortFileName" runat="server" />
                        <asp:HiddenField ID="HdnFileName" runat="server" />
                        <pe:ExtendedButton IsChecked="true" OperateCode="SurveyCreate" ID="BtnCreate" runat="server"
                            Text="创建问卷文件" OnClick="BtnCreate_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>


