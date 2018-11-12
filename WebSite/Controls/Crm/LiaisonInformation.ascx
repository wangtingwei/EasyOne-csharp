<%@ Control Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Controls.Crm.LiaisonInformation" Codebehind="LiaisonInformation.ascx.cs" %>
        <tr class="tdbg">
            <td style="width: 15%" rowspan="2" align="right" class="tdbgleft">
                通讯地址：</td>
            <td colspan="3" style="height: 23px" align="left">
                <pe:Region ID="Region1" runat="server" />
            </td>
        </tr>
        <tr class="tdbg">
            <td colspan="3">
                <table border="0" cellpadding="2" cellspacing="1" style="width: 100%; background-color: white;">
                    <tr class="tdbg">
                        <td style="width: 16%" align="right" class="tdbgleft">
                            联系地址：</td>
                        <td>
                            <asp:TextBox ID="TxtAddress" Width="300px" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr class="tdbg">
                        <td align="right" class="tdbgleft">
                            邮政编码：</td>
                        <td>
                            <asp:TextBox ID="TxtZipCode" runat="server"></asp:TextBox>
                            <pe:ZipCodeValidator ID="VziptZipCode" ControlToValidate="TxtZipCode" SetFocusOnError="true" Display="dynamic" runat="server"></pe:ZipCodeValidator>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="tdbg">
            <td style="width: 15%" align="right" class="tdbgleft">
                办公电话：</td>
            <td style="width: 38%">
                <asp:TextBox ID="TxtOfficePhone" runat="server" onblur="CheckSameOfficePhone(this.value)"></asp:TextBox>
                <asp:Label ID="LblValidOfficePhone" ForeColor="Red" Text="办公电话已有会员使用，不能重复！" style="display:none;" runat="server"></asp:Label>
                <pe:TelephoneValidator ID="VtelOfficePhone" ControlToValidate="TxtOfficePhone" SetFocusOnError="true" Display="dynamic" runat="server"></pe:TelephoneValidator>
            </td>
            <td style="width: 15%" align="right" class="tdbgleft">
                住宅电话：</td>
            <td style="width: 38%">
                <asp:TextBox ID="TxtHomePhone" runat="server" onblur="CheckSameHomePhone(this.value)"></asp:TextBox>
                <asp:Label ID="LblHomePhone" ForeColor="Red" Text="住宅电话已有会员使用，不能重复！" style="display:none;" runat="server" ></asp:Label>
                <pe:TelephoneValidator ID="VtelHomePhone" ControlToValidate="TxtHomePhone" SetFocusOnError="true" Display="dynamic" runat="server"></pe:TelephoneValidator>
            </td>
        </tr>
        <tr class="tdbg">
            <td style="width: 15%" align="right" class="tdbgleft">
                移动电话：</td>
            <td>
                <asp:TextBox ID="TxtMobile" runat="server" onblur="CheckSameMobile(this.value)"></asp:TextBox>
                <asp:Label ID="LblMobile" ForeColor="Red" style="display:none;" runat="server" Text="移动电话已有会员使用，不能重复！"></asp:Label>
                <pe:MobileValidator ID="VmblMobile" ControlToValidate="TxtMobile" SetFocusOnError="true" Display="dynamic" runat="server"></pe:MobileValidator>
            </td>
            <td style="width: 15%" align="right" class="tdbgleft">
                传真号码：</td>
            <td>
                <asp:TextBox ID="TxtFax" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg">
            <td style="width: 15%" align="right" class="tdbgleft">
                小灵通：</td>
            <td>
                <asp:TextBox ID="TxtPHS" runat="server"></asp:TextBox></td>
            <td class="tdbgleft" align="right">
            </td>
            <td>
            </td>
        </tr>
        <tr class="tdbg">
            <td style="width: 15%" align="right" class="tdbgleft">
                个人主页：</td>
            <td>
                <asp:TextBox ID="TxtHomepage" runat="server" onblur="CheckSameHomepage(this.value)"></asp:TextBox>
                <asp:Label ID="LblHomepage" ForeColor="Red" style="display:none;" Text="已有会员使用了此主页，不能重复！" runat="server"></asp:Label>
                </td>
            <td class="tdbgleft" align="right">
                Email地址：</td>
            <td>
                <asp:TextBox ID="TxtEmail" runat="server"></asp:TextBox>
                <pe:EmailValidator ID="VmailEmail" ControlToValidate="TxtEmail" SetFocusOnError="true" Display="dynamic"  runat="server"></pe:EmailValidator>
            </td>
        </tr>
        <tr class="tdbg">
            <td style="width: 15%" align="right" class="tdbgleft">
                QQ号码：</td>
            <td>
                <asp:TextBox ID="TxtQQ" runat="server" onblur="CheckSameQQ(this.value)"></asp:TextBox>
                    <asp:Label runat="server" ForeColor="Red" Text="已有会员使用了此QQ号码，不能重复！" style="display:none" ID="LblValidQQ"></asp:Label>
            </td>
            <td class="tdbgleft" align="right">
                MSN帐号：</td>
            <td>
                <asp:TextBox ID="TxtMSN" runat="server" onblur="CheckSameMsn(this.value)"></asp:TextBox>
                <asp:Label ID="LblValidMsn" Text="已有会员使用了此Msn号码，不能重复！" ForeColor="Red" style="display:none" runat="server" ></asp:Label>
            </td>
        </tr>
        <tr class="tdbg">
            <td style="width: 15%" align="right" class="tdbgleft">
                ICQ号码：</td>
            <td>
                <asp:TextBox ID="TxtICQ" runat="server"></asp:TextBox></td>
            <td style="width: 15%" align="right" class="tdbgleft">
                雅虎通帐号：</td>
            <td>
                <asp:TextBox ID="TxtYahoo" runat="server"></asp:TextBox></td>
        </tr>
        <tr class="tdbg">
            <td style="width: 15%" align="right" class="tdbgleft">
                UC帐号：</td>
            <td>
                <asp:TextBox ID="TxtUC" runat="server"></asp:TextBox></td>
            <td style="width: 15%" align="right" class="tdbgleft">
                Aim帐号：</td>
            <td>
                <asp:TextBox ID="TxtAim" runat="server"></asp:TextBox>
