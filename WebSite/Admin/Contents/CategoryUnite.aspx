<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Contents.CategoryUnite" Title="�ڵ�ϲ�" Codebehind="CategoryUnite.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td class="spacingtitle">
                �ڵ�ϲ�
            </td>
        </tr>
        <tr class="tdbg">
            <td>
                <br />
                &nbsp;&nbsp;���ڵ�
                <asp:DropDownList ID="DropFromNode" DataValueField="NodeId" DataTextField="NodeName"
                    runat="server" Width="225px">
                </asp:DropDownList>
                &nbsp;&nbsp;�ϲ���&nbsp;&nbsp;
                <asp:DropDownList ID="DropToNode" DataValueField="NodeId" DataTextField="NodeName"
                    runat="server" Width="225px">
                </asp:DropDownList>
                <br />
            </td>
        </tr>
        <tr class="tdbg">
            <td colspan="4" style="width: 786px; height: 72px;" align="center">
                <asp:Button ID="EBtnUnite" Text="�ϲ��ڵ�" OnClick="EBtnUnite_Click" runat="server" />
                <asp:Button ID="BtnCancel" runat="server" Text="ȡ��" OnClick="BtnCancel_Click" />
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td valign="top">
                <strong><span style="color: Blue">ע�����</span></strong>
            </td>
            <td>
              
                1�����в��������棬�����ز�����
                <br />
                2��������ͬһ���ڵ��ڽ��в��������ܽ�һ���ڵ�ϲ����������ڵ��С�Ŀ��ڵ��в��ܺ����ӽڵ㡣<br />
                3���ϲ�������ָ���Ľڵ㣨���߰����������ڵ㣩����ɾ��������������Ϣ��ת�Ƶ�Ŀ��ڵ��С�
            </td>
        </tr>
    </table>
</asp:Content>
