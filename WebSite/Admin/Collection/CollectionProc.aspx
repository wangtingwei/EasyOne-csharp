<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Collection.CollectionProc" Codebehind="CollectionProc.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>采集进程页</title>
</head>
<body>
    <form id="form1" runat="server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
    <br />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        <Services>
            <asp:ServiceReference Path="../../Admin/Collection/GetCreateProgressService.asmx" />
        </Services>
    </asp:ScriptManager>

    <script language="javascript" type="text/javascript">
        var timeout;
        function startTask()
        {
            EasyOne.WebSite.Admin.Collection.GetCreateProgressService.AcquireProgress(onTaskCompleted,onErrorCompleted);
        }
        
        function onErrorCompleted(error)
        {
            $get("pb").style.display='none';
            $get("plan").style.display='none'; 
            $get("cartoon").style.display='none'; 
            $get("back").style.display='none'; 
            $get("ExeTime").innerHTML = "";
            $get("Message").innerHTML = "";
            $get("lblError").innerHTML = "网络或服务器错误！";
            $get("Div2").style.display="";
           // error.set_errorHandled(true); 
        }
        
        function onTaskCompleted(value)
        {
            if(value == null)
            {
                $get("NotAssignment").style.display="";
            }
            else if (value.ErrorMessage != ""){
                $get("pb").style.display='none';
                $get("plan").style.display='none'; 
                $get("cartoon").style.display='none'; 
                $get("back").style.display='none'; 
                $get("ExeTime").innerHTML = "";
                $get("Message").innerHTML = "";
                $get("Div2").style.display="";  
                $get("lblError").innerHTML = value.ErrorMessage;
            }
            else
            {
                if (value.IsInput) {
                    $get("pb").style.display='';
                    $get("plan").style.display=''; 
                    $get("cartoon").style.display=''; 
                    $get("back").style.display=''; 
                    $get("pb").style.width = value.Progress;
                    $get("pb").innerHTML = value.Progress == "100%" ? "采集完成!" : value.Progress;
                    $get("Message").innerHTML = value.Message;
                    $get("ExeTime").innerHTML = "执行时间：" + value.ExecutionTime;
                    
                    if(value.ErrorMessage != null)
                    {
                        $get("ErrorMessage").innerHTML = value.ErrorMessage;
                    }
                    if(value.Progress != "100%" || value.CollectionEnd == false)
                    {
                       timeout=setTimeout("startTask()",5000);
                    }
                    else
                    {
                        $get("BtnStopCreate").style.display='none';
                        $get("CreateHtmlImage").src='../../Admin/Images/accomplish.gif';
                        
                        if (value.IsCreateHtml)
                        {
                            setTimeout("CreateHtml('" + value.CreateWorkId + "')",2000);
                        }
                    }
                }
                else
                {
                    $get("pb").style.display='none';
                    $get("plan").style.display='none'; 
                    $get("cartoon").style.display='none'; 
                    $get("back").style.display='none'; 
                    $get("ExeTime").innerHTML = "";
                    $get("Message").innerHTML = value.Message;
                    timeout=setTimeout("startTask()",5000);
                }
            }
        }
        function CreateHtml(createId)
        {
            window.location='../Contents/CreateHtmlProgress.aspx?workId=' + createId;
        }
        
    </script>

    <div id="NotAssignment" style="display: none;">
        <div>
            <br />
            <br />
            <table cellpadding="2" cellspacing="1" border="0" width="400" class="border" align="center">
                <tr align="center" class="title">
                    <td>
                        <strong>提示信息</strong>
                    </td>
                </tr>
                <tr class="tdbg">
                    <td valign="top" style="height: 100px;">
                        <br />
                        当前没有采集任务在执行！
                    </td>
                </tr>
                <tr align="center" class="tdbg">
                    <td>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div id="Div2" style="display: none;">
        <div>
            <br />
            <br />
            <table cellpadding="2" cellspacing="1" border="0" width="400" class="border" align="center">
                <tr align="center" class="title">
                    <td>
                        <strong>发生错误！</strong>
                    </td>
                </tr>
                <tr class="tdbg">
                    <td valign="top" style="height: 100px;">
                        <br />
                        <div id="lblError" style="margin-left: 5%;">
                        </div>
                    </td>
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
            Visible="False" /><asp:HiddenField ID="HdnIsCreateHtml" runat="server" Value="false" />
        &nbsp;&nbsp;&nbsp;
        <input id="back" style="display: none" type="button" class="inputbutton" value="返回上一步"
            onclick="javascript:window.location='CollectionMain.aspx'" />
    </div>
    <div id="Message" style="margin-left: 5%;">
    </div>
    <div id="ErrorMessage" style="margin-left: 5%;">
    </div>
    </form>

    <script language="javascript" type="text/javascript">
    startTask();
    </script>

</body>
</html>