<asp:HiddenField ID="HdnQQ" runat="server" />
<asp:HiddenField ID="HdnMsn" runat="server" />
<asp:HiddenField ID="HdnHomepage" runat="server" />
<asp:HiddenField ID="HdnMobile" runat="server" />
<asp:HiddenField ID="HdnHomephone" runat="server" />
<asp:HiddenField ID="HdnOfficePhone" runat="server" />
</td>
        </tr>
       <script type="text/javascript">
            var oldQQ = document.getElementById("<%=TxtQQ.ClientID %>").value;
            var oldMsn = document.getElementById("<%=TxtMSN.ClientID %>").value;
            var oldOfficePhone = document.getElementById("<%=TxtOfficePhone.ClientID %>").value;
            var oldHomePhone = document.getElementById("<%= TxtHomePhone.ClientID %>").value;
            var oldMobile = document.getElementById("<%=TxtMobile.ClientID %>").value;
            var oldPage =document.getElementById("<%=TxtHomepage.ClientID %>").value;
            var validArray = [false,false,false,false,false,false];
            function CheckSameQQ(value)
            {
                if(value != oldQQ){
                    CallbackToServer("$QQ"+value);
                    oldQQ = value;
                }
            }
            function CheckSameMsn(value)
            {
                if(value != oldMsn){
                    CallbackToServer("$Msn" + value);
                    oldMsn = value;
                }
            }
            function CheckSameOfficePhone(value)
            {
                if(value != oldOfficePhone){
                    CallbackToServer("$OP" + value);
                    oldOfficePhone = value;
                }
            }
            function CheckSameHomePhone(value)
            {
                if(value != oldHomePhone){
                    CallbackToServer("$HP"+value);
                    oldHomePhone = value;
                }
            }
            function CheckSameMobile(value)
            {
                if(value != oldMobile){
                    CallbackToServer("$MP" + value);
                    oldMobile = value;
                }
            }
            function CheckSameHomepage(value)
            {
                if(value != oldPage){
                    CallbackToServer("$Page" + value);
                    oldPage = value;
                }
            }
            
            function CallbackEventReference(arg,context)
            {
                    var result = eval("("+arg+")");
                    switch(result.name){
                        case "QQ":
                           validArray[0] = result.value;
                           document.getElementById("<%=LblValidQQ.ClientID %>").style.display= result.value ? "" : "none";
                            break;
                        case "Msn":
                            validArray[1] = result.value;
                            document.getElementById("<%=LblValidMsn.ClientID %>").style.display = result.value ? "" :"none";                            
                            break;
                        case "OP":
                            validArray[2] = result.value;
                            document.getElementById("<%=LblValidOfficePhone.ClientID %>").style.display = result.value ? "" : "none";                          
                            break;
                        case "HP":
                            validArray[3] = result.value;
                            document.getElementById("<%=LblHomePhone.ClientID %>").style.display = result.value ? "": "none";
                            break;
                        case "MP":
                            validArray[4] = result.value;
                            document.getElementById("<%=LblMobile.ClientID %>").style.display = result.value ? "" : "none";
                            break;
                        case "Page":
                            validArray[5] = result.value;
                            document.getElementById("<%=LblHomepage.ClientID %>").style.display = result.value ? "" : "none";
                        break;                        
                        default:
                        break;
                    }
            }
            
            function LiaisonIsValid()
            {
                for(var index in validArray)
                {
                    if(validArray[index])
                    {
                        return false;
                    }
                }
                return true;
            }
            
       </script>