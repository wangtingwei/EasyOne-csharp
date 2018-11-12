<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    ValidateRequest="false" Inherits="EasyOne.WebSite.Admin.Template.LabelTemplate"
    Title="标签内容编辑" Codebehind="LabelTemplate.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <style type="text/css">
    <!-- 
    .dragspandiv{
    	background-color: #FFFBF5;
    	FILTER: alpha(opacity=80);
        border: 1px solid #F6B9D6;
        text-align: center;
        overflow:hidden;
        padding:2px;
        height:20px;
    }
    .dragspandiv_alt{
    	background-color: #CCFFCC;
    	FILTER: alpha(opacity=80);
        border: 1px solid #00FF00;
        text-align: center;
        overflow:hidden;
        padding:2px;
        height:20px;
    }
    .dragspandiv_ctrl{
    	background-color: #CCCCFF;
    	FILTER: alpha(opacity=80);
        border: 1px solid #0000FF;
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
        width:150px;
        margin: 4px;
    }
    
    #fixdiv {margin: 7px;}
    
    .nodediv 
    {
        background-color: #FFFBF5;
        border: 1px solid #F6B9D6;
        text-align: center;
        overflow:hidden;
        cursor: hand;
        padding:2px;
        height:20px;
    }
    .havechilediv 
    {
        background-color: #FFCCCC;
        border: 1px solid #FF2222;
        text-align: center;
        overflow:hidden;
        cursor: hand;
        padding:2px;
        height:20px;
        font-weight:bolder
    }
    .attribdiv 
    {
        background-color: #F5FFF5;
        border: 1px solid #B9F6D6;
        text-align: center;
        overflow:hidden;
        cursor: hand;
        padding:2px;
        height:20px
    }
    .finaltxt
    {
        border: 1px solid #F6B9D6;
    }
    -->
</style>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" />

    <script id="pejs" type="text/javascript">
<!--
/* 拖放插入控制 */
var start=0, end=0;
var x,y;
var presskey = '';
var dragspan;
var inserttext;
var nn6=document.getElementById&&!document.all;
var isdrag=false;

function initDrag(e) {
    var oDragHandle = nn6 ? e.target : event.srcElement;
    if (oDragHandle.className=="spanfixdiv" || oDragHandle.className=="havechilediv" || oDragHandle.className=="attribdiv" || oDragHandle.className=="nodediv")
    {
        isdrag = true;
        dragspan = document.createElement('div');
        dragspan.style.position = "absolute";
        switch (presskey)
        {
        case 'ctrl':
            dragspan.className = "dragspandiv_ctrl";
            break;
        case 'alt':
            dragspan.className = "dragspandiv_alt";
            break;
        default:
            dragspan.className = "dragspandiv";
            break;
        }   
        y = nn6 ? e.clientY + 5 : event.clientY + 5;
        x = nn6 ? e.clientX + 10 : event.clientX + 10;
        dragspan.style.width = oDragHandle.style.width;
        dragspan.style.height = oDragHandle.style.height;
        dragspan.style.top = y + "px";
        dragspan.style.left = x + "px";
        dragspan.innerHTML = oDragHandle.innerHTML;
        document.body.appendChild(dragspan);
        document.onmousemove = moveMouse;
        
        txtproce(oDragHandle);
        return false;
    }
}

function moveMouse(e) {
    if (isdrag) {
        dragspan.style.top = (nn6 ? e.clientY : event.clientY) + document.documentElement.scrollTop + 5 + "px";
        dragspan.style.left = (nn6 ? e.clientX : event.clientX) + document.documentElement.scrollLeft + 10 + "px";
        return false;
    }
}

function dragend(textBox)
{   
    if(isdrag)
    {
        savePos(textBox);
        cit();
    }
}

function savePos(textBox) 
{
    if(typeof(textBox.selectionStart) == "number"){
        start = textBox.selectionStart;
        end = textBox.selectionEnd;
    }
}

