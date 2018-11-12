<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Index"
    EnableViewState="false" Codebehind="Index.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">

<script language="javascript" src="<%=BasePath%>JS/jquery.pack.js" type="text/javascript"></script>

<script language="javascript" type="text/JavaScript">       
<!--
var displaymode=0;
var StyleSheetPath="<%=StyleSheetPath%>";
function jumpto(inputurl)
{
    if (document.getElementById&&displaymode==0)
        document.getElementById("main_right").src=inputurl
    else if (document.all&&displaymode==0)
        document.all.external.src=inputurl
    else{
        if (!window.win2||win2.closed)
            win2=window.open(inputurl)
        else{
        }
    }
}

function ShowMain(FileName_Left,FileName_Right)
{
    if(FileName_Left != "")
    {
        var checkLeftUrl = CheckCurrentLeftUrl(FileName_Left);
        if(checkLeftUrl=="false")
        {
            document.getElementById("left").src = FileName_Left + GetUrlParm(FileName_Left);
        }
    }
    if(FileName_Right != "")
    {
        document.getElementById("main_right").src = FileName_Right + GetUrlParm(FileName_Right);
    }
} 

function CheckCurrentLeftUrl(leftUrl)
{
    var src = document.getElementById("left").src;
    var regex = /\s*[\?&]{1,1}t=[0-9]{1,}$/;
    var currentLeftUrl = src.replace(regex,'');
    if(currentLeftUrl.lastIndexOf(leftUrl) >= 0)
    {
        if(currentLeftUrl.lastIndexOf(leftUrl) == (currentLeftUrl.length-leftUrl.length))
        {
            return "true";
        }
    }
    return "false";
}

function GetUrlParm(url)
{
    var urlparm = "?";
    if(url.indexOf('?')>=0)
    {
        urlparm = "&";
    }
    urlparm = urlparm + "t=" + GetRandomNum();
    return urlparm;
}
  
function ShowOperatingList(FileName_Left,FileName_Right)
{
    if(FileName_Left != "")
    {
        var checkLeftUrl = CheckCurrentLeftUrl(FileName_Left);
        if(checkLeftUrl=="false")
        {
            document.getElementById("left").src = FileName_Left + GetUrlParm(FileName_Left);
        }
    }
    if(FileName_Right != "")
    {
        document.getElementById("main_right").src = FileName_Right + GetUrlParm(FileName_Right);
    }
}

function GetRandomNum()
{
        var Range = 1000;
        var Rand = Math.random();
        return (Math.round(Rand * Range));
}

function switchSysBar()
{
    var obj = document.getElementById("switchPoint");
    if (obj.alt == "�ر�����")
    {
        obj.alt = "������";
        obj.src = "" + StyleSheetPath + "Images/butOpen.gif";
        document.getElementById("frmTitle").style.display="none";
        var width,height
        width=document.body.clientWidth-12;
        height=document.body.clientHeight-70;
        document.getElementById("main_right").style.height=height;
        document.getElementById("main_right").style.width=width;  
    }
    else
    {
        obj.alt = "�ر�����";
        obj.src = "" + StyleSheetPath + "Images/butClose.gif";
        document.getElementById("frmTitle").style.display="";
        onload();
    }
}

var tID="";

function ShowHideLayer(ID)
{
    if(ID != tID)
    {
        if(tID != "")
        {
            document.getElementById(tID).style.display="none";
            document.getElementById("A"+tID).style.backgroundImage = "url(" + StyleSheetPath + "Images/digital_left.gif)";
            document.getElementById("Span"+tID).style.backgroundImage = "url(" + StyleSheetPath + "Images/digital_side.gif)";
            document.getElementById("Span"+tID).className = "digitaltext";
        }
        document.getElementById(ID).style.display = "";
        document.getElementById("A"+ID).style.backgroundImage ="url(" + StyleSheetPath + "Images/seg_left.gif)";
        document.getElementById("Span"+ID).style.backgroundImage = "url(" + StyleSheetPath + "Images/seg_side.gif)";
        document.getElementById("Span"+ID).className = "segtext";
        tID=ID;
    }
}

