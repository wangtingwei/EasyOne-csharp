<%@ Control Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Controls.CreateHtmlProgress" Codebehind="CreateHtmlProgress.ascx.cs" %>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Services>
                <asp:ServiceReference Path="~/WebServices/GetCreateProgressService.asmx" />
            </Services>
        </asp:ScriptManager>

        <script language="javascript" type="text/javascript">
        var timeout;
        function startTask()
        {
            var workId=$get("<%=HdnWorkId.ClientID %>").value;
            EasyOne.WebSite.Admin.Contents.GetCreateProgressService.AcquireProgress(workId,onTaskCompleted);
        }
        
        function onTaskCompleted(value)
        {
            if(value == null||value.Progress==null||value.Progress=='')
            {
                $get("NotAssignment").style.display="";
            }
            else
            {
                $get("pb").style.display='';
                $get("plan").style.display=''; 
                $get("cartoon").style.display=''; 
                $get("back").style.display=''; 
                $get("pb").style.width = value.Progress;
                $get("pb").innerHTML = parseInt(value.Progress) == 100 ? "生成完成!" : value.Progress;
                $get("Message").innerHTML = value.Message;
                $get("ExeTime").innerHTML = "执行时间：" + value.ExecutionTime;
                $get("RemainingTime").innerHTML = "&nbsp;估计剩余时间：" + value.RemainingTime;
                if(value.ErrorMessage != null)
                {
                    $get("ErrorMessage").innerHTML = value.ErrorMessage;
                }
                if(parseInt(value.Progress) < 100)
                {
                   timeout=setTimeout("startTask()",1000);
                }
                else
                {
                  if(parseInt(value.Progress) ==100)
                  {
                    $get("<%=BtnStopCreate.ClientID %>").style.display='none';
                    $get("CreateHtmlImage").src='../../Admin/Images/accomplish.gif'; 
                    $get("Message").innerHTML = value.Message;
                    $get("ExeTime").innerHTML = "执行时间：" + value.ExecutionTime;
                    $get("RemainingTime").innerHTML = "&nbsp;估计剩余时间：0秒";
                  }
                }
            }
        }      
        </script>

        <div id="NotAssignment" style="display: none;">
            <asp:HiddenField ID="HdnWorkId" runat="server" />
            <div>
                <br />
                <br />
                <table cellpadding="2" cellspacing="1" border="0" width="400" class="border" align="center">
                    <tr align="center" class="title">
                        <td>
                            <strong>提示信息</strong></td>
                    </tr>
                    <tr class="tdbg">
                        <td valign="top" style="height: 100px;">
                            <br />
                            当前没有生成任务在执行！</td>
                    </tr>
                    <tr align="center" class="tdbg">
                        <td>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div id="cartoon" style="margin-left: 20%; margin-top: 2%; display: none">
            <img id="CreateHtmlImage" src="../../Admin/Images/convection.gif" alt="" />
        </div>
        <div id="Div1" style="margin-left: auto; margin-right: auto; margin-bottom: 20px;
            width: 80%;">
            <div id="ExeTime" style="float: left;">
            </div>
            <div id="RemainingTime" style="float: left;">
            </div>
        </div>
        <div id="plan" style="margin-left: auto; margin-right: auto; margin-bottom: 20px;
            width: 80%; background-color: #FFFFFF; border-bottom: #000000 1px solid; border-left: #000000 1px solid;
            border-right: #000000 1px solid; border-top: #000000 1px solid; display: none;">
            <div id="pb" style="text-align: center; background-color: Green; color: White; clear: both;
                height: 22px; display: none;">
            </div>
        </div>
        <div style="margin-left: 45%;">
            <asp:Button ID="BtnStopCreate" runat="server" OnClick="BtnStopCreate_Click" Text="任务中止"
                Visible="False" />
            &nbsp;&nbsp;&nbsp;
            <input id="back" style="display: none" type="button" class="inputbutton" value="返回上一步"
                onclick="javascript:window.location='<%= m_UrlReferrer %>'" />
        </div>
        <div id="Message" style="margin-left: 5%;">
        </div>
        <div id="ErrorMessage" style="margin-left: 5%;">
        </div>
    </form>

    <script language="javascript" type="text/javascript">
    startTask();
    </script>