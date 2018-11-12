<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Accessories.SiteOption"
    Title="网站参数配置" Codebehind="SiteOption.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="server">
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td colspan="2" class="spacingtitle">
                <strong>网站参数配置</strong>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" style="width: 45%">
                <strong>后台管理目录：</strong><br />
                为了安全，您可以在此修改后台管理目录名称，<br />
                为空时默认为Admin，实际文件夹名不需更改。<br />
                <span style="color: #FF0000">注意：目录名只能以字母、数字及下划线组成，<br />
                    且目录名不能与系统根目录下文件夹重名（除Admin外）。</span></td>
            <td>
                <asp:TextBox ID="TxtManageDir" Text="Admin" MaxLength="20" TextMode="singleLine"
                    runat="server"></asp:TextBox>
                <asp:RegularExpressionValidator ID="ValeManageDir" runat="server" ControlToValidate="TxtManageDir"
                    Display="Dynamic" ErrorMessage="目录名只能以字母、数字及下划线组成" SetFocusOnError="True" ValidationExpression="[_a-zA-Z0-9]{1,}"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="ValrManageDir" runat="server" ErrorMessage="请输入后台管理目录名"
                    ControlToValidate="TxtManageDir"></asp:RequiredFieldValidator>
                <asp:HiddenField ID="HdnManageDir" runat="server" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>管理员身份验证票过期时间：</strong><br />
                管理员登录后台后，在不操作的情况下自动保持登录状态的时间设置。<br />
                <span style="color: #FF0000">注意：如果为0则在当前浏览器保持登录状态。<br />
                    修改管理员身份验证票过期时间，必须在下次登录后台才能生效。 </span>
            </td>
            <td>
                <asp:TextBox ID="TxtTicketTime" runat="server" MaxLength="6" Columns="5"></asp:TextBox>&nbsp;分钟<pe:NumberValidator
                    ID="NumberValidator3" ControlToValidate="TxtTicketTime" runat="server"></pe:NumberValidator>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>是否启用后台管理认证码：</strong></td>
            <td>
                <asp:RadioButtonList ID="RadlEnableSiteManageCode" runat="server" RepeatDirection="Horizontal"
                    RepeatLayout="Flow">
                    <asp:ListItem Selected="True" Value="true">是</asp:ListItem>
                    <asp:ListItem Value="false">否</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>后台管理认证码：</strong><br />
                <asp:Label ID="LblNotes" runat="server" Text="该后台管理认证码还是系统默认值，为了网站安全，请及时修改！" ForeColor="red"
                    Visible="false"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="TxtSiteManageCode" runat="server" MaxLength="20"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>是否使用软键盘输入密码：</strong><br />
                若选择是，则管理员登录后台时使用软键盘输入密码，适合网吧等场所上网使用。</td>
            <td>
                <asp:RadioButtonList ID="RadlEnableSoftKey" runat="server" RepeatDirection="Horizontal"
                    RepeatLayout="Flow">
                    <asp:ListItem Value="true">是</asp:ListItem>
                    <asp:ListItem Selected="True" Value="false">否</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>是否对数据库连接字符串加密：</strong></td>
            <td>
                <asp:RadioButtonList ID="RadlConnProtecte" runat="server" RepeatDirection="Horizontal"
                    RepeatLayout="Flow">
                    <asp:ListItem Value="true">是</asp:ListItem>
                    <asp:ListItem Value="false">否</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
        <tr class="tdbg" id="EnablePointMoneyExp" runat ="server">
            <td class="tdbgleft">
                <strong>是否启用点券、金额、积分、有效期功能：</strong><br />
                <span style="color: #FF0000">注意：此功能仅限于 “CMS模块”</span>
            </td>
            <td>
                <asp:RadioButtonList ID="RadlEnablePointMoneyExp" runat="server" RepeatDirection="Horizontal"
                    RepeatLayout="Flow">
                    <asp:ListItem Value="true" Selected="True">是</asp:ListItem>
                    <asp:ListItem Value="false">否</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>网站广告目录：</strong><br />
                为了不让广告拦截软件拦截网站的广告，<br />
                您可以修改广告JS的存放目录（默认为IAA），改过以后，需要再设置此处</td>
            <td>
                <asp:TextBox ID="TxtADDir" Text="IAA" MaxLength="20" runat="server"></asp:TextBox>
                 <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TxtADDir"
                    Display="Dynamic" ErrorMessage="目录名只能以字母、数字及下划线组成" SetFocusOnError="True" ValidationExpression="[_a-zA-Z0-9]{1,}"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>网站生成目录：</strong><br />
                <span style="color: #FF0000">注意：如果生成在根目录下，请保留为空！</span>
            </td>
            <td>
                <asp:TextBox ID="TxtCreateHtmlPath" Text="html" MaxLength="20" runat="server"></asp:TextBox>
                 <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="TxtCreateHtmlPath"
                    Display="Dynamic" ErrorMessage="目录名只能以字母、数字及下划线组成" SetFocusOnError="True" ValidationExpression="[_a-zA-Z0-9]{1,}"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>链接地址方式：</strong>
            </td>
            <td>
                <asp:RadioButtonList ID="RadlUrlType" runat="server">
                    <asp:ListItem Selected="True" Value="false"></asp:ListItem>
                    <asp:ListItem Value="true"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>内嵌代码生成路径：</strong><br />
            </td>
            <td>
                <asp:TextBox ID="TxtIncludeFilePath" Text="Include" MaxLength="20" runat="server"></asp:TextBox>
             <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="TxtIncludeFilePath"
                    Display="Dynamic" ErrorMessage="目录名只能以字母、数字及下划线组成" SetFocusOnError="True" ValidationExpression="[_a-zA-Z0-9]{1,}"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>是否允许上传文件：</strong></td>
            <td>
                <asp:RadioButtonList ID="RadlEnableUploadFiles" RepeatLayout="Flow" RepeatDirection="Horizontal"
                    runat="server">
                    <asp:ListItem Text="是" Selected="True" Value="true"></asp:ListItem>
                    <asp:ListItem Text="否" Value="false"></asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>网站上传目录：</strong><br />
            </td>
            <td>
                <asp:TextBox ID="TxtUploadDir" Text="upload" MaxLength="20" runat="server"></asp:TextBox>
                 <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="TxtUploadDir"
                    Display="Dynamic" ErrorMessage="目录名只能以字母、数字及下划线组成" SetFocusOnError="True" ValidationExpression="[_a-zA-Z0-9]{1,}"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>上传文件的保存目录规则：</strong><br />
                <span style="color: Red">可用变量： </span>
                <br />
                <span style="color: Blue">{$RootDir}：一级栏目目录、{$Year}：年份<br />
                    {$NodeDir}：当前栏目目录、{$Month}：月份<br />
                    {$NodeIdentifier}：栏目标识符、{$Day}：日期<br />
                    {$ParentDir}：当前栏目的父目录、{$FileType}：文件类型</span></td>
            <td>
                <pe:ComboBox ID="TxtUploadFilePathRule" Width="300" runat="server">
                    <Items>
                        <asp:ListItem>{$FileType}/{$Year}{$Month}</asp:ListItem>
                        <asp:ListItem>{$FileType}/{$Year}/{$Month}</asp:ListItem>
                        <asp:ListItem>{$FileType}/{$NodeDir}/{$Year}/{$Month}</asp:ListItem>
                        <asp:ListItem>{$RootDir}/{$Year}/{$Month}</asp:ListItem>
                    </Items>
                </pe:ComboBox>
            </td>
        </tr>
