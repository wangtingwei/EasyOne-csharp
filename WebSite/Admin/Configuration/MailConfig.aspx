<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Accessories.MailConfig"
    Title="邮件参数配置" Codebehind="MailConfig.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        <Services>
            <asp:ServiceReference Path="~/WebServices/MailWebService.asmx" />
        </Services>
    </asp:ScriptManager>

    <script language="javascript" type="text/javascript">
    function GetMailConfig()
    {
        if($get("<%= CollocateCheckBox.ClientID %>").checked)
        {
            if($get("<%= TxtMailFrom.ClientID %>").value != "")
            {
                EasyOne.WebSite.Admin.Accessories.MailWebService.GetMailConfig($get("<%= TxtMailFrom.ClientID %>").value, onMailConfig);
            }
        }
    }
    
    function onMailConfig(values)
    {
        if(values !=null && values.MailServer !=null)
        {
            $get("<%= TxtMailServer.ClientID %>").value = values.MailServer;
            $get("<%= TxtPort.ClientID %>").value = values.Port;
            $get("<%= ChkSsl.ClientID %>").checked = values.EnabledSsl;
            switch(values.AuthenticationType)
            {
                case 1:
                    $get("<%= RadBasic.ClientID %>").checked=true;
                    $get("<%= TxtMailServerUserName.ClientID %>").value = values.MailServerUserName;
                    $get("<%= TxtMailServerPassWord.ClientID %>").value = "";
                    break;
                case 2:
                    $get("<%= RadNTLM.ClientID %>").checked=true;
                    break;
                default:
                    $get("<%= RadNone.ClientID %>").checked=true;
                    break;
            }
        }
        else
        {
            Revert();
        }
        SelectCredential();
    }
    
    function Revert()
    {
        $get("<%= ChkSsl.ClientID %>").checked = false;
        $get("<%= TxtMailServer.ClientID %>").value = "";
        $get("<%= TxtPort.ClientID %>").value = "25";
        $get("<%= TxtMailServerUserName.ClientID %>").value = "";
        $get("<%= TxtMailServerPassWord.ClientID %>").value = "";
    }
    
    function SelectCredential()
    {
        var username = document.getElementById("<%=TxtMailServerUserName.ClientID %>");
        var password = document.getElementById("<%=TxtMailServerPassWord.ClientID %>");
        var radBasic = document.getElementById("<%=RadBasic.ClientID %>");
        var pnlbasic = document.getElementById("<%=PalBasic.ClientID %>");
        pnlbasic.disabled = username.disabled = password.disabled = !radBasic.checked;
    }
    
    </script>

    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td colspan="2" class="spacingtitle">
                <strong>邮件参数配置</strong>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" style="width: 200px;">
                <strong>发送人邮箱:<br />
                </strong>例：someone@EasyOne.net</td>
            <td>
                <asp:TextBox ID="TxtMailFrom" runat="server" Width="200px" ValidationGroup="Config"></asp:TextBox>
                <asp:CheckBox ID="CollocateCheckBox" runat="server" Checked="True" Text="自动配置邮件服务器信息" />
                <pe:EmailValidator ID="EmailValidator1" ControlToValidate="TxtMailFrom" ErrorMessage="错误的电子邮件格式！"
                    runat="server" Display="Dynamic" ValidationGroup="Config"></pe:EmailValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxtMailFrom"
                    Display="Dynamic" ErrorMessage="不能为空！" ValidationGroup="Config"></asp:RequiredFieldValidator></td>
        </tr>
        <tr class="tdbg">
            <td style="width: 30%" class="tdbgleft">
                <strong>发送邮件服务器(SMTP)：</strong><br />
                用来发送邮件的SMTP服务器，如果你不清楚此参数含义，请联系你的空间商
            </td>
            <td>
                <asp:TextBox ID="TxtMailServer" runat="server" Width="200px" ValidationGroup="Config"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtMailServer"
                    Display="Dynamic" ErrorMessage="不能为空！" ValidationGroup="Config"></asp:RequiredFieldValidator></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>端口号：</strong><br />
                端口号必需是非负整正数，默认是25端口</td>
            <td>
                <asp:TextBox ID="TxtPort" runat="server" Width="200px" ValidationGroup="Config">25</asp:TextBox>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="TxtPort"
                    Display="Dynamic" ErrorMessage="端口号必需是非负整正数！" Operator="GreaterThanEqual" Type="Integer"
                    ValueToCompare="0" ValidationGroup="Config"></asp:CompareValidator></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>此服务器要求安全连接(SSL)：</strong></td>
            <td>
                <asp:CheckBox ID="ChkSsl" runat="server" /></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" colspan="2" style="width: 100%; text-align: center; padding: 5px">
                <br />
                <fieldset style="width: 95%;" class="tdbgleft">
                    <legend>身份验证</legend>
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; margin-top: 10px;
                        margin-bottom: 10px;">
                        <tr>
                            <td style="width: 3%">
                                <input id="RadNone" name="RadCredential" type="radio" runat="server" onclick="SelectCredential()" /></td>
                            <td style="text-align: left">
                                无</td>
                        </tr>
                        <tr>
                            <td>
                                <input id="RadBasic" name="RadCredential" type="radio" runat="server" onclick="SelectCredential()" /></td>
                            <td style="text-align: left">
                                基本</td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td style="text-align: left">
                                如果您的电子邮件服务器要求在发送电子邮件时显式传入用户名和密码，请选择此选项。</td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td style="text-align: left" id="TD1">
                                <asp:Panel ID="PalBasic" runat="server" Width="100%" Enabled="False">
                                    发件人的用户名:<asp:TextBox ID="TxtMailServerUserName" runat="server" Columns="30"></asp:TextBox><br />
                                    发件人的密 &nbsp;码:<asp:TextBox ID="TxtMailServerPassWord" TextMode="password" runat="server"
                                        Columns="30"></asp:TextBox></asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <input id="RadNTLM" name="RadCredential" type="radio" runat="server" onclick="SelectCredential()" /></td>
                            <td style="text-align: left">
                                NTLM (Windows 身份验证)</td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td style="text-align: left">
                                如果您的电子邮件服务器位于局域网上，并且您使用 Windows 凭据连接到该服务器，请选择此选项。</td>
                        </tr>
                    </table>
                </fieldset>
                <br />
                <br />
            </td>
        </tr>
        <tr class="tdbgbottom">
            <td colspan="2">
                <asp:Button ID="BtnSubmit" runat="server" Text="保存设置" OnClick="BtnSubmit_Click" ValidationGroup="Config" />
            </td>
        </tr>
    </table>
    <p />
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td colspan="2" class="spacingtitle">
                测试邮件配置</td>
        </tr>
        <tr class="tdbg">
            <td style="width: 30%; height: 48px; text-align: left" class="tdbgleft">
                <strong>&nbsp; &nbsp; &nbsp; EMAIL：</strong></td>
            <td>
                <asp:TextBox ID="TxtTestMail" runat="server" Width="300px" ValidationGroup="Test"></asp:TextBox>
                <asp:Button ID="BtnTest" runat="server" Text="发送测试邮件" ValidationGroup="Test" OnClick="BtnTest_Click" />
                <pe:EmailValidator ID="EmailValidator2" runat="server" ControlToValidate="TxtTestMail"
                    Display="Dynamic" ErrorMessage="错误的电子邮件格式！" ValidationGroup="Test"></pe:EmailValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TxtTestMail"
                    Display="Dynamic" ErrorMessage="不能为空！" ValidationGroup="Test"></asp:RequiredFieldValidator></td>
        </tr>
    </table>
</asp:Content>
