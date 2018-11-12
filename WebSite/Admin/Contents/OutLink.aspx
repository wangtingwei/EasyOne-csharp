<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Contents.OutLink" ValidateRequest="false" Title="��ҳ�ⲿ����" Codebehind="OutLink.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border" style="margin: 0 auto;">
        <tr align="center">
            <td class="spacingtitle" colspan="2">
                <asp:Label ID="LblTitle" runat="server" Text="����ⲿ����" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>�����ڵ㣺</strong></td>
            <td>
                <asp:DropDownList ID="DropParentNode" DataValueField="NodeId" DataTextField="NodeName"
                    runat="server" AppendDataBoundItems="true">
                    <asp:ListItem Text="���ڵ�" Value="0"></asp:ListItem>
                </asp:DropDownList><asp:Label ID="LblNodeName" runat="server" Text=""></asp:Label>
                <asp:Label ID="LblNodePermissions" runat="server" Text=""></asp:Label>
             
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>�ⲿ�������ƣ�</strong></td>
            <td>
                <asp:TextBox ID="TxtNodeName" runat="server"></asp:TextBox>
                <pe:RequiredFieldValidator ID="ValrNodeName" runat="server" ErrorMessage="�ⲿ�������Ʋ���Ϊ�գ�"
                    ControlToValidate="TxtNodeName" Display="Dynamic" SetFocusOnError="True"></pe:RequiredFieldValidator>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>�ⲿ���ӱ�ʶ����</strong><br />
                ����ǰ̨����ʱ����ֱ���ñ�ʶ��ȡ��ID</td>
            <td>
                <asp:TextBox ID="TxtNodeIdentifier" runat="server"></asp:TextBox>
                <pe:RequiredFieldValidator ID="ValrNodeIdentifier" runat="server" ErrorMessage="��ʶ������Ϊ�գ�"
                    ControlToValidate="TxtNodeIdentifier" Display="Dynamic" SetFocusOnError="True"></pe:RequiredFieldValidator></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>�ⲿ���ӵ�ַ��</strong>
            </td>
            <td>
                <asp:TextBox ID="TxtLinkUrl" runat="server" Width="289px"></asp:TextBox><pe:UrlValidator
                    ID="Vurl" ControlToValidate="TxtLinkUrl" runat="server"></pe:UrlValidator>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>�򿪷�ʽ��</strong></td>
            <td>
                <asp:RadioButtonList ID="RadlOpenType" RepeatLayout="Flow" RepeatDirection="Horizontal"
                    runat="server">
                    <asp:ListItem Text="��ԭ���ڴ�" Selected="True" Value="0"></asp:ListItem>
                    <asp:ListItem Text="���´��ڴ�" Value="1"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>�ⲿ����ͼƬ��ַ��</strong><br />
                �������ⲿ����ҳ��ʾָ����ͼƬ</td>
            <td style="width: 498px">
                <asp:TextBox ID="TxtNodePicUrl" MaxLength="255" runat="server" Width="289px"></asp:TextBox></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>�ⲿ������ʾ��</strong><br />
                ��������ⲿ����������ʱ����ʾ�趨����ʾ���֣���֧��HTML��</td>
            <td>
                <asp:TextBox ID="TxtTips" runat="server" Columns="60" Height="56px" Width="289px"
                    Rows="5" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>�Ƿ�λ�õ�������ʾ��</strong></td>
            <td>
                <asp:RadioButtonList ID="RadlShowOnPath" RepeatLayout="Flow" RepeatDirection="Horizontal"
                    runat="server">
                    <asp:ListItem Text="��" Selected="True" Value="True"></asp:ListItem>
                    <asp:ListItem Text="��" Value="False"></asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
        <tr class="tdbgbottom">
            <td colspan="2">
                <asp:Button ID="EBtnSubmit" Text="����" OnClick="EBtnSubmit_Click" runat="server" />&nbsp;&nbsp;&nbsp;
                <input name="Cancel" type="button" class="inputbutton" id="BtnCancel" value="ȡ��"
                    onclick="Redirect('CategoryManage.aspx')" />
            </td>
        </tr>
    </table>
</asp:Content>
