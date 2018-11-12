<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" ValidateRequest="false" Inherits="EasyOne.WebSite.Admin.Template.IncludeFileUI" Codebehind="IncludeFile.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
     <style type="text/css">
    <!-- 
    .dragspandiv{
        background-color: #FFFBF5;
        FILTER: alpha(opacity=70);
        border: 1px solid #F6B9D6;
        text-align: center;
        overflow:hidden;
        padding:2px;
        height:20px;
    }
    .spanfixdiv{
        background-color: #FFFBF5;
        border: 1px solid #F6B9D6;
        text-align: center;
        overflow:hidden;
        cursor: hand;
        height:20px;
        margin: 4px;
    }
    
    #fixdiv {margin: 7px;}
    
    .nodefixdiv 
    {
        background-color: #FFFBF5;
        border: 1px solid #F6B9D6;
        text-align: center;
        overflow:hidden;
        cursor: hand;
        padding:2px;
        height:20px;
    }
    .alertspandiv
    {
        background-color: #FFEBE5;
        border: 1px solid #F6B9D6;
        text-align: center;
        text-valign: middle; 
        padding:2px;
        height:30px;
        width:100px;
    }
        -->
</style>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" />
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td colspan="2" class="spacingtitle">
                <b>
                    <pe:AlternateLiteral ID="AltrTitle" Text="添加内嵌代码" AlternateAction="modify" AlternateText="修改内嵌代码"
                        runat="Server" />
                </b>
            </td>
        </tr>
        <tr class="tdbg">
            <td align="right" class="tdbgleft" style="width: 10%">
                <b>代码名称：</b>
            </td>
            <td>
                <asp:TextBox ID="TxtName" runat="server"></asp:TextBox><pe:RequiredFieldValidator
                    ID="ReqTxtName" ControlToValidate="TxtName" SetFocusOnError="true" runat="server"
                    ErrorMessage="名称不能为空" Display="Dynamic"></pe:RequiredFieldValidator>
            </td>
        </tr>
        <tr class="tdbg">
            <td align="right" class="tdbgleft">
                <b>简 介：</b>
            </td>
            <td>
                <asp:TextBox ID="TxtDescription" TextMode="MultiLine" Columns="40" Height="40" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg">
            <td align="right" class="tdbgleft">
                <b>代码类型：</b>
            </td>
            <td>
                <asp:RadioButtonList ID="RadlIncludeType" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="JS输出" Selected="True" Value="JSWriteHtml"></asp:ListItem>
                    <asp:ListItem Text="JS" Value="JS"></asp:ListItem>
                    <asp:ListItem Text="HTML" Value="Html"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr class="tdbg">
            <td align="right" class="tdbgleft">
                <b>关联类型：</b>
            </td>
            <td>
                <asp:RadioButtonList ID="RadlAssociateType" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal">
                    <asp:ListItem Text="不关联" Selected="True" Value="None"></asp:ListItem>
                    <asp:ListItem Text="关联节点" Value="Node"></asp:ListItem>
                    <asp:ListItem Text="关联专题" Value="Special"></asp:ListItem>
                </asp:RadioButtonList>
                <span style="color: Green;">注：关联节点或专题时，对节点或专题的修改将会自动刷新</span>
            </td>
        </tr>
        <tr class="tdbg">
            <td align="right" class="tdbgleft">
                <b>文 件 名：</b></td>
            <td>
                <asp:TextBox ID="TxtFileName" runat="server"></asp:TextBox><pe:RequiredFieldValidator
                    ID="ReqTxtFileName" ControlToValidate="TxtFileName" SetFocusOnError="true" runat="server"
                    ErrorMessage="文件名不能为空" Display="Dynamic"></pe:RequiredFieldValidator><asp:RegularExpressionValidator
                        ID="RegTxtFileName" SetFocusOnError="true" Display="Dynamic" ControlToValidate="TxtFileName"
                        runat="server"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr class="tdbg">
            <td align="right" class="tdbgleft">
                <b>模 板：</b></td>
            <td>
                <table style="width: 100%">
                    <tr>
                        <td style="width: 252px; vertical-align: top;" id="frmTitle">
                            <ajaxToolkit:TabContainer runat="server" ID="TabContainer1" Height="265px">
                                <ajaxToolkit:TabPanel runat="Server" ID="TabPanel1" HeaderText="内容标签">
                                    <ContentTemplate>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="DropLabelList" runat="server" OnSelectedIndexChanged="DropLabelList_SelectedIndexChanged"
                                                    AutoPostBack="true" />
                                                <div style="overflow: auto; float: left; width: 100%; height: 250px; text-align: center">
                                                    <pe:ExtendedLabel HtmlEncode="false" ID="LblLabelList" runat="server"></pe:ExtendedLabel>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>
                                <ajaxToolkit:TabPanel runat="Server" ID="TabPanel2" HeaderText="分页标签">
                                    <ContentTemplate>
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="DropPagerList" runat="server" OnSelectedIndexChanged="DropPagerList_SelectedIndexChanged"
                                                    AutoPostBack="true" />
                                                <div style="overflow: auto; float: left; width: 100%; height: 250px; text-align: center">
                                                    <pe:ExtendedLabel HtmlEncode="false" ID="LblPageList" runat="server"></pe:ExtendedLabel>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>
                                <ajaxToolkit:TabPanel runat="Server" ID="TabPanel3" HeaderText="固定标签" ToolTip="系统内部专用标签列表">
                                    <ContentTemplate>
                                        <div style="overflow: auto; float: left; width: 100%; height: 250px; text-align: center">
                                            <div code="installdir" onclick="cit()" outype="0" class="spanfixdiv">
                                                网站根目录</div>
                                            <div code="sitename" onclick="cit()" outype="0" class="spanfixdiv">
                                                网站名称</div>
                                            <div code="sitetitle" onclick="cit()" outype="0" class="spanfixdiv">
                                                网站标题</div>
                                            <div code="sitepath" onclick="cit()" outype="0" class="spanfixdiv">
                                                网站路径</div>
                                            <div code="logo" onclick="cit()" outype="0" class="spanfixdiv">
                                                网站LOGO</div>
                                            <div code="banner" onclick="cit()" outype="0" class="spanfixdiv">
                                                BANNER</div>
                                            <div code="webmaster" onclick="cit()" outype="0" class="spanfixdiv">
                                                站长名称</div>
                                            <div code="webmasteremail" onclick="cit()" outype="0" class="spanfixdiv">
                                                站长信箱</div>
                                            <div code="copyright" onclick="cit()" outype="0" class="spanfixdiv">
                                                版权申明</div>
                                            <div code="managedir" onclick="cit()" outype="0" class="spanfixdiv">
                                                管理目录</div>
                                            <div code="addir" onclick="cit()" outype="0" class="spanfixdiv">
                                                广告目录</div>
                                            <div code="metakeywords" onclick="cit()" outype="0" class="spanfixdiv">
                                                网站关键字</div>
                                            <div code="metadescription" onclick="cit()" outype="0" class="spanfixdiv">
                                                网站摘要</div>
                                            <div code="defaultcss" onclick="cit()" outype="0" class="spanfixdiv">
                                                默认CSS连接</div>
                                            <div code="timenow" onclick="cit()" outype="0" class="spanfixdiv">
                                                现在时间</div>
                                            <div code="uploaddir" onclick="cit()" outype="0" class="spanfixdiv">
                                                上传目录</div>
                                            <div code="createhtmlpath" onclick="cit()" outype="0" class="spanfixdiv">
                                                生成HTML路径</div>
                                            <div code="readfile path='文件路径'" onclick="cit()" outype="0" class="spanfixdiv">
                                                读取文本文件</div>
                                        </div>
                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>
                                <ajaxToolkit:TabPanel runat="Server" ID="TabPanel4" HeaderText="快速定位" OnClientClick="rebulideuselist">
                                    <ContentTemplate>
                                        <center>
                                            <input name="rbl" type="button" class="inputbutton" id="rbl" value="刷新列表" onclick="rebulideuselist()" /></center>
                                        <div id="thispagelabel" style="overflow: auto; float: left; width: 100%; height: 250px;
                                            text-align: center">
                                        </div>
                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>
                            </ajaxToolkit:TabContainer>
                        </td>
                        <td onclick="switchSysBar()" style="width: 12px; cursor: hand; cursor: pointer">
                            <img id="switchPoint" src="../../Admin/images/butC.gif" height="265px" alt="关闭标签"
                                style="border: 0px; width: 12px;" />
                        </td>
                        <td style="vertical-align: top;">
                            <asp:TextBox ID="TxtTemplate" runat="server" Width="98%" Height="265px" TextMode="MultiLine"></asp:TextBox>
                            <div style="text-align: center; vertical-align: top;">
                                <img alt="增加高度" src="../../Admin/Images/sizeplus.gif" onclick="sizeChange(50)" />
                                <img alt="减少高度" src="../../Admin/Images/sizeminus.gif" onclick="sizeChange(-50)" />
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="tdbg">
            <td colspan="2" align="center">
                <asp:Button ID="BtnSaveIncludeFile" runat="server" Text="保存" OnClick="BtnSaveIncludeFile_Click" /></td>
        </tr>
    </table>
