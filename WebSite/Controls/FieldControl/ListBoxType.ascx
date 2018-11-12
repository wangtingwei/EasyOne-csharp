<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Controls.FieldControl.ListBoxType" Codebehind="ListBoxType.ascx.cs" %>
<tr id='Tab' runat="server" class='tdbg'>
    <td class='tdbgleft' align='right' style="width: 20%;">
        <div class="DivWordBreak">
            <strong>
                <%= FieldAlias %>
                ：&nbsp;</strong><br />
            <%= Tips %>
        </div>
    </td>
    <td class='tdbg' align='left'>
        <div class="DivWordBreak">
            <asp:PlaceHolder ID="PlhCheckBox" runat="server" Visible="false">
                <asp:CheckBoxList ID="ChkList" runat="server">
                </asp:CheckBoxList>
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="PlhRadioList" runat="server" Visible="false">
                <asp:RadioButtonList ID="RadlList" runat="server">
                </asp:RadioButtonList>
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="PlhDropList" runat="server" Visible="false">
                <asp:RadioButton ID="RadSelectDrop" runat="server" GroupName="SelectDrop" Visible="false" /><asp:DropDownList
                    ID="DrpList" runat="server">
                </asp:DropDownList><br />
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="PlhListBox" runat="server" Visible="false">
                <asp:ListBox ID="LstListBox" Width="200" Height="100" runat="server"></asp:ListBox><br />
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="PlhTextBox" runat="server" Visible="false">
                <asp:RadioButton ID="RadSelectTxt" runat="server" GroupName="SelectDrop" Visible="false"
                    Text="指定自定义值" /><pe:ExtendedLiteral HtmlEncode="false" ID="LitBr" runat="server" Visible="false"><br /></pe:ExtendedLiteral><asp:PlaceHolder
                        ID="LitNbsp" runat="server" Visible="false">&nbsp;&nbsp;&nbsp;</asp:PlaceHolder><asp:TextBox
                            ID="TxtListItem" runat="server"></asp:TextBox>
            </asp:PlaceHolder>
            <span style="color: Green">
                <%= Description %>
            </span>
        </div>
    </td>
</tr>
