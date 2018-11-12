<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Register" Codebehind="Register.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>会员注册</title>
</head>
<body>
    <pe:ExtendedSiteMapPath ID="YourPosition" SiteMapProvider="UserMapProvider" runat="server" />
    <form id="form1" runat="server">
        <asp:ScriptManager ID="SmgeRegion" runat="server" EnablePartialRendering="true">
        </asp:ScriptManager>
        <pe:ExtendedLabel HtmlEncode="false" ID="LitControlTreeInfo" runat="server"></pe:ExtendedLabel>
        <asp:Panel ID="PnlRegStep0" runat="server" Visible="false">
            不允许注册，请和网站管理员联系！
        </asp:Panel>
        <asp:Panel ID="PnlRegStep1" runat="server" Visible="false">
            <div style="text-align: center">
                <b>服务条款和声明</b></div>
            <textarea cols="20" rows="2" style="font-weight: normal; font-size: 9pt; width: 98%;
                line-height: normal; font-style: normal; height: 310px; font-variant: normal"
                readonly="readonly"><pe:ExtendedLiteral HtmlEncode="false" ID="LitProtocol" runat="server"></pe:ExtendedLiteral></textarea>
            <div style="text-align: center">
                <asp:Button ID="BtnRegStep1" runat="server" Text="我同意" OnClick="BtnRegStep1_Click" />
                <asp:Button ID="BtnRegStep1NotApprove" runat="server" Text="不同意" OnClick="BtnRegStep1NotApprove_Click" />
            </div>
        </asp:Panel>
        <asp:Panel ID="PnlRegStep2" runat="server" Visible="false">

            <script type="text/javascript">
                    function CheckUser()
                    {
                        var userName = document.getElementById("<%= TxtUserName.ClientID %>");
                        var checkUserNameMessage = document.getElementById("CheckUserNameMessage");
                        
                        if(userName.value=="")
                        {
                            checkUserNameMessage.innerHTML = "用户名为空";
                            checkUserNameMessage.className = "d_err";
                        }
                        else
                        {
                            CallTheServer(userName.value,"");
                        }
                    }
                    
                    function CallTheServer(arg,context)
                    {
                        var checkUserNameMessage = document.getElementById("CheckUserNameMessage");
                        checkUserNameMessage.className = "";
                        checkUserNameMessage.innerHTML = "<img src=\"images/loading.gif\" align=\"absmiddle\" />";
                        
                        <%= callBackReference %>
                    }
                    
                    function ReceiveServerData(result)
                    {
                        var checkUserNameMessage = document.getElementById("CheckUserNameMessage");
                        if(result == "true")
                        {
                            checkUserNameMessage.innerHTML = "用户名已经被注册了";
                            checkUserNameMessage.className = "d_err";
                        }
                        
                        if(result == "disabled")
                        {
                            checkUserNameMessage.innerHTML = "该用户名禁止注册";
                            checkUserNameMessage.className = "d_err";
                        }
                        
                        if(result == "false")
                        {
                            checkUserNameMessage.innerHTML = "恭喜您，用户名可以使用！";
                            checkUserNameMessage.className = "d_ok";
                        }
                    }
                    
                    function SwicthSelectFill()
                    {
                        var selectFill = document.getElementById("<%= TableRegisterSelect.ClientID %>");
                        if(selectFill.style.display=="none")
                        {
                            selectFill.style.display="";
                        }
                        else
                        {
                            selectFill.style.display="none";
                        }
                    }
                    
                    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
                    function EndRequestHandler(sender, args)
                    {
                        if (args.get_error() != undefined){
                        alert("检测到表单中存在HTML代码！");
                        args.set_errorHandled(true);
                        }
                    }
                    
                    //检查是否有相同数据
                    var resultQQ=true,resultMsn=true,resultPage=true,resultOfficePhone=true,resultHomePhone=true,resultMobile=true;
                    var oldQQ="",oldMsn="",oldPage="",oldOfficePhone="",oldHomePhone="",oldMobile="";
                    function CheckSameData(prefix,args,oldvalue,result)
                    {
                        if(args.Value != "" && args.Value != oldvalue){
                             CallBackByCheckSameData( prefix + args.Value);
                        }
                        args.IsValid = result;                    
                    }
                    function CheckSameQQ(source,args)
                    {
                        CheckSameData("$QQ",args,oldQQ,resultQQ);
                        oldQQ = args.Value;
                    }
                    function CheckSameMsn(source,args)
                    {
                        CheckSameData("$Msn",args,oldMsn,resultMsn);
                        oldMsn = args.Value;
                    }
                    function CheckSameHomepage(source,args)
                    {
                       CheckSameData("$Homepage",args,oldPage,resultPage);
                       oldPage = args.Value;
                    }
                    function CheckSameOfficePhone(source,args)
                    {
                        CheckSameData("$OP",args,oldOfficePhone,resultOfficePhone);
                        oldOfficePhone = args.Value;
                    }
                    function CheckSameHomePhone(source,args)
                    {
                        CheckSameData("$HP",args,oldHomePhone,resultHomePhone);
                        oldHomePhone = args.Value;
                    }
                    function CheckSameMobile(source,args)
                    {
                        CheckSameData("$MO",args,oldMobile,resultMobile);
                        oldMobile = args.Value;
                    }

                    function ReceiveServerDataByCheckSameData(arg,context)
                    {
                        var result = eval("("+arg+")") ;
                        switch(result.name)
                        {
                            case 'QQ':
                                resultQQ = !result.value;
                                break;
                            case 'Msn':
                                resultMsn = !result.value;
                                break;
                            case 'Homepage':
                                resultPage = !result.value;
                                break;
                            case 'OP':
                                resultOfficePhone = !result.value;
                                break;
                            case 'HP':
                                resultHomePhone = !result.value;
                                break;
                            case 'MO':
                                resultMobile = !result.value;
                                break;
                            default:                            
                            break;
                        }

                    }
            </script>

            <table id="TableRegisterMust" runat="server" style="border-collapse: collapse" cellspacing="1"
                cellpadding="2" width="100%" border="0">
                <tr>
                    <td class="tdbgleft" style="width: 15%">
                        <b>用户名：</b>
                    </td>
                    <td valign="middle" style="width: 85%">
                        <asp:TextBox ID="TxtUserName" runat="server"></asp:TextBox>
                        <pe:InteractiveMessager ID="InterMessageUserName" ControlToMessage="TxtUserName"
                            ValidatorOkMessage="用户名已输入" runat="server"></pe:InteractiveMessager>
                        <pe:RequiredFieldValidator ID="ReqTxtUserName" runat="server" ControlToValidate="TxtUserName"
                            SetFocusOnError="false" ErrorMessage="用户名不能为空" Display="None"></pe:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="ValgTextMaxLength" ControlToValidate="TxtUserName"
                            ValidationExpression="^[a-zA-Z0-9_\u4e00-\u9fa5]{4,20}$" SetFocusOnError="false"
                            Display="None" runat="server"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td class="tdbgleft" style="width: 15%">
                    </td>
                    <td valign="middle" style="width: 85%">
                        <input id="CheckUserName" style="float: left;" type="button" value="检查用户名是否可用" onclick="CheckUser()" /><span
                            class="d_default" id="CheckUserNameMessage"></span>
                    </td>
                </tr>
                <tr>
                    <td class="tdbgleft" style="width: 15%">
                        <b>密码：</b>
                    </td>
                    <td valign="middle" style="width: 85%">
                        <asp:TextBox ID="TxtPassword" TextMode="Password" runat="server"></asp:TextBox>
                        <pe:InteractiveMessager ID="InteractiveMessagerTxtPassword" Text="请输入密码，区分大小写(至少6位)。尽量设得复杂一些，以防被人暴力猜解"
                            ControlToMessage="TxtPassword" ValidatorOkMessage="密码已输入" runat="server"></pe:InteractiveMessager>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorPassword" runat="server"
                            ControlToValidate="TxtPassword" SetFocusOnError="false" Display="None" ValidationExpression="[\S]{6,}"
                            ErrorMessage="密码至少6位"></asp:RegularExpressionValidator>
                        <pe:RequiredFieldValidator ID="ReqTxtPassword" runat="server" ControlToValidate="TxtPassword"
                            SetFocusOnError="false" Display="None" ErrorMessage="密码不能为空"></pe:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="tdbgleft" style="width: 15%">
                        <strong>确认密码：</strong>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtPwdConfirm" TextMode="Password" runat="server"></asp:TextBox>
                        <pe:InteractiveMessager ID="InteractiveMessagerTxtPwdConfirm" Text="请再输一遍以确认密码" ControlToMessage="TxtPwdConfirm"
                            ValidatorOkMessage="确认密码已输入" runat="server"></pe:InteractiveMessager>
                        <pe:RequiredFieldValidator ID="ReqTxtPwdConfirm" runat="server" ControlToValidate="TxtPwdConfirm"
                            SetFocusOnError="false" Display="None" ErrorMessage="确认密码不能为空"></pe:RequiredFieldValidator><asp:CompareValidator
                                ID="ValCompPassword" runat="server" ControlToValidate="TxtPwdConfirm" ControlToCompare="TxtPassword"
                                Operator="Equal" SetFocusOnError="false" Display="None" ErrorMessage="两次密码输入不一致"></asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td class="tdbgleft" style="width: 15%">
                        <strong>密码问题：</strong>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtQuestion" runat="server"></asp:TextBox>
                        <pe:InteractiveMessager ID="InteractiveMessager1" Text="最好设成别人不知道答案的问题，以防被人用找回密码功能窃取您的帐号。"
                            ControlToMessage="TxtQuestion" ValidatorOkMessage="密码问题已输入" runat="server"></pe:InteractiveMessager>
                        <pe:RequiredFieldValidator ID="ReqTxtQuestion" runat="server" ControlToValidate="TxtQuestion"
                            SetFocusOnError="false" Display="None" ErrorMessage="密码问题不能为空"></pe:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="tdbgleft" style="width: 15%">
                        <strong>问题答案：</strong><br />
                    </td>
                    <td>
                        <asp:TextBox ID="TxtAnswer" runat="server"></asp:TextBox>
                        <pe:InteractiveMessager ID="InteractiveMessagerTxtAnswer" Text="用于取回密码。请尽量设得复杂一些，以防被人用找回密码功能窃取您的帐号。"
                            ControlToMessage="TxtAnswer" ValidatorOkMessage="问题答案已输入" runat="server"></pe:InteractiveMessager>
                        <pe:RequiredFieldValidator ID="ReqAnswer" runat="server" ControlToValidate="TxtAnswer"
                            SetFocusOnError="false" Display="None" ErrorMessage="问题答案不能为空"></pe:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td class="tdbgleft" style="width: 15%">
                        <strong>Email地址：</strong>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtEmail" runat="server"></asp:TextBox>
                        <pe:InteractiveMessager ID="InteractiveMessagerTxtEmail" Text="请输入有效的邮件地址，以便我们可以及时和你联系。"
                            ControlToMessage="TxtEmail" ValidatorOkMessage="Email地址已输入" runat="server"></pe:InteractiveMessager>
                        <pe:RequiredFieldValidator ID="ReqTxtEmail" runat="server" ControlToValidate="TxtEmail"
                            SetFocusOnError="false" Display="None" ErrorMessage="Email不能为空"></pe:RequiredFieldValidator><pe:EmailValidator
                                ID="ValeTxtEmail" runat="server" ControlToValidate="TxtEmail" ErrorMessage="请输入正确的Email"
                                SetFocusOnError="false" Display="None"></pe:EmailValidator>
                    </td>
                </tr>
            </table>
            <table id="TableRegisterSelect" runat="server" style="border-collapse: collapse;
                display: none;" cellspacing="1" cellpadding="2" width="100%" border="0">
                <thead>
                    <tr>
                        <th style="height: 20px;" colspan="2">
                            &nbsp;&nbsp;&nbsp;选填信息</th>
                    </tr>
                </thead>
            </table>
            <table style="border-collapse: collapse" cellspacing="1" cellpadding="2" width="100%"
                border="0">
                <tr class="tdbgleft" style="width: 15%" align="center">
                    <td colspan="2" style="height: 30px">
                        <span style="color: #ff0000">以上所有信息都必须先正确填写后才能继续下一步注册操作。</span></td>
                </tr>
                <tr class="tdbgleft" style="width: 15%" align="center">
                    <td style="height: 30px" colspan="2">
                        <asp:Button ID="BtnRegStep2" runat="server" Text="下一步" OnClick="BtnRegStep2_Click" />
                        <input id="Reset" type="reset" value=" 重新填写 " name="Reset" />
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                            ShowSummary="False" />
                    </td>
                </tr>
            </table>
            <table id="TableRegister" visible="false" runat="server" style="border-collapse: collapse"
                cellspacing="1" cellpadding="2" width="100%" border="0">
                <tr id="TRSwicthSelectFill" runat="server">
                    <td onclick="SwicthSelectFill()" class="tdbgleft" style="width: 15%">
                        <span><b>选填信息：</b></span></td>
                    <td>
                        <input type="checkbox" id="checkSelectFill" onclick="SwicthSelectFill()" /><label
                            for="checkSelectFill" style="color: Blue">显示用户设置选填信息</label>
                    </td>
                </tr>
                <tr runat="server" id="TRSpareEmail">
                    <td class="tdbgleft" style="width: 15%">
                        <strong>备用Email地址：</strong></td>
                    <td>
                        <asp:TextBox ID="TxtSpareEmail" runat="server"></asp:TextBox>
                        <pe:InteractiveMessager ID="InteractiveMessagerTxtSpareEmail" Text="请输入备用Email地址"
                            ControlToMessage="TxtSpareEmail" ValidatorOkMessage="备用Email地址已输入" runat="server"></pe:InteractiveMessager>
                        <pe:EmailValidator ID="EmailValidator1" runat="server" ControlToValidate="TxtSpareEmail"
                            ErrorMessage="请输入正确的Email" SetFocusOnError="false" Display="None">
                        </pe:EmailValidator>
                        <pe:RequiredFieldValidator ID="ReqTxtSpareEmail" runat="server" ControlToValidate="TxtSpareEmail"
                            SetFocusOnError="false" Display="None" ErrorMessage="备用Email地址不能为空"></pe:RequiredFieldValidator></td>
                </tr>
                <tr runat="server" id="TRHomepage">
                    <td class="tdbgleft" style="width: 15%">
                        <strong>主页：</strong></td>
                    <td>
                        <asp:TextBox ID="TxtHomepage" runat="server">http://</asp:TextBox>
                        <pe:InteractiveMessager ID="InteractiveMessagerTxtHomepage" Text="请输入主页地址" ControlToMessage="TxtHomepage"
                            ValidatorOkMessage="主页地址已输入" runat="server"></pe:InteractiveMessager>
                        <pe:RequiredFieldValidator ID="ReqTxtHomepage" runat="server" ControlToValidate="TxtHomepage"
                            SetFocusOnError="false" Display="None" ErrorMessage="主页地址不能为空"></pe:RequiredFieldValidator>
                        <asp:CustomValidator ID="ValcHomepage" runat="server" Display="None" 
                            ErrorMessage="主页地址已有会员使用，请重新输入！" 
                            ClientValidationFunction="CheckSameHomepage" ControlToValidate="TxtHomepage"></asp:CustomValidator>
                    </td>
                </tr>
                <tr runat="server" id="TRQQ">
                    <td class="tdbgleft" style="width: 15%">
                        <strong>QQ号码：</strong></td>
                    <td>
                        <asp:TextBox ID="TxtQQ" runat="server"></asp:TextBox>
                        <pe:InteractiveMessager ID="InteractiveMessagerTxtQQ" Text="请输入QQ号码" ControlToMessage="TxtQQ"
                            ValidatorOkMessage="QQ号码已输入" runat="server"></pe:InteractiveMessager>
                        <pe:RequiredFieldValidator ID="ReqTxtQQ" runat="server" ControlToValidate="TxtQQ"
                            SetFocusOnError="false" Display="None" ErrorMessage="QQ号码不能为空"></pe:RequiredFieldValidator>

                        <asp:CustomValidator ID="ValcCheckSameQQ" runat="server" 
                            ClientValidationFunction="CheckSameQQ" ControlToValidate="TxtQQ" Display="None" 
                            ErrorMessage="QQ号码已被其他会员使用，请重新输入！"></asp:CustomValidator>

                    </td>
                </tr>
                <tr runat="server" id="TRICQ">
                    <td class="tdbgleft" style="width: 15%">
                        <b>ICQ号码：</b></td>
                    <td>
                        <asp:TextBox ID="TxtICQ" runat="server"></asp:TextBox>
                        <pe:InteractiveMessager ID="InteractiveMessagerTxtICQ" Text="请输入ICQ号码" ControlToMessage="TxtICQ"
                            ValidatorOkMessage="ICQ号码已输入" runat="server"></pe:InteractiveMessager>
                        <pe:RequiredFieldValidator ID="ReqTxtICQ" runat="server" ControlToValidate="TxtICQ"
                            SetFocusOnError="false" Display="None" ErrorMessage="ICQ号码不能为空"></pe:RequiredFieldValidator>
                    </td>
                </tr>
                <tr runat="server" id="TRMSN">
                    <td class="tdbgleft" style="width: 15%">
                        <strong>MSN帐号：</strong></td>
                    <td>
                        <asp:TextBox ID="TxtMSN" runat="server"></asp:TextBox>
                        <pe:InteractiveMessager ID="InteractiveMessagerTxtMSN" Text="请输入MSN帐号" ControlToMessage="TxtMSN"
                            ValidatorOkMessage="MSN帐号已输入" runat="server"></pe:InteractiveMessager>
                        <pe:RequiredFieldValidator ID="ReqTxtMSN" runat="server" ControlToValidate="TxtMSN"
                            SetFocusOnError="false" Display="None" ErrorMessage="MSN帐号不能为空"></pe:RequiredFieldValidator>
                        <asp:CustomValidator ID="ValcCheckSameMsn" runat="server" 
                            ClientValidationFunction="CheckSameMsn" ControlToValidate="TxtMSN" 
                            Display="None" ErrorMessage="MSN帐号已被其他会员使用，请重新输入！" ></asp:CustomValidator>
                    </td>
                </tr>
                <tr runat="server" id="TRYahoo">
                    <td class="tdbgleft" style="width: 15%">
                        <b>雅虎通帐号：</b></td>
                    <td>
                        <asp:TextBox ID="TxtYahoo" runat="server"></asp:TextBox>
                        <pe:InteractiveMessager ID="InteractiveMessagerTxtYahoo" Text="请输入雅虎通帐号" ControlToMessage="TxtYahoo"
                            ValidatorOkMessage="雅虎通帐号已输入" runat="server"></pe:InteractiveMessager>
                        <pe:RequiredFieldValidator ID="ReqTxtYahoo" runat="server" ControlToValidate="TxtYahoo"
                            SetFocusOnError="false" Display="None" ErrorMessage="雅虎通帐号不能为空"></pe:RequiredFieldValidator>
                    </td>
                </tr>
                <tr runat="server" id="TRUC">
                    <td class="tdbgleft" style="width: 15%">
                        <b>UC号码：</b></td>
                    <td>
                        <asp:TextBox ID="TxtUC" runat="server"></asp:TextBox>
                        <pe:InteractiveMessager ID="InteractiveMessagerTxtUC" Text="请输入UC号码" ControlToMessage="TxtUC"
                            ValidatorOkMessage="UC号码已输入" runat="server"></pe:InteractiveMessager>
                        <pe:RequiredFieldValidator ID="ReqTxtUC" runat="server" ControlToValidate="TxtUC"
                            SetFocusOnError="false" Display="None" ErrorMessage="UC号码不能为空"></pe:RequiredFieldValidator>
                    </td>
                </tr>
                <tr runat="server" id="TRAim">
                    <td class="tdbgleft" style="width: 15%">
                        <b>Aim帐号：</b></td>
                    <td>
                        <asp:TextBox ID="TxtAim" runat="server"></asp:TextBox>
                        <pe:InteractiveMessager ID="InteractiveMessagerTxtAim" Text="请输入Aim帐号" ControlToMessage="TxtAim"
                            ValidatorOkMessage="Aim帐号已输入" runat="server"></pe:InteractiveMessager>
                        <pe:RequiredFieldValidator ID="ReqTxtAim" runat="server" ControlToValidate="TxtAim"
                            SetFocusOnError="false" Display="None" ErrorMessage="Aim帐号不能为空"></pe:RequiredFieldValidator>
                    </td>
                </tr>
                <tr runat="server" id="TROfficePhone">
                    <td class="tdbgleft" style="width: 15%">
                        <b>办公电话：</b></td>
                    <td>
                        <asp:TextBox ID="TxtOfficePhone" runat="server"></asp:TextBox>
                        <pe:InteractiveMessager ID="InteractiveMessagerTxtOfficePhone" Text="请输入办公电话" ControlToMessage="TxtOfficePhone"
                            ValidatorOkMessage="办公电话已输入" runat="server"></pe:InteractiveMessager>
                        <pe:RequiredFieldValidator ID="ReqTxtOfficePhone" runat="server" ControlToValidate="TxtOfficePhone"
                            SetFocusOnError="false" Display="None" ErrorMessage="办公电话不能为空"></pe:RequiredFieldValidator>
                        <asp:CustomValidator ID="ValcSameOfficePhone" runat="server" 
                            ClientValidationFunction="CheckSameOfficePhone" ControlToValidate="TxtOfficePhone" 
                            Display="None" ErrorMessage="办公电话已被其他会员注册，请重新输入！" ></asp:CustomValidator>
                    </td>
                </tr>
                <tr runat="server" id="TRHomePhone">
                    <td class="tdbgleft" style="width: 15%">
                        <b>家庭电话：</b></td>
                    <td>
                        <asp:TextBox ID="TxtHomePhone" runat="server"></asp:TextBox>
                        <pe:InteractiveMessager ID="InteractiveMessagerTxtHomePhone" Text="请输入家庭电话" ControlToMessage="TxtHomePhone"
                            ValidatorOkMessage="家庭电话已输入" runat="server"></pe:InteractiveMessager>
                        <pe:RequiredFieldValidator ID="ReqTxtHomePhone" runat="server" ControlToValidate="TxtHomePhone"
                            SetFocusOnError="false" Display="None" ErrorMessage="家庭电话不能为空"></pe:RequiredFieldValidator>
                        <asp:CustomValidator ID="ValcSameHomePhone" runat="server" 
                            ClientValidationFunction="CheckSameHomePhone" ControlToValidate="TxtHomePhone" 
                            Display="None" ErrorMessage="家庭电话已被其他会员注册，请重新输入！" ></asp:CustomValidator>

                    </td>
                </tr>
                <tr runat="server" id="TRFax">
                    <td class="tdbgleft" style="width: 15%">
                        <b>传真号码：</b></td>
                    <td>
                        <asp:TextBox ID="TxtFax" runat="server"></asp:TextBox>
                        <pe:InteractiveMessager ID="InteractiveMessagerTxtFax" Text="请输入传真号码" ControlToMessage="TxtFax"
                            ValidatorOkMessage="传真号码已输入" runat="server"></pe:InteractiveMessager>
                        <pe:RequiredFieldValidator ID="ReqTxtFax" runat="server" ControlToValidate="TxtFax"
                            SetFocusOnError="false" Display="None" ErrorMessage="传真号码不能为空"></pe:RequiredFieldValidator>
                    </td>
                </tr>
                <tr runat="server" id="TRMobile">
                    <td class="tdbgleft" style="width: 15%">
                        <b>手机号码：</b></td>
                    <td>
                        <asp:TextBox ID="TxtMobile" runat="server"></asp:TextBox>
                        <pe:InteractiveMessager ID="InteractiveMessagerTxtMobile" Text="请输入手机号码" ControlToMessage="TxtMobile"
                            ValidatorOkMessage="手机号码已输入" runat="server"></pe:InteractiveMessager>
                        <pe:RequiredFieldValidator ID="ReqTxtMobile" runat="server" ControlToValidate="TxtMobile"
                            SetFocusOnError="false" Display="None" ErrorMessage="手机号码不能为空"></pe:RequiredFieldValidator>
                        <pe:MobileValidator ID="MValTxtMobile" runat="server" ControlToValidate="TxtMobile"
                            SetFocusOnError="false" Display="None"></pe:MobileValidator>
                        <asp:CustomValidator ID="ValcSameMobile" runat="server" 
                            ClientValidationFunction="CheckSameMobile" ControlToValidate="TxtMobile" 
                            Display="None" ErrorMessage="手机号码已被其他会员注册，请重新输入！" ></asp:CustomValidator>


                    </td>
                </tr>
                <tr runat="server" id="TRPHS">
                    <td class="tdbgleft" style="width: 15%">
                        <b>小灵通号码：</b></td>
                    <td>
                        <asp:TextBox ID="TxtPHS" runat="server"></asp:TextBox>
                        <pe:InteractiveMessager ID="InteractiveMessagerTxtPHS" Text="请输入小灵通号码" ControlToMessage="TxtPHS"
                            ValidatorOkMessage="小灵通号码已输入" runat="server"></pe:InteractiveMessager>
                        <pe:RequiredFieldValidator ID="ReqTxtPHS" runat="server" ControlToValidate="TxtPHS"
                            SetFocusOnError="false" Display="None" ErrorMessage="小灵通号码不能为空"></pe:RequiredFieldValidator>
                        <pe:TelephoneValidator ID="TValTxtPHS" runat="server" ControlToValidate="TxtPHS"
                            SetFocusOnError="false" Display="None"></pe:TelephoneValidator>
                    </td>
                </tr>
                <tr runat="server" id="TRRegion">
                    <td class="tdbgleft" style="width: 15%">
                        <strong>区域信息：</strong>
                    </td>
                    <td>
                        <pe:Region ID="Region" runat="server" />
                    </td>
                </tr>
                <tr runat="server" id="TRAddress">
                    <td class="tdbgleft" style="width: 15%">
                        <b>联系地址：</b></td>
                    <td>
                        <asp:TextBox ID="TxtAddress" runat="server"></asp:TextBox>
                        <pe:InteractiveMessager ID="InteractiveMessagerTxtAddress" Text="请输入联系地址" ControlToMessage="TxtAddress"
                            ValidatorOkMessage="联系地址已输入" runat="server"></pe:InteractiveMessager>
                        <pe:RequiredFieldValidator ID="ReqTxtAddress" runat="server" ControlToValidate="TxtAddress"
                            SetFocusOnError="false" Display="None" ErrorMessage="联系地址不能为空"></pe:RequiredFieldValidator>
                    </td>
                </tr>
                <tr runat="server" id="TRZipCode">
                    <td class="tdbgleft" style="width: 15%">
                        <b>邮政编码：</b></td>
                    <td>
                        <asp:TextBox ID="TxtZipCode" runat="server"></asp:TextBox>
                        <pe:RequiredFieldValidator ID="ReqTxtZipCode" runat="server" ControlToValidate="TxtZipCode"
                            SetFocusOnError="false" Display="None" ErrorMessage="邮政编码不能为空"></pe:RequiredFieldValidator>
                        <pe:InteractiveMessager ID="InteractiveMessagerTxtZipCode" Text="请输入邮政编码" ControlToMessage="TxtZipCode"
                            ValidatorOkMessage="邮政编码已输入" IsValidEmpty="false" runat="server"></pe:InteractiveMessager>
                        <pe:ZipCodeValidator ID="ZCValorTxtZipCode" runat="server" ControlToValidate="TxtZipCode"
                            ErrorMessage="" Display="None" SetFocusOnError="false"></pe:ZipCodeValidator>
                    </td>
                </tr>
                <tr runat="server" id="TRTrueName">
                    <td class="tdbgleft" style="width: 15%">
                        <b>真实姓名：</b></td>
                    <td>
                        <asp:TextBox ID="TxtTrueName" runat="server"></asp:TextBox>
                        <pe:InteractiveMessager ID="InteractiveMessagerTxtTrueName" Text="请输入真实姓名" ControlToMessage="TxtTrueName"
                            ValidatorOkMessage="真实姓名已输入" IsValidEmpty="false" runat="server"></pe:InteractiveMessager>
                        <pe:RequiredFieldValidator ID="ReqTxtTrueName" runat="server" ControlToValidate="TxtTrueName"
                            SetFocusOnError="false" Display="None" ErrorMessage="真实姓名不能为空"></pe:RequiredFieldValidator>
                    </td>
                </tr>
                <tr runat="server" id="TRSex">
                    <td class="tdbgleft" style="width: 15%">
                        <strong>性别：</strong></td>
                    <td>
                        <asp:DropDownList ID="DropSex" runat="server">
                            <asp:ListItem Text="保密" Value="0"></asp:ListItem>
                            <asp:ListItem Text="男" Value="1"></asp:ListItem>
                            <asp:ListItem Text="女" Value="2"></asp:ListItem>
                        </asp:DropDownList></td>
                </tr>
                <tr runat="server" id="TRBirthday">
                    <td class="tdbgleft" style="width: 15%">
                        <b>出生日期：</b></td>
                    <td>
                        <pe:DatePicker ID="TxtBirthday" runat="server"></pe:DatePicker>
                        <pe:InteractiveMessager ID="InteractiveMessagerTxtBirthday" Text="请输入出生日期" ControlToMessage="TxtBirthday"
                            ValidatorOkMessage="出生日期已输入" IsValidEmpty="false" runat="server"></pe:InteractiveMessager>
                        <pe:RequiredFieldValidator ID="ReqTxtBirthday" runat="server" ControlToValidate="TxtBirthday"
                            SetFocusOnError="false" Display="None" ErrorMessage="出生日期不能为空"></pe:RequiredFieldValidator>
                        <pe:DateValidator ID="DateValTxtBirthday" runat="server" ControlToValidate="TxtBirthday"
                            SetFocusOnError="false" Display="None" ErrorMessage="请填写正确的日期格式：yyyy-mm-dd"></pe:DateValidator>
                    </td>
                </tr>
                <tr runat="server" id="TRIDCard">
                    <td class="tdbgleft" style="width: 15%">
                        <b>身份证号码：</b></td>
                    <td>
                        <asp:TextBox ID="TxtIDCard" runat="server"></asp:TextBox>
                        <pe:InteractiveMessager ID="InteractiveMessagerTxtIDCard" Text="请输入身份证号码" ControlToMessage="TxtIDCard"
                            ValidatorOkMessage="身份证号码已输入" IsValidEmpty="false" runat="server"></pe:InteractiveMessager>
                        <pe:RequiredFieldValidator ID="ReqTxtIDCard" runat="server" ControlToValidate="TxtIDCard"
                            SetFocusOnError="false" Display="None" ErrorMessage="身份证号码不能为空"></pe:RequiredFieldValidator>
                    </td>
                </tr>
                <tr runat="server" id="TRVocation">
                    <td class="tdbgleft" style="width: 15%">
                        <b>职业：</b></td>
                    <td>
                        <asp:TextBox ID="TxtVocation" runat="server"></asp:TextBox>
                        <pe:InteractiveMessager ID="InteractiveMessagerTxtVocation" Text="请输入职业" ControlToMessage="TxtVocation"
                            ValidatorOkMessage="职业已输入" IsValidEmpty="false" runat="server"></pe:InteractiveMessager>
                        <pe:RequiredFieldValidator ID="ReqTxtVocation" runat="server" ControlToValidate="TxtVocation"
                            SetFocusOnError="false" Display="None" ErrorMessage="职业不能为空"></pe:RequiredFieldValidator>
                    </td>
                </tr>
                <tr runat="server" id="TRCompany">
                    <td class="tdbgleft" style="width: 15%">
                        <b>公司/单位名称：</b></td>
                    <td>
                        <asp:TextBox ID="TxtCompany" runat="server"></asp:TextBox>
                        <pe:InteractiveMessager ID="InteractiveMessagerTxtCompany" Text="请输入公司/单位名称" ControlToMessage="TxtCompany"
                            ValidatorOkMessage="公司/单位名称已输入" IsValidEmpty="false" runat="server"></pe:InteractiveMessager>
                        <pe:RequiredFieldValidator ID="ReqTxtCompany" runat="server" ControlToValidate="TxtCompany"
                            SetFocusOnError="false" Display="None" ErrorMessage="公司/单位名称不能为空"></pe:RequiredFieldValidator></td>
                </tr>
                <tr runat="server" id="TRDepartment">
                    <td class="tdbgleft" style="width: 15%">
                        <b>部门名称：</b></td>
                    <td>
                        <asp:TextBox ID="TxtDepartment" runat="server"></asp:TextBox>
                        <pe:InteractiveMessager ID="InteractiveMessagerTxtDepartment" Text="请输入部门名称" ControlToMessage="TxtDepartment"
                            ValidatorOkMessage="部门名称已输入" IsValidEmpty="false" runat="server"></pe:InteractiveMessager>
                        <pe:RequiredFieldValidator ID="ReqTxtDepartment" runat="server" ControlToValidate="TxtDepartment"
                            SetFocusOnError="false" Display="None" ErrorMessage="部门名称不能为空"></pe:RequiredFieldValidator>
                    </td>
                </tr>
                <tr runat="server" id="TRPosTitle">
                    <td class="tdbgleft" style="width: 15%">
                        <b>职务：</b></td>
                    <td>
                        <asp:TextBox ID="TxtPosTitle" runat="server"></asp:TextBox>
                        <pe:InteractiveMessager ID="InteractiveMessagerTxtPosTitle" Text="请输入职务" ControlToMessage="TxtPosTitle"
                            ValidatorOkMessage="职务已输入" IsValidEmpty="false" runat="server"></pe:InteractiveMessager>
                        <pe:RequiredFieldValidator ID="ReqTxtPosTitle" runat="server" ControlToValidate="TxtPosTitle"
                            SetFocusOnError="false" Display="None" ErrorMessage="职务不能为空"></pe:RequiredFieldValidator>
                    </td>
                </tr>
                <tr runat="server" id="TRMarriage">
                    <td class="tdbgleft" style="width: 15%">
                        <b>婚姻状况：</b></td>
                    <td>
                        <asp:DropDownList ID="DropMarriage" runat="server">
                            <asp:ListItem Text="请选择" Value=""></asp:ListItem>
                            <asp:ListItem Text="保密" Value="0"></asp:ListItem>
                            <asp:ListItem Text="未婚" Value="1"></asp:ListItem>
                            <asp:ListItem Text="已婚" Value="2"></asp:ListItem>
                            <asp:ListItem Text="离异" Value="3"></asp:ListItem>
                        </asp:DropDownList><pe:InteractiveMessager ID="InteractiveMessagerDropMarriage" Text="请选择婚姻状况"
                            ControlToMessage="DropMarriage" ValidatorOkMessage="婚姻状况已选择" IsValidEmpty="false"
                            runat="server"></pe:InteractiveMessager><pe:RequiredFieldValidator ID="ReqDropMarriage"
                                runat="server" ControlToValidate="DropMarriage" SetFocusOnError="false" Display="None"
                                ErrorMessage="请选择婚姻状况"></pe:RequiredFieldValidator></td>
                </tr>
                <tr runat="server" id="TRIncome">
                    <td class="tdbgleft" style="width: 15%">
                        <b>收入情况：</b></td>
                    <td>
                        <asp:DropDownList ID="DropIncome" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr runat="server" id="TRUserFace">
                    <td class="tdbgleft" style="width: 15%">
                        <b>头像地址：</b></td>
                    <td>
                        <asp:TextBox ID="TxtUserFace" runat="server"></asp:TextBox>
                        <pe:InteractiveMessager ID="InteractiveMessagerTxtUserFace" Text="请输入头像地址" ControlToMessage="TxtUserFace"
                            ValidatorOkMessage="头像地址已输入" IsValidEmpty="false" runat="server"></pe:InteractiveMessager>
                        <pe:RequiredFieldValidator ID="ReqTxtUserFace" runat="server" ControlToValidate="TxtUserFace"
                            SetFocusOnError="false" Display="None" ErrorMessage="头像地址不能为空"></pe:RequiredFieldValidator>
                    </td>
                </tr>
                <tr runat="server" id="TRFaceWidth">
                    <td class="tdbgleft" style="width: 15%">
                        <b>头像宽度：</b></td>
                    <td>
                        <asp:TextBox ID="TxtFaceWidth" runat="server"></asp:TextBox>
                        <pe:InteractiveMessager ID="InteractiveMessagerTxtFaceWidth" Text="请输入头像宽度" ControlToMessage="TxtFaceWidth"
                            ValidatorOkMessage="头像宽度已输入" IsValidEmpty="false" runat="server"></pe:InteractiveMessager>
                        <pe:RequiredFieldValidator ID="ReqTxtFaceWidth" runat="server" ControlToValidate="TxtFaceWidth"
                            SetFocusOnError="false" Display="None" ErrorMessage="头像宽度不能为空"></pe:RequiredFieldValidator>
                    </td>
                </tr>
                <tr runat="server" id="TRFaceHeight">
                    <td class="tdbgleft" style="width: 15%">
                        <b>头像高度：</b></td>
                    <td>
                        <asp:TextBox ID="TxtFaceHeight" runat="server"></asp:TextBox>
                        <pe:InteractiveMessager ID="InteractiveMessagerTxtFaceHeight" Text="请输入头像高度" ControlToMessage="TxtFaceHeight"
                            ValidatorOkMessage="头像高度已输入" IsValidEmpty="false" runat="server"></pe:InteractiveMessager>
                        <pe:RequiredFieldValidator ID="ReqTxtFaceHeight" runat="server" ControlToValidate="TxtFaceHeight"
                            SetFocusOnError="false" Display="None" ErrorMessage="头像高度不能为空"></pe:RequiredFieldValidator>
                    </td>
                </tr>
                <tr runat="server" id="TRSign">
                    <td class="tdbgleft" style="width: 15%">
                        <b>签名档：</b></td>
                    <td>
                        <asp:TextBox ID="TxtSign" TextMode="MultiLine" Height="60" Width="300" runat="server"></asp:TextBox>
                        <pe:InteractiveMessager ID="InteractiveMessagerTxtSign" Text="请输入签名档" ControlToMessage="TxtSign"
                            ValidatorOkMessage="签名档已输入" IsValidEmpty="false" runat="server"></pe:InteractiveMessager>
                        <pe:RequiredFieldValidator ID="ReqTxtSign" runat="server" ControlToValidate="TxtSign"
                            SetFocusOnError="false" Display="None" ErrorMessage="签名档不能为空"></pe:RequiredFieldValidator></td>
                </tr>
                <tr runat="server" id="TRPrivacy">
                    <td class="tdbgleft" style="width: 15%">
                        <b>隐私设定：</b></td>
                    <td>
                        <asp:DropDownList ID="DropPrivacy" runat="server">
                            <asp:ListItem Text="公开全部信息(包括真实姓名/电话号码/生日等)" Value="0"></asp:ListItem>
                            <asp:ListItem Text="公开部分信息(只公开QQ/Email等网上联络的信息)" Value="1"></asp:ListItem>
                            <asp:ListItem Text="完全保密(别人只能查看你的昵称)" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr id="TrRegQuestion1" runat="server" visible="false">
                    <td class="tdbgleft" style="width: 15%">
                        <b>回答注册问题：</b><br />
                        <asp:Literal ID="LitRegQuestion1" runat="server"></asp:Literal>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtRegAnswer1" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr id="TrRegQuestion2" runat="server" visible="false">
                    <td class="tdbgleft" style="width: 15%">
                        <b>回答注册问题：</b><br />
                        <asp:Literal ID="LitRegQuestion2" runat="server"></asp:Literal>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtRegAnswer2" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr id="TrRegQuestion3" runat="server" visible="false">
                    <td class="tdbgleft" style="width: 15%">
                        <b>回答注册问题：</b><br />
                        <asp:Literal ID="LitRegQuestion3" runat="server"></asp:Literal>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtRegAnswer3" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr id="TrVcodeRegister" runat="server" visible="false">
                    <td class="tdbgleft" style="width: 15%">
                        <b>验证码：</b></td>
                    <td>
                        <asp:TextBox ID="TxtValidateCode" runat="server"></asp:TextBox><pe:ValidateCode ID="VcodeRegister"
                            runat="server" RefreshLinkToolTip="看不清楚，换一个" />
                        <pe:InteractiveMessager ID="InteractiveMessagerTxtValidateCode" Text="请输入验证码" ControlToMessage="TxtValidateCode"
                            ValidatorOkMessage="验证码已输入" runat="server"></pe:InteractiveMessager>
                        <pe:RequiredFieldValidator ID="ValrValidateCode" runat="server" ErrorMessage="请输入验证码！"
                            ControlToValidate="TxtValidateCode" Display="None" SetFocusOnError="false"></pe:RequiredFieldValidator>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="PnlStep3" runat="server">
        </asp:Panel>
    </form>
</body>
</html>