<script language="JavaScript" type="text/javascript">
<!--
var start=0, end=0;
var x,y;
var dragspan;
var inserttext;
var nn6=document.getElementById&&!document.all;
var isdrag=false;
var labeltype = '0';

function initDrag(e) {
    var oDragHandle = nn6 ? e.target : event.srcElement;
    if (oDragHandle.className=="spanfixdiv")
    {
        isdrag = true;
        dragspan = document.createElement('div');
        dragspan.style.position = "absolute";
        dragspan.className = "dragspandiv";
        y = nn6 ? e.clientY + 5 : event.clientY + 5;
        x = nn6 ? e.clientX + 10 : event.clientX + 10;
        dragspan.style.width = oDragHandle.style.width;
        dragspan.style.height = oDragHandle.style.height;
        dragspan.style.top = y + "px";
        dragspan.style.left = x + "px";
        dragspan.innerHTML = oDragHandle.innerHTML;
        document.body.appendChild(dragspan);
        document.onmousemove = moveMouse;
        if(oDragHandle.getAttribute("outype") == '0')
        {
            inserttext = oDragHandle.getAttribute("code");
        }
        else
        {
            inserttext = oDragHandle.innerHTML;
        }
        labeltype = oDragHandle.getAttribute("outype");
        
        return false;
    }
}

