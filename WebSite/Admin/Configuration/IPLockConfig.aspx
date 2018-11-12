<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Accessories.IPLockConfig"
    Title="IP访问限定配置" Codebehind="IPLockConfig.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="server">
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td colspan="2" class="spacingtitle">
                <strong>IP访问限定配置</strong>
            </td>
        </tr>
        <tr class="tdbg">
            <td style="width: 40%" class="tdbgleft">
                <strong>全站来访限定方式：</strong>
                <br />
                <asp:Label ID="LblMsg1" runat="server" Text="此功能只对ASPX等.NET页面访问方式有效。如果你以前生成了HTML文件，则启用此功能后，这些HTML文件仍可以访问（除非手工删除）。可以使用此功能配合节点、及文章的权限设置和生成HTML方式来达到整站限定IP访问，或者只对有权限设置的内容进行IP限定。"
                    ForeColor="Red"></asp:Label>
            </td>
            <td>
                <asp:RadioButtonList ID="RdnLockIPType" runat="server">
                    <asp:ListItem Value="0">
                                不启用来访限定功能，任何IP都可以访问本站。
                    </asp:ListItem>
                    <asp:ListItem Value="1">
                                仅仅启用白名单，只允许白名单中的IP访问本站。
                    </asp:ListItem>
                    <asp:ListItem Value="2">
                                仅仅启用黑名单，只禁止黑名单中的IP访问本站。
                    </asp:ListItem>
                    <asp:ListItem Value="3">                                
                                同时启用白名单与黑名单，先判断IP是否在白名单中，如果不在，则禁止访问；如果在则再判断是否在黑名单中，如果IP在黑名单中则禁止访问，否则允许访问。
                    </asp:ListItem>
                    <asp:ListItem Value="4">
                                同时启用白名单与黑名单，先判断IP是否在黑名单中，如果不在，则允许访问；如果在则再判断是否在白名单中，如果IP在白名单中则允许访问，否则禁止访问。
                    </asp:ListItem>
                </asp:RadioButtonList>
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
            </td>
        </tr>
        <tr class="tdbg">
            <td style="width: 40%" class="tdbgleft">
                <strong>全站IP段白名单</strong>：
            </td>
            <td class="tdbg">
                &nbsp;<pe:IPLock ID="IPLockWhite" runat="server" />
            </td>
        </tr>
        <tr class="tdbg">
            <td style="width: 40%" class="tdbgleft">
                <strong>全站IP段黑名单</strong>：
            </td>
            <td class="tdbg">
                &nbsp;<pe:IPLock ID="IPLockBlack" runat="server" />
            </td>
        </tr>
        <tr class="tdbg">
            <td style="width: 40%" class="tdbgleft">
                <strong>后台来访限定方式：</strong>
            </td>
            <td>
                <asp:RadioButtonList ID="RdnAdminLockIPType" runat="server">
                    <asp:ListItem Value="0">
                                不启用来访限定功能，任何IP都可以访问本站后台。
                    </asp:ListItem>
                    <asp:ListItem Value="1">
                                仅仅启用白名单，只允许白名单中的IP访问本站后台。
                    </asp:ListItem>
                    <asp:ListItem Value="2">
                                仅仅启用黑名单，只禁止黑名单中的IP访问本站后台。
                    </asp:ListItem>
                    <asp:ListItem Value="3">                                
                                同时启用白名单与黑名单，先判断IP是否在白名单中，如果不在，则禁止访问；如果在则再判断是否在黑名单中，如果IP在黑名单中则禁止访问，否则允许访问。
                    </asp:ListItem>
                    <asp:ListItem Value="4">
                                同时启用白名单与黑名单，先判断IP是否在黑名单中，如果不在，则允许访问；如果在则再判断是否在白名单中，如果IP在白名单中则允许访问，否则禁止访问。
                    </asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr class="tdbg">
            <td style="width: 40%" class="tdbgleft">
                <strong>后台IP段白名单</strong>：
            </td>
            <td class="tdbg">
                &nbsp;<pe:IPLock ID="IPLockAdminWhite" runat="server" />
            </td>
        </tr>
        <tr class="tdbg">
            <td style="width: 40%;" class="tdbgleft">
                <strong>后台IP段黑名单</strong>：
                <br />
            </td>
            <td class="tdbg">
                &nbsp;<pe:IPLock ID="IPLockAdminBlack" runat="server" />
            </td>
        </tr>
        <tr class="tdbgbottom">
            <td colspan="2">
                <asp:Button ID="BtnSubmit" runat="server" Text="保存设置" Width="110px" OnClick="BtnSubmit_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
