<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" Inherits="EasyOne.WebSite.Admin.Survey.SurveyFormCreate" Codebehind="SurveyFormCreate.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>�ޱ���ҳ</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
                <tr align="center">
                    <td class="spacingtitle">
                        <b>�����ʾ�</b></td>
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
                                    &nbsp;&nbsp;&nbsp;&nbsp; ҳ�����ͣ�</td>
                                <td>
                                    <asp:RadioButtonList ID="RadlPageType" RepeatDirection="Horizontal" AutoPostBack="true" 
                                        OnTextChanged="RadlPageType_TextChanged" runat="server">
                                        <asp:ListItem Value="0" Selected="True">��̬ҳ</asp:ListItem>
                                        <asp:ListItem Value="1">��̬ҳ</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr class="tdbg">
                    <td align="left">
                        &nbsp;&nbsp;&nbsp;&nbsp; �ʾ��ļ�����&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:TextBox ID="TxtFileName" runat="server"></asp:TextBox>
                        <pe:RequiredFieldValidator ID="ValrFileName" runat="server" ErrorMessage="�ʾ��ļ�������Ϊ�գ�"
                            SetFocusOnError="true" Display="dynamic" ControlToValidate="TxtFileName"></pe:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="ValeFileName" ValidationExpression="\w+.(html|aspx)"
                            runat="server" ErrorMessage="������ָ����ʽ���ļ�����" ControlToValidate="TxtFileName" SetFocusOnError="true"
                            Display="dynamic"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr class="tdbg">
                    <td align="center">
                        <asp:HiddenField ID="HdnSurveyId" runat="server" />
                        <asp:HiddenField ID="HdnShortFileName" runat="server" />
                        <asp:HiddenField ID="HdnFileName" runat="server" />
                        <pe:ExtendedButton IsChecked="true" OperateCode="SurveyCreate" ID="BtnCreate" runat="server"
                            Text="�����ʾ��ļ�" OnClick="BtnCreate_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>


