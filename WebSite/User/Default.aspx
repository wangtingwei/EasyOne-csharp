<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.User.Default" Codebehind="Default.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title runat="server">��ҳ</title>
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
                        ��Ա��Ϣ</td>
                    <td id="TabTitle1" class="tabtitle" onclick="ShowTabs(1)">
                        ��ϵ��Ϣ</td>
                    <td id="TabTitle2" class="tabtitle" onclick="ShowTabs(2)">
                        ������Ϣ</td>
                    <td id="TabTitle3" class="tabtitle" onclick="ShowTabs(3)">
                        ҵ����Ϣ</td>
                    <td id="TabTitle4" class="tabtitle" style="<%= IsShow()%>" onclick="ShowTabs(4)">
                        ��λ��Ϣ</td>
                    <td id="TabTitle5" class="tabtitle" style="<%= IsShow()%>" onclick="ShowTabs(5)">
                        ��λ��Ա</td>
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
                                        �� �� ����</td>
                                    <td style="width: 30%">
                                        <asp:Label ID="LblUserName" runat="server" Text="" /></td>
                                    <td style="width: 15%" align="right" class="tdbgleft">
                                        �����ַ��</td>
                                    <td style="width: 40%">
                                        <asp:Label ID="LblEmail" runat="server" Text="" /></td>
                                </tr>
                                <tr class="tdbg">
                                    <td align="right" class="tdbgleft">
                                        ��Ա���</td>
                                    <td>
                                        <asp:Label ID="LblGroupName" runat="server" Text="" /></td>
                                    <td align="right" class="tdbgleft">
                                        ��Ա���</td>
                                    <td>
                                        <asp:Label ID="LblUserType" runat="server" Text="" /></td>
                                </tr>
                                <tr class="tdbg" runat="server" id="BalancePoint">
                                    <td align="right" class="tdbgleft">
                                        �ʽ���</td>
                                    <td>
                                        <asp:Label ID="LblBalance" runat="server" Text="" />
                                        Ԫ</td>
                                    <td style="width: 15%" align="right" class="tdbgleft">
                                        ����<pe:ShowPointName ID="ShowPointName1" runat="server" />����</td>
                                    <td>
                                        <asp:Label ID="LblUserPoint" runat="server" Text="" />
                                        <pe:ShowPointName ID="ShowPointName2" runat="server" PointType ="PointUnit" /></td>
                                </tr>
                                <tr class="tdbg" runat="server" id="ExpValid">
                                    <td align="right" class="tdbgleft">
                                        ���û��֣�</td>
                                    <td>
                                        <asp:Label ID="LblUserExp" runat="server" Text="" />
                                        ��</td>
                                    <td align="right" class="tdbgleft">
                                        ʣ��������</td>
                                    <td>
                                        <pe:ExtendedLabel HtmlEncode="false" ID="LblValidNum" runat="server" Text="" />
                                        ��</td>
                                </tr>
                                <tr class="tdbg">
                                    <td align="right" class="tdbgleft">
                                        ��ǩ���£�</td>
                                    <td>
                                        <asp:Label ID="LblUnsignedItems" runat="server" Text="" />
                                        ƪ</td>
                                    <td align="right" class="tdbgleft">
                                        ���Ķ��ţ�</td>
                                    <td>
                                        <asp:Label ID="LblUnreadMsg" runat="server" Text="" />
                                        ��</td>
                                </tr>
                                <tr class="tdbg">
                                    <td align="right" class="tdbgleft">
                                        ��ԱȨ�ޣ�</td>
                                    <td>
                                        <asp:Label ID="LblSpecialPermission" runat="server" Text="" /></td>
                                    <td align="right" class="tdbgleft">
                                        <asp:Label ID="LblAuditingCompanyMemberCountTitle" runat="server" Visible="false"
                                            Text="�����Ա��" />
                                    </td>
                                    <td>
                                        <asp:Label ID="LblAuditingCompanyMemberCount" Visible="false" runat="server" />
                                    </td>
                                </tr>
                                <tr class="tdbg">
                                    <td align="right" class="tdbgleft">
                                        ע�����ڣ�</td>
                                    <td>
                                        <asp:Label ID="LblRegTime" runat="server" /></td>
                                    <td align="right" class="tdbgleft">
                                        �������ڣ�</td>
                                    <td>
                                        <asp:Label ID="LblJoinTime" runat="server" /></td>
                                </tr>
                                <tr class="tdbg">
                                    <td align="right" class="tdbgleft">
                                        ����¼ʱ�䣺</td>
                                    <td>
                                        <asp:Label ID="LblLastLoginTime" runat="server" /></td>
                                    <td align="right" class="tdbgleft">
                                        ����¼IP��</td>
                                    <td>
                                        <asp:Label ID="LblLastLoginIP" runat="server" /></td>
                                </tr>
                            </tbody>
                            <tbody id="Tabs1" style="display: none">
                                <tr class="tdbg">
                                    <td class="tdbgleft" style="width: 15%" align="right">
                                        ��ʵ������</td>
                                    <td style="width: 30%">
                                        <asp:Label ID="LblTrueName" runat="server" /></td>
                                    <td class="tdbgleft" align="right" style="width: 15%">
                                        ��ν��</td>
                                    <td style="width: 40%">
                                        <asp:Label ID="LblTitle" runat="server" /></td>
                                </tr>
                                <tr class="tdbg">
                                    <td class="tdbgleft" align="right">
                                        ����/������</td>
                                    <td>
                                        <asp:Label ID="LblCountry" runat="server" /></td>
                                    <td class="tdbgleft" align="right">
                                        ʡ/�У�</td>
                                    <td>
                                        <asp:Label ID="LblProvince" runat="server" Text="" /></td>
                                </tr>
                                <tr class="tdbg">
                                    <td class="tdbgleft" align="right">
                                        ��/��/����</td>
                                    <td>
                                        <asp:Label ID="LblCity" runat="server" Text="" /></td>
                                    <td class="tdbgleft" align="right">
                                        �������룺</td>
                                    <td>
                                        <asp:Label ID="LblZipCode" runat="server" Text="" /></td>
                                </tr>
                                <tr class="tdbg">
                                    <td class="tdbgleft" align="right" style="height: 23px">
                                        ��ϵ��ַ��</td>
                                    <td colspan="3" style="height: 23px" align="left">
                                        <asp:Label ID="LblAddress" runat="server" Text="" /></td>
                                </tr>
                                <tr class="tdbg">
                                    <td class="tdbgleft" align="right">
                                        �칫�绰��</td>
                                    <td>
                                        <asp:Label ID="LblOfficePhone" runat="server" Text="" /></td>
                                    <td class="tdbgleft" align="right">
                                        סլ�绰��</td>
                                    <td>
                                        <asp:Label ID="LblHomephone" runat="server" Text="" /></td>
                                </tr>
                                <tr class="tdbg">
                                    <td class="tdbgleft" align="right">
                                        �ƶ��绰��</td>
                                    <td>
                                        <asp:Label ID="LblMobile" runat="server" Text="" /></td>
                                    <td class="tdbgleft" align="right">
                                        ������룺</td>
                                    <td>
                                        <asp:Label ID="LblFax" runat="server" Text="" /></td>
                                </tr>
                                <tr class="tdbg">
                                    <td class="tdbgleft" align="right">
                                        С��ͨ��</td>
                                    <td>
                                        <asp:Label ID="LblPHS" runat="server" Text="" /></td>
                                    <td class="tdbgleft" align="right">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr class="tdbg">
                                    <td class="tdbgleft" align="right">
                                        ������ҳ��</td>
                                    <td>
                                        <asp:Label ID="LblHomePage" runat="server" Text="http://" /></td>
                                    <td class="tdbgleft" align="right">
                                        Email��ַ��</td>
                                    <td>
                                        <asp:Label ID="LbllEmail" runat="server" Text="" /></td>
                                </tr>
                                <tr class="tdbg">
                                    <td class="tdbgleft" align="right">
                                        QQ���룺</td>
                                    <td>
                                        <asp:Label ID="LblQQ" runat="server" Text="" /></td>
                                    <td class="tdbgleft" align="right">
                                        MSN�ʺţ�</td>
                                    <td>
                                        <asp:Label ID="LblMSN" runat="server" Text="" /></td>
                                </tr>
                                <tr class="tdbg">
                                    <td class="tdbgleft" align="right">
                                        ICQ���룺</td>
                                    <td>
                                        <asp:Label ID="LblICQ" runat="server" Text="" /></td>
                                    <td class="tdbgleft" align="right">
                                        �Ż�ͨ�ʺţ�</td>
                                    <td>
                                        <asp:Label ID="LblYahoo" runat="server" Text="" /></td>
                                </tr>
                                <tr class="tdbg">
                                    <td class="tdbgleft" align="right">
                                        UC�ʺţ�</td>
                                    <td>
                                        <asp:Label ID="LblUC" runat="server" Text="" /></td>
                                    <td class="tdbgleft" align="right">
                                        Aim�ʺţ�</td>
                                    <td>
                                        <asp:Label ID="LblAim" runat="server" Text="" /></td>
                                </tr>
                            </tbody>
                            <tbody id="Tabs2" style="display: none">
                                <tr class="tdbg">
                                    <td class="tdbgleft" align="right" style="width: 15%;">
                                        �������ڣ�</td>
                                    <td style="width: 30%;">
                                        <asp:Label ID="LblBirthday" runat="server" Text="" /></td>
                                    <td class="tdbgleft" align="right" style="width: 15%;">
                                        ֤�����룺</td>
                                    <td style="width: 40%;">
                                        <asp:Label ID="LblIDCard" runat="server" Text="" /></td>
                                </tr>
                                <tr class="tdbg">
                                    <td class="tdbgleft" align="right">
                                        ���᣺</td>
                                    <td>
                                        <asp:Label ID="LblNativePlace" runat="server" Text="" /></td>
                                    <td class="tdbgleft" align="right">
                                        ���壺</td>
                                    <td>
                                        <asp:Label ID="LblNation" runat="server" Text="" /></td>
                                </tr>
                                <tr class="tdbg">
                                    <td class="tdbgleft" align="right">
                                        �Ա�</td>
                                    <td>
                                        <asp:Label ID="LblSex" runat="server" Text="" /></td>
                                    <td class="tdbgleft" align="right">
                                        ����״����</td>
                                    <td>
                                        <asp:Label ID="LblMarriage" runat="server" Text="" /></td>
                                </tr>
                                <tr class="tdbg">
                                    <td class="tdbgleft" align="right">
                                        ѧ����</td>
                                    <td>
                                        <asp:Label ID="LblEducation" runat="server" Text="" /></td>
                                    <td class="tdbgleft" align="right">
                                        ��ҵѧУ��</td>
                                    <td>
                                        <asp:Label ID="LblGraduateFrom" runat="server" Text="" /></td>
                                </tr>
                                <tr class="tdbg">
                                    <td class="tdbgleft" align="right">
                                        ����ã�</td>
                                    <td>
                                        <asp:Label ID="LblInterestsOfLife" runat="server" Text="" /></td>
                                    <td class="tdbgleft" align="right">
                                        �Ļ����ã�</td>
                                    <td>
                                        <asp:Label ID="LblInterestsOfCulture" runat="server" Text="" /></td>
                                </tr>
                                <tr class="tdbg">
                                    <td class="tdbgleft" align="right">
                                        �������а��ã�</td>
                                    <td>
                                        <asp:Label ID="LblInterestsOfAmusement" runat="server" Text="" /></td>
                                    <td class="tdbgleft" align="right">
                                        �������ã�</td>
                                    <td>
                                        <asp:Label ID="LblInterestsOfSport" runat="server" Text="" /></td>
                                </tr>
                                <tr class="tdbg">
                                    <td class="tdbgleft" align="right">
                                        �������ã�</td>
                                    <td>
                                        <asp:Label ID="LblInterestsOfOther" runat="server" Text="" /></td>
                                    <td class="tdbgleft" align="right">
                                        �� �� �룺</td>
                                    <td>
                                        <asp:Label ID="LblIncome" runat="server" Text="" /></td>
                                </tr>
                            </tbody>
                            <tbody id="Tabs3" style="display: none">
                                <tr class="tdbg">
                                    <td class="tdbgleft" align="right" style="width: 15%">
                                        ��λ���ƣ�</td>
                                    <td style="width: 30%">
                                        <asp:Label ID="LblCompany" runat="server" Text="" /></td>
                                    <td class="tdbgleft" align="right" style="width: 15%">
                                        �������ţ�</td>
                                    <td style="width: 40%">
                                        <asp:Label ID="LblDepartment" runat="server" Text="" /></td>
                                </tr>
                                <tr class="tdbg">
                                    <td class="tdbgleft" align="right">
                                        ְλ��</td>
                                    <td>
                                        <asp:Label ID="LblPosition" runat="server" Text="" /></td>
                                    <td class="tdbgleft" align="right">
                                        ����ҵ��</td>
                                    <td>
                                        <asp:Label ID="LblOperation" runat="server" Text="" /></td>
                                </tr>
                                <tr class="tdbg">
                                    <td class="tdbgleft" align="right">
                                        ��λ��ַ��</td>
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
                        <asp:Button ID="BtnOrder" runat="server" Text="������Ϣ" OnClick="BtnOrder_Click" Width="100" />
                        <asp:Button ID="BtnBankroll" runat="server" Text="�ʽ���ϸ" OnClick="BtnBankroll_Click" Width="100" />
                        <asp:Button ID="BtnRechargeLog" runat="server" Text="��Ч����ϸ" OnClick="BtnRechargeLog_Click" Width="100" />
                        <asp:Button ID="BtnConsumeLog" runat="server" Text="��ȯ��ϸ" OnClick="BtnConsumeLog_Click" Width="100" />
                        <asp:Button ID="BtnPayment" runat="server" Text="����֧����ϸ" OnClick="BtnPayment_Click" Width="100" />
                    </li>
                </ul>
                <ul>
                    <li>
                        <asp:Button ID="BtnPayOnline" runat="server" Text="����֧��" OnClick="BtnPayOnline_Click" Width="100" />
                        <asp:Button ID="BtnRecharge" runat="server" Text="��ֵ����ֵ" OnClick="BtnRecharge_Click" Width="100" />
                        <asp:Button ID="BtnExchangePoint" runat="server" Text="�һ���ȯ" OnClick="BtnExchangePoint_Click" Width="100" />
                        <asp:Button ID="BtnExchangeValidDate" runat="server" Text="�һ���Ч��" OnClick="BtnExchangeValidDate_Click" Width="100" />
                        <asp:Button ID="BtnRemitValidate" runat="server" OnClick="BtnRemitValidate_Click"
                            Text="���ȷ��" Width="100px" /></li>
                </ul>
                <ul>
                    <li>
                        <asp:Button ID="BtnMessage" runat="server" Text="�ҵĶ���Ϣ" OnClick="BtnMessage_Click" Width="100" />
                        <asp:Button ID="BtnReceive" runat="server" Text="�ҵ�ǩ������" OnClick="BtnReceive_Click" Width="100" />
                        <asp:Button ID="BtnModifyPassword" runat="server" Text="�޸�����" OnClick="BtnModifyPassword_Click" Width="100" />
                        <asp:Button ID="BtnModifyUser" runat="server" Text="�޸���Ϣ" OnClick="BtnModifyUser_Click" Width="100" />
                        <asp:Button ID="BtnShoppingCart" runat="server" Text="�ҵĹ��ﳵ" OnClick="BtnShoppingCart_Click" Width="100" />
                    </li>
                </ul>
                <ul>
                    <li>
                        <asp:Button ID="BtnRegCompany" Visible="false" runat="server" Text="ע���ҵ���ҵ" OnClick="BtnRegCompany_Click" Width="100" />
                        <asp:Button ID="BtnExitCompany" Visible="false" runat="server" OnClientClick="return confirm('ȷ��Ҫ�˳���ǰ��ҵ��')"
                            Text="�˳���ҵ" OnClick="BtnExitCompany_Click" Width="100" />
                        <asp:Button ID="BtnDelCompany" Visible="false" runat="server" OnClientClick="return confirm('ȷ��Ҫע��������ҵ��һ��ע�������г�Ա������ɸ��˻�Ա��')"
                            Text="ע����ҵ" OnClick="BtnDelCompany_Click" Width="100" />
                    </li>
                </ul>
            </div>
        </div>
        <asp:HiddenField ID="HdnLockType" runat="server" />
    </form>
</body>
</html>
