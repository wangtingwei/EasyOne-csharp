<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Controls.WebPart.SystemDiagnostics" Codebehind="SystemDiagnostics.ascx.cs" %>
<table style="width: 100%" border="0" cellpadding="2" cellspacing="1" class="border">
    <tr class="tdbg">
        <td class="tdbgleft">
            系统版本号：
        </td>
        <td>
            <span id="pe_systeminfo_Version" runat="server"></span>
        </td>
    </tr>
    <tr class="tdbg">
        <td class="tdbgleft">
            系统启动时间：
        </td>
        <td>
            <span id="pe_systeminfo_StartTime"></span>
        </td>
    </tr>
    <tr class="tdbg">
        <td class="tdbgleft">
            系统已运行时间：
        </td>
        <td>
            <span id="pe_systeminfo_Runtime"></span>
        </td>
    </tr>
    <tr class="tdbg">
        <td class="tdbgleft">
            系统CPU使用率：
        </td>
        <td>
            <span id="pe_systeminfo_CpuUseRatio"></span>
        </td>
    </tr>
    <tr class="tdbg">
        <td class="tdbgleft">
            系统分配的物理内存量：
        </td>
        <td>
            <span id="pe_systeminfo_Memory"></span>
        </td>
    </tr>
    <tr class="tdbg">
        <td class="tdbgleft">
            缓存对象数：
        </td>
        <td>
            <span id="pe_systeminfo_Caches"></span>
        </td>
    </tr>
</table>

<script type="text/javascript" language="javascript">
function CallTheServer()
{
    EasyOne.WebSite.Admin.Profile.GetSystemDiagnostics.SystemDiagnostics(onCompleted,FailedCallback);
}

function FailedCallback(error)
{
    //error.set_errorHandled(true);
}
function onCompleted(value)
{
    $get("pe_systeminfo_Memory").innerHTML = value.Memory;
    $get("pe_systeminfo_StartTime").innerHTML = value.StartTime;
    $get("pe_systeminfo_Runtime").innerHTML = value.Runtime;
    $get("pe_systeminfo_CpuUseRatio").innerHTML = value.CpuUseRatio;
    $get("pe_systeminfo_Caches").innerHTML = value.Caches;
    
    setTimeout("CallTheServer()",2000)
}

CallTheServer();
</script>