<%--        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>允许上传的文件类型：</strong><br />
                多种文件类型之间以“|”分隔</td>
            <td>
                <asp:TextBox ID="TxtUploadFileExts" Text="gif|jpg|jpeg|jpe|bmp|png" runat="server"
                    Columns="50"></asp:TextBox>
            </td>
        </tr>--%>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>允许上传的最大文件大小：</strong></td>
            <td>
                <asp:TextBox ID="TxtUploadFileMaxSize" Text="1024" runat="server" Columns="6" MaxLength ="6"></asp:TextBox>
                KB&nbsp;&nbsp;&nbsp;&nbsp;<span style="color: Blue">提示：1 KB = 1024 Byte，1 MB = 1024
                    KB<asp:RangeValidator ID="ValgMaxFileSize" runat="server" ControlToValidate="TxtUploadFileMaxSize"
                        ErrorMessage="请输入整数" MaximumValue="2147483647" MinimumValue="0" SetFocusOnError="True"
                        Type="Integer"></asp:RangeValidator></span></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>网站模板根目录：</strong><br />
                为防止别人猜测到模板存放地址，<br />
                您可以在此输入多层目录做为网站模板的根目录，最好在取带“#”号的目录名。<br />
                目录的格式如下：#Template1/Template2/模板方案
            </td>
            <td>
                <asp:TextBox ID="TxtTemplateDir" Text="#Template" Columns="40" MaxLength="40" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>是否启用内容自动签收：</strong></td>
            <td>
                <asp:RadioButtonList ID="RadlIsAutoSignin" runat="server" RepeatDirection="Horizontal"
                    RepeatLayout="Flow">
                    <asp:ListItem Value="true">是</asp:ListItem>
                    <asp:ListItem Selected="True" Value="false">否</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>自动签收内容时间：</strong><br />
                单位：秒。启用自动签收后，此设置才起作用。
            </td>
            <td>
                <asp:TextBox ID="TxtAutoSigninTime" Text="10" MaxLength="20" runat="server"></asp:TextBox><pe:NumberValidator
                    ID="NumValTxtAutoSigninTime" ControlToValidate="TxtAutoSigninTime" runat="server"></pe:NumberValidator>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>防刷新队列长度：</strong><br />
                队列长度越长，防止用户恶意刷新提交重复表单越有效。
            </td>
            <td>
                <asp:TextBox ID="TxtRefreshQueueSize" Text="10" MaxLength="20" runat="server"></asp:TextBox><pe:NumberValidator
                    ID="NumValTxtRefreshQueueSize" ControlToValidate="TxtRefreshQueueSize" runat="server"></pe:NumberValidator>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>采集休眠时间：</strong><br />
              每采集5篇信息服务器强制休眠指定秒数，不休眠可以填写0。
            </td>
            <td>
                <asp:TextBox ID="TxtCollectionSleep" Text="0" Columns="5" MaxLength="5" runat="server"></asp:TextBox> <asp:RegularExpressionValidator ID="ValgCollectionSleep" runat="server" ControlToValidate="TxtCollectionSleep"
                    ErrorMessage="只能输入正整数" ValidationExpression="^([0-9])(\d{0,})(\d{0,})$" Display="Dynamic" />
            </td>
        </tr>      
        
        <tr class="tdbgbottom">
            <td colspan="2">
                <asp:Button ID="BtnSubmit" runat="server" Text="保存设置" OnClick="BtnSubmit_Click" />
            </td>
        </tr>
    </table>

    <script type="text/javascript">
        function AddUploadDirRules(RulesValue)
        {
            document.getElementById('<%= TxtUploadFilePathRule.ClientID %>').value = RulesValue;
        }

    </script>

</asp:Content>
