<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPage.master"
    Inherits="EasyOne.WebSite.Admin.User.BatchMove" Codebehind="BatchMove.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table width="100%" border="0" cellpadding="2" cellspacing="0" class="border">
        <tr align="center">
            <td colspan="4" class="spacingtitle">
                <b>�����ƶ���Ա</b>
            </td>
        </tr>
        <tr align="center" class="tdbg">
            <td style="width: 300px;">
                <table width="100%" border="0" cellpadding="2" cellspacing="1">
                    <tr>
                        <td valign="top" align="left" style="height: 380px">
                            <asp:RadioButton ID="RadUserType1" GroupName="UserType" runat="server" Text="ָ����ԱID��"
                                Checked="true" />
                            <asp:TextBox ID="TxtBatchUserID" runat="server"></asp:TextBox>
                            <br />
                            <asp:RadioButton ID="RadUserType2" GroupName="UserType" runat="server" Text="ָ���û�����" />
                            <asp:TextBox ID="TxtBatchUserName" runat="server"></asp:TextBox>
                            <br />
                            <asp:RadioButton ID="RadUserType3" GroupName="UserType" runat="server" Text="ָ����ԱID�ķ�Χ��" />
                            <asp:TextBox ID="TxtStartUserId" runat="server" Width="60px"></asp:TextBox>��
                            <asp:TextBox ID="TxtEndUserId" runat="server" Width="49px"></asp:TextBox>
                            <br />
                            <asp:RadioButton ID="RadUserType4" GroupName="UserType" runat="server" Text="ָ��Ҫ�ƶ��Ļ�Ա�飺" />
                            <br />
                            <asp:ListBox ID="LstBatchUserGroupID" runat="server" Height="286px" Width="276px"
                                SelectionMode="Multiple"></asp:ListBox>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 100px">
                �ƶ���&gt;&gt;</td>
            <td valign="top" style="width: 251px">
                <table width="100%" border="0" cellpadding="2" cellspacing="1">
                    <tr>
                        <td align="left">
                            <br />
                            <br />
                            <br />
                            <br />
                            ��ָ��Ŀ���Ա�飺<br />
                            <asp:ListBox ID="LstUserGroupID" runat="server" Height="281px" Width="255px"></asp:ListBox>
                        </td>
                    </tr>
                </table>
            </td>
            <td>
            </td>
        </tr>
    </table>
    <br />
    <center>
        <asp:Button ID="BtnSubmit" runat="server" Text="ִ��������" OnClick="BtnSubmit_Click" />
        <input name="Cancel" type="button" class="inputbutton" id="Cancel" value="ȡ��" onclick="Redirect('UserManage.aspx')" />
    </center>
    <br />
</asp:Content>
