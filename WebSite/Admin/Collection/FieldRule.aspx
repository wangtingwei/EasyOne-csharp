<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Collection.FieldRule"
    MasterPageFile="~/Admin/MasterPage.master" Title="采集配置字段设置" ValidateRequest="false" Codebehind="FieldRule.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">

    <script language="javascript" type="text/javascript" src="../../JS/Common.js"></script>

    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr align='center'>
            <td colspan='2' class='title'>
                <b>
                    <asp:Label ID="LblFieldName" runat="server" Text=""></asp:Label></b>
            </td>
        </tr>
        <tr class='tdbg'>
            <td colspan="2" style="height: 300px" class='tdbg'>
                <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border" style="height: 300px">
                    <tr class='tdbg'>
                        <td style="width: 45%; height: 300px" valign="top">
                            <pe:ExtendedLabel HtmlEncode="false" ID="LblArrContentUrl" runat="server" Text=""></pe:ExtendedLabel><br />
                            <asp:TextBox ID="TxtShowCode" runat="server" Width="500px" Height="470px" TextMode="MultiLine"></asp:TextBox><br />
                            <input type="button" value="获取源代码" onclick="ShowContent()" class="inputbutton" />
                            &nbsp;&nbsp;<asp:Label ID="LblLink" runat="server" Text=""></asp:Label>
                        </td>
                        <td style="height: 300px" valign="top">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                <tr align="center">
                                    <td id="TabTitle0" class="titlemouseover" onclick="ShowTabs(0)" runat="server">
                                        字段设置
                                    </td>
                                    <td id="TabTitle1" class="tabtitle" onclick="ShowTabs(1)" runat="server" visible="false">
                                        分页设置
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                            <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
                                <tbody id='Tabs0' runat="server">
                                    <tr class='tdbg'>
                                        <td style="height: 30%" valign="top">
                                            字段设置开始：<br />
                                            <asp:TextBox ID="TxtFieldBegin" runat="server" Height="80px" TextMode="MultiLine"
                                                Width="80%"></asp:TextBox>
                                            <pe:RequiredFieldValidator ID="ValeFieldBegin" ControlToValidate="TxtFieldBegin"
                                                ErrorMessage="字段设置开始代码不能为空！" runat="server" ValidationGroup="Field" RequiredText=""></pe:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr class='tdbg'>
                                        <td style="height: 30%" valign="top">
                                            字段设置结束：<br />
                                            <asp:TextBox ID="TxtFieldEnd" runat="server" Height="80px" TextMode="MultiLine" Width="80%"></asp:TextBox>
                                            <pe:RequiredFieldValidator ID="ValeFieldEnd" ControlToValidate="TxtFieldEnd" ErrorMessage="字段设置结束代码不能为空！"
                                                runat="server" ValidationGroup="Field" RequiredText=""></pe:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr class='tdbg'>
                                        <td style="height: 30%" valign="top">
                                            排除规则：
                                            <asp:DropDownList ID="DropExclosionId" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr class='tdbg'>
                                        <td style="height: 40%" valign="top">
                                            <table>
                                                <tr class='tdbg'>
                                                    <td style="width: 48%" valign="top">
                                                        公用过滤项目：<br />
                                                        <asp:ListBox ID="ListFilterRuleID" runat="server" DataTextField="FilterName" DataValueField="FilterRuleId"
                                                            SelectionMode="Multiple" Width="120" Height="160"></asp:ListBox>
                                                    </td>
                                                    <td style="width: 4%">
                                                    </td>
                                                    <td style="width: 48%" valign="top">
                                                        私有过滤项目：<br />
                                                        <asp:ListBox ID="ListFilterSelect" runat="server" SelectionMode="Multiple" Width="120"
                                                            Height="160">
                                                            <asp:ListItem Value="Iframe">过滤内联页</asp:ListItem>
                                                            <asp:ListItem Value="Object">过滤Falsh</asp:ListItem>
                                                            <asp:ListItem Value="Script">过滤脚本</asp:ListItem>
                                                            <asp:ListItem Value="Style">过滤样式</asp:ListItem>
                                                            <asp:ListItem Value="Div">过滤Div容器</asp:ListItem>
                                                            <asp:ListItem Value="Span">过滤Span容器</asp:ListItem>
                                                            <asp:ListItem Value="Table">过滤表格</asp:ListItem>
                                                            <asp:ListItem Value="Img">过滤图片</asp:ListItem>
                                                            <asp:ListItem Value="Font">过滤字体</asp:ListItem>
                                                            <asp:ListItem Value="A">过滤链接</asp:ListItem>
                                                            <asp:ListItem Value="Html">过滤html元素</asp:ListItem>
                                                        </asp:ListBox>
                                                    </td>
                                                </tr>
                                                <tr class='tdbg'>
                                                    <td valign="top" colspan='3' align="center">
                                                        <b>提示：按住“Ctrl”或“Shift”键可以多选</b>
                                                    </td>
                                                </tr>
                                                <tr class='tdbg'>
                                                    <td valign="top" colspan='3'>
                                                        <asp:Label ID="LblKeyWord" runat="server" Text="关键字长度：" Visible="false" />
                                                        <asp:TextBox ID="TxtKeyWord" runat="server" Text="2" Width="70px" Visible="false"></asp:TextBox><pe:NumberValidator
                                                            ID="ValeKeyWord" ControlToValidate="TxtKeyWord" Display="Dynamic" ErrorMessage="请填写整数！"
                                                            runat="server" Visible="false"></pe:NumberValidator>
                                                        <asp:CheckBox ID="SavePhoto" runat="server" Text="是否保存远程图片" Visible="false" />
                                                        <input type="button" value="测试字段" onclick="testField()" class="inputbutton" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </tbody>
                                <tbody id='Tabs1' runat="server" style='display: none;'>
                                    <tr class='tdbg'>
                                        <td style="height: 40%" valign="top">
                                            选择分页类型：<br />
                                            <asp:RadioButton ID="RadlPaingType" Text="不分页" GroupName="RadlPaingType" runat="server"
                                                Checked="true" />
                                            <br />
                                            <asp:RadioButton ID="RadlPaingType1" Text="从源代码中获取下一页的URL" GroupName="RadlPaingType"
                                                runat="server" />
                                            <br />
                                            <asp:RadioButton ID="RadlPaingType2" Text="批量指定分页URL代码" GroupName="RadlPaingType"
                                                runat="server" />
                                            <br />
                                            <asp:RadioButton ID="RadlPaingType3" Text="手动添加分页URL代码" GroupName="RadlPaingType"
                                                runat="server" />
                                            <br />
                                            <asp:RadioButton ID="RadlPaingType4" Text="从源代码中获取分页URL" GroupName="RadlPaingType"
                                                runat="server" />
                                        </td>
                                    </tr>
                                    <tr class='tdbg' id='ListPaing1' style='display: none;'>
                                        <td style="height: 40%" valign="top">
                                            “下一页”URL开始代码：↓<br />
                                            <asp:TextBox ID="TxtPaingBegin" runat="server" Height="80px" TextMode="MultiLine"
                                                Width="80%"></asp:TextBox>
                                            <br />
                                            “下一页”URL结束代码：↓<br />
                                            <asp:TextBox ID="TxtPaingEnd" runat="server" Height="80px" TextMode="MultiLine" Width="80%"></asp:TextBox>
                                            <br />
                                            <input type="button" value="测试下一页" onclick="testPaing()" class="inputbutton" />
                                        </td>
                                    </tr>
                                    <tr class='tdbg' id='ListPaing2' style='display: none'>
                                        <td style="height: 40%" valign="top">
                                            URL地址：<asp:TextBox ID="TxtPaingAddress" runat="server" Width="80%"></asp:TextBox>
                                            <br />
                                            <span style="color: Green">例：http://www.xxxxx.com/news/index_{$ID}.html&nbsp;&nbsp;&nbsp;&nbsp;{$ID}代表分页数</span>
                                            <br />
                                            ID范围：<asp:TextBox ID="TxtScopeBegin" runat="server" Width="50px" /><span lang="en-us">
                                                To </span>
                                            <asp:TextBox ID="TxtScopeEnd" runat="server" Width="50px" />
                                            <br />
                                            <span style="color: Green">例： 1 ~ 9 或 9 ~ 1 升序或倒序采集</span>
                                            <br />
                                        </td>
                                    </tr>
                                    <tr class='tdbg' style='display: none' id='ListPaing3'>
                                        <td style="height: 40%" valign="top">
                                            URL列表：↓<br />
                                            <asp:TextBox ID="TxtListPaing" runat="server" Height="120px" TextMode="MultiLine"
                                                Width="80%"></asp:TextBox>
                                            <br />
                                            <span style="color: Green">注：一行写一个网页地址</span>
                                            <br />
                                        </td>
                                    </tr>
                                    <tr class="tdbg" style="display: none;" id="ListPaing4">
                                        <td style="height: 40%" valign="top">
                                            分页代码开始：<br />
                                            <asp:TextBox ID="TxtPaingBegin2" runat="server" Height="80px" TextMode="MultiLine"
                                                Width="90%"></asp:TextBox><br />
                                            分页代码结束：<br />
                                            <asp:TextBox ID="TxtPaingEnd2" runat="server" Height="80px" TextMode="MultiLine"
                                                Width="90%"></asp:TextBox><br />
                                            分页URL开始代码：<br />
                                            <asp:TextBox ID="TxtLinkBegin2" runat="server" Width="90%"></asp:TextBox><br />
                                            分页URL结束代码：<br />
                                            <asp:TextBox ID="TxtLinkEnd2" runat="server" Width="90%"></asp:TextBox><br />
                                            <input type="button" value="测试从源代码中获取分页URL" onclick="testPaing2()" class="inputbutton" />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HdnTestContent" runat="server" />
    <input id="url" type="hidden" value="<%=m_Url%>" />
    <input id="codeType" type="hidden" value="<%=m_CodeType%>" />
    <input id="listBegin" type="hidden" value="" />
    <input id="listEnd" type="hidden" value="" />
    <input id="fieldType" type="hidden" value="<%=m_FieldType%>" />
    <input id="keyword" type="hidden" value="" />
    <input id="filterRuleId" type="hidden" value="" />
    <input id="filter" type="hidden" value="" />
    <input id="testContent" type="hidden" value="" />
    <input id="paingBegin" type="hidden" value="" />
    <input id="paingEnd" type="hidden" value="" />
    <input id="ListPaing0" type="hidden" value="" />
    <input id="paingBegin2" type="hidden" value="" />
    <input id="paingEnd2" type="hidden" value="" />
    <input id="linkBegin2" type="hidden" value="" />
    <input id="linkEnd2" type="hidden" value="" />
    <br />
    <center>
        <asp:Button ID="BtnSubmit" Text="保存" OnClick="EBtnSubmit_Click" runat="server" ValidationGroup="Field" />
        &nbsp;&nbsp;
        <input name='Cancel' type='button' class="inputbutton" id='Cancel2' value='取消' onclick="Close();" />
    </center>

    <script language="JavaScript" type="text/javascript">
    <!--
        function ShowTabs(ID){
            if (ID == 0){
                document.getElementById("<%=TabTitle0.ClientID%>").className="titlemouseover";
                document.getElementById("<%=TabTitle1.ClientID%>").className="tabtitle";
                document.getElementById("<%=Tabs0.ClientID%>").style.display="";
                document.getElementById("<%=Tabs1.ClientID%>").style.display="none";
            }else{
                document.getElementById("<%=TabTitle1.ClientID%>").className="titlemouseover";
                document.getElementById("<%=TabTitle0.ClientID%>").className="tabtitle";
                document.getElementById("<%=Tabs1.ClientID%>").style.display="";
                document.getElementById("<%=Tabs0.ClientID%>").style.display="none";
            }
        }
                
        function testContentLink(contentUrl){
            if (contentUrl != "")
            {
                $("url").value = contentUrl;
                $("contentLink").href = contentUrl;
                var x = new AjaxRequest('XML','testShowContent');
                x.para = ['url', 'codeType'];
                x.post('testShowContent', 'ajax.aspx', function(s) {
                var xml = x.createXmlDom(s);
                var testContent = xml.getElementsByTagName("testContent")[0].firstChild.data; 
                $("<%=TxtShowCode.ClientID%>").value = testContent;
                $("<%=HdnTestContent.ClientID%>").value = testContent; 
                });
            }
        }
        
        function ShowContent()
        {
             $("<%=TxtShowCode.ClientID%>").value = $("<%=HdnTestContent.ClientID%>").value;
        }
        
        function testField()
        {
            if($("<%=TxtFieldBegin.ClientID%>").value == '')
            {
                alert("字段设置开始代码不能为空！");
                $("<%=TxtFieldBegin.ClientID%>").focus();
                return false; 
            }
            else
            {
                 $("listBegin").value = $("<%=TxtFieldBegin.ClientID%>").value;
            }
            
            if($("<%=TxtFieldEnd.ClientID%>").value == '')
            {
                alert("字段设置结束代码不能为空！");
                $("<%=TxtFieldEnd.ClientID%>").focus();
                return false; 
            }
            else
            {
                 $("listEnd").value = $("<%=TxtFieldEnd.ClientID%>").value;
            }
            
            if  ($("fieldType").value == 'KeywordType'){
                $("keyword").value = $("<%=TxtKeyWord.ClientID%>").value;
            }
             
            var filterRuleId = "";
                 
            obj = document.aspnetForm.<%=ListFilterRuleID.UniqueID %>;
            for(var i = 0; i < obj.length; i++)
            {
                var opt = obj.options[i];
                if (opt.selected)
                {
                    if(filterRuleId == ""){
                        filterRuleId = opt.value;
                    }
                    else
                    {
                        filterRuleId += "," + opt.value;
                    } 
                }
            }
            
            $("filterRuleId").value = filterRuleId;
            
            var filter = "";            
            obj = document.aspnetForm.<%=ListFilterSelect.UniqueID%>;
            for(var i = 0; i < obj.length; i++)
            {
                var opt = obj.options[i];
                if (opt.selected)
                {
	                if(filter != ""){      
		                filter = filter + "," + opt.value; 
	                }
	                else{
		                filter = opt.value;
	                }
                }
            }
                        
            $("filter").value = filter;
		    		                       
            var x = new AjaxRequest('XML','testContent');
                x.para = ['url', 'codeType','listBegin','listEnd','fieldType','keyword','filterRuleId','filter'];
                x.post('testField', 'ajax.aspx', function(s) {
                var xml = x.createXmlDom(s);
                var testContent = xml.getElementsByTagName("testContent")[0].firstChild.data; 
                if (testContent != "$False$"){
                    $("<%=TxtShowCode.ClientID%>").value = testContent;
                }
                else
                {
                    alert("没有截取到数据！");
                }
            });
        }
        
        function testPaing()
        {
            if($("<%=TxtShowCode.ClientID%>").value == '')
            {
                alert("左侧测试文本框代码不能为空！");
                $("<%=TxtShowCode.ClientID%>").focus();
                return false; 
            }
            else
            {
                 $("testContent").value = $("<%=TxtShowCode.ClientID%>").value;
            }
                    
            if($("<%=TxtPaingBegin.ClientID%>").value == '')
            {
                alert("“下一页”URL开始代码不能为空！");
                $("<%=TxtPaingBegin.ClientID%>").focus();
                return false; 
            }
            else
            {
                 $("paingBegin").value = $("<%=TxtPaingBegin.ClientID%>").value;
            }
            
            if($("<%=TxtPaingEnd.ClientID%>").value == '')
            {
                alert("“下一页”URL结束代码不能为空！");
                $("<%=TxtPaingEnd.ClientID%>").focus();
                return false; 
            }
            else
            {
                 $("paingEnd").value = $("<%=TxtPaingEnd.ClientID%>").value;
            }
            
            var x = new AjaxRequest('XML','testContent');
                x.para = ['url','paingBegin','paingEnd','testContent'];
                x.post('testPaing', 'ajax.aspx', function(s) {
                var xml = x.createXmlDom(s);
                var testContent = xml.getElementsByTagName("testContent")[0].firstChild.data; 
                if (testContent != "$False$"){
                    $("<%=TxtShowCode.ClientID%>").value = testContent;
                }
                else
                {
                    alert("没有截取到数据！");
                }
            });
        }     
        
        function testPaing2()
        {
            if($("<%=TxtShowCode.ClientID%>").value == '')
            {
                alert("左侧测试文本框代码不能为空！");
                $("<%=TxtShowCode.ClientID%>").focus();
                return false; 
            }
            else
            {
                 $("testContent").value = $("<%=TxtShowCode.ClientID%>").value;
            }    
            if($("<%=TxtPaingBegin2.ClientID%>").value == '')
            {
                alert("分页代码开始不能为空！");
                $("<%=TxtPaingBegin2.ClientID%>").focus();
                return false; 
            }
            else
            {
                 $("paingBegin2").value = $("<%=TxtPaingBegin2.ClientID%>").value;
            }
            if($("<%=TxtPaingEnd2.ClientID%>").value == '')
            {
                alert("分页代码结束不能为空！");
                $("<%=TxtPaingEnd2.ClientID%>").focus();
                return false; 
            }
            else
            {
                 $("paingEnd2").value = $("<%=TxtPaingEnd2.ClientID%>").value;
            }
            if($("<%=TxtLinkBegin2.ClientID%>").value == '')
            {
                alert("分页URL开始代码不能为空！");
                $("<%=TxtLinkBegin2.ClientID%>").focus();
                return false; 
            }
            else
            {
                 $("linkBegin2").value = $("<%=TxtLinkBegin2.ClientID%>").value;
            }

            if($("<%=TxtLinkEnd2.ClientID%>").value == '')
            {
                alert("分页URL结束代码不能为空！");
                $("<%=TxtLinkEnd2.ClientID%>").focus();
                return false; 
            }
            else
            {
                 $("linkEnd2").value = $("<%=TxtLinkEnd2.ClientID%>").value;
            }
            
            var x = new AjaxRequest('XML','testContent');
                x.para = ['url','paingBegin2','paingEnd2','linkBegin2','linkEnd2','testContent'];
                x.post('testPaing2', 'ajax.aspx', function(s) {
                var xml = x.createXmlDom(s);
                var testContent = xml.getElementsByTagName("testContent")[0].firstChild.data; 
                if (testContent != "$False$"){
                    $("<%=TxtShowCode.ClientID%>").value = testContent;
                }
                else
                {
                    alert("没有截取到数据！");
                }
            });
        } 
        
        function ListPaing(Paing)
        {
            for (i = 0; i <= 4; i++)
            {
                if (Paing == i)
                {
                    document.getElementById("ListPaing" + Paing).style.display="";
                }
                else
                {
                    document.getElementById("ListPaing" + i).style.display="none";
                }
            }
        }
                
        function ResumeError() {
            return true;
        }
        function Close() {
            window.close();
        }
        window.onerror = ResumeError;
        
    //-->
    </script>

</asp:Content>
