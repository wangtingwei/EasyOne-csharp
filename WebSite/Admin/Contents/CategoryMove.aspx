<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Contents.CategoryMove" Codebehind="CategoryMove.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table width="100%" border="0" cellpadding="2" cellspacing="0" class="border">
        <tr align="center">
            <td colspan="4" class="spacingtitle">
                <strong>移动节点</strong>
            </td>
        </tr>
        <tr align="center" class="tdbg">
            <td style="width: 239px;">
                <table width="100%" border="0" cellpadding="2" cellspacing="1">
                    <tr>
                        <td valign="top" align="left" style="height: 380px; width: 246px;">
                            <strong>当前节点：</strong>
                            <div id="DivLstFromNodes">
                            <asp:ListBox ID="LstFromNodes" runat="server" Height="286px" Width="237px" Enabled="True">
                            </asp:ListBox>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 100px">
                <strong>移动到&gt;&gt;&gt;</strong></td>
            <td valign="top" style="width: 380px">
                <table width="100%" border="0" cellpadding="2" cellspacing="1">
                    <tr>
                        <td align="left">
                            <strong>目标节点：</strong><span style="color: #ff0000">（不能指定为当前节点的下属子节点或外部节点）</span>
                            <div id="DivLstToNodes">
                            <asp:ListBox ID="LstToNodes" runat="server" Height="286px" Width="255px" AppendDataBoundItems="true">
                                <asp:ListItem Text="做为根节点" Value="0"></asp:ListItem>
                            </asp:ListBox>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 88px">
            </td>
        </tr>
    </table>
    <br />
    <center>
        <asp:Button ID="EBtnSubmit" Text="保存移动结果" OnClick="EBtnSubmit_Click" OnClientClick="BOX_show('RegUser');" runat="server" />
        <asp:Button ID="BtnCancel" runat="server" Text="取消" OnClick="BtnCancel_Click" />
    </center>
    <br />
    <div id="BOX_overlay" style="display:none;">
        <div id="RegUser">
            <div>
                <label><font color="#FF0000">数据正在更新中……</font></label><br />
                <img alt="" src="<%=BasePath %>admin/Images/progressbar.gif" />
            </div>
        </div>
    </div>
<script src="<%=BasePath %>admin/JS/ModalPopup.js" type="text/javascript"></script>
</asp:Content>
