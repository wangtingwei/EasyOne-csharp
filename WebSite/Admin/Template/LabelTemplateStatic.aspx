<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    ValidateRequest="false" Inherits="EasyOne.WebSite.Admin.Template.LabelTemplateStatic"
    Title="��̬��ǩ���ݱ༭" Codebehind="LabelTemplateStatic.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <style type="text/css">
    .dragspandiv{
    	background-color: #FFFBF5;
    	filter: alpha(opacity=70);
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
        width:150px;
        margin: 4px;
    }                   
</style>
    <asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server" />

    <script id="pejs" type="text/javascript">
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
        y = (nn6 ? e.clientY : event.clientY) + document.documentElement.scrollTop + 5;
        x = (nn6 ? e.clientX : event.clientX) + document.documentElement.scrollLeft + 10;
        dragspan.style.width = oDragHandle.style.width;
        dragspan.style.height = oDragHandle.style.height;
        dragspan.style.top = y + "px";
        dragspan.style.left = x + "px";
        dragspan.innerHTML = oDragHandle.innerHTML;
        document.body.appendChild(dragspan);
        document.onmousemove = moveMouse;
        
        if(oDragHandle.getAttribute("outype") == '0')
        {
            inserttext = "{PE.SiteConfig." + oDragHandle.getAttribute("code") + "/}";
        }
        else if(oDragHandle.getAttribute("outype") == '1')
        {
            inserttext = oDragHandle.innerHTML;;
        }
        else
        {
            inserttext = "<xsl:value-of select=\"$" + oDragHandle.innerHTML + "\"/>";
        }
        labeltype = oDragHandle.getAttribute("outype");
        
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
        if(labeltype == '1')
        {
            var link = "Template_addlabel.aspx?a=a&n=" + escape(inserttext);
            if(window.showModalDialog != null)
            {
                var ret = showModalDialog(link,'','dialogWidth:500px; dialogHeight:350px; help: no; scroll: no; status: no; edge: sunken;');
                if (ret != null)
                {
                    if (ret.replace(/^\s+|\s+$/g,"") == "")
                    {
                        alert("���������ֵ");
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
        else
        {
            target.value = pre + inserttext + post;
        }
    }
    else
    {
        target.focus();
        var range = document.selection.createRange();
        if(labeltype == '1')
        {
            var link = "Template_addlabel.aspx?a=a&n=" + escape(inserttext);
            if(window.showModalDialog != null)
            {
                var ret = showModalDialog(link,'','dialogWidth:500px; dialogHeight:350px; help: no; scroll: no; status: no; edge: sunken;');
                if (ret != null)
                {
                    if (ret.replace(/^\s+|\s+$/g,"") == "")
                    {
                        alert("���������ֵ");
                    }
                    else
                    {
                        range.text = ret;
                    }
                }
            }
            else
            {
            window.open(link,window,'modal=yes,width=500,height=350,menubar=no,toolbar=no,location=no,resizable=no,status=no,scrollbars=no');
            }
        }
        else
        {
            range.text = inserttext;
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
    if(dragspan != null)
    {
        document.body.removeChild(dragspan);
        dragspan = null;
    }
}

var fpath = '<% =StyleSheetPath %>';
function switchSysBar(){
    var obj = $get("switchPoint");

    if (obj.alt == "�رձ�ǩ��"){
        obj.alt = "�򿪱�ǩ��";
        obj.src = fpath + "Images/butOpen.gif";
        $get("frmTitle").style.display="none";
    }
    else
    {
        obj.alt = "�رձ�ǩ��";
        obj.src = fpath + "Images/butClose.gif";
        $get("frmTitle").style.display="";
    }
}

function sizeChange(size){
    var obj=$get("<% = TxtTemplate.ClientID %>");
    var height = parseInt(obj.offsetHeight);
    if (height+size>=100){
        obj.style.height=height+size+'px';
    }
}
-->
    </script>

    <table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="0" class="border">
        <tr>
            <td class="spacingtitle" style="height: 22px;" colspan="3" align="center">
                <b>��ǩ���ݱ༭</b></td>
        </tr>
        <tr class="tdbg">
            <td style="text-align: left; vertical-align: top; width: 220px;" id="frmTitle">
                <ajaxToolkit:TabContainer runat="server" ID="TabContainer1" Height="400px" Width="220px">
                    <ajaxToolkit:TabPanel runat="Server" ID="TabPanel3" HeaderText="��������">
                        <ContentTemplate>
                            <div style="overflow: auto; float: left; width: 100%; height: 350px; text-align: left">
                                <pe:ExtendedLabel HtmlEncode="false" ID="attlist" runat="server"></pe:ExtendedLabel>
                            </div>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel runat="Server" ID="TabPanel4" HeaderText="��չ����">
                        <ContentTemplate>
                            <div style="overflow: auto; float: left; width: 100%; height: 390px; text-align: left">
                                <div code="sitename" onclick="cit()" outype="0" class="spanfixdiv">
                                    ��վ����</div>
                                <div code="sitetitle" onclick="cit()" outype="0" class="spanfixdiv">
                                    ҳ�����</div>
                                <div code="ApplicationPath" onclick="cit()" outype="0" class="spanfixdiv">
                                    վ���Ŀ¼
                                </div>
                                <div code="sitepath" onclick="cit()" outype="0" class="spanfixdiv">
                                    ��վURL</div>
                                <div code="logo" onclick="cit()" outype="0" class="spanfixdiv">
                                    ��վLOGO</div>
                                <div code="banner" onclick="cit()" outype="0" class="spanfixdiv">
                                    ��վBANNER</div>
                                <div code="webmaster" onclick="cit()" outype="0" class="spanfixdiv">
                                    վ������</div>
                                <div code="webmasteremail" onclick="cit()" outype="0" class="spanfixdiv">
                                    վ������</div>
                                <div code="copyright" onclick="cit()" outype="0" class="spanfixdiv">
                                    ��Ȩ����</div>
                                <div code="managedir" onclick="cit()" outype="0" class="spanfixdiv">
                                    ����Ŀ¼</div>
                                <div code="addir" onclick="cit()" outype="0" class="spanfixdiv">
                                    ���Ŀ¼</div>
                                <div code="metakeywords" onclick="cit()" outype="0" class="spanfixdiv">
                                    ҳ��ؼ���</div>
                                <div code="metadescription" onclick="cit()" outype="0" class="spanfixdiv">
                                    ҳ��ժҪ</div>
                                <div code="timenow" onclick="cit()" outype="0" class="spanfixdiv">
                                    ����ʱ��</div>
                                <div code="uploaddir" onclick="cit()" outype="0" class="spanfixdiv">
                                    �ϴ�Ŀ¼</div>
                                <div code="defaultcss" onclick="cit()" outype="0" class="spanfixdiv">
                                    Ĭ��CSS����</div>
                                <div code="createhtmlpath" onclick="cit()" outype="0" class="spanfixdiv">
                                    ����HTML·��</div>
                                <div code="readfile" path="�ļ�·��"" " onclick="cit()" outype="0" class="spanfixdiv">
                                    ��ȡ�ı��ļ�</div>
                            </div>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel runat="Server" ID="TabPanel5" HeaderText="���ݱ�ǩ">
                        <ContentTemplate>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="DropLabelList" runat="server" OnSelectedIndexChanged="DropLabelList_SelectedIndexChanged"
                                        AutoPostBack="true" />
                                    <div style="overflow: auto; float: left; width: 100%; height: 390px; text-align: center">
                                        <pe:ExtendedLabel HtmlEncode="false" ID="LblLabelList" runat="server"></pe:ExtendedLabel>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                </ajaxToolkit:TabContainer>
            </td>
            <td onclick="switchSysBar()" style="width: 12px; cursor: hand; cursor: pointer">
                <img id="switchPoint" src="<% =StyleSheetPath %>images/butClose.gif" alt="�رձ�ǩ��" style="border: 0px;
                    width: 12px;" />
            </td>
            <td style="text-align: left; vertical-align: top;width: 100%">
                <asp:TextBox ID="TxtTemplate" runat="server" Height="430px" Width="99%" TextMode="MultiLine"
                    Wrap="False" />
                <div style="text-align: center; vertical-align: top;">
                    <img alt="���Ӹ߶�" src="../../Admin/Images/sizeplus.gif" onclick="sizeChange(50);" />
                    <img alt="���ٸ߶�" src="../../Admin/Images/sizeminus.gif" onclick="sizeChange(-50);" />
                </div>
            </td>
        </tr>
        <tr class="tdbg">
            <td align="center" colspan="3">
                <asp:Button ID="BtnPrv" runat="server" Text="��һ��" OnClick="BtnPrv_Click" Style="cursor: pointer;
                    cursor: hand; width: 88px;" />&nbsp;&nbsp;<asp:Button ID="BtnFinal" runat="server"
                        Text="������" OnClick="BtnFinal_Click" Style="cursor: pointer; cursor: hand; width: 88px;" />&nbsp;&nbsp;<input
                            id="BtnCancel" type="button" class="inputbutton" value="ȡ����" onclick="Redirect('LabelManage.aspx')"
                            style="cursor: pointer; cursor: hand; width: 88px;" />&nbsp; &nbsp;<asp:Button ID="OpenShowTemplate"
                                runat="server" Text="�鿴Ч��" />
            </td>
        </tr>
    </table>
    <br />
    <table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="border">
        <tr>
            <td class="spacingtitle" style="height: 22px;" colspan="2" align="center">
                <b>����˵��</b></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbg" align="left">
                <ajaxToolkit:TabContainer runat="server" ID="Tabs" Height="150px">
                    <ajaxToolkit:TabPanel runat="Server" ID="TabPanel1" HeaderText="ģ��༭">
                        <ContentTemplate>
                            <p>
                                ��ֱ�����ı����������������Ҫ��ʾ�����ֻ�html���룬Ҳ�������������Ķ���ϵͳ��ǩ������ǩ֧��ǩ��ʹ�á�</p>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel runat="Server" ID="TabPanel2" HeaderText="��������">
                        <ContentTemplate>
                            <p>
                                ��������ģ����ʹ�ñ�ǩ����������ģʽΪ&lt;xsl:value-of select=&quot;��ǩ��&quot;/&gt;��</p>
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
        ���Ա�ǩ��<asp:TextBox ID="TxtTemplateTest" runat="server" Width="480px"></asp:TextBox>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                &nbsp;<asp:Button ID="BtnShowTemplate" runat="server" OnClick="BtnShowTemplate_Click"
                    Text="ˢ��" Style="cursor: pointer; cursor: hand; width: 88px;" />
                &nbsp;&nbsp;<input id="tohtml" type="button" value="Դ��" onclick="sizeshowmode()"
                    style="cursor: pointer; cursor: hand; width: 88px;" />
                <div style="overflow: auto; width: 550px; height: 390px; background-color: #EEEEEE;
                    border: solid 1px Gray; color: Black;">
                    <asp:Label ID="LabShow" runat="server" Text="��ǩ�����ʾ��"></asp:Label>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:Button ID="CloseButton" runat="server" Text="�ر�" Style="cursor: pointer; cursor: hand" />
    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender" runat="server" TargetControlID="OpenShowTemplate"
        PopupControlID="Panel1" BackgroundCssClass="modalBackground" CancelControlID="CloseButton"
        DropShadow="true" PopupDragHandleControlID="Panel3" />
</asp:Content>
