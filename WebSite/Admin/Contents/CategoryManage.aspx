<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Contents.CategoryManage" Title="�ڵ����" Codebehind="CategoryManage.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="border">
        <tr class="tdbg" align="center">
            <td colspan="2" class="spacingtitle">
                �ڵ�ͨ�ò���
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" style="width:15%">
                <asp:HyperLink ID="HypCategory" NavigateUrl="~/Admin/Contents/Category.aspx" runat="server">�����Ŀ�ڵ�</asp:HyperLink>
            </td>
            <td>
                Ϊ���������������н���Ϣ����ɡ�������Ϣ��������Ŀѡ�����ģ��ѡ������շ����á�����ǰ̨��ʽ�������ϴ�ѡ���������ѡ�����Ȩ�����á��͡��������ݡ�����ǩʽ����ѡ��Է��㰴��ݷ���������Ϣѡ�����д�������Ϣ�󣬵���ҳ��ײ�����ӡ���ť��������ӵ���Ŀ�ڵ㡣
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <asp:HyperLink ID="HypSingle" NavigateUrl="~/Admin/Contents/Single.aspx" runat="server">��ӵ�ҳ�ڵ�</asp:HyperLink>
            </td>
            <td>
                ����վ����Ҫ��ʾ����ϵ��ʽ��������˾��顱������Ȩ���������޷�������ĵ�����Ϣҳ��ʱ��������ӵ�ҳ�ڵ�ķ�ʽ��ʵ�֡�
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <asp:HyperLink ID="HypOutLink" NavigateUrl="~/Admin/Contents/OutLink.aspx" runat="server">����ⲿ����</asp:HyperLink>
            </td>
            <td>
                �ⲿ������ָ����վ����ӽڵ�����ӵ�ַΪ��վ�ⲿ�ĵ�ַ��������վ���������У���ʾ��������̳�����֣����ӵ�ַΪhttp://bbs.EasyOne.net/�����´��ڴ򿪷�ʽ����ַ���������ⲿ���ӽڵ�ķ�ʽ��ʵ�֡�
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <asp:HyperLink ID="HypUnitCategory" NavigateUrl="~/Admin/Contents/CategoryUnite.aspx" runat="server">�ϲ���Ŀ</asp:HyperLink>
            </td>
            <td>
                �������ǽ�һ���ڵ���������Ϣת�ƺϲ���Ŀ��ڵ���ȥ��ԭ�ڵ������������ڵ㽫��ɾ����
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <asp:HyperLink ID="HypMoveCategory" NavigateUrl="~/Admin/Contents/CategoryMove.aspx" runat="server">�ƶ���Ŀ</asp:HyperLink> 
            </td>
            <td>
                �������ǽ�һ���ڵ��е�������Ϣת�Ƶ�Ŀ��ڵ���ȥ��ԭָ���ڵ�����������ڵ㲻�ᱻɾ����
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <asp:HyperLink ID="HypCategoryBatchSet" NavigateUrl="~/Admin/Contents/CategoryBatchSet.aspx" runat="server">��������</asp:HyperLink>  
            </td>
            <td>
                ���������������ýڵ����ԣ������Խڵ�ѡ�ǰ̨��ʽ��Ȩ�����õ��������á����ñ����ܿ��ԶԽڵ��������ͬ���Խ��п���������á�
            </td>
        </tr>
         <tr class="tdbg">
            <td class="tdbgleft">
                <asp:HyperLink ID="HypResetCategory" NavigateUrl="~/Admin/Contents/CategoryReset.aspx" runat="server">��λ������Ŀ</asp:HyperLink>
            </td>
            <td>
                �������ǽ�������Ŀ�ڵ㼰������Ŀ�ڵ㶼��λΪһ���ڵ㡣
            </td>
        </tr>
         <tr class="tdbg">
           <td class="tdbgleft">
                <asp:HyperLink ID="HypRepairCategory" NavigateUrl ="~/Admin/Contents/CategoryPatch.aspx" runat="server">�޸���Ŀ�ṹ</asp:HyperLink>
            </td>
            <td>
               ���������޸��ڵ������������λ�������
            </td>
        </tr>
    </table>
</asp:Content>