var message=0,order=0,time=0,uncheckarticle=0;
var unsigninarticle=0,stockalarm=0,noconsignment=0;
var mintime=5000,addtime=1000,maxtime=50000;
var clock;
function timeOut()
{
    if(time>=(maxtime-mintime)/addtime)
       time=(maxtime-mintime)/addtime;
    return mintime+addtime*time;
}
function OpenLink(FileName_Left,FileName_Right)
{
    if(parent.document.getElementById("left").src != FileName_Left)
    {
        parent.document.getElementById("left").src=FileName_Left;
    }
    parent.document.getElementById("main_right").src=FileName_Right;
}
function openMessageManage()
{OpenLink('Accessories/MessageGuide.aspx','Accessories/MessageManage.aspx');}
function openOrderList()
{OpenLink('Shop/OrderGuide.aspx','Shop/OrderList.aspx?SearchType=4');}
function openContentManage()
{OpenLink('Contents/NodeTree.aspx','Contents/ContentManage.aspx?Status=101');}
function openCommentManage()
{OpenLink('Contents/CommentGuide.aspx','Contents/CommentManage.aspx?Enquiries=true&SearchType=2');}
function openContentSignin()
{OpenLink('Contents/NodeTree.aspx','Contents/ContentSignin.aspx');}
function openProductManage(type)
{OpenLink('Shop/ShopNodeTree.aspx','Shop/ProductManage.aspx?SearchType=SpeedSearch&Keyword='+type.toString());}
function openNoConsignment()
{OpenLink('Shop/OrderGuide.aspx','Shop/OrderList.aspx?SearchType=7');}
function showPop()
{
      $.post('<%= BasePath %>' + 'ajax.aspx',"<?xml version='1.0' encoding='utf-8'?><root><type>showPop</type></root>",function(s)
		{
            var ms = $("messagecount",s).text();
			var o = $("ordercount",s).text();
			var unch=$("articlestatuscount",s).text();
			var unsignin=$("articlesignincount",s).text();
			var sa=$("productstockalarmcount",s).text();
			var noc=$("ordercountbynoconsignment",s).text();
			var commentcount = $("commentcount",s).text();
			
			if(message==ms&&order==o&&uncheckarticle==unch&&unsigninarticle==unsignin&&stockalarm==sa&&noconsignment==noc)
			{
			time++;//û�ı�ʱ�ͼ�1�����������������ѯ�ʼ��
			}
			else{ 
			if(time>0)
			time--;//�иı�ʱ�ͼ�1�����ڼ����������ѯ�ʼ��
			message=ms;
			order=o;
			uncheckarticle=unch;
			unsigninarticle=unsignin;
			stockalarm=sa;
			noconsignment=noc;
			var html = "";
			
			if(ms > 0)
			    html+= "<a href='#' onclick='javascript:openMessageManage();'>���� <span style='color:red'>"+ms+"</span> ��վ�ڶ���Ϣ����</a><br/>";
			if(o > 0)
			    html+="<a href='#' onclick='javascript:openOrderList();'>���� <span style='color:red'>"+o+"</span> ��������ȷ��</a><br/>";
			if(noc > 0)
			    html+="<a href='#' onclick='javascript:openNoConsignment();'>���� <span style='color:red'>"+noc+"</span> ������������</a><br/>";
			if(unch > 0)
			    html+="<a href='#' onclick='javascript:openContentManage();'>���� <span style='color:red'>"+unch+"</span> ƪ���ݴ����</a><br/>";
			if(commentcount > 0)
			    html+="<a href='#' onclick='javascript:openCommentManage();'>���� <span style='color:red'>"+commentcount+"</span> ƪ���۴����</a><br/>";
			if(unsignin > 0)
			    html+="<a href='#' onclick='javascript:openContentSignin();'>���� <span style='color:red'>"+unsignin+"</span> ƪ���ݴ�ǩ��</a><br/>";
			if(sa > 0)
			    html+="<a href='#' onclick='javascript:openProductManage(29);'>���� <span style='color:red'>"+sa+"</span> ����汨������Ʒ��Ҫ����</a><br/>";
			$("#info").html(html);
	        if(ms!="0"||o!="0"||unch!="0"||unsignin!="0"||sa!="0"||noc!="0")
			$("#showPop").show("slow");
			else $("#showPop").hide("slow");
			}
         });
}
var isShowPop = true;
function ClosePop()
{
    $('#showPop').hide('slow');
    isShowPop = false;
}
$(document).ready(function(){
     $("#info").ajaxComplete(function(whoareyou, request, settings){
     clearTimeout(clock);
     if(isShowPop)
     {
        clock=setTimeout("showPop()",timeOut());
     }
     });
     if(isShowPop)
        showPop();
        
    setTimeout("ClosePop()",30000);
    });
    //-->
