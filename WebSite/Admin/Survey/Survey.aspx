<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Survey.SurveyUI" Title="调查问卷添加与修改页" Codebehind="Survey.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" runat="server" SiteMapProvider="AdminMapProvider" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <div>
        <table style="text-align: center; width: 100%" border="0" cellpadding="5" cellspacing="1"
            class="border">
            <tr class="title">
                <td colspan="2" style="text-align: center;">
                    <asp:Label ID="LblTitle" runat="server"></asp:Label></td>
            </tr>
            <tr class="tdbg">
                <td align="left" class="tdbgleft">
                    <strong>问卷名称：</strong></td>
                <td style="width: 600px; text-align: left;">
                    <asp:TextBox ID="TxtSurveyName" runat="server" MaxLength="60" Width="339px"></asp:TextBox>
                    <pe:RequiredFieldValidator ID="ValrSurveyName" runat="server" ControlToValidate="TxtSurveyName"
                        Display="Dynamic" ErrorMessage="问卷名称不能为空！" SetFocusOnError="True"></pe:RequiredFieldValidator></td>
            </tr>
            <tr class="tdbg">
                <td align="left" class="tdbgleft">
                    <strong>问卷描述：</strong></td>
                <td style="width: 600px; text-align: left;">
                    <asp:TextBox ID="TxtDescription" runat="server" Height="92px" TextMode="MultiLine"
                        Width="407px"></asp:TextBox></td>
            </tr>
            <tr class="tdbg">
                <td align="left" class="tdbgleft">
                    <strong>同一IP允许重复提交次数：</strong></td>
                <td style="width: 600px; text-align: left;">
                    <asp:TextBox ID="TxtIPRepeat" runat="server" MaxLength="4" Width="70px">1</asp:TextBox>
                    请填写大于0的数字
                    <asp:RangeValidator ID="ValrIPRepeat" runat="server" ControlToValidate="TxtIPRepeat"
                        ErrorMessage="请填写大于0的数字" MaximumValue="9999" MinimumValue="1" SetFocusOnError="True"
                        Display="Dynamic"></asp:RangeValidator></td>
            </tr>
            <tr class="tdbg">
                <td align="left" class="tdbgleft">
                    <strong>只有登录后才能投票：</strong></td>
                <td style="width: 600px; text-align: left;">
                    <asp:RadioButton ID="RadNeedLogin1" runat="server" GroupName="NeedLogin" Text="是" />&nbsp;
                    <asp:RadioButton ID="RadNeedLogin0" runat="server" GroupName="NeedLogin" Text="否"
                        Checked="True" /></td>
            </tr>
            <tr id="TrEncourage" class="tdbg" style="display: none">
                <td align="left" class="tdbgleft">
                    <strong>注册会员参与者奖励点数：</strong></td>
                <td style="width: 600px; text-align: left;">
                    <asp:TextBox ID="TxtPresentPoint" runat="server" MaxLength="10" Width="70px">0</asp:TextBox>如果点数大于0，则注册会员填写问卷时增加相应点数
                    <asp:CompareValidator ID="ValcPressentPoint" runat="server" ControlToValidate="TxtPresentPoint"
                        ErrorMessage="请输入数字" SetFocusOnError="True" Display="Dynamic" Operator="DataTypeCheck"
                        Type="Integer"></asp:CompareValidator></td>
            </tr>
            <tr class="tdbg">
                <td align="left" class="tdbgleft">
                    <strong>设置密码限制：</strong><br />
                    <span style="color: Blue">创建问卷后再启用此设置时请重新创建问卷。</span></td>
                <td style="width: 600px; text-align: left;">
                    <asp:TextBox ID="TxtSetPassword" runat="server" MaxLength="30" TextMode="Password"></asp:TextBox>
                    请输入密码，为空时表示不启用此限制
                </td>
            </tr>
            <tr class="tdbg">
                <td align="left" class="tdbgleft" style="width: 40%">
                    <strong>来访限定方式：</strong></td>
                <td style="width: 600px; text-align: left">
                    <asp:RadioButtonList ID="RadlLockIPType" runat="server">
                        <asp:ListItem Value="0" Selected="True">不启用来访限定功能，任何IP都可以访问本问卷。</asp:ListItem>
                        <asp:ListItem Value="1">仅仅启用白名单，只允许白名单中的IP访问本问卷</asp:ListItem>
                        <asp:ListItem Value="2">仅仅启用黑名单，只禁止黑名单中的IP访问本问卷。</asp:ListItem>
                        <asp:ListItem Value="3">同时启用白名单与黑名单，先判断IP是否在白名单中，如果不在，则禁止访问；如果在则再判断是否在黑名单中，如果IP在黑名单中则禁止访问，否则允许访问。</asp:ListItem>
                        <asp:ListItem Value="4">同时启用白名单与黑名单，先判断IP是否在黑名单中，如果不在，则允许访问；如果在则再判断是否在白名单中，如果IP在白名单中则允许访问，否则禁止访问。</asp:ListItem>
                    </asp:RadioButtonList>
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft" style="width: 40%; text-align: left">
                    <strong>IP段白名单</strong>：</td>
                <td class="tdbg" style="width: 600px; text-align: left">
                    &nbsp;<pe:IPLock ID="IPLockWrite" runat="server" />
                </td>
            </tr>
            <tr class="tdbg">
                <td align="left" class="tdbgleft">
                    <strong>IP段黑名单</strong>：</td>
                <td class="tdbg" style="width: 600px; text-align: left">
                    &nbsp;<pe:IPLock ID="IPLockBlack" runat="server" />
                    &nbsp;
                </td>
            </tr>
            <tr class="tdbg">
                <td align="left" class="tdbgleft">
                    <strong>结束日期：</strong></td>
                <td style="width: 600px; text-align: left;">
                    <pe:DatePicker ID="DateEnd" runat="server"></pe:DatePicker></td>
            </tr>
            <tr class="tdbg">
                <td align="left" class="tdbgleft">
                    <strong>问卷模板：</strong></td>
                <td style="width: 600px; text-align: left;">
                    <pe:TemplateSelectControl ID="FscTemplate" runat="server" Width="250px"></pe:TemplateSelectControl>
                    <pe:RequiredFieldValidator ID="ValrFscTemplate" runat="server" ControlToValidate="FscTemplate"
                        Display="Dynamic" ErrorMessage="问卷模板不能为空！" SetFocusOnError="True"></pe:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="tdbg">
                <td align="left" class="tdbgleft">
                    <strong>允许提交的表单页面地址列表：</strong><br />
                    （注：允许所有页面提交，请留空。<br />
                    添加多个限定地址，请用<span style="color: Red">回车</span>分隔。地址书写方式， 如：<span style="color: Blue">http://www.***.**/Survey/200612080903.html</span>
                    就允许了这个地址提交的问卷数据。<span style="color: Red">推荐使用，防止伪造提交！</span>）
                </td>
                <td style="width: 600px; text-align: left;">
                    <asp:TextBox ID="TxtLockUrl" runat="server" Height="127px" TextMode="MultiLine" Width="494px"></asp:TextBox></td>
            </tr>
            <tr class="tdbg">
                <td colspan="2" style="text-align: center; height: 40px">
                    <pe:ExtendedButton IsChecked="true" OperateCode="SurveyQuestionnaireManage" ID="BtnSave"
                        runat="server" Text="保存" OnClick="BtnSave_Click" />
                    &nbsp;&nbsp;
                    <input id="Cancel" name="Cancel" onclick="Redirect('SurveyManage.aspx')" type="button"
                        class="inputbutton" value="取消" />
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="HdnAction" runat="server" />
    &nbsp;

    <script type="text/javascript"> 
      if(<%=RadNeedLogin1.Checked.ToString().ToLower() %>)
      document.getElementById("TrEncourage").style.display='';
    </script>

</asp:Content>
