<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Contents.CategoryPatch" Title="�޸��ڵ�" Codebehind="CategoryPatch.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td colspan="2" class="spacingtitle">
                �޸��ڵ�
            </td>
        </tr>
        <tr>
            <td class="tdbg" style="width: 100%;">
                <br />
                ���ڵ������������λ�����ʱ��ʹ�ô˹��ܿ����޸����������൱��ȫ�������ϵͳ�����κθ���Ӱ�졣<br />
                <br />
                �޸�����������ˢ��ҳ�棡<br />
                <br />
                <br />
            </td>
        </tr>
        <tr>
            <td class="tdbg" style="width: 100%;" align="center">
                <asp:Button ID="EBtnPatch" Text="��ʼ�޸�" OnClick="EBtnPatch_Click" OnClientClick="BOX_show('RegUser');" runat="server" />
                <asp:Button ID="BtnCancel" runat="server" Text="ȡ��" OnClick="BtnCancel_Click" />
            </td>
        </tr>
    </table>
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
