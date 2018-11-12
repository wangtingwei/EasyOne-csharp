<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Controls.UserIndividuation" Codebehind="UserIndividuation.ascx.cs" %>
<tr class='tdbg' id="PnlPublicInfo" runat="server">
    <td class='tdbgleft'>
        <strong>����Ȩ�ޣ�</strong></td>
    <td>
        <asp:CheckBox ID="ChkPublicInfoNoNeedCheck" runat="server" />������Ϣ��Ҫ��˵���Ŀ����Ա������Ϣ����Ҫ���
        <br />
        <asp:CheckBox ID="ChkManageSelfPublicInfo" runat="server" /><pe:ExtendedLabel HtmlEncode="false" ID="LblManageSelfPublicInfo"
            runat="server" Text="�����޸ĺ�ɾ������˵ģ��Լ��ģ���Ϣ<br />" />
        <span id="SpanSetToNotCheck" runat="server"><asp:CheckBox ID="ChkSetToNotCheck" runat="server" /><pe:ExtendedLabel HtmlEncode="false" ID="LblSetToNotCheck" runat="server"
            Text="���ͨ���������ڻ�Ա�޸ĺ��Զ�תΪ����״̬<br />" /></span>
        <asp:CheckBox ID="ChkSetEditor" runat="server" /><pe:ExtendedLabel HtmlEncode="false" ID="LblSetEditor" runat="server"
            Text="������ϢʱHTML�༭��Ϊ�߼�ģʽ��Ĭ��Ϊ���ģʽ��<br />" />
        <asp:Label ID="LblMaxPublicInfoOneDay" runat="server" Text="ÿ����෢��" /><asp:TextBox
            ID="TxtMaxPublicInfoOneDay" MaxLength="5" Columns="5" runat="server" Text ="0"/><pe:ExtendedLabel HtmlEncode="false"
                ID="LblMaxPublicInfoOneDay2" runat="server" Text="����Ϣ����������������Ϊ<b>0</b>����<br /> " />
        <asp:Label ID="LblGetExp" runat="server" Text="������Ϣʱ��ȡ����Ϊ��Ŀ���õ�" /><asp:TextBox ID="TxtGetExp"
            MaxLength="5" Columns="5" runat="server" /><pe:ExtendedLabel HtmlEncode="false" ID="LblGetExp2" runat="server"
                Text="��<br />" />
        <asp:RangeValidator ID="ValgGetExp" ControlToValidate="TxtGetExp" runat="server"
                        ErrorMessage="��Χ��0��100֮��" Type="Double" Display="Dynamic" MaximumValue="100"
                        MinimumValue="0"></asp:RangeValidator>
        <asp:CheckBox ID="ChkIsXssFilter" runat="server" Checked ="true" />��Ա������Ϣʱ�Ƿ�����XSS����վ������<br />��������ã����ܵ��»�Ա��ӵ�ͼƬ/Flash/��Ƶ�Ȳ���������ʾ��������ã����п�վ����©����<br />��������δ��û�����û������Խ��ô˹��ܣ������������ô˹��ܣ�
    </td>
</tr>
<tr class='tdbg' id="PnlComment" runat="server">
    <td class='tdbgleft'>
        <strong>����Ȩ�ޣ�</strong></td>
    <td>
        <asp:CheckBox ID="ChkEnableComment" runat="server" />�ڽ�ֹ�������۵���Ŀ����Ȼ�ɷ�������<br />
        <asp:CheckBox ID="ChkCommentNeedCheck" runat="server" />��������Ҫ��˵���Ŀ�﷢�����۲���Ҫ���
    </td>
</tr>
<tr class='tdbg' id="PnlMessage" runat="server">
    <td class='tdbgleft'>
        <strong>����ϢȨ�ޣ�</strong></td>
    <td>
        ÿ������ͬʱ��<asp:TextBox ID="TxtMaxSendToUsers" runat="server" MaxLength="6" Columns="6"
            Text="2000" />�˷��Ͷ���Ϣ�����Ϊ0���������Ͷ���Ϣ��</td>
</tr>
<tr class='tdbg' id="PnlFavorite" runat="server">
    <td class='tdbgleft'>
        <strong>�ղؼ�Ȩ�ޣ�</strong></td>
    <td>
        ��Ա�ղؼ���������¼<asp:TextBox ID="TxtMaxSaveInfos" MaxLength="6" Columns="6" runat="server"
            Text="5000" />
        ����Ϣ�����Ϊ0����û���ղ�Ȩ�ޣ�</td>
</tr>
<tr class='tdbg' id="PnlUpload" runat="server">
    <td class='tdbgleft'>
        <strong>�ϴ�Ȩ�ޣ�</strong></td>
    <td>
        <asp:CheckBox ID="ChkEnableUpload" runat="server" />�����ڿ����ϴ���ģ�����ϴ��ļ�<br />
        ��������ϴ�<asp:TextBox ID="TxtFileUploadSize" MaxLength="6" Columns="6" runat="server"
            Text="5000" />
        K���ļ�����������ֵ�����ֶε�����ʱ�����ֶ�����Ϊ׼����</td>
