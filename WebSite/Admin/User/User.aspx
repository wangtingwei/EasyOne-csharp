<%@ Page Language="C#" AutoEventWireup="True" MasterPageFile="~/Admin/MasterPage.master"
    Inherits="EasyOne.WebSite.Admin.User.UserUI" Title="�û�����" Codebehind="User.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <asp:ScriptManager ID="SmgeRegion" runat="server" EnablePartialRendering="true">
    </asp:ScriptManager>

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
            <td valign="top">
                <table width="100%" cellpadding="2" cellspacing="1" style="background-color: white;">
                    <tbody id="Tabs0">
                        <tr class="tdbg">
                            <td class="tdbgleft" style="text-align:right">
                                �� Ա �飺
                            </td>
                            <td align="left" colspan="3">
                                <asp:DropDownList ID="DropGroupId" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td class="tdbgleft" style="text-align:right">
                                �� �� ����</td>
                            <td align="left">
                                <asp:TextBox ID="TxtUserName" runat="server" Width="150px"></asp:TextBox>
                                <pe:RequiredFieldValidator ID="ValrUserName" ControlToValidate="TxtUserName" runat="server"
                                    ErrorMessage="�û�������Ϊ�գ�" Display="Dynamic"></pe:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="ValeUserName" runat="server" ControlToValidate="TxtUserName"
                                    ErrorMessage="���ܰ��������ַ�  ��@��#��$��%��^��&��*��(��)��'��?��{��}��[��]��;��:��" ValidationExpression="^[^@#$%^&*()'?{}\[\];:]*$"
                                    Display="Dynamic"></asp:RegularExpressionValidator>
                                
                            </td>
                            <td class="tdbgleft" style="text-align:right">
                                �û����룺</td>
                            <td align="left">
                                <asp:TextBox ID="TxtUserPassword" runat="server" Width="150px" TextMode="password" ></asp:TextBox>
                                <pe:RequiredFieldValidator ID="ValrUserPassword" ControlToValidate="TxtUserPassword"
                                    runat="server" ErrorMessage="���벻��Ϊ�գ�" Display="Dynamic"></pe:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionPassword" runat="server" ControlToValidate="TxtUserPassword"
                            ErrorMessage="������������(����6λ)��" ValidationExpression="^.{6,}$" Display="Dynamic"></asp:RegularExpressionValidator>
                                <ajaxToolkit:PasswordStrength ID="PasswordStrength2" runat="server" TargetControlID="TxtUserPassword"
                                    StrengthIndicatorType="BarIndicator" BarIndicatorCssClass="BarIndicator_TxtUserPassword"
                                    BarBorderCssClass="BarBorder_TxtUserPassword" PreferredPasswordLength="8" MinimumNumericCharacters="1"
                                    MinimumSymbolCharacters="1" RequiresUpperAndLowerCaseCharacters="true" DisplayPosition="RightSide" />
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td class="tdbgleft" style="text-align:right">
                                ��ʾ���⣺</td>
                            <td align="left">
                                <asp:TextBox ID="TxtQuestion" runat="server" Width="150px"></asp:TextBox>
                                <pe:RequiredFieldValidator ID="ValrQuestion" ControlToValidate="TxtQuestion" runat="server"
                                    ErrorMessage="��ʾ���ⲻ��Ϊ�գ�" Display="Dynamic"></pe:RequiredFieldValidator>
                            </td>
                            <td class="tdbgleft" style="text-align:right">
                                ��ʾ�𰸣�</td>
                            <td align="left">
                                <asp:TextBox ID="TxtAnswer" runat="server" Width="150px"></asp:TextBox>
                                <pe:RequiredFieldValidator ID="ValrAnswer" ControlToValidate="TxtAnswer" runat="server"
                                    ErrorMessage="��ʾ�𰸲���Ϊ�գ�" Display="Dynamic"></pe:RequiredFieldValidator><asp:PlaceHolder
                                        ID="PhAnswer" runat="server" Visible="false"><span style="color: Green">���޸�������</span></asp:PlaceHolder>
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td class="tdbgleft" style="text-align:right">
                                �����ʼ���</td>
                            <td align="left">
                                <asp:TextBox ID="TxtEmail" runat="server" Width="150px"></asp:TextBox>
                                <pe:RequiredFieldValidator ID="ValrEmail" ControlToValidate="TxtEmail" runat="server"
                                    ErrorMessage="Email����Ϊ�գ�" Display="Dynamic"></pe:RequiredFieldValidator>
                                <pe:EmailValidator ID="Vmail" runat="server" ControlToValidate="TxtEmail" Display="Dynamic"
                                    ErrorMessage="�����ʼ���ʽ����"></pe:EmailValidator></td>
                            <td class="tdbgleft" style="text-align:right">
                                ��˽�趨��</td>
                            <td align="left">
                                <asp:RadioButtonList ID="RadlPrivacySetting" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Selected="True" Value="0">ȫ������</asp:ListItem>
                                    <asp:ListItem Value="1">���ֹ���</asp:ListItem>
                                    <asp:ListItem Value="2">��ȫ����</asp:ListItem>
                                </asp:RadioButtonList></td>
                        </tr>
                        <tr class="tdbg">
                            <td class="tdbgleft" style="text-align:right">
                                ͷ���ַ��</td>
                            <td align="left">
                                <asp:TextBox ID="TxtUserFace" runat="server" Width="150px"></asp:TextBox>
                            </td>
                            <td class="tdbgleft" style="text-align:right">
                                ͷ���ȣ�</td>
                            <td align="left">
                                <asp:TextBox ID="TxtFaceWidth" runat="server" Columns ="4" MaxLength ="4"></asp:TextBox>
                                ����
                                <pe:NumberValidator ID="NumberValidatorFaceWidth" ControlToValidate="TxtFaceWidth"
                                    runat="server">
                                </pe:NumberValidator>
                            </td>
                        </tr>
                        <tr class="tdbg" valign="top">
                            <td class="tdbgleft" style="text-align:right">
                                ǩ����Ϣ��</td>
                            <td align="left">
                                <asp:TextBox ID="TxtSign" runat="server" Height="74px" TextMode="MultiLine" Width="288px"></asp:TextBox></td>
                            <td class="tdbgleft" style="text-align:right">
                                ͷ��߶ȣ�</td>
                            <td align="left">
                                <asp:TextBox ID="TxtFaceHeight" runat="server" Columns ="4" MaxLength ="4"></asp:TextBox>
                                ����
                                <pe:NumberValidator ID="NumberValidatorFaceHeight" ControlToValidate="TxtFaceHeight"
                                    runat="server">
                                </pe:NumberValidator>
                            </td>
                        </tr>
                    </tbody>
                    <tbody id="Tabs1" style="display: none">
                        <tr class="tdbg">
                            <td class="tdbgleft" style="text-align:right">
                                ��ʵ������</td>
                            <td align="left">
                                <asp:TextBox ID="TxtTrueName" runat="server" Width="150px"></asp:TextBox>
                            </td>
                            <td class="tdbgleft" style="text-align:right">
                                ��ν��</td>
                            <td align="left">
                                <asp:TextBox ID="TxtTitle" runat="server" Width="150px"></asp:TextBox>
                            </td>
                        </tr>
                        <pe:LiaisonInformation ID="LiaisonInformation1" runat="server"></pe:LiaisonInformation>
                    </tbody>
                    <tbody id="Tabs2" style="display: none">
                        <pe:PersonalInformation ID="PersonalInformation1" runat="server" />
                    </tbody>
                    <tbody id="Tabs3" style="display: none">
                        <tr class="tdbg">
                            <td class="tdbgleft" style="width: 12%;text-align:right">
                                ��λ���ƣ�</td>
                            <td align="left">
                                <asp:TextBox ID="TxtCompany" runat="server" Width="150px"></asp:TextBox></td>
                            <td class="tdbgleft" style="width: 12%;text-align:right">
                                �������ţ�</td>
                            <td style="width: 38%;" align="left">
                                <asp:TextBox ID="TxtDepartment" runat="server" Width="150px"></asp:TextBox></td>
                        </tr>
                        <tr class="tdbg">
                            <td class="tdbgleft" style="text-align:right">
                                ְλ��</td>
                            <td align="left">
                                <asp:TextBox ID="TxtPosition" runat="server" Width="150px"></asp:TextBox></td>
                            <td class="tdbgleft" style="text-align:right">
                                ����ҵ��</td>
                            <td align="left">
                                <asp:TextBox ID="TxtOperation" runat="server" Width="150px"></asp:TextBox></td>
                        </tr>
                        <tr class="tdbg">
                            <td class="tdbgleft" style="text-align:right">
                                ��λ��ַ��</td>
                            <td colspan="3" align="left">
                                <asp:TextBox ID="TxtCompanyAddress" runat="server" Width="150px"></asp:TextBox></td>
                        </tr>
                    </tbody>
                    <tbody id="Tabs4" style="display: none">
                        <pe:Company ID="Company1" Visible="false" runat="server"></pe:Company>
                    </tbody>
                </table>
            </td>
        </tr>
    </table>
    <table width="100%" border="0" cellpadding="5" cellspacing="1">
        <tr align="center">
            <td style="height: 40px;">
                <asp:HiddenField ID="HdnContacterID" runat="server" />
                <pe:ExtendedButton ID="EBtnSubmit" Text="�����Ա��Ϣ" IsChecked="true" OperateCode="UserModify"
                    OnClick="EBtnSubmit_Click" runat="server" IsShowTabs="true" />&nbsp;&nbsp;&nbsp;
                <asp:Button ID="BtnCancel" runat="server" Text="ȡ��" OnClick="BtnCancel_Click" ValidationGroup="BtnCancel" />
            </td>
        </tr>
    </table>
</asp:Content>
