<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Contents.AddContentToSpecial"
    Title="添加内容到专题" Codebehind="AddContentToSpecial.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="border">
        <tr class="tdbg" align="center">
            <td colspan="2" class="spacingtitle">
                添加内容到专题
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>选定的内容ID：</strong>
            </td>
            <td>
                <asp:TextBox ID="TxtGeneralId" runat="server" Width="230px"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>添加到目标专题：</strong>
                <br />
                <span style="color: Red">提示：</span>可以按住“Shift”<br />
                或“Ctrl”键进行多个专题的选择</td>
            <td>
                <asp:ListBox ID="LstSpecial" runat="server" SelectionMode="Multiple" Height="282px"
                    Width="235px"></asp:ListBox><br />
                <br />
                <input id="Button1" onclick="SelectAll()" type="button" class="inputbutton" value="  选定所有专题  " />
                <input id="Button2" onclick="UnSelectAll()" type="button" class="inputbutton" value="取消选定所有专题" />
            </td>
        </tr>
        <tr class="tdbg">
            <td align="center" colspan="2">
                <asp:Button ID="EBtnBacthSet" Text="执行批处理" OnClick="EBtnBacthSet_Click" runat="server" />&nbsp;&nbsp;
                <asp:Button ID="BtnCancel" runat="server" Text="取消" OnClick="BtnCancel_Click" /></td>
        </tr>
    </table>
</asp:Content>
