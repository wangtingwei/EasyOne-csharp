<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Controls.MergeOrder" Codebehind="MergeOrder.ascx.cs" %>
<div style="text-align: center">
    <table width='100%' border='0' cellpadding='2' cellspacing='1' class='border'>
        <tr align='center' class='title'>
            <td colspan='2'>
                �ϲ�����</td>
        </tr>
        <tr class='tdbg'>
            <td align='right' style="width: 30%;" class='tdbgleft'>
                ��������</td>
            <td align='left'>
                <asp:UpdatePanel runat="server" ID="UpnlPrincipalOrder" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:TextBox ID="TxtPrincipalOrder" MaxLength="50" Width="150px" runat="server"></asp:TextBox>
                        <asp:DropDownList ID="DropPrincipalOrder" DataTextField="value" DataValueField="key"
                            AutoPostBack="true" OnSelectedIndexChanged="DropPrincipalOrder_SelectedIndexChanged"
                            runat="server">
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <pe:RequiredFieldValidator ShowRequiredText="false" ID="ValrPrincipalOrder" runat="server"
                    Display="dynamic" ControlToValidate="TxtPrincipalOrder" ErrorMessage="������Ҫ�ϲ�����������"></pe:RequiredFieldValidator>
            </td>
        </tr>
        <tr class='tdbg'>
            <td align='right' style="width: 30%;" class='tdbgleft'>
                �Ӷ�����</td>
            <td align='left'>
                <asp:UpdatePanel runat="server" ID="UpnlSubordinateOrder" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:TextBox ID="TxtSubordinateOrder" MaxLength="50" Width="150px" runat="server"></asp:TextBox>
                        <asp:DropDownList ID="DropSubordinateOrder" DataTextField="value" DataValueField="key"
                            runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropSubordinateOrder_SelectedIndexChanged">
                        </asp:DropDownList>
                        �ϲ���Ӷ�������ֱ��ɾ��
                    </ContentTemplate>
                </asp:UpdatePanel>
                <pe:RequiredFieldValidator ShowRequiredText="false" ID="ValrSubordinateOrder" runat="server"
                    Display="dynamic" ControlToValidate="TxtSubordinateOrder" ErrorMessage="������Ҫ�ϲ��ĴӶ�����"></pe:RequiredFieldValidator>
            </td>
        </tr>
        <tr class='tdbg'>
            <td align='right' style="width: 30%;" class='tdbgleft'>
                �ϲ���ʽ��</td>
            <td align='left'>
                <asp:RadioButtonList ID="RadlMergeType" runat="server">
                    <asp:ListItem Value="0" Selected="true">�����������ı�ע���Ժ��ڲ���¼</asp:ListItem>
                    <asp:ListItem Value="1">����Ӷ����ı�ע���Ժ��ڲ���¼</asp:ListItem>
                    <asp:ListItem Value="2">�������������Ӷ����ı�ע���Ժ��ڲ���¼</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr class="tdbg">
            <td colspan="2">
                <span style="color:blue">ע�⣺ֻ�ܶ�δȷ�ϵĶ������кϲ�</span>
            </td>
        </tr>
        <tr class="tdbgbottom">
            <td colspan="2">
                <asp:Button ID="BtnSubmit" runat="server" Text="�ϲ�" OnClick="BtnSubmit_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                <input type="button" onclick="javascript:history.go(-1)" value="ȡ��" class="inputbutton" />
            </td>
        </tr>
    </table>
</div>
