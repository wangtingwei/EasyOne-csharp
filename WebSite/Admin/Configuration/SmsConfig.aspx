<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Accessories.SmsConfig"
    Title="�ֻ���������" Codebehind="SmsConfig.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="server">
<script language="javascript" type="text/javascript">
function Link ()
{
    window.open("http://sms.EasyOne.net/Register.aspx");
}
</script>
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td colspan="2" class="spacingtitle">
                <strong>�ֻ���������</strong>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>���׶���ͨ���û�����</strong><br />
                ���������� ���׶���ͨƽ̨ ע����û���
            </td>
            <td>
                <asp:TextBox ID="TxtUserName" runat="server" Columns="30"></asp:TextBox> &nbsp;&nbsp;
                <input type="submit" value="ע�����ͨ" class="inputbutton" onclick="Link ();"/>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>MD5��Կ��</strong><br />
                ���������� ���׶���ͨƽ̨ �����õ�MD5��Կ
            </td>
            <td>
                <asp:TextBox ID="TxtMD5Key" runat="server" TextMode="password" Columns="30"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>�ͻ��ύ����ʱ��ϵͳ�Ƿ��Զ������ֻ�����֪ͨ����Ա��</strong>
            </td>
            <td>
                <asp:RadioButtonList ID="RadlIsAutoSend" runat="server" RepeatDirection="Horizontal"
                    RepeatLayout="Flow">
                    <asp:ListItem Value="true">��</asp:ListItem>
                    <asp:ListItem Value="false">��</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>����Ա��С��ͨ���ֻ����룺</strong><br />
                ÿ������һ�����롣 �������������룬ϵͳ��ͬʱ���͵����������
            </td>
            <td>
                <asp:TextBox ID="TxtAdminPhoneNumber" runat="server" TextMode="MultiLine" Height="80px"
                    Width="400px"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg" id = "OrderMessage" runat ="server">
            <td class="tdbgleft">
                <strong>�ͻ��¶���ʱϵͳ������Ա���Ͷ��ŵ����ݣ�</strong><br />
                ��֧��HTML���룬���ñ�ǩ�������ı�ǩ˵��
            </td>
            <td>
                <asp:TextBox ID="TxtOrderMessage" runat="server" TextMode="MultiLine" Height="80px"
                    Width="400px"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>�ͻ�����֧���ɹ����Ƿ���ͻ������ֻ����ţ���֪�俨�ź����룺</strong>
            </td>
            <td>
                <asp:RadioButtonList ID="RadlIsAutoSendCardNumber" runat="server" RepeatDirection="Horizontal"
                    RepeatLayout="Flow">
                    <asp:ListItem Value="true">��</asp:ListItem>
                    <asp:ListItem Value="false">��</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr class="tdbg" id = "ConfirmOrderMessage" runat ="server">
            <td class="tdbgleft">
                <strong>ȷ�϶���ʱ�ֻ�����֪ͨ���ݣ�</strong><br />
                ��֧��HTML���룬���ñ�ǩ�������ı�ǩ˵��
            </td>
            <td>
                <asp:TextBox ID="TxtConfirmOrderMessage" runat="server" TextMode="MultiLine" Height="80px"
                    Width="400px"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>�յ����л����ֻ�����֪ͨ���ݣ�</strong><br />
                ��֧��HTML���룬���ñ�ǩ�������ı�ǩ˵��<br />
                �ر��ǩ��<br />
                {$BankName}����������<br />
                {$Money}�������
            </td>
            <td>
                <asp:TextBox ID="TxtRemitMessage" runat="server" TextMode="MultiLine" Height="80px"
                    Width="400px"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg" id="RefundmentMessage" runat ="server">
            <td class="tdbgleft">
                <strong>�˿���ֻ�����֪ͨ���ݣ�</strong><br />
                ��֧��HTML���룬���ñ�ǩ�������ı�ǩ˵��
            </td>
            <td>
                <asp:TextBox ID="TxtRefundmentMessage" runat="server" TextMode="MultiLine" Height="80px"
                    Width="400px"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg" id="InvoiceMessage" runat ="server">
            <td class="tdbgleft">
                <strong>����Ʊ���ֻ�����֪ͨ���ݣ�</strong><br />
                ��֧��HTML���룬���ñ�ǩ�������ı�ǩ˵��
            </td>
            <td>
                <asp:TextBox ID="TxtInvoiceMessage" runat="server" TextMode="MultiLine" Height="80px"
                    Width="400px"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg" id="ConsignmentMessage" runat ="server">
            <td class="tdbgleft" >
                <strong>����������ֻ�����֪ͨ���ݣ�</strong><br />
                ��֧��HTML���룬���ñ�ǩ�������ı�ǩ˵��<br />
                �ر��ǩ��<br />
                {$ExpressCompany}����ݹ�˾<br />
                {$ExpressNumber}����ݵ���
            </td>
            <td>
                <asp:TextBox ID="TxtConsignmentMessage" runat="server" TextMode="MultiLine" Height="80px"
                    Width="400px"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg" id ="SendCardNumberMessage" runat ="server">
            <td class="tdbgleft">
                <strong>���Ϳ��ź��ֻ�����֪ͨ���ݣ�</strong><br />
                ��֧��HTML���룬���ñ�ǩ�������ı�ǩ˵��<br />
                �ر��ǩ��<br />
                {$CardInfo}������Ŀ��ż�������Ϣ<br />
            </td>
            <td>
                <asp:TextBox ID="TxtSendCardNumberMessage" runat="server" TextMode="MultiLine" Height="80px"
                    Width="400px"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg" id="UseLabel" runat ="server">
            <td class="tdbgleft">
                <strong>֪ͨ�����еĿ��ñ�ǩ�����壺</strong>
            </td>
            <td>
                <asp:TextBox ID="TxtUseLabel" runat="server" TextMode="MultiLine" Height="80px" Width="400px"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg" id ="CartInformMessage" runat ="server">
            <td class="tdbgleft">
                <strong>���ﳵ�����ֻ��ߵ�����֪ͨ���ݣ�</strong><br />
                ��֧��HTML���룬���ñ�ǩ��<br />
                {$UserName}����Ա�û���<br />
                {$UpdateTime}������ʱ��<br />
                {$CartInfo}�����ﳵ��Ϣ
            </td>
            <td>
                <asp:TextBox ID="TxtCartInformMessage" runat="server" TextMode="MultiLine" Height="80px"
                    Width="400px"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>����Ա������л���¼ʱ���͵��ֻ��������ݣ�</strong><br />
                ��֧��HTML���룬���ñ�ǩ��<br />
                {$UserName}����Ա�û���<br />
                {$Balance}���ʽ����<br />
                {$ReceiptDate}����������<br />
                {$Money}�������<br />
                {$BankName}����������
            </td>
            <td>
                <asp:TextBox ID="TxtBankLogMessage" runat="server" TextMode="MultiLine" Height="80px"
                    Width="400px"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>����Ա������������¼ʱ���͵��ֻ��������ݣ�</strong><br />
                ��֧��HTML���룬���ñ�ǩ��<br />
                {$UserName}����Ա�û���<br />
                {$Balance}���ʽ����<br />
                {$Money}��������<br />
                {$Reason}��ԭ��
            </td>
            <td>
                <asp:TextBox ID="TxtIncomeLogMessage" runat="server" TextMode="MultiLine" Height="80px"
                    Width="400px"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>����Ա���֧����¼ʱ���͵��ֻ��������ݣ�</strong><br />
                ��֧��HTML���룬���ñ�ǩ��<br />
                {$UserName}����Ա�û���<br />
                {$Balance}���ʽ����<br />
                {$Money}��֧�����<br />
                {$Reason}��ԭ��
            </td>
            <td>
                <asp:TextBox ID="TxtPayoutLogMessage" runat="server" TextMode="MultiLine" Height="80px"
                    Width="400px"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>����Ա�һ���ȯʱ���͵��ֻ��������ݣ�</strong><br />
                ��֧��HTML���룬���ñ�ǩ��<br />
                {$UserName}����Ա�û���<br />
                {$Balance}���ʽ����<br />
                {$UserPoint}�����õ�ȯ<br />
                {$Money}��֧�����<br />
                {$Point}���õ��ĵ�ȯ��
            </td>
            <td>
                <asp:TextBox ID="TxtExchangePointMessage" runat="server" TextMode="MultiLine" Height="80px"
                    Width="400px"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>����Ա������ȯʱ���͵��ֻ��������ݣ�</strong><br />
                ��֧��HTML���룬���ñ�ǩ��<br />
                {$UserName}����Ա�û���<br />
                {$UserPoint}�����õ�ȯ<br />
                {$Point}�����ӵĵ�ȯ��<br />
                {$Reason}������ԭ��
            </td>
            <td>
                <asp:TextBox ID="TxtEncouragePointMessage" runat="server" TextMode="MultiLine" Height="80px"
                    Width="400px"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>����Ա�۳���ȯʱ���͵��ֻ��������ݣ�</strong><br />
                ��֧��HTML���룬���ñ�ǩ��<br />
                {$UserName}����Ա�û���<br />
                {$UserPoint}�����õ�ȯ<br />
                {$Point}���۳��ĵ�ȯ��<br />
                {$Reason}���۳�ԭ��
            </td>
            <td>
                <asp:TextBox ID="TxtPayoutPointMessage" runat="server" TextMode="MultiLine" Height="80px"
                    Width="400px"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>����Ա�һ���Ч��ʱ���͵��ֻ��������ݣ�</strong><br />
                ��֧��HTML���룬���ñ�ǩ��<br />
                {$UserName}����Ա�û���<br />
                {$Balance}���ʽ����<br />
                {$ValidDays}��ʣ������<br />
                {$Money}��֧�����<br />
                {$Valid}���õ�����Ч��
            </td>
            <td>
                <asp:TextBox ID="TxtExchangePeriodMessage" runat="server" TextMode="MultiLine" Height="80px"
                    Width="400px"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>����Ա������Ч��ʱ���͵��ֻ��������ݣ�</strong><br />
                ��֧��HTML���룬���ñ�ǩ��<br />
                {$UserName}����Ա�û���<br />
                {$ValidDays}��ʣ������<br />
                {$Valid}���õ�����Ч��<br />
                {$Reason}������ԭ��
            </td>
            <td>
                <asp:TextBox ID="TxtEncouragePeriodMessage" runat="server" TextMode="MultiLine" Height="80px"
                    Width="400px"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>����Ա�۳���Ч��ʱ���͵��ֻ��������ݣ�</strong><br />
                ��֧��HTML���룬���ñ�ǩ��<br />
                {$UserName}����Ա�û���<br />
                {$ValidDays}��ʣ������<br />
                {$Valid}���۳�����Ч��<br />
                {$Reason}���۳�ԭ��
            </td>
            <td>
                <asp:TextBox ID="TxtPayoutPeriodMessage" runat="server" TextMode="MultiLine" Height="80px"
                    Width="400px"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>����Ա�����Ϣ���Ƿ����ֻ����Ÿ�֪��Ա��</strong>
            </td>
            <td>
                <asp:RadioButtonList ID="RadlIsAutoSendStateMessage" runat="server" RepeatDirection="Horizontal"
                    RepeatLayout="Flow">
                    <asp:ListItem Value="true">��</asp:ListItem>
                    <asp:ListItem Value="false">��</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>ȷ�����ʱ���͵��ֻ��������ݣ�</strong><br />
                ��֧��HTML���룬���ñ�ǩ��<br />
                {$UserName}����Ա�û���<br />
                {$Title}����Ϣ����<br />
                {$State}��״̬
            </td>
            <td>
                <asp:TextBox ID="TxtChangeStateMessage" runat="server" TextMode="MultiLine" Height="80px"
                    Width="400px"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbgbottom">
            <td colspan="2">
                <asp:Button ID="BtnSubmit" runat="server" Text="��������" OnClick="BtnSubmit_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
