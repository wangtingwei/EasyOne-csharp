<%@ Control Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Controls.Crm.PersonalInformation" Codebehind="PersonalInformation.ascx.cs" %>
        <tr class="tdbg">
            <td style="width: 15%" align="right" class="tdbgleft">
                �������ڣ�</td>
            <td style="width: 38%">
                <pe:DatePicker ID="DpkBirthday" runat="server"></pe:DatePicker><pe:DateValidator
                    ID="Vdate" ControlToValidate="DpkBirthday" Display="Dynamic" SetFocusOnError="true"
                    runat="server"></pe:DateValidator>
            </td>
            <td style="width: 15%" align="right" class="tdbgleft">
                ֤�����룺</td>
            <td style="width: 38%">
                <asp:TextBox ID="TxtIDCard" runat="server"></asp:TextBox></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right">
                ���᣺</td>
            <td>
                <asp:TextBox ID="TxtNativePlace" runat="server"></asp:TextBox></td>
            <td class="tdbgleft" align="right">
                ���壺</td>
            <td>
                <asp:TextBox ID="TxtNation" runat="server"></asp:TextBox></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right">
                �Ա�</td>
            <td>
                <asp:RadioButtonList ID="RadlSex" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True" Value="0">����</asp:ListItem>
                    <asp:ListItem Value="1">��</asp:ListItem>
                    <asp:ListItem Value="2">Ů</asp:ListItem>
                </asp:RadioButtonList></td>
            <td class="tdbgleft" align="right">
                ����״����</td>
            <td>
                <asp:RadioButtonList ID="RadlMarriage" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True" Value="0">����</asp:ListItem>
                    <asp:ListItem Value="1">δ��</asp:ListItem>
                    <asp:ListItem Value="2">�ѻ�</asp:ListItem>
                    <asp:ListItem Value="3">����</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right">
                ѧ����</td>
            <td>
                <asp:DropDownList ID="DropEducation" DataTextField="DataTextField" DataValueField="DataValueField"
                    runat="server">
                </asp:DropDownList>
            </td>
            <td class="tdbgleft" align="right">
                ��ҵѧУ��</td>
            <td>
                <asp:TextBox ID="TxtGraduateFrom" runat="server"></asp:TextBox></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right">
                ����ã�</td>
            <td>
                <asp:TextBox ID="TxtInterestsOfLife" runat="server"></asp:TextBox></td>
            <td class="tdbgleft" align="right">
                �Ļ����ã�</td>
            <td>
                <asp:TextBox ID="TxtInterestsOfCulture" runat="server"></asp:TextBox></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right">
                �������а��ã�</td>
            <td>
                <asp:TextBox ID="TxtInterestsOfAmusement" runat="server"></asp:TextBox></td>
            <td class="tdbgleft" align="right">
                �������ã�</td>
            <td>
                <asp:TextBox ID="TxtInterestsOfSport" runat="server"></asp:TextBox></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right">
                �������ã�</td>
            <td>
                <asp:TextBox ID="TxtInterestsOfOther" runat="server"></asp:TextBox></td>
            <td class="tdbgleft" align="right">
                �� �� �룺</td>
            <td>
                <asp:DropDownList ID="DropIncome" DataTextField="DataTextField" DataValueField="DataValueField"
                    runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right">
                ��ͥ�����</td>
            <td colspan="3">
                <asp:TextBox ID="TxtFamily" runat="server" TextMode="MultiLine" Height="74px" Width="400"></asp:TextBox></td>
        </tr>