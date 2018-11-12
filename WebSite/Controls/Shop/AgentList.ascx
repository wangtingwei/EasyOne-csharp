<%@ Control Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Controls.AgentList" Codebehind="AgentList.ascx.cs" %>
        <div style="width: 100%; text-align: center;">
            <table width="560px" border="0" style="margin:0 auto;" cellpadding="2" cellspacing="0"
                class="border">
                <tr class="title">
                    <td>
                        <asp:button ID="LbtnDelAgent" runat="server" Text="���ѡ�еĴ�����"></asp:button>
                                            
                    </td>
                    <td style="text-align: right">
                        ���Ҵ����̣�
                        <asp:TextBox ID="TxtKeyWord" runat="server" />&nbsp;<asp:Button ID="BtnSearch" runat="server"
                            Text="����" OnClick="BtnSearch_Click" />
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        <asp:DataList ID="DlstAgent" runat="server" CellPadding="1" CellSpacing="1" HorizontalAlign="Center"
                            ItemStyle-BackColor="#FFFFFF" ItemStyle-HorizontalAlign="center" RepeatColumns="5"
                            RepeatDirection="Horizontal" Width="100%" BackColor="#FFFFFF" 
                            onitemdatabound="DlstAgent_ItemDataBound">
                            <ItemTemplate>
                                <asp:LinkButton ID="LbtnAgentName" runat="server"><%#Container.DataItem%></asp:LinkButton><br />
                            </ItemTemplate>
                        </asp:DataList>
                    </td>
                </tr>
            </table>
        </div>
        <div style="text-align: center;">
            <pe:AspNetPager ID="Pager" runat="server" OnPageChanged="Pager_PageChanged">
            </pe:AspNetPager>
        </div>
