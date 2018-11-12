<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Register" Codebehind="Register.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>��Աע��</title>
</head>
<body>
    <pe:ExtendedSiteMapPath ID="YourPosition" SiteMapProvider="UserMapProvider" runat="server" />
    <form id="form1" runat="server">
        <asp:ScriptManager ID="SmgeRegion" runat="server" EnablePartialRendering="true">
        </asp:ScriptManager>
        <pe:ExtendedLabel HtmlEncode="false" ID="LitControlTreeInfo" runat="server"></pe:ExtendedLabel>
        <asp:Panel ID="PnlRegStep0" runat="server" Visible="false">
            ������ע�ᣬ�����վ����Ա��ϵ��
        </asp:Panel>
        <asp:Panel ID="PnlRegStep1" runat="server" Visible="false">
            <div style="text-align: center">
                <b>�������������</b></div>
            <textarea cols="20" rows="2" style="font-weight: normal; font-size: 9pt; width: 98%;
                line-height: normal; font-style: normal; height: 310px; font-variant: normal"
                readonly="readonly"><pe:ExtendedLiteral HtmlEncode="false" ID="LitProtocol" runat="server"></pe:ExtendedLiteral></textarea>
            <div style="text-align: center">
                <asp:Button ID="BtnRegStep1" runat="server" Text="��ͬ��" OnClick="BtnRegStep1_Click" />
                <asp:Button ID="BtnRegStep1NotApprove" runat="server" Text="��ͬ��" OnClick="BtnRegStep1NotApprove_Click" />
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
                            checkUserNameMessage.innerHTML = "�û���Ϊ��";
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
                            checkUserNameMessage.innerHTML = "�û����Ѿ���ע����";
                            checkUserNameMessage.className = "d_err";
                        }
                        
                        if(result == "disabled")
                        {
                            checkUserNameMessage.innerHTML = "���û�����ֹע��";
                            checkUserNameMessage.className = "d_err";
                        }
                        
                        if(result == "false")
                        {
                            checkUserNameMessage.innerHTML = "��ϲ�����û�������ʹ�ã�";
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
                        alert("��⵽���д���HTML���룡");
                        args.set_errorHandled(true);
                        }
                    }
                    
                    //����Ƿ�����ͬ����
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
                        <b>�û�����</b>
                    </td>
                    <td valign="middle" style="width: 85%">
                        <asp:TextBox ID="TxtUserName" runat="server"></asp:TextBox>
                        <pe:InteractiveMessager ID="InterMessageUserName" ControlToMessage="TxtUserName"
                            ValidatorOkMessage="�û���������" runat="server"></pe:InteractiveMessager>
                        <pe:RequiredFieldValidator ID="ReqTxtUserName" runat="server" ControlToValidate="TxtUserName"
                            SetFocusOnError="false" ErrorMessage="�û�������Ϊ��" Display="None"></pe:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="ValgTextMaxLength" ControlToValidate="TxtUserName"
                            ValidationExpression="^[a-zA-Z0-9_\u4e00-\u9fa5]{4,20}$" SetFocusOnError="false"
                            Display="None" runat="server"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td class="tdbgleft" style="width: 15%">
                    </td>
                    <td valign="middle" style="width: 85%">
                        <input id="CheckUserName" style="float: left;" type="button" value="����û����Ƿ����" onclick="CheckUser()" /><span
                            class="d_default" id="CheckUserNameMessage"></span>
                    </td>
                </tr>
                <tr>
                    <td class="tdbgleft" style="width: 15%">
                        <b>���룺</b>
                    </td>
                    <td valign="middle" style="width: 85%">
                        <asp:TextBox ID="TxtPassword" TextMode="Password" runat="server"></asp:TextBox>
                        <pe:InteractiveMessager ID="InteractiveMessagerTxtPassword" Text="���������룬���ִ�Сд(����6λ)��������ø���һЩ���Է����˱����½�"
                            ControlToMessage="TxtPassword" ValidatorOkMessage="����������" runat="server"></pe:InteractiveMessager>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorPassword" runat="server"
                            ControlToValidate="TxtPassword" SetFocusOnError="false" Display="None" ValidationExpression="[\S]{6,}"
                            ErrorMessage="��������6λ"></asp:RegularExpressionValidator>
                        <pe:RequiredFieldValidator ID="ReqTxtPassword" runat="server" ControlToValidate="TxtPassword"
                            SetFocusOnError="false" Display="None" ErrorMessage="���벻��Ϊ��"></pe:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="tdbgleft" style="width: 15%">
                        <strong>ȷ�����룺</strong>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtPwdConfirm" TextMode="Password" runat="server"></asp:TextBox>
                        <pe:InteractiveMessager ID="InteractiveMessagerTxtPwdConfirm" Text="������һ����ȷ������" ControlToMessage="TxtPwdConfirm"
                            ValidatorOkMessage="ȷ������������" runat="server"></pe:InteractiveMessager>
                        <pe:RequiredFieldValidator ID="ReqTxtPwdConfirm" runat="server" ControlToValidate="TxtPwdConfirm"
                            SetFocusOnError="false" Display="None" ErrorMessage="ȷ�����벻��Ϊ��"></pe:RequiredFieldValidator><asp:CompareValidator
                                ID="ValCompPassword" runat="server" ControlToValidate="TxtPwdConfirm" ControlToCompare="TxtPassword"
                                Operator="Equal" SetFocusOnError="false" Display="None" ErrorMessage="�����������벻һ��"></asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td class="tdbgleft" style="width: 15%">
                        <strong>�������⣺</strong>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtQuestion" runat="server"></asp:TextBox>
                        <pe:InteractiveMessager ID="InteractiveMessager1" Text="�����ɱ��˲�֪���𰸵����⣬�Է��������һ����빦����ȡ�����ʺš�"
                            ControlToMessage="TxtQuestion" ValidatorOkMessage="��������������" runat="server"></pe:InteractiveMessager>
                        <pe:RequiredFieldValidator ID="ReqTxtQuestion" runat="server" ControlToValidate="TxtQuestion"
                            SetFocusOnError="false" Display="None" ErrorMessage="�������ⲻ��Ϊ��"></pe:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="tdbgleft" style="width: 15%">
                        <strong>����𰸣�</strong><br />
                    </td>
                    <td>
                        <asp:TextBox ID="TxtAnswer" runat="server"></asp:TextBox>
                        <pe:InteractiveMessager ID="InteractiveMessagerTxtAnswer" Text="����ȡ�����롣�뾡����ø���һЩ���Է��������һ����빦����ȡ�����ʺš�"
                            ControlToMessage="TxtAnswer" ValidatorOkMessage="�����������" runat="server"></pe:InteractiveMessager>
                        <pe:RequiredFieldValidator ID="ReqAnswer" runat="server" ControlToValidate="TxtAnswer"
                            SetFocusOnError="false" Display="None" ErrorMessage="����𰸲���Ϊ��"></pe:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td class="tdbgleft" style="width: 15%">
                        <strong>Email��ַ��</strong>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtEmail" runat="server"></asp:TextBox>
                        <pe:InteractiveMessager ID="InteractiveMessagerTxtEmail" Text="��������Ч���ʼ���ַ���Ա����ǿ��Լ�ʱ������ϵ��"
                            ControlToMessage="TxtEmail" ValidatorOkMessage="Email��ַ������" runat="server"></pe:InteractiveMessager>
                        <pe:RequiredFieldValidator ID="ReqTxtEmail" runat="server" ControlToValidate="TxtEmail"
                            SetFocusOnError="false" Display="None" ErrorMessage="Email����Ϊ��"></pe:RequiredFieldValidator><pe:EmailValidator
                                ID="ValeTxtEmail" runat="server" ControlToValidate="TxtEmail" ErrorMessage="��������ȷ��Email"
                                SetFocusOnError="false" Display="None"></pe:EmailValidator>
                    </td>
                </tr>
            </table>
            <table id="TableRegisterSelect" runat="server" style="border-collapse: collapse;
                display: none;" cellspacing="1" cellpadding="2" width="100%" border="0">
                <thead>
                    <tr>
                        <th style="height: 20px;" colspan="2">
                            &nbsp;&nbsp;&nbsp;ѡ����Ϣ</th>
                    </tr>
                </thead>
            </table>
            <table style="border-collapse: collapse" cellspacing="1" cellpadding="2" width="100%"
                border="0">
                <tr class="tdbgleft" style="width: 15%" align="center">
                    <td colspan="2" style="height: 30px">
                        <span style="color: #ff0000">����������Ϣ����������ȷ��д����ܼ�����һ��ע�������</span></td>
                </tr>
                <tr class="tdbgleft" style="width: 15%" align="center">
                    <td style="height: 30px" colspan="2">
                        <asp:Button ID="BtnRegStep2" runat="server" Text="��һ��" OnClick="BtnRegStep2_Click" />
                        <input id="Reset" type="reset" value=" ������д " name="Reset" />
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                            ShowSummary="False" />
                    </td>
                </tr>
            </table>
            <table id="TableRegister" visible="false" runat="server" style="border-collapse: collapse"
                cellspacing="1" cellpadding="2" width="100%" border="0">
                <tr id="TRSwicthSelectFill" runat="server">
                    <td onclick="SwicthSelectFill()" class="tdbgleft" style="width: 15%">
                        <span><b>ѡ����Ϣ��</b></span></td>
                    <td>
                        <input type="checkbox" id="checkSelectFill" onclick="SwicthSelectFill()" /><label
                            for="checkSelectFill" style="color: Blue">��ʾ�û�����ѡ����Ϣ</label>
                    </td>
                </tr>
                <tr runat="server" id="TRSpareEmail">
                    <td class="tdbgleft" style="width: 15%">
                        <strong>����Email��ַ��</strong></td>
                    <td>
                        <asp:TextBox ID="TxtSpareEmail" runat="server"></asp:TextBox>
                        <pe:InteractiveMessager ID="InteractiveMessagerTxtSpareEmail" Text="�����뱸��Email��ַ"
                            ControlToMessage="TxtSpareEmail" ValidatorOkMessage="����Email��ַ������" runat="server"></pe:InteractiveMessager>
                        <pe:EmailValidator ID="EmailValidator1" runat="server" ControlToValidate="TxtSpareEmail"
                            ErrorMessage="��������ȷ��Email" SetFocusOnError="false" Display="None">
                        </pe:EmailValidator>
                        <pe:RequiredFieldValidator ID="ReqTxtSpareEmail" runat="server" ControlToValidate="TxtSpareEmail"
                            SetFocusOnError="false" Display="None" ErrorMessage="����Email��ַ����Ϊ��"></pe:RequiredFieldValidator></td>
                </tr>
                <tr runat="server" id="TRHomepage">
                    <td class="tdbgleft" style="width: 15%">
                        <strong>��ҳ��</strong></td>
                    <td>
                        <asp:TextBox ID="TxtHomepage" runat="server">http://</asp:TextBox>
                        <pe:InteractiveMessager ID="InteractiveMessagerTxtHomepage" Text="��������ҳ��ַ" ControlToMessage="TxtHomepage"
                            ValidatorOkMessage="��ҳ��ַ������" runat="server"></pe:InteractiveMessager>
                        <pe:RequiredFieldValidator ID="ReqTxtHomepage" runat="server" ControlToValidate="TxtHomepage"
                            SetFocusOnError="false" Display="None" ErrorMessage="��ҳ��ַ����Ϊ��"></pe:RequiredFieldValidator>
                        <asp:CustomValidator ID="ValcHomepage" runat="server" Display="None" 
                            ErrorMessage="��ҳ��ַ���л�Աʹ�ã����������룡" 
                            ClientValidationFunction="CheckSameHomepage" ControlToValidate="TxtHomepage"></asp:CustomValidator>
                    </td>
                </tr>
                <tr runat="server" id="TRQQ">
                    <td class="tdbgleft" style="width: 15%">
                        <strong>QQ���룺</strong></td>
                    <td>
                        <asp:TextBox ID="TxtQQ" runat="server"></asp:TextBox>
                        <pe:InteractiveMessager ID="InteractiveMessagerTxtQQ" Text="������QQ����" ControlToMessage="TxtQQ"
                            ValidatorOkMessage="QQ����������" runat="server"></pe:InteractiveMessager>
                        <pe:RequiredFieldValidator ID="ReqTxtQQ" runat="server" ControlToValidate="TxtQQ"
                            SetFocusOnError="false" Display="None" ErrorMessage="QQ���벻��Ϊ��"></pe:RequiredFieldValidator>

                        <asp:CustomValidator ID="ValcCheckSameQQ" runat="server" 
                            ClientValidationFunction="CheckSameQQ" ControlToValidate="TxtQQ" Display="None" 
                            ErrorMessage="QQ�����ѱ�������Աʹ�ã����������룡"></asp:CustomValidator>

                    </td>
                </tr>
                <tr runat="server" id="TRICQ">
                    <td class="tdbgleft" style="width: 15%">
                        <b>ICQ���룺</b></td>
                    <td>
                        <asp:TextBox ID="TxtICQ" runat="server"></asp:TextBox>
                        <pe:InteractiveMessager ID="InteractiveMessagerTxtICQ" Text="������ICQ����" ControlToMessage="TxtICQ"
                            ValidatorOkMessage="ICQ����������" runat="server"></pe:InteractiveMessager>
                        <pe:RequiredFieldValidator ID="ReqTxtICQ" runat="server" ControlToValidate="TxtICQ"
                            SetFocusOnError="false" Display="None" ErrorMessage="ICQ���벻��Ϊ��"></pe:RequiredFieldValidator>
                    </td>
                </tr>
                <tr runat="server" id="TRMSN">
                    <td class="tdbgleft" style="width: 15%">
                        <strong>MSN�ʺţ�</strong></td>
                    <td>
                        <asp:TextBox ID="TxtMSN" runat="server"></asp:TextBox>
                        <pe:InteractiveMessager ID="InteractiveMessagerTxtMSN" Text="������MSN�ʺ�" ControlToMessage="TxtMSN"
                            ValidatorOkMessage="MSN�ʺ�������" runat="server"></pe:InteractiveMessager>
                        <pe:RequiredFieldValidator ID="ReqTxtMSN" runat="server" ControlToValidate="TxtMSN"
                            SetFocusOnError="false" Display="None" ErrorMessage="MSN�ʺŲ���Ϊ��"></pe:RequiredFieldValidator>
                        <asp:CustomValidator ID="ValcCheckSameMsn" runat="server" 
                            ClientValidationFunction="CheckSameMsn" ControlToValidate="TxtMSN" 
                            Display="None" ErrorMessage="MSN�ʺ��ѱ�������Աʹ�ã����������룡" ></asp:CustomValidator>
                    </td>
                </tr>
                <tr runat="server" id="TRYahoo">
                    <td class="tdbgleft" style="width: 15%">
                        <b>�Ż�ͨ�ʺţ�</b></td>
                    <td>
                        <asp:TextBox ID="TxtYahoo" runat="server"></asp:TextBox>
                        <pe:InteractiveMessager ID="InteractiveMessagerTxtYahoo" Text="�������Ż�ͨ�ʺ�" ControlToMessage="TxtYahoo"
                            ValidatorOkMessage="�Ż�ͨ�ʺ�������" runat="server"></pe:InteractiveMessager>
                        <pe:RequiredFieldValidator ID="ReqTxtYahoo" runat="server" ControlToValidate="TxtYahoo"
                            SetFocusOnError="false" Display="None" ErrorMessage="�Ż�ͨ�ʺŲ���Ϊ��"></pe:RequiredFieldValidator>
                    </td>
                </tr>
                <tr runat="server" id="TRUC">
                    <td class="tdbgleft" style="width: 15%">
                        <b>UC���룺</b></td>
                    <td>
                        <asp:TextBox ID="TxtUC" runat="server"></asp:TextBox>
                        <pe:InteractiveMessager ID="InteractiveMessagerTxtUC" Text="������UC����" ControlToMessage="TxtUC"
                            ValidatorOkMessage="UC����������" runat="server"></pe:InteractiveMessager>
                        <pe:RequiredFieldValidator ID="ReqTxtUC" runat="server" ControlToValidate="TxtUC"
                            SetFocusOnError="false" Display="None" ErrorMessage="UC���벻��Ϊ��"></pe:RequiredFieldValidator>
                    </td>
                </tr>
                <tr runat="server" id="TRAim">
                    <td class="tdbgleft" style="width: 15%">
                        <b>Aim�ʺţ�</b></td>
                    <td>
                        <asp:TextBox ID="TxtAim" runat="server"></asp:TextBox>
                        <pe:InteractiveMessager ID="InteractiveMessagerTxtAim" Text="������Aim�ʺ�" ControlToMessage="TxtAim"
                            ValidatorOkMessage="Aim�ʺ�������" runat="server"></pe:InteractiveMessager>
                        <pe:RequiredFieldValidator ID="ReqTxtAim" runat="server" ControlToValidate="TxtAim"
                            SetFocusOnError="false" Display="None" ErrorMessage="Aim�ʺŲ���Ϊ��"></pe:RequiredFieldValidator>
                    </td>
                </tr>
                <tr runat="server" id="TROfficePhone">
                    <td class="tdbgleft" style="width: 15%">
                        <b>�칫�绰��</b></td>
                    <td>
                        <asp:TextBox ID="TxtOfficePhone" runat="server"></asp:TextBox>
                        <pe:InteractiveMessager ID="InteractiveMessagerTxtOfficePhone" Text="������칫�绰" ControlToMessage="TxtOfficePhone"
                            ValidatorOkMessage="�칫�绰������" runat="server"></pe:InteractiveMessager>
                        <pe:RequiredFieldValidator ID="ReqTxtOfficePhone" runat="server" ControlToValidate="TxtOfficePhone"
                            SetFocusOnError="false" Display="None" ErrorMessage="�칫�绰����Ϊ��"></pe:RequiredFieldValidator>
                        <asp:CustomValidator ID="ValcSameOfficePhone" runat="server" 
                            ClientValidationFunction="CheckSameOfficePhone" ControlToValidate="TxtOfficePhone" 
                            Display="None" ErrorMessage="�칫�绰�ѱ�������Աע�ᣬ���������룡" ></asp:CustomValidator>
                    </td>
                </tr>
                <tr runat="server" id="TRHomePhone">
                    <td class="tdbgleft" style="width: 15%">
                        <b>��ͥ�绰��</b></td>
                    <td>
                        <asp:TextBox ID="TxtHomePhone" runat="server"></asp:TextBox>
                        <pe:InteractiveMessager ID="InteractiveMessagerTxtHomePhone" Text="�������ͥ�绰" ControlToMessage="TxtHomePhone"
                            ValidatorOkMessage="��ͥ�绰������" runat="server"></pe:InteractiveMessager>
                        <pe:RequiredFieldValidator ID="ReqTxtHomePhone" runat="server" ControlToValidate="TxtHomePhone"
                            SetFocusOnError="false" Display="None" ErrorMessage="��ͥ�绰����Ϊ��"></pe:RequiredFieldValidator>
                        <asp:CustomValidator ID="ValcSameHomePhone" runat="server" 
                            ClientValidationFunction="CheckSameHomePhone" ControlToValidate="TxtHomePhone" 
                            Display="None" ErrorMessage="��ͥ�绰�ѱ�������Աע�ᣬ���������룡" ></asp:CustomValidator>

                    </td>
                </tr>
                <tr runat="server" id="TRFax">
                    <td class="tdbgleft" style="width: 15%">
                        <b>������룺</b></td>
                    <td>
                        <asp:TextBox ID="TxtFax" runat="server"></asp:TextBox>
                        <pe:InteractiveMessager ID="InteractiveMessagerTxtFax" Text="�����봫�����" ControlToMessage="TxtFax"
                            ValidatorOkMessage="�������������" runat="server"></pe:InteractiveMessager>
                        <pe:RequiredFieldValidator ID="ReqTxtFax" runat="server" ControlToValidate="TxtFax"
                            SetFocusOnError="false" Display="None" ErrorMessage="������벻��Ϊ��"></pe:RequiredFieldValidator>
                    </td>
                </tr>
                <tr runat="server" id="TRMobile">
                    <td class="tdbgleft" style="width: 15%">
                        <b>�ֻ����룺</b></td>
                    <td>
                        <asp:TextBox ID="TxtMobile" runat="server"></asp:TextBox>
                        <pe:InteractiveMessager ID="InteractiveMessagerTxtMobile" Text="�������ֻ�����" ControlToMessage="TxtMobile"
                            ValidatorOkMessage="�ֻ�����������" runat="server"></pe:InteractiveMessager>
                        <pe:RequiredFieldValidator ID="ReqTxtMobile" runat="server" ControlToValidate="TxtMobile"
                            SetFocusOnError="false" Display="None" ErrorMessage="�ֻ����벻��Ϊ��"></pe:RequiredFieldValidator>
                        <pe:MobileValidator ID="MValTxtMobile" runat="server" ControlToValidate="TxtMobile"
                            SetFocusOnError="false" Display="None"></pe:MobileValidator>
                        <asp:CustomValidator ID="ValcSameMobile" runat="server" 
                            ClientValidationFunction="CheckSameMobile" ControlToValidate="TxtMobile" 
                            Display="None" ErrorMessage="�ֻ������ѱ�������Աע�ᣬ���������룡" ></asp:CustomValidator>


                    </td>
                </tr>
                <tr runat="server" id="TRPHS">
                    <td class="tdbgleft" style="width: 15%">
                        <b>С��ͨ���룺</b></td>
                    <td>
                        <asp:TextBox ID="TxtPHS" runat="server"></asp:TextBox>
                        <pe:InteractiveMessager ID="InteractiveMessagerTxtPHS" Text="������С��ͨ����" ControlToMessage="TxtPHS"
                            ValidatorOkMessage="С��ͨ����������" runat="server"></pe:InteractiveMessager>
                        <pe:RequiredFieldValidator ID="ReqTxtPHS" runat="server" ControlToValidate="TxtPHS"
                            SetFocusOnError="false" Display="None" ErrorMessage="С��ͨ���벻��Ϊ��"></pe:RequiredFieldValidator>
                        <pe:TelephoneValidator ID="TValTxtPHS" runat="server" ControlToValidate="TxtPHS"
                            SetFocusOnError="false" Display="None"></pe:TelephoneValidator>
                    </td>
                </tr>
                <tr runat="server" id="TRRegion">
                    <td class="tdbgleft" style="width: 15%">
                        <strong>������Ϣ��</strong>
                    </td>
                    <td>
                        <pe:Region ID="Region" runat="server" />
                    </td>
                </tr>
                <tr runat="server" id="TRAddress">
                    <td class="tdbgleft" style="width: 15%">
                        <b>��ϵ��ַ��</b></td>
                    <td>
                        <asp:TextBox ID="TxtAddress" runat="server"></asp:TextBox>
                        <pe:InteractiveMessager ID="InteractiveMessagerTxtAddress" Text="��������ϵ��ַ" ControlToMessage="TxtAddress"
                            ValidatorOkMessage="��ϵ��ַ������" runat="server"></pe:InteractiveMessager>
                        <pe:RequiredFieldValidator ID="ReqTxtAddress" runat="server" ControlToValidate="TxtAddress"
                            SetFocusOnError="false" Display="None" ErrorMessage="��ϵ��ַ����Ϊ��"></pe:RequiredFieldValidator>
                    </td>
                </tr>
                <tr runat="server" id="TRZipCode">
                    <td class="tdbgleft" style="width: 15%">
                        <b>�������룺</b></td>
                    <td>
                        <asp:TextBox ID="TxtZipCode" runat="server"></asp:TextBox>
                        <pe:RequiredFieldValidator ID="ReqTxtZipCode" runat="server" ControlToValidate="TxtZipCode"
                            SetFocusOnError="false" Display="None" ErrorMessage="�������벻��Ϊ��"></pe:RequiredFieldValidator>
                        <pe:InteractiveMessager ID="InteractiveMessagerTxtZipCode" Text="��������������" ControlToMessage="TxtZipCode"
                            ValidatorOkMessage="��������������" IsValidEmpty="false" runat="server"></pe:InteractiveMessager>
                        <pe:ZipCodeValidator ID="ZCValorTxtZipCode" runat="server" ControlToValidate="TxtZipCode"
                            ErrorMessage="" Display="None" SetFocusOnError="false"></pe:ZipCodeValidator>
                    </td>
                </tr>
                <tr runat="server" id="TRTrueName">
                    <td class="tdbgleft" style="width: 15%">
                        <b>��ʵ������</b></td>
                    <td>
                        <asp:TextBox ID="TxtTrueName" runat="server"></asp:TextBox>
                        <pe:InteractiveMessager ID="InteractiveMessagerTxtTrueName" Text="��������ʵ����" ControlToMessage="TxtTrueName"
                            ValidatorOkMessage="��ʵ����������" IsValidEmpty="false" runat="server"></pe:InteractiveMessager>
                        <pe:RequiredFieldValidator ID="ReqTxtTrueName" runat="server" ControlToValidate="TxtTrueName"
                            SetFocusOnError="false" Display="None" ErrorMessage="��ʵ��������Ϊ��"></pe:RequiredFieldValidator>
                    </td>
                </tr>
                <tr runat="server" id="TRSex">
                    <td class="tdbgleft" style="width: 15%">
                        <strong>�Ա�</strong></td>
                    <td>
                        <asp:DropDownList ID="DropSex" runat="server">
                            <asp:ListItem Text="����" Value="0"></asp:ListItem>
                            <asp:ListItem Text="��" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Ů" Value="2"></asp:ListItem>
                        </asp:DropDownList></td>
                </tr>
                <tr runat="server" id="TRBirthday">
                    <td class="tdbgleft" style="width: 15%">
                        <b>�������ڣ�</b></td>
                    <td>
                        <pe:DatePicker ID="TxtBirthday" runat="server"></pe:DatePicker>
                        <pe:InteractiveMessager ID="InteractiveMessagerTxtBirthday" Text="�������������" ControlToMessage="TxtBirthday"
                            ValidatorOkMessage="��������������" IsValidEmpty="false" runat="server"></pe:InteractiveMessager>
                        <pe:RequiredFieldValidator ID="ReqTxtBirthday" runat="server" ControlToValidate="TxtBirthday"
                            SetFocusOnError="false" Display="None" ErrorMessage="�������ڲ���Ϊ��"></pe:RequiredFieldValidator>
                        <pe:DateValidator ID="DateValTxtBirthday" runat="server" ControlToValidate="TxtBirthday"
                            SetFocusOnError="false" Display="None" ErrorMessage="����д��ȷ�����ڸ�ʽ��yyyy-mm-dd"></pe:DateValidator>
                    </td>
                </tr>
                <tr runat="server" id="TRIDCard">
                    <td class="tdbgleft" style="width: 15%">
                        <b>���֤���룺</b></td>
                    <td>
                        <asp:TextBox ID="TxtIDCard" runat="server"></asp:TextBox>
                        <pe:InteractiveMessager ID="InteractiveMessagerTxtIDCard" Text="���������֤����" ControlToMessage="TxtIDCard"
                            ValidatorOkMessage="���֤����������" IsValidEmpty="false" runat="server"></pe:InteractiveMessager>
                        <pe:RequiredFieldValidator ID="ReqTxtIDCard" runat="server" ControlToValidate="TxtIDCard"
                            SetFocusOnError="false" Display="None" ErrorMessage="���֤���벻��Ϊ��"></pe:RequiredFieldValidator>
                    </td>
                </tr>
                <tr runat="server" id="TRVocation">
                    <td class="tdbgleft" style="width: 15%">
                        <b>ְҵ��</b></td>
                    <td>
                        <asp:TextBox ID="TxtVocation" runat="server"></asp:TextBox>
                        <pe:InteractiveMessager ID="InteractiveMessagerTxtVocation" Text="������ְҵ" ControlToMessage="TxtVocation"
                            ValidatorOkMessage="ְҵ������" IsValidEmpty="false" runat="server"></pe:InteractiveMessager>
                        <pe:RequiredFieldValidator ID="ReqTxtVocation" runat="server" ControlToValidate="TxtVocation"
                            SetFocusOnError="false" Display="None" ErrorMessage="ְҵ����Ϊ��"></pe:RequiredFieldValidator>
                    </td>
                </tr>
                <tr runat="server" id="TRCompany">
                    <td class="tdbgleft" style="width: 15%">
                        <b>��˾/��λ���ƣ�</b></td>
                    <td>
                        <asp:TextBox ID="TxtCompany" runat="server"></asp:TextBox>
                        <pe:InteractiveMessager ID="InteractiveMessagerTxtCompany" Text="�����빫˾/��λ����" ControlToMessage="TxtCompany"
                            ValidatorOkMessage="��˾/��λ����������" IsValidEmpty="false" runat="server"></pe:InteractiveMessager>
                        <pe:RequiredFieldValidator ID="ReqTxtCompany" runat="server" ControlToValidate="TxtCompany"
                            SetFocusOnError="false" Display="None" ErrorMessage="��˾/��λ���Ʋ���Ϊ��"></pe:RequiredFieldValidator></td>
                </tr>
                <tr runat="server" id="TRDepartment">
                    <td class="tdbgleft" style="width: 15%">
                        <b>�������ƣ�</b></td>
                    <td>
                        <asp:TextBox ID="TxtDepartment" runat="server"></asp:TextBox>
                        <pe:InteractiveMessager ID="InteractiveMessagerTxtDepartment" Text="�����벿������" ControlToMessage="TxtDepartment"
                            ValidatorOkMessage="��������������" IsValidEmpty="false" runat="server"></pe:InteractiveMessager>
                        <pe:RequiredFieldValidator ID="ReqTxtDepartment" runat="server" ControlToValidate="TxtDepartment"
                            SetFocusOnError="false" Display="None" ErrorMessage="�������Ʋ���Ϊ��"></pe:RequiredFieldValidator>
                    </td>
                </tr>
                <tr runat="server" id="TRPosTitle">
                    <td class="tdbgleft" style="width: 15%">
                        <b>ְ��</b></td>
                    <td>
                        <asp:TextBox ID="TxtPosTitle" runat="server"></asp:TextBox>
                        <pe:InteractiveMessager ID="InteractiveMessagerTxtPosTitle" Text="������ְ��" ControlToMessage="TxtPosTitle"
                            ValidatorOkMessage="ְ��������" IsValidEmpty="false" runat="server"></pe:InteractiveMessager>
                        <pe:RequiredFieldValidator ID="ReqTxtPosTitle" runat="server" ControlToValidate="TxtPosTitle"
                            SetFocusOnError="false" Display="None" ErrorMessage="ְ����Ϊ��"></pe:RequiredFieldValidator>
                    </td>
                </tr>
                <tr runat="server" id="TRMarriage">
                    <td class="tdbgleft" style="width: 15%">
                        <b>����״����</b></td>
                    <td>
                        <asp:DropDownList ID="DropMarriage" runat="server">
                            <asp:ListItem Text="��ѡ��" Value=""></asp:ListItem>
                            <asp:ListItem Text="����" Value="0"></asp:ListItem>
                            <asp:ListItem Text="δ��" Value="1"></asp:ListItem>
                            <asp:ListItem Text="�ѻ�" Value="2"></asp:ListItem>
                            <asp:ListItem Text="����" Value="3"></asp:ListItem>
                        </asp:DropDownList><pe:InteractiveMessager ID="InteractiveMessagerDropMarriage" Text="��ѡ�����״��"
                            ControlToMessage="DropMarriage" ValidatorOkMessage="����״����ѡ��" IsValidEmpty="false"
                            runat="server"></pe:InteractiveMessager><pe:RequiredFieldValidator ID="ReqDropMarriage"
                                runat="server" ControlToValidate="DropMarriage" SetFocusOnError="false" Display="None"
                                ErrorMessage="��ѡ�����״��"></pe:RequiredFieldValidator></td>
                </tr>
                <tr runat="server" id="TRIncome">
                    <td class="tdbgleft" style="width: 15%">
                        <b>���������</b></td>
                    <td>
                        <asp:DropDownList ID="DropIncome" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr runat="server" id="TRUserFace">
                    <td class="tdbgleft" style="width: 15%">
                        <b>ͷ���ַ��</b></td>
                    <td>
                        <asp:TextBox ID="TxtUserFace" runat="server"></asp:TextBox>
                        <pe:InteractiveMessager ID="InteractiveMessagerTxtUserFace" Text="������ͷ���ַ" ControlToMessage="TxtUserFace"
                            ValidatorOkMessage="ͷ���ַ������" IsValidEmpty="false" runat="server"></pe:InteractiveMessager>
                        <pe:RequiredFieldValidator ID="ReqTxtUserFace" runat="server" ControlToValidate="TxtUserFace"
                            SetFocusOnError="false" Display="None" ErrorMessage="ͷ���ַ����Ϊ��"></pe:RequiredFieldValidator>
                    </td>
                </tr>
                <tr runat="server" id="TRFaceWidth">
                    <td class="tdbgleft" style="width: 15%">
                        <b>ͷ���ȣ�</b></td>
                    <td>
                        <asp:TextBox ID="TxtFaceWidth" runat="server"></asp:TextBox>
                        <pe:InteractiveMessager ID="InteractiveMessagerTxtFaceWidth" Text="������ͷ����" ControlToMessage="TxtFaceWidth"
                            ValidatorOkMessage="ͷ����������" IsValidEmpty="false" runat="server"></pe:InteractiveMessager>
                        <pe:RequiredFieldValidator ID="ReqTxtFaceWidth" runat="server" ControlToValidate="TxtFaceWidth"
                            SetFocusOnError="false" Display="None" ErrorMessage="ͷ���Ȳ���Ϊ��"></pe:RequiredFieldValidator>
                    </td>
                </tr>
                <tr runat="server" id="TRFaceHeight">
                    <td class="tdbgleft" style="width: 15%">
                        <b>ͷ��߶ȣ�</b></td>
                    <td>
                        <asp:TextBox ID="TxtFaceHeight" runat="server"></asp:TextBox>
                        <pe:InteractiveMessager ID="InteractiveMessagerTxtFaceHeight" Text="������ͷ��߶�" ControlToMessage="TxtFaceHeight"
                            ValidatorOkMessage="ͷ��߶�������" IsValidEmpty="false" runat="server"></pe:InteractiveMessager>
                        <pe:RequiredFieldValidator ID="ReqTxtFaceHeight" runat="server" ControlToValidate="TxtFaceHeight"
                            SetFocusOnError="false" Display="None" ErrorMessage="ͷ��߶Ȳ���Ϊ��"></pe:RequiredFieldValidator>
                    </td>
                </tr>
                <tr runat="server" id="TRSign">
                    <td class="tdbgleft" style="width: 15%">
                        <b>ǩ������</b></td>
                    <td>
                        <asp:TextBox ID="TxtSign" TextMode="MultiLine" Height="60" Width="300" runat="server"></asp:TextBox>
                        <pe:InteractiveMessager ID="InteractiveMessagerTxtSign" Text="������ǩ����" ControlToMessage="TxtSign"
                            ValidatorOkMessage="ǩ����������" IsValidEmpty="false" runat="server"></pe:InteractiveMessager>
                        <pe:RequiredFieldValidator ID="ReqTxtSign" runat="server" ControlToValidate="TxtSign"
                            SetFocusOnError="false" Display="None" ErrorMessage="ǩ��������Ϊ��"></pe:RequiredFieldValidator></td>
                </tr>
                <tr runat="server" id="TRPrivacy">
                    <td class="tdbgleft" style="width: 15%">
                        <b>��˽�趨��</b></td>
                    <td>
                        <asp:DropDownList ID="DropPrivacy" runat="server">
                            <asp:ListItem Text="����ȫ����Ϣ(������ʵ����/�绰����/���յ�)" Value="0"></asp:ListItem>
                            <asp:ListItem Text="����������Ϣ(ֻ����QQ/Email�������������Ϣ)" Value="1"></asp:ListItem>
                            <asp:ListItem Text="��ȫ����(����ֻ�ܲ鿴����ǳ�)" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr id="TrRegQuestion1" runat="server" visible="false">
                    <td class="tdbgleft" style="width: 15%">
                        <b>�ش�ע�����⣺</b><br />
                        <asp:Literal ID="LitRegQuestion1" runat="server"></asp:Literal>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtRegAnswer1" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr id="TrRegQuestion2" runat="server" visible="false">
                    <td class="tdbgleft" style="width: 15%">
                        <b>�ش�ע�����⣺</b><br />
                        <asp:Literal ID="LitRegQuestion2" runat="server"></asp:Literal>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtRegAnswer2" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr id="TrRegQuestion3" runat="server" visible="false">
                    <td class="tdbgleft" style="width: 15%">
                        <b>�ش�ע�����⣺</b><br />
                        <asp:Literal ID="LitRegQuestion3" runat="server"></asp:Literal>
                    </td>
                    <td>
                        <asp:TextBox ID="TxtRegAnswer3" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr id="TrVcodeRegister" runat="server" visible="false">
                    <td class="tdbgleft" style="width: 15%">
                        <b>��֤�룺</b></td>
                    <td>
                        <asp:TextBox ID="TxtValidateCode" runat="server"></asp:TextBox><pe:ValidateCode ID="VcodeRegister"
                            runat="server" RefreshLinkToolTip="�����������һ��" />
                        <pe:InteractiveMessager ID="InteractiveMessagerTxtValidateCode" Text="��������֤��" ControlToMessage="TxtValidateCode"
                            ValidatorOkMessage="��֤��������" runat="server"></pe:InteractiveMessager>
                        <pe:RequiredFieldValidator ID="ValrValidateCode" runat="server" ErrorMessage="��������֤�룡"
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
