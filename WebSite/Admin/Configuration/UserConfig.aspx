<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPage.master"
    ValidateRequest="false" EnableEventValidation="false"
    Inherits="EasyOne.WebSite.Admin.User.UserConfig" Title="用户参数配置" Codebehind="UserConfig.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td class="spacingtitle" colspan="2">
                <strong>用户参数配置</strong>
            </td>
        </tr>
        <tr class="tdbg">
            <td style="width: 40%;" class="tdbgleft">
                <strong>是否开启会员注册功能：</strong></td>
            <td>
                <asp:RadioButtonList ID="RadlEnableUserReg" runat="server" RepeatDirection="Horizontal"
                    RepeatLayout="Flow">
                    <asp:ListItem Selected="True" Value="true">是</asp:ListItem>
                    <asp:ListItem Value="false">否</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>是否允许一个Email注册多个会员：</strong></td>
            <td>
                <asp:RadioButtonList ID="RadlEnableMultiRegPerEmail" runat="server" RepeatDirection="Horizontal"
                    RepeatLayout="Flow">
                    <asp:ListItem Value="true">是</asp:ListItem>
                    <asp:ListItem Selected="True" Value="false">否</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>新会员注册时用户名最少字符数：</strong>
            </td>
            <td>
                <asp:TextBox ID="TxtUserNameLimit" Text="4" runat="server" Columns="5" MaxLength="3" />个字符
                <pe:RequiredFieldValidator ID="ReqTxtUserNameLimit" runat="server" Display="Dynamic"
                    ControlToValidate="TxtUserNameLimit" ErrorMessage="最少字符数不能为空" />
                <asp:CompareValidator ID="CValTxtUserNameLimit" runat="server" ControlToValidate="TxtUserNameLimit"
                    ValueToCompare="1" Type="Integer" Operator="GreaterThanEqual" ErrorMessage="最少字符数必须大于等于1"
                    Display="Dynamic" SetFocusOnError="true" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>新会员注册时用户名最多字符数：</strong>
            </td>
            <td>
                <asp:TextBox ID="TxtUserNameMax" Text="20" runat="server" Columns="5" MaxLength="3" />个字符
                <pe:RequiredFieldValidator ID="ReqTxtUserNameMax" runat="server" Display="Dynamic"
                    ErrorMessage="最多字符数不能为空" ControlToValidate="TxtUserNameMax" />
                <asp:CompareValidator ID="CValTxtUserNameMax" runat="server" ControlToValidate="TxtUserNameMax"
                    ControlToCompare="TxtUserNameLimit" Type="Integer" Operator="GreaterThanEqual"
                    ErrorMessage="最多字符数必须大于等于最小字符数" Display="Dynamic" SetFocusOnError="true" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>禁止注册的用户名：</strong><br />
                在右边指定的用户名将被禁止注册，每个用户名请用“|”符号分隔
            </td>
            <td>
                <asp:TextBox ID="TxtUserName_RegDisabled" Text="" Height="60" TextMode="MultiLine"
                    runat="server" Columns="60" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>会员注册时的必填、选填项目：</strong><br />
                在会员注册时，可根据需要设定注册的必填项、选填项。<br />
                将“可用项”中内容添加到“必填项”或者“选填项”的列表中即可设定。<br />
                用户名、 密码、 确认密码、 密码问题、 问题答案、 Email为系统强制必填信息。<br />
                <span style="color: Blue">注：若修改此项，前台正在注册的表单页面将失效</span>
            </td>
            <td style="width: 60%">
                <table>
                    <tr>
                        <td>
                            <asp:HiddenField ID="HdnRegFields_MustFill" runat="server" />
                            <asp:HiddenField ID="HdnRegFields_SelectFill" runat="server" />
                            可用项：<br />
                            <asp:ListBox ID="LitRegFields" SelectionMode="Multiple" Width="130" Height="285"
                                runat="server" /></td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <input id="Button4" value=" >> " onclick="AddFields_MustFill()" title="添加所选项" type="button" /><br />
                                        <input id="Button2" value=" << " onclick="RemoveFields_MustFill()" title="移除所选项"
                                            type="button" />
                                    </td>
                                    <td>
                                        必填项：<br />
                                        <asp:ListBox ID="LitRegFields_MustFill" SelectionMode="Multiple" Width="130" Height="130"
                                            runat="server" />
                                    </td>
                                    <td>
                                        <input id="Button5" value=" ︽ " onclick="UpFields_MustFill()" title="上移" type="button" /><br />
                                        <input id="Button6" value=" ︾ " onclick="DownFields_MustFill()" title="下移" type="button" />
                                        <br />
                                        <br />
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <input id="Button1" value=" >> " onclick="AddFields_SelectFill()" title="添加所选项" type="button" /><br />
                                        <input id="Button3" value=" << " onclick="RemoveFields_SelectFill()" title="移除所选项"
                                            type="button" />
                                    </td>
                                    <td>
                                        选填项：<br />
                                        <asp:ListBox ID="LitRegFields_SelectFill" SelectionMode="Multiple" Width="130" Height="130"
                                            runat="server" />
                                    </td>
                                    <td>
                                        <input id="Button7" value=" ︽ " onclick="UpFields_SelectFill()" title="上移" type="button" /><br />
                                        <input id="Button8" value=" ︾ " onclick="DownFields_SelectFill()" title="下移" type="button" />
                                        <br />
                                        <br />
                                        <br />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>是否启用注册企业功能：</strong><br />
                若选择“是”，则会员注册后会同时提示注册一个企业。</td>
            <td>
                <asp:RadioButtonList ID="RadlEnableRegCompany" runat="server" RepeatDirection="Horizontal"
                    RepeatLayout="Flow">
                    <asp:ListItem Selected="True" Value="true">是</asp:ListItem>
                    <asp:ListItem Value="false">否</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>会员注册时是否启用验证码功能：</strong><br />
                启用验证码功能可以在一定程度上防止暴力营销软件或注册机自动注册。</td>
            <td>
                <asp:RadioButtonList ID="RadlEnableCheckCodeOfReg" runat="server" RepeatDirection="Horizontal"
                    RepeatLayout="Flow">
                    <asp:ListItem Value="true">是</asp:ListItem>
                    <asp:ListItem Selected="True" Value="false">否</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>会员注册时是否启用回答问题验证功能：</strong><br />
                启用此功能，可以最大程度上防止暴力营销软件或注册机自动注册，也可以用于某些特殊场合，防止无关人员注册会员。</td>
            <td>
                <asp:RadioButtonList ID="RadlEnableQAofReg" runat="server" RepeatDirection="Horizontal"
                    RepeatLayout="Flow">
                    <asp:ListItem Value="true">是</asp:ListItem>
                    <asp:ListItem Selected="True" Value="false">否</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>问题一：</strong><br />
                如果启用验证功能，则问题一和答案必须填写。
            </td>
            <td>
                问题：<asp:TextBox ID="TxtRegQuestion1" Text="问题一" runat="server" Width="267px" /><br />
                答案：<asp:TextBox ID="TxtRegAnswer1" Text="答案一" runat="server" Width="267px" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>问题二：</strong><br />
                如果启用验证功能，则问题二和答案必须填写。
            </td>
            <td>
                问题：<asp:TextBox ID="TxtRegQuestion2" Text="问题二" runat="server" Width="267px" /><br />
                答案：<asp:TextBox ID="TxtRegAnswer2" Text="答案二" runat="server" Width="267px" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>问题三：</strong><br />
                如果启用验证功能，则问题三和答案必须填写。
            </td>
            <td>
                问题：<asp:TextBox ID="TxtRegQuestion3" Text="问题三" runat="server" Width="267px" /><br />
                答案：<asp:TextBox ID="TxtRegAnswer3" Text="答案三" runat="server" Width="267px" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>新会员注册是否需要管理员认证：</strong><br />
                若选择“是”，则会员必须在通过管理员认证后才能真正成为正式注册会员。</td>
            <td>
                <asp:RadioButtonList ID="RadlAdminCheckReg" runat="server" RepeatDirection="Horizontal"
                    RepeatLayout="Flow">
                    <asp:ListItem Value="true">是</asp:ListItem>
                    <asp:ListItem Selected="True" Value="false">否</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>新会员注册是否需要邮件验证： </strong>
                <br />
                若选择“是”，则会员注册后系统会发一封带有验证码的邮件给此会员，会员必须在通过邮件验证后才能真正成为正式注册会员。</td>
            <td>
                <asp:RadioButtonList ID="RadlEmailCheckReg" runat="server" RepeatDirection="Horizontal"
                    RepeatLayout="Flow">
                    <asp:ListItem Value="true">是</asp:ListItem>
                    <asp:ListItem Selected="True" Value="false">否</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>新会员注册时发送的验证邮件内容：</strong><br />
                邮件内容支持HTML，邮件内容中可用标签说明如下：<br />
                <span style="cursor: hand;" onclick="Insert('{$CheckNum}')">{$CheckNum}</span>：验证码<br />
                <span style="cursor: hand;" onclick="Insert('{$CheckUrl}')">{$CheckUrl}</span>：验证地址</td>
            <td>
                <asp:TextBox ID="TxtEmailOfRegCheck" TextMode="MultiLine" runat="server" Height="80px"
                    Width="400px" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>新会员注册成功后所属会员组：</strong></td>
            <td>
                <asp:DropDownList ID="RadlUserGroup" DataTextField="GroupName" DataValueField="GroupId"
                    runat="server" />
            </td>
        </tr>
        <tr class="tdbg" runat="server" id="PresentExp">
            <td class="tdbgleft">
                <strong>新会员注册时赠送的积分：</strong>
            </td>
            <td>
                <asp:TextBox ID="TxtPresentExp" Text="0" runat="server" Columns="7" MaxLength="7" />分积分
                <asp:RegularExpressionValidator ID="ValgPresentExp" runat="server" ControlToValidate="TxtPresentExp"
                    ErrorMessage="只能输入正整数" ValidationExpression="^([0-9])(\d{0,})(\d{0,})$" Display="Dynamic" />
            </td>
        </tr>
        <tr class="tdbg" runat="server" id="PresentMoney">
            <td class="tdbgleft">
                <strong>新会员注册时赠送的金钱：</strong>
            </td>
            <td>
                <asp:TextBox ID="TxtPresentMoney" Text="0" runat="server" Columns="7" MaxLength="7" />元钱
                <asp:RegularExpressionValidator ID="ValgTxtPresentMoney" runat="server" ControlToValidate="TxtPresentMoney"
                    ErrorMessage="只能输入货币字符，并且不能为负数" ValidationExpression="^[0-9]+(\.?[0-9]{1,4})?" Display="Dynamic" />
            </td>
        </tr>
        <tr class="tdbg" runat="server" id="PresentPoint">
            <td class="tdbgleft">
                <strong>新会员注册时赠送的<pe:ShowPointName ID="ShowPointName1" runat="server" />：</strong>
            </td>
            <td>
                <asp:TextBox ID="TxtPresentPoint" Text="0" runat="server" Columns="7" MaxLength="7" /><pe:ShowPointName ID="ShowPointName2" runat="server" PointType ="PointUnit"/><pe:ShowPointName ID="ShowPointName6" runat="server" />
                <asp:RegularExpressionValidator ID="ValgPresentPoint" runat="server" ControlToValidate="TxtPresentPoint"
                    ErrorMessage="只能输入正整数" ValidationExpression="^([0-9])(\d{0,})(\d{0,})$" Display="Dynamic" />
            </td>
        </tr>
        <tr class="tdbg" runat="server" id="PresentValidNum">
            <td class="tdbgleft">
                <strong>新会员注册时赠送的有效期：</strong>
            </td>
            <td>
                <asp:TextBox ID="TxtPresentValidNum" Text="0" runat="server" Columns="5" /><asp:DropDownList
                    ID="DropPresentValidUnit" runat="server">
                    <asp:ListItem Value="1">天</asp:ListItem>
                    <asp:ListItem Value="2">月</asp:ListItem>
                    <asp:ListItem Value="3">年</asp:ListItem>
                </asp:DropDownList>（为－1表示无限期）
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>会员找回密码的方式：</strong>
            </td>
            <td>
                <asp:RadioButtonList ID="RadlGetPasswordType" runat="server">
                    <asp:ListItem Value="0">回答正确密码答案后，直接在页面修改密码</asp:ListItem>
                    <asp:ListItem Value="1" Selected="True">回答正确密码答案后，发送邮件到会员邮箱（必须在网站信息配置配置邮件服务器与会员注册时填写了邮件地址！）</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>会员登录时是否启用验证码功能：</strong><br />
                启用验证码功能可以在一定程度上防止会员密码被暴力破解</td>
            <td>
                <asp:RadioButtonList ID="RadlEnableCheckCodeOfLogin" runat="server" RepeatDirection="Horizontal"
                    RepeatLayout="Flow">
                    <asp:ListItem Selected="True" Value="true">是</asp:ListItem>
                    <asp:ListItem Value="false">否</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>会员登录时是否允许多人同时使用同一会员帐号：</strong></td>
            <td>
                <asp:RadioButtonList ID="RadlEnableMultiLogin" runat="server" RepeatDirection="Horizontal"
                    RepeatLayout="Flow">
                    <asp:ListItem Selected="True" Value="true">是</asp:ListItem>
                    <asp:ListItem Value="false">否</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr class="tdbg" runat="server" id="PresentExpPerLogin">
            <td class="tdbgleft">
                <strong>会员每登录一次奖励的积分：</strong><br />
                一天只计算一次
            </td>
            <td>
                <asp:TextBox ID="TxtPresentExpPerLogin" Text="0" runat="server" Columns="5" />分积分
            </td>
        </tr>
        <tr class="tdbg" runat="server" id="MoneyExchangePoint">
            <td class="tdbgleft">
                <strong>会员的资金与<pe:ShowPointName ID="ShowPointName3" runat="server" />的兑换比率：</strong>
            </td>
            <td>
                每
                <asp:TextBox ID="TxtMoneyExchangePoint" Text="0" runat="server" Columns="7" MaxLength="7" />
                元钱可兑换 <strong><asp:TextBox ID="TxtCMoneyExchangePoint" Text="1" runat="server" Columns="7" MaxLength="7" /></strong> <pe:ShowPointName ID="ShowPointName10" runat="server" PointType ="PointUnit" /><pe:ShowPointName ID="ShowPointName4" runat="server" />
                <asp:RegularExpressionValidator ID="ValeMoneyExchangePoint" runat="server" ControlToValidate="TxtMoneyExchangePoint"
                    ErrorMessage="只能输入货币字符，并且不能为零和负数" ValidationExpression="^[0-9]+(\.?[0-9]{1,4})?" Display="Dynamic" />
                <asp:RegularExpressionValidator ID="ValeCMoneyExchangePoint" runat="server" ControlToValidate="TxtCMoneyExchangePoint"
                    ErrorMessage="只能输入大于零的正整数" ValidationExpression="^[1-9]+(\.?[0-9]{1,4})?" Display="Dynamic" />
            </td>
        </tr>
        <tr class="tdbg" runat="server" id="MoneyExchangeValidDay">
            <td class="tdbgleft">
                <strong>会员的资金与有效期的兑换比率：</strong>
            </td>
            <td>
                每
                <asp:TextBox ID="TxtMoneyExchangeValidDay" Text="0" runat="server" Columns="7" MaxLength="7" />
                元钱可兑换 <strong><asp:TextBox ID="TxtCMoneyExchangeValidDay" Text="1" runat="server" Columns="7" MaxLength="7" /></strong> 天有效期
                <asp:RegularExpressionValidator ID="ValeMoneyExchangeValidDay" runat="server" ControlToValidate="TxtMoneyExchangeValidDay"
                    ErrorMessage="只能输入货币字符，并且不能为负数" ValidationExpression="^[0-9]+(\.?[0-9]{1,4})?" Display="Dynamic" />
                <asp:RegularExpressionValidator ID="ValeCMoneyExchangeValidDay" runat="server" ControlToValidate="TxtCMoneyExchangeValidDay"
                    ErrorMessage="只能输入大于零的正整数" ValidationExpression="^[1-9]+(\.?[0-9]{1,4})?" Display="Dynamic" />
            </td>
        </tr>
        <tr class="tdbg" runat="server" id="UserExpExchangePoint">
            <td class="tdbgleft">
                <strong>会员的积分与<pe:ShowPointName ID="ShowPointName5" runat="server" />的兑换比率：</strong>
            </td>
            <td>
                每
                <asp:TextBox ID="TxtUserExpExchangePoint" Text="0" runat="server" Columns="7" MaxLength="7" />
                分积分可兑换 <strong><asp:TextBox ID="TxtCUserExpExchangePoint" Text="1" runat="server" Columns="7" MaxLength="7" /></strong> <pe:ShowPointName ID="ShowPointName11" runat="server" PointType ="PointUnit" /><pe:ShowPointName ID="ShowPointName7" runat="server" />
               <asp:RegularExpressionValidator ID="ValgUserExpExchangePoint" runat="server" ControlToValidate="TxtUserExpExchangePoint"
                    ErrorMessage="只能输入大于零的正整数" ValidationExpression="^([1-9])(\d{0,})(\d{0,})$" Display="Dynamic" />
               <asp:RegularExpressionValidator ID="ValgCUserExpExchangePoint" runat="server" ControlToValidate="TxtCUserExpExchangePoint"
                    ErrorMessage="只能输入大于零的正整数" ValidationExpression="^([1-9])(\d{0,})(\d{0,})$" Display="Dynamic" />
            </td>
        </tr>
        <tr class="tdbg" runat="server" id="UserExpExchangeValidDay">
            <td class="tdbgleft">
                <strong>会员的积分与有效期的兑换比率：</strong>
            </td>
            <td>
                每
                <asp:TextBox ID="TxtUserExpExchangeValidDay" Text="0" runat="server" Columns="7"
                    MaxLength="7" />
                分积分可兑换 <strong><asp:TextBox ID="TxtCUserExpExchangeValidDay" Text="1" runat="server" Columns="7" MaxLength="7" /></strong> 天有效期
               <asp:RegularExpressionValidator ID="ValgUserExpExchangeValidDay" runat="server" ControlToValidate="TxtUserExpExchangeValidDay"
                    ErrorMessage="只能输入大于零的正整数" ValidationExpression="^([1-9])(\d{0,})(\d{0,})$" Display="Dynamic" />
               <asp:RegularExpressionValidator ID="ValgCUserExpExchangeValidDay" runat="server" ControlToValidate="TxtCUserExpExchangeValidDay"
                    ErrorMessage="只能输入大于零的正整数" ValidationExpression="^([1-9])(\d{0,})(\d{0,})$" Display="Dynamic" />
            </td>
        </tr>
        <tr class="tdbg" runat="server" id="PointName">
            <td class="tdbgleft">
                <strong><pe:ShowPointName ID="ShowPointName8" runat="server" />的名称：</strong><br />
                例如：点券、金币
            </td>
            <td style="height: 36px">
                <asp:TextBox ID="TxtPointName" Text="点券" runat="server" Columns="5" MaxLength="5" />
            </td>
        </tr>
        <tr class="tdbg" runat="server" id="PointUnit">
            <td class="tdbgleft">
                <strong><pe:ShowPointName ID="ShowPointName9" runat="server" />的单位：</strong><br />
                例如：点、个
            </td>
            <td>
                <asp:TextBox ID="TxtPointUnit" Text="点" runat="server" Columns="5" MaxLength="5" />
            </td>
        </tr>
        <tr class="tdbgbottom">
            <td colspan="2">
                <asp:Button ID="BtnSubmit" runat="server" Text="保存设置" OnClick="BtnSubmit_Click" />
            </td>
        </tr>
    </table>

    <script type="text/javascript">
    var LitRegFieldsClientID = "<%=LitRegFields.ClientID %>";
    var HdnRegFields_MustFillClientID = "<%=HdnRegFields_MustFill.ClientID %>";
    var LitRegFields_MustFillClientID = "<%=LitRegFields_MustFill.ClientID %>";
    var HdnRegFields_SelectFillClientID = "<%=HdnRegFields_SelectFill.ClientID %>";
    var LitRegFields_SelectFillClientID = "<%=LitRegFields_SelectFill.ClientID %>";
    var TxtEmailOfRegCheckClientID = "<%=TxtEmailOfRegCheck.ClientID %>";
    function AddFields_MustFill()
    {
        var itemList = document.getElementById(LitRegFieldsClientID);
        var target = document.getElementById(LitRegFields_MustFillClientID);
        addItem(itemList,target);
        SetHdn(target,document.getElementById(HdnRegFields_MustFillClientID));
    }
    
    function RemoveFields_MustFill()
    {
        var itemList = document.getElementById(LitRegFieldsClientID);
        var target = document.getElementById(LitRegFields_MustFillClientID);
        addItem(target,itemList);
        SetHdn(target,document.getElementById(HdnRegFields_MustFillClientID));
    }
    
    function AddFields_SelectFill()
    {
        var itemList = document.getElementById(LitRegFieldsClientID);
        var target = document.getElementById(LitRegFields_SelectFillClientID);
        addItem(itemList,target);
        SetHdn(target,document.getElementById(HdnRegFields_SelectFillClientID));
    }
    
    function RemoveFields_SelectFill()
    {
        var itemList = document.getElementById(LitRegFieldsClientID);
        var target = document.getElementById(LitRegFields_SelectFillClientID);
        addItem(target,itemList);
        SetHdn(target,document.getElementById(HdnRegFields_SelectFillClientID));
    }
    
    function UpFields_MustFill()
    {
        var target = document.getElementById(LitRegFields_MustFillClientID);
        UpOption(target);
        SetHdn(target,document.getElementById(HdnRegFields_MustFillClientID));
    }
    
    function DownFields_MustFill()
    {
        var target = document.getElementById(LitRegFields_MustFillClientID);
        DownOption(target);
        SetHdn(target,document.getElementById(HdnRegFields_MustFillClientID));
    }
    
    function UpFields_SelectFill()
    {
        var target = document.getElementById(LitRegFields_SelectFillClientID);
        UpOption(target);
        SetHdn(target,document.getElementById(HdnRegFields_SelectFillClientID));
    }
    
    function DownFields_SelectFill()
    {
        var target = document.getElementById(LitRegFields_SelectFillClientID);
        DownOption(target);
        SetHdn(target,document.getElementById(HdnRegFields_SelectFillClientID));
    }
    
    function UpOption(obj)
    {
        for(var i = 0; i < obj.length; i++)
        {
            var opt = obj.options[i];
            if (opt.selected)
            {
                if(i >= 1)
                {
                    var temp = obj.options[i-1];
                    obj.options[i] = new Option(temp.text, temp.value, 0, 0);
                    obj.options[i-1] = new Option(opt.text, opt.value, 0, 1);
                }
            }
        }
    }
    
    function DownOption(obj)
    {
        for(var i = obj.length-1; i >= 0; i--)
        {
            var opt = obj.options[i];
            if (opt.selected)
            {
                if(i <= obj.length-2)
                {
                    var temp = obj.options[i+1];
                    obj.options[i] = new Option(temp.text, temp.value, 0, 0);
                    obj.options[i+1] = new Option(opt.text, opt.value, 0, 1);
                }
            }
        }
    }
    
    function addItem(ItemList,Target)
    {
        for(var i = 0; i < ItemList.length; i++)
        {
            var opt = ItemList.options[i];
            if (opt.selected)
            {
                flag = true;
                for (var y=0;y<Target.length;y++)
                {
                    var myopt = Target.options[y];
                    if (myopt.value == opt.value)
                    {
                        flag = false;
                    }
                }
                if(flag)
                {
                    Target.options[Target.options.length] = new Option(opt.text, opt.value, 0, 0);
                }
            }
        }
        
        for (var y=0;y<Target.length;y++)
        {
              var myopt = Target.options[y];
              for(var i = 0; i < ItemList.length; i++)
              {
                    if(ItemList.options[i].value == myopt.value)
                    {
                        ItemList.options[i] = null;
                    }
              }
         }
    }

    function SetHdn(ItemList,HdnObj)
    {
        var adminId = "";
        for(var i = 0; i < ItemList.length; i++)
        {
            if (adminId == "")
            {
                adminId = ItemList.options[i].value;
            }
            else
            {
                adminId += "," + ItemList.options[i].value;
            }
        }
        HdnObj.value = adminId;
    }

    function Insert(input){
        document.getElementById(TxtEmailOfRegCheckClientID).focus();
        var str = document.selection.createRange();
        if (input != null){
            str.text = input;
        }
    }
    //-->
    </script>

</asp:Content>