</tr>
<tr class='tdbg' id="PnlShop" runat="server">
    <td class='tdbgleft'>
        <strong>�̵�Ȩ�ޣ�</strong></td>
    <td>
        <asp:CheckBox ID="ChkSetEnableSale" runat="server" />
        ��Ա���������Ʒʱ�򣬿���ָ��Ϊ��������<br />  
        ����ʱ�������ܵ��ۿ��ʣ�<asp:TextBox ID="TxtDiscount" runat="server" MaxLength="5" Columns="5"
            Text="80" />%<br />
        ����͸֧������ȣ�<asp:TextBox ID="TxtOverdraft" runat="server" MaxLength="5" Columns="5"
            Text="0" />Ԫ�����
        <br />
        <asp:CheckBox ID="ChkEnablePm" runat="server" />�Ƿ����������Ʒ<br />
    </td>
</tr>
<tr class='tdbg' id="PnlCharge" runat="server">
    <td class='tdbgleft'>
        <strong>�Ʒѷ�ʽ��</strong></td>
    <td>
        <asp:RadioButton ID="RadChargeByPoint" GroupName="RadCharge" runat="server" Checked ="true" />ֻ�ж�<pe:ShowPointName ID="ShowPointName" runat="server"></pe:ShowPointName>����<pe:ShowPointName ID="ShowPointName1" runat="server"></pe:ShowPointName>ʱ����ʹ��Ч���Ѿ����ڣ��Կ��Բ鿴�շ����ݣ�<pe:ShowPointName ID="ShowPointName4" runat="server"></pe:ShowPointName>����󣬼�ʹ��Ч��û�е��ڣ�Ҳ���ܲ鿴�շ����ݡ�<br />
        <asp:RadioButton ID="RadChargeByValidDate" GroupName="RadCharge" runat="server" />ֻ�ж���Ч�ڣ�ֻҪ����Ч���ڣ�<pe:ShowPointName ID="ShowPointName2" runat="server"></pe:ShowPointName>������Կ��Բ鿴�շ����ݣ����ں󣬼�ʹ��Ա��<pe:ShowPointName ID="ShowPointName5" runat="server"></pe:ShowPointName>Ҳ���ܲ鿴�շ����ݡ�<br />
        <asp:RadioButton ID="RadChargeByPointOrValidDate" GroupName="RadCharge" runat="server" />ͬʱ�ж�<pe:ShowPointName ID="ShowPointName3" runat="server"></pe:ShowPointName>����Ч�ڣ�<pe:ShowPointName ID="ShowPointName6" runat="server"></pe:ShowPointName>�������Ч�ڵ��ں󣬾Ͳ��ɲ鿴�շ����ݡ�<br />
        <asp:RadioButton ID="RadChargeByPointAndValidDate" GroupName="RadCharge" runat="server" />ͬʱ�ж�<pe:ShowPointName ID="ShowPointName7" runat="server"></pe:ShowPointName>����Ч�ڣ�<pe:ShowPointName ID="ShowPointName8" runat="server"></pe:ShowPointName>���겢����Ч�ڵ��ں󣬲Ų��ܲ鿴�շ����ݡ�
    </td>
</tr>
<tr class='tdbg' id="PnlMinusPoint" runat="server">
    <td class='tdbgleft'>
        <strong>��<pe:ShowPointName ID="ShowPointName9" runat="server"></pe:ShowPointName>��ʽ��</strong></td>
    <td>
        <asp:RadioButton ID="RadNotMinusPointNotWriteToLog" GroupName="RadPoint" runat="server" />��Ч���ڣ��鿴�շ����ݲ���<pe:ShowPointName ID="ShowPointName10" runat="server"></pe:ShowPointName>��Ҳ������¼��<br />
        <asp:RadioButton ID="RadWriteToLog" GroupName="RadPoint" runat="server" />��Ч���ڣ��鿴�շ����ݲ���<pe:ShowPointName ID="ShowPointName11" runat="server"></pe:ShowPointName>��������¼��<br />
        <asp:RadioButton ID="RadMinusPoint" GroupName="RadPoint" runat="server" Checked ="true" />��Ч���ڣ��鿴�շ�����Ҳ��<pe:ShowPointName ID="ShowPointName12" runat="server"></pe:ShowPointName>��<br />
        ��Ч���ڣ��ܹ����Կ�<asp:TextBox ID="TxtTotalViewInfoNumber" MaxLength="5" Columns="5" runat="server"
            Text="50" />���շ���Ϣ�����Ϊ0�������ƣ�<br />
        ��Ч���ڣ�ÿ�������Կ�<asp:TextBox ID="TxtViewInfoNumberOneDay" MaxLength="5" Columns="5" runat="server"
            Text="1" />���շ���Ϣ�����Ϊ0�������ƣ�
    </td>
</tr>
<tr class='tdbg' id="PnlEnableExchange" runat="server">
    <td class='tdbgleft'>
        <strong>������ֵ��</strong></td>
    <td>
        <asp:CheckBox ID="ChkEnableExchangePoint" runat="server" Checked ="true" />���������һ�<pe:ShowPointName ID="ShowPointName13" runat="server"></pe:ShowPointName>
        <asp:CheckBox ID="ChkEnableExchangeValidDate" runat="server" Checked ="true" />���������һ���Ч��
        <asp:CheckBox ID="ChkEnableGivePointToOthers" runat="server" Checked ="true"  />����<pe:ShowPointName ID="ShowPointName14" runat="server"></pe:ShowPointName>���͸�����
        <asp:CheckBox ID="ChkEnableBuyPoint" runat="server" Checked ="true"  />������<pe:ShowPointName ID="ShowPointName15" runat="server"></pe:ShowPointName>
    </td>
</tr>