</script>

<head id="Head1" runat="server">
    <title id="AdminPageTitle" runat="server"></title>
</head>
<body id="Indexbody" onload="onload();">
    <form id="myform" runat="server">
    <table border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td colspan="3">
                <div id="content">
                    <ul id="ChannelMenuItems" runat="server">
                    </ul>
                    <div id="SubMenu" runat="server">
                    </div>
                    <div id="Announce">
                        <a href="<%= BasePath %>" target="_blank" title="��վ��ҳ">��վ��ҳ</a>&nbsp;|&nbsp;<a href="http://tech.EasyOne.net/"
                            target="_blank" title="EasyOne.NET ����">����</a>&nbsp;|&nbsp;<a href="Logout.aspx" title="��ȫ�˳�">��ȫ�˳�</a></div>
                </div>
            </td>
        </tr>
        <tr style="vertical-align: top;">
            <td id="frmTitle">
                <iframe frameborder="0" id="left" name="left" scrolling="auto" src="<%= IndexLeftUrl %>"
                    style="width: 195px; height: 800px; visibility: inherit; z-index: 2;" title="hj"></iframe>
            </td>
            <td onclick="switchSysBar()" class="but">
                <img id="switchPoint" src="<%=StyleSheetPath%>images/butClose.gif" alt="�ر�����" style="border: 0px;
                    width: 12px;" />
            </td>
            <td>
                <iframe frameborder="0" id="main_right" name="main_right" scrolling="yes" src="<%=IndexRightUrl%>"
                    style="width: 1280px; height: 800px; visibility: inherit; z-index: 2; overflow-x: hidden;"  title="">
                </iframe>
                <div class="clearbox2" />
            </td>
        </tr>
    </table>

    <script type="text/javascript">
    <!--
    
    function onload()
    {
        var width = document.body.clientWidth - 207;
        var height = document.body.clientHeight - 78;
        document.getElementById("main_right").style.width = width > 0 ? width : 0;
        document.getElementById("main_right").style.height = height> 0 ? height : 0;
        document.getElementById("left").style.height = height > 0 ? height : 0;
        ShowHideLayer('ChannelMenu_MenuMyDeskTop');
    }
    window.onresize = onload;
    //-->
    </script>

    <script type="text/javascript" src="<%= CheckNewVersionJSUrl %>"></script>

    </form>
    <div id="showPop" class="popupTips" title="��ʾ����">
        <dl>
            <dt><a onclick="javascript:ClosePop();" href="#" title="������ʾ" class="tips_close"></a><a
                onclick="javascript:$('#showPop').hide('slow');" href="#" title="����" class="tips_fold">
            </a>��ܰ��ʾ </dt>
            <dd id="info">
            </dd>
        </dl>
    </div>
</body>
</html>
