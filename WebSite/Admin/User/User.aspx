<%@ Page Language="C#" AutoEventWireup="True" MasterPageFile="~/Admin/MasterPage.master"
    Inherits="EasyOne.WebSite.Admin.User.UserUI" Title="用户管理" Codebehind="User.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <asp:ScriptManager ID="SmgeRegion" runat="server" EnablePartialRendering="true">
    </asp:ScriptManager>

    <script language="javascript" type="text/javascript">
          function ShowTabs(ID){
               for (i=0;i< 5;i++){
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
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <table width="100%" border="0" cellpadding="5" cellspacing="1" class="border">
        <tr class="tdbg">
            <td valign="top">
                <table width="100%" cellpadding="2" cellspacing="1" style="background-color: white;">
                    <tbody id="Tabs0">
                        <tr class="tdbg">
                            <td class="tdbgleft" style="text-align:right">
                                会 员 组：
                            </td>
                            <td align="left" colspan="3">
                                <asp:DropDownList ID="DropGroupId" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td class="tdbgleft" style="text-align:right">
                                用 户 名：</td>
                            <td align="left">
                                <asp:TextBox ID="TxtUserName" runat="server" Width="150px"></asp:TextBox>
                                <pe:RequiredFieldValidator ID="ValrUserName" ControlToValidate="TxtUserName" runat="server"
                                    ErrorMessage="用户名不能为空！" Display="Dynamic"></pe:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="ValeUserName" runat="server" ControlToValidate="TxtUserName"
                                    ErrorMessage="不能包含特殊字符  如@，#，$，%，^，&，*，(，)，'，?，{，}，[，]，;，:等" ValidationExpression="^[^@#$%^&*()'?{}\[\];:]*$"
                                    Display="Dynamic"></asp:RegularExpressionValidator>
                                
                            </td>
                            <td class="tdbgleft" style="text-align:right">
                                用户密码：</td>
                            <td align="left">
                                <asp:TextBox ID="TxtUserPassword" runat="server" Width="150px" TextMode="password" ></asp:TextBox>
                                <pe:RequiredFieldValidator ID="ValrUserPassword" ControlToValidate="TxtUserPassword"
                                    runat="server" ErrorMessage="密码不能为空！" Display="Dynamic"></pe:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionPassword" runat="server" ControlToValidate="TxtUserPassword"
                            ErrorMessage="请输入新密码(至少6位)！" ValidationExpression="^.{6,}$" Display="Dynamic"></asp:RegularExpressionValidator>
                                <ajaxToolkit:PasswordStrength ID="PasswordStrength2" runat="server" TargetControlID="TxtUserPassword"
                                    StrengthIndicatorType="BarIndicator" BarIndicatorCssClass="BarIndicator_TxtUserPassword"
                                    BarBorderCssClass="BarBorder_TxtUserPassword" PreferredPasswordLength="8" MinimumNumericCharacters="1"
                                    MinimumSymbolCharacters="1" RequiresUpperAndLowerCaseCharacters="true" DisplayPosition="RightSide" />
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td class="tdbgleft" style="text-align:right">
                                提示问题：</td>
                            <td align="left">
                                <asp:TextBox ID="TxtQuestion" runat="server" Width="150px"></asp:TextBox>
                                <pe:RequiredFieldValidator ID="ValrQuestion" ControlToValidate="TxtQuestion" runat="server"
                                    ErrorMessage="提示问题不能为空！" Display="Dynamic"></pe:RequiredFieldValidator>
                            </td>
                            <td class="tdbgleft" style="text-align:right">
                                提示答案：</td>
                            <td align="left">
                                <asp:TextBox ID="TxtAnswer" runat="server" Width="150px"></asp:TextBox>
                                <pe:RequiredFieldValidator ID="ValrAnswer" ControlToValidate="TxtAnswer" runat="server"
                                    ErrorMessage="提示答案不能为空！" Display="Dynamic"></pe:RequiredFieldValidator><asp:PlaceHolder
                                        ID="PhAnswer" runat="server" Visible="false"><span style="color: Green">不修改请留空</span></asp:PlaceHolder>
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td class="tdbgleft" style="text-align:right">
                                电子邮件：</td>
                            <td align="left">
                                <asp:TextBox ID="TxtEmail" runat="server" Width="150px"></asp:TextBox>
                                <pe:RequiredFieldValidator ID="ValrEmail" ControlToValidate="TxtEmail" runat="server"
                                    ErrorMessage="Email不能为空！" Display="Dynamic"></pe:RequiredFieldValidator>
                                <pe:EmailValidator ID="Vmail" runat="server" ControlToValidate="TxtEmail" Display="Dynamic"
                                    ErrorMessage="电子邮件格式不对"></pe:EmailValidator></td>
                            <td class="tdbgleft" style="text-align:right">
                                隐私设定：</td>
                            <td align="left">
                                <asp:RadioButtonList ID="RadlPrivacySetting" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Selected="True" Value="0">全部公开</asp:ListItem>
                                    <asp:ListItem Value="1">部分公开</asp:ListItem>
                                    <asp:ListItem Value="2">完全保密</asp:ListItem>
                                </asp:RadioButtonList></td>
                        </tr>
                        <tr class="tdbg">
                            <td class="tdbgleft" style="text-align:right">
                                头像地址：</td>
                            <td align="left">
                                <asp:TextBox ID="TxtUserFace" runat="server" Width="150px"></asp:TextBox>
                            </td>
                            <td class="tdbgleft" style="text-align:right">
                                头像宽度：</td>
                            <td align="left">
                                <asp:TextBox ID="TxtFaceWidth" runat="server" Columns ="4" MaxLength ="4"></asp:TextBox>
                                像素
                                <pe:NumberValidator ID="NumberValidatorFaceWidth" ControlToValidate="TxtFaceWidth"
                                    runat="server">
                                </pe:NumberValidator>
                            </td>
                        </tr>
                        <tr class="tdbg" valign="top">
                            <td class="tdbgleft" style="text-align:right">
                                签名信息：</td>
                            <td align="left">
                                <asp:TextBox ID="TxtSign" runat="server" Height="74px" TextMode="MultiLine" Width="288px"></asp:TextBox></td>
                            <td class="tdbgleft" style="text-align:right">
                                头像高度：</td>
                            <td align="left">
                                <asp:TextBox ID="TxtFaceHeight" runat="server" Columns ="4" MaxLength ="4"></asp:TextBox>
                                像素
                                <pe:NumberValidator ID="NumberValidatorFaceHeight" ControlToValidate="TxtFaceHeight"
                                    runat="server">
                                </pe:NumberValidator>
                            </td>
                        </tr>
                    </tbody>
                    <tbody id="Tabs1" style="display: none">
                        <tr class="tdbg">
                            <td class="tdbgleft" style="text-align:right">
                                真实姓名：</td>
                            <td align="left">
                                <asp:TextBox ID="TxtTrueName" runat="server" Width="150px"></asp:TextBox>
                            </td>
                            <td class="tdbgleft" style="text-align:right">
                                称谓：</td>
                            <td align="left">
                                <asp:TextBox ID="TxtTitle" runat="server" Width="150px"></asp:TextBox>
                            </td>
                        </tr>
                        <pe:LiaisonInformation ID="LiaisonInformation1" runat="server"></pe:LiaisonInformation>
                    </tbody>
                    <tbody id="Tabs2" style="display: none">
                        <pe:PersonalInformation ID="PersonalInformation1" runat="server" />
                    </tbody>
                    <tbody id="Tabs3" style="display: none">
                        <tr class="tdbg">
                            <td class="tdbgleft" style="width: 12%;text-align:right">
                                单位名称：</td>
                            <td align="left">
                                <asp:TextBox ID="TxtCompany" runat="server" Width="150px"></asp:TextBox></td>
                            <td class="tdbgleft" style="width: 12%;text-align:right">
                                所属部门：</td>
                            <td style="width: 38%;" align="left">
                                <asp:TextBox ID="TxtDepartment" runat="server" Width="150px"></asp:TextBox></td>
                        </tr>
                        <tr class="tdbg">
                            <td class="tdbgleft" style="text-align:right">
                                职位：</td>
                            <td align="left">
                                <asp:TextBox ID="TxtPosition" runat="server" Width="150px"></asp:TextBox></td>
                            <td class="tdbgleft" style="text-align:right">
                                负责业务：</td>
                            <td align="left">
                                <asp:TextBox ID="TxtOperation" runat="server" Width="150px"></asp:TextBox></td>
                        </tr>
                        <tr class="tdbg">
                            <td class="tdbgleft" style="text-align:right">
                                单位地址：</td>
                            <td colspan="3" align="left">
                                <asp:TextBox ID="TxtCompanyAddress" runat="server" Width="150px"></asp:TextBox></td>
                        </tr>
                    </tbody>
                    <tbody id="Tabs4" style="display: none">
                        <pe:Company ID="Company1" Visible="false" runat="server"></pe:Company>
                    </tbody>
                </table>
            </td>
        </tr>
    </table>
    <table width="100%" border="0" cellpadding="5" cellspacing="1">
        <tr align="center">
            <td style="height: 40px;">
                <asp:HiddenField ID="HdnContacterID" runat="server" />
                <pe:ExtendedButton ID="EBtnSubmit" Text="保存会员信息" IsChecked="true" OperateCode="UserModify"
                    OnClick="EBtnSubmit_Click" runat="server" IsShowTabs="true" />&nbsp;&nbsp;&nbsp;
                <asp:Button ID="BtnCancel" runat="server" Text="取消" OnClick="BtnCancel_Click" ValidationGroup="BtnCancel" />
            </td>
        </tr>
    </table>
</asp:Content>
