<%@ Control Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Controls.CategoryMonthControl" Codebehind="CategoryMonthControl.ascx.cs" %>
<table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
    <tr class="tdbg">
        <td colspan="2" align="center" class="spacingtitle">
            按栏目/月份统计</td>
    </tr>
    <tr class="tdbg">
        <td class="tdbgleft">
            <strong>选择栏目：</strong></td>
        <td>
            <asp:DropDownList ID="DrpCategory" runat="server">
            </asp:DropDownList>
        </td>
    </tr>
    <%--<tr class="tdbg">
        <td class="tdbgleft">
            <strong>指定审核者：</strong></td>
        <td>
            <asp:DropDownList ID="DrpInputer" runat="server">
            </asp:DropDownList>
        </td>
    </tr>--%>
    <tr class="tdbg">
        <td class="tdbgleft">
            <strong>统计日期范围：</strong></td>
        <td>
            起始日期：<pe:DatePicker ID="DpkStartDate" runat="server"></pe:DatePicker>
            结束日期：<pe:DatePicker ID="DpkEndDate" runat="server"></pe:DatePicker>
             <asp:CompareValidator ID="CompareValidator1" ControlToCompare="DpkEndDate" ControlToValidate="DpkStartDate" Display="Dynamic" Operator="LessThanEqual" Type="Date"  runat="server" ErrorMessage="起始日期必须小于结束日期"></asp:CompareValidator>
        </td>
    </tr>
    <tr align="center" class="tdbg">
        <td colspan="2">
            <asp:Button ID="BtnSubmit" runat="server" Text="开始统计" OnClick="BtnSubmit_Click" />
        </td>
    </tr>
</table>
<br />
<div style="text-align: center;">
    <span id="SpanCount" runat="server"></span>
</div>