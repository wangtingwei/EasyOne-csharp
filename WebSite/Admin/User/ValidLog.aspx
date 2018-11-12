<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.User.ValidLog"
    MasterPageFile="~/Admin/MasterPage.master" Title="会员有效期明细管理" Codebehind="ValidLog.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <pe:ValidLog ID="SystemValidLog" runat="server" ></pe:ValidLog>
    <br />
    如果有效期明细记录太多，影响了系统性能，可以删除一定时间段前的记录以加快速度。但可能会带来会员在查看以前收过费的信息时重复收费（这样会引发众多消费纠纷问题），无法通过有效期明细记录来真实分析会员的消费习惯等问题。
    <br />
    <br />
    <table width="100%" cellpadding="5" cellspacing="0" class="border">
        <tr class="tdbg">
            <td align="right" style="width: 10%;">
                时间范围：</td>
            <td align="left" style="width: 55%;">
                <asp:RadioButtonList ID="RadlDatepartType" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="0" Selected="True">10天前</asp:ListItem>
                    <asp:ListItem Value="1">1个月前</asp:ListItem>
                    <asp:ListItem Value="2">2个月前</asp:ListItem>
                    <asp:ListItem Value="3">3个月前</asp:ListItem>
                    <asp:ListItem Value="4">6个月前</asp:ListItem>
                    <asp:ListItem Value="5">1年前</asp:ListItem>
                </asp:RadioButtonList></td>
            <td align="left">
                <asp:Button ID="BtnDelete" runat="server" OnClientClick="return confirm('确实要删除有关记录吗？一旦删除这些记录，会出现会员查看原来已经付过费的收费信息时重复收费等问题。请慎重！')"
                    Text="删除" OnClick="BtnDelete_Click" CausesValidation="False" />
            </td>
        </tr>
    </table>
</asp:Content>
