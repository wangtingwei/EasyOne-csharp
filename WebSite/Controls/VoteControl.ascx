<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Controls.VoteControl" Codebehind="VoteControl.ascx.cs" %>
<table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="border">
    <tr class="tdbg">
        <td class="tdbgleft">
            <strong>����ͶƱ��</strong></td>
        <td>
            <asp:CheckBox ID="ChkIsAlive" runat="server" />
        </td>
    </tr>
    <tr class="tdbg">
        <td class="tdbgleft">
            <strong>ͶƱ���⣺</strong></td>
        <td>
            <asp:TextBox ID="TxtVoteTitle" runat="server" TextMode="multiline" Height="49px"
                Width="308px"></asp:TextBox>
        </td>
    </tr>
    <tr class="tdbg">
        <td class="tdbgleft">
            <strong>ѡ��1��</strong></td>
        <td>
            <asp:TextBox ID="TxtItem1" runat="server"></asp:TextBox>&nbsp;Ʊ����
            <asp:TextBox ID="TxtVoteNumber1" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr class="tdbg">
        <td class="tdbgleft">
            <strong>ѡ��2��</strong></td>
        <td>
            <asp:TextBox ID="TxtItem2" runat="server"></asp:TextBox>&nbsp;Ʊ����
            <asp:TextBox ID="TxtVoteNumber2" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr class="tdbg">
        <td class="tdbgleft">
            <strong>ѡ��3��</strong></td>
        <td>
            <asp:TextBox ID="TxtItem3" runat="server"></asp:TextBox>&nbsp;Ʊ����
            <asp:TextBox ID="TxtVoteNumber3" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr class="tdbg">
        <td class="tdbgleft">
            <strong>ѡ��4��</strong></td>
        <td>
            <asp:TextBox ID="TxtItem4" runat="server"></asp:TextBox>&nbsp;Ʊ����
            <asp:TextBox ID="TxtVoteNumber4" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr class="tdbg">
        <td class="tdbgleft">
            <strong>ѡ��5��</strong></td>
        <td>
            <asp:TextBox ID="TxtItem5" runat="server"></asp:TextBox>&nbsp;Ʊ����
            <asp:TextBox ID="TxtVoteNumber5" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr class="tdbg">
        <td class="tdbgleft">
            <strong>ѡ��6��</strong></td>
        <td>
            <asp:TextBox ID="TxtItem6" runat="server"></asp:TextBox>&nbsp;Ʊ����
            <asp:TextBox ID="TxtVoteNumber6" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr class="tdbg">
        <td class="tdbgleft">
            <strong>ѡ��7��</strong></td>
        <td>
            <asp:TextBox ID="TxtItem7" runat="server"></asp:TextBox>&nbsp;Ʊ����
            <asp:TextBox ID="TxtVoteNumber7" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr class="tdbg">
        <td class="tdbgleft">
            <strong>ѡ��8��</strong></td>
        <td>
            <asp:TextBox ID="TxtItem8" runat="server"></asp:TextBox>&nbsp;Ʊ����
            <asp:TextBox ID="TxtVoteNumber8" runat="server"></asp:TextBox>
        </td>
    </tr>
     <tr class="tdbg">
        <td class="tdbgleft">
            <strong>ѡ�����ͣ�</strong></td>
        <td>
            <asp:DropDownList ID="DropItemType" runat="server">
            <asp:ListItem Value="0" Text="��ѡ��" Selected="true"></asp:ListItem>
            <asp:ListItem Value="1" Text="��ѡ��"></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr class="tdbg" id="item">
        <td class="tdbgleft">
            <strong>ͶƱ��ʼ��ʱ�䣺</strong></td>
        <td>
            <pe:DatePicker ID="DpkStartTime" DateFormat="yyyy-MM-dd HH:mm:ss" runat="server"></pe:DatePicker>
        </td>
    </tr>
    <tr class="tdbg">
        <td class="tdbgleft">
            <strong>ͶƱ������ʱ�䣺</strong></td>
        <td>
            <pe:DatePicker ID="DpkEndTime" DateFormat="yyyy-MM-dd HH:mm:ss" runat="server"></pe:DatePicker>
        </td>
    </tr>
</table>
