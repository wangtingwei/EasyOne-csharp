<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.User.MinusValidDate"
    MasterPageFile="~/Admin/MasterPage.master" Title="�۳��û���Ч��" Codebehind="MinusValidDate.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <div style="text-align: center">
        <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
            <tr align="center" class="title">
                <td colspan="2">
                    <asp:Label ID="LblTitle" runat="server" Text=" �۳��û���Ч�� " Font-Bold="True"></asp:Label></td>
            </tr>
            <tr class="tdbg">
                <td align="right" style="width: 100%;" colspan="2">
                    <pe:ShowUserInfo ID="showUserInfo" runat="server" />
                </td>
            </tr>
            <tr class="tdbg">
                <td align="right" style="width: 15%;" class="tdbgleft">
                    <asp:Label ID="LblValid" Text="�۳���Ч�ڣ�" runat="server"></asp:Label>
                </td>
                <td align="left" style="width: 586px">
                    <asp:RadioButton ID="RadValidType" runat="server" Text="ָ�����ޣ�" GroupName="ValidType"
                        Checked="true" />
                    &nbsp;<asp:TextBox ID="TxtValidNum" runat="server" Width="30px" MaxLength = "4" ></asp:TextBox>
                    <asp:DropDownList ID="DropValidUnit" runat="server">
                        <asp:ListItem Selected="True" Value="1">��</asp:ListItem>
                        <asp:ListItem Value="2">��</asp:ListItem>
                        <asp:ListItem Value="3">��</asp:ListItem>
                    </asp:DropDownList>
                    <br />
                    <asp:RadioButton ID="RadValidType2" GroupName="ValidType" runat="server" Text="��Ч�ڹ���" />����ĳ����Ա����Ч���ǡ������ڡ�ʱ������ͨ��������������Ч�����ޣ�
                </td>
            </tr>
            <tr class="tdbg">
                <td align="right" style="width: 15%;" class="tdbgleft">
                    ������۳�ԭ��</td>
                <td align="left">
                    <asp:TextBox ID="TxtReason" runat="server" Height="50px" Width="300px" TextMode="MultiLine"></asp:TextBox>
                    <pe:RequiredFieldValidator ID="ValrReason" ControlToValidate="TxtReason" runat="server"
                        ErrorMessage="�۳���Ч��ԭ����Ϊ��"></pe:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="tdbg">
                <td style="width: 15%" class="tdbgleft" align="right">
                    �ڲ���¼��</td>
                <td align="left">
                    <asp:TextBox ID="TxtMemo" runat="server" Width="400px" Columns="50" Height="60px"
                        Rows="4" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr class="tdbg">
                <td style="width: 15%" class="tdbgleft" align="right">
                </td>
                <td align="left">
                    <asp:CheckBox ID="ChkIsSendMessage" runat="server" />ͬʱ�����ֻ�����֪ͨ��Ա
                </td>
            </tr>
            <tr align="center" class="tdbg">
                <td style="height: 30px;" colspan="2">
                    <asp:Button ID="EBtnSubmit" Text="�۳���Ч��" OnClick="EBtnSubmit_Click" runat="server" />&nbsp;&nbsp;
                </td>
            </tr>
            <asp:HiddenField ID="HdnUsersId" runat="server" />
        </table>
    </div>
</asp:Content>
