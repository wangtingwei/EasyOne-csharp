<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Contents.CategoryReset" Title="��λ���нڵ�" Codebehind="CategoryReset.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td colspan="2" class="spacingtitle">
                ��λ���нڵ�
            </td>
        </tr>
        <tr>
            <td class="tdbg" style="width: 100%">
                <span style="color: #ff0000"><strong>ע�⣺</strong></span><br />
                ���ѡ��λ���нڵ㣬�����нڵ㶼����Ϊһ���ڵ㣬��ʱ����Ҫ���¶Ը����ڵ���й����Ļ������á���Ҫ����ʹ�øù��ܣ����������˴�������ö��޷���ԭ�ڵ�֮��Ĺ�ϵ�������ʱ��ʹ�á�<br />
                <br />
                �����λʱ������ͬ���ڵ㣬��ϵͳ���Զ���Ŀ¼��������������
                <br />
                <br />
                ��λ�ɹ�����ǵ�һ��Ҫ������������HTML�����ݡ�
            </td>
        </tr>
        <tr>
            <td class="tdbg" style="width: 100%" align="center">
                <asp:Button ID="EBtnReset" Text="��λ���нڵ�" OnClick="EBtnReset_Click" OnClientClick="BOX_show('RegUser');" runat="server" />
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
