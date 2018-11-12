<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Controls.CompanyInfo" Codebehind="CompanyInfo.ascx.cs" %>
<table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
    <tr class='tdbg'>
        <td class='tdbgleft' style="width: 15%;text-align:right;">
            单位名称：</td>
        <td align='left' style="width: 30%">
            <asp:Label ID="LblCompanyName" runat="server"></asp:Label>
        </td>
        <td class='tdbgleft' style="width: 15%;text-align:right;">
            联系地址：</td>
        <td align='left' style="width: 40%">
            <asp:Label ID="LblAddress" runat="server"></asp:Label>
        </td>
    </tr>
    <tr class='tdbg'>
        <td class="tdbgleft" style="text-align:right">
            国家/地区：</td>
        <td align='left'>
            <asp:Label ID="LblCountry" runat="server"></asp:Label>
        </td>
        <td class="tdbgleft" style="text-align:right">
            省/市：</td>
        <td align='left'>
            <asp:Label ID="LblProvince" runat="server"></asp:Label>
        </td>
    </tr>
    <tr class='tdbg'>
        <td class="tdbgleft" style="text-align:right">
            市/县/区：</td>
        <td align='left'>
            <asp:Label ID="LblCity" runat="server"></asp:Label>
        </td>
        <td class="tdbgleft" style="text-align:right">
            邮政编码：</td>
        <td align='left'>
            <asp:Label ID="LblZipCode" runat="server"></asp:Label>
        </td>
    </tr>
    <tr class='tdbg'>
        <td class="tdbgleft" style="text-align:right">
            联系电话：</td>
        <td align='left'>
            <asp:Label ID="LblPhone" runat="server"></asp:Label>
        </td>
        <td class="tdbgleft" style="text-align:right">
            传真号码：</td>
        <td align='left'>
            <asp:Label ID="LblFax" runat="server"></asp:Label>
        </td>
    </tr>
    <tr class='tdbg'>
        <td class="tdbgleft" style="text-align:right">
            开户银行：</td>
        <td align='left'>
            <asp:Label ID="LblBankOfDeposit" runat="server"></asp:Label>
        </td>
        <td class="tdbgleft" style="text-align:right">
            银行帐号：</td>
        <td align='left'>
            <asp:Label ID="LblBankAccount" runat="server"></asp:Label>
        </td>
    </tr>
    <tr class='tdbg'>
        <td class="tdbgleft" style="text-align:right">
            税号：</td>
        <td align='left'>
            <asp:Label ID="LblTaxNum" runat="server"></asp:Label>
        </td>
        <td class="tdbgleft" style="text-align:right">
            网址：</td>
        <td align='left'>
            <asp:Label ID="LblHomepage" runat="server"></asp:Label>
        </td>
    </tr>
    <tr class='tdbg'>
        <td class="tdbgleft" style="text-align:right">
            行业地位：</td>
        <td align='left'>
            <asp:Label ID="LblStatusInField" runat="server"></asp:Label>
        </td>
        <td class="tdbgleft" style="text-align:right">
            公司规模：</td>
        <td align='left'>
            <asp:Label ID="LblCompanySize" runat="server"></asp:Label>
        </td>
    </tr>
    <tr class='tdbg'>
        <td class="tdbgleft" style="text-align:right">
            业务范围：</td>
        <td align='left'>
            <asp:Label ID="LblBusinessScope" runat="server"></asp:Label>
        </td>
        <td class="tdbgleft" style="text-align:right">
            年销售额：</td>
        <td align='left'>
            <asp:Label ID="LblAnnualSales" runat="server"></asp:Label>万元
        </td>
    </tr>
    <tr class='tdbg'>
        <td class="tdbgleft" style="text-align:right">
            经营状态：</td>
        <td align='left'>
            <asp:Label ID="LblManagementForms" runat="server"></asp:Label>
        </td>
        <td class="tdbgleft" style="text-align:right">
            注册资本：</td>
        <td align='left'>
            <asp:Label ID="LblRegisteredCapital" runat="server"></asp:Label>万元
        </td>
    </tr>
    <tr class='tdbg'>
        <td class="tdbgleft" style="text-align:right">
            公司照片：</td>
        <td align='left' colspan="3">
            <asp:Label ID="LblCompanyPic" runat="server"></asp:Label>
        </td>
    </tr>
    <tr class='tdbg'>
        <td class="tdbgleft" style="text-align:right">
            公司简介：</td>
        <td align='left' colspan="3">
            <asp:Label ID="LblCompanyIntro" runat="server"></asp:Label>
        </td>
    </tr>
</table>
