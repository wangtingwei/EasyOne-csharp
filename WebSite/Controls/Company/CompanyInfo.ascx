<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Controls.CompanyInfo" Codebehind="CompanyInfo.ascx.cs" %>
<table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
    <tr class='tdbg'>
        <td class='tdbgleft' style="width: 15%;text-align:right;">
            ��λ���ƣ�</td>
        <td align='left' style="width: 30%">
            <asp:Label ID="LblCompanyName" runat="server"></asp:Label>
        </td>
        <td class='tdbgleft' style="width: 15%;text-align:right;">
            ��ϵ��ַ��</td>
        <td align='left' style="width: 40%">
            <asp:Label ID="LblAddress" runat="server"></asp:Label>
        </td>
    </tr>
    <tr class='tdbg'>
        <td class="tdbgleft" style="text-align:right">
            ����/������</td>
        <td align='left'>
            <asp:Label ID="LblCountry" runat="server"></asp:Label>
        </td>
        <td class="tdbgleft" style="text-align:right">
            ʡ/�У�</td>
        <td align='left'>
            <asp:Label ID="LblProvince" runat="server"></asp:Label>
        </td>
    </tr>
    <tr class='tdbg'>
        <td class="tdbgleft" style="text-align:right">
            ��/��/����</td>
        <td align='left'>
            <asp:Label ID="LblCity" runat="server"></asp:Label>
        </td>
        <td class="tdbgleft" style="text-align:right">
            �������룺</td>
        <td align='left'>
            <asp:Label ID="LblZipCode" runat="server"></asp:Label>
        </td>
    </tr>
    <tr class='tdbg'>
        <td class="tdbgleft" style="text-align:right">
            ��ϵ�绰��</td>
        <td align='left'>
            <asp:Label ID="LblPhone" runat="server"></asp:Label>
        </td>
        <td class="tdbgleft" style="text-align:right">
            ������룺</td>
        <td align='left'>
            <asp:Label ID="LblFax" runat="server"></asp:Label>
        </td>
    </tr>
    <tr class='tdbg'>
        <td class="tdbgleft" style="text-align:right">
            �������У�</td>
        <td align='left'>
            <asp:Label ID="LblBankOfDeposit" runat="server"></asp:Label>
        </td>
        <td class="tdbgleft" style="text-align:right">
            �����ʺţ�</td>
        <td align='left'>
            <asp:Label ID="LblBankAccount" runat="server"></asp:Label>
        </td>
    </tr>
    <tr class='tdbg'>
        <td class="tdbgleft" style="text-align:right">
            ˰�ţ�</td>
        <td align='left'>
            <asp:Label ID="LblTaxNum" runat="server"></asp:Label>
        </td>
        <td class="tdbgleft" style="text-align:right">
            ��ַ��</td>
        <td align='left'>
            <asp:Label ID="LblHomepage" runat="server"></asp:Label>
        </td>
    </tr>
    <tr class='tdbg'>
        <td class="tdbgleft" style="text-align:right">
            ��ҵ��λ��</td>
        <td align='left'>
            <asp:Label ID="LblStatusInField" runat="server"></asp:Label>
        </td>
        <td class="tdbgleft" style="text-align:right">
            ��˾��ģ��</td>
        <td align='left'>
            <asp:Label ID="LblCompanySize" runat="server"></asp:Label>
        </td>
    </tr>
    <tr class='tdbg'>
        <td class="tdbgleft" style="text-align:right">
            ҵ��Χ��</td>
        <td align='left'>
            <asp:Label ID="LblBusinessScope" runat="server"></asp:Label>
        </td>
        <td class="tdbgleft" style="text-align:right">
            �����۶</td>
        <td align='left'>
            <asp:Label ID="LblAnnualSales" runat="server"></asp:Label>��Ԫ
        </td>
    </tr>
    <tr class='tdbg'>
        <td class="tdbgleft" style="text-align:right">
            ��Ӫ״̬��</td>
        <td align='left'>
            <asp:Label ID="LblManagementForms" runat="server"></asp:Label>
        </td>
        <td class="tdbgleft" style="text-align:right">
            ע���ʱ���</td>
        <td align='left'>
            <asp:Label ID="LblRegisteredCapital" runat="server"></asp:Label>��Ԫ
        </td>
    </tr>
    <tr class='tdbg'>
        <td class="tdbgleft" style="text-align:right">
            ��˾��Ƭ��</td>
        <td align='left' colspan="3">
            <asp:Label ID="LblCompanyPic" runat="server"></asp:Label>
        </td>
    </tr>
    <tr class='tdbg'>
        <td class="tdbgleft" style="text-align:right">
            ��˾��飺</td>
        <td align='left' colspan="3">
            <asp:Label ID="LblCompanyIntro" runat="server"></asp:Label>
        </td>
    </tr>
</table>
