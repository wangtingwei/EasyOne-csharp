<%@ Control Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Controls.CategoryMonthControl" Codebehind="CategoryMonthControl.ascx.cs" %>
<table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
    <tr class="tdbg">
        <td colspan="2" align="center" class="spacingtitle">
            ����Ŀ/�·�ͳ��</td>
    </tr>
    <tr class="tdbg">
        <td class="tdbgleft">
            <strong>ѡ����Ŀ��</strong></td>
        <td>
            <asp:DropDownList ID="DrpCategory" runat="server">
            </asp:DropDownList>
        </td>
    </tr>
    <%--<tr class="tdbg">
        <td class="tdbgleft">
            <strong>ָ������ߣ�</strong></td>
        <td>
            <asp:DropDownList ID="DrpInputer" runat="server">
            </asp:DropDownList>
        </td>
    </tr>--%>
    <tr class="tdbg">
        <td class="tdbgleft">
            <strong>ͳ�����ڷ�Χ��</strong></td>
        <td>
            ��ʼ���ڣ�<pe:DatePicker ID="DpkStartDate" runat="server"></pe:DatePicker>
            �������ڣ�<pe:DatePicker ID="DpkEndDate" runat="server"></pe:DatePicker>
             <asp:CompareValidator ID="CompareValidator1" ControlToCompare="DpkEndDate" ControlToValidate="DpkStartDate" Display="Dynamic" Operator="LessThanEqual" Type="Date"  runat="server" ErrorMessage="��ʼ���ڱ���С�ڽ�������"></asp:CompareValidator>
        </td>
    </tr>
    <tr align="center" class="tdbg">
        <td colspan="2">
            <asp:Button ID="BtnSubmit" runat="server" Text="��ʼͳ��" OnClick="BtnSubmit_Click" />
        </td>
    </tr>
</table>
<br />
<div style="text-align: center;">
    <span id="SpanCount" runat="server"></span>
</div>