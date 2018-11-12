<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Install.Default" Codebehind="Default.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>安装向导</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rev="stylesheet" media="all" href="images/styles.css" type="text/css" rel="stylesheet" />
        <script type="text/javascript">
        function ShowProgress()
        {
            var labelDoingID=document.getElementById("LblCreateDataProgress");
            var labelBeforeID=document.getElementById("LblCreateDataBaseBefore");
            
            if(labelDoingID!=null)
            {
                labelDoingID.style.visibility="visible";
            }
            if(labelBeforeID!=null)
            {
                labelBeforeID.innerText="正在创建数据库。";
            }
        }
    </script>
</head>
<body>
    <div class="top">
        <form id="search" action="http://help.EasyOne.net/search.asp" method="post">
            <dl>
                <dt class="linking"><a href="http://www.EasyOne.net/" target="_blank" title="访问动易官方网站">
                    EasyOne.net</a> | <a href="http://EasyOne.net/soft/" target="_blank" title="免费下载动易系列软件产品">
                        免费下载</a> | <a href="http://EasyOne.net/User/" target="_blank" title="动易官方网站客户自助服务">
                            客户自助服务</a> | <a href="http://bbs.EasyOne.net/" target="_blank" title="今天您上动易论坛了吗？">
                                动易论坛</a></dt>
                <dt class="search"><span style="width: 320px; height: 22px; height: 26px; padding: 6px 0 0 0;
                    padding: 2px 0 0 0; _padding: 4px 0 0 0; overflow: hidden; float: right;"><span style="float: right;
                        padding: 0px 0 0 10px; padding: 2px 0 0 10px;">
                        <input id="Submit" style="border: 0px; width: 47px; height: 20px;" type="image" src="Images/search_but.gif"
                            name="Submit" />
                    </span><span>
                        <input name="Keyword" class="input_sea" id="Keyword" onclick="value=''" onmouseover="this.style.backgroundColor='#ffffff'"
                            onmouseout="this.style.backgroundColor='#ebf7ff'" value="动易全站搜索" size="35" />
                        <input name="ModuleName" type="hidden" id="ModuleName" value="Article" />
                        <input id="Field" type="hidden" value="Title" name="Field" />
                    </span></span></dt>
            </dl>
        </form>
    </div>
    <div class="con" runat="server">
        <form id="form1" runat="server">
            <asp:Wizard ID="WzdInstall" OnNextButtonClick="WzdInstall_NextButtonClick" OnFinishButtonClick="WzdInstall_FinishButtonClick"
                runat="server" ActiveStepIndex="0" DisplaySideBar="False" Width="100%">
                <WizardSteps>
                    <asp:WizardStep ID="WizardStep1" runat="server" Title="Step 1">
                        <div id="DivDefault" runat="server" class="left140">
                            <dl>
                                <dt class="title">欢迎安装
                                    <asp:Label ID="LblProductName1" runat="server" Text="Label"></asp:Label>
                                </dt>
                                <dd class="message">
                                    欢迎您选择安装
                                    <asp:Label ID="LblProductName2" runat="server" Text="Label"></asp:Label>
                                    ！<br />
                                    本向导将协助您一步步的安装此软件。<br />
                                    建议您在运行本向导前仔细阅读程序包中的《安装说明》文档，如果您已经阅读过，请点击下一步。<br />
                                    <table width="82%" border="0" style="text-align: left;" cellpadding="1" cellspacing="1"
                                        class="border">
                                        <tr class='tdbg'>
                                            <td style="height: 18px" align="center">
                                                <strong>阅读许可协议</strong></td>
                                        </tr>
                                        <tr class="tdbg">
                                            <td align="center">
                                                <textarea id="TxtLicense" style="width: 99%; height: 180px" cols="100" rows="12"
                                                    runat="server" readonly="readonly">                                
                                                    </textarea> </td>
                                        </tr>
                                        <tr class="tdbg">
                                            <td align='left'>
                                                <asp:CheckBox ID="ChlkAgreeLicense" AutoPostBack="True" OnCheckedChanged="ChlkAgreeLicense_CheckedChanged"
                                                    runat="server" />
                                                <label for="ChlkAgreeLicense">
                                                    我已经阅读并同意此协议</label></td>
                                        </tr>
                                    </table>
                                </dd>
                            </dl>
                        </div>
                        <div class="left140">
                            <div class="left140_cen">
                                <asp:Button ID="StartNextButton" runat="server" Enabled="False" CssClass="button_link"
                                    CommandName="MoveNext" Text="下一步" />
                            </div>
                        </div>
                    </asp:WizardStep>
                    <asp:WizardStep ID="WizardStep2" runat="server" Title="Step 2">
                        <div id="DivInstall1" runat="server" class="left140">
                            <dl>
                                <dt class="check">
                                    <img alt="" src="Images/ico01.gif"  />
                                    现在对您的运行环境进行检测，以确认您的环境符合要求。</dt><dd><asp:Table ID="TblInstallFileCheck" CellSpacing="1"
                                        CellPadding="1" CssClass="table_date" runat="server">
                                    </asp:Table>

                                    </dd>
                            </dl>
                        </div>
                                                <div class="left140">
                            <div class="left140_cen">
                                                                    <asp:Button ID="PreviousButtonStep2" runat="server" CausesValidation="False" CssClass="button_link"
                                            CommandName="MovePrevious" Text="上一步" />
                                        <asp:Button ID="NextButtonStep2" runat="server" CssClass="button_link" CommandName="MoveNext" Visible="true" 
                                            Text="下一步" />
                            </div>
                        </div>
                    </asp:WizardStep>
                    <asp:WizardStep ID="WizardStep3" runat="server" Title="Step 3">
                        <div id="DivInstall2" runat="server" class="left140">
                            <dl>
                                <dt class="check">
                                    <img alt="" src="Images/ico01.gif" />下面进行数据库连接设置</dt><dd><label
                                        style="color: Blue;">请确保设置好的数据库中没有旧的数据表和存储过程。</label>
                                        <table cellspacing="1" cellpadding="1" class="table_date">
                                            <tr>
                                                <td style="width: 235px">
                                                    请选择数据库版本：</td>
                                                <td>
                                                    <asp:DropDownList ID="DropSqlVersion" runat="server">
                                                        <asp:ListItem Value="2000">Sql Server 2000</asp:ListItem>
                                                        <asp:ListItem Selected="True" Value="2005">Sql Server 2005</asp:ListItem>
                                                    </asp:DropDownList></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 235px">
                                                    数据源：</td>
                                                <td>
                                                    <asp:TextBox ID="TxtDataSource" runat="server" Width="150px"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="ValrDataSource" runat="server" ControlToValidate="TxtDataSource"
                                                        ErrorMessage="*"></asp:RequiredFieldValidator></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 235px">
                                                    数据库名称：</td>
                                                <td>
                                                    <asp:TextBox ID="TxtDataBase" runat="server" Width="150px"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="ValrDataBase" runat="server" ControlToValidate="TxtDataBase"
                                                        ErrorMessage="*"></asp:RequiredFieldValidator></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 235px">
                                                    数据库用户名称：</td>
                                                <td>
                                                    <asp:TextBox ID="TxtUserID" runat="server" Width="150px"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="ValrUserID" runat="server" ControlToValidate="TxtUserID"
                                                        ErrorMessage="*"></asp:RequiredFieldValidator></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 235px">
                                                    数据库用户口令：</td>
                                                <td>
                                                    <asp:TextBox ID="TxtPassword" runat="server" TextMode="Password" Width="150px"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="ValrPassword" runat="server" ControlToValidate="TxtPassWord"
                                                        ErrorMessage="*"></asp:RequiredFieldValidator></td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <div class="center">
                                                        <asp:Label ID="LblCheckConnectString" Visible="False" runat="server" ForeColor="Red">请检查数据库连接字符串设置是否正确或数据库服务器身份验证模式是否SQL Server和Windows混合模式！</asp:Label></div>
                                                </td>
                                            </tr>
                                        </table>
                                        
                                    </dd>
                            </dl>
                        </div>
                                                                        <div class="left140">
                            <div class="left140_cen">
                                                                                                <asp:Button ID="PreviousButtonStep3" runat="server" CausesValidation="False" CssClass="button_link"
                                            CommandName="MovePrevious" Text="上一步" OnClick="PreviousButtonStep3_Click" />
                                        <asp:Button ID="NextButtonStep3" runat="server" CssClass="button_link" CommandName="MoveNext"
                                            Text="下一步" />
                            </div>
                        </div>
                    </asp:WizardStep>
                    <asp:WizardStep ID="WizardStep4" runat="server" Title="Step 4">
                        <div id="DivInstall3" runat="server" class="left140">
                            <dl>
                                <dt class="check">
                                    <img alt="" src="Images/ico01.gif" />下面将创建数据库，大约需要1～2分钟。</dt><dd><label
                                        id="LblCreateDataBaseMessage" runat="server" style="visibility: visible;">点击“<strong>开始创建</strong>”按钮开始后，请耐心等候。</label>
                                        <br />
                                        <br />
                                        <table cellspacing="1" cellpadding="1" class="table_date">
                                            <tr>
                                                <td style="height: 33px;">
                                                    <label id="LblCreateDataBaseBefore" runat="server" style="visibility: visible;">
                                                        准备创建数据库。</label>
                                                </td>
                                                <td style="width: 20%;">
                                                    <div class="center">
                                                        <label id="LblCreateDataProgress" runat="server" style="visibility: hidden;">
                                                            创建中。。。</label>
                                                        <img id="ImgCreateDataBaseOK" visible="False" runat="server" src="images/ok.gif"
                                                            width="16" height="16" />
                                                        <img id="ImgCreateDataBaseFail" visible="False" runat="server" src="images/error.gif"
                                                            width="16" height="16" />
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                        <asp:CheckBox ID="ChlkIsCreateDataBase" runat="server" AutoPostBack="True" OnCheckedChanged="ChlkIsCreateDataBase_CheckedChanged" />
                                        <label style="color: blue" for="ChlkIsCreateDataBase">
                                            如果数据库已创建好，可跳过这一步。</label>
                                    </dd>
                            </dl>
                        </div>
                                                                                                <div class="left140">
                            <div class="left140_cen">
                                        <asp:Button ID="PreviousButtonStep4" runat="server" CausesValidation="False" CssClass="button_link"
                                            CommandName="MovePrevious" Text="上一步" />
                                        <asp:Button ID="NextButtonStep4" runat="server" CommandName="MoveNext" CssClass="button_link"
                                            Text="下一步" Enabled="False" />
                                        <asp:Button ID="BtnCreateDateBase" CssClass="button_link" runat="server" OnClientClick="ShowProgress();" Text="开始创建"
                                            OnClick="BtnCreateDateBase_Click" />
                            </div>
                        </div>
                    </asp:WizardStep>
                    <asp:WizardStep ID="WizardStep5" runat="server" Title="Step 5">
                        <div id="DivInstall4" runat="server" class="left140">
                            <dl>
                                <dt class="check">
                                    <img alt="" src="Images/ico01.gif" />下面进行配置文件设置。</dt>
                                <dd>
                                    <table cellspacing="1" cellpadding="1" class="table_date">
                                        <tr>
                                            <td style="width: 30%">
                                                网站标题：</td>
                                            <td>
                                                <asp:TextBox ID="TxtSiteTitle" runat="server" Width="150px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="ValrSiteTitle" runat="server" ControlToValidate="TxtSiteTitle"
                                                    ErrorMessage="*"></asp:RequiredFieldValidator></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                网站地址：</td>
                                            <td>
                                                <asp:TextBox ID="TxtSiteUrl" runat="server" Width="150px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="ValrSiteUrl" runat="server" ControlToValidate="TxtSiteUrl"
                                                    ErrorMessage="*"></asp:RequiredFieldValidator></td>
                                        </tr>
                                        <tr>
                                            <td style="height: 16px">
                                                后台管理目录：</td>
                                            <td style="height: 16px">
                                                <asp:TextBox ID="TxtManageDir" runat="server" Width="150px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="ValrManageDir" runat="server" ControlToValidate="TxtManageDir"
                                                    ErrorMessage="*"></asp:RequiredFieldValidator></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                后台管理认证码：</td>
                                            <td>
                                                <asp:TextBox ID="TxtSiteManageCode" runat="server" Width="150px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="ValrSiteManageCode" runat="server" ControlToValidate="TxtSiteManageCode"
                                                    ErrorMessage="*"></asp:RequiredFieldValidator></td>
                                        </tr>
                                        <tr>
                                            <td style="height: 15px" colspan="2">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                管理员名称：</td>
                                            <td>
                                                <asp:TextBox ID="TxtAdminName" runat="server" Width="150px" Enabled="False">Admin</asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                管理员密码：</td>
                                            <td>
                                                <asp:TextBox ID="TxtAdminPassword" runat="server" Width="150px" TextMode="Password"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="ValrAdminPassword" runat="server" ControlToValidate="TxtAdminPassword"
                                                    ErrorMessage="*"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                确认密码：</td>
                                            <td>
                                                <asp:TextBox ID="TxtAdminPasswordAgain" runat="server" Width="150px" TextMode="Password"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="ValrAdminPasswordAgain" runat="server" ControlToValidate="TxtAdminPasswordAgain"
                                                    ErrorMessage="*"></asp:RequiredFieldValidator>
                                                <asp:CompareValidator ID="ValcAdminPasswordAgain" runat="server" ErrorMessage="两次密码不相同"
                                                    ControlToCompare="TxtAdminPassword" ControlToValidate="TxtAdminPasswordAgain"></asp:CompareValidator></td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <div class="center">
                                                   <pe:ExtendedLabel ID="LblErrorMessage" HtmlEncode="false" runat="server" ForeColor="Red"></pe:ExtendedLabel>
                                                   </div>
                                            </td>
                                        </tr>
                                    </table>
 
                                </dd>
                            </dl>
                        </div>
                                                                                                                        <div class="left140">
                            <div class="left140_cen">
                                    <asp:Button ID="PreviousButtonStep5" runat="server" CssClass="button_link" CausesValidation="False"
                                        CommandName="MovePrevious" Text="上一步" />
                                    <asp:Button ID="NextButtonStep5" runat="server" CssClass="button_link" CommandName="MoveNext"
                                        Text="下一步" />
                            </div>
                        </div>
                    </asp:WizardStep>
                    <asp:WizardStep ID="WizardStep6" runat="server" Title="Step 6">
                        <div id="DivIntallComplete" runat="server" class="left140">
                            <dl>
                                <dt class="check">
                                    <img alt="" src="Images/ico01.gif"/>安装完成。 </dt>
                                <dd class="message">
                                    已经成功安装！<br/>安装结束后，无需重命名或删除此安装文件，系统会自动限制此文件的访问。
