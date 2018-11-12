<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Contents.SiteOtherCreate"
    Title="无标题页" Codebehind="SiteOtherCreate.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="server">
    <asp:Panel ID="PanelForm" runat="server">
        <table width="100%">
            <tr>
                <td align="center">
                    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
                        <tr align="center">
                            <td class="spacingtitle">
                                RSS生成
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td>
                                <table>
                                    <tr class="tdbg">
                                        <td align="left">
                                           <img src="../../Admin/Images/feedicon.gif" alt="RSS页面" style="border: 0px;" /> 
                                        </td>
                                        <td>生成网站首页的RSS页面，当您禁用RSS或网站首页为动态ASPX格式时，本功能无效。
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Button ID="BtnRss" runat="server" Text="开始生成" OnClick="BtnRss_Click" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
                        <tr align="center">
                            <td class="spacingtitle">
                                Google地图生成操作
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td>
                                <table>
                                    <tr align="left">
                                        <td>
                                            <img src="../../Admin/Images/GoogleSiteMaplogo.gif" alt="Google地图" style="border: 0px;" />
                                        </td>
                                        <td>
                                            生成符合GOOGLE规范的XML格式地图页面，生成参数在标签中配置
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:HyperLink ID="HypGoogleMap" runat="server"></asp:HyperLink>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Button ID="BtnGoogle" runat="server" Text="开始生成" OnClick="BtnGoogle_Click" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
                        <tr align="center">
                            <td class="spacingtitle">
                                BaiDu地图生成操作
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td>
                                <table>
                                    <tr align="left">
                                        <td>
                                            <img src="../../admin/Images/BaiduSiteMaplogo.gif" style="border: 0px;" alt="百度地图" />
                                        </td>
                                        <td>
                                            生成符合百度规范的XML格式地图页面，生成参数在标签中配置
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:HyperLink ID="HypBaidMap" runat="server"></asp:HyperLink>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Button ID="BtnBaidu" runat="server" Text="开始生成" OnClick="BtnBaidu_Click" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <%-- <tr>
            <td align="center">
                <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
                    <tr align="center">
                        <td class="spacingtitle">
                            其它类HTML格式地图生成操作
                        </td>
                    </tr>
                    <tr class="tdbg">
                        <td>
                            <table>
                                <tr align="left">
                                    <td>
                                        生成HTML格式的全站地图页面。
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr align="left">
                                    <td>
                                        总输出数量：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtSiteTotal" runat="server"></asp:TextBox>HTML地图总输出数量
                                    </td>
                                </tr>
                                <tr align="left">
                                    <td>
                                        每页连接数：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtSiteLinkPage" runat="server"></asp:TextBox>每页输出数量，不能大于１００
                                    </td>
                                </tr>
                                <tr align="left">
                                    <td>
                                        分页换行数：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtPageNumber" runat="server"></asp:TextBox>地图分页连接每行显示数
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Button ID="BtnSiteMap" runat="server" Text="开始生成" OnClick="BtnSiteMap_Click" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>--%>
        </table>
    </asp:Panel>
    <asp:Panel ID="PanelMsg" runat="server">
    </asp:Panel>
</asp:Content>
