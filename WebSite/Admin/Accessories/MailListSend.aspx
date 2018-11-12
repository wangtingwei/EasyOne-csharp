<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Accessories.MailListSend" Title="�����ʼ��б�" ValidateRequest="false" Codebehind="MailListSend.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">

    <script type="text/javascript">
        function SelectUser()
        {            
            var newwin = window.open('../User/UserNameList.aspx?TypeSelect=UserList&Default='+ escape(document.getElementById(<%="'" +TxtUserName.ClientID + "'" %>).value),'UserNameList','width=670,height=400,resizable=0,scrollbars=yes');
            newwin.focus();
        }
        
        function DoPostBack(value)
        {
            document.getElementById('<%=TxtUserName.ClientID%>').value=value;
        }
    </script>

    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr>
            <td align="center" colspan="2" class="spacingtitle">
                <b>�ʼ�����</b></td>
        </tr>
        <tr class="tdbg">
            <td width="15%" align="right" class="tdbgleft">
                <strong>�ռ���ѡ��</strong></td>
            <td style="text-align: left">
                <table>
                    <tr>
                        <td>
                            <asp:RadioButton ID="RadUserType0" runat="server" GroupName="UserType" Text="���л�Ա"
                                Checked="True" /></td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            <asp:RadioButton ID="RadUserType1" runat="server" GroupName="UserType" Text="ָ����Ա��" /></td>
                        <td>
                            <asp:CheckBoxList ID="ChklUserGroupList" runat="server" RepeatColumns="5" RepeatDirection="Horizontal"
                                Width="100%">
                            </asp:CheckBoxList></td>
                    </tr>
                    <tr>
                        <td valign="top">
                            <asp:RadioButton ID="RadUserType2" runat="server" GroupName="UserType" Text="ָ���û���" /></td>
                        <td>
                            <asp:TextBox ID="TxtUserName" runat="server" Width="260px"></asp:TextBox>
                            <span style="color: blue"><=��</span><a href="#" onclick="SelectUser();"><span style="color: green">��Ա�б�</span></a><span
                                style="color: blue">��</span>����û���������<span style="color: blue">Ӣ�ĵĶ���</span>�ָ�</td>
                    </tr>
                    <tr>
                        <td valign="top">
                            <asp:RadioButton ID="RadUserType3" runat="server" GroupName="UserType" Text="ָ����ԱEmail" /></td>
                        <td>
                            <asp:TextBox ID="TxtEmails" runat="server" Width="260px"></asp:TextBox>
                            ���Email������<span style="color: blue">Ӣ�ĵĶ���</span>�ָ�</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="tdbg">
            <td align="right" class="tdbgleft">
                <strong>�ʼ����⣺</strong></td>
            <td>
                <asp:TextBox ID="TxtSubject" runat="server" Width="390px"></asp:TextBox>
                <pe:RequiredFieldValidator ID="ValrSubject" runat="server" ControlToValidate="TxtSubject"
                    Display="Dynamic" ErrorMessage="�ʼ����ⲻ��Ϊ�գ�"></pe:RequiredFieldValidator></td>
        </tr>
        <tr class="tdbg">
            <td align="right" class="tdbgleft">
                <strong>�ʼ����ݣ�</strong></td>
            <td>
                <pe:PEeditor Height="300px" Width="600px" ID="EditorContent" runat="server" ToolbarSet="Simple">
                </pe:PEeditor>
                <pe:FckEditorValidator ID="FckEditorValidator1" runat="server" 
                    ControlToValidate="EditorContent" Display="Dynamic" ErrorMessage="�ʼ����ݲ���Ϊ�գ�"></pe:FckEditorValidator>
                </td>
        </tr>
        <tr class="tdbg">
            <td align="right" class="tdbgleft">
                <strong>�����ˣ�</strong></td>
            <td>
                <asp:TextBox ID="TxtSenderName" runat="server" Width="350px"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg">
            <td align="right" class="tdbgleft">
                <strong>�ظ�Email��</strong></td>
            <td>
                <asp:TextBox ID="TxtSenderEmail" runat="server" Width="350px"></asp:TextBox>
                <asp:RegularExpressionValidator ID="ValeSenderEmail" runat="server" ControlToValidate="TxtSenderEmail"
                    Display="Dynamic" ErrorMessage="�ظ�Email������Ч��Email��ַ��" 
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator></td>
        </tr>
        <tr class="tdbg">
            <td align="right" class="tdbgleft">
                <strong>�ʼ����ȼ���</strong></td>
            <td>
                <asp:RadioButtonList ID="RadlPriority" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="2">��</asp:ListItem>
                    <asp:ListItem Selected="True" Value="0">��ͨ</asp:ListItem>
                    <asp:ListItem Value="1">��</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
        <tr class="tdbgbottom">
            <td colspan="2">
                <asp:Button ID="BtnSend" runat="server" Text="����" OnClick="BtnSend_Click" />
                &nbsp;&nbsp; &nbsp;
                <input id="Reset1" type="reset" value=" ��� " /></td>
        </tr>
    </table>
    </asp:Content>
