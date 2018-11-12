<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Collection.ConfigStep2"
    MasterPageFile="~/Admin/MasterPage.master" Title="采集配置第二步" ValidateRequest="false" Codebehind="ConfigStep2.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">

    <script language="javascript" type="text/javascript" src="../../JS/Common.js"></script>

    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td colspan="2" class="spacingtitle">
                <b>列表页采集设置</b>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" style="width: 15%" align="right">
                <strong>所属采集项目：</strong></td>
            <td>
                <asp:Label ID="LblItemName" runat="server" Text="" />
            </td>
        </tr>
    </table>
    <br />
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border" style="height: 300px">
        <tr class="tdbg">
            <td style="width: 45%; height: 300px" valign="top">
                <asp:TextBox ID="TxtShowCode" runat="server" Height="420px" TextMode="MultiLine"
                    Width="500px" ></asp:TextBox><br />
                <input type="button" value="获取源代码" onclick="ShowContent()" class="inputbutton" />
                &nbsp;&nbsp;<pe:ExtendedLabel HtmlEncode="false" ID="LblLink" runat="server" Text=""></pe:ExtendedLabel>
            </td>
            <td style="height: 300px" valign="top">
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr align="center">
                        <td id="TabTitle0" class="titlemouseover" onclick="ShowTabs(0)" runat="server">
                            列表设置</td>
                        <td id="TabTitle1" class="tabtitle" onclick="ShowTabs(1)" runat="server">
                            分页设置</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
                <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border" style="height: 100%">
                    <tbody id="Tabs0" runat="server">
                        <tr class="tdbg">
                            <td style="height: 30%" valign="top">
                                列表开始代码：<br />
                                <asp:TextBox ID="TxtListBegin" runat="server" Height="80px" TextMode="MultiLine"
                                    Width="90%"></asp:TextBox>
                                <pe:RequiredFieldValidator ID="ValeListBegin" ControlToValidate="TxtListBegin" ErrorMessage="列表设置开始代码不能为空！"
                                    runat="server" RequiredText=""></pe:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td style="height: 30%" valign="top">
                                列表结束代码：<br />
                                <asp:TextBox ID="TxtListEnd" runat="server" Height="80px" TextMode="MultiLine" Width="90%"></asp:TextBox>
                                <pe:RequiredFieldValidator ID="ValeListEnd" ControlToValidate="TxtListEnd" ErrorMessage="列表设置结束代码不能为空！"
                                    runat="server" RequiredText=""></pe:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td style="height: 20%" valign="top">
                                链接开始代码：<br />
                                <asp:TextBox ID="TxtLinkBegin" runat="server" Width="90%"  Height="40px" TextMode="MultiLine"></asp:TextBox>
                                <pe:RequiredFieldValidator ID="ValeLinkBegin" ControlToValidate="TxtLinkBegin" ErrorMessage="链接设置开始代码不能为空！"
                                    runat="server" RequiredText=""></pe:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td style="height: 20%" valign="top">
                                链接结束代码：<br />
                                <asp:TextBox ID="TxtLinkEnd" runat="server" Width="90%"  Height="40px" TextMode="MultiLine"></asp:TextBox>
                                <pe:RequiredFieldValidator ID="ValeLinkEnd" ControlToValidate="TxtLinkEnd" ErrorMessage="链接设置结束代码不能为空！"
                                    runat="server" RequiredText=""></pe:RequiredFieldValidator>
                                <br />
                                <input type="button" value="测试列表" onclick="testList()" class="inputbutton" />&nbsp;&nbsp;<input
                                    type="button" value="测试链接" onclick="testLink()" class="inputbutton" />
                            </td>
                        </tr>
                    </tbody>
                    <tbody id="Tabs1" runat="server" style="display: none;">
                        <tr class="tdbg">
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
                        <tr class="tdbg" id="ListPaing1" style="display: none;">
                            <td style="height: 40%" valign="top">
                                “下一页”URL开始代码：<br />
                                <asp:TextBox ID="TxtPaingBegin" runat="server" Height="80px" TextMode="MultiLine"
                                    Width="80%"></asp:TextBox>
                                <br />
                                “下一页”URL结束代码：<br />
                                <asp:TextBox ID="TxtPaingEnd" runat="server" Height="80px" TextMode="MultiLine" Width="80%"></asp:TextBox>
                                <br />
                                <input type="button" value="测试下一页" onclick="testPaing()" class="inputbutton" />
                            </td>
                        </tr>
                        <tr class="tdbg" id="ListPaing2" style="display: none">
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
                        <tr class="tdbg" style="display: none" id="ListPaing3">
                            <td style="height: 40%" valign="top">
                                URL列表：<br />
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
    <br />
    <center>
        <asp:Button ID="BtnCancel1" runat="server" Text="上一步" OnClick="BtnCancel1_Click"
            Visible="false" ValidationGroup="BtnCancel1" />&nbsp;&nbsp;<asp:Button ID="BtnSubmit"
                Text="下一步" OnClick="EBtnSubmit_Click" runat="server" />&nbsp;&nbsp;<input name="Cancel"
                    type="button" class="inputbutton" id="Cancel2" value="返回采集管理" onclick="Redirect('ItemManage.aspx')" />
        <asp:HiddenField ID="HdnAction" runat="server" />
        <asp:HiddenField ID="HdnTestContent" runat="server" />
        <input id="url" type="hidden" value="<%=m_Url%>" />
        <input id="codeType" type="hidden" value="<%=m_CodeType%>" />
        <input id="listBegin" type="hidden" value="" />
        <input id="listEnd" type="hidden" value="" />
        <input id="linkBegin" type="hidden" value="" />
        <input id="linkEnd" type="hidden" value="" />
        <input id="testContent" type="hidden" value="" />
        <input id="paingBegin" type="hidden" value="" />
        <input id="paingEnd" type="hidden" value="" />
        <input id="ListPaing0" type="hidden" value="" />
        <input id="paingBegin2" type="hidden" value="" />
        <input id="paingEnd2" type="hidden" value="" />
        <input id="linkBegin2" type="hidden" value="" />
        <input id="linkEnd2" type="hidden" value="" />
        
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
        
        function ShowContent()
        {
             $("<%=TxtShowCode.ClientID%>").value = $("<%=HdnTestContent.ClientID%>").value;
        }
    
        function testList()
        {
            if($("<%=TxtListBegin.ClientID%>").value == '')
            {
                alert("列表设置开始代码不能为空！");
                $("<%=TxtListBegin.ClientID%>").focus();
                return false; 
            }
            else
            {
                 $("listBegin").value = $("<%=TxtListBegin.ClientID%>").value;
            }
            
            if($("<%=TxtListEnd.ClientID%>").value == '')
            {
                alert("列表设置结束代码不能为空！");
                $("<%=TxtListEnd.ClientID%>").focus();
                return false; 
            }
            else
            {
                 $("listEnd").value = $("<%=TxtListEnd.ClientID%>").value;
            }
                        
            var x = new AjaxRequest('XML','testContent');
                x.para = ['url', 'codeType','listBegin','listEnd'];
                x.post('testList', 'ajax.aspx', function(s) {
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
        
        function testLink()
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
            
            if($("<%=TxtLinkBegin.ClientID%>").value == '')
            {
                alert("链接设置开始代码不能为空！");
                $("<%=TxtLinkBegin.ClientID%>").focus();
                return false; 
            }
            else
            {
                 $("linkBegin").value = $("<%=TxtLinkBegin.ClientID%>").value;
            }
            
            if($("<%=TxtLinkEnd.ClientID%>").value == '')
            {
                alert("链接设置结束代码不能为空！");
                $("<%=TxtLinkEnd.ClientID%>").focus();
                return false; 
            }
            else
            {
                 $("linkEnd").value = $("<%=TxtLinkEnd.ClientID%>").value;
            }
            
            var x = new AjaxRequest('XML','testContent');
                x.para = ['url','linkBegin','linkEnd','testContent'];
                x.post('testLink', 'ajax.aspx', function(s) {
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

        function ResumeError() {
            return true;
        }
        window.onerror = ResumeError;
        
    //-->
    </script>

</asp:Content>
