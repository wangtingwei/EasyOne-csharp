<%@ Control Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Controls.Crm.PersonalInformation" Codebehind="PersonalInformation.ascx.cs" %>
        <tr class="tdbg">
            <td style="width: 15%" align="right" class="tdbgleft">
                出生日期：</td>
            <td style="width: 38%">
                <pe:DatePicker ID="DpkBirthday" runat="server"></pe:DatePicker><pe:DateValidator
                    ID="Vdate" ControlToValidate="DpkBirthday" Display="Dynamic" SetFocusOnError="true"
                    runat="server"></pe:DateValidator>
            </td>
            <td style="width: 15%" align="right" class="tdbgleft">
                证件号码：</td>
            <td style="width: 38%">
                <asp:TextBox ID="TxtIDCard" runat="server"></asp:TextBox></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right">
                籍贯：</td>
            <td>
                <asp:TextBox ID="TxtNativePlace" runat="server"></asp:TextBox></td>
            <td class="tdbgleft" align="right">
                民族：</td>
            <td>
                <asp:TextBox ID="TxtNation" runat="server"></asp:TextBox></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right">
                性别：</td>
            <td>
                <asp:RadioButtonList ID="RadlSex" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True" Value="0">保密</asp:ListItem>
                    <asp:ListItem Value="1">男</asp:ListItem>
                    <asp:ListItem Value="2">女</asp:ListItem>
                </asp:RadioButtonList></td>
            <td class="tdbgleft" align="right">
                婚姻状况：</td>
            <td>
                <asp:RadioButtonList ID="RadlMarriage" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True" Value="0">保密</asp:ListItem>
                    <asp:ListItem Value="1">未婚</asp:ListItem>
                    <asp:ListItem Value="2">已婚</asp:ListItem>
                    <asp:ListItem Value="3">离异</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right">
                学历：</td>
            <td>
                <asp:DropDownList ID="DropEducation" DataTextField="DataTextField" DataValueField="DataValueField"
                    runat="server">
                </asp:DropDownList>
            </td>
            <td class="tdbgleft" align="right">
                毕业学校：</td>
            <td>
                <asp:TextBox ID="TxtGraduateFrom" runat="server"></asp:TextBox></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right">
                生活爱好：</td>
            <td>
                <asp:TextBox ID="TxtInterestsOfLife" runat="server"></asp:TextBox></td>
            <td class="tdbgleft" align="right">
                文化爱好：</td>
            <td>
                <asp:TextBox ID="TxtInterestsOfCulture" runat="server"></asp:TextBox></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right">
                娱乐休闲爱好：</td>
            <td>
                <asp:TextBox ID="TxtInterestsOfAmusement" runat="server"></asp:TextBox></td>
            <td class="tdbgleft" align="right">
                体育爱好：</td>
            <td>
                <asp:TextBox ID="TxtInterestsOfSport" runat="server"></asp:TextBox></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right">
                其他爱好：</td>
            <td>
                <asp:TextBox ID="TxtInterestsOfOther" runat="server"></asp:TextBox></td>
            <td class="tdbgleft" align="right">
                月 收 入：</td>
            <td>
                <asp:DropDownList ID="DropIncome" DataTextField="DataTextField" DataValueField="DataValueField"
                    runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right">
                家庭情况：</td>
            <td colspan="3">
                <asp:TextBox ID="TxtFamily" runat="server" TextMode="MultiLine" Height="74px" Width="400"></asp:TextBox></td>
        </tr>