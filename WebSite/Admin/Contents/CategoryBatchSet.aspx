<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Contents.CategoryBatchSet" Title="批量设置节点属性" Codebehind="CategoryBatchSet.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <asp:ScriptManager ID="SmCategory" runat="server" EnablePartialRendering="true">
    </asp:ScriptManager>

    <script language="JavaScript" type="text/JavaScript">        
        var tID=0;
        var arrTabTitle = new Array("TabTitle0","TabTitle1","<%= TabTitle2.ClientID %>","TabTitle3","TabTitle4","<%= TabTitle5.ClientID %>");
        var arrTabs = new Array("Tabs0","Tabs1","Tabs2","Tabs3","Tabs4","Tabs5");
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
        
        if(<%=RadNeedCache1.Checked.ToString().ToLower() %>)
          document.getElementById("TrSetCacheTime").style.display='';
       
    </script>

    <table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td colspan="5" class="spacingtitle">
                批量设置节点属性
            </td>
        </tr>
        <tr>
            <td class="tdbgleft" style="width: 10%" valign="top">
                <table>
                    <tr>
                        <td>
                            <span style="color: Red">提示：</span>可以按住“Shift”<br />
                            或“Ctrl”键进行多个节点的选择</td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:ListBox ID="LstNodes" runat="server" DataTextField="NodeName" DataValueField="NodeId"
                                SelectionMode="Multiple" Height="282px" Width="231px"></asp:ListBox></td>
                    </tr>
                    <tr>
                        <td align="center">
                            <input id="BtnSelectAll" onclick="SelectAll()" type="button" class="inputbutton"
                                value="  选定所有节点  " />
                            <input id="BtnCancelAll" onclick="UnSelectAll()" type="button" class="inputbutton"
                                value="取消选定所有节点" />
                        </td>
                    </tr>
                </table>
            </td>
            <td class="tdbgleft" style="width: 90%" valign="top">
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr align="center">
                        <td id="TabTitle0" class="titlemouseover" onclick="ShowTabs(0)">
                            栏目选项</td>
                        <td id="TabTitle1" class="tabtitle" onclick="ShowTabs(1)">
                            模板选项</td>
                        <td id="TabTitle2" class="tabtitle" onclick="ShowTabs(2)" runat="server">
                            收费设置</td>
                        <td id="TabTitle3" class="tabtitle" onclick="ShowTabs(3)">
                            前台样式</td>
                        <td id="TabTitle4" class="tabtitle" onclick="ShowTabs(4)">
                            生成选项</td>
                        <td id="TabTitle5" class="tabtitle" runat="server" onclick="ShowTabs(5)">
                            权限设置</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
                <table width="100%" border="0" cellpadding="5" cellspacing="1" class="border">
                    <%--栏目选项--%>
                    <tbody id="Tabs0">
                        <tr class="tdbg">
                            <td style="width: 5%" class="tdbgleft">
                                <asp:CheckBox ID="ChkOpenType" runat="server" /></td>
                            <td style="width: 206px" class="tdbgleft">
                                <strong>打开方式：</strong></td>
                            <td>
                                <asp:RadioButton ID="RadOpenType0" Checked="true" GroupName="OpenType" runat="server" />在原窗口打开&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:RadioButton ID="RadOpenType1" GroupName="OpenType" runat="server" />在新窗口打开</td>
                        </tr>
                        <tr class="tdbg">
                            <td style="width: 5%" class="tdbgleft">
                                <asp:CheckBox ID="ChkPurviewType" runat="server" /></td>
                            <td style="width: 206px;" class="tdbgleft" valign="top">
                                <strong>浏览/查看权限：</strong><br />
                                <div style="color: Red; width: 213px;">
                                    栏目权限为继承关系。<br />
                                    例如：当父栏目设为“认证栏目”时，<br />
                                    子栏目的权限设置将继承父栏目设置，<br />
                                    即使子栏目设为“开放栏目”也无效。<br />
                                    相反，如果父栏目设为“开放栏目”，<br />
                                    子栏目可以设为“半开放栏目”或“认证栏目”。</div>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpPurviewType" runat="server">
                                    <ContentTemplate>
                                        <table width="100%" border="0" cellpadding="2" cellspacing="1">
                                            <tr>
                                                <td style="width: 15%;" valign="top">
                                                    <asp:RadioButton ID="RadPurviewType0" AutoPostBack="true" GroupName="PurviewType"
                                                        runat="server" OnCheckedChanged="RadPurviewType_CheckedChanged" />开放栏目</td>
                                                <td style="width: 80%;">
                                                    任何人（包括游客）可以浏览和查看此栏目下的信息。</td>
                                            </tr>
                                            <tr>
                                                <td style="width: 15%;" valign="top">
                                                    <asp:RadioButton ID="RadPurviewType1" AutoPostBack="true" GroupName="PurviewType"
                                                        runat="server" OnCheckedChanged="RadPurviewType_CheckedChanged" />半开放栏目</td>
                                                <td>
                                                    任何人（包括游客）都可以浏览。游客不可查看，其他会员根据会员组的栏目权限设置决定是否可以查看。</td>
                                            </tr>
                                            <tr>
                                                <td style="width: 15%;" valign="top">
                                                    <asp:RadioButton ID="RadPurviewType2" AutoPostBack="true" GroupName="PurviewType"
                                                        runat="server" OnCheckedChanged="RadPurviewType_CheckedChanged" />认证栏目</td>
                                                <td valign="top">
                                                    游客不能浏览和查看，其他会员根据会员组的栏目权限设置决定是否可以浏览和查看。</td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td style="width: 5%" class="tdbgleft">
                                <asp:CheckBox ID="ChkEnableComment2" runat="server" /></td>
                            <td style="width: 206px" class="tdbgleft">
                                <strong>评论权限：</strong></td>
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
                            <td style="width: 5%" class="tdbgleft">
                                <asp:CheckBox ID="ChkWorkFlow" runat="server" /></td>
                            <td style="width: 206px" class="tdbgleft">
                                <strong>工作流：</strong></td>
                            <td>
                                <asp:DropDownList ID="DropWorkFlow" DataTextField="FlowName" DataValueField="FlowId"
                                    runat="server" AppendDataBoundItems="true">
                                    <asp:ListItem Text="请选择" Value="-1"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td style="width: 5%" class="tdbgleft">
                                <asp:CheckBox ID="ChkEnableProtect" runat="server" /></td>
                            <td style="width: 206px" class="tdbgleft">
                                <strong>是否启用此栏目的防止复制、防盗链功能：</strong></td>
                            <td>
                                <asp:RadioButtonList ID="RadlEnableProtect" RepeatLayout="Flow" RepeatDirection="Horizontal"
                                    runat="server">
                                    <asp:ListItem Text="是&nbsp;&nbsp;&nbsp;&nbsp;" Selected="True" Value="True"></asp:ListItem>
                                    <asp:ListItem Text="否" Value="False"></asp:ListItem>
                                </asp:RadioButtonList></td>
                        </tr>
                        <tr class="tdbg">
                            <td style="width: 5%" class="tdbgleft">
                                <asp:CheckBox ID="ChkEnableAddWhenHasChild" runat="server" /></td>
                            <td style="width: 206px" class="tdbgleft">
                                <strong>有子栏目时是否可以在此栏目添加文章：</strong></td>
                            <td>
                                <asp:RadioButtonList ID="RadlEnableAddWhenHasChild" RepeatLayout="Flow" RepeatDirection="Horizontal"
                                    runat="server">
                                    <asp:ListItem Text="是&nbsp;&nbsp;&nbsp;&nbsp;" Selected="True" Value="True"></asp:ListItem>
                                    <asp:ListItem Text="否" Value="False"></asp:ListItem>
                                </asp:RadioButtonList></td>
                        </tr>
                        <tr class="tdbg">
                            <td style="width: 5%" class="tdbgleft">
                                <asp:CheckBox ID="ChkHitsOfHot" runat="server" /></td>
                            <td style="width: 206px" class="tdbgleft">
                                <strong>本栏目热点的点击数最小值：</strong></td>
                            <td>
                                <asp:TextBox ID="TxtHitsOfHot" runat="server" Columns="5"></asp:TextBox>
                                <asp:RangeValidator ID="ValgHitsOfHot" runat="server" ControlToValidate="TxtHitsOfHot"
                                    ErrorMessage="请输入整数" MinimumValue="0" MaximumValue="2147483647" Type="Integer"
                                    SetFocusOnError="True"></asp:RangeValidator></td>
                        </tr>
                        <tr class="tdbg">
                            <td style="width: 5%" class="tdbgleft">
                                <asp:CheckBox ID="ChkIsSetCache" runat="server" /></td>
                            <td style="width: 206px" class="tdbgleft">
                                <strong>是否缓存本节点的首页HTML：</strong></td>
                            <td>
                                <asp:RadioButton ID="RadNeedCache1" runat="server" GroupName="NeedCache" Text="是" />&nbsp;&nbsp;
                                &nbsp;<asp:RadioButton ID="RadNeedCache0" runat="server" GroupName="NeedCache" Text="否"
                                    Checked="True" /></td>
                        </tr>
                        <tr id="TrSetCacheTime" class="tdbg" style="display: none">
                            <td style="width: 5%" class="tdbgleft">
                                </td>
                            <td style="width: 206px" class="tdbgleft">
                                <strong>设置缓存更新时间为：</strong></td>
                            <td>
                                <asp:TextBox ID="TxtCacheTime" runat="server" Columns="5"></asp:TextBox>分钟
                                <asp:RangeValidator ID="ValgCacheTime" runat="server" ControlToValidate="TxtCacheTime"
                                    ErrorMessage="请输入有效实数" MinimumValue="0" MaximumValue="2147483647" Type="Double"
                                    SetFocusOnError="True"></asp:RangeValidator></td>
                        </tr>
                    </tbody>
                    <%--模板选项--%>
                    <tbody id="Tabs1" style="display: none">
                        <tr class="tdbg">
                            <td style="width: 5%" class="tdbgleft">
                                <asp:CheckBox ID="ChkFileContainChildTemplate" runat="server" /></td>
                            <td align="left" colspan="2">
                                <table width="100%">
                                    <tr>
                                        <td align="left" class="tdbgleft" style="width: 40%">
                                            <strong>栏目列表页模板：</strong>
                                        </td>
                                        <td align="left" style="width: 60%">
                                            <pe:TemplateSelectControl ID="FileContainChildTemplate" Width="300px" runat="server"></pe:TemplateSelectControl>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td style="width: 5%" class="tdbgleft">
                                <asp:CheckBox ID="ChkFileCdefaultListTmeplate" runat="server" /></td>
                            <td align="left" colspan="2">
                                <table width="100%">
                                    <tr>
                                        <td align="left" class="tdbgleft" style="width: 40%">
                                            <strong>栏目首页模板：</strong>
                                            <br />
                                            如果不设置栏目首页模板，则自动使用栏目列表页模板。
                                        </td>
                                        <td align="left" style="width: 60%">
                                            <pe:TemplateSelectControl ID="FileCdefaultListTmeplate" Width="300px" runat="server"></pe:TemplateSelectControl>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td style="width: 5%" class="tdbgleft">
                                <asp:CheckBox ID="ChkFileCTemplate" runat="server" /></td>
                            <td style="width: 100%">
                                <asp:Repeater ID="RepContentModelTemplate" runat="server" OnItemDataBound="RepModelTemplate_ItemDataBound">
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr class="tdbg">
                                                <td align="left">
                                                    <strong>选择内容模型</strong></td>
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
                                                    <strong>选择商品模型：</strong></td>
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
                    <tbody id="Tabs2" style="display: none">
                        <tr class="tdbg">
                            <td style="width: 5%" class="tdbgleft">
                                <asp:CheckBox ID="ChkPresentExp" runat="server" /></td>
                            <td style="width: 206px" class="tdbgleft">
                                <strong>积分奖励：</strong><br />
                                会员在此栏目发表信息时可以得到的积分</td>
                            <td>
                                会员在此栏目每发表一条信息，可以得到
                                <asp:TextBox ID="TxtPresentExp" runat="server" Columns="5"></asp:TextBox>分积分<asp:RangeValidator
                                    ID="ValgPresentExp" runat="server" ControlToValidate="TxtPresentExp" ErrorMessage="请输入整数"
                                    MaximumValue="2147483647" MinimumValue="0" SetFocusOnError="True" Type="Integer"></asp:RangeValidator></td>
                        </tr>
                        <tr class="tdbg">
                            <td style="width: 5%" class="tdbgleft">
                                <asp:CheckBox ID="ChkDefaultItemPoint" runat="server" /></td>
                            <td style="width: 206px" class="tdbgleft">
                                <strong>默认消费点券数：</strong><br />
                                会员在此栏目下添加文章时，该文章默认的收费点券数</td>
                            <td>
                                <asp:TextBox ID="TxtDefaultItemPoint" runat="server" Columns="5"></asp:TextBox>点<asp:RangeValidator
                                    ID="ValgDefaultItemPoint" runat="server" ControlToValidate="TxtDefaultItemPoint"
                                    ErrorMessage="请输入整数" MaximumValue="2147483647" MinimumValue="0" SetFocusOnError="True"
                                    Type="Integer"></asp:RangeValidator></td>
                        </tr>
                        <tr class="tdbg">
                            <td style="width: 5%" class="tdbgleft">
                                <asp:CheckBox ID="ChkShowChargeType" runat="server" /></td>
                            <td style="width: 206px" class="tdbgleft">
                                <strong>默认重复收费：</strong><br />
                                会员在此栏目下添加文章时，该文章默认的重复收费方式</td>
                            <td>
                                <pe:ShowChargeType ID="ShowChargeType" runat="server" />
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td style="width: 5%" class="tdbgleft">
                                <asp:CheckBox ID="ChkDefaultItemDividePercent" runat="server" /></td>
                            <td style="width: 206px" class="tdbgleft">
                                <strong>默认分成比例：</strong><br />
                                会员在此栏目下添加文章时，该文章默认的分成比例</td>
                            <td>
                                <asp:TextBox ID="TxtDefaultItemDividePercent" runat="server" Columns="5"></asp:TextBox>
                                %</td>
                        </tr>
                    </tbody>
                    <%-- 前台样式--%>
                    <tbody id="Tabs3" style="display: none">
                        <tr class="tdbg">
                            <td style="width: 5%" class="tdbgleft">
                                <asp:CheckBox ID="ChkShowOnMenu" runat="server" /></td>
                            <td style="width: 206px" class="tdbgleft">
                                <strong>是否在顶部菜单处显示：</strong><br />
                                此选项只对一级栏目有效。</td>
                            <td>
                                <asp:RadioButtonList ID="RadlShowOnMenu" RepeatLayout="Flow" RepeatDirection="Horizontal"
                                    runat="server">
                                    <asp:ListItem Text="是&nbsp;&nbsp;&nbsp;&nbsp;" Selected="True" Value="True"></asp:ListItem>
                                    <asp:ListItem Text="否" Value="False"></asp:ListItem>
                                </asp:RadioButtonList></td>
                        </tr>
                        <tr class="tdbg">
                            <td style="width: 5%" class="tdbgleft">
                                <asp:CheckBox ID="ChkShowOnPath" runat="server" /></td>
                            <td style="width: 206px" class="tdbgleft">
                                <strong>是否位置导航处显示：</strong></td>
                            <td>
                                <asp:RadioButtonList ID="RadlShowOnPath" RepeatLayout="Flow" RepeatDirection="Horizontal"
                                    runat="server">
                                    <asp:ListItem Text="是&nbsp;&nbsp;&nbsp;&nbsp;" Selected="True" Value="True"></asp:ListItem>
                                    <asp:ListItem Text="否" Value="False"></asp:ListItem>
                                </asp:RadioButtonList></td>
                        </tr>
                        <tr class="tdbg">
                            <td style="width: 5%" class="tdbgleft">
                                <asp:CheckBox ID="ChkShowOnMap" runat="server" /></td>
                            <td style="width: 206px" class="tdbgleft">
                                <strong>是否在网站地图（栏目导航）处显示：</strong></td>
                            <td>
                                <asp:RadioButtonList ID="RadlShowOnMap" RepeatLayout="Flow" RepeatDirection="Horizontal"
                                    runat="server">
                                    <asp:ListItem Text="是&nbsp;&nbsp;&nbsp;&nbsp;" Selected="True" Value="True"></asp:ListItem>
                                    <asp:ListItem Text="否" Value="False"></asp:ListItem>
                                </asp:RadioButtonList></td>
                        </tr>
                        <tr class="tdbg">
                            <td style="width: 5%" class="tdbgleft">
                                <asp:CheckBox ID="ChkShowOnListIndex" runat="server" /></td>
                            <td style="width: 206px" class="tdbgleft">
                                <strong>是否在首页的分类列表处显示：</strong><br />
                                此选项只对一级栏目有效。如果一级栏目比较多，但首页不想显示太多的分类列表，这个选项就非常有用。</td>
                            <td>
                                <asp:RadioButtonList ID="RadlShowOnListIndex" RepeatLayout="Flow" RepeatDirection="Horizontal"
                                    runat="server">
                                    <asp:ListItem Text="是&nbsp;&nbsp;&nbsp;&nbsp;" Selected="True" Value="True"></asp:ListItem>
                                    <asp:ListItem Text="否" Value="False"></asp:ListItem>
                                </asp:RadioButtonList></td>
                        </tr>
                        <tr class="tdbg">
                            <td style="width: 5%" class="tdbgleft">
                                <asp:CheckBox ID="ChkShowOnListParent" runat="server" /></td>
                            <td style="width: 206px" class="tdbgleft">
                                <strong>是否在父栏目的分类列表处显示：</strong><br />
                                如果某栏目下有几十个子栏目，但只想显示其中几个子栏目的文章列表，这个选项就非常有用。</td>
                            <td>
                                <asp:RadioButtonList ID="RadlShowOnListParent" RepeatLayout="Flow" RepeatDirection="Horizontal"
                                    runat="server">
                                    <asp:ListItem Text="是&nbsp;&nbsp;&nbsp;&nbsp;" Selected="True" Value="True"></asp:ListItem>
                                    <asp:ListItem Text="否" Value="False"></asp:ListItem>
                                </asp:RadioButtonList></td>
                        </tr>
                        <tr class="tdbg">
                            <td style="width: 5%" class="tdbgleft">
                                <asp:CheckBox ID="ChkItemPageSize" runat="server" /></td>
                            <td style="width: 206px" class="tdbgleft">
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
                            <td style="width: 5%" class="tdbgleft">
                                <asp:CheckBox ID="ChkItemListOrderType" runat="server" /></td>
                            <td style="width: 206px" class="tdbgleft">
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
                            <td style="width: 5%" class="tdbgleft">
                                <asp:CheckBox ID="ChkItemOpenType" runat="server" /></td>
                            <td style="width: 206px" class="tdbgleft">
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
                    <tbody id="Tabs4" style="display: none">
                        <tr class="tdbg">
                            <td style="width: 5%" class="tdbgleft">
                                <asp:CheckBox ID="ChkListPageCreateHtmlType" runat="server" /></td>
                            <td class="tdbgleft" style="width: 206px">
                                <strong>列表页是否生成HTML：</strong><br />
                                请谨慎选择！以后在每一次更改生成方式前，<br />
                                你最好先删除所有以前生成的文件，<br />
                                然后在保存参数后再重新生成所有文件。</td>
                            <td>
                                <asp:RadioButtonList ID="RadlIsListPageCreate" RepeatDirection="horizontal" runat="server">
                                    <asp:ListItem Value="false" Selected="True">不生成</asp:ListItem>
                                    <asp:ListItem Value="true">生成</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td style="width: 5%" class="tdbgleft">
                                <asp:CheckBox ID="ChkAutoCreateHtmlType" runat="server" /></td>
                            <td class="tdbgleft" style="width: 206px">
                                <strong>自动生成HTML时的生成方式：</strong><br />
                                添加/修改信息时，系统可以自动生成有关页面文件，<br />
                                请在这里选择自动生成时的方式。
                            </td>
                            <td>
                                <asp:RadioButtonList ID="RadlAutoCreateHtmlType" runat="server">
                                    <asp:ListItem Value="None" Selected="True">不自动生成，由管理员手工生成相关页面自动生成全部所需页面</asp:ListItem>
                                    <asp:ListItem Value="Content">只自动生成内容页</asp:ListItem>
                                    <asp:ListItem Value="ContentAndNode">自动生成内容页和所属栏目的列表页</asp:ListItem>
                                    <asp:ListItem Value="ContentAndNodeAndParentNode">自动生成内容页和所属栏目及父栏目的列表页</asp:ListItem>
                                    <asp:ListItem Value="ContentAndNodeAndParentNodeAndSpecial">自动生成内容页和所属栏目及父栏目的列表页以及自动关联的专题页</asp:ListItem>
                                    <asp:ListItem Value="ContentAndRelatedNode">自动生成所有关联的页（在发表、更新文章时，除了自动生成内容页和所属栏目及父栏目的列表页以外，还会自动会生成指定栏目的列表页）。</asp:ListItem>
                                </asp:RadioButtonList></td>
                        </tr>
                        <tr class="tdbg" runat="server" id="SelectRelation" style="display: none">
                            <td style="width: 5%" class="tdbgleft">
                                <asp:CheckBox ID="ChkRelation" runat="server" /></td>
                            <td class="tdbgleft" style="width: 206px">
                                <strong>关联</strong>
                            </td>
                            <td>
                                <div style="float: right">
                                    关联专题<br />
                                    <asp:ListBox SelectionMode="Multiple" ID="LstRelationSpecial" DataTextField="SpecialName"
                                        DataValueField="SpecialId" runat="server" Height="251px" Width="200px"></asp:ListBox><br />
                                    <input type="button" class="inputbutton" onclick="SelectAll('<%= LstRelationSpecial.ClientID %>')"
                                        value="选择所有" />
                                    <input type="button" class="inputbutton" onclick="UnSelectAll('<%= LstRelationSpecial.ClientID %>')"
                                        value="取消选择" /></div>
                                <div style="float: left">
                                    关联栏目<br />
                                    <asp:ListBox SelectionMode="Multiple" ID="LstRelationNodes" DataTextField="NodeName"
                                        DataValueField="NodeId" runat="server" Height="251px" Width="200px"></asp:ListBox><br />
                                    <input type="button" class="inputbutton" onclick="SelectAll('<%= LstRelationNodes.ClientID %>')"
                                        value="选择所有" />
                                    <input type="button" class="inputbutton" onclick="UnSelectAll('<%= LstRelationNodes.ClientID %>')"
                                        value="取消选择" /></div>
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td style="width: 5%" class="tdbgleft">
                                <asp:CheckBox ID="ChkListPageHtmlDirType" runat="server" /></td>
                            <td class="tdbgleft" style="width: 206px">
                                <strong>栏目列表文件的存放位置：</strong><br />
                            </td>
                            <td>
                                <asp:RadioButtonList ID="RadlListPageHtmlDirType" runat="server">
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td style="width: 5%" class="tdbgleft">
                                <asp:CheckBox ID="ChkPagePostfix" runat="server" /></td>
                            <td class="tdbgleft" style="width: 206px">
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
                        <%-- <tr class="tdbg">
                            <td style="width: 5%" class="tdbgleft">
                                <asp:CheckBox ID="ChkAutoUpdatePages" runat="server" /></td>
                            <td class="tdbgleft" style="width: 206px">
                                <strong>自动更新的页数（列表在此页交叉，重复显示）：</strong><br />
                                当此栏目为最下一级栏目时，则会分页显示此栏目中的文章，<br />
                                这里指定的是每页显示的文章数。</td>
                            <td>
                                <asp:TextBox ID="TxtAutoUpdatePages" runat="server" Columns="5"></asp:TextBox>
                                <asp:RangeValidator ID="ValgAutoUpdatePages" runat="server" ControlToValidate="TxtAutoUpdatePages"
                                    ErrorMessage="请输入整数" MaximumValue="2147483647" MinimumValue="0" SetFocusOnError="True"
                                    Type="Integer"></asp:RangeValidator></td>
                        </tr>--%>
                        <tr class="tdbg">
                            <td style="width: 5%" class="tdbgleft">
                                <asp:CheckBox ID="ChkContentPageCreateHtmlType" runat="server" /></td>
                            <td class="tdbgleft" style="width: 206px">
                                <strong>内容页是否生成HTML：</strong><br />
                                请谨慎选择！以后在每一次更改生成方式前，<br />
                                你最好先删除所有以前生成的文件，<br />
                                然后在保存参数后再重新生成所有文件。</td>
                            <td>
                                <asp:RadioButtonList ID="RadlIsContentPageCreate" RepeatDirection="horizontal" runat="server">
                                    <asp:ListItem Value="false" Selected="True">不生成</asp:ListItem>
                                    <asp:ListItem Value="true">生成</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td style="width: 5%" class="tdbgleft">
                                <asp:CheckBox ID="ChkContentHtmlDir" runat="server" /></td>
                            <td class="tdbgleft" style="width: 206px">
                                <strong>内容页的文件名规则：</strong><br />
                                例如：{$InstallDir}/{$CategoryDir}/<br />
                                {$Year}/{$Month}/{$Day}/{$InfoID}.shtml</td>
                            <td>
                                <table border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <pe:ComboBox ID="TxtContentHtmlDir" Width="250px" runat="server">
                                                <Items>
                                                    <asp:ListItem>{$CategoryDir}/{$Year}/{$Month}/{$Day}</asp:ListItem>
                                                    <asp:ListItem>{$Year}/{$Month}/{$Day}</asp:ListItem>
                                                </Items>
                                            </pe:ComboBox>
                                        </td>
                                        <td>
                                            /</td>
                                        <td>
                                            <pe:ComboBox ID="TxtContentHtmlFile" Width="150px" runat="server">
                                                <Items>
                                                    <asp:ListItem>{$Time}{$InfoId}</asp:ListItem>
                                                    <asp:ListItem>{$InfoId}</asp:ListItem>
                                                    <asp:ListItem>{$pinyinOfTitle}</asp:ListItem>
                                                </Items>
                                            </pe:ComboBox>
                                        </td>
                                        <td>
                                            .</td>
                                        <td>
                                            <pe:ComboBox ID="TxtContentHtmlExt" runat="server">
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
                    </tbody>
                    <tbody id="Tabs5" style="display: none">
                        <tr class="tdbg">
                            <td style="width: 5%" class="tdbgleft">
                                <asp:CheckBox ID="ChkPermissions" runat="server" /></td>
                            <td colspan="2">
                                <table width="100%" border="0" cellspacing="1">
                                    <tr valign="top" align="center">
                                        <td class="tdbgleft">
                                            <strong>会员组权限</strong>
                                        </td>
                                        <td class="tdbgleft">
                                            <strong>角色权限</strong></td>
                                    </tr>
                                    <tr class="tdbg" valign="top">
                                        <td style="width: 40%" id="TdGroupPermissions" runat="server">
                                            <asp:UpdatePanel ID="UpPermissions" runat="server">
                                                <ContentTemplate>
                                                    <pe:ExtendedGridView ID="EgvPermissions" runat="server" AutoGenerateColumns="False"
                                                        DataKeyNames="GroupId" OnRowDataBound="EgvPermissions_RowDataBound">
                                                        <Columns>
                                                            <pe:BoundField HeaderText="会员组名" DataField="GroupName"/>
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
                                        <td style="width: 60%" id="TdRolePermissions" runat="server">
                                            <pe:ExtendedGridView ID="EgvRoleView" runat="server" AutoGenerateColumns="False"
                                                DataKeyNames="RoleId" OnRowDataBound="EgvRoleView_RowDataBound">
                                                <Columns>
                                                    <pe:BoundField HeaderText="角色名" DataField="RoleName"/>
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
                                                    <pe:TemplateField HeaderText="栏目管理">
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
                    <tbody>
                        <tr class="tdbg">
                            <td colspan="3">
                                <strong><span style="color: Blue;">说明：</span></strong>
                                <br />
                                1、若要批量修改某个属性的值，请先选中其左侧的复选框，然后再设定属性值。<br />
                                2、这里显示的属性值都是系统默认值，与所选节点的已有属性无关
                            </td>
                        </tr>
                    </tbody>
                </table>
            </td>
        </tr>
    </table>
    <br />
    <center>
        <asp:Button ID="EBtnBacthSet" Text="执行批处理" OnClick="EBtnBacthSet_Click" runat="server" />
        <asp:Button ID="BtnCancel" runat="server" Text="取消" OnClick="BtnCancel_Click" /></center>

    <script language="javascript" type="text/javascript">
                 //评论部分
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
    </script>

</asp:Content>
