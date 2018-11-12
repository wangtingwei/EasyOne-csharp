<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    ValidateRequest="false" Inherits="EasyOne.WebSite.Admin.Template.Pager" Title="��ӷ�ҳ��ǩ" Codebehind="Pager.aspx.cs" %>

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
            padding: 2px 2px 2px 2px; 
            width: 80px;
            height: 15px;
            float: left;
            text-align: center;
            margin: 5px;
            overflow:hidden;
            cursor: hand;
        }
        -->
</style>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" />

    <script id="pejs" type="text/javascript">
    <!--
    var dragspan;
    var src;
    
    function dragstart(showtype)
    {
        window.drag = 1;
        window.event.cancelBubble = true;
        window.event.returnValue = false;
        
        dragspan = document.createElement('div');
        dragspan.style.position = "absolute";
        dragspan.className = "dragspandiv";
        dragspan.style.zIndex = 300;
        var mousePos = mouseCoords(window.event);
        dragspan.style.left = mousePos.x + 10;
        dragspan.style.top = mousePos.y + 8;
                
        dragspan.appendChild(document.createTextNode(window.event.srcElement.innerHTML));
        src = showtype; 
        document.body.appendChild(dragspan);
    }

    function dragend()
    {
        if(window.drag)
        {
            document.body.removeChild(dragspan);
            var target = window.event.srcElement;
            target.focus();
            var tarobj = document.selection.createRange();
            tarobj.text = src;
            window.drag = 0;
            window.event.cancelBubble = false;
            window.event.returnValue = true;
        }
        
    }

    function dragmove()
    {
        if(window.drag)
        {
            var ev = ev || window.event;
            var mousePos = mouseCoords(ev);
            ev.cancelBubble = false;
            ev.returnValue = false;
                     
            dragspan.style.left = mousePos.x + 10;
            dragspan.style.top = mousePos.y + 8;
        }
    }

    function dragclear()
    {
        if(window.drag)
        {
            document.body.removeChild(dragspan);
            window.drag = 0;
            window.event.cancelBubble = false;
            window.event.returnValue = true;
        }
        
    }
       
    function mouseCoords(ev) {
        if(ev.pageX || ev.pageY) {
          return {x:ev.pageX, y:ev.pageY};
        }
        return {
          x:ev.clientX + document.documentElement.scrollLeft - document.body.clientLeft,
          y:ev.clientY + document.documentElement.scrollTop - document.body.clientTop
        };
    }
    
    function movePoint() 
    {
        if(window.drag)
        {
            var rng = event.srcElement.createTextRange(); 
            rng.moveToPoint(event.x,event.y); 
            rng.select(); 
        }
    }
    
    function sizeChange(size)
    {
        var obj=document.getElementById("<% = TxtLabelTemplate.ClientID %>");
        var height = parseInt(obj.offsetHeight);
        if (height+size>=100){
            obj.style.height=height+size+'px';
        }
    }
    
    function addclass(sourceid, tarid){
        var select=document.getElementById(sourceid);
        var tar=document.getElementById(tarid);
        for(i=0;i<select.length;i++){
            if(select[i].selected==true){
                tar.value=select[i].value;
            }
        }
    } 
    -->
    </script>

    <table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="border">
        <tr>
            <td class="spacingtitle" colspan="2" align="center">
                <b>
                    <asp:Label ID="LblPTitle" runat="server" Text="��ӷ�ҳ��ǩ"></asp:Label></b></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right" style="width: 105px">
                <strong>��ǩ���ƣ�&nbsp;</strong></td>
            <td class="tdbg" align="left">
                <asp:TextBox ID="TxtLabelName" runat="server" Width="288px"></asp:TextBox>
                <pe:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="TxtLabelName"
                    Display="Dynamic" ErrorMessage="�������ǩ����" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right" style="width: 105px">
                <strong>��ǩ���ࣺ&nbsp;</strong></td>
            <td class="tdbg" align="left">
                <asp:TextBox ID="TxtLabelType" runat="server" Width="200px" />
                <asp:DropDownList ID="DropLabelType" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right" style="width: 105px">
                <strong>��ǩ˵����&nbsp;</strong></td>
            <td class="tdbg" align="left">
                <asp:TextBox ID="TxtLabelIntro" runat="server" Width="95%" Height="80px" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right" style="width: 105px">
                <strong>��ǩ���ݣ�&nbsp;</strong></td>
            <td class="tdbg" align="left">
                <table style="width: 95%">
                    <tr>
                        <td>
                            <div id="labellist">
                                <div onmousedown="dragstart('{$totalpub/}');" class="spanfixdiv">
                                    �ܼ�¼/�ַ���</div>
                                <div onmousedown="dragstart('{$pagesize/}');" class="spanfixdiv">
                                    ÿҳ��ʾ��</div>
                                <div onmousedown="dragstart('{$firsturl/}');" class="spanfixdiv">
                                    ��ҳ��ַ</div>
                                <div onmousedown="dragstart('{$endid/}');" class="spanfixdiv">
                                    βҳID</div>
                                <div onmousedown="dragstart('{$endurl/}');" class="spanfixdiv">
                                    βҳ��ַ</div>
                                <div onmousedown="dragstart('{$currentid/}');" class="spanfixdiv">
                                    ��ǰҳID</div>
                                <div onmousedown="dragstart('{$currenturl/}');" class="spanfixdiv">
                                    ��ǰҳ·��</div>
                                <div onmousedown="dragstart('{$pagename/}');" class="spanfixdiv">
                                    ��ǰҳ����</div>
                                <div onmousedown="dragstart('{$sourcename/}');" class="spanfixdiv">
                                    ����Դ����ID</div>
                                <div onmousedown="dragstart('{$spanname/}');" class="spanfixdiv">
                                    ��ҳ����ID</div>
                                <div onmousedown="dragstart('{$prvurl/}');" class="spanfixdiv">
                                    ��һҳ��ַ</div>
                                <div onmousedown="dragstart('{$prvid/}');" class="spanfixdiv">
                                    ��һҳID</div>
                                <div onmousedown="dragstart('{$nexturl/}');" class="spanfixdiv">
                                    ��һҳ��ַ</div>
                                <div onmousedown="dragstart('{$nextid/}');" class="spanfixdiv">
                                    ��һҳID</div>
                                <div onmousedown="dragstart('{$loop range=\'��ʾ�뾶\'}��ͨҳ��ʽ$$$��ǰҳ��ʽ{$/loop}');" class="spanfixdiv">
                                    ��ҳѭ��</div>
                                <div onmousedown="dragstart('{$pageid/}');" class="spanfixdiv">
                                    ѭ����ID</div>
                                <div onmousedown="dragstart('{$pagetitle/}');" class="spanfixdiv">
                                    ѭ���ڱ���</div>
                                <div onmousedown="dragstart('{$pageurl/}');" class="spanfixdiv">
                                    ѭ����·��</div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="TxtLabelTemplate" runat="server" Height="250px" Width="100%" TextMode="MultiLine"
                                Rows="10" Wrap="False" /><asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                    ControlToValidate="TxtLabelTemplate" runat="server" ErrorMessage="��ǩ���ݲ�����Ϊ�գ�"></asp:RequiredFieldValidator>
                            <div style="text-align: center; vertical-align: top;">
                                <img alt="���Ӹ߶�" src="../../Admin/Images/sizeplus.gif" onclick="sizeChange(50);" />
                                <img alt="���ٸ߶�" src="../../Admin/Images/sizeminus.gif" onclick="sizeChange(-50);" />
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="tdbg">
            <td colspan="2" align="center">
                <asp:Button ID="BtnSubmit" runat="server" Text="ȷ��" OnClick="BtnSubmit_Click" Style="cursor: pointer;
                    cursor: hand; width: 88px;" />&nbsp;
                <input name="Cancel" type="button" class="inputbutton" id="Cancel" value="ȡ��" onclick="Redirect('PagerManage.aspx')" />
            </td>
        </tr>
    </table>
    <br />
    <table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="border">
        <tr>
            <td class="spacingtitle" colspan="2" align="center">
                <span id="Span1" style="font-weight: bold;">����˵��</span></td>
        </tr>
        <tr class="tdbg">
            <td>
                <ajaxToolkit:TabContainer runat="server" ID="Tabs" Height="150px">
                    <ajaxToolkit:TabPanel runat="Server" ID="TabPanel" HeaderText="��ǩ����">
                        <ContentTemplate>
                            <p>
                                �����뱾��ǩ�����ƣ�������һ��ȷ��������ҳ�е��ø�ʽ��ȷ��Ϊ{$PE_Page id="��ǩ��" datasoce="����Դ��ǩ��"/}��</p>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel runat="Server" ID="TabPanel1" HeaderText="��ǩ����">
                        <ContentTemplate>
                            <p>
                                ������Ϊ��ǩѡ��һ�����࣬���û������Ҫ�ķ��࣬����ֱ���ڷ������������������Ҫ�ķ������ƣ��÷��ཫ���Զ�������������ַ���Ϊ�գ���ñ�ǩ�������κη��ࡣ</p>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel runat="Server" ID="TabPanel2" HeaderText="��ǩͼ��">
                        <ContentTemplate>
                            <p>
                                Ϊ�˷������ڹ���ģ��ʱ��ʶ��ͬ�ı�ǩ��������Ϊÿ����ǩָ��һ��ͼ�꣬�༭ʱģ���еı�ǩ����ʾΪ��ѡ���ͼ�꣬ʹ�༭��ֱ�ۣ����㡣</p>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel runat="Server" ID="TabPanel3" HeaderText="��ǩģ��">
                        <ContentTemplate>
                            <p>
                                ��ǩģ�壬�����ڷ�ҳ��ǩ������ʾʱ��ʹ�õ�ģ�壬�������˷�ҳ��ǩ��ҳ���г���ʱ����ʽ��</p>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                </ajaxToolkit:TabContainer>
            </td>
        </tr>
    </table>
</asp:Content>
