<%@ Control Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Controls.RegionControl" Codebehind="Region.ascx.cs" %>
<asp:UpdatePanel ID="UpnlRegion" UpdateMode="Conditional" runat="server">
    <ContentTemplate>
        <table width="100%" border="0" cellpadding="2" cellspacing="1">
            <asp:PlaceHolder ID="PlhCountry" runat="server">
            <tr class="tdbg">
                <td style="width: 100px" align="right" class="tdbgleft">
                    ����/������
                </td>
                <td colspan="2" align="left">
                    <asp:DropDownList ID="DropCountry" runat="server" DataTextField="Country" DataValueField="Country"
                        AutoPostBack="true" OnSelectedIndexChanged="DropCountry_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            </asp:PlaceHolder>
            <tr class="tdbg">
                <td style="width: 100px" align="right" class="tdbgleft">
                    ʡ/��/��������
                </td>
                <td align="left">
                    <asp:DropDownList ID="DropProvince" DataTextField="Province" DataValueField="Province"
                        AutoPostBack="true" runat="server" OnSelectedIndexChanged="DropProvince_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr class="tdbg">
                <td align="right" class="tdbgleft">
                    ��/��/��/�죺
                </td>
                <td align="left">
                    <asp:DropDownList ID="DropCity" DataTextField="City" DataValueField="City"
                        runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>