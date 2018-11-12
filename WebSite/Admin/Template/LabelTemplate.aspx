<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    ValidateRequest="false" Inherits="EasyOne.WebSite.Admin.Template.LabelTemplate"
    Title="��ǩ���ݱ༭" Codebehind="LabelTemplate.aspx.cs" %>

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
/* �ϷŲ������ */
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

/* ����XML���� */
function openxmlshow()
{
    window.open('LabelXmlShow.aspx?name=' + encodeURI('<%= m_LabelName %>'), '_blank');
}

/* ҳ��Ԫ�ؿ��� */
var fpath = '<% =StyleSheetPath %>';
function switchSysBar(objname, tarname, dir){
    var obj = $get(objname);
    if (obj.alt == "�ر�"){
        obj.alt = "��";
        if(dir == "right")
            obj.src = fpath + "Images/butClose.gif";
        else
            obj.src = fpath + "Images/butOpen.gif";
        $get(tarname).style.display="none";
    }
    else
    {
        obj.alt = "�ر�";
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
    
    if($get("tohtml").value != "Դ��")
    {
        $get("tohtml").value = "Դ��";
        converter.innerHTML = obj.innerHTML;
        obj.innerHTML = converter.innerText;
    }
    else
    {
        $get("tohtml").value = "����";
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
                <b>��ǩ���ݱ༭</b></td>
        </tr>
        <tr class="tdbg">
            <td style="text-align: center; vertical-align: top; width: 220px" id="ft">
                <ajaxToolkit:TabContainer runat="server" ID="TabContainer1" Height="400px">
                    <ajaxToolkit:TabPanel runat="Server" ID="TabPanel" HeaderText="��׼����">
                        <ContentTemplate>
                            <div style="overflow: auto; float: left; width: 100%; height: 390px; text-align: left">
                                <div code="xmlns:ms=&qout;urn:schemas-microsoft-com:xslt&qout;" onclick="cit()" outype="4"
                                    class="spanfixdiv">
                                    MS�����ռ�</div>
                                <div code="xmlns:js=&qout;urn:the-xml-files:xslt-js&qout;" onclick="cit()" outype="4"
                                    class="spanfixdiv">
                                    JS���������ռ�</div>
                                <div code="xmlns:vb=&qout;urn:the-xml-files:xslt-vb&qout;" onclick="cit()" outype="4"
                                    class="spanfixdiv">
                                    VB���������ռ�</div>
                                <div code="xmlns:csharp=&qout;urn:the-xml-files:xslt-csharp&qout;" onclick="cit()"
                                    outype="4" class="spanfixdiv">
                                    C#�����ռ�</div>
                                <div code="xmlns:pe=&qout;labelproc&qout;" onclick="cit()" outype="4" class="spanfixdiv">
                                    ��չ���������ռ�</div>
                                <div code="disable-output-escaping=&qout;yes&qout;" onclick="cit()" outype="4" class="spanfixdiv">
                                    HTML�������</div>
                                <div code="<xsl:value-of select=&qout;&qout;/>" onclick="cit()" outype="4" class="spanfixdiv"
                                    title="�������ñ�ǩ">
                                    value-of</div>
                                <div code="<xsl:for-each select=&qout;&qout;></xsl:for-each>" onclick="cit()" outype="4"
                                    class="spanfixdiv" title="����ѭ��">
                                    for-each</div>
                                <div code="<xsl:param name=&qout;&qout;/>" onclick="cit()" outype="4" class="spanfixdiv"
                                    title="��������">
                                    Param</div>
                                <div code="<xsl:with-param name=&qout;&qout;/></xsl:with-param>" onclick="cit()"
                                    outype="4" class="spanfixdiv" title="��������">
                                    With-Param</div>
                                <div code="<xsl:variable name=&qout;&qout;/>" onclick="cit()" outype="4" class="spanfixdiv"
                                    title="��������">
                                    Variable</div>
                                <div code="<xsl:template match=&qout;&qout;></xsl:template>" onclick="cit()" outype="4"
                                    class="spanfixdiv" title="ѭ��ģ�嶨��">
                                    Templates</div>
                                <div code="<xsl:apply-templates select=&qout;&qout;/>" onclick="cit()" outype="4"
                                    class="spanfixdiv" title="ѭ�����ñ�ǩ">
                                    Apply-templates</div>
                                <div code="<a> <xsl:attribute name=&qout;href&qout;></xsl:attribute> <xsl:attribute name=&qout;title&qout;></xsl:attribute></a>"
                                    onclick="cit()" outype="4" class="spanfixdiv" title="������Ԫ��">
                                    A</div>
                                <div code="<xsl:element name=&qout;img&qout;><xsl:attribute name=&qout;src&qout;></xsl:attribute><xsl:attribute name=&qout;border&qout;>0</xsl:attribute></xsl:element>"
                                    onclick="cit()" outype="4" class="spanfixdiv" title="ͼ��Ԫ��">
                                    Img</div>
                                <div code="<xsl:text disable-output-escaping=&qout;yes&qout;></xsl:text>" onclick="cit()"
                                    outype="4" class="spanfixdiv" title="��ʽ���ַ����">
                                    Text</div>
                                <div code="<xsl:if test=&qout;&qout;></xsl:if>" onclick="cit()" outype="4" class="spanfixdiv"
                                    title="������ǩ">
                                    If</div>
                                <div code="<xsl:choose><xsl:when test=&qout;&qout;></xsl:when><xsl:otherwise></xsl:otherwise></xsl:choose>"
                                    onclick="cit()" outype="4" class="spanfixdiv" title="����ѭ��">
                                    When</div>
                            </div>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel runat="Server" ID="TabPanel1" HeaderText="��չ����">
                        <ContentTemplate>
                            <div style="overflow: auto; float: left; width: 100%; height: 390px; text-align: left">
                                <div code="GetInfoPath(��ϢID)" onclick="cit()" outype="5" class="spanfixdiv">
                                    ȡ����Ϣ·��</div>
                                <div code="GetNodePath(�Ƿ������,��ĿID)" onclick="cit()" outype="5" class="spanfixdiv">
                                    ȡ����Ŀ·��</div>
                                <div code="GetSpecialPath(&qout;ר��ID&qout;)" onclick="cit()" outype="5" class="spanfixdiv">
                                    ȡ��ר��·��</div>
                                <div code="GetSpecialCategoryPath(&qout;ר�����ID&qout;)" onclick="cit()" outype="5"
                                    class="spanfixdiv">
                                    ȡ��ר�����·��</div>
                                <div code="GetNode(&qout;��ĿID&qout;,&qout;name|dir|aspxname&qout;)" onclick="cit()"
                                    outype="5" class="spanfixdiv">
                                    ȡ����Ŀ����</div>
                                <div code="GetSpecial(&qout;ר��ID&qout;,&qout;name|dir|categoryid&qout;)" onclick="cit()"
                                    outype="5" class="spanfixdiv">
                                    ȡ��ר�����</div>
                                <div code="ReplaceText(&qout;�ַ�&qout;,&qout;�滻Ŀ��&qout;,&qout;�滻����&qout;)" onclick="cit()"
                                    outype="5" class="spanfixdiv">
                                    �滻�ַ�</div>
                                <div code="FiltText(&qout;�ַ�&qout;)" onclick="cit()" outype="5" class="spanfixdiv">
                                    վ�ڹ����ַ�</div>
                                <div code="FiltInsideLink(&qout;�ַ�&qout;)" onclick="cit()" outype="5" class="spanfixdiv">
                                    �滻վ������</div>
                                <div code="ShowHeightLineText(&qout;�ַ�&qout;,&qout;�ؼ���&qout;,&qout;#ff0000&qout;)"
                                    onclick="cit()" outype="5" class="spanfixdiv">
                                    ������ʾ�ַ����еĹؼ���</div>
                                <div code="TimeNow()" onclick="cit()" outype="5" class="spanfixdiv">
                                    ����ʱ��</div>
                                <div code="ConverToWeek(&qout;1999/01/01&qout;)" onclick="cit()" outype="5" class="spanfixdiv">
                                    ת��������</div>
                                <div code="FormatDate(&qout;1999/01/01&qout;,&qout;YYYY-MM-DD&qout;)" onclick="cit()"
                                    outype="5" class="spanfixdiv">
                                    ת�����ڸ�ʽ</div>
                                <div code="TimeSpan(&qout;1999/01/01&qout;,pe:TimeNow())" onclick="cit()" outype="5"
                                    class="spanfixdiv">
                                    �Ƚ��������</div>
                                <div code="Convert2Char(&qout;����&qout;,&qout;����&qout;)" onclick="cit()" outype="5"
                                    class="spanfixdiv">
                                    ��ʾָ�������ķ���</div>
                                <div code="Convert2Int(&qout;����&qout;)" onclick="cit()" outype="5" class="spanfixdiv">
                                    ת��������</div>
                                <div code="Int2Chinese(&qout;����&qout;)" onclick="cit()" outype="5" class="spanfixdiv">
                                    ת�����ֵ�����</div>
                                <div code="Int2CMoney(&qout;����&qout;)" onclick="cit()" outype="5" class="spanfixdiv">
                                    ת�����ֵ����Ľ��</div>
                                <div code="Convert2JS(&qout;�ַ�&qout;)" onclick="cit()" outype="5" class="spanfixdiv">
                                    ת����JS��ʽ</div>
                                <div code="CutText(&qout;�ַ�&qout;,&qout;����&qout;,&qout;��׺����&qout;)" onclick="cit()"
                                    outype="5" class="spanfixdiv">
                                    ȡ�̶���������</div>
                                <div code="RemoveHtml(&qout;�ַ�&qout;)" onclick="cit()" outype="5" class="spanfixdiv">
                                    �Ƴ�HTML���</div>
                                <div code="EncodeText(&qout;�ַ�&qout;,&qout;md5_16&qout;)" onclick="cit()" outype="5"
                                    class="spanfixdiv">
                                    ����Ϊ16λmd5</div>
                                <div code="EncodeText(&qout;�ַ�&qout;,&qout;md5_32&qout;)" onclick="cit()" outype="5"
                                    class="spanfixdiv">
                                    ����Ϊ32λmd5</div>
                                <div code="EncodeText(&qout;�ַ�&qout;,&qout;enbase64&qout;)" onclick="cit()" outype="5"
                                    class="spanfixdiv">
                                    ����Ϊbase64</div>
                                <div code="EncodeText(&qout;base64����&qout;,&qout;debase64&qout;)" onclick="cit()"
                                    outype="5" class="spanfixdiv">
                                    ��base64����</div>
                                <div code="EncodeText(&qout;html����&qout;,&qout;htmlencode&qout;)" onclick="cit()"
                                    outype="5" class="spanfixdiv">
                                    HTML�������</div>
                                <div code="EncodeText(&qout;�ַ�&qout;,&qout;htmldecode&qout;)" onclick="cit()"
                                    outype="5" class="spanfixdiv">
                                    HTML�������</div>
                                <div code="ReadTxtFile(&qout;�ı�·��&qout;)" onclick="cit()" outype="5" class="spanfixdiv">
                                    ��ȡ�ı��ļ�</div>
                                <div code="Txt2Img(&qout;�ı�����&qout;,&qout;����;100;1;true&qout;,&qout;40;460;0;0&qout;,&qout;200;20;20;200&qout;,&qout;230;230;230&qout;,&qout;true&qout;,&qout;����ļ���&qout;,2)"
                                    onclick="cit()" outype="5" class="spanfixdiv">
                                    ����תͼƬ</div>
                                <div code="SiteName()" onclick="cit()" outype="5" class="spanfixdiv">
                                    ��վ����</div>
                                <div code="SiteTitle()" onclick="cit()" outype="5" class="spanfixdiv">
                                    ҳ�����</div>
                                <div code="InstallDir()" onclick="cit()" outype="5" class="spanfixdiv">
                                    վ���Ŀ¼</div>
                                <div code="SitePath()" onclick="cit()" outype="5" class="spanfixdiv">
                                    ��վURL</div>
                                <div code="Logo()" onclick="cit()" outype="5" class="spanfixdiv">
                                    ��վLOGO</div>
                                <div code="Banner()" onclick="cit()" outype="5" class="spanfixdiv">
                                    ��վBANNER</div>
                                <div code="Webmaster()" onclick="cit()" outype="5" class="spanfixdiv">
                                    վ������</div>
                                <div code="WebmasterEmail()" onclick="cit()" outype="5" class="spanfixdiv">
                                    վ������</div>
                                <div code="Copyright()" onclick="cit()" outype="5" class="spanfixdiv">
                                    ��Ȩ����</div>
                                <div code="ManageDir()" onclick="cit()" outype="5" class="spanfixdiv">
                                    ����Ŀ¼</div>
                                <div code="ADdir()" onclick="cit()" outype="5" class="spanfixdiv">
                                    ���Ŀ¼</div>
                                <div code="MetaKeywords()" onclick="cit()" outype="5" class="spanfixdiv">
                                    ҳ��ؼ���</div>
                                <div code="MetaDescription()" onclick="cit()" outype="5" class="spanfixdiv">
                                    ҳ��ժҪ</div>
                                <div code="UpLoadDir()" onclick="cit()" outype="5" class="spanfixdiv">
                                    �ϴ�Ŀ¼</div>
                                <div code="HitsOfHot()" onclick="cit()" outype="5" class="spanfixdiv">
                                    �ȵ�����</div>
                                <div code="CreateHtmlPath()" onclick="cit()" outype="5" class="spanfixdiv">
                                    ����HTML·��</div>
                                <div code="GetNodeEnableProtect(��ĿID)" onclick="cit()" outype="5" class="spanfixdiv">
                                    �ڵ��ֹ���ơ�����ֵ</div>
                                <div code="GetNodeEnableComment(��ĿID)" onclick="cit()" outype="5" class="spanfixdiv">
                                    ����ڵ㷢������ֵ</div>
                                <div code="GetNodeCommentNeedCheck(��ĿID)" onclick="cit()" outype="5" class="spanfixdiv">
                                    �����Ƿ���Ҫ���ֵ</div>
                                <div code="EnableComment(��ĿID)" onclick="cit()" outype="5" class="spanfixdiv">
                                    �Ƿ������ڴ˽ڵ㷢������</div>
                                <div code="EnableTouristsComment(��ĿID)" onclick="cit()" outype="5" class="spanfixdiv">
                                    �Ƿ������ο��ڴ˽ڵ㷢������</div>
                                <div code="IsLogined()" onclick="cit()" outype="5" class="spanfixdiv">
                                    ��ǰ�û��Ƿ��ѵ�¼</div>
                                <div code="IsAdminLogined()" onclick="cit()" outype="5" class="spanfixdiv">
                                    ����Ա�Ƿ��¼</div>
                                <div code="LoginedUserName()" onclick="cit()" outype="5" class="spanfixdiv">
                                    ȡ�õ�ǰ��¼�û�������</div>
                                <div code="LoginedUserEmail()" onclick="cit()" outype="5" class="spanfixdiv">
                                    ȡ�õ�ǰ��¼�û���E_mail</div>
                                <div code="GetFieldList(&qout;ģ��ID&qout;,&qout;��ĿID&qout;,&qout;�����ʽ&qout;,&qout;���Լ���ʽ&qout;,&qout;����ֵ��ʽ&qout;)"
                                    onclick="cit()" outype="5" class="spanfixdiv">
                                    ��ȡ��Ʒ������
                                </div>
                                <div code="GetModelItemName(ģ��ID)" onclick="cit()" outype="5" class="spanfixdiv">
                                    ȡ��ģ�͵���Ŀ��</div>
                                <div code="GetModelName(ģ�Ͷ�Ӧ�ı���)" onclick="cit()" outype="5" class="spanfixdiv">
                                    ȡ��ģ����</div>
                                <div code="IsShop(ģ�Ͷ�Ӧ�ı���)" onclick="cit()" outype="5" class="spanfixdiv">
                                    �Ƿ���Ʒģ��</div>
                                <div code="GetNodeFieldName(�ڵ�ID, &qout;�ڵ��ֶ���&qout;)" onclick="cit()" outype="5"
                                    class="spanfixdiv">
                                    ȡ�ýڵ��ֶ�ֵ</div>
                                <div code="IsStartWithhttp(url��ַ)" onclick="cit()" outype="5" class="spanfixdiv">
                                    �Ƿ���Ե�ַ</div>
                                <div code="ConvertSizeToShow(�ļ���С)" onclick="cit()" outype="5" class="spanfixdiv">
                                    �����ļ���С��ʾ��λ</div>
                            </div>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel runat="Server" ID="TabPanel2" HeaderText="����">
                        <ContentTemplate>
                            <div style="overflow: auto; float: left; width: 100%; height: 390px; text-align: left">
                                <pe:ExtendedLabel HtmlEncode="false" ID="attlist" runat="server"></pe:ExtendedLabel>
                            </div>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                </ajaxToolkit:TabContainer>
            </td>
            <td onclick="switchSysBar('sp','ft')" style="width: 12px; cursor: pointer">
                <img id="sp" src="<% =StyleSheetPath %>images/butClose.gif" alt="�ر�" style="border: 0px;
                    width: 12px;" />
            </td>
            <td style="text-align: left; vertical-align: top;">
                <asp:TextBox ID="TxtTemplate" runat="server" Height="430px" Width="99%" TextMode="MultiLine"
                    Wrap="False" CssClass="txt_main"></asp:TextBox>
                <div style="text-align: center; vertical-align: top;">
                    <img alt="���Ӹ߶�" src="../../Admin/Images/sizeplus.gif" onclick="sizeChange(50)" />
                    <img alt="���ٸ߶�" src="../../Admin/Images/sizeminus.gif" onclick="sizeChange(-50)" />
                </div>
            </td>
            <td onclick="switchSysBar('sp2','ft2', 'right')" style="width: 12px; cursor: pointer">
                <img id="sp2" src="<% =StyleSheetPath %>images/butOpen.gif" alt="�ر�" style="border: 0px;
                    width: 12px;" />
            </td>
            <td style="text-align: left; vertical-align: top; width: 200px;" id="ft2">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div style="text-align: center;">
                            <asp:Button ID="BtnShowSchema" OnClick="BtnShowSchema_Click" runat="server" Text="�ܹ�"
                                BackColor="Red"></asp:Button>
                            <asp:Button ID="BtnShowDetal" OnClick="BtnShowDetal_Click" runat="server" Text="ȫ��">
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
                &nbsp;<asp:Button ID="BtnPrv" OnClick="BtnPrv_Click" runat="server" Text="��һ��" Style="cursor: pointer;
                    cursor: hand; width: 88px;"></asp:Button>&nbsp;&nbsp;<asp:Button ID="BtnFinal" OnClick="BtnFinal_Click"
                        runat="server" Text="�ꡡ��" Style="cursor: pointer; cursor: hand; width: 88px;"></asp:Button>&nbsp;&nbsp;<input
                            id="BtnCancel" type="button" class="inputbutton" value="ȡ����" onclick="Redirect('LabelManage.aspx')"
                            style="cursor: pointer; cursor: hand; width: 88px;" />&nbsp; &nbsp;<asp:Button ID="OpenShowTemplate"
                                runat="server" Text="�鿴Ч��" />
                &nbsp;&nbsp;<input id="BtnXmlShow" type="button" class="inputbutton" value="��������"
                    onclick="openxmlshow();" style="cursor: pointer; cursor: hand; width: 88px;" /></td>
        </tr>
    </table>
    <table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="border">
        <tr>
            <td class="spacingtitle" style="height: 22px;" align="center">
                <b>���˵��</b></td>
        </tr>
        <tr class="tdbg">
            <td align="left">
                <ajaxToolkit:TabContainer runat="server" ID="Tabs" Height="150px">
                    <ajaxToolkit:TabPanel runat="Server" ID="TabPanel3" HeaderText="����˵��">
                        <ContentTemplate>
                            <p>
                                ���г�����XML�ڵ��б��ҷ�����ģ��༭��</p>
                            <p>
                                ��ֱ���ϷŽڵ㵽�ҷ���ģ��༭���У�ϵͳ���Զ����ϵ��ô��롣</p>
                            <p>
                                ����Ҫ����ȫ·���ڵ㣬�밴ס"CTRL"���ٽ����Ϸš�</p>
                            <p>
                                ������ó�ʹ��Macromedia Dreamweaver 8������ͨ�����������ݡ����ܣ�����ǩ���ݱ���ΪXML�ļ�����ʹ��DW8��XSLT�༭���ܽ��б��ر༭��</p>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel runat="Server" ID="TabPanel4" HeaderText="XSLT���">
                        <ContentTemplate>
                            <p>
                                XSLT�����������XML�����һ��ģ��༭���ԣ�XSLTͨ����XML���ݵ������������ɷ�����Ҫ��HTML���������룬�������������Ĺ淶���ͳ�ΪXSLTģ�塣<br />
                                XSLT����Ҳ��XML�ṹ���ĵ�ģʽ������XSLT�ж����ģ�壬Ҳ�������W3C��XML�ĸ���涨��</p>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel runat="Server" ID="TabPanel5" HeaderText="����˵��">
                        <ContentTemplate>
                            <p>
                                XSLTҪ��ʹ�õ�HTML��������з�շ��ţ�����ԭ����&lt;br&gt;�ͱ���дΪ&lt;br /&gt��</p>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                </ajaxToolkit:TabContainer>
            </td>
        </tr>
    </table>
    <asp:Panel ID="Panel1" runat="server" Style="display: none" ScrollBars="Auto" CssClass="modalPopup">
        <asp:Panel ID="Panel3" runat="server" Style="cursor: move; background-color: #CCCCDD;
            border: solid 1px Gray; color: Black; text-align: center">
            <b>��ǩ���Դ���</b>
        </asp:Panel>
        ��ǰ��ҳ��<asp:TextBox ID="TxtTempPage" runat="server" Text="1" Width="30px"></asp:TextBox><br />
        ����ҳ����<asp:TextBox ID="TxtTempPageName" runat="server" Text="test.aspx?page={$pageid/}"
            Width="480px"></asp:TextBox><br />
        ���Ա�ǩ��<asp:TextBox ID="TxtTemplateTest" runat="server" Width="480px"></asp:TextBox>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                &nbsp;<asp:Button ID="BtnShowTemplate" runat="server" OnClick="BtnShowTemplate_Click"
                    Text="ˢ��" Style="cursor: pointer; cursor: hand; width: 88px;" />
                &nbsp;&nbsp;<input id="tohtml" type="button" value="Դ��" onclick="sizeshowmode()"
                    style="cursor: pointer; cursor: hand; width: 88px;" />
                <div style="overflow: auto; width: 550px; height: 390px; background-color: #EEEEEE;
                    border: solid 1px Gray; color: Black;">
                    <pe:ExtendedLabel ID="LabShow" HtmlEncode="false" Text="��ǩ�����ʾ��" runat="server"></pe:ExtendedLabel>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:Button ID="CloseButton" runat="server" Text="�ر�" Style="cursor: pointer; cursor: hand" />
    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender" runat="server" TargetControlID="OpenShowTemplate"
        PopupControlID="Panel1" BackgroundCssClass="modalBackground" CancelControlID="CloseButton"
        DropShadow="true" PopupDragHandleControlID="Panel3" />
</asp:Content>
