<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.AD.ADZones" Title="��ӹ���λ" ValidateRequest="false" Codebehind="ADZone.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table class="border" width="100%" border="0" cellpadding="2" cellspacing="1">
        <tr align="center">
            <td colspan="2" class="spacingtitle">
                <pe:AlternateLiteral ID="LblTitle" Text="��ӹ���λ" AlternateText="�޸Ĺ���λ" runat="Server" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" style="width: 30%">
                <strong>��λ���ƣ�</strong>
            </td>
            <td>
                <asp:TextBox ID="TxtZoneName" TextMode="SingleLine" MaxLength="100" runat="server"
                    EnableViewState="False" Width="180" />
                <pe:RequiredFieldValidator ID="ValrZoneName" runat="server" ControlToValidate="TxtZoneName"
                    ErrorMessage="��λ���Ʋ���Ϊ�գ�" SetFocusOnError="true" Display="Dynamic" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>����JS�ļ�����</strong>
            </td>
            <td>
                <asp:TextBox ID="TxtZoneJSName" TextMode="singleLine" MaxLength="100" runat="server"
                    EnableViewState="False" Width="180" />
                <pe:RequiredFieldValidator ID="ValrtZoneJSName" runat="server" ControlToValidate="TxtZoneJSName"
                    ErrorMessage="����JS�ļ�������Ϊ�գ�" SetFocusOnError="true" Display="Dynamic" />
                <asp:RegularExpressionValidator ID="ValeZoneJSName" ControlToValidate="TxtZoneJSName"
                    runat="server" ErrorMessage="JS�ļ�������ȷ����Ϊ�գ�" ValidationExpression="[0-9]{6}\/[0-9]{1,}\.js"
                    SetFocusOnError="true" Display="Dynamic" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>��λ������</strong>
            </td>
            <td>
                <asp:TextBox ID="TxtZoneIntro" TextMode="multiline" MaxLength="255" runat="server"
                    EnableViewState="False" Height="63px" Width="280px" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>��λ���ͣ�</strong><br />
                ѡ������ڴ˰�λ�Ĺ�����͡�
            </td>
            <td>
                <asp:RadioButtonList ID="RadlZoneType" runat="server" RepeatDirection="Horizontal"
                    RepeatLayout="Flow" EnableViewState="true" AutoPostBack="false" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>��λ���ã�</strong><br />
                �԰�λ����ϸ�����������á�
            </td>
            <td>
                <asp:RadioButtonList ID="RadlDefaultSetting" runat="server" RepeatDirection="Horizontal"
                    RepeatLayout="Flow" AutoPostBack="false" Height="30" />
                <div id="ZoneTypeSetting" runat="server">
                    <table id="ZoneTypeSetting1" runat="server" border="0" cellpadding="2" cellspacing="1"
                        style="display: none">
                        <tr>
                            <td>
                                <table border="0" cellpadding="2" cellspacing="1">
                                    <tr class="tdbg">
                                        <td colspan="2" align="center" style="height: 23px">
                                            �������ް�λ�������ã�</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table id="ZoneTypeSetting2" runat="server" border="0" cellpadding="0" cellspacing="0"
                        style="display: none">
                        <tr>
                            <td>
                                <table border="0" cellpadding="2" cellspacing="1">
                                    <tr class="tdbg">
                                        <td>
                                            ������ʽ��
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DropPopType" runat="server">
                                                <asp:ListItem Value="1">ǰ�ô���</asp:ListItem>
                                                <asp:ListItem Value="2">���ô���</asp:ListItem>
                                                <asp:ListItem Value="3">��ҳ�Ի���</asp:ListItem>
                                                <asp:ListItem Value="4">��Ͷ���</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            ����λ�ã�
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DropPopPosition" runat="server">
                                                <asp:ListItem Value="1" Text="����" Selected="true"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="����"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="����"></asp:ListItem>
                                                <asp:ListItem Value="4" Text="����"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr class="tdbg">
                                        <td>
                                            <span id="PopLeft">��</span>��
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtPopLeft" runat="server" Text="100" MaxLength="4" TextMode="SingleLine"
                                                Width="36px" />
                                        </td>
                                    </tr>
                                    <tr class="tdbg">
                                        <td>
                                            <span id="PopTop">��</span>��
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtPopTop" runat="server" Text="100" MaxLength="4" TextMode="singleLine"
                                                Width="36px" />
                                        </td>
                                    </tr>
                                    <tr class="tdbg">
                                        <td>
                                            ʱ������
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtPopCookieHour" Text="0" MaxLength="2" TextMode="singleLine" Width="36px"
                                                runat="server" />
                                            Сʱ ��ʱ�����ڲ��ظ���������Ϊ0ʱ���ǵ���
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table id="ZoneTypeSetting3" runat="server" border="0" cellpadding="0" cellspacing="0"
                        style="display: none">
                        <tr>
                            <td>
                                <table border="0" cellpadding="2" cellspacing="1">
                                    <tr>
                                        <td>
                                            ���λ�ã�
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DropMovePosition" runat="server">
                                                <asp:ListItem Value="1" Text="����" Selected="true"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="����"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="����"></asp:ListItem>
                                                <asp:ListItem Value="4" Text="����"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr class="tdbg">
                                        <td>
                                            <span id="MoveLeft">��</span>��
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtMoveLeft" MaxLength="4" Width="36px" Text="15" TextMode="singleLine"
                                                runat="server" />
                                        </td>
                                    </tr>
                                    <tr class="tdbg">
                                        <td>
                                            <span id="MoveTop">��</span>��
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtMoveTop" MaxLength="4" Width="36px" Text="200" TextMode="singleLine"
                                                runat="server" />
                                        </td>
                                    </tr>
                                    <tr class="tdbg">
                                        <td>
                                            �ƶ�ƽ���ȣ�
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtMoveDelay" MaxLength="7" Text="0.015" TextMode="singleLine" runat="server"
                                                Width="36px" />
                                            ��ȡֵ��0.001��1֮�䣩
                                        </td>
                                    </tr>
                                    <tr class="tdbg">
                                        <td>
                                            �Ƿ���ʾ�رձ�ǩ��
                                        </td>
                                        <td>
                                            <asp:RadioButtonList ID="RadlMoveShowCloseAD" runat="server" RepeatDirection="Horizontal"
                                                RepeatLayout="Flow">
                                                <asp:ListItem Value="true">��</asp:ListItem>
                                                <asp:ListItem Value="false" Selected="True">��</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr class="tdbg">
                                        <td>
                                            �رձ�ǩ����ɫ��
                                        </td>
                                        <td>
                                            <pe:ColorPicker ID="TxtMoveCloseFontColor" Text="#FFFFFF" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table id="ZoneTypeSetting4" runat="server" border="0" cellpadding="0" cellspacing="0"
                        style="display: none">
                        <tr>
                            <td>
                                <table border="0" cellpadding="2" cellspacing="1">
                                    <tr>
                                        <td>
                                            ���λ�ã�
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DropFixedPosition" runat="server">
                                                <asp:ListItem Value="1" Text="����" Selected="true"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="����"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="����"></asp:ListItem>
                                                <asp:ListItem Value="4" Text="����"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr class="tdbg">
                                        <td>
                                            <span id="FixedLeft">��</span>��
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtFixedLeft" MaxLength="4" Text="100" TextMode="singleLine" runat="server"
                                                Width="36px" />
                                        </td>
                                    </tr>
                                    <tr class="tdbg">
                                        <td>
                                            <span id="FixedTop">��</span>��
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtFixedTop" MaxLength="4" Text="100" TextMode="singleLine" Width="36px"
                                                runat="server" />
                                        </td>
                                    </tr>
                                    <tr class="tdbg">
                                        <td>
                                            �Ƿ���ʾ�رձ�ǩ��
                                        </td>
                                        <td>
                                            <asp:RadioButtonList ID="RadlFixedShowCloseAD" runat="server" RepeatDirection="Horizontal"
                                                RepeatLayout="Flow">
                                                <asp:ListItem Value="true">��</asp:ListItem>
                                                <asp:ListItem Value="false" Selected="True">��</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr class="tdbg">
                                        <td>
                                            �رձ�ǩ����ɫ��
                                        </td>
                                        <td>
                                            <pe:ColorPicker ID="TxtFixedCloseFontColor" Text="#FFFFFF" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table id="ZoneTypeSetting5" runat="server" border="0" cellpadding="0" cellspacing="0"
                        style="display: none">
                        <tr>
                            <td>
                                <table border="0" cellpadding="2" cellspacing="1">
                                    <tr class="tdbg">
                                        <td>
                                            Ư�����ͣ�
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DropFloatType" runat="server">
                                                <asp:ListItem Value="1">����Ư��</asp:ListItem>
                                                <asp:ListItem Value="2">����Ư��</asp:ListItem>
                                                <asp:ListItem Value="3">����Ư��</asp:ListItem>
                                                <asp:ListItem Value="4">����Ư��</asp:ListItem>
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            ��ʼλ�ã�
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DropFloatPosition" runat="server">
                                                <asp:ListItem Value="1" Text="����" Selected="true"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="����"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="����"></asp:ListItem>
                                                <asp:ListItem Value="4" Text="����"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr class="tdbg">
                                        <td>
                                            <span id="FloatLeft">��</span>��
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtFloatLeft" MaxLength="4" Text="100" TextMode="singleLine" Width="36px"
                                                runat="server" />
                                        </td>
                                    </tr>
                                    <tr class="tdbg">
                                        <td>
                                            <span id="FloatTop">��</span>��
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtFloatTop" MaxLength="4" Text="100" TextMode="singleLine" Width="36px"
                                                runat="server" />
                                        </td>
                                    </tr>
                                    <tr class="tdbg">
                                        <td>
                                            �Ƿ���ʾ�رձ�ǩ��
                                        </td>
                                        <td>
                                            <asp:RadioButtonList ID="RadlFloatShowCloseAD" runat="server" RepeatDirection="Horizontal"
                                                RepeatLayout="Flow">
                                                <asp:ListItem Value="true">��</asp:ListItem>
                                                <asp:ListItem Value="false" Selected="True">��</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr class="tdbg">
                                        <td>
                                            �رձ�ǩ����ɫ��
                                        </td>
                                        <td>
                                            <pe:ColorPicker ID="TxtFloatCloseFontColor" Text="#FFFFFF" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table id="ZoneTypeSetting6" runat="server" border="0" cellpadding="0" cellspacing="0"
                        style="display: none">
                        <tr>
                            <td>
                                <table border="0" cellpadding="2" cellspacing="1">
                                    <tr class="tdbg">
                                        <td align="center">
                                            �������ް�λ�������ã�</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table id="ZoneTypeSetting7" runat="server" border="0" cellpadding="0" cellspacing="0"
                        style="display: none">
                        <tr>
                            <td>
                                <table border="0" cellpadding="2" cellspacing="1">
                                    <tr class="tdbg">
                                        <td>
                                            ���ұ߾ࣺ
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtCoupletLeft" MaxLength="4" Width="36px" Text="15" TextMode="singleLine"
                                                runat="server" />
                                        </td>
                                    </tr>
                                    <tr class="tdbg">
                                        <td>
                                            �ϱ߾ࣺ
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtCoupletTop" MaxLength="4" Width="36px" Text="200" TextMode="singleLine"
                                                runat="server" />
                                        </td>
                                    </tr>
                                    <tr class="tdbg">
                                        <td>
                                            �ƶ�ƽ���ȣ�
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtCoupletDelay" MaxLength="7" Text="0.015" TextMode="singleLine"
                                                runat="server" Width="36px" />
                                            ��ȡֵ��0.001��1֮�䣩
                                        </td>
                                    </tr>
                                    <tr class="tdbg">
                                        <td>
                                            �Ƿ���ʾ�رձ�ǩ��
                                        </td>
                                        <td>
                                            <asp:RadioButtonList ID="RadlCoupletShowCloseAD" runat="server" RepeatDirection="Horizontal"
                                                RepeatLayout="Flow">
                                                <asp:ListItem Value="true">��</asp:ListItem>
                                                <asp:ListItem Value="false" Selected="True">��</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr class="tdbg">
                                        <td>
                                            �رձ�ǩ����ɫ��
                                        </td>
                                        <td>
                                            <pe:ColorPicker ID="TxtCoupletCloseFontColor" Text="#FFFFFF" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>��λ�ߴ磺</strong><br />
                IAB��������������ϻ��׼�ߴ硣<br />
                ��*�ŵ�Ϊ�����ӵı�׼���ߴ硣
            </td>
            <td>
                <table>
                    <tr>
                        <td>
                            <asp:DropDownList ID="DropAdZoneSize" runat="server" AutoPostBack="false" EnableViewState="False">
                                <asp:ListItem Value="468x60">IAB - 468 x 60 IMU (������)</asp:ListItem>
                                <asp:ListItem Value="234x60">IAB - 234 x 60 IMU (������)</asp:ListItem>
                                <asp:ListItem Value="88x31">IAB - 88 x 31 IMU (С��ť)</asp:ListItem>
                                <asp:ListItem Value="120x90">IAB - 120 x 90 IMU (��ťһ)</asp:ListItem>
                                <asp:ListItem Value="120x60">IAB - 120 x 60 IMU (��ť��)</asp:ListItem>
                                <asp:ListItem Value="728x90">IAB - 728 x 90 IMU (ͨ�����) *</asp:ListItem>
                                <asp:ListItem Value="120x240">IAB - 120 x 240 IMU (�������)</asp:ListItem>
                                <asp:ListItem Value="125x125">IAB - 125 x 125 IMU (���ΰ�ť)</asp:ListItem>
                                <asp:ListItem Value="180x150">IAB - 180 x 150 IMU (������) *</asp:ListItem>
                                <asp:ListItem Value="300x250">IAB - 300 x 250 IMU (�г�����) *</asp:ListItem>
                                <asp:ListItem Value="336x280">IAB - 336 x 280 IMU (�󳤷���)</asp:ListItem>
                                <asp:ListItem Value="240x400">IAB - 240 x 400 IMU (��������)</asp:ListItem>
                                <asp:ListItem Value="250x250">IAB - 250 x 250 IMU (�����ε���)</asp:ListItem>
                                <asp:ListItem Value="120x600">IAB - 120 x 600 IMU (Ħ���¥)</asp:ListItem>
                                <asp:ListItem Value="160x600">IAB - 160 x 600 IMU (��Ħ���¥) *</asp:ListItem>
                                <asp:ListItem Value="300x600">IAB - 300 x 600 IMU (��ҳ���) *</asp:ListItem>
                                <asp:ListItem Value="0x0">�Զ����С</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            ��ȣ�
                            <asp:TextBox ID="TxtZoneWidth" MaxLength="4" Width="36px" Text="468" TextMode="singleLine"
                                runat="server" />
                            <pe:RequiredFieldValidator ID="ValrTxtZoneWidth" runat="server" ControlToValidate="TxtZoneWidth"
                                SetFocusOnError="true" ErrorMessage="" Display="Dynamic" />
                            &nbsp;&nbsp;&nbsp;&nbsp;�߶ȣ�
                            <asp:TextBox ID="TxtZoneHeight" MaxLength="4" Width="36px" Text="60" TextMode="singleLine"
                                runat="server" />
                            <pe:RequiredFieldValidator ID="ValrTxtZoneHeight" runat="server" ControlToValidate="TxtZoneHeight"
                                SetFocusOnError="true" ErrorMessage="" Display="Dynamic" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>��ʾ��ʽ��</strong><br />
                ����λ���ж�����ʱ���մ��趨������ʾ�����ݹ���Ȩ�أ���</td>
            <td>
                <asp:RadioButtonList ID="RadlShowType" runat="server">
                    <asp:ListItem Value="1" Selected="true">��Ȩ�������ʾ��Ȩ��Խ����ʾ����Խ��</asp:ListItem>
                    <asp:ListItem Value="2">��Ȩ��������ʾ����ʾȨ��ֵ���Ĺ�档</asp:ListItem>
                    <asp:ListItem Value="3">��˳��ѭ����ʾ���˷�ʽ���Ծ��κ����Ч��</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>��λ״̬��</strong><br />
                ��Ϊ��İ�λ������ǰ̨��ʾ��</td>
            <td>
                <asp:CheckBox ID="ChkActive" Checked="true" runat="server" EnableViewState="False" />
                ���λ
            </td>
        </tr>
        <tr class="tdbgbottom">
            <td colspan="2">
                <asp:Button ID="EBtnAdZone" runat="server" Text="����" OnClick="EBtnAdZone_Click" />&nbsp;&nbsp;
                <input name="Cancel" type="button" onclick="Redirect('ADZoneManage.aspx')" class="inputbutton"
                    id="Cancel" value="ȡ��" style="cursor: pointer; cursor: hand;" />
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HdnAction" runat="server" />
    <asp:HiddenField ID="HdnZoneId" runat="server" />

    <script language="javascript" type="text/javascript">
  
    function $(value)
    {
        return document.getElementById(value);
    }
  
    function ShowZoneTypePanel()
    {
        
       
         if($('<%=RadlZoneType.ClientID %>_0').checked==false)
        {
            $('<%=RadlShowType.ClientID %>_2').disabled = true;
            
            $('<%=RadlShowType.ClientID %>_0').checked=true;//$('<%=RadlShowType.ClientID %>_2').checked;
        } else{
            $('<%=RadlShowType.ClientID %>_2').disabled=false;
            $('<%=RadlShowType.ClientID %>_0').checked=true;
           
        }
        
        Zone_DisableSize($('<%=RadlZoneType.ClientID %>_5').checked);
        
        var obj=document.getElementsByName('<%=RadlZoneType.UniqueID %>');
        for (var j=0;j<obj.length;j++)
        {
            var ot = eval($('<%=ZoneTypeSetting.ClientID %>' + (j + 1)))
            if($('<%=RadlZoneType.ClientID %>_'+j).checked && $('<%=RadlDefaultSetting.ClientID %>_1').checked)
            {
                ot.style.display = '';
                
            }else{ 
                ot.style.display = 'none';
            }
        } 
         
     }
     
     function Zone_DisableSize(value)
     {
        $('<%=TxtZoneWidth.ClientID %>').disabled=value;
        $('<%=TxtZoneHeight.ClientID %>').disabled=value;
        $('<%=DropAdZoneSize.ClientID %>').disabled=value;
     }
   
    function Zone_SelectSize(o)
    {
        size = o.options[o.selectedIndex].value;
        if (size != '0x0')
        {
            sarray = size.split('x');
            height = sarray.pop();
            width  = sarray.pop();
            $('<%=TxtZoneWidth.ClientID %>').value=width;
            $('<%=TxtZoneHeight.ClientID %>').value=height;
        }else{
            $('<%=TxtZoneHeight.ClientID %>').value=100;
            $('<%=TxtZoneWidth.ClientID %>').value=100;
        }
     }
    
    function ChangePositonShow(drop)
    {
        var text=drop.options[drop.options.selectedIndex].text;
        var name=drop.id.replace("ctl00_CphContent_Drop","");
        name=name.replace("Position","");
        $(name+"Left").innerHTML=text.substring(0,1);
        $(name+"Top").innerHTML=text.substring(1,2);
    }
     if($('<%=HdnAction.ClientID %>').value=="Modify")
     {
         if($('<%=RadlZoneType.ClientID %>_0').checked==false)
        {
            $('<%=RadlShowType.ClientID %>_2').disabled = true;
        } else{
            $('<%=RadlShowType.ClientID %>_2').disabled=false;
        }
     }
    </script>

</asp:Content>
