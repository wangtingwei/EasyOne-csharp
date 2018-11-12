<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPage.master"
    Inherits="EasyOne.WebSite.Admin.User.UserShow" Title="��ʾ�û���Ϣ" Codebehind="UserShow.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <br />
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr align="center">
            <td id="TabTitle0" class="titlemouseover" onclick="ShowTabs(0)">
                ��Ա��Ϣ
            </td>
            <td id="TabTitle1" class="tabtitle" onclick="ShowTabs(1)">
                ��ϵ��Ϣ
            </td>
            <td id="TabTitle2" class="tabtitle" onclick="ShowTabs(2)">
                ������Ϣ
            </td>
            <td id="TabTitle3" class="tabtitle" onclick="ShowTabs(3)">
                ҵ����Ϣ
            </td>
            <td id="TabTitle4" class="tabtitle" style="<%= IsShow()%>" onclick="ShowTabs(4)">
                ��λ��Ϣ
            </td>
            <td id="TabTitle5" class="tabtitle" style="<%= IsShow()%>" onclick="ShowTabs(5)">
                ��λ��Ա
            </td>
            <td>
                &nbsp;
                <pe:ExtendedButton ID="EBtnRegCompany" OnClick="EBtnRegCompany_Click" Text="����Ϊ��ҵ��Ա"
                    OperateCode="UserUpdateToCompany" runat="server" />
                <pe:ExtendedButton ID="EBtnRegClient" OnClick="EBtnRegClient_Click" Text="����Ϊ�ͻ�"
                    OperateCode="UserUpdateToClient" runat="server" />
                <asp:Button ID="BtnToClient" runat="server" Text="�л�����Ӧ�ͻ���Ϣҳ" OnClick="BtnToClient_Click" />
            </td>
        </tr>
    </table>
    <table width="100%" border="0" cellpadding="5" cellspacing="1" class="border">
        <tbody id="Tabs0">
            <tr class="tdbg">
                <td style="width: 15%; text-align: right" class="tdbgleft">
                    �� Ա ID��
                </td>
                <td>
                    <asp:Label ID="LblUserId" runat="server" Text=""></asp:Label>
                </td>
                <td class="tdbgleft" style="width: 15%; text-align: right">
                    �����ַ��
                </td>
                <td style="width: 210px">
                    <asp:HyperLink ID="LnkEmail" runat="server"></asp:HyperLink>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft" style="text-align: right">
                    �� Ա ����
                </td>
                <td>
                    <asp:Label ID="LblUserName" runat="server" Text=""></asp:Label>
                </td>
                <td class="tdbgleft" style="text-align: right">
                    �û�״̬��
                </td>
                <td style="width: 210px">
                    <pe:ExtendedLabel HtmlEncode="false" ID="LblUserStatus" runat="server" Text=""></pe:ExtendedLabel>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft" style="text-align: right">
                    �� Ա �飺
                </td>
                <td>
                    <asp:Label ID="LblGroupName" runat="server" Text=""></asp:Label>
                </td>
                <td class="tdbgleft" style="text-align: right">
                    �û������飺
                </td>
                <td style="width: 210px">
                    <asp:Label ID="LblUserFriendGroup" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft" style="text-align: right">
                    ��ԱȨ�ޣ�
                </td>
                <td>
                    <asp:Label ID="LblSpecialPermission" runat="server" Text=""></asp:Label>
                </td>
                <td class="tdbgleft" style="text-align: right">
                    ��Ա���
                </td>
                <td style="width: 210px">
                    <asp:Label ID="LblUserType" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg" runat="server" id="BalancePoint">
                <td class="tdbgleft" style="text-align: right">
                    �ʽ���
                </td>
                <td>
                    <asp:Label ID="LblBalance" runat="server" Text=""></asp:Label>
                </td>
                <td class="tdbgleft" style="text-align: right;">
                    ����<pe:ShowPointName ID="ShowPointName" runat="server"></pe:ShowPointName>����
                </td>
                <td style="width: 210px">
                    <asp:Label ID="LblUserPoint" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg" runat="server" id="ExpValid">
                <td class="tdbgleft" style="text-align: right">
                    ���û��֣�
                </td>
                <td>
                    <asp:Label ID="LblUserExp" runat="server" Text=""></asp:Label>
                </td>
                <td class="tdbgleft" style="text-align: right">
                    ʣ��������
                </td>
                <td style="width: 210px">
                    <pe:ExtendedLabel HtmlEncode="false" ID="LblValidNum" runat="server" Text=""></pe:ExtendedLabel>
                    ��
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft" style="text-align: right">
                    ��ǩ���£�
                </td>
                <td>
                    <asp:Label ID="LblUnsignedItems" runat="server" Text=""></asp:Label>
                </td>
                <td class="tdbgleft" style="text-align: right">
                    ���Ķ��ţ�
                </td>
                <td style="width: 210px">
                    <asp:Label ID="LblUnreadMsg" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg" runat="server" id="ConsumeMoney">
                <td class="tdbgleft" style="text-align: right">
                    ���ѵĽ�
                </td>
                <td>
                    <asp:Label ID="LblConsumeMoney" runat="server" Text=""></asp:Label>
                </td>
                <td style="width: 15%; text-align: right;" class="tdbgleft">
                    ���ѵ�<pe:ShowPointName ID="ShowPointName1" runat="server"></pe:ShowPointName>����
                </td>
                <td style="width: 210px">
                    <asp:Label ID="LblConsumePoint" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg" runat="server" id="ConsumeExp">
                <td class="tdbgleft" style="text-align: right">
                    ���ѵĻ�������
                </td>
                <td>
                    <asp:Label ID="LblConsumeExp" runat="server" Text=""></asp:Label>
                </td>
                <td class="tdbgleft" style="text-align: right">
                    ��ӵ���Ϣ����
                </td>
                <td style="width: 210px">
                    <asp:Label ID="LblPostItems" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft" style="text-align: right">
                    ���ͨ������Ϣ����
                </td>
                <td>
                    <asp:Label ID="LblPassedItems" runat="server" Text=""></asp:Label>
                </td>
                <td class="tdbgleft" style="text-align: right">
                    ���˸����Ϣ����
                </td>
                <td style="width: 210px">
                    <asp:Label ID="LblRejectItems" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft" style="text-align: right">
                    ��ɾ������Ϣ����
                </td>
                <td>
                    <asp:Label ID="LblDelItems" runat="server" Text=""></asp:Label>
                </td>
                <td class="tdbgleft" style="text-align: right">
                    ��¼������
                </td>
                <td style="width: 210px">
                    <asp:Label ID="LblLoginTimes" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft" style="text-align: right">
                    ����¼ʱ�䣺
                </td>
                <td>
                    <asp:Label ID="LblLastLoginTime" runat="server" Text=""></asp:Label>
                </td>
                <td class="tdbgleft" style="text-align: right">
                    ����¼IP��
                </td>
                <td style="width: 210px">
                    <asp:Label ID="LblLastLoginIP" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft" style="text-align: right">
                    ע�����ڣ�
                </td>
                <td>
                    <asp:Label ID="LblRegTime" runat="server" Text=""></asp:Label>
                </td>
                <td class="tdbgleft" style="text-align: right">
                    �������ڣ�
                </td>
                <td style="width: 210px">
                    <asp:Label ID="LblJoinTime" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </tbody>
        <tbody id="Tabs1" style="display: none">
            <tr class="tdbg">
                <td class="tdbgleft" style="width: 15%; text-align: right">
                    ��ʵ������
                </td>
                <td style="width: 35%">
                    <asp:Label ID="LblTrueName" runat="server" Text=""></asp:Label>
                </td>
                <td class="tdbgleft" style="width: 15%; text-align: right">
                    ��ν��
                </td>
                <td style="width: 210px">
                    <asp:Label ID="LblTitle" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft" style="text-align: right">
                    ����/������
                </td>
                <td>
                    <asp:Label ID="LblCountry" runat="server" Text=""></asp:Label>
                </td>
                <td class="tdbgleft" style="text-align: right">
                    ʡ/�У�
                </td>
                <td style="width: 210px">
                    <asp:Label ID="LblProvince" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft" style="text-align: right">
                    ��/��/����
                </td>
                <td>
                    <asp:Label ID="LblCity" runat="server" Text=""></asp:Label>
                </td>
                <td class="tdbgleft" style="text-align: right">
                    �������룺
                </td>
                <td style="width: 210px">
                    <asp:Label ID="LblZipCode" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft" style="text-align: right">
                    ��ϵ��ַ��
                </td>
                <td colspan="3">
                    <asp:Label ID="LblAddress" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft" style="text-align: right">
                    �칫�绰��
                </td>
                <td>
                    <asp:Label ID="LblOfficePhone" runat="server" Text=""></asp:Label>
                </td>
                <td class="tdbgleft" style="text-align: right">
                    סլ�绰��
                </td>
                <td style="width: 210px">
                    <asp:Label ID="LblHomephone" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft" style="text-align: right">
                    �ƶ��绰��
                </td>
                <td>
                    <asp:Label ID="LblMobile" runat="server" Text=""></asp:Label>
                </td>
                <td class="tdbgleft" style="text-align: right">
                    ������룺
                </td>
                <td style="width: 210px">
                    <asp:Label ID="LblFax" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft" style="text-align: right">
                    С��ͨ��
                </td>
                <td>
                    <asp:Label ID="LblPHS" runat="server" Text=""></asp:Label>
                </td>
                <td class="tdbgleft" style="text-align: right">
                </td>
                <td style="width: 210px">
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft" style="text-align: right">
                    ������ҳ��
                </td>
                <td>
                    <asp:Label ID="LblHomePage" runat="server" Text="http://"></asp:Label>
                </td>
                <td class="tdbgleft" style="text-align: right">
                    Email��ַ��
                </td>
                <td style="width: 210px">
                    <asp:HyperLink ID="LnkEmail1" runat="server"></asp:HyperLink>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft" style="text-align: right">
                    QQ���룺
                </td>
                <td>
                    <pe:ExtendedLabel ID="LblQQ" runat="server" Text=""></pe:ExtendedLabel>
                </td>
                <td class="tdbgleft" style="text-align: right">
                    MSN�ʺţ�
                </td>
                <td style="width: 210px">
                    <asp:Label ID="LblMSN" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft" style="text-align: right">
                    ICQ���룺
                </td>
                <td>
                    <asp:Label ID="LblICQ" runat="server" Text=""></asp:Label>
                </td>
                <td class="tdbgleft" style="text-align: right">
                    �Ż�ͨ�ʺţ�
                </td>
                <td style="width: 210px">
                    <asp:Label ID="LblYahoo" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft" style="text-align: right">
                    UC�ʺţ�
                </td>
                <td>
                    <asp:Label ID="LblUC" runat="server" Text=""></asp:Label>
                </td>
                <td class="tdbgleft" style="text-align: right">
                    Aim�ʺţ�
                </td>
                <td style="width: 210px">
                    <asp:Label ID="LblAim" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </tbody>
        <tbody id="Tabs2" style="display: none">
            <tr class="tdbg">
                <td class="tdbgleft" style="width: 15%; text-align: right">
                    �������ڣ�
                </td>
                <td style="width: 35%;">
                    <asp:Label ID="LblBirthday" runat="server" Text=""></asp:Label>
                </td>
                <td class="tdbgleft" style="width: 15%; text-align: right">
                    ֤�����룺
                </td>
                <td style="width: 210px;">
                    <asp:Label ID="LblIDCard" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft" style="text-align: right">
                    ���᣺
                </td>
                <td>
                    <asp:Label ID="LblNativePlace" runat="server" Text=""></asp:Label>
                </td>
                <td class="tdbgleft" style="text-align: right">
                    ���壺
                </td>
                <td style="width: 210px">
                    <asp:Label ID="LblNation" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft" style="text-align: right">
                    �Ա�
                </td>
                <td>
                    <asp:Label ID="LblSex" runat="server" Text=""></asp:Label>
                </td>
                <td class="tdbgleft" style="text-align: right">
                    ����״����
                </td>
                <td style="width: 210px">
                    <asp:Label ID="LblMarriage" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft" style="text-align: right">
                    ѧ����
                </td>
                <td>
                    <asp:Label ID="LblEducation" runat="server" Text=""></asp:Label>
                </td>
                <td class="tdbgleft" style="text-align: right">
                    ��ҵѧУ��
                </td>
                <td style="width: 210px">
                    <asp:Label ID="LblGraduateFrom" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft" style="text-align: right">
                    ����ã�
                </td>
                <td>
                    <asp:Label ID="LblInterestsOfLife" runat="server" Text=""></asp:Label>
                </td>
                <td class="tdbgleft" style="text-align: right">
                    �Ļ����ã�
                </td>
                <td style="width: 210px">
                    <asp:Label ID="LblInterestsOfCulture" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft" style="text-align: right">
                    �������а��ã�
                </td>
                <td>
                    <asp:Label ID="LblInterestsOfAmusement" runat="server" Text=""></asp:Label>
                </td>
                <td class="tdbgleft" style="text-align: right">
                    �������ã�
                </td>
                <td style="width: 210px">
                    <asp:Label ID="LblInterestsOfSport" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft" style="text-align: right">
                    �������ã�
                </td>
                <td>
                    <asp:Label ID="LblInterestsOfOther" runat="server" Text=""></asp:Label>
                </td>
                <td class="tdbgleft" style="text-align: right">
                    �� �� �룺
                </td>
                <td style="width: 210px">
                    <asp:Label ID="LblIncome" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </tbody>
        <tbody id="Tabs3" style="display: none">
            <tr class="tdbg">
                <td class="tdbgleft" style="width: 15%; text-align: right">
                    ��λ���ƣ�
                </td>
                <td style="width: 35%">
                    <asp:Label ID="LblCompany" runat="server" Text=""></asp:Label>
                </td>
                <td class="tdbgleft" style="width: 15%; text-align: right">
                    �������ţ�
                </td>
                <td style="width: 210px">
                    <asp:Label ID="LblDepartment" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft" style="text-align: right">
                    ְλ��
                </td>
                <td>
                    <asp:Label ID="LblPosition" runat="server" Text=""></asp:Label>
                </td>
                <td class="tdbgleft" style="text-align: right">
                    ����ҵ��
                </td>
                <td style="width: 210px">
                    <asp:Label ID="LblOperation" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft" style="text-align: right">
                    ��λ��ַ��
                </td>
                <td colspan="3">
                    <asp:Label ID="LblCompanyAddress" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </tbody>
        <tbody id="Tabs4" style="display: none">
            <tr class="tdbg">
                <td>
                    <pe:CompanyInfo ID="CompanyInfo1" runat="server" />
                </td>
            </tr>
        </tbody>
        <tbody id="Tabs5" style="display: none">
            <tr class="tdbg">
                <td>
                    <pe:CompanyMemberManage ID="CompanyMemberManage1" runat="server" />
                </td>
            </tr>
        </tbody>
    </table>
    <br />
    <table width="100%" style="width: 100%; height: 60px;">
        <tr align="center">
            <td align="left">
                <pe:ExtendedButton IsChecked="true" OperateCode="UserModify" ID="BtnModifyUserSubmit"
                    runat="server" Text="�޸Ļ�Ա��Ϣ" OnClick="BtnModifyUserSubmit_Click" />
                <pe:ExtendedButton IsChecked="true" OperateCode="UserModifyPermissions" ID="BtnModifyPurview"
                    runat="server" Text="�޸Ļ�ԱȨ��" OnClick="BtnModifyPurview_Click" CausesValidation="False" />
                <pe:ExtendedButton IsChecked="true" OperateCode="UserLock" ID="BtnLock" runat="server"
                    OnClientClick="return confirm('ȷ��Ҫ�����˻�Ա��');" Text=" �����˻�Ա " OnClick="BtnLock_Click"
                    CausesValidation="False" />
                <pe:ExtendedButton IsChecked="true" OperateCode="UserDelete" ID="BtnDelete" runat="server"
                    Text=" ɾ���˻�Ա " OnClientClick="return confirm('ȷ��Ҫɾ���˻�Ա��');" OnClick="BtnDelete_Click" />
                <pe:ExtendedButton IsChecked="true" OperateCode="MessageManage" ID="BtnMessage" runat="server"
                    Text=" ���Ͷ���Ϣ " OnClick="BtnMessage_Click" UseSubmitBehavior="False" />
                <pe:ExtendedButton IsChecked="true" OperateCode="UserMoneyManage" ID="BtnIncome"
                    runat="server" Text="������л��" UseSubmitBehavior="False" OnClick="BtnIncome_Click" />
                <pe:ExtendedButton IsChecked="true" OperateCode="UserMoneyManage" ID="OtherIncome"
                    runat="server" Text="�����������" OnClick="OtherIncome_Click" />
                <br />
                <pe:ExtendedButton IsChecked="true" OperateCode="UserMoneyManage" ID="BtnPayment"
                    runat="server" Text="���֧�����" UseSubmitBehavior="False" OnClick="BtnPayment_Click" />
                <pe:ExtendedButton IsChecked="true" OperateCode="UserPointManage" ID="BtnExchangePoint"
                    runat="server" Text="   ��ȯ�һ�   " UseSubmitBehavior="False" OnClick="BtnExchangePoint_Click" />
                <pe:ExtendedButton IsChecked="true" OperateCode="UserPointManage" ID="BtnAddPoint"
                    runat="server" Text="  ������ȯ  " UseSubmitBehavior="False" OnClick="BtnAddPoint_Click" />
                <pe:ExtendedButton IsChecked="true" OperateCode="UserPointManage" ID="BtnMinusPoint"
                    runat="server" Text="  �۳���ȯ  " UseSubmitBehavior="False" OnClick="BtnMinusPoint_Click" />
                <pe:ExtendedButton IsChecked="true" OperateCode="UserValidDateManage" ID="BtnExchangeValid"
                    runat="server" Text=" �һ���Ч�� " UseSubmitBehavior="False" OnClick="BtnExchangeValid_Click" />
                <pe:ExtendedButton IsChecked="true" OperateCode="UserValidDateManage" ID="BtnAddValidDate"
                    runat="server" Text=" �����Ч�� " UseSubmitBehavior="False" OnClick="BtnAddValidDate_Click" />
                <pe:ExtendedButton IsChecked="true" OperateCode="UserValidDateManage" ID="BtnMinusValidDate"
                    runat="server" Text=" �۳���Ч�� " UseSubmitBehavior="False" OnClick="BtnMinusValidDate_Click" />
                <pe:ExtendedButton IsChecked="true" OperateCode="OrderAdd" ID="BtnOrderAdd" runat="server"
                    Text="  ��Ӷ���  " UseSubmitBehavior="False" Visible="false" OnClick="BtnOrderAdd_Click" />
                <pe:ExtendedButton ID="EBtnSendEmail" Text=" �����ʼ� " IsChecked="true" OperateCode="sendinfomanage"
                    OnClick="EBtnSendEmail_Click" CausesValidation="False" runat="server" />
                <pe:ExtendedButton ID="EBtnSendTelMessage" Text="���ֻ�����" IsChecked="true" OperateCode="smsmanage"
                    OnClick="EBtnSendTelMessage_Click" CausesValidation="False" runat="server" />
            </td>
            <asp:HiddenField ID="HdnLockType" runat="server" />
        </tr>
    </table>
    <div runat="server" id="Details">
        <table id="Table1" width="100%" border="0" cellpadding="0" cellspacing="0" runat="server">
            <tr id="Tr1" align="center" runat="server">
                <td id="InfoTabTitle0" class="tabtitle" runat="server">
                    ��Ա����
                </td>
                <td id="InfoTabTitle1" class="tabtitle" runat="server">
                    �ʽ���ϸ
                </td>
                <td id="InfoTabTitle2" class="tabtitle" runat="server">
                    <%=m_PointName %>��ϸ
                </td>
                <td id="InfoTabTitle3" class="tabtitle" runat="server">
                    ��Ч����ϸ
                </td>
                <td id="InfoTabTitle4" class="tabtitle" runat="server">
                    ����֧����ϸ
                </td>
                <td id="InfoTabTitle5" class="tabtitle" visible="false" runat="server">
                    ��Ͷ�߼�¼
                </td>
                <td id="InfoTabTitle6" class="tabtitle" visible="false" runat="server">
                    ������
                </td>
                <td id="InfoTabTitle7" class="tabtitle" visible="false" runat="server">
                    ���˵�
                </td>
                <td id="InfoNull" runat="server">
                    &nbsp;
                </td>
            </tr>
        </table>
        <pe:ExtendedGridView ID="EgvOrder" Visible="false" ItemName="����" ItemUnit="��" AutoGenerateColumns="False"
            DataKeyNames="OrderId" AllowPaging="True" runat="server" OnRowDataBound="EgvOrder_RowDataBound">
            <Columns>
                <pe:TemplateField HeaderText="�������" SortExpression="OrderNum">
                    <ItemTemplate>
                        <a href='../Shop/OrderManage.aspx?OrderID=<%#Eval("OrderId")%>'>
                            <%#Eval("OrderNum")%>
                        </a>
                    </ItemTemplate>
                </pe:TemplateField>
                <pe:BoundField DataField="ClientName" HeaderText="�ͻ�����" SortExpression="ClientName"
                    >
                    <HeaderStyle Width="10%" />
                </pe:BoundField>
                <pe:BoundField DataField="UserName" HeaderText="�û���" SortExpression="UserName" >
                    <HeaderStyle Width="10%" />
                </pe:BoundField>
                <pe:TemplateField HeaderText="�µ�ʱ��" SortExpression="InputTime">
                    <HeaderStyle Width="14%" />
                    <ItemTemplate>
                        <%# Eval("InputTime", "{0:yyyy-MM-dd HH:mm:ss}")%>
                    </ItemTemplate>
                </pe:TemplateField>
                <pe:TemplateField HeaderText="�������" SortExpression="MoneyTotal">
                    <HeaderStyle Width="8%" />
                    <ItemTemplate>
                        <%# Eval("MoneyTotal", "{0:N2}")%>
                    </ItemTemplate>
                </pe:TemplateField>
                <pe:TemplateField HeaderText="�տ���" SortExpression="MoneyReceipt">
                    <HeaderStyle Width="8%" />
                    <ItemTemplate>
                        <%# Eval("MoneyReceipt", "{0:N2}")%>
                    </ItemTemplate>
                </pe:TemplateField>
                <pe:TemplateField HeaderText="��Ҫ��Ʊ" SortExpression="NeedInvoice">
                    <HeaderStyle Width="5%" />
                    <ItemTemplate>
                        <%# (bool)Eval("NeedInvoice") == false ? "<font color=red>��</font>" : "��"%>
                    </ItemTemplate>
                </pe:TemplateField>
                <pe:TemplateField HeaderText="�ѿ���Ʊ">
                    <HeaderStyle Width="5%" />
                    <ItemTemplate>
                        <%# (bool)Eval("Invoiced") == false ? "<font color=red>��</font>" : "��"%>
                    </ItemTemplate>
                </pe:TemplateField>
                <pe:TemplateField HeaderText="����״̬">
                    <HeaderStyle Width="10%" />
                    <ItemTemplate>
                        <pe:ExtendedLabel HtmlEncode="false" ID="LblOrderStatus" runat="server" />
                    </ItemTemplate>
                </pe:TemplateField>
                <pe:TemplateField HeaderText="����״̬">
                    <HeaderStyle Width="10%" />
                    <ItemTemplate>
                        <pe:ExtendedLabel HtmlEncode="false" ID="LblPayStatus" runat="server" />
                    </ItemTemplate>
                </pe:TemplateField>
                <pe:TemplateField HeaderText="����״̬">
                    <HeaderStyle Width="10%" />
                    <ItemTemplate>
                        <pe:ExtendedLabel HtmlEncode="false" ID="LblDeliverStatus" runat="server" ForeColor="AliceBlue" />
                    </ItemTemplate>
                </pe:TemplateField>
            </Columns>
        </pe:ExtendedGridView>
        <pe:ExtendedGridView ID="EgvBankrollItem" runat="server" AutoGenerateColumns="False"
            ShowFooter="True" EmptyDataText="û���κη����������ʽ��¼��" ItemName="�ʽ���ϸ" AllowPaging="True"
            OnDataBound="EgvBankrollItem_DataBound" OnRowDataBound="EgvBankrollItem_RowDataBound"
            SerialText="" DataKeyNames="ItemID" OnRowCommand="EgvBankrollItem_RowCommand"
            CheckBoxFieldHeaderWidth="3%" IsHoldState="True">
            <Columns>
                <asp:BoundField DataField="DateAndTime" HeaderText="����ʱ��" SortExpression="DateAndTime"
                    DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HtmlEncode="False">
                    <HeaderStyle Width="16%" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="���׷�ʽ" SortExpression="MoneyType">
                    <HeaderStyle Width="8%" />
                    <ItemTemplate>
                        <%#GetMoneyType(Eval("MoneyType")) %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="����" SortExpression="CurrencyType">
                    <HeaderStyle Width="8%" />
                    <ItemTemplate>
                        <%#GetCurrencyType(Eval("CurrencyType")) %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="������">
                    <HeaderStyle Width="10%" />
                    <ItemTemplate>
                        <%# (decimal)Eval("Money")>0?Eval("Money","{0:N2}"):"" %>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Right" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="֧�����">
                    <HeaderStyle Width="10%" />
                    <ItemTemplate>
                        <%#  (decimal)Eval("Money")>0?"":Math.Abs((decimal)Eval("Money")).ToString("N2") %>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Right" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="��������" SortExpression="Bank">
                    <HeaderStyle Width="8%" />
                    <ItemTemplate>
                        <%# Eval("Bank")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="��ע/˵��">
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemTemplate>
                        <asp:Label runat="server" Text='' ID="LblRemark"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ȷ��">
                    <HeaderStyle Width="5%" />
                    <ItemTemplate>
                        <%#(int)Eval("Status") == 0 ? "<font color=red>��</font>" : "��"%>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </pe:ExtendedGridView>
        <pe:ExtendedGridView ID="EgvUserPoint" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            DataKeyNames="LogId" EmptyDataText="�����κ��û�<%=m_PointName %>���ݣ�" ShowFooter="True"
            ItemName="��¼" ItemUnit="��" OnRowDataBound="EgvUserPoint_RowDataBound" OnDataBound="EgvUserPoint_DataBound">
            <Columns>
                <pe:BoundField DataField="LogTime" HeaderText="����ʱ��" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"
                    SortExpression="LogTime" HtmlEncode="False">
                    <HeaderStyle Width="20%" />
                </pe:BoundField>
                <pe:BoundField DataField="IP" HeaderText="IP��ַ" SortExpression="IP">
                    <HeaderStyle Width="10%" />
                </pe:BoundField>
                <pe:TemplateField HeaderText="<%=m_PointName %>��" SortExpression="IncomePayOut">
                    <HeaderStyle Width="10%" />
                    <ItemTemplate>
                        <pe:ExtendedLabel HtmlEncode="false" runat="server" ID="LblIncomePayOut">
                        </pe:ExtendedLabel>
                    </ItemTemplate>
                </pe:TemplateField>
                <pe:BoundField DataField="Times" HeaderText="�ظ�����" SortExpression="Times">
                    <HeaderStyle Width="10%" />
                </pe:BoundField>
                <pe:BoundField DataField="Inputer" HeaderText="����Ա" SortExpression="Inputer" >
                    <HeaderStyle Width="10%" />
                </pe:BoundField>
                <pe:BoundField DataField="Remark" HeaderText="��ע/˵��" SortExpression="Remark" >
                    <ItemStyle HorizontalAlign="Left" />
                </pe:BoundField>
            </Columns>
        </pe:ExtendedGridView>
        <pe:ExtendedGridView ID="EgvUserValid" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            DataKeyNames="LogId" ItemName="��¼" ItemUnit="��">
            <Columns>
                <pe:BoundField DataField="LogTime" HeaderText="ʱ��" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"
                    SortExpression="LogTime" HtmlEncode="False">
                    <HeaderStyle Width="20%" />
                </pe:BoundField>
                <pe:BoundField DataField="IP" HeaderText="IP��ַ" SortExpression="IP">
                    <HeaderStyle Width="10%" />
                </pe:BoundField>
                <pe:TemplateField HeaderText="��Ч��" SortExpression="IncomePayout">
                    <HeaderStyle Width="10%" />
                    <ItemTemplate>
                        <pe:ExtendedLabel HtmlEncode="false" runat="server" Text='<%#IncomePayout(Convert.ToInt32(DataBinder.Eval(Container.DataItem,"IncomePayout")),Convert.ToInt32(DataBinder.Eval(Container.DataItem,"ValidNum")))%>'
                            ID="LblIncomePayOut">
                        </pe:ExtendedLabel>
                    </ItemTemplate>
                </pe:TemplateField>
                <pe:BoundField DataField="Inputer" HeaderText="����Ա" SortExpression="Inputer" >
                    <HeaderStyle Width="10%" />
                </pe:BoundField>
                <pe:BoundField DataField="Remark" HeaderText="��ע/˵��" SortExpression="Remark" >
                    <ItemStyle HorizontalAlign="Left" />
                </pe:BoundField>
            </Columns>
        </pe:ExtendedGridView>
        <pe:ExtendedGridView ID="GdvPaymentLogList" runat="server" DataKeyNames="PaymentLogId"
            AllowPaging="True" AutoGenerateColumns="False" ItemName="��¼" ItemUnit="��" OnRowDataBound="GdvPaymentLogList_RowDataBound">
            <Columns>
                <pe:BoundField DataField="PaymentNum" HeaderText="֧�����" SortExpression="PaymentNum"
                    >
                    <HeaderStyle Width="120px" />
                </pe:BoundField>
                <pe:TemplateField HeaderText="֧��ƽ̨">
                    <ItemTemplate>
                        <asp:Label ID="LblPlatform" runat="server" />
                    </ItemTemplate>
                </pe:TemplateField>
                <pe:TemplateField HeaderText="����ʱ��" SortExpression="PayTime">
                    <HeaderStyle Width="120px" />
                    <ItemTemplate>
                        <%# Eval("PayTime", "{0:yyyy-MM-dd HH:mm:ss}")%>
                    </ItemTemplate>
                </pe:TemplateField>
                <pe:TemplateField HeaderText="�����" SortExpression="MoneyPay">
                    <HeaderStyle Width="80px" />
                    <ItemTemplate>
                        <%# Eval("MoneyPay", "{0:N2}")%>
                    </ItemTemplate>
                </pe:TemplateField>
                <pe:TemplateField HeaderText="ʵ��ת�˽��" SortExpression="MoneyTrue">
                    <HeaderStyle Width="80px" />
                    <ItemTemplate>
                        <%# Eval("MoneyTrue", "{0:N2}")%>
                    </ItemTemplate>
                </pe:TemplateField>
                <pe:TemplateField HeaderText="����״̬">
                    <HeaderStyle Width="60px" />
                    <ItemTemplate>
                        <asp:Label ID="LblStatus" runat="server" />
                        <itemstyle horizontalalign="Center" />
                    </ItemTemplate>
                </pe:TemplateField>
            </Columns>
        </pe:ExtendedGridView>
        <pe:ExtendedGridView ID="EgvComplain" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            DataKeyNames="ItemId" OnRowDataBound="EgvComplain_RowDataBound">
            <Columns>
                <pe:BoundField DataField="DateAndTime" HeaderText="Ͷ��ʱ��" SortExpression="DateAndTime"
                    DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HtmlEncode="False">
                    <HeaderStyle Width="16%" />
                </pe:BoundField>
                <pe:TemplateField HeaderText="�ͻ�����">
                    <HeaderStyle Width="10%" />
                    <ItemTemplate>
                        <pe:ExtendedAnchor ID="ExtendedAnchor1" IsChecked="true" runat="server" OperateCode="ClientView" href='<%# Eval("ClientId", "../Crm/ClientShow.aspx?ClientId={0}") %>'>
                            <%# Eval("ShortedForm") %>
                        </pe:ExtendedAnchor>
                    </ItemTemplate>
                </pe:TemplateField>
                <pe:BoundField HeaderText="Ͷ������" SortExpression="ComplainType" >
                    <HeaderStyle Width="12%" />
                </pe:BoundField>
                <pe:TemplateField HeaderText="����">
                    <ItemTemplate>
                        <pe:ExtendedAnchor ID="ExtendedAnchor2" IsChecked="true" runat="server" OperateCode="ComplainView" href='<%# Eval("ItemId", "../Crm/ComplainShow.aspx?ItemId={0}") %>'>
                            <%# Eval("Title") %>
                        </pe:ExtendedAnchor>
                    </ItemTemplate>
                </pe:TemplateField>
                <pe:BoundField HeaderText="�����̶�" SortExpression="MagnitudeOfExigence" >
                    <HeaderStyle Width="8%" />
                </pe:BoundField>
                <pe:BoundField HeaderText="��¼״̬" SortExpression="Status" >
                    <HeaderStyle Width="8%" />
                </pe:BoundField>
            </Columns>
        </pe:ExtendedGridView>
        <pe:ExtendedGridView ID="EgvAgentOrders" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            CheckBoxFieldHeaderWidth="3%" DataKeyNames="OrderID" IsHoldState="True" SerialText=""
            OnRowDataBound="EgvAgentOrders_RowDataBound" EmptyDataText="û���κζ�����" OnDataBound="EgvAgentOrders_DataBound"
            ShowFooter="True" Visible="False">
            <Columns>
                <asp:HyperLinkField DataNavigateUrlFields="OrderId" DataTextField="OrderNum" HeaderText="�������"
                    DataNavigateUrlFormatString="../Shop/OrderManage.aspx?OrderID={0}" />
                <asp:HyperLinkField DataNavigateUrlFields="ClientId" DataNavigateUrlFormatString="../Crm/ClientShow.aspx?ClientID={0}"
                    DataTextField="ClientName" HeaderText="�ͻ�����" />
                <asp:HyperLinkField DataNavigateUrlFields="UserName" DataNavigateUrlFormatString="UserShow.aspx?UserName={0}"
                    DataTextField="UserName" HeaderText="�û���" />
                <pe:BoundField HeaderText="�µ�ʱ��" DataField="InputTime" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False" />
                <pe:BoundField HeaderText="�������" DataField="MoneyTotal" DataFormatString="{0:0.00}"
                    HtmlEncode="False">
                    <ItemStyle HorizontalAlign="Right" />
                </pe:BoundField>
                <pe:BoundField HeaderText="�տ���" DataField="MoneyReceipt" DataFormatString="{0:0.00}"
                    HtmlEncode="False">
                    <ItemStyle HorizontalAlign="Right" />
                </pe:BoundField>
                <pe:TemplateField HeaderText="��Ҫ��Ʊ">
                    <ItemTemplate>
                        <%#(bool)Eval("NeedInvoice") ? "<span style=\"color:red\">��<span>" : ""%>
                    </ItemTemplate>
                </pe:TemplateField>
                <pe:TemplateField HeaderText="�ѿ���Ʊ">
                    <ItemTemplate>
                        <%# (bool)Eval("NeedInvoice") ? ((bool)Eval("Invoiced") ? "��" : "<span style=\"color:red\">��<span>") : ""%>
                    </ItemTemplate>
                </pe:TemplateField>
                <pe:TemplateField HeaderText="����״̬">
                    <ItemTemplate>
                        <pe:ExtendedLabel HtmlEncode="false" runat="server" ID="LblStatus"></pe:ExtendedLabel>
                    </ItemTemplate>
                </pe:TemplateField>
                <pe:TemplateField HeaderText="����״̬">
                    <ItemTemplate>
                        <pe:ExtendedLabel HtmlEncode="false" runat="server" ID="LblPaymentStatus"></pe:ExtendedLabel>
                    </ItemTemplate>
                </pe:TemplateField>
                <pe:TemplateField HeaderText="����״̬">
                    <ItemTemplate>
                        <pe:ExtendedLabel HtmlEncode="false" runat="server" ID="LblDeliverStatus"></pe:ExtendedLabel>
                    </ItemTemplate>
                </pe:TemplateField>
            </Columns>
        </pe:ExtendedGridView>

        <script language="javascript" type="text/javascript">
          function ShowTabs(ID){
               for (i=0;i< 6;i++){
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

        <pe:ExtendedGridView ID="EgvBill" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            CheckBoxFieldHeaderWidth="3%" IsHoldState="True" OnDataBound="EgvBill_OnDataBound"
            OnRowDataBound="EgvBill_OnRowDataBound" SerialText="" ShowFooter="True">
            <FooterStyle CssClass="tdbg" />
            <Columns>
                <asp:BoundField DataField="DateAndTime" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"
                    HeaderText="����ʱ��" HtmlEncode="False"></asp:BoundField>
                <asp:BoundField DataField="OrderNum" HeaderText="������"></asp:BoundField>
                <asp:TemplateField HeaderText="������">
                    <ItemStyle HorizontalAlign="Right" />
                    <ItemTemplate>
                        <asp:Label ID="LblRecieveMoney" runat="server" Text=""></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="֧�����">
                    <ItemStyle HorizontalAlign="Right" />
                    <ItemTemplate>
                        <asp:Label ID="LblPayoutMoney" runat="server" Text=""></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Remark" HeaderText="��ע/˵��"></asp:BoundField>
            </Columns>
        </pe:ExtendedGridView>
    </div>
    <asp:ObjectDataSource ID="OdsInfo" runat="server" EnablePaging="True"></asp:ObjectDataSource>
    <asp:Literal ID="LblBankrollItemNotice" runat="server" Text="ע�⣺ûȷ�ϵ��ʽ𽫲������ϼƵ��С�" Visible="false" />
</asp:Content>
