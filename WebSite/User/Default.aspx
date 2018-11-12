<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.User.Default" Codebehind="Default.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title runat="server">首页</title>
    <script type="text/javascript" src="../JS/jquery.pack.js"></script>
</head>
<body>
    <pe:UserNavigation ID="UserCenterNavigation" Tab="user" runat="server" />
    <pe:ExtendedSiteMapPath ID="YourPosition" SiteMapProvider="UserMapProvider" runat="server" />
    <form id="form1" runat="server">

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

        <div style="text-align: center">
            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr align="center">
                    <td id="TabTitle0" class="titlemouseover" onclick="ShowTabs(0)">
                        会员信息</td>
                    <td id="TabTitle1" class="tabtitle" onclick="ShowTabs(1)">
                        联系信息</td>
                    <td id="TabTitle2" class="tabtitle" onclick="ShowTabs(2)">
                        个人信息</td>
                    <td id="TabTitle3" class="tabtitle" onclick="ShowTabs(3)">
                        业务信息</td>
                    <td id="TabTitle4" class="tabtitle" style="<%= IsShow()%>" onclick="ShowTabs(4)">
                        单位信息</td>
                    <td id="TabTitle5" class="tabtitle" style="<%= IsShow()%>" onclick="ShowTabs(5)">
                        单位成员</td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
            <table width="100%" border="0" cellpadding="5" cellspacing="1" class="border" style="text-align: left;">
                <tr class="tdbg">
                    <td style="width: 100%;" valign="top">
                        <table cellpadding="2" cellspacing="1" style="width: 100%; background-color: white;">
                            <tbody id="Tabs0">
                                <tr class="tdbg">
                                    <td style="width: 15%" align="right" class="tdbgleft">
                                        用 户 名：</td>
                                    <td style="width: 30%">
                                        <asp:Label ID="LblUserName" runat="server" Text="" /></td>
                                    <td style="width: 15%" align="right" class="tdbgleft">
                                        邮箱地址：</td>
                                    <td style="width: 40%">
                                        <asp:Label ID="LblEmail" runat="server" Text="" /></td>
                                </tr>
                                <tr class="tdbg">
                                    <td align="right" class="tdbgleft">
                                        会员组别：</td>
                                    <td>
                                        <asp:Label ID="LblGroupName" runat="server" Text="" /></td>
                                    <td align="right" class="tdbgleft">
                                        会员类别：</td>
                                    <td>
                                        <asp:Label ID="LblUserType" runat="server" Text="" /></td>
                                </tr>
                                <tr class="tdbg" runat="server" id="BalancePoint">
                                    <td align="right" class="tdbgleft">
                                        资金余额：</td>
                                    <td>
                                        <asp:Label ID="LblBalance" runat="server" Text="" />
                                        元</td>
                                    <td style="width: 15%" align="right" class="tdbgleft">
                                        可用<pe:ShowPointName ID="ShowPointName1" runat="server" />数：</td>
                                    <td>
                                        <asp:Label ID="LblUserPoint" runat="server" Text="" />
                                        <pe:ShowPointName ID="ShowPointName2" runat="server" PointType ="PointUnit" /></td>
                                </tr>
                                <tr class="tdbg" runat="server" id="ExpValid">
                                    <td align="right" class="tdbgleft">
                                        可用积分：</td>
                                    <td>
                                        <asp:Label ID="LblUserExp" runat="server" Text="" />
                                        分</td>
                                    <td align="right" class="tdbgleft">
                                        剩余天数：</td>
                                    <td>
                                        <pe:ExtendedLabel HtmlEncode="false" ID="LblValidNum" runat="server" Text="" />
                                        天</td>
                                </tr>
                                <tr class="tdbg">
                                    <td align="right" class="tdbgleft">
                                        待签文章：</td>
                                    <td>
                                        <asp:Label ID="LblUnsignedItems" runat="server" Text="" />
                                        篇</td>
                                    <td align="right" class="tdbgleft">
                                        待阅短信：</td>
                                    <td>
                                        <asp:Label ID="LblUnreadMsg" runat="server" Text="" />
                                        条</td>
                                </tr>
                                <tr class="tdbg">
                                    <td align="right" class="tdbgleft">
                                        会员权限：</td>
                                    <td>
                                        <asp:Label ID="LblSpecialPermission" runat="server" Text="" /></td>
                                    <td align="right" class="tdbgleft">
                                        <asp:Label ID="LblAuditingCompanyMemberCountTitle" runat="server" Visible="false"
                                            Text="待审成员：" />
                                    </td>
                                    <td>
                                        <asp:Label ID="LblAuditingCompanyMemberCount" Visible="false" runat="server" />
                                    </td>
                                </tr>
                                <tr class="tdbg">
                                    <td align="right" class="tdbgleft">
                                        注册日期：</td>
                                    <td>
                                        <asp:Label ID="LblRegTime" runat="server" /></td>
                                    <td align="right" class="tdbgleft">
                                        加入日期：</td>
                                    <td>
                                        <asp:Label ID="LblJoinTime" runat="server" /></td>
                                </tr>
                                <tr class="tdbg">
                                    <td align="right" class="tdbgleft">
                                        最后登录时间：</td>
                                    <td>
                                        <asp:Label ID="LblLastLoginTime" runat="server" /></td>
                                    <td align="right" class="tdbgleft">
                                        最后登录IP：</td>
                                    <td>
                                        <asp:Label ID="LblLastLoginIP" runat="server" /></td>
                                </tr>
                            </tbody>
                            <tbody id="Tabs1" style="display: none">
                                <tr class="tdbg">
                                    <td class="tdbgleft" style="width: 15%" align="right">
                                        真实姓名：</td>
                                    <td style="width: 30%">
                                        <asp:Label ID="LblTrueName" runat="server" /></td>
                                    <td class="tdbgleft" align="right" style="width: 15%">
                                        称谓：</td>
                                    <td style="width: 40%">
                                        <asp:Label ID="LblTitle" runat="server" /></td>
                                </tr>
                                <tr class="tdbg">
                                    <td class="tdbgleft" align="right">
                                        国家/地区：</td>
                                    <td>
                                        <asp:Label ID="LblCountry" runat="server" /></td>
                                    <td class="tdbgleft" align="right">
                                        省/市：</td>
                                    <td>
                                        <asp:Label ID="LblProvince" runat="server" Text="" /></td>
                                </tr>
                                <tr class="tdbg">
                                    <td class="tdbgleft" align="right">
                                        市/县/区：</td>
                                    <td>
                                        <asp:Label ID="LblCity" runat="server" Text="" /></td>
                                    <td class="tdbgleft" align="right">
                                        邮政编码：</td>
                                    <td>
                                        <asp:Label ID="LblZipCode" runat="server" Text="" /></td>
                                </tr>
                                <tr class="tdbg">
                                    <td class="tdbgleft" align="right" style="height: 23px">
                                        联系地址：</td>
                                    <td colspan="3" style="height: 23px" align="left">
                                        <asp:Label ID="LblAddress" runat="server" Text="" /></td>
                                </tr>
                                <tr class="tdbg">
                                    <td class="tdbgleft" align="right">
                                        办公电话：</td>
                                    <td>
                                        <asp:Label ID="LblOfficePhone" runat="server" Text="" /></td>
                                    <td class="tdbgleft" align="right">
                                        住宅电话：</td>
                                    <td>
                                        <asp:Label ID="LblHomephone" runat="server" Text="" /></td>
                                </tr>
                                <tr class="tdbg">
                                    <td class="tdbgleft" align="right">
                                        移动电话：</td>
                                    <td>
                                        <asp:Label ID="LblMobile" runat="server" Text="" /></td>
                                    <td class="tdbgleft" align="right">
                                        传真号码：</td>
                                    <td>
                                        <asp:Label ID="LblFax" runat="server" Text="" /></td>
                                </tr>
                                <tr class="tdbg">
                                    <td class="tdbgleft" align="right">
                                        小灵通：</td>
                                    <td>
                                        <asp:Label ID="LblPHS" runat="server" Text="" /></td>
                                    <td class="tdbgleft" align="right">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr class="tdbg">
                                    <td class="tdbgleft" align="right">
                                        个人主页：</td>
                                    <td>
                                        <asp:Label ID="LblHomePage" runat="server" Text="http://" /></td>
                                    <td class="tdbgleft" align="right">
                                        Email地址：</td>
                                    <td>
                                        <asp:Label ID="LbllEmail" runat="server" Text="" /></td>
                                </tr>
                                <tr class="tdbg">
                                    <td class="tdbgleft" align="right">
                                        QQ号码：</td>
                                    <td>
                                        <asp:Label ID="LblQQ" runat="server" Text="" /></td>
                                    <td class="tdbgleft" align="right">
                                        MSN帐号：</td>
                                    <td>
                                        <asp:Label ID="LblMSN" runat="server" Text="" /></td>
                                </tr>
                                <tr class="tdbg">
                                    <td class="tdbgleft" align="right">
                                        ICQ号码：</td>
                                    <td>
                                        <asp:Label ID="LblICQ" runat="server" Text="" /></td>
                                    <td class="tdbgleft" align="right">
                                        雅虎通帐号：</td>
                                    <td>
                                        <asp:Label ID="LblYahoo" runat="server" Text="" /></td>
                                </tr>
                                <tr class="tdbg">
                                    <td class="tdbgleft" align="right">
                                        UC帐号：</td>
                                    <td>
                                        <asp:Label ID="LblUC" runat="server" Text="" /></td>
                                    <td class="tdbgleft" align="right">
                                        Aim帐号：</td>
                                    <td>
                                        <asp:Label ID="LblAim" runat="server" Text="" /></td>
                                </tr>
                            </tbody>
                            <tbody id="Tabs2" style="display: none">
                                <tr class="tdbg">
                                    <td class="tdbgleft" align="right" style="width: 15%;">
                                        出生日期：</td>
                                    <td style="width: 30%;">
                                        <asp:Label ID="LblBirthday" runat="server" Text="" /></td>
                                    <td class="tdbgleft" align="right" style="width: 15%;">
                                        证件号码：</td>
                                    <td style="width: 40%;">
                                        <asp:Label ID="LblIDCard" runat="server" Text="" /></td>
                                </tr>
                                <tr class="tdbg">
                                    <td class="tdbgleft" align="right">
                                        籍贯：</td>
                                    <td>
                                        <asp:Label ID="LblNativePlace" runat="server" Text="" /></td>
                                    <td class="tdbgleft" align="right">
                                        民族：</td>
                                    <td>
                                        <asp:Label ID="LblNation" runat="server" Text="" /></td>
                                </tr>
                                <tr class="tdbg">
                                    <td class="tdbgleft" align="right">
                                        性别：</td>
                                    <td>
                                        <asp:Label ID="LblSex" runat="server" Text="" /></td>
                                    <td class="tdbgleft" align="right">
                                        婚姻状况：</td>
                                    <td>
                                        <asp:Label ID="LblMarriage" runat="server" Text="" /></td>
                                </tr>
                                <tr class="tdbg">
                                    <td class="tdbgleft" align="right">
                                        学历：</td>
                                    <td>
                                        <asp:Label ID="LblEducation" runat="server" Text="" /></td>
                                    <td class="tdbgleft" align="right">
                                        毕业学校：</td>
                                    <td>
                                        <asp:Label ID="LblGraduateFrom" runat="server" Text="" /></td>
                                </tr>
                                <tr class="tdbg">
                                    <td class="tdbgleft" align="right">
                                        生活爱好：</td>
                                    <td>
                                        <asp:Label ID="LblInterestsOfLife" runat="server" Text="" /></td>
                                    <td class="tdbgleft" align="right">
                                        文化爱好：</td>
                                    <td>
                                        <asp:Label ID="LblInterestsOfCulture" runat="server" Text="" /></td>
                                </tr>
                                <tr class="tdbg">
                                    <td class="tdbgleft" align="right">
                                        娱乐休闲爱好：</td>
                                    <td>
                                        <asp:Label ID="LblInterestsOfAmusement" runat="server" Text="" /></td>
                                    <td class="tdbgleft" align="right">
                                        体育爱好：</td>
                                    <td>
                                        <asp:Label ID="LblInterestsOfSport" runat="server" Text="" /></td>
                                </tr>
                                <tr class="tdbg">
                                    <td class="tdbgleft" align="right">
                                        其他爱好：</td>
                                    <td>
                                        <asp:Label ID="LblInterestsOfOther" runat="server" Text="" /></td>
                                    <td class="tdbgleft" align="right">
                                        月 收 入：</td>
                                    <td>
                                        <asp:Label ID="LblIncome" runat="server" Text="" /></td>
                                </tr>
                            </tbody>
                            <tbody id="Tabs3" style="display: none">
                                <tr class="tdbg">
                                    <td class="tdbgleft" align="right" style="width: 15%">
                                        单位名称：</td>
                                    <td style="width: 30%">
                                        <asp:Label ID="LblCompany" runat="server" Text="" /></td>
                                    <td class="tdbgleft" align="right" style="width: 15%">
                                        所属部门：</td>
                                    <td style="width: 40%">
                                        <asp:Label ID="LblDepartment" runat="server" Text="" /></td>
                                </tr>
                                <tr class="tdbg">
                                    <td class="tdbgleft" align="right">
                                        职位：</td>
                                    <td>
                                        <asp:Label ID="LblPosition" runat="server" Text="" /></td>
                                    <td class="tdbgleft" align="right">
                                        负责业务：</td>
                                    <td>
                                        <asp:Label ID="LblOperation" runat="server" Text="" /></td>
                                </tr>
                                <tr class="tdbg">
                                    <td class="tdbgleft" align="right">
                                        单位地址：</td>
                                    <td colspan="3">
                                        <asp:Label ID="LblCompanyAddress" runat="server" Text="" /></td>
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
                    </td>
                </tr>
            </table>
            <div id="Main_Operation">
                <ul>
                    <li>
                        <asp:Button ID="BtnOrder" runat="server" Text="订单信息" OnClick="BtnOrder_Click" Width="100" />
                        <asp:Button ID="BtnBankroll" runat="server" Text="资金明细" OnClick="BtnBankroll_Click" Width="100" />
                        <asp:Button ID="BtnRechargeLog" runat="server" Text="有效期明细" OnClick="BtnRechargeLog_Click" Width="100" />
                        <asp:Button ID="BtnConsumeLog" runat="server" Text="点券明细" OnClick="BtnConsumeLog_Click" Width="100" />
                        <asp:Button ID="BtnPayment" runat="server" Text="在线支付明细" OnClick="BtnPayment_Click" Width="100" />
                    </li>
                </ul>
                <ul>
                    <li>
                        <asp:Button ID="BtnPayOnline" runat="server" Text="在线支付" OnClick="BtnPayOnline_Click" Width="100" />
                        <asp:Button ID="BtnRecharge" runat="server" Text="充值卡充值" OnClick="BtnRecharge_Click" Width="100" />
                        <asp:Button ID="BtnExchangePoint" runat="server" Text="兑换点券" OnClick="BtnExchangePoint_Click" Width="100" />
                        <asp:Button ID="BtnExchangeValidDate" runat="server" Text="兑换有效期" OnClick="BtnExchangeValidDate_Click" Width="100" />
                        <asp:Button ID="BtnRemitValidate" runat="server" OnClick="BtnRemitValidate_Click"
                            Text="汇款确认" Width="100px" /></li>
                </ul>
                <ul>
                    <li>
                        <asp:Button ID="BtnMessage" runat="server" Text="我的短消息" OnClick="BtnMessage_Click" Width="100" />
                        <asp:Button ID="BtnReceive" runat="server" Text="我的签收文章" OnClick="BtnReceive_Click" Width="100" />
                        <asp:Button ID="BtnModifyPassword" runat="server" Text="修改密码" OnClick="BtnModifyPassword_Click" Width="100" />
                        <asp:Button ID="BtnModifyUser" runat="server" Text="修改信息" OnClick="BtnModifyUser_Click" Width="100" />
                        <asp:Button ID="BtnShoppingCart" runat="server" Text="我的购物车" OnClick="BtnShoppingCart_Click" Width="100" />
                    </li>
                </ul>
                <ul>
                    <li>
                        <asp:Button ID="BtnRegCompany" Visible="false" runat="server" Text="注册我的企业" OnClick="BtnRegCompany_Click" Width="100" />
                        <asp:Button ID="BtnExitCompany" Visible="false" runat="server" OnClientClick="return confirm('确定要退出当前企业吗？')"
                            Text="退出企业" OnClick="BtnExitCompany_Click" Width="100" />
                        <asp:Button ID="BtnDelCompany" Visible="false" runat="server" OnClientClick="return confirm('确定要注销您的企业吗？一旦注销，所有成员都将变成个人会员。')"
                            Text="注销企业" OnClick="BtnDelCompany_Click" Width="100" />
                    </li>
                </ul>
            </div>
        </div>
        <asp:HiddenField ID="HdnLockType" runat="server" />
    </form>
</body>
</html>
