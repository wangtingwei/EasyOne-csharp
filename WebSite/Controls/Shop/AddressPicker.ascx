<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Controls.Shop.AddressPicker" Codebehind="AddressPicker.ascx.cs" %>
<asp:UpdatePanel ID="UpnlAddress" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div class="border">
            <table border="0" cellpadding="2" cellspacing="1" style="background: #ffffff" width="100%">
                <tr class="tdbg">
                    <td align="right" class="tdbgleft" style="width: 100px">
                        ���ң�
                    </td>
                    <td align="left" colspan="2">
                        <asp:DropDownList ID="DropCountry" runat="server" AutoPostBack="true" DataTextField="Country"
                            DataValueField="Country" OnSelectedIndexChanged="DropCountry_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="DropCountry"
                            Display="Dynamic" ErrorMessage="��ѡ�����" InitialValue="-1" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                </tr>
                <tr class="tdbg">
                    <td align="right" class="tdbgleft">
                        ʡ�ݣ�
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="DropProvince" runat="server" AutoPostBack="true" DataTextField="Province"
                            DataValueField="Province" OnSelectedIndexChanged="DropProvince_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr class="tdbg">
                    <td align="right" class="tdbgleft">
                        ���У�
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="DropCity" runat="server" AutoPostBack="true" DataTextField="City"
                            DataValueField="City" OnSelectedIndexChanged="DropCity_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr class="tdbg">
                    <td align="right" class="tdbgleft">
                        ����(��)��
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="DropArea" runat="server" AutoPostBack="true" DataTextField="Area"
                            DataValueField="Area" OnSelectedIndexChanged="DropArea_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr class="tdbg">
                    <td align="right" class="tdbgleft">
                        �ֵ���ַ��
                    </td>
                    <td align="left">
                        <asp:TextBox ID="TxtAddress" runat="server" Width="300px"></asp:TextBox><br />
                        (�����ظ���д����/ʡ/��/��)<pe:RequiredFieldValidator
                            ID="ValrAddress" runat="server" ControlToValidate="TxtAddress" Display="dynamic"
                            ErrorMessage="������ֵ���ַ��" SetFocusOnError="true" RequiredText="*" ShowRequiredText="False"></pe:RequiredFieldValidator>
                    </td>
                </tr>
            </table>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<asp:HiddenField ID="HdnZipCode" runat="server" />
