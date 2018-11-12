<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    EnableEventValidation="false" Inherits="EasyOne.WebSite.Admin.Contents.CreateHtmlContent"
    ValidateRequest="false" Title="��������" Codebehind="CreateHtmlContent.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td class="spacingtitle" colspan="2" align="center">
                ��������ҳ
            </td>
        </tr>
        <tr>
            <td rowspan="8" class="tdbg" align="left" valign="top">
                <table>
                    <tr>
                        <td align="right">
                            �Ƿ�ͬʱ��������Ŀ�µ���Ϣ��</td>
                        <td>
                            <asp:RadioButtonList ID="RdlIsCreateChild" RepeatDirection="horizontal" RepeatLayout="flow"
                                runat="server">
                                <asp:ListItem Selected="true" Text="��" Value="true"></asp:ListItem>
                                <asp:ListItem Text="��" Value="false"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        
        </tr>
        <tr>
            <td align="right">
                ��ѡ��������Ŀ��<br />
                <span style="color: Blue">����δѡ����ָ��Ϊ������Ŀ��</span>
                </td><td>
                <asp:ListBox ID="LstNodes" runat="server" Height="275px" Width="200px" SelectionMode="Multiple"
                    DataTextField="NodeName" DataValueField="NodeId" ToolTip="��ס��Ctrl����Shift�������Զ�ѡ����ס��Ctrl����ȡ��ѡ��">
                </asp:ListBox>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                
            </td>
        </tr>
    </table>
    </td>
    <td class="tdbg">
        ��������
        <asp:TextBox ID="TxtTopNew" runat="server" Width="58px" ValidationGroup="TopNew" />����Ŀ&nbsp;&nbsp;
        <asp:Button ID="EBtnTopNew" Text="��ʼ���� >> " OnClick="EBtnTopNew_Click" runat="server"
            ValidationGroup="TopNew" />
        <pe:RequiredFieldValidator ID="ValrTopNew" ControlToValidate="TxtTopNew" ValidationGroup="TopNew"
            runat="server" ErrorMessage="�������²���Ϊ��" ShowRequiredText="false"></pe:RequiredFieldValidator>
    </td>
    </tr>
    <tr>
        <td class="tdbg">
            ���ɸ���ʱ���
            <pe:DatePicker ID="DpkBeginDate" runat="server" Width="80px" ValidationGroup="Date"></pe:DatePicker>
            ��
            <pe:DatePicker ID="DpkEndDate" runat="server" Width="80px" ValidationGroup="Date"></pe:DatePicker>
            ����Ŀ
            <asp:Button ID="EBtnDate" Text="��ʼ���� >> " OnClick="EBtnDate_Click" runat="server"
                ValidationGroup="Date" />
            <pe:RequiredFieldValidator ID="ValrBeginDate" ControlToValidate="DpkBeginDate" ValidationGroup="Date"
                Display="Dynamic" runat="server" ErrorMessage="��ʼʱ�䲻��Ϊ��" ShowRequiredText="false" />
            <pe:RequiredFieldValidator ID="ValrEndDate" ControlToValidate="DpkEndDate" ValidationGroup="Date"
                Display="Dynamic" runat="server" ErrorMessage="����ʱ�䲻��Ϊ��" ShowRequiredText="false" />
            <asp:CompareValidator ID="CompareValidator1" Operator="GreaterThan" ValidationGroup="Date"
                Type="Date" ControlToValidate="DpkEndDate" ControlToCompare="DpkBeginDate" Display="Dynamic"
                runat="server" ErrorMessage="��ʼʱ�䲻��С�ڽ���ʱ�䣡" />
        </td>
    </tr>
    <tr>
        <td class="tdbg">
            ����ID�Ŵ�
            <asp:TextBox ID="TxtBeginId" runat="server" Width="59px" ValidationGroup="BeginId" />
            ��
            <asp:TextBox ID="TxtEndId" runat="server" Width="60px" ValidationGroup="BeginId" />
            ����Ŀ
            <asp:Button ID="EBtnBoundId" Text="��ʼ���� >> " OnClick="EBtnBoundId_Click" runat="server"
                ValidationGroup="BeginId" />
            <pe:RequiredFieldValidator ID="ValrBeginId" ControlToValidate="TxtBeginId" ValidationGroup="BeginId"
                Display="Dynamic" runat="server" ErrorMessage="��ʼID����Ϊ��" ShowRequiredText="false"></pe:RequiredFieldValidator>
            <pe:RequiredFieldValidator ID="ValrEndId" ControlToValidate="TxtEndId" ValidationGroup="BeginId"
                Display="Dynamic" runat="server" ErrorMessage="����ID����Ϊ��" ShowRequiredText="false"></pe:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator2" Operator="GreaterThanEqual" Type="Integer"
                ControlToValidate="TxtEndId" ControlToCompare="TxtBeginId" runat="server" ErrorMessage="��ʼID����С�ڽ���ID��"></asp:CompareValidator>
        </td>
    </tr>
    <tr>
        <td class="tdbg">
            ����ָ��ID����Ŀ�����ID�����ö��Ÿ�����
            <asp:TextBox ID="TxtAppointId" runat="server" ValidationGroup="AppointId" />
            <asp:Button ID="EBtnAppointId" Text="��ʼ���� >> " OnClick="EBtnAppointId_Click" runat="server"
                ValidationGroup="AppointId" />
            <pe:RequiredFieldValidator ID="ValrAppointId" ControlToValidate="TxtAppointId" ValidationGroup="AppointId"
                Display="Dynamic" runat="server" ErrorMessage="ָ��ID����Ϊ��" ShowRequiredText="false"></pe:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="tdbg">
            ��������δ���ɵ���Ŀ
            <asp:Button ID="EBtnNotCreate" Text="��ʼ���� >> " OnClick="EBtnNotCreate_Click" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="tdbg">
            ����������Ŀ
            <asp:Button ID="EBtnAll" Text="��ʼ���� >> " OnClick="EBtnAll_Click" runat="server" />
        </td>
    </tr>
    </table>
    <br />
    <span style="color: Blue"><strong>ע�⣺</strong></span>���ѡ������Ŀ����ֻ����ѡ����Ŀ�µ����ݣ������ѡ��ֱ�����ɵģ�������ȫվ�����ݡ�
    <%--    <script type="text/javascript" language="javascript">
    function StartCreate()
    {
        document.getElementById("create").height=100;
    }
    </script>--%>
</asp:Content>
