<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Accessories.MailListExport" Title="�ʼ��б���������" Codebehind="MailListExport.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table style="text-align: center" border="0" cellpadding="2" cellspacing="1" class="border"
        width="100%">
        <tr class="title">
            <td class="title" colspan="2" style="text-align: center;">
                <b>�ʼ��б��������������ݿ�</b></td>
        </tr>
        <tr class="tdbg">
            <td style="text-align: right; height: 80px; width: 24%">
                �����ʼ��б����ݿ⣺</td>
            <td style="height: 80px; width: 76%">
                &nbsp;&nbsp; <span style="color: #0000ff">����</span> &nbsp;<asp:DropDownList ID="DropGroup1"
                    runat="server" Width="90px">
                    <asp:ListItem Selected="True" Value="0">ȫ����Ա</asp:ListItem>
                </asp:DropDownList>
                <span style="color: #0000ff">��</span>
                <asp:TextBox ID="TxtExportToAccess" runat="server" MaxLength="200" Width="180px">Maillist.mdb</asp:TextBox>
                &nbsp;<asp:Button ID="BtnSaveAccess" runat="server" OnClick="BtnSaveAccess_Click" Text="��ʼ" ValidationGroup="ExportToAccess" /><br />
                <pe:RequiredFieldValidator ID="ValcExportToAccess" runat="server" ControlToValidate="TxtExportToAccess"
                    Display="Dynamic" ErrorMessage="������Ҫ���������ݿ��ļ�����" ValidationGroup="ExportToAccess" RequiredText=""></pe:RequiredFieldValidator></td>
        </tr>
    </table>
    <br />
    <table style="text-align: center; width: 100%;" border="0" cellpadding="2" cellspacing="1"
        class="border">
        <tr class="title">
            <td style="text-align: center;" class="title" colspan="2">
                <b>�ʼ��б������������ı�</b></td>
        </tr>
        <tr class="tdbg">
            <td style="text-align: right; height: 80px; width: 24%">
                �����ʼ��б��ı���</td>
            <td style="height: 80px; width: 76%">
                &nbsp;&nbsp; <span style="color: #0000ff">����</span> &nbsp;<asp:DropDownList ID="DropGroup2"
                    runat="server" Width="90px">
                    <asp:ListItem Selected="True" Value="0">ȫ����Ա</asp:ListItem>
                </asp:DropDownList>
                <span style="color: #0000ff">��</span>
                <asp:TextBox ID="TxtExportToText" runat="server" MaxLength="200" Width="180px">maillist.txt</asp:TextBox>&nbsp;
                <asp:Button ID="BtnSaveText" runat="server" OnClick="BtnSaveText_Click" Text="��ʼ" ValidationGroup="ExportToText" /><br />
                <pe:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtExportToText"
                    Display="Dynamic" ErrorMessage="������Ҫ���������ݿ��ļ�����" ValidationGroup="ExportToText" RequiredText=""></pe:RequiredFieldValidator></td>
        </tr>
    </table>
</asp:Content>
