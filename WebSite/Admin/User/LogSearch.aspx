<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPage.master"
    Inherits="EasyOne.WebSite.Admin.User.LogSearch" Codebehind="LogSearch.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td colspan="6" class="spacingtitle">
                <b>
                    <asp:Label ID="LblLogTitle" runat="server">明细复杂查询</asp:Label></b>
            </td>
        </tr>
        <tr class="tdbg">
            <td style="width: 120px; text-align: right" class="tdbgleft">
                ID范围：</td>
            <td>
                起始ID
                <asp:TextBox ID="TxtBeginId" runat="server"  Columns="8" MaxLength="8"></asp:TextBox>&nbsp;终止ID<asp:TextBox
                    ID="TxtEndId" runat="server"  Columns="8" MaxLength="8"></asp:TextBox>
                <asp:Label ID="LblNote" runat="server" Text=""></asp:Label>           
                <asp:RegularExpressionValidator ID="ValgBeginId" runat="server" ControlToValidate="TxtBeginId"
                    ErrorMessage="只能输入正整数" ValidationExpression="^([0-9])(\d{0,})(\d{0,})$" Display="Dynamic" />    
                <asp:RegularExpressionValidator ID="ValgEndIf" runat="server" ControlToValidate="TxtEndId"
                    ErrorMessage="只能输入正整数" ValidationExpression="^([0-9])(\d{0,})(\d{0,})$" Display="Dynamic" />
            </td>
        </tr>
        <tr class="tdbg">
            <td style="width: 120px; text-align: right" class="tdbgleft">
                日期范围：</td>
            <td>
                起始日期
                <pe:DatePicker ID="DpkBeginDate" runat="server"></pe:DatePicker><pe:DateValidator
                    ID="Vdate" ControlToValidate="DpkBeginDate" Display="Dynamic" SetFocusOnError="true"
                    runat="server"></pe:DateValidator>
                &nbsp;结束日期
                <pe:DatePicker ID="DpkEndDate" runat="server"></pe:DatePicker><pe:DateValidator ID="Vdate2"
                    ControlToValidate="DpkEndDate" Display="Dynamic" SetFocusOnError="true" runat="server"></pe:DateValidator>
            </td>
        </tr>
        <tr class="tdbg">
            <td style="width: 120px; text-align: right" class="tdbgleft">
                用户名称：</td>
            <td>
                <asp:TextBox ID="TxtUserName" runat="server"></asp:TextBox>
                <asp:RegularExpressionValidator ID="ValeUserName" runat="server" ControlToValidate="TxtUserName"
                    ErrorMessage="不能包含特殊字符  如@，#，$，%，^，&，*，(，)，'，?，{，}，[，]，;，:等" ValidationExpression="^[^@#$%^&*()'?{}\[\];:]*$"
                    Display="Dynamic"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr class="tdbg">
            <td style="width: 120px; text-align: right" class="tdbgleft">
                指定时间：</td>
            <td>
                <pe:DatePicker ID="DpkLogTime" runat="server"></pe:DatePicker><pe:DateValidator ID="Vdate3"
                    ControlToValidate="DpkLogTime" Display="Dynamic" SetFocusOnError="true" runat="server"></pe:DateValidator>
            </td>
        </tr>
        <tr class="tdbg">
            <td style="width: 120px; text-align: right" class="tdbgleft">
                操作员：</td>
            <td>
                <asp:TextBox ID="TxtInputer" runat="server"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TxtInputer"
                    ErrorMessage="不能包含特殊字符  如@，#，$，%，^，&，*，(，)，'，?，{，}，[，]，;，:等" ValidationExpression="^[^@#$%^&*()'?{}\[\];:]*$"
                    Display="Dynamic"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr class="tdbg">
            <td style="width: 120px; text-align: right" class="tdbgleft">
                IP地址：</td>
            <td>
                <asp:TextBox ID="TxtIP" runat="server"></asp:TextBox></td>
        </tr>
        <tr class="tdbg" style="height: 40px; text-align: center">
            <td colspan="6">
                <asp:Button ID="BtnSearch" runat="server" Text="查询" OnClick="BtnSearch_Click" />
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>
