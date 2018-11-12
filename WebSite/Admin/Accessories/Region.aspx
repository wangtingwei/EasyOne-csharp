<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Accessories.Regions" Title="�����������" Codebehind="Region.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td colspan="2" class="spacingtitle">
                <b>
                    <pe:AlternateLiteral ID="AltrTitle" Text="�����������" AlternateText="�޸���������" runat="Server" />
                </b>
            </td>
        </tr>
        <tr class="tdbg">
            <td align="right" class="tdbgleft" style="width: 40%">
                <strong>�������ң�</strong></td>
            <td class="tdbg">
                <asp:TextBox ID="TxtCountry" runat="server" Width="200" Text="�л����񹲺͹�"></asp:TextBox>
                <pe:RequiredFieldValidator ID="ValrCountry" runat="server" ControlToValidate="TxtCountry"
                    ErrorMessage="RequiredFieldValidator" Display="Dynamic">�������Ҳ���Ϊ�գ�</pe:RequiredFieldValidator></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right">
                <strong>����ʡ�ݣ�</strong></td>
            <td class="tdbg">
                <asp:TextBox ID="TxtProvince" runat="server" Width="200"></asp:TextBox>
                <pe:RequiredFieldValidator ID="ValrProvince" runat="server" ControlToValidate="TxtProvince"
                    ErrorMessage="RequiredFieldValidator" Display="Dynamic">����ʡ�ݲ���Ϊ�գ�</pe:RequiredFieldValidator></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right">
                <strong>�������У�</strong></td>
            <td class="tdbg">
                <asp:TextBox ID="TxtCity" runat="server" Width="200"></asp:TextBox></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right">
                <strong>����������</strong></td>
            <td class="tdbg">
                <asp:TextBox ID="TxtArea" runat="server" Width="200"></asp:TextBox>&nbsp;</td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right">
                <strong>�������룺</strong></td>
            <td class="tdbg">
                <asp:TextBox ID="TxtPostCode" runat="server" Width="200"></asp:TextBox>&nbsp;<pe:RequiredFieldValidator
                    ID="ValrPostCode" runat="server" ControlToValidate="TxtPostCode" ErrorMessage="RequiredFieldValidator"
                    Display="Dynamic">�������벻��Ϊ�գ�</pe:RequiredFieldValidator>
                <pe:ZipCodeValidator ID="Vzip" runat="server" ControlToValidate="TxtPostCode" Display="Dynamic"
                    ErrorMessage="��������Ч���������룡" SetFocusOnError="True"></pe:ZipCodeValidator></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right">
                <strong>�������ţ�</strong>
            </td>
            <td class="tdbg" style="height: 27px">
                <asp:TextBox ID="TxtAreaCode" runat="server" Width="200"></asp:TextBox>&nbsp;<pe:RequiredFieldValidator
                    ID="ValrAreaCode" runat="server" ControlToValidate="TxtAreaCode" ErrorMessage="RequiredFieldValidator"
                    Display="Dynamic">���Ų���Ϊ�գ�</pe:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="ValeAreaCode" runat="server" ControlToValidate="TxtAreaCode"
                    Display="Dynamic" ErrorMessage="��������Ч�����ţ�" ValidationExpression="\d{3,5}"></asp:RegularExpressionValidator></td>
        </tr>
        <tr class="tdbgbottom">
            <td colspan="2">
                <asp:Button ID="BtnSave" runat="server" OnClick="BtnSubmit_Click" Text="ȷ��" />
                &nbsp;&nbsp;
                <input name="Cancel" type="button" class="inputbutton" id="Cancel" value="ȡ��" onclick="Redirect('RegionManage.aspx')" />
            </td>
        </tr>
    </table>
</asp:Content>
