<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Accessories.CompareFilesOnline" Codebehind="CompareFilesOnline.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
   
    <div id="notes" runat="server">
        <table width="100%" border="0" cellspacing="1" cellpadding="2" class="border">
            <tr>
                <td align="center" class="spacingtitle">
                    <strong>���߱Ƚ���վ�ļ�</strong></td>
            </tr>
            <tr class="tdbg">
                <td>
                    <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;����ͨ���˹��ܱȽ���վ�еĿ������ļ��͹ٷ���������Ӧ�汾��ԭʼ�������ļ�֮��Ĳ��죬�Է������վ���ļ����м��͸��¡�<br />
                    �������������ʱ����ʹ�ñ����ܽ��бȽϣ�<div style="color: Green">
                        <br />
                        &nbsp;&nbsp;&nbsp;&nbsp;1�����ٷ������ļ����߷�������ʱ��<br />
                        &nbsp;&nbsp;&nbsp;&nbsp;2��������վ��Ŀ������ļ�����ɾ��������޸�ʱ��<br />
                    </div>
                    <p>
                        &nbsp;&nbsp;&nbsp;&nbsp;�����վ�ļ��ܶ࣬���������ٶȱȽ�����ִ�б�������Ҫ�ķ��൱����ʱ�䣬���ڷ�������ʱִ�б�������</p>
                    &nbsp;&nbsp;
                    <div style="text-align: center">
                        <asp:Button ID="BtnCompareTogether" runat="server" OnClick="BtnCompareTogether_Click"
                            Text="��ʼ�Ƚ�" /><br />
                        &nbsp;</div>
                </td>
            </tr>
        </table>
    </div>
    <asp:ScriptManager ID="ScriptManager2" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <br />
            <asp:Panel ID="PnlCompareFiles" runat="server" Width="100%" Visible="False">
                <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
                    <tr class="tdbg">
                        <td style="width: 80px; height: 30px; text-align: right;">
                            <strong>����壺</strong></td>
                        <td style="height: 30px">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                <tr class="tdbg">
                                    <td>
                                        <b>'= '</b>----���ߴ�Сʱ����ȫ��ͬ</td>
                                    <td>
                                        <span style="color: Red"><b>'��'</b></span>----���ߴ�С����ͬ</td>
                                    <td>
                                        <span style="color: Gray"><b>'��'</b></span>----���߽���ʱ�䲻ͬ</td>
                                </tr>
                                <tr class="tdbg">
                                    <td>
                                        <span style="color: Red">��ɫ</span>----����ͬ���޸Ļ���¹����ļ�</td>
                                    <td>
                                        <span style="color: Blue">��ɫ</span>----��һ�߲����ڵ��ļ�</td>
                                    <td>
                                        <span style="color: Gray">��ɫ</span>----�ٷ������ļ���������δ���µ��ļ�</td>
                                </tr>
                                <tr class="tdbg">
                                    <td>
                                        <span style="color: Black">��ɫ</span>----��ͬ�ļ���ٷ��ļ�</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                   <%-- <tr>
                        <td style="text-align: right; height: 28px;" class="tdbg">
                            <strong>��������</strong></td>
                        <td class="tdbg" style="height: 28px">
                            &nbsp;<asp:LinkButton ID="LBtnShowAll" runat="server" >��ʾȫ���ȽϽ��</asp:LinkButton>&nbsp;
                            | &nbsp;<asp:LinkButton ID="LBtnShowDiff" runat="server">ֻ��ʾ���첿��</asp:LinkButton></td>
                    </tr>--%>
                </table>
                <br />
                <asp:Table ID="TbCompareFiles" runat="server" Width="100%" CellPadding="3" CellSpacing="0"
                    CssClass="border">
                    <asp:TableRow ID="TableRow1" runat="server" CssClass="compare_title" Height="24px">
                        <asp:TableCell ID="TableCell1" runat="server" HorizontalAlign="Center">
                            <asp:LinkButton ID="LbtnServerFileName" CommandName="FileName" CommandArgument="DESC"
                                Text="����(�ٷ�)" runat="server" OnClick="LbtnSort_Click" /></asp:TableCell>
                        <asp:TableCell ID="TableCell2" runat="server" HorizontalAlign="Center" Width="60px">
                            <asp:LinkButton ID="LbtnServerSize" CommandName="ServerSize" CommandArgument="DESC" Text="��С(Byte)"
                                runat="server" OnClick="LbtnSort_Click" /></asp:TableCell>
                        <asp:TableCell ID="TableCell3" runat="server" HorizontalAlign="Center" Width="125px">
                            <asp:LinkButton ID="LbtnServerTime" CommandName="ServerTime" CommandArgument="DESC" Text="�޸�ʱ��"
                                runat="server" OnClick="LbtnSort_Click" /></asp:TableCell>
                        <asp:TableCell ID="TableCell4" runat="server" CssClass="compare_tdtop" Width="30px"
                            HorizontalAlign="Center"></asp:TableCell>
                        <asp:TableCell ID="TableCell5" runat="server" HorizontalAlign="Center">
                            <asp:LinkButton ID="LbtnLocalFileName" CommandName="FileName" CommandArgument="DESC"
                                Text="����(��վ)" runat="server" OnClick="LbtnSort_Click" /></asp:TableCell>
                        <asp:TableCell ID="TableCell6" runat="server" HorizontalAlign="Center" Width="60px">
                            <asp:LinkButton ID="LbtnLocalSize" CommandName="LocalSize" CommandArgument="DESC" Text="��С(Byte)"
                                runat="server" OnClick="LbtnSort_Click" /></asp:TableCell>
                        <asp:TableCell ID="TableCell7" runat="server" HorizontalAlign="Center" Width="125px" >
                            <asp:LinkButton ID="LbtnLocalTime" CommandName="LocalTime" CommandArgument="DESC" Text="�޸�ʱ��"
                                runat="server" OnClick="LbtnSort_Click" /></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                <br />
                <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
                    <tr class="tdbg">
                        <td colspan="4">
                            <b>�ٷ��ͱ�վ�ȽϽ��ͳ�ƣ�</b></td>
                    </tr>
                    <tr class="tdbg">
                        <td>
                            �ٷ������ļ���<asp:Label ID="LblServerFiles" runat="server" ForeColor="Blue"></asp:Label>��</td>
                        <td>
                            �ٷ������ڵ��ļ���<asp:Label ID="LblServerNoExists" runat="server" ForeColor="Red"></asp:Label>��</td>
                        <td>
                            ���ع����ļ���<asp:Label ID="LblLocalFiles" runat="server" ForeColor="Blue"></asp:Label>��</td>
                        <td>
                            ���ز����ڵ��ļ���<asp:Label ID="lblLocalNoExists" runat="server" ForeColor="Red"></asp:Label>��</td>
                    </tr>
                    <tr class="tdbg">
                        <td>
                            ���ߴ�Сʱ����ȫ��ͬ��<asp:Label ID="LblSame" runat="server" ForeColor="Green"></asp:Label>��</td>
                        <td>
                            ���ߴ�С����ͬ��<asp:Label ID="LblSizeDiff" runat="server" ForeColor="Red"></asp:Label>��</td>
                        <td>
                            ���߽���ʱ�䲻ͬ��<asp:Label ID="LblOnlyDateDiff" runat="server" ForeColor="Gray"></asp:Label>��</td>
                        <td>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    &nbsp;
    &nbsp;&nbsp;
</asp:Content>
