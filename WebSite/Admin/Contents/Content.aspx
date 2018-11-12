<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Contents.Content" Title="������ӡ��޸�" ValidateRequest="false"
    EnableEventValidation="false" Codebehind="Content.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <table style="width: 100%; margin: 0 auto;" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
            </td>
            <td align="right">
                <pe:ExtendedLabel ID="LblNodeName" HtmlEncode="false" runat="server" Text=""></pe:ExtendedLabel>
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <asp:ScriptManager ID="SmContent" runat="server" EnablePartialRendering="true">
        <Services>
            <asp:ServiceReference Path="~/WebServices/CategoryService.asmx" />
        </Services>
    </asp:ScriptManager>

    <script language="JavaScript" type="text/JavaScript">
   var tID=0;
   var arrTabTitle = new Array("TabTitle0","TabTitle1","<%= TabTitle2.ClientID %>","<%= TabTitle3.ClientID %>","<%=TabTitle4.ClientID %>");
   var arrTrs0 = new Array(<%= arrTrs0 %>);
   var arrTrs1 = new Array(<%= arrTrs1 %>);
   var arrTrs2 = new Array("TabsCharge"<%= m_TbodyChargeId %>);
   var arrTrs3 = new Array("TabsSign");
   var arrTrs4 = new Array("TabsVote");
   var arrTab = new Array(arrTrs0,arrTrs1,arrTrs2,arrTrs3,arrTrs4);
   
   function ShowTabs(ID)
   {
       if(ID!=tID)
       {
           document.getElementById(arrTabTitle[tID].toString()).className = "tabtitle";
           document.getElementById(arrTabTitle[ID].toString()).className = "titlemouseover";
           
           for (var i = 0; i < arrTab.length; i++) 
           {
                var tab = arrTab[i];
                if(i==ID)
                {
                    for (var j = 0; j < tab.length; j++) 
                    {
                       document.getElementById(tab[j].toString()).style.display = "";
                    }
                }
                else
                {
                    for (var j = 0; j < tab.length; j++) 
                    {
                       document.getElementById(tab[j].toString()).style.display = "none";
                    }
                }
           }
           
           tID=ID;
        }
    }
    
    function SelectUser()
    {
        var strUrl = '../User/UserList.aspx?TypeSelect=UserList&OpenerText='+ '<%=TxtSigninUser.ClientID%>' +'&Default='+ document.getElementById(<%="'" +TxtSigninUser.ClientID + "'" %>).value;
        arr= window.open(strUrl,'newWin','modal=yes,width=520,height=400,resizable=yes,scrollbars=yes');
    }
   
    function ClearUser()
    {
        document.getElementById(<%="'" +TxtSigninUser.ClientID + "'" %>).value="";
    }
    </script>

    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr align="center">
            <td colspan="6" class="spacingtitle">
                <pe:AlternateLiteral ID="LblTitle" Text="�������" AlternateText="�޸�����" runat="Server" />
            </td>
        </tr>
        <tr align="center">
            <td id="TabTitle0" class="titlemouseover" onclick="ShowTabs(0)">
                ������Ϣ
            </td>
            <td id="TabTitle1" class="tabtitle" onclick="ShowTabs(1)">
                ����ѡ��
            </td>
            <td id="TabTitle2" class="tabtitle" runat="server" onclick="ShowTabs(2)">
                Ȩ���շ�����
            </td>
            <td id="TabTitle3" class="tabtitle" runat="server" onclick="ShowTabs(3)">
                ǩ��ѡ��
            </td>
            <td id="TabTitle4" class="tabtitle" runat="server" onclick="ShowTabs(4)">
                ͶƱ
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="border">
        <asp:Repeater ID="RepModel" runat="server" OnItemDataBound="RepModel_OnItemDataBound">
            <ItemTemplate>
                <pe:FieldControl ID="Field" runat="server" EnableNull='<%# (bool)Eval("EnableNull") %>'
                    FieldAlias='<%# Eval("FieldAlias")%>' Tips='<%# Eval("Tips") %>' FieldName='<%#Eval("FieldName")%>'
                    ControlType='<%# Eval("FieldType") %>' FieldLevel='<%# Eval("FieldLevel") %>'
                    Description='<%# Eval("Description")%>' Settings='<%# ((EasyOne.Model.CommonModel.FieldInfo)Container.DataItem).Settings %>'
                    Value='<%# Eval("DefaultValue") %>'>
                </pe:FieldControl>
            </ItemTemplate>
        </asp:Repeater>
        <tbody id="TabsCharge" style="display: none">
            <tr class="tdbg">
                <td style="width: 150px;" align="right" class="tdbgleft">
                    <strong>�Ķ�Ȩ�ޣ�&nbsp;</strong>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadlInfoPurview" runat="server">
                        <asp:ListItem Value="0" Selected="True">�̳���ĿȨ�ޣ���������ĿΪ��֤��Ŀʱ������ѡ����</asp:ListItem>
                        <asp:ListItem Value="1">���л�Ա���뵥����ĳЩ���½����Ķ�Ȩ�����ã�����ѡ����</asp:ListItem>
                        <asp:ListItem Value="2">ָ����Ա�飨�뵥����ĳЩ���½����Ķ�Ȩ�����ã�����ѡ����</asp:ListItem>
                    </asp:RadioButtonList>
                    <table>
                        <tr>
                            <td width="20">
                            </td>
                            <td>
                                <pe:ExtendedCheckBoxList ID="EChklUserGroupList" RepeatColumns="5" runat="server">
                                </pe:ExtendedCheckBoxList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </tbody>
        <tbody id="TbodyCharge" runat="server" style="display: none">
            <tr class="tdbg">
                <td style="width: 150px;" align="right" class="tdbgleft">
                    <strong>����<pe:ShowPointName ID="ShowPointName" runat="server" />����&nbsp;</strong>
                </td>
                <td style="height: 17px">
                    <asp:TextBox ID="TxtInfoPoint" runat="server" Columns="5" MaxLength="4"></asp:TextBox>&nbsp;
                    <span style="color: #0000FF">���<pe:ShowPointName ID="ShowPointName1" runat="server" />������0������Ȩ�޵Ļ�Ա�Ķ�������ʱ��������Ӧ<pe:ShowPointName
                        ID="ShowPointName2" runat="server" />������Ϊ9999ʱ���⣩���οͽ��޷��Ķ�������</span>
                </td>
            </tr>
            <tr class="tdbg">
                <td style="width: 150px;" align="right" class="tdbgleft">
                    <strong>�ظ��շѣ�&nbsp;</strong>
                </td>
                <td>
                    <pe:ShowChargeType ID="ShowChargeType" runat="server" />
                </td>
            </tr>
            <tr class="tdbg">
                <td style="width: 150px;" align="right" class="tdbgleft">
                    <strong>�ֳɱ�����&nbsp;</strong>
                </td>
                <td>
                    <asp:TextBox ID="TxtDividePercent" runat="server" Columns="5" MaxLength="3"></asp:TextBox>%
                    &nbsp;<span style="color: #0000FF">�����������0���򽫰����������Ķ�����ȡ��<pe:ShowPointName ID="ShowPointName3"
                        runat="server" />��֧����¼����</span>
                </td>
            </tr>
        </tbody>
        <tbody id="TabsSign" style="display: none">
            <tr class="tdbg">
                <td class="tdbgleft" align="right" style="width: 150px;">
                    <strong>ǩ���û���&nbsp;</strong>
                </td>
                <td class="tdbg" align="left">
                    <asp:TextBox ID="TxtSigninUser" Columns="72" Rows="5" runat="server" TextMode="MultiLine"
                        Height="75"></asp:TextBox><br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type="button" class="inputbutton"
                        value="ѡ���û�" onclick="SelectUser()" />&nbsp;&nbsp;&nbsp;&nbsp;<input type="button"
                            class="inputbutton" value="����û�" onclick="ClearUser()" />
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft" align="right" style="width: 150px;">
                    <strong>�ĵ����ͣ�&nbsp;</strong>
                </td>
                <td class="tdbg" align="left">
                    <asp:DropDownList ID="DrpSigninType" runat="server">
                        <asp:ListItem Text="�����ĵ�" Value="EnableSignInPublic"></asp:ListItem>
                        <asp:ListItem Text="ר���ĵ�" Value="EnableSignInPrivate"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft" align="right" style="width: 150px;">
                    <strong>ǩ�����ȼ���&nbsp;</strong><br />
                </td>
                <td class="tdbg" align="left">
                    <asp:TextBox ID="TxtPriority" runat="server" Columns="8"></asp:TextBox><pe:NumberValidator
                        ID="Valnum" runat="server" ControlToValidate="TxtPriority" ErrorMessage="����д����"
                        SetFocusOnError="true" Display="Dynamic"></pe:NumberValidator><asp:RangeValidator
                            ID="RangeValTxtPriority" runat="server" Type="Integer" MaximumValue="2147483646"
                            MinimumValue="0" ControlToValidate="TxtPriority" Display="Dynamic" SetFocusOnError="true"
                            ErrorMessage="����д0-2147483646��Χ������"></asp:RangeValidator>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft" align="right" style="width: 150px;">
                    <strong>ǩ�ս�ֹ���ڣ�&nbsp;</strong><br />
                </td>
                <td class="tdbg" align="left">
                    <pe:DatePicker ID="DpkEndTime" IsLongDate="true" runat="server"></pe:DatePicker>
                </td>
            </tr>
        </tbody>
        <tbody id="TabsVote" style="display: none">
            <tr class="tdbg">
                <td valign="top">
                    <pe:VoteControl ID="Vote" runat="server"></pe:VoteControl>
                </td>
            </tr>
        </tbody>
        <tr class="tdbgbottom">
            <td colspan="2">
                <pe:ExtendedButton ID="EBtnSubmit" IsChecked="false" IsShowTabs="true" CustomValProcessFunction="ValProcessFunction"
                    Text="������ӵ���Ŀ" OnClick="EBtnSubmit_Click" runat="server" />
                &nbsp;
                <pe:ExtendedButton ID="EBtnNewAddItem" IsChecked="false" IsShowTabs="true" CustomValProcessFunction="ValProcessFunction"
                    Text="���Ϊ����Ŀ" runat="server" OnClick="EBtnNewAddItem_Click" Visible="false" />
                &nbsp;
                <asp:Button ID="BtnBack" runat="server" Text="������" OnClick="BtnBack_Click" UseSubmitBehavior="False"
                    CausesValidation="False" />
            </td>
        </tr>
    </table>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
        ShowSummary="False" />

    <script type="text/javascript">
        function ValProcessFunction()
        {
            for (i = 0; i < Page_Validators.length; i++) 
            {
                val = Page_Validators[i];
                if (val.isvalid == false) 
                {
                    var id = val.id;
                    var controltovalidate = document.getElementById(val.controltovalidate);
                    var tempobj = controltovalidate;
                    var tabIndex;
                    while (tempobj)
                    {
                        if(tempobj.id != "")
                        {
                            if(tempobj.nodeName == "TR" || tempobj.nodeName == "TBODY")
                            {
                                var tabIndex = -1;
                                for (var i = 0; i < arrTab.length; i++) 
                                {
                                     var tab = arrTab[i];
                                     for (var j = 0; j < tab.length; j++) 
                                     {
                                          if(tempobj.id == tab[j].toString())
                                          {
                                             tabIndex = i;
                                             break;
                                          }
                                     }
                                     if(tabIndex != -1)
                                     {
                                         break;
                                     }
                                }
                                ShowTabs(tabIndex);
                                break;
                            }
                        }
                        tempobj = tempobj.parentNode;
                    }
                    if (typeof(controltovalidate.focus) != "undefined" && controltovalidate.focus != null)
                    {
                        try
                        {
                            controltovalidate.focus();
                        }
                        catch(err) 
                        {
                        }
                    }
                    break;
                }
            }
        }
    </script>

</asp:Content>
