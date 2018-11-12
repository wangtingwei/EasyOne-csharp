<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Accessories.SmsConfig"
    Title="手机短信配置" Codebehind="SmsConfig.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="server">
<script language="javascript" type="text/javascript">
function Link ()
{
    window.open("http://sms.EasyOne.net/Register.aspx");
}
</script>
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td colspan="2" class="spacingtitle">
                <strong>手机短信配置</strong>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>动易短信通的用户名：</strong><br />
                请填入您在 动易短信通平台 注册的用户名
            </td>
            <td>
                <asp:TextBox ID="TxtUserName" runat="server" Columns="30"></asp:TextBox> &nbsp;&nbsp;
                <input type="submit" value="注册短信通" class="inputbutton" onclick="Link ();"/>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>MD5密钥：</strong><br />
                请填入您在 动易短信通平台 中设置的MD5密钥
            </td>
            <td>
                <asp:TextBox ID="TxtMD5Key" runat="server" TextMode="password" Columns="30"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>客户提交订单时，系统是否自动发送手机短信通知管理员：</strong>
            </td>
            <td>
                <asp:RadioButtonList ID="RadlIsAutoSend" runat="server" RepeatDirection="Horizontal"
                    RepeatLayout="Flow">
                    <asp:ListItem Value="true">是</asp:ListItem>
                    <asp:ListItem Value="false">否</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>管理员的小灵通或手机号码：</strong><br />
                每行输入一个号码。 可以输入多个号码，系统将同时发送到多个号码上
            </td>
            <td>
                <asp:TextBox ID="TxtAdminPhoneNumber" runat="server" TextMode="MultiLine" Height="80px"
                    Width="400px"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg" id = "OrderMessage" runat ="server">
            <td class="tdbgleft">
                <strong>客户下订单时系统给管理员发送短信的内容：</strong><br />
                不支持HTML代码，可用标签详见下面的标签说明
            </td>
            <td>
                <asp:TextBox ID="TxtOrderMessage" runat="server" TextMode="MultiLine" Height="80px"
                    Width="400px"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>客户在线支付成功后是否给客户发送手机短信，告知其卡号和密码：</strong>
            </td>
            <td>
                <asp:RadioButtonList ID="RadlIsAutoSendCardNumber" runat="server" RepeatDirection="Horizontal"
                    RepeatLayout="Flow">
                    <asp:ListItem Value="true">是</asp:ListItem>
                    <asp:ListItem Value="false">否</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr class="tdbg" id = "ConfirmOrderMessage" runat ="server">
            <td class="tdbgleft">
                <strong>确认订单时手机短信通知内容：</strong><br />
                不支持HTML代码，可用标签详见下面的标签说明
            </td>
            <td>
                <asp:TextBox ID="TxtConfirmOrderMessage" runat="server" TextMode="MultiLine" Height="80px"
                    Width="400px"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>收到银行汇款后手机短信通知内容：</strong><br />
                不支持HTML代码，可用标签详见下面的标签说明<br />
                特别标签：<br />
                {$BankName}：汇入银行<br />
                {$Money}：汇款金额
            </td>
            <td>
                <asp:TextBox ID="TxtRemitMessage" runat="server" TextMode="MultiLine" Height="80px"
                    Width="400px"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg" id="RefundmentMessage" runat ="server">
            <td class="tdbgleft">
                <strong>退款后手机短信通知内容：</strong><br />
                不支持HTML代码，可用标签详见下面的标签说明
            </td>
            <td>
                <asp:TextBox ID="TxtRefundmentMessage" runat="server" TextMode="MultiLine" Height="80px"
                    Width="400px"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg" id="InvoiceMessage" runat ="server">
            <td class="tdbgleft">
                <strong>开发票后手机短信通知内容：</strong><br />
                不支持HTML代码，可用标签详见下面的标签说明
            </td>
            <td>
                <asp:TextBox ID="TxtInvoiceMessage" runat="server" TextMode="MultiLine" Height="80px"
                    Width="400px"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg" id="ConsignmentMessage" runat ="server">
            <td class="tdbgleft" >
                <strong>发出货物后手机短信通知内容：</strong><br />
                不支持HTML代码，可用标签详见下面的标签说明<br />
                特别标签：<br />
                {$ExpressCompany}：快递公司<br />
                {$ExpressNumber}：快递单号
            </td>
            <td>
                <asp:TextBox ID="TxtConsignmentMessage" runat="server" TextMode="MultiLine" Height="80px"
                    Width="400px"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg" id ="SendCardNumberMessage" runat ="server">
            <td class="tdbgleft">
                <strong>发送卡号后手机短信通知内容：</strong><br />
                不支持HTML代码，可用标签详见下面的标签说明<br />
                特别标签：<br />
                {$CardInfo}：购买的卡号及密码信息<br />
            </td>
            <td>
                <asp:TextBox ID="TxtSendCardNumberMessage" runat="server" TextMode="MultiLine" Height="80px"
                    Width="400px"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg" id="UseLabel" runat ="server">
            <td class="tdbgleft">
                <strong>通知内容中的可用标签及含义：</strong>
            </td>
            <td>
                <asp:TextBox ID="TxtUseLabel" runat="server" TextMode="MultiLine" Height="80px" Width="400px"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg" id ="CartInformMessage" runat ="server">
            <td class="tdbgleft">
                <strong>购物车管理手机催单短信通知内容：</strong><br />
                不支持HTML代码，可用标签：<br />
                {$UserName}：会员用户名<br />
                {$UpdateTime}：购买时间<br />
                {$CartInfo}：购物车信息
            </td>
            <td>
                <asp:TextBox ID="TxtCartInformMessage" runat="server" TextMode="MultiLine" Height="80px"
                    Width="400px"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>给会员添加银行汇款记录时发送的手机短信内容：</strong><br />
                不支持HTML代码，可用标签：<br />
                {$UserName}：会员用户名<br />
                {$Balance}：资金余额<br />
                {$ReceiptDate}：到款日期<br />
                {$Money}：汇款金额<br />
                {$BankName}：汇入银行
            </td>
            <td>
                <asp:TextBox ID="TxtBankLogMessage" runat="server" TextMode="MultiLine" Height="80px"
                    Width="400px"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>给会员添加其他收入记录时发送的手机短信内容：</strong><br />
                不支持HTML代码，可用标签：<br />
                {$UserName}：会员用户名<br />
                {$Balance}：资金余额<br />
                {$Money}：收入金额<br />
                {$Reason}：原因
            </td>
            <td>
                <asp:TextBox ID="TxtIncomeLogMessage" runat="server" TextMode="MultiLine" Height="80px"
                    Width="400px"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>给会员添加支出记录时发送的手机短信内容：</strong><br />
                不支持HTML代码，可用标签：<br />
                {$UserName}：会员用户名<br />
                {$Balance}：资金余额<br />
                {$Money}：支出金额<br />
                {$Reason}：原因
            </td>
            <td>
                <asp:TextBox ID="TxtPayoutLogMessage" runat="server" TextMode="MultiLine" Height="80px"
                    Width="400px"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>给会员兑换点券时发送的手机短信内容：</strong><br />
                不支持HTML代码，可用标签：<br />
                {$UserName}：会员用户名<br />
                {$Balance}：资金余额<br />
                {$UserPoint}：可用点券<br />
                {$Money}：支出金额<br />
                {$Point}：得到的点券数
            </td>
            <td>
                <asp:TextBox ID="TxtExchangePointMessage" runat="server" TextMode="MultiLine" Height="80px"
                    Width="400px"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>给会员奖励点券时发送的手机短信内容：</strong><br />
                不支持HTML代码，可用标签：<br />
                {$UserName}：会员用户名<br />
                {$UserPoint}：可用点券<br />
                {$Point}：增加的点券数<br />
                {$Reason}：奖励原因
            </td>
            <td>
                <asp:TextBox ID="TxtEncouragePointMessage" runat="server" TextMode="MultiLine" Height="80px"
                    Width="400px"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>给会员扣除点券时发送的手机短信内容：</strong><br />
                不支持HTML代码，可用标签：<br />
                {$UserName}：会员用户名<br />
                {$UserPoint}：可用点券<br />
                {$Point}：扣除的点券数<br />
                {$Reason}：扣除原因
            </td>
            <td>
                <asp:TextBox ID="TxtPayoutPointMessage" runat="server" TextMode="MultiLine" Height="80px"
                    Width="400px"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>给会员兑换有效期时发送的手机短信内容：</strong><br />
                不支持HTML代码，可用标签：<br />
                {$UserName}：会员用户名<br />
                {$Balance}：资金余额<br />
                {$ValidDays}：剩余天数<br />
                {$Money}：支出金额<br />
                {$Valid}：得到的有效期
            </td>
            <td>
                <asp:TextBox ID="TxtExchangePeriodMessage" runat="server" TextMode="MultiLine" Height="80px"
                    Width="400px"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>给会员奖励有效期时发送的手机短信内容：</strong><br />
                不支持HTML代码，可用标签：<br />
                {$UserName}：会员用户名<br />
                {$ValidDays}：剩余天数<br />
                {$Valid}：得到的有效期<br />
                {$Reason}：奖励原因
            </td>
            <td>
                <asp:TextBox ID="TxtEncouragePeriodMessage" runat="server" TextMode="MultiLine" Height="80px"
                    Width="400px"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>给会员扣除有效期时发送的手机短信内容：</strong><br />
                不支持HTML代码，可用标签：<br />
                {$UserName}：会员用户名<br />
                {$ValidDays}：剩余天数<br />
                {$Valid}：扣除的有效期<br />
                {$Reason}：扣除原因
            </td>
            <td>
                <asp:TextBox ID="TxtPayoutPeriodMessage" runat="server" TextMode="MultiLine" Height="80px"
                    Width="400px"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>管理员审核信息后是否发送手机短信告知会员：</strong>
            </td>
            <td>
                <asp:RadioButtonList ID="RadlIsAutoSendStateMessage" runat="server" RepeatDirection="Horizontal"
                    RepeatLayout="Flow">
                    <asp:ListItem Value="true">是</asp:ListItem>
                    <asp:ListItem Value="false">否</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>确认审核时发送的手机短信内容：</strong><br />
                不支持HTML代码，可用标签：<br />
                {$UserName}：会员用户名<br />
                {$Title}：信息标题<br />
                {$State}：状态
            </td>
            <td>
                <asp:TextBox ID="TxtChangeStateMessage" runat="server" TextMode="MultiLine" Height="80px"
                    Width="400px"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbgbottom">
            <td colspan="2">
                <asp:Button ID="BtnSubmit" runat="server" Text="保存设置" OnClick="BtnSubmit_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
