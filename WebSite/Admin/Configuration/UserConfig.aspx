<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPage.master"
    ValidateRequest="false" EnableEventValidation="false"
    Inherits="EasyOne.WebSite.Admin.User.UserConfig" Title="�û���������" Codebehind="UserConfig.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td class="spacingtitle" colspan="2">
                <strong>�û���������</strong>
            </td>
        </tr>
        <tr class="tdbg">
            <td style="width: 40%;" class="tdbgleft">
                <strong>�Ƿ�����Աע�Ṧ�ܣ�</strong></td>
            <td>
                <asp:RadioButtonList ID="RadlEnableUserReg" runat="server" RepeatDirection="Horizontal"
                    RepeatLayout="Flow">
                    <asp:ListItem Selected="True" Value="true">��</asp:ListItem>
                    <asp:ListItem Value="false">��</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>�Ƿ�����һ��Emailע������Ա��</strong></td>
            <td>
                <asp:RadioButtonList ID="RadlEnableMultiRegPerEmail" runat="server" RepeatDirection="Horizontal"
                    RepeatLayout="Flow">
                    <asp:ListItem Value="true">��</asp:ListItem>
                    <asp:ListItem Selected="True" Value="false">��</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>�»�Աע��ʱ�û��������ַ�����</strong>
            </td>
            <td>
                <asp:TextBox ID="TxtUserNameLimit" Text="4" runat="server" Columns="5" MaxLength="3" />���ַ�
                <pe:RequiredFieldValidator ID="ReqTxtUserNameLimit" runat="server" Display="Dynamic"
                    ControlToValidate="TxtUserNameLimit" ErrorMessage="�����ַ�������Ϊ��" />
                <asp:CompareValidator ID="CValTxtUserNameLimit" runat="server" ControlToValidate="TxtUserNameLimit"
                    ValueToCompare="1" Type="Integer" Operator="GreaterThanEqual" ErrorMessage="�����ַ���������ڵ���1"
                    Display="Dynamic" SetFocusOnError="true" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>�»�Աע��ʱ�û�������ַ�����</strong>
            </td>
            <td>
                <asp:TextBox ID="TxtUserNameMax" Text="20" runat="server" Columns="5" MaxLength="3" />���ַ�
                <pe:RequiredFieldValidator ID="ReqTxtUserNameMax" runat="server" Display="Dynamic"
                    ErrorMessage="����ַ�������Ϊ��" ControlToValidate="TxtUserNameMax" />
                <asp:CompareValidator ID="CValTxtUserNameMax" runat="server" ControlToValidate="TxtUserNameMax"
                    ControlToCompare="TxtUserNameLimit" Type="Integer" Operator="GreaterThanEqual"
                    ErrorMessage="����ַ���������ڵ�����С�ַ���" Display="Dynamic" SetFocusOnError="true" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>��ֹע����û�����</strong><br />
                ���ұ�ָ�����û���������ֹע�ᣬÿ���û������á�|�����ŷָ�
            </td>
            <td>
                <asp:TextBox ID="TxtUserName_RegDisabled" Text="" Height="60" TextMode="MultiLine"
                    runat="server" Columns="60" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>��Աע��ʱ�ı��ѡ����Ŀ��</strong><br />
                �ڻ�Աע��ʱ���ɸ�����Ҫ�趨ע��ı����ѡ���<br />
                �����������������ӵ�����������ߡ�ѡ������б��м����趨��<br />
                �û����� ���롢 ȷ�����롢 �������⡢ ����𰸡� EmailΪϵͳǿ�Ʊ�����Ϣ��<br />
                <span style="color: Blue">ע�����޸Ĵ��ǰ̨����ע��ı�ҳ�潫ʧЧ</span>
            </td>
            <td style="width: 60%">
                <table>
                    <tr>
                        <td>
                            <asp:HiddenField ID="HdnRegFields_MustFill" runat="server" />
                            <asp:HiddenField ID="HdnRegFields_SelectFill" runat="server" />
                            �����<br />
                            <asp:ListBox ID="LitRegFields" SelectionMode="Multiple" Width="130" Height="285"
                                runat="server" /></td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <input id="Button4" value=" >> " onclick="AddFields_MustFill()" title="�����ѡ��" type="button" /><br />
                                        <input id="Button2" value=" << " onclick="RemoveFields_MustFill()" title="�Ƴ���ѡ��"
                                            type="button" />
                                    </td>
                                    <td>
                                        �����<br />
                                        <asp:ListBox ID="LitRegFields_MustFill" SelectionMode="Multiple" Width="130" Height="130"
                                            runat="server" />
                                    </td>
                                    <td>
                                        <input id="Button5" value=" �� " onclick="UpFields_MustFill()" title="����" type="button" /><br />
                                        <input id="Button6" value=" �� " onclick="DownFields_MustFill()" title="����" type="button" />
                                        <br />
                                        <br />
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <input id="Button1" value=" >> " onclick="AddFields_SelectFill()" title="�����ѡ��" type="button" /><br />
                                        <input id="Button3" value=" << " onclick="RemoveFields_SelectFill()" title="�Ƴ���ѡ��"
                                            type="button" />
                                    </td>
                                    <td>
                                        ѡ���<br />
                                        <asp:ListBox ID="LitRegFields_SelectFill" SelectionMode="Multiple" Width="130" Height="130"
                                            runat="server" />
                                    </td>
                                    <td>
                                        <input id="Button7" value=" �� " onclick="UpFields_SelectFill()" title="����" type="button" /><br />
                                        <input id="Button8" value=" �� " onclick="DownFields_SelectFill()" title="����" type="button" />
                                        <br />
                                        <br />
                                        <br />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>�Ƿ�����ע����ҵ���ܣ�</strong><br />
                ��ѡ���ǡ������Աע����ͬʱ��ʾע��һ����ҵ��</td>
            <td>
                <asp:RadioButtonList ID="RadlEnableRegCompany" runat="server" RepeatDirection="Horizontal"
                    RepeatLayout="Flow">
                    <asp:ListItem Selected="True" Value="true">��</asp:ListItem>
                    <asp:ListItem Value="false">��</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>��Աע��ʱ�Ƿ�������֤�빦�ܣ�</strong><br />
                ������֤�빦�ܿ�����һ���̶��Ϸ�ֹ����Ӫ�������ע����Զ�ע�ᡣ</td>
            <td>
                <asp:RadioButtonList ID="RadlEnableCheckCodeOfReg" runat="server" RepeatDirection="Horizontal"
                    RepeatLayout="Flow">
                    <asp:ListItem Value="true">��</asp:ListItem>
                    <asp:ListItem Selected="True" Value="false">��</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>��Աע��ʱ�Ƿ����ûش�������֤���ܣ�</strong><br />
                ���ô˹��ܣ��������̶��Ϸ�ֹ����Ӫ�������ע����Զ�ע�ᣬҲ��������ĳЩ���ⳡ�ϣ���ֹ�޹���Աע���Ա��</td>
            <td>
                <asp:RadioButtonList ID="RadlEnableQAofReg" runat="server" RepeatDirection="Horizontal"
                    RepeatLayout="Flow">
                    <asp:ListItem Value="true">��</asp:ListItem>
                    <asp:ListItem Selected="True" Value="false">��</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>����һ��</strong><br />
                ���������֤���ܣ�������һ�ʹ𰸱�����д��
            </td>
            <td>
                ���⣺<asp:TextBox ID="TxtRegQuestion1" Text="����һ" runat="server" Width="267px" /><br />
                �𰸣�<asp:TextBox ID="TxtRegAnswer1" Text="��һ" runat="server" Width="267px" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>�������</strong><br />
                ���������֤���ܣ���������ʹ𰸱�����д��
            </td>
            <td>
                ���⣺<asp:TextBox ID="TxtRegQuestion2" Text="�����" runat="server" Width="267px" /><br />
                �𰸣�<asp:TextBox ID="TxtRegAnswer2" Text="�𰸶�" runat="server" Width="267px" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>��������</strong><br />
                ���������֤���ܣ����������ʹ𰸱�����д��
            </td>
            <td>
                ���⣺<asp:TextBox ID="TxtRegQuestion3" Text="������" runat="server" Width="267px" /><br />
                �𰸣�<asp:TextBox ID="TxtRegAnswer3" Text="����" runat="server" Width="267px" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>�»�Աע���Ƿ���Ҫ����Ա��֤��</strong><br />
                ��ѡ���ǡ������Ա������ͨ������Ա��֤�����������Ϊ��ʽע���Ա��</td>
            <td>
                <asp:RadioButtonList ID="RadlAdminCheckReg" runat="server" RepeatDirection="Horizontal"
                    RepeatLayout="Flow">
                    <asp:ListItem Value="true">��</asp:ListItem>
                    <asp:ListItem Selected="True" Value="false">��</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>�»�Աע���Ƿ���Ҫ�ʼ���֤�� </strong>
                <br />
                ��ѡ���ǡ������Աע���ϵͳ�ᷢһ�������֤����ʼ����˻�Ա����Ա������ͨ���ʼ���֤�����������Ϊ��ʽע���Ա��</td>
            <td>
                <asp:RadioButtonList ID="RadlEmailCheckReg" runat="server" RepeatDirection="Horizontal"
                    RepeatLayout="Flow">
                    <asp:ListItem Value="true">��</asp:ListItem>
                    <asp:ListItem Selected="True" Value="false">��</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>�»�Աע��ʱ���͵���֤�ʼ����ݣ�</strong><br />
                �ʼ�����֧��HTML���ʼ������п��ñ�ǩ˵�����£�<br />
                <span style="cursor: hand;" onclick="Insert('{$CheckNum}')">{$CheckNum}</span>����֤��<br />
                <span style="cursor: hand;" onclick="Insert('{$CheckUrl}')">{$CheckUrl}</span>����֤��ַ</td>
            <td>
                <asp:TextBox ID="TxtEmailOfRegCheck" TextMode="MultiLine" runat="server" Height="80px"
                    Width="400px" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>�»�Աע��ɹ���������Ա�飺</strong></td>
            <td>
                <asp:DropDownList ID="RadlUserGroup" DataTextField="GroupName" DataValueField="GroupId"
                    runat="server" />
            </td>
        </tr>
        <tr class="tdbg" runat="server" id="PresentExp">
            <td class="tdbgleft">
                <strong>�»�Աע��ʱ���͵Ļ��֣�</strong>
            </td>
            <td>
                <asp:TextBox ID="TxtPresentExp" Text="0" runat="server" Columns="7" MaxLength="7" />�ֻ���
                <asp:RegularExpressionValidator ID="ValgPresentExp" runat="server" ControlToValidate="TxtPresentExp"
                    ErrorMessage="ֻ������������" ValidationExpression="^([0-9])(\d{0,})(\d{0,})$" Display="Dynamic" />
            </td>
        </tr>
        <tr class="tdbg" runat="server" id="PresentMoney">
            <td class="tdbgleft">
                <strong>�»�Աע��ʱ���͵Ľ�Ǯ��</strong>
            </td>
            <td>
                <asp:TextBox ID="TxtPresentMoney" Text="0" runat="server" Columns="7" MaxLength="7" />ԪǮ
                <asp:RegularExpressionValidator ID="ValgTxtPresentMoney" runat="server" ControlToValidate="TxtPresentMoney"
                    ErrorMessage="ֻ����������ַ������Ҳ���Ϊ����" ValidationExpression="^[0-9]+(\.?[0-9]{1,4})?" Display="Dynamic" />
            </td>
        </tr>
        <tr class="tdbg" runat="server" id="PresentPoint">
            <td class="tdbgleft">
                <strong>�»�Աע��ʱ���͵�<pe:ShowPointName ID="ShowPointName1" runat="server" />��</strong>
            </td>
            <td>
                <asp:TextBox ID="TxtPresentPoint" Text="0" runat="server" Columns="7" MaxLength="7" /><pe:ShowPointName ID="ShowPointName2" runat="server" PointType ="PointUnit"/><pe:ShowPointName ID="ShowPointName6" runat="server" />
                <asp:RegularExpressionValidator ID="ValgPresentPoint" runat="server" ControlToValidate="TxtPresentPoint"
                    ErrorMessage="ֻ������������" ValidationExpression="^([0-9])(\d{0,})(\d{0,})$" Display="Dynamic" />
            </td>
        </tr>
        <tr class="tdbg" runat="server" id="PresentValidNum">
            <td class="tdbgleft">
                <strong>�»�Աע��ʱ���͵���Ч�ڣ�</strong>
            </td>
            <td>
                <asp:TextBox ID="TxtPresentValidNum" Text="0" runat="server" Columns="5" /><asp:DropDownList
                    ID="DropPresentValidUnit" runat="server">
                    <asp:ListItem Value="1">��</asp:ListItem>
                    <asp:ListItem Value="2">��</asp:ListItem>
                    <asp:ListItem Value="3">��</asp:ListItem>
                </asp:DropDownList>��Ϊ��1��ʾ�����ڣ�
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>��Ա�һ�����ķ�ʽ��</strong>
            </td>
            <td>
                <asp:RadioButtonList ID="RadlGetPasswordType" runat="server">
                    <asp:ListItem Value="0">�ش���ȷ����𰸺�ֱ����ҳ���޸�����</asp:ListItem>
                    <asp:ListItem Value="1" Selected="True">�ش���ȷ����𰸺󣬷����ʼ�����Ա���䣨��������վ��Ϣ���������ʼ����������Աע��ʱ��д���ʼ���ַ����</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>��Ա��¼ʱ�Ƿ�������֤�빦�ܣ�</strong><br />
                ������֤�빦�ܿ�����һ���̶��Ϸ�ֹ��Ա���뱻�����ƽ�</td>
            <td>
                <asp:RadioButtonList ID="RadlEnableCheckCodeOfLogin" runat="server" RepeatDirection="Horizontal"
                    RepeatLayout="Flow">
                    <asp:ListItem Selected="True" Value="true">��</asp:ListItem>
                    <asp:ListItem Value="false">��</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>��Ա��¼ʱ�Ƿ��������ͬʱʹ��ͬһ��Ա�ʺţ�</strong></td>
            <td>
                <asp:RadioButtonList ID="RadlEnableMultiLogin" runat="server" RepeatDirection="Horizontal"
                    RepeatLayout="Flow">
                    <asp:ListItem Selected="True" Value="true">��</asp:ListItem>
                    <asp:ListItem Value="false">��</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr class="tdbg" runat="server" id="PresentExpPerLogin">
            <td class="tdbgleft">
                <strong>��Աÿ��¼һ�ν����Ļ��֣�</strong><br />
                һ��ֻ����һ��
            </td>
            <td>
                <asp:TextBox ID="TxtPresentExpPerLogin" Text="0" runat="server" Columns="5" />�ֻ���
            </td>
        </tr>
        <tr class="tdbg" runat="server" id="MoneyExchangePoint">
            <td class="tdbgleft">
                <strong>��Ա���ʽ���<pe:ShowPointName ID="ShowPointName3" runat="server" />�Ķһ����ʣ�</strong>
            </td>
            <td>
                ÿ
                <asp:TextBox ID="TxtMoneyExchangePoint" Text="0" runat="server" Columns="7" MaxLength="7" />
                ԪǮ�ɶһ� <strong><asp:TextBox ID="TxtCMoneyExchangePoint" Text="1" runat="server" Columns="7" MaxLength="7" /></strong> <pe:ShowPointName ID="ShowPointName10" runat="server" PointType ="PointUnit" /><pe:ShowPointName ID="ShowPointName4" runat="server" />
                <asp:RegularExpressionValidator ID="ValeMoneyExchangePoint" runat="server" ControlToValidate="TxtMoneyExchangePoint"
                    ErrorMessage="ֻ����������ַ������Ҳ���Ϊ��͸���" ValidationExpression="^[0-9]+(\.?[0-9]{1,4})?" Display="Dynamic" />
                <asp:RegularExpressionValidator ID="ValeCMoneyExchangePoint" runat="server" ControlToValidate="TxtCMoneyExchangePoint"
                    ErrorMessage="ֻ������������������" ValidationExpression="^[1-9]+(\.?[0-9]{1,4})?" Display="Dynamic" />
            </td>
        </tr>
        <tr class="tdbg" runat="server" id="MoneyExchangeValidDay">
            <td class="tdbgleft">
                <strong>��Ա���ʽ�����Ч�ڵĶһ����ʣ�</strong>
            </td>
            <td>
                ÿ
                <asp:TextBox ID="TxtMoneyExchangeValidDay" Text="0" runat="server" Columns="7" MaxLength="7" />
                ԪǮ�ɶһ� <strong><asp:TextBox ID="TxtCMoneyExchangeValidDay" Text="1" runat="server" Columns="7" MaxLength="7" /></strong> ����Ч��
                <asp:RegularExpressionValidator ID="ValeMoneyExchangeValidDay" runat="server" ControlToValidate="TxtMoneyExchangeValidDay"
                    ErrorMessage="ֻ����������ַ������Ҳ���Ϊ����" ValidationExpression="^[0-9]+(\.?[0-9]{1,4})?" Display="Dynamic" />
                <asp:RegularExpressionValidator ID="ValeCMoneyExchangeValidDay" runat="server" ControlToValidate="TxtCMoneyExchangeValidDay"
                    ErrorMessage="ֻ������������������" ValidationExpression="^[1-9]+(\.?[0-9]{1,4})?" Display="Dynamic" />
            </td>
        </tr>
        <tr class="tdbg" runat="server" id="UserExpExchangePoint">
            <td class="tdbgleft">
                <strong>��Ա�Ļ�����<pe:ShowPointName ID="ShowPointName5" runat="server" />�Ķһ����ʣ�</strong>
            </td>
            <td>
                ÿ
                <asp:TextBox ID="TxtUserExpExchangePoint" Text="0" runat="server" Columns="7" MaxLength="7" />
                �ֻ��ֿɶһ� <strong><asp:TextBox ID="TxtCUserExpExchangePoint" Text="1" runat="server" Columns="7" MaxLength="7" /></strong> <pe:ShowPointName ID="ShowPointName11" runat="server" PointType ="PointUnit" /><pe:ShowPointName ID="ShowPointName7" runat="server" />
               <asp:RegularExpressionValidator ID="ValgUserExpExchangePoint" runat="server" ControlToValidate="TxtUserExpExchangePoint"
                    ErrorMessage="ֻ������������������" ValidationExpression="^([1-9])(\d{0,})(\d{0,})$" Display="Dynamic" />
               <asp:RegularExpressionValidator ID="ValgCUserExpExchangePoint" runat="server" ControlToValidate="TxtCUserExpExchangePoint"
                    ErrorMessage="ֻ������������������" ValidationExpression="^([1-9])(\d{0,})(\d{0,})$" Display="Dynamic" />
            </td>
        </tr>
        <tr class="tdbg" runat="server" id="UserExpExchangeValidDay">
            <td class="tdbgleft">
                <strong>��Ա�Ļ�������Ч�ڵĶһ����ʣ�</strong>
            </td>
            <td>
                ÿ
                <asp:TextBox ID="TxtUserExpExchangeValidDay" Text="0" runat="server" Columns="7"
                    MaxLength="7" />
                �ֻ��ֿɶһ� <strong><asp:TextBox ID="TxtCUserExpExchangeValidDay" Text="1" runat="server" Columns="7" MaxLength="7" /></strong> ����Ч��
               <asp:RegularExpressionValidator ID="ValgUserExpExchangeValidDay" runat="server" ControlToValidate="TxtUserExpExchangeValidDay"
                    ErrorMessage="ֻ������������������" ValidationExpression="^([1-9])(\d{0,})(\d{0,})$" Display="Dynamic" />
               <asp:RegularExpressionValidator ID="ValgCUserExpExchangeValidDay" runat="server" ControlToValidate="TxtCUserExpExchangeValidDay"
                    ErrorMessage="ֻ������������������" ValidationExpression="^([1-9])(\d{0,})(\d{0,})$" Display="Dynamic" />
            </td>
        </tr>
        <tr class="tdbg" runat="server" id="PointName">
            <td class="tdbgleft">
                <strong><pe:ShowPointName ID="ShowPointName8" runat="server" />�����ƣ�</strong><br />
                ���磺��ȯ�����
            </td>
            <td style="height: 36px">
                <asp:TextBox ID="TxtPointName" Text="��ȯ" runat="server" Columns="5" MaxLength="5" />
            </td>
        </tr>
        <tr class="tdbg" runat="server" id="PointUnit">
            <td class="tdbgleft">
                <strong><pe:ShowPointName ID="ShowPointName9" runat="server" />�ĵ�λ��</strong><br />
                ���磺�㡢��
            </td>
            <td>
                <asp:TextBox ID="TxtPointUnit" Text="��" runat="server" Columns="5" MaxLength="5" />
            </td>
        </tr>
        <tr class="tdbgbottom">
            <td colspan="2">
                <asp:Button ID="BtnSubmit" runat="server" Text="��������" OnClick="BtnSubmit_Click" />
            </td>
        </tr>
    </table>

    <script type="text/javascript">
    var LitRegFieldsClientID = "<%=LitRegFields.ClientID %>";
    var HdnRegFields_MustFillClientID = "<%=HdnRegFields_MustFill.ClientID %>";
    var LitRegFields_MustFillClientID = "<%=LitRegFields_MustFill.ClientID %>";
    var HdnRegFields_SelectFillClientID = "<%=HdnRegFields_SelectFill.ClientID %>";
    var LitRegFields_SelectFillClientID = "<%=LitRegFields_SelectFill.ClientID %>";
    var TxtEmailOfRegCheckClientID = "<%=TxtEmailOfRegCheck.ClientID %>";
    function AddFields_MustFill()
    {
        var itemList = document.getElementById(LitRegFieldsClientID);
        var target = document.getElementById(LitRegFields_MustFillClientID);
        addItem(itemList,target);
        SetHdn(target,document.getElementById(HdnRegFields_MustFillClientID));
    }
    
    function RemoveFields_MustFill()
    {
        var itemList = document.getElementById(LitRegFieldsClientID);
        var target = document.getElementById(LitRegFields_MustFillClientID);
        addItem(target,itemList);
        SetHdn(target,document.getElementById(HdnRegFields_MustFillClientID));
    }
    
    function AddFields_SelectFill()
    {
        var itemList = document.getElementById(LitRegFieldsClientID);
        var target = document.getElementById(LitRegFields_SelectFillClientID);
        addItem(itemList,target);
        SetHdn(target,document.getElementById(HdnRegFields_SelectFillClientID));
    }
    
    function RemoveFields_SelectFill()
    {
        var itemList = document.getElementById(LitRegFieldsClientID);
        var target = document.getElementById(LitRegFields_SelectFillClientID);
        addItem(target,itemList);
        SetHdn(target,document.getElementById(HdnRegFields_SelectFillClientID));
    }
    
    function UpFields_MustFill()
    {
        var target = document.getElementById(LitRegFields_MustFillClientID);
        UpOption(target);
        SetHdn(target,document.getElementById(HdnRegFields_MustFillClientID));
    }
    
    function DownFields_MustFill()
    {
        var target = document.getElementById(LitRegFields_MustFillClientID);
        DownOption(target);
        SetHdn(target,document.getElementById(HdnRegFields_MustFillClientID));
    }
    
    function UpFields_SelectFill()
    {
        var target = document.getElementById(LitRegFields_SelectFillClientID);
        UpOption(target);
        SetHdn(target,document.getElementById(HdnRegFields_SelectFillClientID));
    }
    
    function DownFields_SelectFill()
    {
        var target = document.getElementById(LitRegFields_SelectFillClientID);
        DownOption(target);
        SetHdn(target,document.getElementById(HdnRegFields_SelectFillClientID));
    }
    
    function UpOption(obj)
    {
        for(var i = 0; i < obj.length; i++)
        {
            var opt = obj.options[i];
            if (opt.selected)
            {
                if(i >= 1)
                {
                    var temp = obj.options[i-1];
                    obj.options[i] = new Option(temp.text, temp.value, 0, 0);
                    obj.options[i-1] = new Option(opt.text, opt.value, 0, 1);
                }
            }
        }
    }
    
    function DownOption(obj)
    {
        for(var i = obj.length-1; i >= 0; i--)
        {
            var opt = obj.options[i];
            if (opt.selected)
            {
                if(i <= obj.length-2)
                {
                    var temp = obj.options[i+1];
                    obj.options[i] = new Option(temp.text, temp.value, 0, 0);
                    obj.options[i+1] = new Option(opt.text, opt.value, 0, 1);
                }
            }
        }
    }
    
    function addItem(ItemList,Target)
    {
        for(var i = 0; i < ItemList.length; i++)
        {
            var opt = ItemList.options[i];
            if (opt.selected)
            {
                flag = true;
                for (var y=0;y<Target.length;y++)
                {
                    var myopt = Target.options[y];
                    if (myopt.value == opt.value)
                    {
                        flag = false;
                    }
                }
                if(flag)
                {
                    Target.options[Target.options.length] = new Option(opt.text, opt.value, 0, 0);
                }
            }
        }
        
        for (var y=0;y<Target.length;y++)
        {
              var myopt = Target.options[y];
              for(var i = 0; i < ItemList.length; i++)
              {
                    if(ItemList.options[i].value == myopt.value)
                    {
                        ItemList.options[i] = null;
                    }
              }
         }
    }

    function SetHdn(ItemList,HdnObj)
    {
        var adminId = "";
        for(var i = 0; i < ItemList.length; i++)
        {
            if (adminId == "")
            {
                adminId = ItemList.options[i].value;
            }
            else
            {
                adminId += "," + ItemList.options[i].value;
            }
        }
        HdnObj.value = adminId;
    }

    function Insert(input){
        document.getElementById(TxtEmailOfRegCheckClientID).focus();
        var str = document.selection.createRange();
        if (input != null){
            str.text = input;
        }
    }
    //-->
    </script>

</asp:Content>
