<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.User.ValidLog"
    MasterPageFile="~/Admin/MasterPage.master" Title="��Ա��Ч����ϸ����" Codebehind="ValidLog.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <pe:ValidLog ID="SystemValidLog" runat="server" ></pe:ValidLog>
    <br />
    �����Ч����ϸ��¼̫�࣬Ӱ����ϵͳ���ܣ�����ɾ��һ��ʱ���ǰ�ļ�¼�Լӿ��ٶȡ������ܻ������Ա�ڲ鿴��ǰ�չ��ѵ���Ϣʱ�ظ��շѣ������������ڶ����Ѿ������⣩���޷�ͨ����Ч����ϸ��¼����ʵ������Ա������ϰ�ߵ����⡣
    <br />
    <br />
    <table width="100%" cellpadding="5" cellspacing="0" class="border">
        <tr class="tdbg">
            <td align="right" style="width: 10%;">
                ʱ�䷶Χ��</td>
            <td align="left" style="width: 55%;">
                <asp:RadioButtonList ID="RadlDatepartType" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="0" Selected="True">10��ǰ</asp:ListItem>
                    <asp:ListItem Value="1">1����ǰ</asp:ListItem>
                    <asp:ListItem Value="2">2����ǰ</asp:ListItem>
                    <asp:ListItem Value="3">3����ǰ</asp:ListItem>
                    <asp:ListItem Value="4">6����ǰ</asp:ListItem>
                    <asp:ListItem Value="5">1��ǰ</asp:ListItem>
                </asp:RadioButtonList></td>
            <td align="left">
                <asp:Button ID="BtnDelete" runat="server" OnClientClick="return confirm('ȷʵҪɾ���йؼ�¼��һ��ɾ����Щ��¼������ֻ�Ա�鿴ԭ���Ѿ������ѵ��շ���Ϣʱ�ظ��շѵ����⡣�����أ�')"
                    Text="ɾ��" OnClick="BtnDelete_Click" CausesValidation="False" />
            </td>
        </tr>
    </table>
</asp:Content>
