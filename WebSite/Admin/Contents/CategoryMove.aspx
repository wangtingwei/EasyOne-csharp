<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Contents.CategoryMove" Codebehind="CategoryMove.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table width="100%" border="0" cellpadding="2" cellspacing="0" class="border">
        <tr align="center">
            <td colspan="4" class="spacingtitle">
                <strong>�ƶ��ڵ�</strong>
            </td>
        </tr>
        <tr align="center" class="tdbg">
            <td style="width: 239px;">
                <table width="100%" border="0" cellpadding="2" cellspacing="1">
                    <tr>
                        <td valign="top" align="left" style="height: 380px; width: 246px;">
                            <strong>��ǰ�ڵ㣺</strong>
                            <div id="DivLstFromNodes">
                            <asp:ListBox ID="LstFromNodes" runat="server" Height="286px" Width="237px" Enabled="True">
                            </asp:ListBox>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 100px">
                <strong>�ƶ���&gt;&gt;&gt;</strong></td>
            <td valign="top" style="width: 380px">
                <table width="100%" border="0" cellpadding="2" cellspacing="1">
                    <tr>
                        <td align="left">
                            <strong>Ŀ��ڵ㣺</strong><span style="color: #ff0000">������ָ��Ϊ��ǰ�ڵ�������ӽڵ���ⲿ�ڵ㣩</span>
                            <div id="DivLstToNodes">
                            <asp:ListBox ID="LstToNodes" runat="server" Height="286px" Width="255px" AppendDataBoundItems="true">
                                <asp:ListItem Text="��Ϊ���ڵ�" Value="0"></asp:ListItem>
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
        <asp:Button ID="EBtnSubmit" Text="�����ƶ����" OnClick="EBtnSubmit_Click" OnClientClick="BOX_show('RegUser');" runat="server" />
        <asp:Button ID="BtnCancel" runat="server" Text="ȡ��" OnClick="BtnCancel_Click" />
    </center>
    <br />
    <div id="BOX_overlay" style="display:none;">
        <div id="RegUser">
            <div>
                <label><font color="#FF0000">�������ڸ����С���</font></label><br />
                <img alt="" src="<%=BasePath %>admin/Images/progressbar.gif" />
            </div>
        </div>
    </div>
<script src="<%=BasePath %>admin/JS/ModalPopup.js" type="text/javascript"></script>
</asp:Content>
