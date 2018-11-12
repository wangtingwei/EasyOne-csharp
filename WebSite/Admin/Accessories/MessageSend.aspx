<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPage.master"
    Inherits="EasyOne.WebSite.Admin.Accessories.MessageSend"
    ValidateRequest="false" Codebehind="MessageSend.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">

    <script type="text/javascript">
        function SelectUser()
        {
            window.open('../User/UserList.aspx?TypeSelect=UserList&OpenerText=<%=TxtUserName.ClientID %>','','width=600,height=450,resizable=0,scrollbars=yes');

        }
    </script>

    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td colspan="2" class="spacingtitle">
                <b>发送网站短消息</b>
            </td>
        </tr>
        <tr class="tdbg">
            <td align="right" style="height: 28px; width: 15%;">
                &nbsp;收件人：</td>
            <td>
                <table id="TblAddMessage" visible="true" runat="server">
                    <tr>
                        <td>
                            <asp:RadioButton ID="RadIncept1" runat="server" GroupName="InceptGroup" Checked="true" />所有会员
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            <asp:RadioButton ID="RadIncept2" runat="server" GroupName="InceptGroup" />指定会员组
                        </td>
                        <td>
                            <asp:CheckBoxList ID="ChklUserGroupList" runat="server" RepeatColumns="5" RepeatDirection="Horizontal"
                                Width="100%">
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButton ID="RadIncept3" runat="server" GroupName="InceptGroup" />指定用户名
                        </td>
                        <td>
                            <asp:TextBox ID="TxtUserName" runat="server" Width="260px"></asp:TextBox>
                            <span style="color: #0000ff">&lt;=【</span><a href="#" onclick="SelectUser();"> <span
                                style="text-decoration: underline; color: Green;">会员列表</span></a><span style="color: #0000ff">】</span>
                        </td>
                    </tr>
                </table>
                <table id="TblEditMessage" visible="false" runat="server">
                    <tr>
                        <td>
                            <asp:TextBox ID="TxtInceptUser" runat="server" Width="426px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="tdbg">
            <td align="right" style="height: 27px; width: 15%;">
                发送人：</td>
            <td>
                <asp:TextBox ID="TxtSender" runat="server"></asp:TextBox>
                <pe:RequiredFieldValidator ID="ValrSender" runat="server" ControlToValidate="TxtSender"
                    ErrorMessage="短消息发送人不能为空"></pe:RequiredFieldValidator></td>
        </tr>
        <tr class="tdbg">
            <td align="right" style="height: 28px; width: 15%;">
                短消息主题：</td>
            <td>
                <asp:TextBox ID="TxtTitle" runat="server" Width="300px"></asp:TextBox>
                <pe:RequiredFieldValidator ID="ValrTitle" runat="server" ControlToValidate="TxtTitle"
                    ErrorMessage="短消息主题不能为空" Display="Dynamic"></pe:RequiredFieldValidator>
            </td>
        </tr>
        <tr class="tdbg">
            <td align="right" style="height: 23px; width: 15%;">
                短消息内容：</td>
            <td>
                <pe:PEeditor ID="EditorContent" runat="server" Width="580px" Height="300px" IsUpload = "true" ToolbarSet ="Simple">
                </pe:PEeditor>
                <pe:FckEditorValidator ID="ValrContent" runat="server" ControlToValidate="EditorContent"
                    ErrorMessage="短消息内容不能为空" Display="Dynamic"></pe:FckEditorValidator>
            </td>
        </tr>
        <tr align="center" class="tdbg">
            <td colspan="2" style="height: 50px;">
                <asp:HiddenField ID="HdnMessageID" runat="server" />
                <asp:Button ID="BtnSend" runat="server" Text="发送" OnClick="BtnSend_Click" />
                <asp:Button ID="BtnSave" runat="server" Text="保存" OnClick="BtnSave_Click" />
                <asp:Button ID="BtnReset" runat="server" Text="清除" OnClick="BtnReset_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