function cit()
{
    var target = $get('<% =TxtTemplate.ClientID %>');
    if(nn6)
    {
        var pre = target.value.substr(0, start);
        var post = target.value.substr(end);
        target.value = pre + inserttext + post;
    }
    else
    {
        target.focus();
        var range = document.selection.createRange();
        range.text = inserttext;
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

function txtproce(selectobj)
{
    switch (selectobj.getAttribute("outype"))
    {
     case '0':
        switch (presskey)
        {
        case 'ctrl':
            inserttext = "<xsl:apply-templates select=\"" + selectobj.getAttribute("path") + "/" + selectobj.getAttribute("tn") + "\"/>";
            break;
        case 'alt':
            inserttext = "<xsl:template match=\"" + selectobj.getAttribute("path")  + "/" + selectobj.getAttribute("tn") + "\">\n</xsl:template>";
            break;
        default:
            inserttext = "<xsl:for-each select=\"" + selectobj.getAttribute("path")  + "/" + selectobj.getAttribute("tn") + "\">\n</xsl:for-each>";
            break;
        }
        break;
    case '1':
        switch (presskey)
        {
        case 'ctrl':
            inserttext = "<xsl:value-of select=\"" + selectobj.getAttribute("path")  + "/" + selectobj.getAttribute("tn") + "\"/>";
            break;
        case 'alt':
            inserttext = "{" + selectobj.getAttribute("path") + "/" + selectobj.getAttribute("tn") + "}";
            break;
        default:
            inserttext = "<xsl:value-of select=\"" + selectobj.getAttribute("tn") + "\"/>";
            break;
        }
        break;
    case '2':
        switch (presskey)
        {
        case 'ctrl':
            inserttext = "<xsl:value-of select=\"" + selectobj.getAttribute("path")  + "$" + selectobj.getAttribute("tn") + "\"/>";
            break;
        case 'alt':
            inserttext = "{" + selectobj.getAttribute("path")  + "/$" + selectobj.getAttribute("tn") + "}";
            break;
        default:
            inserttext = "<xsl:value-of select=\"$" + selectobj.getAttribute("tn") + "\"/>";
            break;
        }
        break;
    case '3':
        switch (presskey)
        {
        case 'ctrl':
            inserttext = "<xsl:param name=\"" + selectobj.innerText + "\" />";
            break;
        case 'alt':
            inserttext = "$" + selectobj.innerText;
            break;
        default:
            inserttext = "<xsl:value-of select=\"$" + selectobj.innerText + "\" />";
            break;
        }
        break;
    case '4':
        inserttext = selectobj.getAttribute("code").replace(/&qout;/g, "\"");
        break;
    case '5':
        inserttext = "pe:" + selectobj.getAttribute("code").replace(/&qout;/g, "'");
        break;
    }
}

document.onmousedown = initDrag;

document.onmouseup = function() {
    isdrag=false;
    if(dragspan != null)
    {
        document.body.removeChild(dragspan);
        dragspan = null;
    }
}

document.onkeyup = function() { 
    presskey = '';
} 

document.onkeydown = function(e) {
    var isie = (document.all) ? true : false;
    var key;
    if (isie)
        key = window.event.keyCode;
    else
        key = e.which;

    if(key == 18)
    {
        presskey = 'alt';
    }
    else if(key == 17)
    {
        presskey = 'ctrl';
    }
    else
    {
        presskey = '';
    }
}

/* 导出XML数据 */
function openxmlshow()
{
    window.open('LabelXmlShow.aspx?name=' + encodeURI('<%= m_LabelName %>'), '_blank');
}

/* 页面元素控制 */
var fpath = '<% =StyleSheetPath %>';
function switchSysBar(objname, tarname, dir){
    var obj = $get(objname);
    if (obj.alt == "关闭"){
        obj.alt = "打开";
        if(dir == "right")
            obj.src = fpath + "Images/butClose.gif";
        else
            obj.src = fpath + "Images/butOpen.gif";
        $get(tarname).style.display="none";
    }
    else
    {
        obj.alt = "关闭";
        if(dir == "right")
            obj.src = fpath + "Images/butOpen.gif";
        else
            obj.src = fpath + "Images/butClose.gif";
        $get(tarname).style.display="";
    }
}

function sizeChange(size){

    var obj=document.getElementById("<% = TxtTemplate.ClientID %>");
    var height = parseInt(obj.offsetHeight);
    if (height+size>=100)
    {
        obj.style.height=height+size + 'px';
    }
}

function sizeshowmode(){
    var obj=$get("<% = LabShow.ClientID %>");
    var converter = document.createElement("DIV");
    
    if($get("tohtml").value != "源码")
    {
        $get("tohtml").value = "源码";
        converter.innerHTML = obj.innerHTML;
        obj.innerHTML = converter.innerText;
    }
    else
    {
        $get("tohtml").value = "内容";
        converter.innerText = obj.innerHTML;
        obj.innerHTML = converter.innerHTML;
    }
    converter = null;
}
-->
    </script>

    <table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="0" class="border">
        <tr>
            <td class="spacingtitle" style="height: 22px;" colspan="5" align="center">
                <b>标签内容编辑</b></td>
        </tr>
        <tr class="tdbg">
            <td style="text-align: center; vertical-align: top; width: 220px" id="ft">
                <ajaxToolkit:TabContainer runat="server" ID="TabContainer1" Height="400px">
                    <ajaxToolkit:TabPanel runat="Server" ID="TabPanel" HeaderText="标准代码">
                        <ContentTemplate>
                            <div style="overflow: auto; float: left; width: 100%; height: 390px; text-align: left">
                                <div code="xmlns:ms=&qout;urn:schemas-microsoft-com:xslt&qout;" onclick="cit()" outype="4"
                                    class="spanfixdiv">
                                    MS命名空间</div>
                                <div code="xmlns:js=&qout;urn:the-xml-files:xslt-js&qout;" onclick="cit()" outype="4"
                                    class="spanfixdiv">
                                    JS代码命名空间</div>
                                <div code="xmlns:vb=&qout;urn:the-xml-files:xslt-vb&qout;" onclick="cit()" outype="4"
                                    class="spanfixdiv">
                                    VB代码命名空间</div>
                                <div code="xmlns:csharp=&qout;urn:the-xml-files:xslt-csharp&qout;" onclick="cit()"
                                    outype="4" class="spanfixdiv">
                                    C#命名空间</div>
                                <div code="xmlns:pe=&qout;labelproc&qout;" onclick="cit()" outype="4" class="spanfixdiv">
                                    扩展函数命名空间</div>
                                <div code="disable-output-escaping=&qout;yes&qout;" onclick="cit()" outype="4" class="spanfixdiv">
                                    HTML编码输出</div>
                                <div code="<xsl:value-of select=&qout;&qout;/>" onclick="cit()" outype="4" class="spanfixdiv"
                                    title="变量调用标签">
                                    value-of</div>
                                <div code="<xsl:for-each select=&qout;&qout;></xsl:for-each>" onclick="cit()" outype="4"
                                    class="spanfixdiv" title="变量循环">
                                    for-each</div>
                                <div code="<xsl:param name=&qout;&qout;/>" onclick="cit()" outype="4" class="spanfixdiv"
                                    title="变量引入">
                                    Param</div>
                                <div code="<xsl:with-param name=&qout;&qout;/></xsl:with-param>" onclick="cit()"
                                    outype="4" class="spanfixdiv" title="变量传递">
                                    With-Param</div>
                                <div code="<xsl:variable name=&qout;&qout;/>" onclick="cit()" outype="4" class="spanfixdiv"
                                    title="变量定义">
                                    Variable</div>
                                <div code="<xsl:template match=&qout;&qout;></xsl:template>" onclick="cit()" outype="4"
                                    class="spanfixdiv" title="循环模板定义">
                                    Templates</div>
                                <div code="<xsl:apply-templates select=&qout;&qout;/>" onclick="cit()" outype="4"
                                    class="spanfixdiv" title="循环引用标签">
                                    Apply-templates</div>
                                <div code="<a> <xsl:attribute name=&qout;href&qout;></xsl:attribute> <xsl:attribute name=&qout;title&qout;></xsl:attribute></a>"
                                    onclick="cit()" outype="4" class="spanfixdiv" title="超连接元素">
                                    A</div>
                                <div code="<xsl:element name=&qout;img&qout;><xsl:attribute name=&qout;src&qout;></xsl:attribute><xsl:attribute name=&qout;border&qout;>0</xsl:attribute></xsl:element>"
                                    onclick="cit()" outype="4" class="spanfixdiv" title="图象元素">
                                    Img</div>
                                <div code="<xsl:text disable-output-escaping=&qout;yes&qout;></xsl:text>" onclick="cit()"
                                    outype="4" class="spanfixdiv" title="格式化字符输出">
                                    Text</div>
                                <div code="<xsl:if test=&qout;&qout;></xsl:if>" onclick="cit()" outype="4" class="spanfixdiv"
                                    title="条件标签">
                                    If</div>
                                <div code="<xsl:choose><xsl:when test=&qout;&qout;></xsl:when><xsl:otherwise></xsl:otherwise></xsl:choose>"
                                    onclick="cit()" outype="4" class="spanfixdiv" title="条件循环">
                                    When</div>
                            </div>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel runat="Server" ID="TabPanel1" HeaderText="扩展函数">
                        <ContentTemplate>
                            <div style="overflow: auto; float: left; width: 100%; height: 390px; text-align: left">
                                <div code="GetInfoPath(信息ID)" onclick="cit()" outype="5" class="spanfixdiv">
                                    取得信息路径</div>
                                <div code="GetNodePath(是否带参数,栏目ID)" onclick="cit()" outype="5" class="spanfixdiv">
                                    取得栏目路径</div>
                                <div code="GetSpecialPath(&qout;专题ID&qout;)" onclick="cit()" outype="5" class="spanfixdiv">
                                    取得专题路径</div>
                                <div code="GetSpecialCategoryPath(&qout;专题类别ID&qout;)" onclick="cit()" outype="5"
                                    class="spanfixdiv">
                                    取得专题类别路径</div>
                                <div code="GetNode(&qout;栏目ID&qout;,&qout;name|dir|aspxname&qout;)" onclick="cit()"
                                    outype="5" class="spanfixdiv">
                                    取得栏目对象</div>
                                <div code="GetSpecial(&qout;专题ID&qout;,&qout;name|dir|categoryid&qout;)" onclick="cit()"
                                    outype="5" class="spanfixdiv">
                                    取得专题对象</div>
                                <div code="ReplaceText(&qout;字符&qout;,&qout;替换目标&qout;,&qout;替换内容&qout;)" onclick="cit()"
                                    outype="5" class="spanfixdiv">
                                    替换字符</div>
                                <div code="FiltText(&qout;字符&qout;)" onclick="cit()" outype="5" class="spanfixdiv">
                                    站内过滤字符</div>
                                <div code="FiltInsideLink(&qout;字符&qout;)" onclick="cit()" outype="5" class="spanfixdiv">
                                    替换站内链接</div>
                                <div code="ShowHeightLineText(&qout;字符&qout;,&qout;关键字&qout;,&qout;#ff0000&qout;)"
                                    onclick="cit()" outype="5" class="spanfixdiv">
                                    高亮显示字符串中的关键字</div>
                                <div code="TimeNow()" onclick="cit()" outype="5" class="spanfixdiv">
                                    现在时间</div>
                                <div code="ConverToWeek(&qout;1999/01/01&qout;)" onclick="cit()" outype="5" class="spanfixdiv">
                                    转换到星期</div>
                                <div code="FormatDate(&qout;1999/01/01&qout;,&qout;YYYY-MM-DD&qout;)" onclick="cit()"
                                    outype="5" class="spanfixdiv">
                                    转换日期格式</div>
                                <div code="TimeSpan(&qout;1999/01/01&qout;,pe:TimeNow())" onclick="cit()" outype="5"
                                    class="spanfixdiv">
                                    比较相差天数</div>
                                <div code="Convert2Char(&qout;数字&qout;,&qout;符号&qout;)" onclick="cit()" outype="5"
                                    class="spanfixdiv">
                                    显示指定数量的符号</div>
                                <div code="Convert2Int(&qout;数字&qout;)" onclick="cit()" outype="5" class="spanfixdiv">
                                    转换到整数</div>
                                <div code="Int2Chinese(&qout;数字&qout;)" onclick="cit()" outype="5" class="spanfixdiv">
                                    转换数字到中文</div>
                                <div code="Int2CMoney(&qout;数字&qout;)" onclick="cit()" outype="5" class="spanfixdiv">
                                    转换数字到中文金额</div>
                                <div code="Convert2JS(&qout;字符&qout;)" onclick="cit()" outype="5" class="spanfixdiv">
                                    转换到JS格式</div>
                                <div code="CutText(&qout;字符&qout;,&qout;长度&qout;,&qout;后缀符号&qout;)" onclick="cit()"
                                    outype="5" class="spanfixdiv">
                                    取固定长度文字</div>
                                <div code="RemoveHtml(&qout;字符&qout;)" onclick="cit()" outype="5" class="spanfixdiv">
                                    移除HTML标记</div>
                                <div code="EncodeText(&qout;字符&qout;,&qout;md5_16&qout;)" onclick="cit()" outype="5"
                                    class="spanfixdiv">
                                    编码为16位md5</div>
                                <div code="EncodeText(&qout;字符&qout;,&qout;md5_32&qout;)" onclick="cit()" outype="5"
                                    class="spanfixdiv">
                                    编码为32位md5</div>
                                <div code="EncodeText(&qout;字符&qout;,&qout;enbase64&qout;)" onclick="cit()" outype="5"
                                    class="spanfixdiv">
                                    编码为base64</div>
                                <div code="EncodeText(&qout;base64代码&qout;,&qout;debase64&qout;)" onclick="cit()"
                                    outype="5" class="spanfixdiv">
                                    从base64解码</div>
                                <div code="EncodeText(&qout;html代码&qout;,&qout;htmlencode&qout;)" onclick="cit()"
                                    outype="5" class="spanfixdiv">
                                    HTML代码编码</div>
                                <div code="EncodeText(&qout;字符&qout;,&qout;htmldecode&qout;)" onclick="cit()"
                                    outype="5" class="spanfixdiv">
                                    HTML代码解码</div>
                                <div code="ReadTxtFile(&qout;文本路径&qout;)" onclick="cit()" outype="5" class="spanfixdiv">
                                    读取文本文件</div>
                                <div code="Txt2Img(&qout;文本内容&qout;,&qout;隶书;100;1;true&qout;,&qout;40;460;0;0&qout;,&qout;200;20;20;200&qout;,&qout;230;230;230&qout;,&qout;true&qout;,&qout;输出文件名&qout;,2)"
                                    onclick="cit()" outype="5" class="spanfixdiv">
                                    文字转图片</div>
                                <div code="SiteName()" onclick="cit()" outype="5" class="spanfixdiv">
                                    网站名称</div>
                                <div code="SiteTitle()" onclick="cit()" outype="5" class="spanfixdiv">
                                    页面标题</div>
                                <div code="InstallDir()" onclick="cit()" outype="5" class="spanfixdiv">
                                    站点根目录</div>
                                <div code="SitePath()" onclick="cit()" outype="5" class="spanfixdiv">
                                    网站URL</div>
                                <div code="Logo()" onclick="cit()" outype="5" class="spanfixdiv">
                                    网站LOGO</div>
                                <div code="Banner()" onclick="cit()" outype="5" class="spanfixdiv">
                                    网站BANNER</div>
                                <div code="Webmaster()" onclick="cit()" outype="5" class="spanfixdiv">
                                    站长名称</div>
                                <div code="WebmasterEmail()" onclick="cit()" outype="5" class="spanfixdiv">
                                    站长邮箱</div>
                                <div code="Copyright()" onclick="cit()" outype="5" class="spanfixdiv">
                                    版权申明</div>
                                <div code="ManageDir()" onclick="cit()" outype="5" class="spanfixdiv">
                                    管理目录</div>
                                <div code="ADdir()" onclick="cit()" outype="5" class="spanfixdiv">
                                    广告目录</div>
                                <div code="MetaKeywords()" onclick="cit()" outype="5" class="spanfixdiv">
                                    页面关键字</div>
                                <div code="MetaDescription()" onclick="cit()" outype="5" class="spanfixdiv">
                                    页面摘要</div>
                                <div code="UpLoadDir()" onclick="cit()" outype="5" class="spanfixdiv">
                                    上传目录</div>
                                <div code="HitsOfHot()" onclick="cit()" outype="5" class="spanfixdiv">
                                    热点下限</div>
                                <div code="CreateHtmlPath()" onclick="cit()" outype="5" class="spanfixdiv">
                                    生成HTML路径</div>
                                <div code="GetNodeEnableProtect(栏目ID)" onclick="cit()" outype="5" class="spanfixdiv">
                                    节点防止复制、盗链值</div>
                                <div code="GetNodeEnableComment(栏目ID)" onclick="cit()" outype="5" class="spanfixdiv">
                                    允许节点发表评论值</div>
                                <div code="GetNodeCommentNeedCheck(栏目ID)" onclick="cit()" outype="5" class="spanfixdiv">
                                    评论是否需要审核值</div>
                                <div code="EnableComment(栏目ID)" onclick="cit()" outype="5" class="spanfixdiv">
                                    是否允许在此节点发表评论</div>
                                <div code="EnableTouristsComment(栏目ID)" onclick="cit()" outype="5" class="spanfixdiv">
                                    是否允许游客在此节点发表评论</div>
                                <div code="IsLogined()" onclick="cit()" outype="5" class="spanfixdiv">
                                    当前用户是否已登录</div>
                                <div code="IsAdminLogined()" onclick="cit()" outype="5" class="spanfixdiv">
                                    管理员是否登录</div>
                                <div code="LoginedUserName()" onclick="cit()" outype="5" class="spanfixdiv">
                                    取得当前登录用户的名称</div>
                                <div code="LoginedUserEmail()" onclick="cit()" outype="5" class="spanfixdiv">
                                    取得当前登录用户的E_mail</div>
                                <div code="GetFieldList(&qout;模板ID&qout;,&qout;栏目ID&qout;,&qout;表格样式&qout;,&qout;属性键样式&qout;,&qout;属性值样式&qout;)"
                                    onclick="cit()" outype="5" class="spanfixdiv">
                                    获取物品的属性
                                </div>
                                <div code="GetModelItemName(模型ID)" onclick="cit()" outype="5" class="spanfixdiv">
                                    取得模型的项目名</div>
                                <div code="GetModelName(模型对应的表名)" onclick="cit()" outype="5" class="spanfixdiv">
                                    取得模型名</div>
                                <div code="IsShop(模型对应的表名)" onclick="cit()" outype="5" class="spanfixdiv">
                                    是否商品模型</div>
                                <div code="GetNodeFieldName(节点ID, &qout;节点字段名&qout;)" onclick="cit()" outype="5"
                                    class="spanfixdiv">
                                    取得节点字段值</div>
                                <div code="IsStartWithhttp(url地址)" onclick="cit()" outype="5" class="spanfixdiv">
                                    是否绝对地址</div>
                                <div code="ConvertSizeToShow(文件大小)" onclick="cit()" outype="5" class="spanfixdiv">
                                    根据文件大小显示单位</div>
                            </div>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel runat="Server" ID="TabPanel2" HeaderText="属性">
                        <ContentTemplate>
                            <div style="overflow: auto; float: left; width: 100%; height: 390px; text-align: left">
                                <pe:ExtendedLabel HtmlEncode="false" ID="attlist" runat="server"></pe:ExtendedLabel>
                            </div>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                </ajaxToolkit:TabContainer>
            </td>
            <td onclick="switchSysBar('sp','ft')" style="width: 12px; cursor: pointer">
                <img id="sp" src="<% =StyleSheetPath %>images/butClose.gif" alt="关闭" style="border: 0px;
                    width: 12px;" />
            </td>
            <td style="text-align: left; vertical-align: top;">
                <asp:TextBox ID="TxtTemplate" runat="server" Height="430px" Width="99%" TextMode="MultiLine"
                    Wrap="False" CssClass="txt_main"></asp:TextBox>
                <div style="text-align: center; vertical-align: top;">
                    <img alt="增加高度" src="../../Admin/Images/sizeplus.gif" onclick="sizeChange(50)" />
                    <img alt="减少高度" src="../../Admin/Images/sizeminus.gif" onclick="sizeChange(-50)" />
                </div>
            </td>
            <td onclick="switchSysBar('sp2','ft2', 'right')" style="width: 12px; cursor: pointer">
                <img id="sp2" src="<% =StyleSheetPath %>images/butOpen.gif" alt="关闭" style="border: 0px;
                    width: 12px;" />
            </td>
            <td style="text-align: left; vertical-align: top; width: 200px;" id="ft2">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div style="text-align: center;">
                            <asp:Button ID="BtnShowSchema" OnClick="BtnShowSchema_Click" runat="server" Text="架构"
                                BackColor="Red"></asp:Button>
                            <asp:Button ID="BtnShowDetal" OnClick="BtnShowDetal_Click" runat="server" Text="全部">
                            </asp:Button></div>
                        <div id="xmltreediv" style="overflow: auto; float: left; width: 100%; height: 400px;
                            text-align: left">
                            <pe:ExtendedLabel HtmlEncode="false" ID="ShowXml" runat="server"></pe:ExtendedLabel>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr class="tdbg">
            <td align="center" colspan="5">
                &nbsp;<asp:Button ID="BtnPrv" OnClick="BtnPrv_Click" runat="server" Text="上一步" Style="cursor: pointer;
                    cursor: hand; width: 88px;"></asp:Button>&nbsp;&nbsp;<asp:Button ID="BtnFinal" OnClick="BtnFinal_Click"
                        runat="server" Text="完　成" Style="cursor: pointer; cursor: hand; width: 88px;"></asp:Button>&nbsp;&nbsp;<input
                            id="BtnCancel" type="button" class="inputbutton" value="取　消" onclick="Redirect('LabelManage.aspx')"
                            style="cursor: pointer; cursor: hand; width: 88px;" />&nbsp; &nbsp;<asp:Button ID="OpenShowTemplate"
                                runat="server" Text="查看效果" />
                &nbsp;&nbsp;<input id="BtnXmlShow" type="button" class="inputbutton" value="导出数据"
                    onclick="openxmlshow();" style="cursor: pointer; cursor: hand; width: 88px;" /></td>
        </tr>
    </table>
    <table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="border">
        <tr>
            <td class="spacingtitle" style="height: 22px;" align="center">
                <b>相关说明</b></td>
        </tr>
        <tr class="tdbg">
            <td align="left">
                <ajaxToolkit:TabContainer runat="server" ID="Tabs" Height="150px">
                    <ajaxToolkit:TabPanel runat="Server" ID="TabPanel3" HeaderText="操作说明">
                        <ContentTemplate>
                            <p>
                                左方列出的是XML节点列表，右方进行模板编辑。</p>
                            <p>
                                请直接拖放节点到右方的模板编辑器中，系统会自动加上调用代码。</p>
                            <p>
                                如需要输入全路径节点，请按住"CTRL"键再进行拖放。</p>
                            <p>
                                如果您擅长使用Macromedia Dreamweaver 8，可以通过“导出数据”功能，将标签数据保存为XML文件，再使用DW8的XSLT编辑功能进行本地编辑。</p>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel runat="Server" ID="TabPanel4" HeaderText="XSLT简介">
                        <ContentTemplate>
                            <p>
                                XSLT，是最近伴随XML兴起的一种模板编辑语言，XSLT通过对XML数据的重新整理，生成符合需要的HTML或其他代码，而这个重新整理的规范，就成为XSLT模板。<br />
                                XSLT本身也是XML结构的文档模式，您在XSLT中定义的模板，也必须符合W3C对XML的各项规定。</p>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel runat="Server" ID="TabPanel5" HeaderText="其他说明">
                        <ContentTemplate>
                            <p>
                                XSLT要求使用的HTML代码必须有封闭符号，比如原来的&lt;br&gt;就必须写为&lt;br /&gt。</p>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                </ajaxToolkit:TabContainer>
            </td>
        </tr>
    </table>
    <asp:Panel ID="Panel1" runat="server" Style="display: none" ScrollBars="Auto" CssClass="modalPopup">
        <asp:Panel ID="Panel3" runat="server" Style="cursor: move; background-color: #CCCCDD;
            border: solid 1px Gray; color: Black; text-align: center">
            <b>标签测试窗口</b>
        </asp:Panel>
        当前分页：<asp:TextBox ID="TxtTempPage" runat="server" Text="1" Width="30px"></asp:TextBox><br />
        测试页名：<asp:TextBox ID="TxtTempPageName" runat="server" Text="test.aspx?page={$pageid/}"
            Width="480px"></asp:TextBox><br />
        测试标签：<asp:TextBox ID="TxtTemplateTest" runat="server" Width="480px"></asp:TextBox>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                &nbsp;<asp:Button ID="BtnShowTemplate" runat="server" OnClick="BtnShowTemplate_Click"
                    Text="刷新" Style="cursor: pointer; cursor: hand; width: 88px;" />
                &nbsp;&nbsp;<input id="tohtml" type="button" value="源码" onclick="sizeshowmode()"
                    style="cursor: pointer; cursor: hand; width: 88px;" />
                <div style="overflow: auto; width: 550px; height: 390px; background-color: #EEEEEE;
                    border: solid 1px Gray; color: Black;">
                    <pe:ExtendedLabel ID="LabShow" HtmlEncode="false" Text="标签结果显示区" runat="server"></pe:ExtendedLabel>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:Button ID="CloseButton" runat="server" Text="关闭" Style="cursor: pointer; cursor: hand" />
    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender" runat="server" TargetControlID="OpenShowTemplate"
        PopupControlID="Panel1" BackgroundCssClass="modalBackground" CancelControlID="CloseButton"
        DropShadow="true" PopupDragHandleControlID="Panel3" />
</asp:Content>
