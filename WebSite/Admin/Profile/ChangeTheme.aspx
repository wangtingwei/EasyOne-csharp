<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPage.master" Inherits="EasyOne.WebSite.Admin.ChangeTheme" Codebehind="ChangeTheme.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td colspan="2" class="spacingtitle">
                主题控制</td>
        </tr>
        <tr class="tdbg">
            <td>
                <table width="100%">
                    <tr>
                        <asp:Repeater ID="RptTheme" runat="server" DataSourceID="OdsTheme">
                            <ItemTemplate>
                                <td align="center">
                                    <asp:Image Width="300" runat="server" ImageUrl='<%# BasePath  + "App_Themes/"+ Eval("Name").ToString() + "/ThemeThumb.gif" %>' />
                                    <br />
                                    <%# "<input id='" + Eval("Name").ToString() + "' name='Theme' type='radio' value='" + Eval("Name").ToString() + "'" + (StyleSheetTheme == Eval("Name").ToString() ? " Checked='Checked'" : "") + " />"%>
                                    <label for='<%# Eval("Name").ToString() %>'>
                                        <%# Eval("Name") %>
                                    </label>
                                    <br />
                                </td>
                                <% 
                                    m_ItemIndex++; 
                                %>
                                <%
                                    if (m_ItemIndex % 2 == 0 && m_ItemIndex > 1)
                                    {
                                %>
                                </tr><tr>
                                    <%
                                        }
                                    %>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="tdbgbottom">
            <td colspan="2">
                <asp:Button ID="BtnChangeTheme" runat="server" Text="应用主题" OnClick="BtnChangeTheme_Click" />
            </td>
        </tr>
    </table>
    <br />
    说明：可以通过在~/App_Themes/目录下增加“Admin***”目录来设置自定义的后台主题。
    <asp:ObjectDataSource ID="OdsTheme" runat="server" SelectMethod="AdminThemesList"
        TypeName="EasyOne.Web.ThemeManager"></asp:ObjectDataSource>
</asp:Content>
