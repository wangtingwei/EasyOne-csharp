<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Analytics.StatConfig" Title="��վͳ�ƹ���" ValidateRequest="false" Codebehind="StatConfig.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">

    <script type="text/javascript">
    function ConfirmModify()
    {
      returun (confirm('ǿ�ҽ��龡��ѡ���ٵ�ͳ�ƹ�����Ŀ�����һ���������ã�����'))
    }
    
    var tID=0;
    
    function ShowTabs(ID)
    {
      if(ID!=tID){
        document.getElementById("TabTitle"+ID).className = 'titlemouseover';
        document.getElementById("TabTitle"+tID).className = 'tabtitle';
        document.getElementById("Tabs"+tID).style.display = 'none';
        document.getElementById("Tabs"+ID).style.display = '';
        tID=ID;
      }
    }
    function setFileFileds(num){    
        var str="";
        if (num==1){
            str = str + "<s"+ "c" + "r" + "i" + "pt src='{PE.SiteConfig.sitepath/}Analytics/CounterLink.aspx?Style=simple'></sc" + "ri" +"pt>";
        }
        else if(num==2){
            str = str + "<s"+ "c" + "r" + "i" + "pt src='{PE.SiteConfig.sitepath/}Analytics/CounterLink.aspx?Style=common'></sc" + "ri" +"pt>";
        }
        else if(num==3){
            str = str + "<s"+ "c" + "r" + "i" + "pt src='{PE.SiteConfig.sitepath/}Analytics/CounterLink.aspx?Style=all'></sc" + "ri" +"pt>";
        }
        else if(num==4){
            str = str + "<s"+ "c" + "r" + "i" + "pt src='{PE.SiteConfig.sitepath/}Analytics/CounterLink.aspx?Style=none'></sc" + "ri" +"pt>";
        }
        document.getElementById("selectKey").value = str;
    }
    function setValue()
    {
        setFileFileds(1);
        document.getElementById("LinkContent").value = "<a href='{PE.SiteConfig.sitepath/}Analytics/ShowOnline.aspx' target='_blank'>��վ���������ϸ�б�</a>";
    }
    </script>

    <table width="100%" border="0" cellpadding="0" cellspacing="0" style="text-align: center;">
        <tr align="center">
            <td id="TabTitle0" class="titlemouseover" onclick="ShowTabs(0)">
                ������Ϣ</td>
            <td id="TabTitle1" class="tabtitle" onclick="ShowTabs(1)">
                ��ʼ������</td>
            <td id="TabTitle2" class="tabtitle" onclick="ShowTabs(2)">
                ������Ŀ</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tbody id="Tabs0">
            <tr class="tdbg">
                <td style="width: 30%;" class="tdbgleft">
                    <strong>����������ʱ����</strong></td>
                <td>
                    <asp:DropDownList ID="DropTimezone" runat="server">
                         		<asp:ListItem>��ѡ��...</asp:ListItem>
	 				    		<asp:ListItem Value="-12">(GMT -12:00) �ս�����</asp:ListItem>
	 				    		<asp:ListItem Value="-11">(GMT -11:00) ��;��,��Ħ��Ⱥ��</asp:ListItem>
	 				    		<asp:ListItem Value="-10">(GMT -10:00) ������</asp:ListItem>
	 				    		<asp:ListItem Value="-9">(GMT -09:00) ����˹��</asp:ListItem>
	 				    		<asp:ListItem Value="-8">(GMT -08:00) ̫ƽ��ʱ��(�����ͼ��ô�)</asp:ListItem>
	 				    		<asp:ListItem Value="-8">(GMT -08:00) �ٻ���</asp:ListItem>
	 				    		<asp:ListItem Value="-7">(GMT -07:00) ɽ��ʱ��(�����ͼ��ô�)  </asp:ListItem>
	 				    		<asp:ListItem Value="-7">(GMT -07:00) ����ɣ��</asp:ListItem>
	 				    		<asp:ListItem Value="-7">(GMT -07:00) ������,����˹,��������</asp:ListItem>
	 				    		<asp:ListItem Value="-6">(GMT -06:00) ��˹������</asp:ListItem>
	 				    		<asp:ListItem Value="-6">(GMT -06:00) �в�ʱ��(�����ͼ��ô�)</asp:ListItem>
	 				    		<asp:ListItem Value="-6">(GMT -06:00) ������</asp:ListItem>
	 				    		<asp:ListItem Value="-6">(GMT -06:00) �ϴ�������,ī�����,������</asp:ListItem>
	 				    		<asp:ListItem Value="-5">(GMT -05:00) �����,����,����</asp:ListItem>
	 				    		<asp:ListItem Value="-5">(GMT -05:00) ����ʱ��(�����ͼ��ô�)</asp:ListItem>
	 				    		<asp:ListItem Value="-5">(GMT -05:00) ӡ�ڰ�����(����)</asp:ListItem>
	 				    		<asp:ListItem Value="-4">(GMT -04:00) ������ʱ��(���ô�)</asp:ListItem>
	 				    		<asp:ListItem Value="-4">(GMT -04:00) ������˹,����˹</asp:ListItem>
	 				    		<asp:ListItem Value="-4">(GMT -04:00) ʥ���Ǹ�</asp:ListItem>
	 				    		<asp:ListItem Value="-3.5">(GMT -03:30) Ŧ����</asp:ListItem>
	 				    		<asp:ListItem Value="-3">(GMT -03:00) ��������</asp:ListItem>
	 				    		<asp:ListItem Value="-3">(GMT -03:00) ����ŵ˹����˹,���ζ�</asp:ListItem>
	 				    		<asp:ListItem Value="-3">(GMT -03:00) ������</asp:ListItem>
	 				    		<asp:ListItem Value="-2">(GMT -02:00) �д�����</asp:ListItem>
	 				    		<asp:ListItem Value="-1">(GMT -01:00) ��ý�Ⱥ��</asp:ListItem>
	 				    		<asp:ListItem Value="-1">(GMT -01:00) ���ٶ�Ⱥ��</asp:ListItem>
	 				    		<asp:ListItem Value="0">(GMT  00:00) ��������ʱ�䣺������,������,�׶�,��˹��</asp:ListItem>
	 				    		<asp:ListItem Value="0">(GMT  00:00) ����������,����ά��</asp:ListItem>
	 				    		<asp:ListItem Value="1">(GMT +01:00) ��ķ˹�ص�,����,������,����,˹�¸��Ħ,άҲ��</asp:ListItem>
	 				    		<asp:ListItem Value="1">(GMT +01:00) ����������,������˹����,������˹,¬��������,������</asp:ListItem>
	 				    		<asp:ListItem Value="1">(GMT +01:00) ��³����,�籾����,�����,����</asp:ListItem>
	 				    		<asp:ListItem Value="1">(GMT +01:00) ��������,˹������</asp:ListItem>
	 				    		<asp:ListItem Value="1">(GMT +01:00) ������,ά��Ŧ˹</asp:ListItem>
	 				    		<asp:ListItem Value="1">(GMT +01:00) ��ɳ,�����ղ�</asp:ListItem>
	 				    		<asp:ListItem Value="1">(GMT +01:00) �з�����</asp:ListItem>
	 				    		<asp:ListItem Value="2">(GMT +02:00) ������˹��</asp:ListItem>
	 				    		<asp:ListItem Value="2">(GMT +02:00) ������,����������</asp:ListItem>
	 				    		<asp:ListItem Value="2">(GMT +02:00) �ŵ�,��˹̹����</asp:ListItem>
	 				    		<asp:ListItem Value="2">(GMT +02:00) ��˹��</asp:ListItem>
	 				    		<asp:ListItem Value="2">(GMT +02:00) �ն�����,����,���,������,����,ά��Ŧ˹</asp:ListItem>
	 				    		<asp:ListItem Value="2">(GMT +02:00) ����</asp:ListItem>
	 				    		<asp:ListItem Value="2">(GMT +02:00) Ү·����</asp:ListItem>
	 				    		<asp:ListItem Value="3">(GMT +03:00) �͸��</asp:ListItem>
	 				    		<asp:ListItem Value="3">(GMT +03:00) ������,���ŵ�</asp:ListItem>
	 				    		<asp:ListItem Value="3">(GMT +03:00) Ī˹��</asp:ListItem>
	 				    		<asp:ListItem Value="3">(GMT +03:00) ʥ�˵ñ�</asp:ListItem>
	 				    		<asp:ListItem Value="3">(GMT +03:00) �����Ӹ���</asp:ListItem>
	 				    		<asp:ListItem Value="3">(GMT +03:00) ���ޱ�</asp:ListItem>
	 				    		<asp:ListItem Value="3.5">(GMT +03:30) �º���</asp:ListItem>
	 				    		<asp:ListItem Value="4">(GMT +04:00) ��������,��˹����</asp:ListItem>
	 				    		<asp:ListItem Value="4">(GMT +04:00) �Ϳ�</asp:ListItem>
	 				    		<asp:ListItem Value="4">(GMT +04:00) �ڱ���˹</asp:ListItem>
	 				    		<asp:ListItem Value="4">(GMT +04:00) ������</asp:ListItem>
	 				    		<asp:ListItem Value="4.5">(GMT +04:30) ������</asp:ListItem>
	 				    		<asp:ListItem Value="5">(GMT +05:00) Ҷ�����ձ�</asp:ListItem>
	 				    		<asp:ListItem Value="5">(GMT +05:00) ��˹����,������,��ʲ��</asp:ListItem>
	 				    		<asp:ListItem Value="5">(GMT +05:30) �����˹,�Ӷ�����,����,�µ���</asp:ListItem>
	 				    		<asp:ListItem Value="5.75">(GMT +05:45) �ӵ�����</asp:ListItem>
	 				    		<asp:ListItem Value="6">(GMT +06:00) ����ľͼ</asp:ListItem>
	 				    		<asp:ListItem Value="6">(GMT +06:00) ����������</asp:ListItem>
	 				    		<asp:ListItem Value="6">(GMT +06:00) ��˹����,�￨</asp:ListItem>
	 				    		<asp:ListItem Value="6">(GMT +06:00) ˹����ǻ���������</asp:ListItem>
	 				    		<asp:ListItem Value="6.5">(GMT +06:30) ����</asp:ListItem>
	 				    		<asp:ListItem Value="7">(GMT +07:00) ����˹ŵ�Ƕ�˹��</asp:ListItem>
	 				    		<asp:ListItem Value="7">(GMT +07:00) ����,����,�żӴ�</asp:ListItem>
	 				    		<asp:ListItem Value="8">(GMT +08:00) ����,����,����ر�������,��³ľ��</asp:ListItem>
	 				    		<asp:ListItem Value="8">(GMT +08:00) ��¡��,�¼���</asp:ListItem>
	 				    		<asp:ListItem Value="8">(GMT +08:00) ��˹</asp:ListItem>
	 				    		<asp:ListItem Value="8">(GMT +08:00) ̨��</asp:ListItem>
	 				    		<asp:ListItem Value="8">(GMT +08:00) ������Ŀ�,������ͼ</asp:ListItem>
	 				    		<asp:ListItem Value="9">(GMT +09:00) ����,����,����</asp:ListItem>
	 				    		<asp:ListItem Value="9">(GMT +09:00) ����</asp:ListItem>
	 				    		<asp:ListItem Value="9">(GMT +09:00) �ſ�Ŀ�</asp:ListItem>
	 				    		<asp:ListItem Value="9.5">(GMT +09:30) ��������</asp:ListItem>
	 				    		<asp:ListItem Value="9.5">(GMT +09:30) �����</asp:ListItem>
	 				    		<asp:ListItem Value="10">(GMT +10:00) ����˹��</asp:ListItem>
	 				    		<asp:ListItem Value="10">(GMT +10:00) ��������˹�п�</asp:ListItem>
	 				    		<asp:ListItem Value="10">(GMT +10:00) �ص�,Ī���ȱȸ�</asp:ListItem>
	 				    		<asp:ListItem Value="10">(GMT +10:00) ������</asp:ListItem>
	 				    		<asp:ListItem Value="10">(GMT +10:00) ������,ī����,Ϥ��</asp:ListItem>
	 				    		<asp:ListItem Value="11">(GMT +11:00) ��ӵ�,������Ⱥ��,�¿��������</asp:ListItem>
	 				    		<asp:ListItem Value="12">(GMT +12:00) �¿���,�����</asp:ListItem>
	 				    		<asp:ListItem Value="12">(GMT +12:00) 쳼�,����Ӱ뵺,���ܶ�Ⱥ��</asp:ListItem>
	 				    		<asp:ListItem Value="13">(GMT +13:00) Ŭ�Ⱒ�巨   </asp:ListItem>
                    </asp:DropDownList>
                    <br />
                    <asp:CompareValidator ID="ValcTimeZone" runat="server" ControlToValidate="DropTimezone"
                        Display="Dynamic" ErrorMessage="��ѡ����������ڵ�ʱ��" Operator="NotEqual"
                        SetFocusOnError="True" ValueToCompare="��ѡ��..."></asp:CompareValidator></td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>�����û��ı���ʱ�䣺</strong><br />
                    �û��л�ҳ����������վ���߹ر������������������������ʱ����ɾ�����û���������ԽС����վͳ�Ƶĵ�ǰʱ����������Խ׼ȷ��������Խ����վͳ�Ƶ���������Խ�ࡣ
                </td>
                <td>
                    <asp:TextBox ID="TxtOnlineTime" runat="server" Width="128px" MaxLength="9"></asp:TextBox>
                    ��
                    <asp:CompareValidator ID="ValcOnlineTime" runat="server" ControlToValidate="TxtOnlineTime"
                        Display="Dynamic" ErrorMessage="��������Ч�����֣�" Operator="DataTypeCheck" SetFocusOnError="True"
                        Type="Integer"></asp:CompareValidator></td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>�Զ�������߼����</strong><br />
                    �ͻ����������ÿ������ʱ����������ύһ��������Ϣ��ͬʱ������������Ϊ���ߣ�������ԽС����������Ҫ���������Խ�ࡣ</td>
                <td>
                    <asp:TextBox ID="TxtInterval" runat="server" Width="128px" MaxLength="9"></asp:TextBox>
                    ��
                    <asp:CompareValidator ID="ValcInterval" runat="server" ControlToValidate="TxtInterval"
                        Display="Dynamic" ErrorMessage="��������Ч�����֣�" Operator="DataTypeCheck" SetFocusOnError="True"
                        Type="Integer"></asp:CompareValidator></td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>�Զ�������߼��ѭ��������</strong><br />
                    ����Ϊ�˷�ֹ�û�����ҳ������ʱ�����κλ�����á��ͻ����������������ύ������Ϣ���������˴���������ֹͣ�ύ��
                </td>
                <td>
                    <asp:TextBox ID="TxtIntervalNum" runat="server" Width="128px" MaxLength="9"></asp:TextBox>
                    ��
                    <asp:CompareValidator ID="ValcIntervalNum" runat="server" ControlToValidate="TxtIntervalNum"
                        Display="Dynamic" ErrorMessage="��������Ч�����֣�" Operator="DataTypeCheck" SetFocusOnError="True"
                        Type="Integer"></asp:CompareValidator></td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>�������ʼ�¼����</strong><br />
                    ���������ϸ(������)��Ŀ����</td>
                <td>
                    <asp:TextBox ID="TxtVisitRecord" runat="server" Width="128px" MaxLength="9"></asp:TextBox>
                    ��
                    <asp:CompareValidator ID="ValcVisitRecord" runat="server" ControlToValidate="TxtVisitRecord"
                        Display="Dynamic" ErrorMessage="��������Ч�����֣�" Operator="DataTypeCheck" SetFocusOnError="True"
                        Type="Integer"></asp:CompareValidator></td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>��������IP��(�������20С��800������)�� </strong>
                    <br />
                    �������á���������ͳ�ơ�����ʱ��ϵͳ���Ա���������IP�ķ�ʽ����ֹˢ�£���ͬһ��IP���ʶ�λ�������վ���л�ҳ�棬��ֻ������������������������
                </td>
                <td>
                    <asp:TextBox ID="TxtKillRefresh" runat="server" Width="128px" MaxLength="9"></asp:TextBox>
                    ��
                    <asp:CompareValidator ID="ValcKillRefresh" runat="server" ControlToValidate="TxtKillRefresh"
                        Display="Dynamic" ErrorMessage="��������Ч�����֣�" Operator="DataTypeCheck" SetFocusOnError="True"
                        Type="Integer"></asp:CompareValidator></td>
            </tr>
        </tbody>
        <tbody id="Tabs1" style="display: none">
            <tr class="tdbg">
                <td class="tdbgleft" style="width: 30%;">
                    <strong>��ʼͳ�����ڣ�</strong></td>
                <td>
                    <pe:DatePicker ID="DpkStartDate" runat="server" Width="70px"></pe:DatePicker></td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>ʹ�ñ�ϵͳǰ�ķ�������</strong>
                </td>
                <td>
                    <asp:TextBox ID="TxtOldTotalNum" runat="server" Width="128px" MaxLength="9"></asp:TextBox>
                    �˴�
                    <asp:CompareValidator ID="ValcOldTotalNum" runat="server" ControlToValidate="TxtOldTotalNum"
                        Display="Dynamic" ErrorMessage="��������Ч�����֣�" Operator="DataTypeCheck" SetFocusOnError="True"
                        Type="Integer"></asp:CompareValidator></td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>ʹ�ñ�ϵͳǰ���������</strong>
                </td>
                <td>
                    <asp:TextBox ID="TxtOldTotalView" runat="server" Width="128px" MaxLength="9"></asp:TextBox>
                    �˴�
                    <asp:CompareValidator ID="ValcOldTotalView" runat="server" ControlToValidate="TxtOldTotalView"
                        Display="Dynamic" ErrorMessage="��������Ч�����֣�" Operator="DataTypeCheck" SetFocusOnError="True"
                        Type="Integer"></asp:CompareValidator></td>
            </tr>
        </tbody>
        <tbody id="Tabs2" style="display: none">
            <tr class="tdbg">
                <td style="width: 30%;" class="tdbgleft">
                    <strong>������Ŀ��</strong><br />
                    ͳ��̫�����Ŀ����������ٶȣ��ķ�̫����վ��Դ��һ��ʱ�䲻������Ĺ�����Ŀ���鲻Ҫ���ã�<br />
                    <span style="color: #ff0000">ǿ�ҽ��龡��ѡ���ٵĹ�����Ŀ�����һ���������ã�����<br />
                    </span>
                </td>
                <td>
                    <asp:CheckBoxList ID="ChklRegFields" runat="server" RepeatColumns="3" RepeatDirection="Horizontal"
                        Width="100%">
                        <asp:ListItem Value="IsCountOnline">���á���������ͳ�ơ�����</asp:ListItem>
                        <asp:ListItem Value="FIP">�ͻ���IP��ַ����</asp:ListItem>
                        <asp:ListItem Value="FAddress">�ͻ��˵�ַ����</asp:ListItem>
                        <asp:ListItem Value="FRefer">�ͻ�������ҳ�����</asp:ListItem>
                        <asp:ListItem Value="FTimezone">�ͻ���ʱ������</asp:ListItem>
                        <asp:ListItem Value="FWeburl">�ͻ���������վ����</asp:ListItem>
                        <asp:ListItem Value="FBrowser">�ͻ������������</asp:ListItem>
                        <asp:ListItem Value="FMozilla">�ͻ����ִ�����</asp:ListItem>
                        <asp:ListItem Value="FSystem">�ͻ��˲���ϵͳ����</asp:ListItem>
                        <asp:ListItem Value="FScreen">�ͻ�����Ļ��С����</asp:ListItem>
                        <asp:ListItem Value="FColor">�ͻ�����Ļɫ�ʷ���</asp:ListItem>
                        <asp:ListItem Value="FKeyword">�����ؼ��ʷ���</asp:ListItem>
                        <asp:ListItem Value="FVisit">���ʴ���ͳ�Ʒ���</asp:ListItem>
                        <asp:ListItem Value="FYesterDay">��������ͳ��</asp:ListItem>
                    </asp:CheckBoxList></td>
            </tr>
        </tbody>
    </table>
    <p style="text-align: center">
        <asp:Button ID="BtnSave" runat="server" Text="��������" Width="88px" OnClick="BtnSave_Click"
            OnClientClick="ReloadLeft()" />
        &nbsp;
        <asp:HiddenField ID="HdnSaveConfig" runat="server" Value="0" />
    </p>
</asp:Content>
