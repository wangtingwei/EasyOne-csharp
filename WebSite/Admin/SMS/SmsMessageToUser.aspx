<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Sms.SmsMessageToUser"
    Title="无标题页" Codebehind="SmsMessageToUser.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="server">

    <script type="text/javascript">
        function SelectUser()
        {
            window.open('../User/UserList.aspx?TypeSelect=UserList&OpenerText=<%=TxtUserName.ClientID %>','','width=600,height=450,resizable=0,scrollbars=yes');

        }
    </script>

    <asp:Panel ID="Panel2" runat="server">
        <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
            <tr align="center">
                <td colspan="2" class="spacingtitle">
                    预 览 短 信</td>
            </tr>
            <tr class="tdbg" valign="top">
                <td>
                    下面是根据您指定的条件查找到的接收人：<br />
                    <asp:TextBox ID="TxtSendNum" TextMode="multiline" runat="server" Width="260px" Height="301px"></asp:TextBox>
                </td>
                <td>
                    <b>短信内容：</b><br />
                    <asp:TextBox ID="TxtContent" TextMode="multiline" ReadOnly="true" Width="360px" runat="server"
                        Height="150px"></asp:TextBox>
                    <br />
                    <b>短信统计：</b><br />
                    需要向
                    <asp:Label ID="LabRecieverCount" runat="server"></asp:Label>
                    个号码发送
                    <asp:Label ID="LabMessageCount" runat="server"></asp:Label>
                    条短信<br />
                    <br />
                    <b>说明：</b><br />
                    因为每条短信不能超过70个字，所以短信数可能会大于号码数。<br />
                    因为短信内容中的变量替换等原因，可能会导致实际发送的短<br />
                    信数会超过这里计算的短信数，最终结果以动易短信通平台上<br />
                    的实际发送数目为准。</td>
            </tr>
            <tr class="tdbgbottom">
                <td colspan="2">
                    <asp:Button ID="BtnSubmitServer" runat="server" Text="提交给短信服务器" OnClick="BtnSubmitServer_Click" />
                    <asp:HiddenField ID="SendTiming" runat="server" />
                    <asp:HiddenField ID="SendTime" runat="server" />
                    <asp:HiddenField ID="MD5String" runat="server" />
                    <asp:HiddenField ID="Reserve" runat="server" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="Panel1" runat="server">
        <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
            <tr align="center">
                <td colspan="2" class="spacingtitle">
                    给会员发送手机短信
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>接收人：</strong></td>
                <td>
                    <table>
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
                                <asp:RadioButton ID="RadIncept3" runat="server" GroupName="InceptGroup" />指定会员名
                            </td>
                            <td>
                                <asp:TextBox ID="TxtUserName" runat="server" Width="270px"></asp:TextBox><span style="color: #0000ff">&lt;=【</span><a
                                    href="#" onclick="SelectUser();"><span style="text-decoration: underline; color: Green;">会员列表</span></a><span
                                        style="color: #0000ff">】</span>多个用户名间请用英文的逗号分隔
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:RadioButton ID="RadIncept4" runat="server" GroupName="InceptGroup" />指定会员ID范围
                            </td>
                            <td>
                                起始会员ID：<asp:TextBox ID="TxtStartUser" runat="server" Width="50px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                                终止会员ID：<asp:TextBox ID="TxtEndUser" runat="server" Width="50px"></asp:TextBox>
                                <asp:CompareValidator ID="CompareValidator1" ControlToValidate="TxtEndUser" ControlToCompare="TxtStartUser"
                                    Operator="GreaterThanEqual" runat="server" Display="dynamic" ErrorMessage="起始会员ID必须小于等于终止会员ID！"></asp:CompareValidator>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>短信内容：</strong></td>
                <td>
                    可以在短信内容中使用几个变量：<br />
                    {$1}：手机号码或小灵通号码<br />
                    {$2}：真实姓名<br />
                    {$3}：会员名<br />
                    <asp:TextBox ID="TxtMessage" TextMode="MultiLine" runat="server" Height="87px" Width="337px"></asp:TextBox>
                    <pe:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="TxtMessage"
                        Display="dynamic" runat="server"></pe:RequiredFieldValidator><br />
                    每70个字计算为一条短信发送 已经填写的字数：&nbsp;<asp:TextBox ID="TxtMessageNumber" ReadOnly="true" runat="server"
                        Width="60px"></asp:TextBox>&nbsp;字
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>发送时间：</strong></td>
                <td>
                    <asp:RadioButton ID="RadSendType" GroupName="SendType" Text="立即发送" Checked="true"
                        runat="server" />
                    <asp:RadioButton ID="RadTimeSend" GroupName="SendType" Text="定时发送" runat="server" />
                    <pe:DatePicker ID="Dpk" DateFormat="yyyy-MM-dd HH:mm:ss" runat="server"></pe:DatePicker><span style="color:Red">初始定时时间已加1天，如需定时在当天发送，请注意修改日期。</span>
                </td>
            </tr>
            <tr align="center" class="tdbg">
                <td colspan="2" style="height: 50px;">
                    <asp:Button ID="BtnSubmit" runat="server" Text="发送" OnClick="BtnSubmit_Click" /></td>
            </tr>
        </table>

        <script language="javascript" type="text/javascript">
        function checkLength()
        {
            document.getElementById("<%=TxtMessageNumber.ClientID %>").value=document.getElementById("<%=TxtMessage.ClientID %>").value.length;
        }
        </script>

    </asp:Panel>
</asp:Content>
