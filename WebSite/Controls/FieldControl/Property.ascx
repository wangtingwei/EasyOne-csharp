<%@ Control Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Controls.FieldControl.Property" Codebehind="Property.ascx.cs" %>
<tr id="Tab" runat="server" class="tdbg">
    <td class="tdbgleft" align="right" style="width: 20%;">
        <strong>
            <%= FieldAlias %>
            ：&nbsp;</strong><br />
    </td>
    <td>
        <asp:TextBox ID="TxtProperty" runat="server" Width="185px" Text="点击选择" onfocus="this.value=''" onblur="if(this.value=='') this.value='点击选择'"
    ontextchanged="TxtProperty_TextChanged" AutoPostBack="true" ToolTip="名称中不能带有“$”、“|”、“*”字符！"></asp:TextBox>
                <asp:Panel ID="DropPanel" runat="server" CssClass="ContextMenuPanel" Style="display :none; visibility: hidden;">       
                </asp:Panel>     
<%--        <asp:Literal ID="LtrProperty" runat="server" Visible="false"></asp:Literal>  --%>
        <asp:UpdatePanel ID="UpnlSelectProperty" runat="server">
            <ContentTemplate>
                <asp:Repeater runat="server" ID="RptSelectPropertyItem" 
                    onitemdatabound="RptSelectPropertyItem_ItemDataBound">
                    <HeaderTemplate>
                        <table width="80%">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class="tdbg">
                            <td style="width:185px">
                                <%#Container.DataItem%>
                            </td>
                            <td>
                                <asp:LinkButton ID="DelItem" runat="server" Text="×" CausesValidation="false" CommandArgument='<%#Container.DataItem %>' CommandName="DelPropertyItem" OnCommand="DelItem_Command" ForeColor="Red" Font-Bold="true"></asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </ContentTemplate>
        </asp:UpdatePanel>
        <ajaxToolkit:DropDownExtender runat="server" ID="DDE" TargetControlID="TxtProperty" DropDownControlID="DropPanel" />
    </td>
</tr>