function moveMouse(e) {
    if (isdrag) {
        dragspan.style.top  =  (nn6 ? e.clientY + 5 : event.clientY + 5) + "px";
        dragspan.style.left  =  (nn6 ? e.clientX + 10 : event.clientX + 10) + "px";
        return false;
    }
}

function cit()
{
   var target = $get('<% =TxtTemplate.ClientID %>');
   var pre = target.value.substr(0, start);
   var post = target.value.substr(end);
   if(labeltype == '1')
    {
        var link= "Template_addlabel.aspx?a=a&n=" + escape(inserttext);
        if(window.showModalDialog != null)
        {
            var ret = showModalDialog(link,'','dialogWidth:500px; dialogHeight:350px; help: no; scroll: no; status: no; edge: sunken;');
            if (ret != null)
            {
                if (ret.replace(/^\s+|\s+$/g,"") == "")
                {
                    alert("不能输入空值");
                }
                else
                {
                    target.value = pre + ret + post;
                }
            }
        }
        else
        {
            window.open(link,window,'modal=yes,width=500,height=350,menubar=no,toolbar=no,location=no,resizable=no,status=no,scrollbars=no');
        }
    }
    else if(labeltype == '2')
    {
        var fstr = "";
        var tmbody = $get("<% =TxtTemplate.ClientID %>").value;
        tmbody = tmbody.replace(/\n/g,"");
        var regExp = /({PE\.Label|{PE\.DataSource)([\s\S](?!{))*?\/}/g;
        var arr;
        while((arr = regExp.exec(tmbody)) != null)
        {
            if(arr[0].indexOf('page=\"true\"',3,true) > 0)
            {
                var myregexp = /id=\"(.*?)\"/;
                var match = myregexp.exec(arr[0]);
                if (match != null) {
                    fstr += match[1] + "|||";
                }                
            }
        }
        var link= "Template_addPage.aspx?n=" + escape(inserttext) + "&b=" + escape(fstr);
        if(window.showModalDialog != null)
        {
            var ret = showModalDialog(link,'','dialogWidth:250px; dialogHeight:250px; help: no; scroll: no; status: no; edge: sunken;');
            if (ret != null)
            {
                if (ret.replace(/^\s+|\s+$/g,"") == "")
                {
                    alert("不能输入空值");
                }
                else
                {
                    target.value = pre + ret + post;
                }
            }
        }
        else
        {
            window.open(link,window,'modal=yes,width=250,height=250,menubar=no,toolbar=no,location=no,resizable=no,status=no,scrollbars=no');
        }
    }
    else
    {
        target.value = pre + "{PE.SiteConfig." + inserttext + "/}" + post;
    }
    rebulideuselist();
}

function dragend(textBox)
{   
    if(isdrag)
    {
        if(typeof(textBox.selectionStart) == "number")
        {
            start = textBox.selectionStart;
            end = textBox.selectionEnd;
        }
        else if(document.selection)
        {
            var range = document.selection.createRange();
            var range_all = document.body.createTextRange();
            range_all.moveToElementText(textBox);
            for (start=0; range_all.compareEndPoints("StartToStart", range) < 0; start++)
            {
                range_all.moveStart('character', 1);
            }
            for (var i = 0; i <= start; i ++)
            {
                if (textBox.value.charAt(i) == '\n')
                {
                    start++;
                }
            }
            var range_all = document.body.createTextRange();
            range_all.moveToElementText(textBox);
            for (end = 0; range_all.compareEndPoints('StartToEnd', range) < 0; end ++)
            {
                range_all.moveStart('character', 1);
            }

            for (var i = 0; i <= end; i ++)
            {
                if (textBox.value.charAt(i) == '\n')
                end ++;
            }
        }
        cit();
    }
}

function savePos(textBox) 
{
    if(typeof(textBox.selectionStart) == "number"){
        start = textBox.selectionStart;
        end = textBox.selectionEnd;
    }
    else if(document.selection){
        var range = document.selection.createRange();
        if(range.parentElement().id == textBox.id)
        {
            var range_all = document.body.createTextRange();
            range_all.moveToElementText(textBox);
            for (start=0; range_all.compareEndPoints("StartToStart", range) < 0; start++)
            {
                range_all.moveStart('character', 1);
            }
            for (var i = 0; i <= start; i ++)
            {
                if (textBox.value.charAt(i) == '\n')
                {
                    start++;
                }
            }
            var range_all = document.body.createTextRange();
            range_all.moveToElementText(textBox);
            for (end = 0; range_all.compareEndPoints('StartToEnd', range) < 0; end ++)
            {
                range_all.moveStart('character', 1);
            }

            for (var i = 0; i <= end; i ++)
            {
                if (textBox.value.charAt(i) == '\n')
                end ++;
            }
        }
    }
}

function DragPos(textBox) 
{
    if(isdrag)
    {
        if(nn6)
        {
            textBox.focus();
        }
        else
        {
            var rng = textBox.createTextRange(); 
            rng.moveToPoint(event.x,event.y); 
            rng.select(); 
        }
    }
}

document.onmousedown = initDrag;

document.onmouseup = function() {
    isdrag=false;
    if(dragspan)
    {
        try{
        document.body.removeChild(dragspan);
        }
        catch(err){}
    }
}

function rebulideuselist()
{
    $get("thispagelabel").innerHTML = "";
    var tmbody = $get("<% =TxtTemplate.ClientID %>").value;
    tmbody = tmbody.replace(/\n/g,"");
    var regExp = /{pe\.label(([\s\S](?!{pe\.label))*?)\/}/gi;
    var arr
    while((arr = regExp.exec(tmbody)) != null)
    {
        var labelspan = "<div class='spanfixdiv' onmousedown='dit(3);'onclick ='selectlabel(" + arr.index + "," + arr.lastIndex + ");' oncontextmenu='changelabel(" + arr.index + "," + arr.lastIndex + ");return false'>" + arr[1] + "</div>"; 
        $get("thispagelabel").innerHTML += labelspan;
    }
}
        
function selectlabel(begin,end)
{
    begin = parseInt(begin);
    end = parseInt(end);
    if(begin != end)
    {
        var rng = $get("<% =TxtTemplate.ClientID %>").createTextRange();
        rng.moveEnd("character",-$get("<% =TxtTemplate.ClientID %>").value.length);
        rng.moveStart("character",-$get("<% =TxtTemplate.ClientID %>").value.length);
        rng.collapse(true); 
        rng.moveEnd("character",end);
        rng.moveStart("character",begin); 
        rng.select();
    }
}

function changelabel(begin,end)
{
    begin = parseInt(begin);
    end = parseInt(end);
    if(begin != end)
    {           
        var rng = $get("<% =TxtTemplate.ClientID %>").createTextRange();
        rng.moveEnd("character",-$get("<% =TxtTemplate.ClientID %>").value.length);
        rng.moveStart("character",-$get("<% =TxtTemplate.ClientID %>").value.length);
        rng.collapse(true); 
        rng.moveEnd("character",end);
        rng.moveStart("character",begin); 
        rng.select();
        
        var getlabel = rng.text;
        var link= "Template_addlabel.aspx?a=m&n=" + escape(getlabel);
        var ret = showModalDialog(link,'','dialogWidth:500px; dialogHeight:350px; help: no; scroll: no; status: no; edge: sunken;');
        if (ret != null)
        {
            rng.text = ret;
            rebulideuselist();
        }
    }
}

function switchSysBar(){
    var obj = $get("switchPoint");

    if (obj.alt == "关闭标签"){
        obj.alt = "打开标签";
        obj.src = "../../Admin/Images/butd.gif";
        $get("frmTitle").style.display="none";
    }
    else
    {
        obj.alt = "关闭标签";
        obj.src = "../../Admin/Images/butc.gif";
        $get("frmTitle").style.display="";
    }
}

/* AJAX获取目标页面源码  */
function getHTTPObject(){
	var oXmlHttp = false;
	if(window.XMLHttpRequest) {
		oXmlHttp = new XMLHttpRequest();
		if(oXmlHttp.overrideMimeType) {
			oXmlHttp.overrideMimeType('text/xml');
		}
	} else if(window.ActiveXObject) {
		var xmlobjectarry = ["Microsoft.XMLHTTP","MSXML.XMLHTTP","Msxml2.XMLHTTP.7.0","Msxml2.XMLHTTP.6.0","Msxml2.XMLHTTP.5.0","Msxml2.XMLHTTP.4.0","MSXML2.XMLHTTP.3.0","MSXML2.XMLHTTP"];
		for(var i=0; i<xmlobjectarry.length; i++) {
			try {
				oXmlHttp = new ActiveXObject(xmlobjectarry[i]);
				if(oXmlHttp) {
					return oXmlHttpt;
			}
			} catch(oError) {}
		}
	}
	return oXmlHttp;
}

function loadhtml()
{
    var ret = prompt("请输入导入地址","http://");
    if (ret != null)
    {
        if (ret.replace(/^\s+|\s+$/g,"") == "")
        {
            alert("不能输入空值");
        }
        else if (ret == "http://")
        {
            alert("请输入完整的网址");
        }
        else
        {
            var alertspan;
            alertspan = document.createElement('div');
            alertspan.style.position = "absolute";
            alertspan.className = "alertspandiv";

            var e = $get("<% =TxtTemplate.ClientID %>");
            alertspan.style.width = 100;
            alertspan.style.height = 30;
            var t=e.offsetTop;
            var l=e.offsetLeft;
            while(e=e.offsetParent){
                t+=e.offsetTop;
                l+=e.offsetLeft;
            }
            alertspan.style.left = l;
            alertspan.style.top = t;
            alertspan.innerHTML = "读取中...";
            document.body.appendChild(alertspan);

            var userhttp = getHTTPObject();
            try
            {
                userhttp.open("get",ret,true);
                userhttp.onreadystatechange = function () 
                {
                    if (userhttp.readyState == 4 && userhttp.status==200)
                    {
                       document.body.removeChild(alertspan);
                       if(userhttp.responseText.indexOf('',0,true) > 0)
                       {
                           $get("<% =TxtTemplate.ClientID %>").value = bs2str(userhttp.responseBody);
                       }
                       else
                       {
                           $get("<% =TxtTemplate.ClientID %>").value = userhttp.responseText;
                       }	
                    }
                }
                userhttp.send();
            }
            catch(e)
            {
                 document.body.removeChild(alertspan);
                 alert("访问失败，请检查浏览器设置");
            }
        }
    }
}

function bs2str(str)
{
	if(!bs2str.ss) bs2str.ss = [];
	return unescape(escape(jsMidb(str,1)).replace(/..(..)(..)/g, "%$2%$1").replace(/%(\D.)%(..)/g,function(a,a1,a2){var s=a1+a2;if(!bs2str.ss[s]) return bs2str.ss[s]=jsChr("&h"+s);return bs2str.ss[s];}));
}

/* 重载indexof方法，不区分大小写  */
String.prototype._indexOf = String.prototype.indexOf;
String.prototype.indexOf = function()
{
    if(typeof(arguments[arguments.length - 1]) != 'boolean')
    {
        return this._indexOf.apply(this,arguments);
    }
    else
    {
        var bi = arguments[arguments.length - 1];
        var thisObj = this;
        var idx = 0;
        if(typeof(arguments[arguments.length - 2]) == 'number')
        {
            idx = arguments[arguments.length - 2];
            thisObj = this.substr(idx);
        }
  
        var re = new RegExp(arguments[0],bi?'i':'');
        var r = thisObj.match(re);
        return r==null?-1:r.index + idx;
    }
}

function sizeChange(size){
    var obj=$get("<% = TxtTemplate.ClientID %>");
    var height = parseInt(obj.offsetHeight);
    if (height+size>=100){
        obj.style.height=height+size+'px';
    }
}

/* 编辑器用，暂时保留  */
var editorTranslate;
var editortype = 1;

function FCKeditor_OnComplete( editorInstance )
{
    editorTranslate = editorInstance;
    ExecuteCommand('Source'); //默认代码模式
}

function ExecuteCommand(commandName)
{
    var oEditor = FCKeditorAPI.GetInstance('TxtTemplate') ;
    var editorTranslateName = document.getElementById( 'editorTranslateName' );
    oEditor.Commands.GetCommand( commandName ).Execute() ;
	
    if(editortype == 0){
        editortype = 1;
        editorTranslateName.value = "代码模式";
        editorTranslate.ToolbarSet.Expand();
    }else{
        editortype = 0;
        editorTranslateName.value = "编辑模式";
        editorTranslate.ToolbarSet.Collapse();
    }
}

-->
    </script>

    <script language="VBScript" type="text/vbscript"> 
<!--
Function jsMidB(str, p)
	jsMidB = MidB(str, p)
	if lenb(jsMidB) mod 2 <> 0 then jsMidB=jsMidB & chrb(0)
End Function
Function jsChr(ascii)
	jsChr = Chr(ascii)
End Function
-->
    </script>

</asp:Content>
