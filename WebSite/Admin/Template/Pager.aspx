<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    ValidateRequest="false" Inherits="EasyOne.WebSite.Admin.Template.Pager" Title="添加分页标签" Codebehind="Pager.aspx.cs" %>

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
                    <asp:Label ID="LblPTitle" runat="server" Text="添加分页标签"></asp:Label></b></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right" style="width: 105px">
                <strong>标签名称：&nbsp;</strong></td>
            <td class="tdbg" align="left">
                <asp:TextBox ID="TxtLabelName" runat="server" Width="288px"></asp:TextBox>
                <pe:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="TxtLabelName"
                    Display="Dynamic" ErrorMessage="请输入标签名称" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right" style="width: 105px">
                <strong>标签分类：&nbsp;</strong></td>
            <td class="tdbg" align="left">
                <asp:TextBox ID="TxtLabelType" runat="server" Width="200px" />
                <asp:DropDownList ID="DropLabelType" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right" style="width: 105px">
                <strong>标签说明：&nbsp;</strong></td>
            <td class="tdbg" align="left">
                <asp:TextBox ID="TxtLabelIntro" runat="server" Width="95%" Height="80px" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right" style="width: 105px">
                <strong>标签内容：&nbsp;</strong></td>
            <td class="tdbg" align="left">
                <table style="width: 95%">
                    <tr>
                        <td>
                            <div id="labellist">
                                <div onmousedown="dragstart('{$totalpub/}');" class="spanfixdiv">
                                    总记录/字符数</div>
                                <div onmousedown="dragstart('{$pagesize/}');" class="spanfixdiv">
                                    每页显示数</div>
                                <div onmousedown="dragstart('{$firsturl/}');" class="spanfixdiv">
                                    首页地址</div>
                                <div onmousedown="dragstart('{$endid/}');" class="spanfixdiv">
                                    尾页ID</div>
                                <div onmousedown="dragstart('{$endurl/}');" class="spanfixdiv">
                                    尾页地址</div>
                                <div onmousedown="dragstart('{$currentid/}');" class="spanfixdiv">
                                    当前页ID</div>
                                <div onmousedown="dragstart('{$currenturl/}');" class="spanfixdiv">
                                    当前页路径</div>
                                <div onmousedown="dragstart('{$pagename/}');" class="spanfixdiv">
                                    当前页名称</div>
                                <div onmousedown="dragstart('{$sourcename/}');" class="spanfixdiv">
                                    数据源容器ID</div>
                                <div onmousedown="dragstart('{$spanname/}');" class="spanfixdiv">
                                    分页容器ID</div>
                                <div onmousedown="dragstart('{$prvurl/}');" class="spanfixdiv">
                                    上一页地址</div>
                                <div onmousedown="dragstart('{$prvid/}');" class="spanfixdiv">
                                    上一页ID</div>
                                <div onmousedown="dragstart('{$nexturl/}');" class="spanfixdiv">
                                    下一页地址</div>
                                <div onmousedown="dragstart('{$nextid/}');" class="spanfixdiv">
                                    下一页ID</div>
                                <div onmousedown="dragstart('{$loop range=\'显示半径\'}普通页样式$$$当前页样式{$/loop}');" class="spanfixdiv">
                                    分页循环</div>
                                <div onmousedown="dragstart('{$pageid/}');" class="spanfixdiv">
                                    循环内ID</div>
                                <div onmousedown="dragstart('{$pagetitle/}');" class="spanfixdiv">
                                    循环内标题</div>
                                <div onmousedown="dragstart('{$pageurl/}');" class="spanfixdiv">
                                    循环内路径</div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="TxtLabelTemplate" runat="server" Height="250px" Width="100%" TextMode="MultiLine"
                                Rows="10" Wrap="False" /><asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                    ControlToValidate="TxtLabelTemplate" runat="server" ErrorMessage="标签内容不可以为空！"></asp:RequiredFieldValidator>
                            <div style="text-align: center; vertical-align: top;">
                                <img alt="增加高度" src="../../Admin/Images/sizeplus.gif" onclick="sizeChange(50);" />
                                <img alt="减少高度" src="../../Admin/Images/sizeminus.gif" onclick="sizeChange(-50);" />
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="tdbg">
            <td colspan="2" align="center">
                <asp:Button ID="BtnSubmit" runat="server" Text="确定" OnClick="BtnSubmit_Click" Style="cursor: pointer;
                    cursor: hand; width: 88px;" />&nbsp;
                <input name="Cancel" type="button" class="inputbutton" id="Cancel" value="取消" onclick="Redirect('PagerManage.aspx')" />
            </td>
        </tr>
    </table>
    <br />
    <table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="border">
        <tr>
            <td class="spacingtitle" colspan="2" align="center">
                <span id="Span1" style="font-weight: bold;">操作说明</span></td>
        </tr>
        <tr class="tdbg">
            <td>
                <ajaxToolkit:TabContainer runat="server" ID="Tabs" Height="150px">
                    <ajaxToolkit:TabPanel runat="Server" ID="TabPanel" HeaderText="标签名称">
                        <ContentTemplate>
                            <p>
                                请输入本标签的名称，该名称一旦确定后，在网页中调用格式即确定为{$PE_Page id="标签名" datasoce="数据源标签名"/}。</p>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel runat="Server" ID="TabPanel1" HeaderText="标签分类">
                        <ContentTemplate>
                            <p>
                                您可以为标签选择一个分类，如果没有您需要的分类，可以直接在分类输入框中输入您需要的分类名称，该分类将会自动创建，如果保持分类为空，则该标签不属于任何分类。</p>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel runat="Server" ID="TabPanel2" HeaderText="标签图标">
                        <ContentTemplate>
                            <p>
                                为了方便您在管理模板时，识别不同的标签，您可以为每个标签指定一个图标，编辑时模板中的标签会显示为您选择的图标，使编辑更直观，方便。</p>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel runat="Server" ID="TabPanel3" HeaderText="标签模板">
                        <ContentTemplate>
                            <p>
                                标签模板，就是在分页标签具体显示时，使用的模板，它定义了分页标签在页面中呈现时的样式。</p>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                </ajaxToolkit:TabContainer>
            </td>
        </tr>
    </table>
</asp:Content>
