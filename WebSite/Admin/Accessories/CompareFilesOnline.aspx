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
                    <strong>在线比较网站文件</strong></td>
            </tr>
            <tr class="tdbg">
                <td>
                    <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;可以通过此功能比较网站中的可运行文件和官方发布的相应版本的原始可运行文件之间的差异，以方便对网站的文件进行检查和更新。<br />
                    当以下情况出现时可以使用本功能进行比较：<div style="color: Green">
                        <br />
                        &nbsp;&nbsp;&nbsp;&nbsp;1）当官方更新文件或者发布补丁时；<br />
                        &nbsp;&nbsp;&nbsp;&nbsp;2）当怀疑站点的可运行文件被人删除或恶意修改时；<br />
                    </div>
                    <p>
                        &nbsp;&nbsp;&nbsp;&nbsp;如果网站文件很多，或者网络速度比较慢，执行本操作需要耗费相当长的时间，请在访问量少时执行本操作。</p>
                    &nbsp;&nbsp;
                    <div style="text-align: center">
                        <asp:Button ID="BtnCompareTogether" runat="server" OnClick="BtnCompareTogether_Click"
                            Text="开始比较" /><br />
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
                            <strong>各项含义：</strong></td>
                        <td style="height: 30px">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                <tr class="tdbg">
                                    <td>
                                        <b>'= '</b>----两边大小时间完全相同</td>
                                    <td>
                                        <span style="color: Red"><b>'≠'</b></span>----两边大小不相同</td>
                                    <td>
                                        <span style="color: Gray"><b>'≈'</b></span>----两边仅仅时间不同</td>
                                </tr>
                                <tr class="tdbg">
                                    <td>
                                        <span style="color: Red">红色</span>----不相同，修改或更新过的文件</td>
                                    <td>
                                        <span style="color: Blue">蓝色</span>----另一边不存在的文件</td>
                                    <td>
                                        <span style="color: Gray">灰色</span>----官方有新文件，但本地未更新的文件</td>
                                </tr>
                                <tr class="tdbg">
                                    <td>
                                        <span style="color: Black">黑色</span>----相同文件或官方文件</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                   <%-- <tr>
                        <td style="text-align: right; height: 28px;" class="tdbg">
                            <strong>管理导航：</strong></td>
                        <td class="tdbg" style="height: 28px">
                            &nbsp;<asp:LinkButton ID="LBtnShowAll" runat="server" >显示全部比较结果</asp:LinkButton>&nbsp;
                            | &nbsp;<asp:LinkButton ID="LBtnShowDiff" runat="server">只显示差异部分</asp:LinkButton></td>
                    </tr>--%>
                </table>
                <br />
                <asp:Table ID="TbCompareFiles" runat="server" Width="100%" CellPadding="3" CellSpacing="0"
                    CssClass="border">
                    <asp:TableRow ID="TableRow1" runat="server" CssClass="compare_title" Height="24px">
                        <asp:TableCell ID="TableCell1" runat="server" HorizontalAlign="Center">
                            <asp:LinkButton ID="LbtnServerFileName" CommandName="FileName" CommandArgument="DESC"
                                Text="名称(官方)" runat="server" OnClick="LbtnSort_Click" /></asp:TableCell>
                        <asp:TableCell ID="TableCell2" runat="server" HorizontalAlign="Center" Width="60px">
                            <asp:LinkButton ID="LbtnServerSize" CommandName="ServerSize" CommandArgument="DESC" Text="大小(Byte)"
                                runat="server" OnClick="LbtnSort_Click" /></asp:TableCell>
                        <asp:TableCell ID="TableCell3" runat="server" HorizontalAlign="Center" Width="125px">
                            <asp:LinkButton ID="LbtnServerTime" CommandName="ServerTime" CommandArgument="DESC" Text="修改时间"
                                runat="server" OnClick="LbtnSort_Click" /></asp:TableCell>
                        <asp:TableCell ID="TableCell4" runat="server" CssClass="compare_tdtop" Width="30px"
                            HorizontalAlign="Center"></asp:TableCell>
                        <asp:TableCell ID="TableCell5" runat="server" HorizontalAlign="Center">
                            <asp:LinkButton ID="LbtnLocalFileName" CommandName="FileName" CommandArgument="DESC"
                                Text="名称(本站)" runat="server" OnClick="LbtnSort_Click" /></asp:TableCell>
                        <asp:TableCell ID="TableCell6" runat="server" HorizontalAlign="Center" Width="60px">
                            <asp:LinkButton ID="LbtnLocalSize" CommandName="LocalSize" CommandArgument="DESC" Text="大小(Byte)"
                                runat="server" OnClick="LbtnSort_Click" /></asp:TableCell>
                        <asp:TableCell ID="TableCell7" runat="server" HorizontalAlign="Center" Width="125px" >
                            <asp:LinkButton ID="LbtnLocalTime" CommandName="LocalTime" CommandArgument="DESC" Text="修改时间"
                                runat="server" OnClick="LbtnSort_Click" /></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                <br />
                <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
                    <tr class="tdbg">
                        <td colspan="4">
                            <b>官方和本站比较结果统计：</b></td>
                    </tr>
                    <tr class="tdbg">
                        <td>
                            官方共有文件：<asp:Label ID="LblServerFiles" runat="server" ForeColor="Blue"></asp:Label>个</td>
                        <td>
                            官方不存在的文件：<asp:Label ID="LblServerNoExists" runat="server" ForeColor="Red"></asp:Label>个</td>
                        <td>
                            本地共有文件：<asp:Label ID="LblLocalFiles" runat="server" ForeColor="Blue"></asp:Label>个</td>
                        <td>
                            本地不存在的文件：<asp:Label ID="lblLocalNoExists" runat="server" ForeColor="Red"></asp:Label>个</td>
                    </tr>
                    <tr class="tdbg">
                        <td>
                            两边大小时间完全相同：<asp:Label ID="LblSame" runat="server" ForeColor="Green"></asp:Label>个</td>
                        <td>
                            两边大小不相同：<asp:Label ID="LblSizeDiff" runat="server" ForeColor="Red"></asp:Label>个</td>
                        <td>
                            两边仅仅时间不同：<asp:Label ID="LblOnlyDateDiff" runat="server" ForeColor="Gray"></asp:Label>个</td>
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
