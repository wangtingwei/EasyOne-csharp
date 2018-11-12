<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Contents.SpecialBatchSet"
    Title="批量设置专题属性" Codebehind="SpecialBatchSet.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="border">
        <tr>
            <td class="spacingtitle" colspan="2" align="center">
                批量设置专题属性
            </td>
        </tr>
        <tr>
            <td class="tdbg" style="width: 231" valign="top">
                <table>
                    <tr class="tdbg">
                        <td>
                            <span style="color: Red">提示：</span>可以按住“Shift”<br />
                            或“Ctrl”键进行多个专题的选择
                        </td>
                    </tr>
                    <tr class="tdbg">
                        <td>
                            <asp:ListBox ID="LstSpecial" runat="server" DataTextField="SpecialName" DataValueField="SpecialId"
                                SelectionMode="Multiple" Height="282px" Width="231px"></asp:ListBox>
                        </td>
                    </tr>
                    <tr class="tdbg">
                        <td>
                            <input id="BtnSelectAll" onclick="SelectAll()" type="button" class="inputbutton"
                                value="  选定所有专题  " />
                            <input id="BtnCancelAll" onclick="UnSelectAll()" type="button" class="inputbutton"
                                value="取消选定所有专题" />
                        </td>
                    </tr>
                </table>
            </td>
            <td class="tdbg" valign="top" style="width: 539px">
                <table width="100%" cellpadding="2" cellspacing="1">
                    <%--专题选项--%>
                    <tbody id="Tabs0">
                        <tr class="tdbg">
                            <td class="tdbg">
                                <asp:CheckBox ID="ChkOpenType" Checked="true" runat="server" />
                            </td>
                            <td style="width: 300" class="tdbg">
                                <strong>打开方式：</strong>
                            </td>
                            <td>
                                <asp:RadioButtonList ID="RadlOpenType" runat="server">
                                    <asp:ListItem Value="0" Selected="true">在原窗口打开</asp:ListItem>
                                    <asp:ListItem Value="1">在新窗口打开</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td class="tdbg">
                                <asp:CheckBox ID="ChkIsElite" Checked="true" runat="server" />
                            </td>
                            <td style="width: 300" class="tdbg">
                                <strong>是否推荐：</strong>
                            </td>
                            <td>
                                <asp:RadioButtonList ID="RadlIsElite" runat="server" Height="3px" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="True" Selected="True">是</asp:ListItem>
                                    <asp:ListItem Value="False">否</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td class="tdbg">
                                <asp:CheckBox ID="ChKSpecialTemplatePath" Checked="true" runat="server" />
                            </td>
                            <td style="width: 300" class="tdbg">
                                <strong>专题页模板：</strong>
                            </td>
                            <td>
                                <pe:TemplateSelectControl ID="FileCSpecialTemplatePath" Width="300px" runat="server"></pe:TemplateSelectControl>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </td>
        </tr>
        <tr align="center">
            <td class="tdbg" id="commonfooter" colspan="2">
                <asp:Button ID="EBtnBacthSet" Text="执行批处理" OnClick="EBtnBacthSet_Click" runat="server" />&nbsp;&nbsp;
                <asp:Button ID="BtnCancel" runat="server" Text="取消" OnClick="BtnCancel_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
