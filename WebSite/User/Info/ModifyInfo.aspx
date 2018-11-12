<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.User.Info.ModifyInfo" Codebehind="ModifyInfo.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>�޸ĸ�����Ϣ</title>
</head>
<body>
    <pe:UserNavigation Tab="user" ID="UserCenterNavigation" runat="server" />
    <pe:ExtendedSiteMapPath ID="YourPosition" SiteMapProvider="UserMapProvider" runat="server" />
    <form id="form1" runat="server">
        <asp:ScriptManager ID="SmgeRegion" runat="server" EnablePartialRendering="true" />

        <script language="javascript" type="text/javascript">
                function ShowTabs(ID){
                   for (i=0;i< 5;i++){
                        if(i == ID){
                            document.getElementById("TabTitle" + i).className="titlemouseover";
                            document.getElementById("Tabs" + i).style.display="";
                        }
                        else{
                            document.getElementById("TabTitle" + i).className="tabtitle";
                            document.getElementById("Tabs" + i).style.display="none";
                        }
                   }
                } 
        </script>

        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr align="center">
                <td id="TabTitle0" class="titlemouseover" onclick="ShowTabs(0)">
                    ��Ա��Ϣ</td>
                <td id="TabTitle1" class="tabtitle" onclick="ShowTabs(1)">
                    ��ϵ��Ϣ</td>
                <td id="TabTitle2" class="tabtitle" onclick="ShowTabs(2)">
                    ������Ϣ</td>
                <td id="TabTitle3" class="tabtitle" onclick="ShowTabs(3)">
                    ҵ����Ϣ</td>
                <td id="TabTitle4" class="tabtitle" style="<%= IsShow()%>" onclick="ShowTabs(4)">
                    ��λ��Ϣ</td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
        <table width="100%" border="0" cellpadding="5" cellspacing="1" class="border">
            <tr class="tdbg">
                <td style="width: 100%;" valign="top">
                    <table cellpadding="2" cellspacing="1" style="width: 100%; background-color: white;">
                        <tbody id="Tabs0">
                            <tr class="tdbg">
                                <td style="width: 12%;" class="tdbgleft" align="right">
                                    �� Ա �飺
                                </td>
                                <td style="width: 38%;">
                                    <asp:Label ID="LblUserGroup" runat="server" Text="" />
                                </td>
                                <td style="width: 12%;" class="tdbgleft" align="right">
                                    ��Ա���
                                </td>
                                <td style="width: 38%;">
                                    <asp:Label ID="LblUserType" runat="server" Text="" />
                                </td>
                            </tr>
                            <tr class="tdbg">
                                <td class="tdbgleft" align="right">
                                    �� �� ����
                                </td>
                                <td>
                                    <asp:Label ID="LblUserName" runat="server" Text="" />
                                </td>
                                <td class="tdbgleft" align="right">
                                    �û����룺
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtUserPassword" runat="server" Width="150" TextMode="Password" />
                                    <span style="color: Red">���޸������գ�</span>
                                </td>
                            </tr>
                            <tr class="tdbg">
                                <td class="tdbgleft" align="right">
                                    ��ʾ���⣺</td>
                                <td>
                                    <asp:TextBox ID="TxtQuestion" runat="server" Width="138px" />
                                    <pe:RequiredFieldValidator ID="ValrQuestion" ControlToValidate="TxtQuestion" runat="server"
                                        ErrorMessage="��ʾ���ⲻ��Ϊ�գ�" Display="Dynamic" />
                                </td>
                                <td class="tdbgleft" align="right">
                                    ��ʾ�𰸣�
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtAnswer" runat="server" Width="150px" />
                                    <span style="color: Red">���޸������գ�</span>
                                </td>
                            </tr>
                            <tr class="tdbg">
                                <td class="tdbgleft" align="right">
                                    �����ʼ���
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtEmail" runat="server" Width="200" />
                                    <pe:RequiredFieldValidator ID="ValrEmail" ControlToValidate="TxtEmail" runat="server"
                                        ErrorMessage="Email����Ϊ�գ�" Display="Dynamic" />
                                    <pe:EmailValidator ID="Vmail" runat="server" ControlToValidate="TxtEmail" Display="Dynamic"
                                        ErrorMessage="�����ʼ���ʽ����" />
                                </td>
                                <td class="tdbgleft" align="right">
                                    ��˽�趨��</td>
                                <td>
                                    <asp:RadioButtonList ID="RadlPrivacySetting" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Selected="True" Value="0">ȫ������</asp:ListItem>
                                        <asp:ListItem Value="1">���ֹ���</asp:ListItem>
                                        <asp:ListItem Value="2">��ȫ����</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr class="tdbg">
                                <td class="tdbgleft" align="right">
                                    ͷ���ַ��
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtUserFace" runat="server" Width="285" />
                                </td>
                                <td class="tdbgleft" align="right">
                                    ͷ���ȣ�
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtFaceWidth" runat="server" Width="78px" />
                                    ����
                                    <pe:NumberValidator ID="NumberValidatorFaceWidth" ControlToValidate="TxtFaceWidth"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr class="tdbg">
                                <td class="tdbgleft" align="right">
                                    ǩ����Ϣ��
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtSign" runat="server" Height="74px" TextMode="MultiLine" Width="288px" />
                                </td>
                                <td class="tdbgleft" align="right">
                                    ͷ��߶ȣ�
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtFaceHeight" runat="server" Width="78px" />
                                    ����
                                    <pe:NumberValidator ID="NumberValidatorFaceHeight" ControlToValidate="TxtFaceHeight"
                                        runat="server" />
                                </td>
                            </tr>
                        </tbody>
                        <tbody id="Tabs1" style="display: none">
                            <tr class="tdbg">
                                <td style="width: 12%;" class="tdbgleft" align="right">
                                    ��ʵ������</td>
                                <td style="width: 38%;">
                                    <asp:TextBox ID="TxtTrueName" runat="server" Width="150px" />
                                </td>
                                <td style="width: 12%;" class="tdbgleft" align="right">
                                    ��ν��</td>
                                <td style="width: 38%;">
                                    <asp:TextBox ID="TxtTitle" runat="server" Width="150px" />
                                </td>
                            </tr>
                            <pe:LiaisonInformation ID="LiaisonInformation1" runat="server" />
                        </tbody>
                        <tbody id="Tabs2" style="display: none">
                            <pe:PersonalInformation ID="PersonalInformation1" runat="server" />
                        </tbody>
                        <tbody id="Tabs3" style="display: none">
                            <tr class="tdbg">
                                <td style="width: 12%;" class="tdbgleft" align="right">
                                    ��λ���ƣ�
                                </td>
                                <td style="width: 38%;">
                                    <asp:TextBox ID="TxtCompany" runat="server" Width="150px" />
                                </td>
                                <td style="width: 12%;" class="tdbgleft" align="right">
                                    �������ţ�
                                </td>
                                <td style="width: 38%;">
                                    <asp:TextBox ID="TxtDepartment" runat="server" Width="150px" />
                                </td>
                            </tr>
                            <tr class="tdbg">
                                <td class="tdbgleft" align="right">
                                    ְλ��
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtPosition" runat="server" Width="150px" />
                                </td>
                                <td class="tdbgleft" align="right">
                                    ����ҵ��
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtOperation" runat="server" Width="150px" />
                                </td>
                            </tr>
                            <tr class="tdbg">
                                <td class="tdbgleft" align="right">
                                    ��λ��ַ��
                                </td>
                                <td colspan="3">
                                    <asp:TextBox ID="TxtCompanyAddress" runat="server" Width="150px" />
                                </td>
                            </tr>
                        </tbody>
                        <tbody id="Tabs4" style="display: none">
                            <pe:Company ID="Company1" runat="server" Visible="false" />
                        </tbody>
                    </table>
                </td>
            </tr>
            <tr class="tdbgbottom">
                <td>
                    <asp:HiddenField ID="HdnContacterID" runat="server" />
                    <pe:ExtendedButton ID="EBtnSubmit" Text="�����Ա��Ϣ" IsChecked="true" OperateCode="UserModify"
                        OnClick="EBtnSubmit_Click" runat="server" IsShowTabs="true" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
