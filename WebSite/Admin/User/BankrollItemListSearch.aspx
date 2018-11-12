<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.User.BankrollItemListSearch" Title="�ʽ���ϸ��ѯ" Codebehind="BankrollItemListSearch.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td colspan="2" class="spacingtitle">
                <b>�ʽ���ϸ���Ӳ�ѯ</b>
            </td>
        </tr>
        <tr class="tdbg">
            <td style="width: 120px; text-align: right" class="tdbgleft">
                ID��Χ��</td>
            <td>
                ��ʼID
                <asp:TextBox ID="TxtBeginId" runat="server" Width="78px"></asp:TextBox>&nbsp;<asp:CompareValidator
                    ID="ValcStartID" runat="server" ControlToValidate="TxtBeginId" Display="Dynamic"
                    ErrorMessage="��������ȷID�ţ�" Operator="GreaterThan" SetFocusOnError="True" Type="Integer" ValueToCompare="0"></asp:CompareValidator>��ֹID<asp:TextBox
                    ID="TxtEndId" runat="server" Width="78px"></asp:TextBox>
                <asp:CompareValidator ID="ValcEndID" runat="server" ControlToValidate="TxtEndId"
                    Display="Dynamic" ErrorMessage="��������ȷID�ţ�" Operator="GreaterThan" SetFocusOnError="True"
                    Type="Integer" ValueToCompare="0"></asp:CompareValidator></td>
        </tr>
        <tr class="tdbg">
            <td style="width: 120px; text-align: right" class="tdbgleft">
                ���ڷ�Χ��</td>
            <td>
                ��ʼ����
                <pe:DatePicker ID="DpkBegin" runat="server" Width="70px"></pe:DatePicker>
                &nbsp; &nbsp; &nbsp;��������
                <pe:DatePicker ID="DpkEnd" runat="server" Width="70px"></pe:DatePicker>
                <asp:CompareValidator ID="ValcBegin" runat="server" ControlToValidate="DpkBegin"
                    Display="Dynamic" ErrorMessage="��ʼ���ڸ�ʽ�д���" Operator="DataTypeCheck" SetFocusOnError="True"
                    Type="Date"></asp:CompareValidator>
                <asp:CompareValidator ID="ValcEnd" runat="server" ControlToValidate="DpkEnd" Display="Dynamic"
                    ErrorMessage="�������ڸ�ʽ�д���" Operator="DataTypeCheck" SetFocusOnError="True" Type="Date"></asp:CompareValidator></td>
        </tr>
        <tr class="tdbg">
            <td style="width: 120px; text-align: right" class="tdbgleft">
                �ͻ����ƣ�</td>
            <td>
                <asp:TextBox ID="TxtClientName" runat="server"></asp:TextBox></td>
        </tr>
        <tr class="tdbg">
            <td style="width: 120px; text-align: right" class="tdbgleft">
                �û����ƣ�</td>
            <td>
                <asp:TextBox ID="TxtUserName" runat="server"></asp:TextBox></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" style="width: 120px; text-align: right">
                �������ƣ�</td>
            <td>
                <asp:TextBox ID="TxtBank" runat="server"></asp:TextBox></td>
        </tr>
        <tr class="tdbg" style="height: 40px; text-align: center">
            <td colspan="6">
                <asp:Button ID="BtnSearch" runat="server" Text="��ѯ" OnClick="BtnSearch_Click" />
                &nbsp;
                <asp:Button ID="BtnExportExcel" runat="server" Text="������EXCEL" OnClick="BtnExportExcel_Click" /></td>
        </tr>
    </table>
</asp:Content>
