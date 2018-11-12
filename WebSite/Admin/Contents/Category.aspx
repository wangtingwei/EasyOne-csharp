<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    EnableEventValidation="false" Inherits="EasyOne.WebSite.Admin.Contents.Category"
    ValidateRequest="false" Title="栏目添加" Codebehind="Category.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <asp:ScriptManager ID="SmCategory" ScriptMode="release" runat="server" EnablePartialRendering="true">
        <Services>
            <asp:ServiceReference Path="~/WebServices/CategoryService.asmx" />
        </Services>
    </asp:ScriptManager>

    <script language="JavaScript" type="text/JavaScript">
        var tID=0;
        var arrTabTitle = new Array("TabTitle0","TabTitle1","TabTitle2","<%= TabTitle3.ClientID %>","TabTitle4","TabTitle5","<%= TabTitle6.ClientID %>","TabTitle7","TabTitle8");
        var arrTabs = new Array("Tabs0","Tabs1","Tabs2","Tabs3","Tabs4","Tabs5","Tabs6","Tabs7","Tabs8");
        function ShowTabs(ID)
        {
            if(ID!=tID)
            {
                document.getElementById(arrTabTitle[tID].toString()).className = "tabtitle";
                document.getElementById(arrTabTitle[ID].toString()).className = "titlemouseover";
                document.getElementById(arrTabs[tID].toString()).style.display = "none";
                document.getElementById(arrTabs[ID].toString()).style.display = "";
                tID=ID;
            }
        }
        function HideTabTitle(displayValue,tempType)
        {
            for (var i = 1; i < arrTabTitle.length; i++) 
            {
                if(tempType==0&&i==2) 
                {
                    document.getElementById(arrTabTitle[i].toString()).style.display="none";
                }
                else
                {
                    document.getElementById(arrTabTitle[i].toString()).style.display=displayValue;
                }
            }
        }
        function setFileFileds(num)
        {    
             var id = "<%=objFiles1.ClientID%>";
             id = id.replace("1","");
             for(var i=1,str='';i<=20;i++)
             {
                 document.getElementById(id + i.toString()).style.display="none";
             }
             for(var i=1,str='';i<=num;i++)
             {
                 document.getElementById(id + i.toString()).style.display="";
             }
        }

        function HidePermissionTitle(type)
        {
            if(type==0)
            {
                document.getElementById(arrTabTitle[6].toString()).style.display="none";
            }
            
            if(type==1)
            {
                document.getElementById(arrTabTitle[6].toString()).style.display="";
            }
        }
    </script>

    <script language="JavaScript" type="text/javascript">
    <!--
        function addItem(ItemList,Target)
        {
            for(var x = 0; x < ItemList.length; x++)
            {
                var opt = ItemList.options[x];
                if (opt.selected)
                {
                    flag = true;
                    for (var y=0;y<Target.length;y++)
                    {
                        var myopt = Target.options[y];
                        if (myopt.value == opt.value)
                        {
                            flag = false;
                        }
                    }
                    if(flag)
                    {
                        Target.options[Target.options.length] = new Option(opt.text, opt.value, 0, 0);
                    }
                }
            }
        }

        /**
         * move one selected option from a select.
         *
         * @author  Chunsheng Wang <wwccss@263.net>
         */
        function delItem(ItemList)
        {
            for(var x=ItemList.length-1;x>=0;x--)
            {
                var opt = ItemList.options[x];
                if (opt.selected)
                {
                    ItemList.options[x] = null;
                }
            }
        }
        
        function SelectedItem(ItemList)
        {
            var input = "";
            for(var x=ItemList.length-1;x>=0;x--)
            {
                var opt = ItemList.options[x];
                if(input=="")
                {
                    input = opt.value;
                }
                else
                {
                    input = input + "," + opt.value;
                }
            }
            document.getElementById("ListTemplateInput").value = input;
        }
        
        function ChangeElementValue(elementId,Value)
        {
            if(Value != "-1")
            {
                document.getElementById(elementId).value = Value;
            }
        }
        
        function ShowSelectRelation(type)
        {
            if(type=="4")
            {
                document.getElementById("<%= SelectRelation.ClientID %>").style.display = "";
            }
            else
            {
                document.getElementById("<%= SelectRelation.ClientID %>").style.display = "none";
            }
        }
        
        function SelectAll(elementId)
        {
            for(var i=0;i<document.getElementById(elementId).length;i++)
            {
                document.getElementById(elementId).options[i].selected=true;
            }
        }
        
        function UnSelectAll(elementId)
        {
            for(var i=0;i<document.getElementById(elementId).length;i++)
            {
                document.getElementById(elementId).options[i].selected=false;
            }
        }
    //-->
    </script>

    <script language="javascript" type="text/javascript">  
        function GetInitial()
        {
            if($get("<%= TxtNodeName.ClientID %>").value != "")
            {
                EasyOne.WebSite.Admin.Contents.CategoryService.GetInitial($get("<%= TxtNodeName.ClientID %>").value, onInitial);
            }
        }
        
        function onInitial(value)
        {
                $get("<%= TxtNodeIdentifier.ClientID %>").value = value;
                $get("<%= TxtNodeDir.ClientID %>").value = value;
        }
        
        
        function GetBatchInitial()
        {
            var nodeNames = $get("<%= TxtNodeNames.ClientID %>").value;
            if(nodeNames.value != "")
            {
                EasyOne.WebSite.Admin.Contents.CategoryService.GetInitial(nodeNames, onBatchInitial);
            }
        }
        
        function onBatchInitial(value)
        {
                $get("<%= TxtNodeIdentifiers.ClientID %>").value = value;
                $get("<%= TxtNodeDirs.ClientID %>").value = value;
        }
    </script>

    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr align="center">
            <td id="TabTitle0" class="titlemouseover" onclick="ShowTabs(0)">
                基本信息
            </td>
            <td id="TabTitle1" class="tabtitle" onclick="ShowTabs(1)">
                栏目选项
            </td>
            <td id="TabTitle2" class="tabtitle" onclick="ShowTabs(2)">
                模板选项
            </td>
            <td id="TabTitle3" class="tabtitle" onclick="ShowTabs(3)" runat="server">
                收费设置
            </td>
            <td id="TabTitle4" class="tabtitle" onclick="ShowTabs(4)">
                前台样式
            </td>
            <td id="TabTitle5" class="tabtitle" onclick="ShowTabs(5)">
                生成选项
            </td>
            <td id="TabTitle6" class="tabtitle" runat="server" onclick="ShowTabs(6)">
                权限设置
            </td>
            <td id="TabTitle7" class="tabtitle" onclick="ShowTabs(7)">
                自设内容
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <table width="100%" border="0" cellpadding="5" cellspacing="1" class="border">
        <tr class="tdbg">
            <td valign="top" style="margin-top: 0px; margin-left: 0px;">
                <table width="100%" cellpadding="2" cellspacing="1" style="background-color: white;">
                    <%-- 基本信息--%>
                    <tbody id="Tabs0">
                        <tr id="TrNodeId" class="tdbg" runat="server" visible="false">
                            <td style="width: 300" class="tdbgleft">
                                <strong>栏目ID：</strong>
                            </td>
                            <td>
                                <span style="color: Red">
                                    <asp:Literal runat="server" ID="LitNodeId"></asp:Literal></span>
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td style="width: 300" class="tdbgleft">
                                <strong>所属栏目：</strong>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="DropParentNode" DataValueField="NodeId" DataTextField="NodeName"
                                            runat="server" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="DropParentNode_SelectedIndexChanged">
                                            <asp:ListItem Text="无" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                        <pe:ExtendedLabel HtmlEncode="false" ID="LblNodePermissions" runat="server" Text=""></pe:ExtendedLabel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <pe:ExtendedLabel HtmlEncode="false" ID="LblNodeName" runat="server" Text=""></pe:ExtendedLabel>
                                <asp:HiddenField ID="HdnNodeId" Value="0" runat="server" />
                            </td>
                        </tr>
                        <tr class="tdbg" runat="server" id="NodeName">
                            <td style="width: 300" class="tdbgleft">
                                <strong>栏目名称：</strong>
                            </td>
                            <td>
                                <asp:TextBox ID="TxtNodeName" runat="server" Columns="30" MaxLength="200"></asp:TextBox>
                                <pe:RequiredFieldValidator ID="ValrNodeNameTab0" runat="server" ErrorMessage="栏目名称不能为空！"
                                    ControlToValidate="TxtNodeName" Display="Dynamic" SetFocusOnError="True"></pe:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr class="tdbg" runat="server" id="NodeIdentifier">
                            <td class="tdbgleft" style="width: 300">
                                <strong>栏目标识符：</strong><br />
                                用于前台调用时可以直接用标识符取代ID
                            </td>
                            <td>
                                <asp:TextBox ID="TxtNodeIdentifier" runat="server" Columns="30"></asp:TextBox>
                                <pe:RequiredFieldValidator ID="ValrNodeIdentifierTab0" runat="server" ErrorMessage="标识符不能为空！"
                                    ControlToValidate="TxtNodeIdentifier" Display="Dynamic" SetFocusOnError="True"></pe:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr class="tdbg" runat="server" id="NodeDir">
                            <td style="width: 300" class="tdbgleft">
                                <strong>栏目的目录名：</strong><br />
                            </td>
                            <td>
                                <asp:TextBox ID="TxtNodeDir" MaxLength="20" runat="server" Columns="20"></asp:TextBox>
                                <span style="color: Blue">注意，目录名只能是字母、数字、下划线组成<pe:RequiredFieldValidator ID="Valr"
                                    ControlToValidate="TxtNodeDir" Display="Dynamic" ErrorMessage="栏目的目录名不能为空！" SetFocusOnError="True"
                                    runat="server"></pe:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegTxtNodeDir" runat="server" ControlToValidate="TxtNodeDir"
                                        Display="Dynamic" ValidationExpression="[_a-zA-Z0-9]*" ErrorMessage="目录名格式错误"
                                        SetFocusOnError="True"></asp:RegularExpressionValidator></span><br />
                            </td>
                        </tr>
                        <tr class="tdbg" runat="server" id="BatchCategory" visible="false">
                            <td style="width: 300" class="tdbgleft">
                                <strong>批量添加栏目：</strong><br />
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td align="center">
                                            栏目名称
                                        </td>
                                        <td align="center">
                                            栏目标识符
                                        </td>
                                        <td align="center">
                                            栏目的目录名
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="TxtNodeNames" MaxLength="255" runat="server" Columns="50" Height="180px"
                                                TextMode="MultiLine" Width="135px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtNodeIdentifiers" MaxLength="255" runat="server" Columns="50"
                                                Height="180px" TextMode="MultiLine" Width="135px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtNodeDirs" MaxLength="255" runat="server" Columns="50" Height="180px"
                                                TextMode="MultiLine" Width="135px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <pe:RequiredFieldValidator ID="ValrNodeNameTab1" runat="server" ErrorMessage="栏目名称不能为空！"
                                    ControlToValidate="TxtNodeNames" Display="Dynamic" SetFocusOnError="True" RequiredText=""></pe:RequiredFieldValidator>
                                <pe:RequiredFieldValidator ID="ValrNodeIdentifierTab1" runat="server" ErrorMessage="标识符不能为空！"
                                    ControlToValidate="TxtNodeIdentifiers" Display="Dynamic" SetFocusOnError="True"
                                    RequiredText=""></pe:RequiredFieldValidator>
                                <pe:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="TxtNodeDirs"
                                    Display="Dynamic" ErrorMessage="栏目的目录名不能为空！" SetFocusOnError="True" runat="server"
                                    RequiredText=""></pe:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegTxtNodeDir1" runat="server" ControlToValidate="TxtNodeDirs"
                                    Display="Dynamic" ValidationExpression="[_a-zA-Z\r\n][_a-zA-Z0-9\r\n]*" ErrorMessage="目录名格式错误"
                                    SetFocusOnError="True"></asp:RegularExpressionValidator>
                                <span style="color: Blue">注：一行填写一个栏目信息，并且目录名只能是字母、数字、下划线组成，首字符不能是数字。</span>
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td style="width: 300" class="tdbgleft">
                                <strong>栏目图片地址：</strong><br />
                                用于在栏目页显示指定的图片
                            </td>
                            <td>
                                <asp:TextBox ID="TxtNodePicUrl" MaxLength="255" runat="server" Columns="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td style="width: 300" class="tdbgleft">
                                <strong>栏目提示：</strong><br />
                                鼠标移至栏目名称上时将显示设定的提示文字（不支持HTML）
                            </td>
                            <td>
                                <asp:TextBox ID="TxtTips" runat="server" Columns="60" Height="30" Width="500" Rows="2"
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td style="width: 300" class="tdbgleft">
                                <strong>栏目说明：</strong><br />
                                用于在栏目页详细介绍栏目信息，支持HTML
                            </td>
                            <td>
                                <asp:TextBox ID="TxtDescription" runat="server" Columns="60" Height="30" Width="500"
                                    Rows="2" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td style="width: 300" class="tdbgleft">
                                <strong>栏目META关键词：</strong><br />
                                针对搜索引擎设置的关键词<br />
                                多个关键词请用,号分隔
                            </td>
                            <td>
                                <asp:TextBox ID="TxtMetaKeywords" runat="server" Height="65" Width="500" Columns="60"
                                    Rows="4" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td style="width: 300" class="tdbgleft">
                                <strong>栏目META网页描述：</strong><br />
                                针对搜索引擎设置的网页描述<br />
                                多个描述请用,号分隔
                            </td>
                            <td>
                                <asp:TextBox ID="TxtMetaDescription" runat="server" Height="65" Width="500" Columns="60"
                                    Rows="4" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                    </tbody>
                    <%--栏目选项--%>
                    <tbody id="Tabs1" style="display: none">
                        <tr class="tdbg">
                            <td style="width: 300" class="tdbgleft">
                                <strong>打开方式：</strong>
                            </td>
                            <td>
                                <asp:RadioButton ID="RadOpenType0" Checked="true" GroupName="OpenType" runat="server" />在原窗口打开&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:RadioButton ID="RadOpenType1" GroupName="OpenType" runat="server" />在新窗口打开
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td style="width: 300;" class="tdbgleft">
                                <strong>栏目类型：</strong><br />
                                <div style="color: Red; width: 300px;">
                                    栏目权限为继承关系。 例如：当父栏目设为“认证栏目”时，子栏目的权限设置将继承父栏目设置， 即使子栏目设为“开放栏目”也无效。相反，如果父栏目设为“开放栏目”，
                                    子栏目可以设为“半开放栏目”或“认证栏目”。</div>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpPurviewType" runat="server">
                                    <ContentTemplate>
                                        <table width="100%" border="0" cellpadding="2" cellspacing="1">
                                            <tr>
                                                <td style="width: 20%;" valign="top">
                                                    <asp:RadioButton ID="RadPurviewType0" AutoPostBack="true" GroupName="PurviewType"
                                                        runat="server" OnCheckedChanged="RadPurviewType_CheckedChanged" />开放栏目
                                                </td>
                                                <td>
                                                    任何人（包括游客）可以浏览和查看此栏目下的信息。
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 20%;" valign="top">
                                                    <asp:RadioButton ID="RadPurviewType1" AutoPostBack="true" GroupName="PurviewType"
                                                        runat="server" OnCheckedChanged="RadPurviewType_CheckedChanged" />半开放栏目
                                                </td>
                                                <td>
                                                    任何人（包括游客）都可以浏览。游客不可查看，其他会员根据会员组的栏目权限设置决定是否可以查看。
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 20%;" valign="top">
                                                    <asp:RadioButton ID="RadPurviewType2" AutoPostBack="true" GroupName="PurviewType"
                                                        runat="server" OnCheckedChanged="RadPurviewType_CheckedChanged" />认证栏目
                                                </td>
                                                <td>
                                                    游客不能浏览和查看，其他会员根据会员组的栏目权限设置决定是否可以浏览和查看。
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td style="width: 300" class="tdbgleft">
                                <strong>评论权限：</strong>
                            </td>
                            <td>
                                <span onclick="javascript:SetEnableComment();" id="SetEnableComment">
                                    <asp:CheckBox ID="ChkEnableComment" runat="server" /></span>允许在此栏目发表评论&nbsp;&nbsp;<span
                                        onclick="javascript:SetEnableTouristsComment();" id="SetEnableTouristsComment"><asp:CheckBox
                                            ID="ChkEnableTouristsComment" runat="server" />允许游客在此栏目发表评论</span>
                                <br />
                                <asp:CheckBox ID="ChkCommentNeedCheck" runat="server" />评论需要审核
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td style="width: 300" class="tdbgleft">
                                <strong>工作流：</strong>
                            </td>
                            <td>
                                <asp:DropDownList ID="DropWorkFlow" DataTextField="FlowName" DataValueField="FlowId"
                                    runat="server" AppendDataBoundItems="true">
                                    <asp:ListItem Text="请选择" Value="-1"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td style="width: 300" class="tdbgleft">
                                <strong>是否启用此栏目的防止复制、防盗链功能：</strong>
                            </td>
                            <td>
                                <asp:RadioButtonList ID="RadlEnableProtect" RepeatLayout="Flow" RepeatDirection="Horizontal"
                                    runat="server">
                                    <asp:ListItem Text="是&nbsp;&nbsp;&nbsp;&nbsp;" Selected="True" Value="True"></asp:ListItem>
                                    <asp:ListItem Text="否" Value="False"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td style="width: 300" class="tdbgleft">
                                <strong>有子栏目时是否可以在此栏目添加内容：</strong>
                            </td>
                            <td>
                                <asp:RadioButtonList ID="RadlEnableAddWhenHasChild" RepeatLayout="Flow" RepeatDirection="Horizontal"
                                    runat="server">
                                    <asp:ListItem Text="是&nbsp;&nbsp;&nbsp;&nbsp;" Selected="True" Value="True"></asp:ListItem>
                                    <asp:ListItem Text="否" Value="False"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td style="width: 300" class="tdbgleft">
                                <strong>本栏目热点的点击数最小值：</strong>
                            </td>
                            <td>
                                <asp:TextBox ID="TxtHitsOfHot" runat="server" Columns="5"></asp:TextBox>
                                <asp:RangeValidator ID="ValgHitsOfHot" runat="server" ControlToValidate="TxtHitsOfHot"
                                    ErrorMessage="请输入整数" MinimumValue="0" MaximumValue="2147483647" Type="Integer"
                                    SetFocusOnError="True"></asp:RangeValidator>
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td style="width: 300" class="tdbgleft">
                                <strong>是否缓存本节点的首页HTML：</strong>
                            </td>
                            <td>
                                <asp:RadioButton ID="RadNeedCache1" runat="server" GroupName="NeedCache" Text="是" />&nbsp;&nbsp;
                                &nbsp;<asp:RadioButton ID="RadNeedCache0" runat="server" GroupName="NeedCache" Text="否"
                                    Checked="True" />
                            </td>
                        </tr>
                        <tr id="TrSetCacheTime" class="tdbg" style="display: none">
                            <td style="width: 300" class="tdbgleft">
                                <strong>设置缓存更新时间为：</strong>
                            </td>
                            <td>
                                <asp:TextBox ID="TxtCacheTime" runat="server" Columns="5"></asp:TextBox>分钟
                                <asp:RangeValidator ID="ValgCacheTime" runat="server" ControlToValidate="TxtCacheTime"
                                    ErrorMessage="请输入有效实数" MinimumValue="0" MaximumValue="2147483647" Type="Double"
                                    SetFocusOnError="True"></asp:RangeValidator>
                            </td>
                        </tr>
                    </tbody>
                    <%--模板选项--%>
                    <tbody id="Tabs2" style="display: none">
                        <tr class="tdbg">
                            <td align="left" colspan="2">
                                <table width="100%">
                                    <tr>
                                        <td align="left" class="tdbgleft" style="width: 20%">
                                            <strong>栏目列表页模板：</strong>
                                        </td>
                                        <td align="left" style="width: 80%">
                                            <pe:TemplateSelectControl ID="FileContainChildTemplate" Width="300px" runat="server"></pe:TemplateSelectControl>
                                            <pe:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                                                SetFocusOnError="true" ErrorMessage="栏目列表页模板不能为空！" ControlToValidate="FileContainChildTemplate"></pe:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td align="left" colspan="2">
                                <table width="100%">
                                    <tr>
                                        <td align="left" class="tdbgleft" style="width: 20%">
                                            <strong>栏目首页模板：</strong>
                                            <br />
                                            如果不设置栏目首页模板，则自动使用栏目列表页模板。
                                        </td>
                                        <td align="left" style="width: 80%">
                                            <pe:TemplateSelectControl ID="FileCdefaultListTmeplate" Width="300px" runat="server"></pe:TemplateSelectControl>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td>
                                <asp:Repeater ID="RepContentModelTemplate" runat="server" OnItemDataBound="RepModelTemplate_ItemDataBound">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr class="tdbg">
                                                <td align="left">
                                                    <strong>选择内容模型</strong>
                                                </td>
                                                <td align="left">
                                                    <strong>选择内容模型对应的内容页模板</strong>
                                                </td>
                                            </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td class="tdbgleft">
                                                <asp:CheckBox ID="ChkModel" Text='<%# Eval("ModelName") %>' runat="server" /><asp:HiddenField
                                                    ID="HdnModelId" runat="server" Value='<%# Eval("ModelId") %>' />
                                            </td>
                                            <td style="width: 65%" align="left">
                                                <pe:TemplateSelectControl ID="FileCTemplate" Width="300px" runat="server"></pe:TemplateSelectControl>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </table></FooterTemplate>
                                </asp:Repeater>
                                <asp:Repeater ID="RepShopModelTemplate" runat="server" OnItemDataBound="RepModelTemplate_ItemDataBound">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr class="tdbg">
                                                <td align="left">
                                                    <strong>选择商品模型：</strong>
                                                </td>
                                                <td align="left" style="width: 65%">
                                                    <strong>选择商品模型对应的商品页模板</strong>
                                                </td>
                                            </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td class="tdbgleft">
                                                <asp:CheckBox ID="ChkModel" Text='<%# Eval("ModelName") %>' runat="server" /><asp:HiddenField
                                                    ID="HdnModelId" runat="server" Value='<%# Eval("ModelId") %>' />
                                            </td>
                                            <td style="width: 65%" align="left">
                                                <pe:TemplateSelectControl ID="FileCTemplate" Width="300px" runat="server"></pe:TemplateSelectControl>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </table></FooterTemplate>
                                </asp:Repeater>
                            </td>
                        </tr>
                    </tbody>
                    <%--收费设置--%>
                    <tbody id="Tabs3" style="display: none">
                        <tr class="tdbg">
                            <td style="width: 300" class="tdbgleft">
                                <strong>积分奖励：</strong><br />
                                <span style="color: Green;">注：</span> 会员在此栏目发表信息时可以得到的积分
                            </td>
                            <td>
                                会员在此栏目每发表一条信息，可以得到
                                <asp:TextBox ID="TxtPresentExp" runat="server" Columns="5"></asp:TextBox>分积分<asp:RangeValidator
                                    ID="ValgPresentExp" runat="server" ControlToValidate="TxtPresentExp" ErrorMessage="请输入整数"
                                    MaximumValue="2147483647" MinimumValue="0" SetFocusOnError="True" Type="Integer"></asp:RangeValidator>
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td style="width: 300" class="tdbgleft">
                                <strong>默认消费<pe:ShowPointName ID="ShowPointName" runat="server" />数：</strong><br />
                                <span style="color: Green;">注：</span> 会员在此栏目下阅读内容时，该内容默认的收费<pe:ShowPointName ID="ShowPointName1"
                                    runat="server" />数
                            </td>
                            <td>
                                <asp:TextBox ID="TxtDefaultItemPoint" runat="server" Columns="5"></asp:TextBox><pe:ShowPointName
                                    ID="ShowPointName2" PointType="PointUnit" runat="server"></pe:ShowPointName><asp:RangeValidator
                                        ID="ValgDefaultItemPoint" runat="server" ControlToValidate="TxtDefaultItemPoint"
                                        ErrorMessage="请输入整数" MaximumValue="2147483647" MinimumValue="0" SetFocusOnError="True"
                                        Type="Integer"></asp:RangeValidator>
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td style="width: 300" class="tdbgleft">
                                <strong>默认重复收费：</strong><br />
                                <span style="color: Green;">注：</span> 会员在此栏目下阅读内容时，该内容默认的重复收费方式
                            </td>
                            <td>
                                <pe:ShowChargeType ID="ShowChargeType" runat="server" />
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td style="width: 300" class="tdbgleft">
                                <strong>默认分成比例：</strong><br />
                                <span style="color: Green;">注：</span> 会员在此栏目下添加内容时，该内容默认的分成比例
                            </td>
                            <td>
                                <asp:TextBox ID="TxtDefaultItemDividePercent" runat="server" Columns="5"></asp:TextBox>
                                %
                            </td>
                        </tr>
                    </tbody>
                    <%-- 前台样式--%>
                    <tbody id="Tabs4" style="display: none">
                        <tr class="tdbg">
                            <td style="width: 50%" class="tdbgleft">
                                <strong>是否在顶部菜单处显示：</strong><br />
                                此选项只对一级栏目有效。
                            </td>
                            <td>
                                <asp:RadioButtonList ID="RadlShowOnMenu" RepeatLayout="Flow" RepeatDirection="Horizontal"
                                    runat="server">
                                    <asp:ListItem Text="是&nbsp;&nbsp;&nbsp;&nbsp;" Selected="True" Value="True"></asp:ListItem>
                                    <asp:ListItem Text="否" Value="False"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td style="width: 50%" class="tdbgleft">
                                <strong>是否位置导航处显示：</strong>
                            </td>
                            <td>
                                <asp:RadioButtonList ID="RadlShowOnPath" RepeatLayout="Flow" RepeatDirection="Horizontal"
                                    runat="server">
                                    <asp:ListItem Text="是&nbsp;&nbsp;&nbsp;&nbsp;" Selected="True" Value="True"></asp:ListItem>
                                    <asp:ListItem Text="否" Value="False"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td style="width: 50%" class="tdbgleft">
                                <strong>是否在网站地图（栏目导航）处显示：</strong>
                            </td>
                            <td>
                                <asp:RadioButtonList ID="RadlShowOnMap" RepeatLayout="Flow" RepeatDirection="Horizontal"
                                    runat="server">
                                    <asp:ListItem Text="是&nbsp;&nbsp;&nbsp;&nbsp;" Selected="True" Value="True"></asp:ListItem>
                                    <asp:ListItem Text="否" Value="False"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td style="width: 50%" class="tdbgleft">
                                <strong>是否在首页的分类列表处显示：</strong><br />
                                此选项只对一级栏目有效。如果一级栏目比较多，但首页不想显示太多的分类列表，这个选项就非常有用。
                            </td>
                            <td>
                                <asp:RadioButtonList ID="RadlShowOnListIndex" RepeatLayout="Flow" RepeatDirection="Horizontal"
                                    runat="server">
                                    <asp:ListItem Text="是&nbsp;&nbsp;&nbsp;&nbsp;" Selected="True" Value="True"></asp:ListItem>
                                    <asp:ListItem Text="否" Value="False"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td style="width: 50%" class="tdbgleft">
                                <strong>是否在父栏目的分类列表处显示：</strong><br />
                                如果某栏目下有几十个子栏目，但只想显示其中几个子栏目的文章列表，这个选项就非常有用。
                            </td>
                            <td>
                                <asp:RadioButtonList ID="RadlShowOnListParent" RepeatLayout="Flow" RepeatDirection="Horizontal"
                                    runat="server">
                                    <asp:ListItem Text="是&nbsp;&nbsp;&nbsp;&nbsp;" Selected="True" Value="True"></asp:ListItem>
                                    <asp:ListItem Text="否" Value="False"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td style="width: 50%" class="tdbgleft">
                                <strong>每页显示的内容数：</strong><br />
                                当此栏目为最下一级栏目时，则会分页显示此栏目中的内容，这里指定的是每页显示的内容数。
                            </td>
                            <td>
                                <pe:ComboBox ID="CombItemPageSize" Value="20" runat="server">
                                    <Items>
                                        <asp:ListItem>10</asp:ListItem>
                                        <asp:ListItem>20</asp:ListItem>
                                        <asp:ListItem>30</asp:ListItem>
                                        <asp:ListItem>40</asp:ListItem>
                                        <asp:ListItem>50</asp:ListItem>
                                    </Items>
                                </pe:ComboBox>
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td style="width: 50%" class="tdbgleft">
                                <strong>此栏目下的内容列表的排序方式：</strong>
                            </td>
                            <td>
                                <asp:DropDownList ID="DrpItemListOrderType" runat="server">
                                    <asp:ListItem Value="1" Selected="True" Text="按ID降序"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="按ID升序"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="按更新时间降序"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="按更新时间升序"></asp:ListItem>
                                    <asp:ListItem Value="5" Text="按点击数降序"></asp:ListItem>
                                    <asp:ListItem Value="6" Text="按点击数升序"></asp:ListItem>
                                    <asp:ListItem Value="7" Text="按评论数降序"></asp:ListItem>
                                    <asp:ListItem Value="8" Text="按评论数升序"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td style="width: 50%" class="tdbgleft">
                                <strong>此栏目下的内容打开方式：</strong>
                            </td>
                            <td>
                                <asp:DropDownList ID="DrpItemOpenType" runat="server">
                                    <asp:ListItem Text="在新窗口打开" Value="1" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="在原窗口打开" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </tbody>
                    <%--生成选项--%>
                    <tbody id="Tabs5" style="display: none">
                        <tr class="tdbg">
                            <td class="tdbgleft">
                                <strong>列表页生成HTML：</strong><br />
                                <br />
                                请谨慎选择！以后在每一次更改生成方式前， 你最好先删除所有以前生成的文件， 然后在保存参数后再重新生成所有文件。
                            </td>
                            <td>
                                <asp:RadioButtonList ID="RadlIsListPageCreate" RepeatDirection="horizontal" runat="server">
                                    <asp:ListItem Value="false" Selected="True">不生成</asp:ListItem>
                                    <asp:ListItem Value="true">生成</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td class="tdbgleft">
                                <strong>栏目列表文件的存放位置：</strong><br />
                            </td>
                            <td>
                                <asp:RadioButtonList ID="RadlListPageHtmlDirType" runat="server">
                                    <asp:ListItem Selected="true" Value="NodePath">列表文件分目录保存在所属栏目的文件夹中<br/><span id="ListPage1" style="color: blue"></span></asp:ListItem>
                                    <asp:ListItem Value="ListPath">列表文件统一保存在指定的“List”文件夹中<br/><span id="ListPage2" style="color: blue"></span></asp:ListItem>
                                    <asp:ListItem Value="RootPath">列表文件统一保存在一级栏目文件夹中<br/><span id="ListPage3" style="color: blue"></span></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td class="tdbgleft">
                                <strong>栏目列表文件的文件扩展名：</strong><br />
                            </td>
                            <td>
                                <pe:ComboBox ID="PagePostfix" runat="server">
                                    <Items>
                                        <asp:ListItem>html</asp:ListItem>
                                        <asp:ListItem>htm</asp:ListItem>
                                        <asp:ListItem>shtml</asp:ListItem>
                                        <asp:ListItem>shtm</asp:ListItem>
                                    </Items>
                                </pe:ComboBox>
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td class="tdbgleft">
                                <strong>内容页生成HTML：</strong>
                                <br />
                                <br />
                                请谨慎选择！以后在每一次更改生成方式前，你最好先删除所有以前生成的文件， 然后在保存参数后再重新生成所有文件。
                            </td>
                            <td>
                                <asp:RadioButtonList ID="RadlIsContentPageCreate" RepeatDirection="horizontal" runat="server">
                                    <asp:ListItem Value="false" Selected="True">不生成</asp:ListItem>
                                    <asp:ListItem Value="true">生成</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td class="tdbgleft">
                                <strong>内容页的文件名规则：</strong><br />
                                <span style="color: Blue">注意：</span> 可用标签为 {$CategoryDir}、{$Year}、{$Month}、{$Day}、{$InfoID}、{$pinyinOfTitle}
                            </td>
                            <td>
                                <table border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td colspan="5">
                                            生成路径示例： <span id="ContentHtmlDir" style="color: Blue;"></span><span id="ContentHtmlFile"
                                                style="color: Blue;"></span><span id="ContentHtmlExt" style="color: Blue;"></span>
                                            <input id="ExampleRule" type="hidden" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <pe:ComboBox ID="TxtContentHtmlDir" Attribute="ShowContentHtmlDir(this.value);" Width="250px"
                                                runat="server">
                                                <Items>
                                                    <asp:ListItem>{$CategoryDir}/{$Year}/{$Month}/{$Day}</asp:ListItem>
                                                    <asp:ListItem>{$Year}/{$Month}/{$Day}</asp:ListItem>
                                                </Items>
                                            </pe:ComboBox>
                                        </td>
                                        <td>
                                            /
                                        </td>
                                        <td>
                                            <pe:ComboBox ID="TxtContentHtmlFile" Attribute="ShowContentHtmlFile(this.value);"
                                                Width="150px" runat="server">
                                                <Items>
                                                    <asp:ListItem>{$Time}{$InfoId}</asp:ListItem>
                                                    <asp:ListItem>{$InfoId}</asp:ListItem>
                                                    <asp:ListItem>{$pinyinOfTitle}</asp:ListItem>
                                                </Items>
                                            </pe:ComboBox>
                                        </td>
                                        <td>
                                            .
                                        </td>
                                        <td>
                                            <pe:ComboBox ID="TxtContentHtmlExt" Attribute="ShowContentHtmlExt(this.value);" runat="server">
                                                <Items>
                                                    <asp:ListItem>html</asp:ListItem>
                                                    <asp:ListItem>htm</asp:ListItem>
                                                    <asp:ListItem>shtml</asp:ListItem>
                                                    <asp:ListItem>shtm</asp:ListItem>
                                                </Items>
                                            </pe:ComboBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td class="tdbgleft">
                                <strong>自动生成HTML时的生成方式：</strong><br />
                                <br />
                                添加/修改信息时，系统可以自动生成有关页面文件， 请在这里选择自动生成时的方式。
                            </td>
                            <td>
                                <asp:RadioButtonList ID="RadlAutoCreateHtmlType" runat="server">
                                    <asp:ListItem Value="None" Selected="True">手动生成，由管理员在生成管理手动生成全部所需的页面</asp:ListItem>
                                    <asp:ListItem Value="Content">只自动生成内容页</asp:ListItem>
                                    <asp:ListItem Value="ContentAndNode">自动生成内容页和所属栏目的列表页</asp:ListItem>
                                    <asp:ListItem Value="ContentAndNodeAndParentNode">自动生成内容页和所属栏目及父栏目的列表页</asp:ListItem>
                                    <asp:ListItem Value="ContentAndNodeAndParentNodeAndSpecial">自动生成内容页和所属栏目及父栏目的列表页以及自动关联的专题页</asp:ListItem>
                                    <asp:ListItem Value="ContentAndRelatedNode">自动生成所有关联的页（在发表、更新文章时，除了自动生成内容页和所属栏目及父栏目的列表页以外，还会自动会生成指定栏目的列表页）。</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr class="tdbg" runat="server" id="SelectRelation" style="display: none">
                            <td class="tdbgleft" style="width: 200px">
                                <strong>关联</strong>
                            </td>
                            <td>
                                <div style="float: right">
                                    关联专题<br />
                                    <asp:ListBox SelectionMode="Multiple" ID="LstRelationSpecial" DataTextField="SpecialName"
                                        DataValueField="SpecialId" runat="server" Height="251px" Width="200px"></asp:ListBox>
                                    <br />
                                    <input type="button" class="inputbutton" onclick="SelectAll('<%= LstRelationSpecial.ClientID %>')"
                                        value="选择所有" />
                                    <input type="button" class="inputbutton" onclick="UnSelectAll('<%= LstRelationSpecial.ClientID %>')"
                                        value="取消选择" /></div>
                                <div style="float: left">
                                    关联栏目<br />
                                    <asp:ListBox SelectionMode="Multiple" ID="LstRelationNodes" DataTextField="NodeName"
                                        DataValueField="NodeId" runat="server" Height="251px" Width="200px"></asp:ListBox>
                                    <br />
                                    <input type="button" class="inputbutton" onclick="SelectAll('<%= LstRelationNodes.ClientID %>')"
                                        value="选择所有" />
                                    <input type="button" class="inputbutton" onclick="UnSelectAll('<%= LstRelationNodes.ClientID %>')"
                                        value="取消选择" /></div>
                            </td>
                        </tr>
                    </tbody>
                    <tbody id="Tabs6" style="display: none">
                        <tr class="tdbg">
                            <td colspan="2">
                                <table width="100%" border="0" cellspacing="1">
                                    <tr class="tdbg">
                                        <td class="tdbgleft" style="width: 200px">
                                            <strong>会员组权限： </strong>
                                        </td>
                                        <td id="TdGroupPermissions" runat="server">
                                            <asp:UpdatePanel ID="UpPermissions" runat="server">
                                                <ContentTemplate>
                                                    <pe:ExtendedGridView ID="EgvPermissions" runat="server" AutoGenerateColumns="False"
                                                        DataKeyNames="GroupId" OnRowDataBound="EgvPermissions_RowDataBound">
                                                        <Columns>
                                                            <pe:BoundField HeaderText="会员组名" DataField="GroupName" />
                                                            <pe:TemplateField HeaderText="浏览">
                                                                <HeaderStyle Width="15%" />
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="ChkNodeSkim" runat="server"></asp:CheckBox>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </pe:TemplateField>
                                                            <pe:TemplateField HeaderText="查看">
                                                                <HeaderStyle Width="15%" />
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="ChkNodeShow" runat="server" />
                                                                </ItemTemplate>
                                                            </pe:TemplateField>
                                                            <pe:TemplateField HeaderText="录入">
                                                                <HeaderStyle Width="15%" />
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="ChkNodeInput" runat="server" />
                                                                </ItemTemplate>
                                                            </pe:TemplateField>
                                                        </Columns>
                                                    </pe:ExtendedGridView>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr class="tdbg">
                                        <td class="tdbgleft">
                                            <strong>角色权限： </strong>
                                        </td>
                                        <td id="TdRolePermissions" runat="server">
                                            <pe:ExtendedGridView ID="EgvRoleView" runat="server" AutoGenerateColumns="False"
                                                DataKeyNames="RoleId" OnRowDataBound="EgvRoleView_RowDataBound">
                                                <Columns>
                                                    <pe:BoundField HeaderText="角色名" DataField="RoleName" />
                                                    <pe:TemplateField HeaderText="查看">
                                                        <HeaderStyle Width="10%" />
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="ChkNodePreview" runat="server" />
                                                        </ItemTemplate>
                                                    </pe:TemplateField>
                                                    <pe:TemplateField HeaderText="录入">
                                                        <HeaderStyle Width="10%" />
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="ChkNodeInput" runat="server" />
                                                        </ItemTemplate>
                                                    </pe:TemplateField>
                                                    <pe:TemplateField HeaderText="审核">
                                                        <HeaderStyle Width="10%" />
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="ChkNodeCheck" runat="server"></asp:CheckBox>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </pe:TemplateField>
                                                    <pe:TemplateField HeaderText="信息管理">
                                                        <HeaderStyle Width="15%" />
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="ChkContentManage" runat="server"></asp:CheckBox>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </pe:TemplateField>
                                                    <pe:TemplateField HeaderText="当前栏目管理">
                                                        <HeaderStyle Width="15%" />
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="ChkNodeManage" runat="server"></asp:CheckBox>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </pe:TemplateField>
                                                    <pe:TemplateField HeaderText="评论管理">
                                                        <HeaderStyle Width="15%" />
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="ChkCommentManage" runat="server"></asp:CheckBox>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </pe:TemplateField>
                                                </Columns>
                                            </pe:ExtendedGridView>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                    <%--自设内容--%>
                    <tbody id="Tabs7" style="display: none">
                        <asp:Panel ID="PnlCustomFileds" runat="server">
                            <tr class="tdbg">
                                <td style="width: 150" class="tdbgleft">
                                    <strong>自设内容项目数：</strong>
                                </td>
                                <td>
                                    <asp:DropDownList ID="DropCustomNum" runat="server">
                                        <asp:ListItem Value="1">1</asp:ListItem>
                                        <asp:ListItem Value="2">2</asp:ListItem>
                                        <asp:ListItem Value="3">3</asp:ListItem>
                                        <asp:ListItem Value="4">4</asp:ListItem>
                                        <asp:ListItem Value="5">5</asp:ListItem>
                                        <asp:ListItem Value="6">6</asp:ListItem>
                                        <asp:ListItem Value="7">7</asp:ListItem>
                                        <asp:ListItem Value="8">8</asp:ListItem>
                                        <asp:ListItem Value="9">9</asp:ListItem>
                                        <asp:ListItem Value="10">10</asp:ListItem>
                                        <asp:ListItem Value="11">11</asp:ListItem>
                                        <asp:ListItem Value="12">12</asp:ListItem>
                                        <asp:ListItem Value="13">13</asp:ListItem>
                                        <asp:ListItem Value="14">14</asp:ListItem>
                                        <asp:ListItem Value="15">15</asp:ListItem>
                                        <asp:ListItem Value="16">16</asp:ListItem>
                                        <asp:ListItem Value="17">17</asp:ListItem>
                                        <asp:ListItem Value="18">18</asp:ListItem>
                                        <asp:ListItem Value="19">19</asp:ListItem>
                                        <asp:ListItem Value="20">20</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr class="tdbg" id="objFiles1" runat="server">
                                <td style="width: 150" class="tdbgleft">
                                    <strong>自设内容1：</strong>
                                    <br />
                                    在列表模板页面插入<span style="color: Blue">{PE.Label id="自设内容" nodeid="@Request_id" num="1"
                                        /} </span>
                                </td>
                                <td>
                                    <asp:TextBox ID="Custom_Content1" Style="width: 500px; height: 100px" TextMode="MultiLine"
                                        runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr class="tdbg" id="objFiles2" style="display: none" runat="server">
                                <td style="width: 150" class="tdbgleft">
                                    <strong>自设内容2：</strong>
                                    <br />
                                    在列表模板页面插入<span style="color: Blue">{PE.Label id="自设内容" nodeid="@Request_id" num="2"
                                        /} </span>
                                </td>
                                <td>
                                    <asp:TextBox ID="Custom_Content2" Style="width: 500px; height: 100px" TextMode="MultiLine"
                                        runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr class="tdbg" id="objFiles3" style="display: none" runat="server">
                                <td style="width: 150" class="tdbgleft">
                                    <strong>自设内容3：</strong>
                                    <br />
                                    在列表模板页面插入<span style="color: Blue">{PE.Label id="自设内容" nodeid="@Request_id" num="3"
                                        /} </span>
                                </td>
                                <td>
                                    <asp:TextBox ID="Custom_Content3" Style="width: 500px; height: 100px" TextMode="MultiLine"
                                        runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr class="tdbg" id="objFiles4" style="display: none" runat="server">
                                <td style="width: 150" class="tdbgleft">
                                    <strong>自设内容4：</strong>
                                    <br />
                                    在列表模板页面插入<span style="color: Blue">{PE.Label id="自设内容" nodeid="@Request_id" num="4"
                                        /} </span>
                                </td>
                                <td>
                                    <asp:TextBox ID="Custom_Content4" Style="width: 500px; height: 100px" TextMode="MultiLine"
                                        runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr class="tdbg" id="objFiles5" style="display: none" runat="server">
                                <td style="width: 150" class="tdbgleft">
                                    <strong>自设内容5：</strong>
                                    <br />
                                    在列表模板页面插入<span style="color: Blue">{PE.Label id="自设内容" nodeid="@Request_id" num="5"
                                        /} </span>
                                </td>
                                <td>
                                    <asp:TextBox ID="Custom_Content5" Style="width: 500px; height: 100px" TextMode="MultiLine"
                                        runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr class="tdbg" id="objFiles6" style="display: none" runat="server">
                                <td style="width: 150" class="tdbgleft">
                                    <strong>自设内容6：</strong>
                                    <br />
                                    在列表模板页面插入<span style="color: Blue">{PE.Label id="自设内容" nodeid="@Request_id" num="6"
                                        /} </span>
                                </td>
                                <td>
                                    <asp:TextBox ID="Custom_Content6" Style="width: 500px; height: 100px" TextMode="MultiLine"
                                        runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr class="tdbg" id="objFiles7" style="display: none" runat="server">
                                <td style="width: 150" class="tdbgleft">
                                    <strong>自设内容7：</strong>
                                    <br />
                                    在列表模板页面插入<span style="color: Blue">{PE.Label id="自设内容" nodeid="@Request_id" num="7"
                                        /} </span>
                                </td>
                                <td>
                                    <asp:TextBox ID="Custom_Content7" Style="width: 500px; height: 100px" TextMode="MultiLine"
                                        runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr class="tdbg" id="objFiles8" style="display: none" runat="server">
                                <td style="width: 150" class="tdbgleft">
                                    <strong>自设内容8：</strong>
                                    <br />
                                    在列表模板页面插入<span style="color: Blue">{PE.Label id="自设内容" nodeid="@Request_id" num="8"
                                        /} </span>
                                </td>
                                <td>
                                    <asp:TextBox ID="Custom_Content8" Style="width: 500px; height: 100px" TextMode="MultiLine"
                                        runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr class="tdbg" id="objFiles9" style="display: none" runat="server">
                                <td style="width: 150" class="tdbgleft">
                                    <strong>自设内容9：</strong>
                                    <br />
                                    在列表模板页面插入<span style="color: Blue">{PE.Label id="自设内容" nodeid="@Request_id" num="9"
                                        /} </span>
                                </td>
                                <td>
                                    <asp:TextBox ID="Custom_Content9" Style="width: 500px; height: 100px" TextMode="MultiLine"
                                        runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr class="tdbg" id="objFiles10" style="display: none" runat="server">
                                <td style="width: 150" class="tdbgleft">
                                    <strong>自设内容10：</strong>
                                    <br />
                                    在列表模板页面插入<span style="color: Blue">{PE.Label id="自设内容" nodeid="@Request_id" num="10"
                                        /} </span>
                                </td>
                                <td>
                                    <asp:TextBox ID="Custom_Content10" Style="width: 500px; height: 100px" TextMode="MultiLine"
                                        runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr class="tdbg" id="objFiles11" style="display: none" runat="server">
                                <td style="width: 150" class="tdbgleft">
                                    <strong>自设内容11：</strong>
                                    <br />
                                    在列表模板页面插入<span style="color: Blue">{PE.Label id="自设内容" nodeid="@Request_id" num="11"
                                        /} </span>
                                </td>
                                <td>
                                    <asp:TextBox ID="Custom_Content11" Style="width: 500px; height: 100px" TextMode="MultiLine"
                                        runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr class="tdbg" id="objFiles12" style="display: none" runat="server">
                                <td style="width: 150" class="tdbgleft">
                                    <strong>自设内容12：</strong>
                                    <br />
                                    在列表模板页面插入<span style="color: Blue">{PE.Label id="自设内容" nodeid="@Request_id" num="12"
                                        /} </span>
                                </td>
                                <td>
                                    <asp:TextBox ID="Custom_Content12" Style="width: 500px; height: 100px" TextMode="MultiLine"
                                        runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr class="tdbg" id="objFiles13" style="display: none" runat="server">
                                <td style="width: 150" class="tdbgleft">
                                    <strong>自设内容13：</strong>
                                    <br />
                                    在列表模板页面插入<span style="color: Blue">{PE.Label id="自设内容" nodeid="@Request_id" num="13"
                                        /} </span>
                                </td>
                                <td>
                                    <asp:TextBox ID="Custom_Content13" Style="width: 500px; height: 100px" TextMode="MultiLine"
                                        runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr class="tdbg" id="objFiles14" style="display: none" runat="server">
                                <td style="width: 150" class="tdbgleft">
                                    <strong>自设内容14：</strong>
                                    <br />
                                    在列表模板页面插入<span style="color: Blue">{PE.Label id="自设内容" nodeid="@Request_id" num="14"
                                        /} </span>
                                </td>
                                <td>
                                    <asp:TextBox ID="Custom_Content14" Style="width: 500px; height: 100px" TextMode="MultiLine"
                                        runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr class="tdbg" id="objFiles15" style="display: none" runat="server">
                                <td style="width: 150" class="tdbgleft">
                                    <strong>自设内容15：</strong>
                                    <br />
                                    在列表模板页面插入<span style="color: Blue">{PE.Label id="自设内容" nodeid="@Request_id" num="15"
                                        /} </span>
                                </td>
                                <td>
                                    <asp:TextBox ID="Custom_Content15" Style="width: 500px; height: 100px" TextMode="MultiLine"
                                        runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr class="tdbg" id="objFiles16" style="display: none" runat="server">
                                <td style="width: 150" class="tdbgleft">
                                    <strong>自设内容16：</strong>
                                    <br />
                                    &nbsp;&nbsp;在列表模板页面插入<span style="color: Blue">{PE.Label id="自设内容" nodeid="@Request_id"
                                        num="16" /} </span>
                                </td>
                                <td>
                                    <asp:TextBox ID="Custom_Content16" Style="width: 500px; height: 100px" TextMode="MultiLine"
                                        runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr class="tdbg" id="objFiles17" style="display: none" runat="server">
                                <td style="width: 150" class="tdbgleft">
                                    <strong>自设内容17：</strong>
                                    <br />
                                    在列表模板页面插入<span style="color: Blue">{PE.Label id="自设内容" nodeid="@Request_id" num="17"
                                        /} </span>
                                </td>
                                <td>
                                    <asp:TextBox ID="Custom_Content17" Style="width: 500px; height: 100px" TextMode="MultiLine"
                                        runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr class="tdbg" id="objFiles18" style="display: none" runat="server">
                                <td style="width: 150" class="tdbgleft">
                                    <strong>自设内容18：</strong>
                                    <br />
                                    在列表模板页面插入<span style="color: Blue">{PE.Label id="自设内容" nodeid="@Request_id" num="18"
                                        /} </span>
                                </td>
                                <td>
                                    <asp:TextBox ID="Custom_Content18" Style="width: 500px; height: 100px" TextMode="MultiLine"
                                        runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr class="tdbg" id="objFiles19" style="display: none" runat="server">
                                <td style="width: 150" class="tdbgleft">
                                    <strong>自设内容19：</strong>
                                    <br />
                                    在列表模板页面插入<span style="color: Blue">{PE.Label id="自设内容" nodeid="@Request_id" num="19"
                                        /} </span>
                                </td>
                                <td>
                                    <asp:TextBox ID="Custom_Content19" Style="width: 500px; height: 100px" TextMode="MultiLine"
                                        runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr class="tdbg" id="objFiles20" style="display: none" runat="server">
                                <td style="width: 150" class="tdbgleft">
                                    <strong>自设内容20：</strong>
                                    <br />
                                    在列表模板页面插入<span style="color: Blue">{PE.Label id="自设内容" nodeid="@Request_id" num="20"
                                        /} </span>
                                </td>
                                <td>
                                    <asp:TextBox ID="Custom_Content20" Style="width: 500px; height: 100px" TextMode="MultiLine"
                                        runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </asp:Panel>
                    </tbody>
                </table>
            </td>
        </tr>
    </table>
    <br />
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td align="center">
                <pe:ExtendedButton ID="EBtnAdd" Text="添加" IsChecked="false" OnClick="EBtnAdd_Click"
                    runat="server" IsShowTabs="true" Visible="false" />&nbsp;&nbsp;
                <pe:ExtendedButton ID="EBtnModify" Text="修改" IsChecked="false" OnClick="EBtnModify_Click"
                    runat="server" Visible="false" IsShowTabs="true" />
                <input name="Cancel" type="button" class="inputbutton" id="BtnCancel" value="取消"
                    onclick="Redirect('CategoryManage.aspx')" />
                <asp:Button ID="EbtnDelete" Text="删除" OnClick="EBtnDelete_Click" OnClientClick="if(!this.disabled) return confirm('确实要删除此栏目吗？')"
                    runat="server" ValidationGroup="Delete" Visible="false" />
            </td>
        </tr>
    </table>

    <script language="JavaScript" type="text/javascript">
	<!--
	    var enableComment = document.getElementById('<%=ChkEnableComment.ClientID%>');
	    var enableTouristsComment = document.getElementById('<%=ChkEnableTouristsComment.ClientID%>');
	    var setEnableTouristsComment = document.getElementById('SetEnableTouristsComment');
	    function SetEnableComment(){
	        if (!enableComment.checked) {
	            enableTouristsComment.checked = false;
	            setEnableTouristsComment.style.display = "none";
	        }
	        else
	        {
	            setEnableTouristsComment.style.display = "";
	        }
		}
	    
	    function SetEnableTouristsComment(){
	        if (enableTouristsComment.checked) {
	            enableComment.checked = true;
	        } 
		}
		
		function ShowNodeDir(value)
		{
		    EasyOne.WebSite.Admin.Contents.CategoryService.CategoryDir(value,ShowRule);
		}
		
		function ShowContentHtmlDir(value)
		{
		   var dir=$get("ExampleRule").value
		   value=value.replace("{$CategoryDir}/",dir);
		   var d= new Date();
		   value=value.replace("{$Year}",d.getFullYear());
		   value=value.replace("{$Month}",d.getMonth());
		   value=value.replace("{$Day}",d.getDay());
		  
		   $get("ContentHtmlDir").innerHTML=value;
		}
		function ShowContentHtmlFile(value)
		{
		   
		    var d=new Date();
		    value=value.replace("{$Time}", String(d.getHours())+String(d.getMinutes())+String(d.getSeconds()));
		    value=value.replace("{$InfoId}",1);
		    $get("ContentHtmlFile").innerHTML="/"+value
		}
		function ShowContentHtmlExt(value)
		{
		    $get("ContentHtmlExt").innerHTML="."+value;
		}
		function GetRootPath(parentDir)
		{
		 if(parentDir!=""||parentDir!=null)
		 {
		    var tempDir=parentDir;
            var tempArray=tempDir.split("/");
            if(tempArray.length>2)
            {
                tempDir="/"+tempArray[1]+"/";
            }
            return tempDir;
          }else
          {
          return "";
          }
		}
		function ShowRule(value)
	    {
		    $get("ExampleRule").value=value
		    if($get("ContentHtmlDir")!="")
		    {
		        $get("ContentHtmlDir").innerHTML=value+$get("ContentHtmlDir").innerHTML;
		        $get("ListPage1").innerHTML="例："+value+"Index.html（栏目首页）<br />"+value+"List_1.html（栏目列表第一页）";
		        $get("ListPage2").innerHTML="例："+GetRootPath(value)+"List/List_236.html（栏目首页）<br />"+GetRootPath(value)+"List/List_236_1.html（栏目列表第一页）";
		       
		        $get("ListPage3").innerHTML="例："+GetRootPath(value)+"List_236.html（栏目首页）<br />"+GetRootPath(value)+"List_236_1.html（栏目列表第一页）";
		        
		    }
		}
		
		function InitCategoryDir()
		{
		    
		    if($get("<%=TxtContentHtmlDir.ClientID %>_Text").value!="")
		    {
		        ShowContentHtmlDir($get("<%=TxtContentHtmlDir.ClientID %>_Text").value);
		    }
		    
		    if($get("<%=TxtContentHtmlFile.ClientID %>_Text").value!="")
		    {
		        ShowContentHtmlFile($get("<%=TxtContentHtmlFile.ClientID %>_Text").value);
		    }
		    if($get("<%=TxtContentHtmlExt.ClientID %>_Text").value!="")
		    {
		        ShowContentHtmlExt($get("<%=TxtContentHtmlExt.ClientID %>_Text").value);
		    }

		      ShowNodeDir($get("<%=HdnNodeId.ClientID %>").value);
		}
	

		InitCategoryDir();
		
		if(<%=RadNeedCache1.Checked.ToString().ToLower() %>)
          document.getElementById("TrSetCacheTime").style.display='';
	//-->
    </script>

    <asp:ValidationSummary ID="ValSum" runat="server" ShowMessageBox="true" ShowSummary="false" />
</asp:Content>
