<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Survey.SurveyUI" Title="�����ʾ�������޸�ҳ" Codebehind="Survey.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" runat="server" SiteMapProvider="AdminMapProvider" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <div>
        <table style="text-align: center; width: 100%" border="0" cellpadding="5" cellspacing="1"
            class="border">
            <tr class="title">
                <td colspan="2" style="text-align: center;">
                    <asp:Label ID="LblTitle" runat="server"></asp:Label></td>
            </tr>
            <tr class="tdbg">
                <td align="left" class="tdbgleft">
                    <strong>�ʾ����ƣ�</strong></td>
                <td style="width: 600px; text-align: left;">
                    <asp:TextBox ID="TxtSurveyName" runat="server" MaxLength="60" Width="339px"></asp:TextBox>
                    <pe:RequiredFieldValidator ID="ValrSurveyName" runat="server" ControlToValidate="TxtSurveyName"
                        Display="Dynamic" ErrorMessage="�ʾ����Ʋ���Ϊ�գ�" SetFocusOnError="True"></pe:RequiredFieldValidator></td>
            </tr>
            <tr class="tdbg">
                <td align="left" class="tdbgleft">
                    <strong>�ʾ�������</strong></td>
                <td style="width: 600px; text-align: left;">
                    <asp:TextBox ID="TxtDescription" runat="server" Height="92px" TextMode="MultiLine"
                        Width="407px"></asp:TextBox></td>
            </tr>
            <tr class="tdbg">
                <td align="left" class="tdbgleft">
                    <strong>ͬһIP�����ظ��ύ������</strong></td>
                <td style="width: 600px; text-align: left;">
                    <asp:TextBox ID="TxtIPRepeat" runat="server" MaxLength="4" Width="70px">1</asp:TextBox>
                    ����д����0������
                    <asp:RangeValidator ID="ValrIPRepeat" runat="server" ControlToValidate="TxtIPRepeat"
                        ErrorMessage="����д����0������" MaximumValue="9999" MinimumValue="1" SetFocusOnError="True"
                        Display="Dynamic"></asp:RangeValidator></td>
            </tr>
            <tr class="tdbg">
                <td align="left" class="tdbgleft">
                    <strong>ֻ�е�¼�����ͶƱ��</strong></td>
                <td style="width: 600px; text-align: left;">
                    <asp:RadioButton ID="RadNeedLogin1" runat="server" GroupName="NeedLogin" Text="��" />&nbsp;
                    <asp:RadioButton ID="RadNeedLogin0" runat="server" GroupName="NeedLogin" Text="��"
                        Checked="True" /></td>
            </tr>
            <tr id="TrEncourage" class="tdbg" style="display: none">
                <td align="left" class="tdbgleft">
                    <strong>ע���Ա�����߽���������</strong></td>
                <td style="width: 600px; text-align: left;">
                    <asp:TextBox ID="TxtPresentPoint" runat="server" MaxLength="10" Width="70px">0</asp:TextBox>�����������0����ע���Ա��д�ʾ�ʱ������Ӧ����
                    <asp:CompareValidator ID="ValcPressentPoint" runat="server" ControlToValidate="TxtPresentPoint"
                        ErrorMessage="����������" SetFocusOnError="True" Display="Dynamic" Operator="DataTypeCheck"
                        Type="Integer"></asp:CompareValidator></td>
            </tr>
            <tr class="tdbg">
                <td align="left" class="tdbgleft">
                    <strong>�����������ƣ�</strong><br />
                    <span style="color: Blue">�����ʾ�������ô�����ʱ�����´����ʾ�</span></td>
                <td style="width: 600px; text-align: left;">
                    <asp:TextBox ID="TxtSetPassword" runat="server" MaxLength="30" TextMode="Password"></asp:TextBox>
                    ���������룬Ϊ��ʱ��ʾ�����ô�����
                </td>
            </tr>
            <tr class="tdbg">
                <td align="left" class="tdbgleft" style="width: 40%">
                    <strong>�����޶���ʽ��</strong></td>
                <td style="width: 600px; text-align: left">
                    <asp:RadioButtonList ID="RadlLockIPType" runat="server">
                        <asp:ListItem Value="0" Selected="True">�����������޶����ܣ��κ�IP�����Է��ʱ��ʾ�</asp:ListItem>
                        <asp:ListItem Value="1">�������ð�������ֻ����������е�IP���ʱ��ʾ�</asp:ListItem>
                        <asp:ListItem Value="2">�������ú�������ֻ��ֹ�������е�IP���ʱ��ʾ�</asp:ListItem>
                        <asp:ListItem Value="3">ͬʱ���ð�����������������ж�IP�Ƿ��ڰ������У�������ڣ����ֹ���ʣ�����������ж��Ƿ��ں������У����IP�ں����������ֹ���ʣ�����������ʡ�</asp:ListItem>
                        <asp:ListItem Value="4">ͬʱ���ð�����������������ж�IP�Ƿ��ں������У�������ڣ���������ʣ�����������ж��Ƿ��ڰ������У����IP�ڰ���������������ʣ������ֹ���ʡ�</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft" style="width: 40%; text-align: left">
                    <strong>IP�ΰ�����</strong>��</td>
                <td class="tdbg" style="width: 600px; text-align: left">
                    &nbsp;<pe:IPLock ID="IPLockWrite" runat="server" />
                </td>
            </tr>
            <tr class="tdbg">
                <td align="left" class="tdbgleft">
                    <strong>IP�κ�����</strong>��</td>
                <td class="tdbg" style="width: 600px; text-align: left">
                    &nbsp;<pe:IPLock ID="IPLockBlack" runat="server" />
                    &nbsp;
                </td>
            </tr>
            <tr class="tdbg">
                <td align="left" class="tdbgleft">
                    <strong>�������ڣ�</strong></td>
                <td style="width: 600px; text-align: left;">
                    <pe:DatePicker ID="DateEnd" runat="server"></pe:DatePicker></td>
            </tr>
            <tr class="tdbg">
                <td align="left" class="tdbgleft">
                    <strong>�ʾ�ģ�壺</strong></td>
                <td style="width: 600px; text-align: left;">
                    <pe:TemplateSelectControl ID="FscTemplate" runat="server" Width="250px"></pe:TemplateSelectControl>
                    <pe:RequiredFieldValidator ID="ValrFscTemplate" runat="server" ControlToValidate="FscTemplate"
                        Display="Dynamic" ErrorMessage="�ʾ�ģ�岻��Ϊ�գ�" SetFocusOnError="True"></pe:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="tdbg">
                <td align="left" class="tdbgleft">
                    <strong>�����ύ�ı�ҳ���ַ�б�</strong><br />
                    ��ע����������ҳ���ύ�������ա�<br />
                    ��Ӷ���޶���ַ������<span style="color: Red">�س�</span>�ָ�����ַ��д��ʽ�� �磺<span style="color: Blue">http://www.***.**/Survey/200612080903.html</span>
                    �������������ַ�ύ���ʾ����ݡ�<span style="color: Red">�Ƽ�ʹ�ã���ֹα���ύ��</span>��
                </td>
                <td style="width: 600px; text-align: left;">
                    <asp:TextBox ID="TxtLockUrl" runat="server" Height="127px" TextMode="MultiLine" Width="494px"></asp:TextBox></td>
            </tr>
            <tr class="tdbg">
                <td colspan="2" style="text-align: center; height: 40px">
                    <pe:ExtendedButton IsChecked="true" OperateCode="SurveyQuestionnaireManage" ID="BtnSave"
                        runat="server" Text="����" OnClick="BtnSave_Click" />
                    &nbsp;&nbsp;
                    <input id="Cancel" name="Cancel" onclick="Redirect('SurveyManage.aspx')" type="button"
                        class="inputbutton" value="ȡ��" />
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="HdnAction" runat="server" />
    &nbsp;

    <script type="text/javascript"> 
      if(<%=RadNeedLogin1.Checked.ToString().ToLower() %>)
      document.getElementById("TrEncourage").style.display='';
    </script>

</asp:Content>