<br/>请点击“<strong>完成</strong>”按钮跳转到首页。
                                    <input id="HdnPassword" type="hidden" visible="False" runat="server" >
                                        
                                        

</input>


                                        
                                        

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                                    <div class="clearbox">
                                    
                                            
&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; </div>
                                        
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    
                                    <div class="clearbox"></div>
                                </dd>
                            </dl>
                        </div>
                    </asp:WizardStep>
                </WizardSteps>
                <StepNavigationTemplate>
                    <center>
                    </center>
                </StepNavigationTemplate>
                <StartNavigationTemplate>
                    <center>
                    </center>
                </StartNavigationTemplate>
                <FinishNavigationTemplate>
                    <center>
                        <br />
                         <asp:Button ID="PreviousButtonFinish" runat="server" CausesValidation="False" CssClass="button_link"
                                            CommandName="MovePrevious" Text="上一步" />
                        <asp:Button ID="FinishButton" runat="server" CssClass="button_link" CommandName="MoveComplete"
                            Text="完成" />
                    </center>
                </FinishNavigationTemplate>
            </asp:Wizard>
        </form>
    </div>
    <div class="bottom">
        Powered by <a href="http://www.EasyOne.net" target="_blank">
            <asp:Label ID="LblProductName3" runat="server" Text="Label"></asp:Label>
        </a>
        <br />
        <a href="http://www.EasyOne.net" target="_blank">
            <asp:Label ID="LblProductCopyright" runat="server" Text="Label"></asp:Label>
        </a>
    </div>
</body>
</html>
