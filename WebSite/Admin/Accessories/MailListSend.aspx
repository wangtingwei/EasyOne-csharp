<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Accessories.MailListSend" Title="发送邮件列表" ValidateRequest="false" Codebehind="MailListSend.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">

    <script type="text/javascript">
        function SelectUser()
        {            
            var newwin = window.open('../User/UserNameList.aspx?TypeSelect=UserList&Default='+ escape(document.getElementById(<%="'" +TxtUserName.ClientID + "'" %>).value),'UserNameList','width=670,height=400,resizable=0,scrollbars=yes');
            newwin.focus();
        }
        
        function DoPostBack(value)
        {
            document.getElementById('<%=TxtUserName.ClientID%>').value=value;
        }
    </script>

    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr>
            <td align="center" colspan="2" class="spacingtitle">
                <b>邮件发送</b></td>
        </tr>
        <tr class="tdbg">
            <td width="15%" align="right" class="tdbgleft">
                <strong>收件人选择：</strong></td>
            <td style="text-align: left">
                <table>
                    <tr>
                        <td>
                            <asp:RadioButton ID="RadUserType0" runat="server" GroupName="UserType" Text="所有会员"
                                Checked="True" /></td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            <asp:RadioButton ID="RadUserType1" runat="server" GroupName="UserType" Text="指定会员组" /></td>
                        <td>
                            <asp:CheckBoxList ID="ChklUserGroupList" runat="server" RepeatColumns="5" RepeatDirection="Horizontal"
                                Width="100%">
                            </asp:CheckBoxList></td>
                    </tr>
                    <tr>
                        <td valign="top">
                            <asp:RadioButton ID="RadUserType2" runat="server" GroupName="UserType" Text="指定用户名" /></td>
                        <td>
                            <asp:TextBox ID="TxtUserName" runat="server" Width="260px"></asp:TextBox>
                            <span style="color: blue"><=【</span><a href="#" onclick="SelectUser();"><span style="color: green">会员列表</span></a><span
                                style="color: blue">】</span>多个用户名间请用<span style="color: blue">英文的逗号</span>分隔</td>
                    </tr>
                    <tr>
                        <td valign="top">
                            <asp:RadioButton ID="RadUserType3" runat="server" GroupName="UserType" Text="指定会员Email" /></td>
                        <td>
                            <asp:TextBox ID="TxtEmails" runat="server" Width="260px"></asp:TextBox>
                            多个Email间请用<span style="color: blue">英文的逗号</span>分隔</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="tdbg">
            <td align="right" class="tdbgleft">
                <strong>邮件主题：</strong></td>
            <td>
                <asp:TextBox ID="TxtSubject" runat="server" Width="390px"></asp:TextBox>
                <pe:RequiredFieldValidator ID="ValrSubject" runat="server" ControlToValidate="TxtSubject"
                    Display="Dynamic" ErrorMessage="邮件主题不能为空！"></pe:RequiredFieldValidator></td>
        </tr>
        <tr class="tdbg">
            <td align="right" class="tdbgleft">
                <strong>邮件内容：</strong></td>
            <td>
                <pe:PEeditor Height="300px" Width="600px" ID="EditorContent" runat="server" ToolbarSet="Simple">
                </pe:PEeditor>
                <pe:FckEditorValidator ID="FckEditorValidator1" runat="server" 
                    ControlToValidate="EditorContent" Display="Dynamic" ErrorMessage="邮件内容不能为空！"></pe:FckEditorValidator>
                </td>
        </tr>
        <tr class="tdbg">
            <td align="right" class="tdbgleft">
                <strong>发件人：</strong></td>
            <td>
                <asp:TextBox ID="TxtSenderName" runat="server" Width="350px"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg">
            <td align="right" class="tdbgleft">
                <strong>回复Email：</strong></td>
            <td>
                <asp:TextBox ID="TxtSenderEmail" runat="server" Width="350px"></asp:TextBox>
                <asp:RegularExpressionValidator ID="ValeSenderEmail" runat="server" ControlToValidate="TxtSenderEmail"
                    Display="Dynamic" ErrorMessage="回复Email不是有效的Email地址！" 
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator></td>
        </tr>
        <tr class="tdbg">
            <td align="right" class="tdbgleft">
                <strong>邮件优先级：</strong></td>
            <td>
                <asp:RadioButtonList ID="RadlPriority" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="2">高</asp:ListItem>
                    <asp:ListItem Selected="True" Value="0">普通</asp:ListItem>
                    <asp:ListItem Value="1">低</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
        <tr class="tdbgbottom">
            <td colspan="2">
                <asp:Button ID="BtnSend" runat="server" Text="发送" OnClick="BtnSend_Click" />
                &nbsp;&nbsp; &nbsp;
                <input id="Reset1" type="reset" value=" 清除 " /></td>
        </tr>
    </table>
    </asp:Content>
