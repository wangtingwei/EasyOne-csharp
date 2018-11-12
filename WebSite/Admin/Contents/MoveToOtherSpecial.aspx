<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Contents.MoveToOtherSpecial"
    Title="批量移动专题信息" Codebehind="MoveToOtherSpecial.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="border">
        <tr class="tdbg">
            <td class="spacingtitle" colspan="3" align="center">
                批量移动专题信息
            </td>
        </tr>
        <tr class="tdbg">
            <td align="center">
                <table>
                    <tr align="left">
                        <td>
                            <asp:RadioButton ID="RadSpecialInfoId0" Checked="true" GroupName="RadSpecialInfoId"
                                runat="server" /><strong>指定专题信息ID：</strong></td>
                        <td>
                            <asp:TextBox ID="TxtGeneralId" runat="server" Width="214px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr align="left">
                        <td>
                            <asp:RadioButton ID="RadSpecialInfoId1" GroupName="RadSpecialInfoId" runat="server" /><strong>指定专题的信息：</strong></td>
                        <td>
                            <asp:ListBox ID="LstSpecial" runat="server" DataTextField="SpecialName" DataValueField="SpecialId"
                                SelectionMode="Multiple" Height="355px" Width="272px"></asp:ListBox><br />
                            <input id="Button6" onclick="UnSelectAll()" type="button" class="inputbutton" value="取消选定所有专题" />
                            <input id="Button5" onclick="SelectAll()" type="button" class="inputbutton" value="  选定所有专题  " />
                        </td>
                    </tr>
                </table>
            </td>
            <td valign="middle" class="tdbgleft">
                <strong>移动到>></strong>
            </td>
            <td valign="top">
                <table>
                    <tr>
                        <td>
                            <strong>目标专题：</strong></td>
                        <td>
                            <asp:ListBox ID="LstTargetSpecial" runat="server" DataTextField="SpecialName" DataValueField="SpecialId"
                                SelectionMode="Single" Height="371px" Width="281px"></asp:ListBox></td>
                    </tr>
                </table>
            </td>
        </tr>
      <tr class="tdbg">
            <td align="center" colspan="3">
                <asp:Button ID="EBtnBacthSet" Text="执行批处理" OnClick="EBtnBacthSet_Click" runat="server" />&nbsp;&nbsp;
                <asp:Button ID="BtnCancel" runat="server" Text="取消" OnClick="BtnCancel_Click" /></td>
        </tr>
    </table>
    <asp:HiddenField ID="HdnSpecialId" runat="server" />
</asp:Content>
