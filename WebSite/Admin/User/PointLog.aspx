<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.User.PointLog"
    MasterPageFile="~/Admin/MasterPage.master" Title="��ȯ����" Codebehind="PointLog.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <pe:PointLog ID="SystemPointLog" runat="server"></pe:PointLog>
    <br />
    ���<pe:ShowPointName ID="ShowPointName" runat="server" />��ϸ��¼̫�࣬Ӱ����ϵͳ���ܣ�����ɾ��һ��ʱ���ǰ�ļ�¼�Լӿ��ٶȡ������ܻ������Ա�ڲ鿴��ǰ�չ��ѵ���Ϣʱ�ظ��շѣ������������ڶ����Ѿ������⣩���޷�ͨ��<pe:ShowPointName ID="ShowPointName1" runat="server" />��ϸ��¼����ʵ������Ա������ϰ�ߵ����⡣
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
                    Text="ɾ��" OnClick="BtnDelete_Click" CausesValidation="False" /></td>
        </tr>
    </table>
</asp:Content>
