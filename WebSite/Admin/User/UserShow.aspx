<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPage.master"
    Inherits="EasyOne.WebSite.Admin.User.UserShow" Title="显示用户信息" Codebehind="UserShow.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <br />
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr align="center">
            <td id="TabTitle0" class="titlemouseover" onclick="ShowTabs(0)">
                会员信息
            </td>
            <td id="TabTitle1" class="tabtitle" onclick="ShowTabs(1)">
                联系信息
            </td>
            <td id="TabTitle2" class="tabtitle" onclick="ShowTabs(2)">
                个人信息
            </td>
            <td id="TabTitle3" class="tabtitle" onclick="ShowTabs(3)">
                业务信息
            </td>
            <td id="TabTitle4" class="tabtitle" style="<%= IsShow()%>" onclick="ShowTabs(4)">
                单位信息
            </td>
            <td id="TabTitle5" class="tabtitle" style="<%= IsShow()%>" onclick="ShowTabs(5)">
                单位成员
            </td>
            <td>
                &nbsp;
                <pe:ExtendedButton ID="EBtnRegCompany" OnClick="EBtnRegCompany_Click" Text="升级为企业会员"
                    OperateCode="UserUpdateToCompany" runat="server" />
                <pe:ExtendedButton ID="EBtnRegClient" OnClick="EBtnRegClient_Click" Text="升级为客户"
                    OperateCode="UserUpdateToClient" runat="server" />
                <asp:Button ID="BtnToClient" runat="server" Text="切换到对应客户信息页" OnClick="BtnToClient_Click" />
            </td>
        </tr>
    </table>
    <table width="100%" border="0" cellpadding="5" cellspacing="1" class="border">
        <tbody id="Tabs0">
            <tr class="tdbg">
                <td style="width: 15%; text-align: right" class="tdbgleft">
                    会 员 ID：
                </td>
                <td>
                    <asp:Label ID="LblUserId" runat="server" Text=""></asp:Label>
                </td>
                <td class="tdbgleft" style="width: 15%; text-align: right">
                    邮箱地址：
                </td>
                <td style="width: 210px">
                    <asp:HyperLink ID="LnkEmail" runat="server"></asp:HyperLink>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft" style="text-align: right">
                    会 员 名：
                </td>
                <td>
                    <asp:Label ID="LblUserName" runat="server" Text=""></asp:Label>
                </td>
                <td class="tdbgleft" style="text-align: right">
                    用户状态：
                </td>
                <td style="width: 210px">
                    <pe:ExtendedLabel HtmlEncode="false" ID="LblUserStatus" runat="server" Text=""></pe:ExtendedLabel>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft" style="text-align: right">
                    会 员 组：
                </td>
                <td>
                    <asp:Label ID="LblGroupName" runat="server" Text=""></asp:Label>
                </td>
                <td class="tdbgleft" style="text-align: right">
                    用户好友组：
                </td>
                <td style="width: 210px">
                    <asp:Label ID="LblUserFriendGroup" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft" style="text-align: right">
                    会员权限：
                </td>
                <td>
                    <asp:Label ID="LblSpecialPermission" runat="server" Text=""></asp:Label>
                </td>
                <td class="tdbgleft" style="text-align: right">
                    会员类别：
                </td>
                <td style="width: 210px">
                    <asp:Label ID="LblUserType" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg" runat="server" id="BalancePoint">
                <td class="tdbgleft" style="text-align: right">
                    资金余额：
                </td>
                <td>
                    <asp:Label ID="LblBalance" runat="server" Text=""></asp:Label>
                </td>
                <td class="tdbgleft" style="text-align: right;">
                    可用<pe:ShowPointName ID="ShowPointName" runat="server"></pe:ShowPointName>数：
                </td>
                <td style="width: 210px">
                    <asp:Label ID="LblUserPoint" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg" runat="server" id="ExpValid">
                <td class="tdbgleft" style="text-align: right">
                    可用积分：
                </td>
                <td>
                    <asp:Label ID="LblUserExp" runat="server" Text=""></asp:Label>
                </td>
                <td class="tdbgleft" style="text-align: right">
                    剩余天数：
                </td>
                <td style="width: 210px">
                    <pe:ExtendedLabel HtmlEncode="false" ID="LblValidNum" runat="server" Text=""></pe:ExtendedLabel>
                    天
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft" style="text-align: right">
                    待签文章：
                </td>
                <td>
                    <asp:Label ID="LblUnsignedItems" runat="server" Text=""></asp:Label>
                </td>
                <td class="tdbgleft" style="text-align: right">
                    待阅短信：
                </td>
                <td style="width: 210px">
                    <asp:Label ID="LblUnreadMsg" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg" runat="server" id="ConsumeMoney">
                <td class="tdbgleft" style="text-align: right">
                    消费的金额：
                </td>
                <td>
                    <asp:Label ID="LblConsumeMoney" runat="server" Text=""></asp:Label>
                </td>
                <td style="width: 15%; text-align: right;" class="tdbgleft">
                    消费的<pe:ShowPointName ID="ShowPointName1" runat="server"></pe:ShowPointName>数：
                </td>
                <td style="width: 210px">
                    <asp:Label ID="LblConsumePoint" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg" runat="server" id="ConsumeExp">
                <td class="tdbgleft" style="text-align: right">
                    消费的积分数：
                </td>
                <td>
                    <asp:Label ID="LblConsumeExp" runat="server" Text=""></asp:Label>
                </td>
                <td class="tdbgleft" style="text-align: right">
                    添加的信息数：
                </td>
                <td style="width: 210px">
                    <asp:Label ID="LblPostItems" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft" style="text-align: right">
                    审核通过的信息数：
                </td>
                <td>
                    <asp:Label ID="LblPassedItems" runat="server" Text=""></asp:Label>
                </td>
                <td class="tdbgleft" style="text-align: right">
                    被退稿的信息数：
                </td>
                <td style="width: 210px">
                    <asp:Label ID="LblRejectItems" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft" style="text-align: right">
                    被删除的信息数：
                </td>
                <td>
                    <asp:Label ID="LblDelItems" runat="server" Text=""></asp:Label>
                </td>
                <td class="tdbgleft" style="text-align: right">
                    登录次数：
                </td>
                <td style="width: 210px">
                    <asp:Label ID="LblLoginTimes" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft" style="text-align: right">
                    最后登录时间：
                </td>
                <td>
                    <asp:Label ID="LblLastLoginTime" runat="server" Text=""></asp:Label>
                </td>
                <td class="tdbgleft" style="text-align: right">
                    最后登录IP：
                </td>
                <td style="width: 210px">
                    <asp:Label ID="LblLastLoginIP" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft" style="text-align: right">
                    注册日期：
                </td>
                <td>
                    <asp:Label ID="LblRegTime" runat="server" Text=""></asp:Label>
                </td>
                <td class="tdbgleft" style="text-align: right">
                    加入日期：
                </td>
                <td style="width: 210px">
                    <asp:Label ID="LblJoinTime" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </tbody>
        <tbody id="Tabs1" style="display: none">
            <tr class="tdbg">
                <td class="tdbgleft" style="width: 15%; text-align: right">
                    真实姓名：
                </td>
                <td style="width: 35%">
                    <asp:Label ID="LblTrueName" runat="server" Text=""></asp:Label>
                </td>
                <td class="tdbgleft" style="width: 15%; text-align: right">
                    称谓：
                </td>
                <td style="width: 210px">
                    <asp:Label ID="LblTitle" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft" style="text-align: right">
                    国家/地区：
                </td>
                <td>
                    <asp:Label ID="LblCountry" runat="server" Text=""></asp:Label>
                </td>
                <td class="tdbgleft" style="text-align: right">
                    省/市：
                </td>
                <td style="width: 210px">
                    <asp:Label ID="LblProvince" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft" style="text-align: right">
                    市/县/区：
                </td>
                <td>
                    <asp:Label ID="LblCity" runat="server" Text=""></asp:Label>
                </td>
                <td class="tdbgleft" style="text-align: right">
                    邮政编码：
                </td>
                <td style="width: 210px">
                    <asp:Label ID="LblZipCode" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft" style="text-align: right">
                    联系地址：
                </td>
                <td colspan="3">
                    <asp:Label ID="LblAddress" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft" style="text-align: right">
                    办公电话：
                </td>
                <td>
                    <asp:Label ID="LblOfficePhone" runat="server" Text=""></asp:Label>
                </td>
                <td class="tdbgleft" style="text-align: right">
                    住宅电话：
                </td>
                <td style="width: 210px">
                    <asp:Label ID="LblHomephone" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft" style="text-align: right">
                    移动电话：
                </td>
                <td>
                    <asp:Label ID="LblMobile" runat="server" Text=""></asp:Label>
                </td>
                <td class="tdbgleft" style="text-align: right">
                    传真号码：
                </td>
                <td style="width: 210px">
                    <asp:Label ID="LblFax" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft" style="text-align: right">
                    小灵通：
                </td>
                <td>
                    <asp:Label ID="LblPHS" runat="server" Text=""></asp:Label>
                </td>
                <td class="tdbgleft" style="text-align: right">
                </td>
                <td style="width: 210px">
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft" style="text-align: right">
                    个人主页：
                </td>
                <td>
                    <asp:Label ID="LblHomePage" runat="server" Text="http://"></asp:Label>
                </td>
                <td class="tdbgleft" style="text-align: right">
                    Email地址：
                </td>
                <td style="width: 210px">
                    <asp:HyperLink ID="LnkEmail1" runat="server"></asp:HyperLink>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft" style="text-align: right">
                    QQ号码：
                </td>
                <td>
                    <pe:ExtendedLabel ID="LblQQ" runat="server" Text=""></pe:ExtendedLabel>
                </td>
                <td class="tdbgleft" style="text-align: right">
                    MSN帐号：
                </td>
                <td style="width: 210px">
                    <asp:Label ID="LblMSN" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft" style="text-align: right">
                    ICQ号码：
                </td>
                <td>
                    <asp:Label ID="LblICQ" runat="server" Text=""></asp:Label>
                </td>
                <td class="tdbgleft" style="text-align: right">
                    雅虎通帐号：
                </td>
                <td style="width: 210px">
                    <asp:Label ID="LblYahoo" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft" style="text-align: right">
                    UC帐号：
                </td>
                <td>
                    <asp:Label ID="LblUC" runat="server" Text=""></asp:Label>
                </td>
                <td class="tdbgleft" style="text-align: right">
                    Aim帐号：
                </td>
                <td style="width: 210px">
                    <asp:Label ID="LblAim" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </tbody>
        <tbody id="Tabs2" style="display: none">
            <tr class="tdbg">
                <td class="tdbgleft" style="width: 15%; text-align: right">
                    出生日期：
                </td>
                <td style="width: 35%;">
                    <asp:Label ID="LblBirthday" runat="server" Text=""></asp:Label>
                </td>
                <td class="tdbgleft" style="width: 15%; text-align: right">
                    证件号码：
                </td>
                <td style="width: 210px;">
                    <asp:Label ID="LblIDCard" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft" style="text-align: right">
                    籍贯：
                </td>
                <td>
                    <asp:Label ID="LblNativePlace" runat="server" Text=""></asp:Label>
                </td>
                <td class="tdbgleft" style="text-align: right">
                    民族：
                </td>
                <td style="width: 210px">
                    <asp:Label ID="LblNation" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft" style="text-align: right">
                    性别：
                </td>
                <td>
                    <asp:Label ID="LblSex" runat="server" Text=""></asp:Label>
                </td>
                <td class="tdbgleft" style="text-align: right">
                    婚姻状况：
                </td>
                <td style="width: 210px">
                    <asp:Label ID="LblMarriage" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft" style="text-align: right">
                    学历：
                </td>
                <td>
                    <asp:Label ID="LblEducation" runat="server" Text=""></asp:Label>
                </td>
                <td class="tdbgleft" style="text-align: right">
                    毕业学校：
                </td>
                <td style="width: 210px">
                    <asp:Label ID="LblGraduateFrom" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft" style="text-align: right">
                    生活爱好：
                </td>
                <td>
                    <asp:Label ID="LblInterestsOfLife" runat="server" Text=""></asp:Label>
                </td>
                <td class="tdbgleft" style="text-align: right">
                    文化爱好：
                </td>
                <td style="width: 210px">
                    <asp:Label ID="LblInterestsOfCulture" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft" style="text-align: right">
                    娱乐休闲爱好：
                </td>
                <td>
                    <asp:Label ID="LblInterestsOfAmusement" runat="server" Text=""></asp:Label>
                </td>
                <td class="tdbgleft" style="text-align: right">
                    体育爱好：
                </td>
                <td style="width: 210px">
                    <asp:Label ID="LblInterestsOfSport" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft" style="text-align: right">
                    其他爱好：
                </td>
                <td>
                    <asp:Label ID="LblInterestsOfOther" runat="server" Text=""></asp:Label>
                </td>
                <td class="tdbgleft" style="text-align: right">
                    月 收 入：
                </td>
                <td style="width: 210px">
                    <asp:Label ID="LblIncome" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </tbody>
        <tbody id="Tabs3" style="display: none">
            <tr class="tdbg">
                <td class="tdbgleft" style="width: 15%; text-align: right">
                    单位名称：
                </td>
                <td style="width: 35%">
                    <asp:Label ID="LblCompany" runat="server" Text=""></asp:Label>
                </td>
                <td class="tdbgleft" style="width: 15%; text-align: right">
                    所属部门：
                </td>
                <td style="width: 210px">
                    <asp:Label ID="LblDepartment" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft" style="text-align: right">
                    职位：
                </td>
                <td>
                    <asp:Label ID="LblPosition" runat="server" Text=""></asp:Label>
                </td>
                <td class="tdbgleft" style="text-align: right">
                    负责业务：
                </td>
                <td style="width: 210px">
                    <asp:Label ID="LblOperation" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft" style="text-align: right">
                    单位地址：
                </td>
                <td colspan="3">
                    <asp:Label ID="LblCompanyAddress" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </tbody>
        <tbody id="Tabs4" style="display: none">
            <tr class="tdbg">
                <td>
                    <pe:CompanyInfo ID="CompanyInfo1" runat="server" />
                </td>
            </tr>
        </tbody>
        <tbody id="Tabs5" style="display: none">
            <tr class="tdbg">
                <td>
                    <pe:CompanyMemberManage ID="CompanyMemberManage1" runat="server" />
                </td>
            </tr>
        </tbody>
    </table>
    <br />
    <table width="100%" style="width: 100%; height: 60px;">
        <tr align="center">
            <td align="left">
                <pe:ExtendedButton IsChecked="true" OperateCode="UserModify" ID="BtnModifyUserSubmit"
                    runat="server" Text="修改会员信息" OnClick="BtnModifyUserSubmit_Click" />
                <pe:ExtendedButton IsChecked="true" OperateCode="UserModifyPermissions" ID="BtnModifyPurview"
                    runat="server" Text="修改会员权限" OnClick="BtnModifyPurview_Click" CausesValidation="False" />
                <pe:ExtendedButton IsChecked="true" OperateCode="UserLock" ID="BtnLock" runat="server"
                    OnClientClick="return confirm('确定要锁定此会员吗？');" Text=" 锁定此会员 " OnClick="BtnLock_Click"
                    CausesValidation="False" />
                <pe:ExtendedButton IsChecked="true" OperateCode="UserDelete" ID="BtnDelete" runat="server"
                    Text=" 删除此会员 " OnClientClick="return confirm('确定要删除此会员吗？');" OnClick="BtnDelete_Click" />
                <pe:ExtendedButton IsChecked="true" OperateCode="MessageManage" ID="BtnMessage" runat="server"
                    Text=" 发送短消息 " OnClick="BtnMessage_Click" UseSubmitBehavior="False" />
                <pe:ExtendedButton IsChecked="true" OperateCode="UserMoneyManage" ID="BtnIncome"
                    runat="server" Text="添加银行汇款" UseSubmitBehavior="False" OnClick="BtnIncome_Click" />
                <pe:ExtendedButton IsChecked="true" OperateCode="UserMoneyManage" ID="OtherIncome"
                    runat="server" Text="添加其他收入" OnClick="OtherIncome_Click" />
                <br />
                <pe:ExtendedButton IsChecked="true" OperateCode="UserMoneyManage" ID="BtnPayment"
                    runat="server" Text="添加支出金额" UseSubmitBehavior="False" OnClick="BtnPayment_Click" />
                <pe:ExtendedButton IsChecked="true" OperateCode="UserPointManage" ID="BtnExchangePoint"
                    runat="server" Text="   点券兑换   " UseSubmitBehavior="False" OnClick="BtnExchangePoint_Click" />
                <pe:ExtendedButton IsChecked="true" OperateCode="UserPointManage" ID="BtnAddPoint"
                    runat="server" Text="  奖励点券  " UseSubmitBehavior="False" OnClick="BtnAddPoint_Click" />
                <pe:ExtendedButton IsChecked="true" OperateCode="UserPointManage" ID="BtnMinusPoint"
                    runat="server" Text="  扣除点券  " UseSubmitBehavior="False" OnClick="BtnMinusPoint_Click" />
                <pe:ExtendedButton IsChecked="true" OperateCode="UserValidDateManage" ID="BtnExchangeValid"
                    runat="server" Text=" 兑换有效期 " UseSubmitBehavior="False" OnClick="BtnExchangeValid_Click" />
                <pe:ExtendedButton IsChecked="true" OperateCode="UserValidDateManage" ID="BtnAddValidDate"
                    runat="server" Text=" 添加有效期 " UseSubmitBehavior="False" OnClick="BtnAddValidDate_Click" />
                <pe:ExtendedButton IsChecked="true" OperateCode="UserValidDateManage" ID="BtnMinusValidDate"
                    runat="server" Text=" 扣除有效期 " UseSubmitBehavior="False" OnClick="BtnMinusValidDate_Click" />
                <pe:ExtendedButton IsChecked="true" OperateCode="OrderAdd" ID="BtnOrderAdd" runat="server"
                    Text="  添加订单  " UseSubmitBehavior="False" Visible="false" OnClick="BtnOrderAdd_Click" />
                <pe:ExtendedButton ID="EBtnSendEmail" Text=" 发送邮件 " IsChecked="true" OperateCode="sendinfomanage"
                    OnClick="EBtnSendEmail_Click" CausesValidation="False" runat="server" />
                <pe:ExtendedButton ID="EBtnSendTelMessage" Text="发手机短信" IsChecked="true" OperateCode="smsmanage"
                    OnClick="EBtnSendTelMessage_Click" CausesValidation="False" runat="server" />
            </td>
            <asp:HiddenField ID="HdnLockType" runat="server" />
        </tr>
    </table>
    <div runat="server" id="Details">
        <table id="Table1" width="100%" border="0" cellpadding="0" cellspacing="0" runat="server">
            <tr id="Tr1" align="center" runat="server">
                <td id="InfoTabTitle0" class="tabtitle" runat="server">
                    会员订单
                </td>
                <td id="InfoTabTitle1" class="tabtitle" runat="server">
                    资金明细
                </td>
                <td id="InfoTabTitle2" class="tabtitle" runat="server">
                    <%=m_PointName %>明细
                </td>
                <td id="InfoTabTitle3" class="tabtitle" runat="server">
                    有效期明细
                </td>
                <td id="InfoTabTitle4" class="tabtitle" runat="server">
                    在线支付明细
                </td>
                <td id="InfoTabTitle5" class="tabtitle" visible="false" runat="server">
                    被投诉记录
                </td>
                <td id="InfoTabTitle6" class="tabtitle" visible="false" runat="server">
                    代理订单
                </td>
                <td id="InfoTabTitle7" class="tabtitle" visible="false" runat="server">
                    对账单
                </td>
                <td id="InfoNull" runat="server">
                    &nbsp;
                </td>
            </tr>
        </table>
        <pe:ExtendedGridView ID="EgvOrder" Visible="false" ItemName="订单" ItemUnit="个" AutoGenerateColumns="False"
            DataKeyNames="OrderId" AllowPaging="True" runat="server" OnRowDataBound="EgvOrder_RowDataBound">
            <Columns>
                <pe:TemplateField HeaderText="订单编号" SortExpression="OrderNum">
                    <ItemTemplate>
                        <a href='../Shop/OrderManage.aspx?OrderID=<%#Eval("OrderId")%>'>
                            <%#Eval("OrderNum")%>
                        </a>
                    </ItemTemplate>
                </pe:TemplateField>
                <pe:BoundField DataField="ClientName" HeaderText="客户名称" SortExpression="ClientName"
                    >
                    <HeaderStyle Width="10%" />
                </pe:BoundField>
                <pe:BoundField DataField="UserName" HeaderText="用户名" SortExpression="UserName" >
                    <HeaderStyle Width="10%" />
                </pe:BoundField>
                <pe:TemplateField HeaderText="下单时间" SortExpression="InputTime">
                    <HeaderStyle Width="14%" />
                    <ItemTemplate>
                        <%# Eval("InputTime", "{0:yyyy-MM-dd HH:mm:ss}")%>
                    </ItemTemplate>
                </pe:TemplateField>
                <pe:TemplateField HeaderText="订单金额" SortExpression="MoneyTotal">
                    <HeaderStyle Width="8%" />
                    <ItemTemplate>
                        <%# Eval("MoneyTotal", "{0:N2}")%>
                    </ItemTemplate>
                </pe:TemplateField>
                <pe:TemplateField HeaderText="收款金额" SortExpression="MoneyReceipt">
                    <HeaderStyle Width="8%" />
                    <ItemTemplate>
                        <%# Eval("MoneyReceipt", "{0:N2}")%>
                    </ItemTemplate>
                </pe:TemplateField>
                <pe:TemplateField HeaderText="需要发票" SortExpression="NeedInvoice">
                    <HeaderStyle Width="5%" />
                    <ItemTemplate>
                        <%# (bool)Eval("NeedInvoice") == false ? "<font color=red>×</font>" : "√"%>
                    </ItemTemplate>
                </pe:TemplateField>
                <pe:TemplateField HeaderText="已开发票">
                    <HeaderStyle Width="5%" />
                    <ItemTemplate>
                        <%# (bool)Eval("Invoiced") == false ? "<font color=red>×</font>" : "√"%>
                    </ItemTemplate>
                </pe:TemplateField>
                <pe:TemplateField HeaderText="订单状态">
                    <HeaderStyle Width="10%" />
                    <ItemTemplate>
                        <pe:ExtendedLabel HtmlEncode="false" ID="LblOrderStatus" runat="server" />
                    </ItemTemplate>
                </pe:TemplateField>
                <pe:TemplateField HeaderText="付款状态">
                    <HeaderStyle Width="10%" />
                    <ItemTemplate>
                        <pe:ExtendedLabel HtmlEncode="false" ID="LblPayStatus" runat="server" />
                    </ItemTemplate>
                </pe:TemplateField>
                <pe:TemplateField HeaderText="物流状态">
                    <HeaderStyle Width="10%" />
                    <ItemTemplate>
                        <pe:ExtendedLabel HtmlEncode="false" ID="LblDeliverStatus" runat="server" ForeColor="AliceBlue" />
                    </ItemTemplate>
                </pe:TemplateField>
            </Columns>
        </pe:ExtendedGridView>
        <pe:ExtendedGridView ID="EgvBankrollItem" runat="server" AutoGenerateColumns="False"
            ShowFooter="True" EmptyDataText="没有任何符合条件的资金记录！" ItemName="资金明细" AllowPaging="True"
            OnDataBound="EgvBankrollItem_DataBound" OnRowDataBound="EgvBankrollItem_RowDataBound"
            SerialText="" DataKeyNames="ItemID" OnRowCommand="EgvBankrollItem_RowCommand"
            CheckBoxFieldHeaderWidth="3%" IsHoldState="True">
            <Columns>
                <asp:BoundField DataField="DateAndTime" HeaderText="交易时间" SortExpression="DateAndTime"
                    DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HtmlEncode="False">
                    <HeaderStyle Width="16%" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="交易方式" SortExpression="MoneyType">
                    <HeaderStyle Width="8%" />
                    <ItemTemplate>
                        <%#GetMoneyType(Eval("MoneyType")) %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="币种" SortExpression="CurrencyType">
                    <HeaderStyle Width="8%" />
                    <ItemTemplate>
                        <%#GetCurrencyType(Eval("CurrencyType")) %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="收入金额">
                    <HeaderStyle Width="10%" />
                    <ItemTemplate>
                        <%# (decimal)Eval("Money")>0?Eval("Money","{0:N2}"):"" %>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Right" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="支出金额">
                    <HeaderStyle Width="10%" />
                    <ItemTemplate>
                        <%#  (decimal)Eval("Money")>0?"":Math.Abs((decimal)Eval("Money")).ToString("N2") %>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Right" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="银行名称" SortExpression="Bank">
                    <HeaderStyle Width="8%" />
                    <ItemTemplate>
                        <%# Eval("Bank")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="备注/说明">
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemTemplate>
                        <asp:Label runat="server" Text='' ID="LblRemark"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="确认">
                    <HeaderStyle Width="5%" />
                    <ItemTemplate>
                        <%#(int)Eval("Status") == 0 ? "<font color=red>×</font>" : "√"%>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </pe:ExtendedGridView>
        <pe:ExtendedGridView ID="EgvUserPoint" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            DataKeyNames="LogId" EmptyDataText="暂无任何用户<%=m_PointName %>数据！" ShowFooter="True"
            ItemName="记录" ItemUnit="条" OnRowDataBound="EgvUserPoint_RowDataBound" OnDataBound="EgvUserPoint_DataBound">
            <Columns>
                <pe:BoundField DataField="LogTime" HeaderText="消费时间" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"
                    SortExpression="LogTime" HtmlEncode="False">
                    <HeaderStyle Width="20%" />
                </pe:BoundField>
                <pe:BoundField DataField="IP" HeaderText="IP地址" SortExpression="IP">
                    <HeaderStyle Width="10%" />
                </pe:BoundField>
                <pe:TemplateField HeaderText="<%=m_PointName %>数" SortExpression="IncomePayOut">
                    <HeaderStyle Width="10%" />
                    <ItemTemplate>
                        <pe:ExtendedLabel HtmlEncode="false" runat="server" ID="LblIncomePayOut">
                        </pe:ExtendedLabel>
                    </ItemTemplate>
                </pe:TemplateField>
                <pe:BoundField DataField="Times" HeaderText="重复次数" SortExpression="Times">
                    <HeaderStyle Width="10%" />
                </pe:BoundField>
                <pe:BoundField DataField="Inputer" HeaderText="操作员" SortExpression="Inputer" >
                    <HeaderStyle Width="10%" />
                </pe:BoundField>
                <pe:BoundField DataField="Remark" HeaderText="备注/说明" SortExpression="Remark" >
                    <ItemStyle HorizontalAlign="Left" />
                </pe:BoundField>
            </Columns>
        </pe:ExtendedGridView>
        <pe:ExtendedGridView ID="EgvUserValid" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            DataKeyNames="LogId" ItemName="记录" ItemUnit="条">
            <Columns>
                <pe:BoundField DataField="LogTime" HeaderText="时间" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"
                    SortExpression="LogTime" HtmlEncode="False">
                    <HeaderStyle Width="20%" />
                </pe:BoundField>
                <pe:BoundField DataField="IP" HeaderText="IP地址" SortExpression="IP">
                    <HeaderStyle Width="10%" />
                </pe:BoundField>
                <pe:TemplateField HeaderText="有效期" SortExpression="IncomePayout">
                    <HeaderStyle Width="10%" />
                    <ItemTemplate>
                        <pe:ExtendedLabel HtmlEncode="false" runat="server" Text='<%#IncomePayout(Convert.ToInt32(DataBinder.Eval(Container.DataItem,"IncomePayout")),Convert.ToInt32(DataBinder.Eval(Container.DataItem,"ValidNum")))%>'
                            ID="LblIncomePayOut">
                        </pe:ExtendedLabel>
                    </ItemTemplate>
                </pe:TemplateField>
                <pe:BoundField DataField="Inputer" HeaderText="操作员" SortExpression="Inputer" >
                    <HeaderStyle Width="10%" />
                </pe:BoundField>
                <pe:BoundField DataField="Remark" HeaderText="备注/说明" SortExpression="Remark" >
                    <ItemStyle HorizontalAlign="Left" />
                </pe:BoundField>
            </Columns>
        </pe:ExtendedGridView>
        <pe:ExtendedGridView ID="GdvPaymentLogList" runat="server" DataKeyNames="PaymentLogId"
            AllowPaging="True" AutoGenerateColumns="False" ItemName="记录" ItemUnit="条" OnRowDataBound="GdvPaymentLogList_RowDataBound">
            <Columns>
                <pe:BoundField DataField="PaymentNum" HeaderText="支付序号" SortExpression="PaymentNum"
                    >
                    <HeaderStyle Width="120px" />
                </pe:BoundField>
                <pe:TemplateField HeaderText="支付平台">
                    <ItemTemplate>
                        <asp:Label ID="LblPlatform" runat="server" />
                    </ItemTemplate>
                </pe:TemplateField>
                <pe:TemplateField HeaderText="交易时间" SortExpression="PayTime">
                    <HeaderStyle Width="120px" />
                    <ItemTemplate>
                        <%# Eval("PayTime", "{0:yyyy-MM-dd HH:mm:ss}")%>
                    </ItemTemplate>
                </pe:TemplateField>
                <pe:TemplateField HeaderText="汇款金额" SortExpression="MoneyPay">
                    <HeaderStyle Width="80px" />
                    <ItemTemplate>
                        <%# Eval("MoneyPay", "{0:N2}")%>
                    </ItemTemplate>
                </pe:TemplateField>
                <pe:TemplateField HeaderText="实际转账金额" SortExpression="MoneyTrue">
                    <HeaderStyle Width="80px" />
                    <ItemTemplate>
                        <%# Eval("MoneyTrue", "{0:N2}")%>
                    </ItemTemplate>
                </pe:TemplateField>
                <pe:TemplateField HeaderText="交易状态">
                    <HeaderStyle Width="60px" />
                    <ItemTemplate>
                        <asp:Label ID="LblStatus" runat="server" />
                        <itemstyle horizontalalign="Center" />
                    </ItemTemplate>
                </pe:TemplateField>
            </Columns>
        </pe:ExtendedGridView>
        <pe:ExtendedGridView ID="EgvComplain" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            DataKeyNames="ItemId" OnRowDataBound="EgvComplain_RowDataBound">
            <Columns>
                <pe:BoundField DataField="DateAndTime" HeaderText="投诉时间" SortExpression="DateAndTime"
                    DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HtmlEncode="False">
                    <HeaderStyle Width="16%" />
                </pe:BoundField>
                <pe:TemplateField HeaderText="客户名称">
                    <HeaderStyle Width="10%" />
                    <ItemTemplate>
                        <pe:ExtendedAnchor ID="ExtendedAnchor1" IsChecked="true" runat="server" OperateCode="ClientView" href='<%# Eval("ClientId", "../Crm/ClientShow.aspx?ClientId={0}") %>'>
                            <%# Eval("ShortedForm") %>
                        </pe:ExtendedAnchor>
                    </ItemTemplate>
                </pe:TemplateField>
                <pe:BoundField HeaderText="投诉类型" SortExpression="ComplainType" >
                    <HeaderStyle Width="12%" />
                </pe:BoundField>
                <pe:TemplateField HeaderText="主题">
                    <ItemTemplate>
                        <pe:ExtendedAnchor ID="ExtendedAnchor2" IsChecked="true" runat="server" OperateCode="ComplainView" href='<%# Eval("ItemId", "../Crm/ComplainShow.aspx?ItemId={0}") %>'>
                            <%# Eval("Title") %>
                        </pe:ExtendedAnchor>
                    </ItemTemplate>
                </pe:TemplateField>
                <pe:BoundField HeaderText="紧急程度" SortExpression="MagnitudeOfExigence" >
                    <HeaderStyle Width="8%" />
                </pe:BoundField>
                <pe:BoundField HeaderText="记录状态" SortExpression="Status" >
                    <HeaderStyle Width="8%" />
                </pe:BoundField>
            </Columns>
        </pe:ExtendedGridView>
        <pe:ExtendedGridView ID="EgvAgentOrders" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            CheckBoxFieldHeaderWidth="3%" DataKeyNames="OrderID" IsHoldState="True" SerialText=""
            OnRowDataBound="EgvAgentOrders_RowDataBound" EmptyDataText="没有任何订单！" OnDataBound="EgvAgentOrders_DataBound"
            ShowFooter="True" Visible="False">
            <Columns>
                <asp:HyperLinkField DataNavigateUrlFields="OrderId" DataTextField="OrderNum" HeaderText="订单编号"
                    DataNavigateUrlFormatString="../Shop/OrderManage.aspx?OrderID={0}" />
                <asp:HyperLinkField DataNavigateUrlFields="ClientId" DataNavigateUrlFormatString="../Crm/ClientShow.aspx?ClientID={0}"
                    DataTextField="ClientName" HeaderText="客户名称" />
                <asp:HyperLinkField DataNavigateUrlFields="UserName" DataNavigateUrlFormatString="UserShow.aspx?UserName={0}"
                    DataTextField="UserName" HeaderText="用户名" />
                <pe:BoundField HeaderText="下单时间" DataField="InputTime" DataFormatString="{0:yyyy-MM-dd}"
                    HtmlEncode="False" />
                <pe:BoundField HeaderText="订单金额" DataField="MoneyTotal" DataFormatString="{0:0.00}"
                    HtmlEncode="False">
                    <ItemStyle HorizontalAlign="Right" />
                </pe:BoundField>
                <pe:BoundField HeaderText="收款金额" DataField="MoneyReceipt" DataFormatString="{0:0.00}"
                    HtmlEncode="False">
                    <ItemStyle HorizontalAlign="Right" />
                </pe:BoundField>
                <pe:TemplateField HeaderText="需要发票">
                    <ItemTemplate>
                        <%#(bool)Eval("NeedInvoice") ? "<span style=\"color:red\">√<span>" : ""%>
                    </ItemTemplate>
                </pe:TemplateField>
                <pe:TemplateField HeaderText="已开发票">
                    <ItemTemplate>
                        <%# (bool)Eval("NeedInvoice") ? ((bool)Eval("Invoiced") ? "√" : "<span style=\"color:red\">×<span>") : ""%>
                    </ItemTemplate>
                </pe:TemplateField>
                <pe:TemplateField HeaderText="订单状态">
                    <ItemTemplate>
                        <pe:ExtendedLabel HtmlEncode="false" runat="server" ID="LblStatus"></pe:ExtendedLabel>
                    </ItemTemplate>
                </pe:TemplateField>
                <pe:TemplateField HeaderText="付款状态">
                    <ItemTemplate>
                        <pe:ExtendedLabel HtmlEncode="false" runat="server" ID="LblPaymentStatus"></pe:ExtendedLabel>
                    </ItemTemplate>
                </pe:TemplateField>
                <pe:TemplateField HeaderText="物流状态">
                    <ItemTemplate>
                        <pe:ExtendedLabel HtmlEncode="false" runat="server" ID="LblDeliverStatus"></pe:ExtendedLabel>
                    </ItemTemplate>
                </pe:TemplateField>
            </Columns>
        </pe:ExtendedGridView>

        <script language="javascript" type="text/javascript">
          function ShowTabs(ID){
               for (i=0;i< 6;i++){
                    if(i == ID){
                        document.getElementById("TabTitle" + i).className="titlemouseover";
                        document.getElementById("Tabs" + i).style.display="";
                    }
                    else{
                        document.getElementById("TabTitle" + i).className="tabtitle";
                        document.getElementById("Tabs" + i).style.display="none";
                    }
               }
          } 
        </script>

        <pe:ExtendedGridView ID="EgvBill" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            CheckBoxFieldHeaderWidth="3%" IsHoldState="True" OnDataBound="EgvBill_OnDataBound"
            OnRowDataBound="EgvBill_OnRowDataBound" SerialText="" ShowFooter="True">
            <FooterStyle CssClass="tdbg" />
            <Columns>
                <asp:BoundField DataField="DateAndTime" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"
                    HeaderText="交易时间" HtmlEncode="False"></asp:BoundField>
                <asp:BoundField DataField="OrderNum" HeaderText="订单号"></asp:BoundField>
                <asp:TemplateField HeaderText="收入金额">
                    <ItemStyle HorizontalAlign="Right" />
                    <ItemTemplate>
                        <asp:Label ID="LblRecieveMoney" runat="server" Text=""></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="支出金额">
                    <ItemStyle HorizontalAlign="Right" />
                    <ItemTemplate>
                        <asp:Label ID="LblPayoutMoney" runat="server" Text=""></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Remark" HeaderText="备注/说明"></asp:BoundField>
            </Columns>
        </pe:ExtendedGridView>
    </div>
    <asp:ObjectDataSource ID="OdsInfo" runat="server" EnablePaging="True"></asp:ObjectDataSource>
    <asp:Literal ID="LblBankrollItemNotice" runat="server" Text="注意：没确认的资金将不会计入合计当中。" Visible="false" />
</asp:Content>
