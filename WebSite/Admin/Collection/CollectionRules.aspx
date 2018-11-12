<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Collection.CollectionRules"
    MasterPageFile="~/Admin/MasterPage.master" Title="采集规则管理" ValidateRequest="false" Codebehind="CollectionRules.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td colspan="2" class="spacingtitle">
                <b>
                    <pe:AlternateLiteral ID="LblTitle" Text="添加采集规则" AlternateText="修改采集规则" runat="Server" /></b>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>网站名称：</strong></td>
            <td>
                <asp:TextBox ID="TxtWebSiteName" runat="server" Width="165px"></asp:TextBox>
                <pe:RequiredFieldValidator ID="ValeWebSiteName" ControlToValidate="TxtWebSiteName"
                    ErrorMessage="网站名称不能为空！" runat="server"></pe:RequiredFieldValidator></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>编码选择：</strong></td>
            <td style="height: 30px">
                <asp:RadioButtonList runat="server" ID="RadlCodeType" RepeatDirection="Horizontal">
                    <asp:ListItem Value="0" Selected="True">GB2312</asp:ListItem>
                    <asp:ListItem Value="1">UTF-8</asp:ListItem>
                    <asp:ListItem Value="2">Big5</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>规则类型：</strong></td>
            <td style="height: 32px">
                <asp:RadioButton ID="RadList" AutoPostBack="true" Text="列表" GroupName="RadlRulesType"
                    runat="server" OnCheckedChanged="RadList_SelectedIndexChanged" Checked="true" />
                <asp:RadioButton ID="RadField" AutoPostBack="true" Text="字段" GroupName="RadlRulesType"
                    runat="server" OnCheckedChanged="RadList_SelectedIndexChanged" />
                <asp:RadioButton ID="RadPaing" AutoPostBack="true" Text="分页" GroupName="RadlRulesType"
                    runat="server" OnCheckedChanged="RadList_SelectedIndexChanged" />
                <asp:RadioButton ID="RadFilter" AutoPostBack="true" Text="过滤" GroupName="RadlRulesType"
                    runat="server" OnCheckedChanged="RadList_SelectedIndexChanged" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>采集Url：</strong></td>
            <td style="height: 32px">
                <asp:TextBox ID="TxtListAddress" runat="server" Width="50%"></asp:TextBox>
                <asp:Button ID="BtnShowCode" runat="server" Text="查看源代码" OnClick="BtnShowCode_Click" />
                <pe:UrlValidator ID="ValeListAddress" ControlToValidate="TxtListAddress" runat="server"></pe:UrlValidator>
            </td>
        </tr>
        <tr class="tdbg">
            <td colspan="2" style="height: 300px" class="tdbg">
                <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border" style="height: 300px">
                    <tr class="tdbg">
                        <td style="width: 45%; height: 300px" valign="top">
                            <asp:TextBox ID="TxtShowCode" runat="server" Height="320px" TextMode="MultiLine"
                                Width="100%"></asp:TextBox>
                        </td>
                        <td style="height: 300px" valign="top">
                            <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
                                <tbody id="PnlList" visible="true" runat="server">
                                    <tr class="tdbg">
                                        <td style="height: 40%" valign="top">
                                            列表设置开始↓<br />
                                            <asp:TextBox ID="TxtListBegin" runat="server" Height="80px" TextMode="MultiLine"
                                                Width="80%"></asp:TextBox>
                                            <br />
                                            <pe:RequiredFieldValidator ID="ValeListBegin" ControlToValidate="TxtListBegin" ErrorMessage="列表设置开始代码不能为空！"
                                                runat="server" ValidationGroup="List"></pe:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr class="tdbg">
                                        <td style="height: 40%" valign="top">
                                            列表设置结束↓<br />
                                            <asp:TextBox ID="TxtListEnd" runat="server" Height="80px" TextMode="MultiLine" Width="80%"></asp:TextBox>
                                            <br />
                                            <pe:RequiredFieldValidator ID="ValeListEnd" ControlToValidate="TxtListEnd" ErrorMessage="列表设置结束代码不能为空！"
                                                runat="server" ValidationGroup="List"></pe:RequiredFieldValidator>
                                            <br />
                                            <asp:Button ID="BtnTestList" runat="server" Text="测试一下" OnClick="BtnTestList_Click"
                                                ValidationGroup="List" />
                                        </td>
                                    </tr>
                                    <tr class="tdbg">
                                        <td style="height: 10%" valign="top">
                                            链接设置开始↓<br />
                                            <asp:TextBox ID="TxtLinkBegin" runat="server" Width="80%"></asp:TextBox>
                                            <br />
                                            <pe:RequiredFieldValidator ID="ValeLinkBegin" ControlToValidate="TxtLinkBegin" ErrorMessage="链接设置开始代码不能为空！"
                                                runat="server" ValidationGroup="Link"></pe:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr class="tdbg">
                                        <td style="height: 10%" valign="top">
                                            链接设置结束↓<br />
                                            <asp:TextBox ID="TxtLinkEnd" runat="server" Width="80%"></asp:TextBox>
                                            <br />
                                            <pe:RequiredFieldValidator ID="ValeLinkEnd" ControlToValidate="TxtLinkEnd" ErrorMessage="链接设置结束代码不能为空！"
                                                runat="server" ValidationGroup="Link"></pe:RequiredFieldValidator>
                                            <br />
                                            <asp:Button ID="BtnTestLink" runat="server" Text="测试一下" OnClick="BtnTestLink_Click"
                                                ValidationGroup="Link" />
                                        </td>
                                    </tr>
                                </tbody>
                                <tbody id="PnlField" visible="false" runat="server">
                                    <tr class="tdbg">
                                        <td style="height: 40%" valign="top">
                                            字段设置开始↓<br />
                                            <asp:TextBox ID="TxtFieldBegin" runat="server" Height="80px" TextMode="MultiLine"
                                                Width="80%"></asp:TextBox>
                                            <br />
                                            <pe:RequiredFieldValidator ID="ValeFieldBegin" ControlToValidate="TxtFieldBegin"
                                                ErrorMessage="字段设置开始代码不能为空！" runat="server" ValidationGroup="Field"></pe:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr class="tdbg">
                                        <td style="height: 40%" valign="top">
                                            字段设置结束↓<br />
                                            <asp:TextBox ID="TxtFieldEnd" runat="server" Height="80px" TextMode="MultiLine" Width="80%"></asp:TextBox>
                                            <br />
                                            <pe:RequiredFieldValidator ID="ValeFieldEnd" ControlToValidate="TxtFieldEnd" ErrorMessage="字段设置结束代码不能为空！"
                                                runat="server" ValidationGroup="Field"></pe:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr class="tdbg">
                                        <td style="height: 10%" valign="top">
                                            <asp:RadioButton ID="RadContent" runat="server" GroupName="FieldSet" Text="保存为截取内容"
                                                Checked="true" />
                                            <br />
                                            <asp:RadioButton ID="RadKeyWord" runat="server" GroupName="FieldSet" Text="保存为关键字" />
                                            关键字长度：<asp:TextBox ID="TxtKeyWord" runat="server" Text="2" Width="70px"></asp:TextBox><pe:NumberValidator
                                                ID="ValeKeyWord" ControlToValidate="TxtKeyWord" Display="Dynamic" ErrorMessage="请填写整数！"
                                                runat="server"></pe:NumberValidator>&nbsp;<br />
                                            <asp:RadioButton ID="RadDate" runat="server" GroupName="FieldSet" Text="保存为日期" />
                                            <br />
                                            <asp:CheckBox ID="ChkDesignated" runat="server" Text="是否保存为指定信息" /><asp:TextBox ID="TxtDesignated"
                                                runat="server" Text=""></asp:TextBox>&nbsp;<br />
                                            <asp:CheckBox ID="SavePhoto" runat="server" Text="是否保存远程图片" />
                                            <asp:Button ID="BtnField" runat="server" Text="测试一下" OnClick="BtnField_Click" ValidationGroup="Field" /></td>
                                    </tr>
                                </tbody>
                                <tbody id="PnlPaing" visible="false" runat="server">
                                    <tr class="tdbg">
                                        <td style="height: 40%" valign="top">
                                            选择分页类型：<br />
                                            <asp:RadioButton ID="RadlPaingType1" Text="从源代码中获取下一页的URL" GroupName="RadlPaingType"
                                                runat="server" Checked="true" />
                                            <asp:RadioButton ID="RadlPaingType2" Text="批量指定分页URL代码" GroupName="RadlPaingType"
                                                runat="server" />
                                            <asp:RadioButton ID="RadlPaingType3" Text="手动添加分页URL代码" GroupName="RadlPaingType"
                                                runat="server" />
                                        </td>
                                    </tr>
                                    <tr class="tdbg" id="ListPaing1" style="display: ">
                                        <td style="height: 40%" valign="top">
                                            “下一页”URL开始代码：↓<br />
                                            <asp:TextBox ID="TxtPaingBegin" runat="server" Height="80px" TextMode="MultiLine"
                                                Width="80%"></asp:TextBox>
                                            <br />
                                            <pe:RequiredFieldValidator ID="ValePaingBegin" ControlToValidate="TxtPaingBegin"
                                                ErrorMessage="“下一页”URL开始代码不能为空！" runat="server" ValidationGroup="ListPaing"></pe:RequiredFieldValidator>
                                            <br />
                                            “下一页”URL结束代码：↓<br />
                                            <asp:TextBox ID="TxtPaingEnd" runat="server" Height="80px" TextMode="MultiLine" Width="80%"></asp:TextBox>
                                            <br />
                                            <pe:RequiredFieldValidator ID="ValePaingEnd" ControlToValidate="TxtPaingEnd" ErrorMessage="“下一页”URL结束代码不能为空！"
                                                runat="server" ValidationGroup="ListPaing"></pe:RequiredFieldValidator>
                                            <br />
                                            <asp:Button ID="BtnPaingType1" runat="server" Text="测试一下" OnClick="BtnPaingType1_Click"
                                                ValidationGroup="ListPaing" />
                                        </td>
                                    </tr>
                                    <tr class="tdbg" id="ListPaing2" style="display: none">
                                        <td style="height: 40%" valign="top">
                                            URL字符串：<asp:TextBox ID="TxtPaingAddress" runat="server" Width="80%"></asp:TextBox>
                                            <pe:RequiredFieldValidator ID="ValePaingAddress" ControlToValidate="TxtPaingAddress"
                                                ErrorMessage="URL列表不能为空！" runat="server" ValidationGroup="ListPaing2"></pe:RequiredFieldValidator>
                                            <br />
                                            <span style="color: Green">例：http://www.xxxxx.com/news/index_{$ID}.html&nbsp;&nbsp;&nbsp;&nbsp;{$ID}代表分页数</span>
                                            <br />
                                            ID范围：<asp:TextBox ID="TxtScopeBegin" runat="server" Width="50px" /><span lang="en-us">
                                                To </span>
                                            <asp:TextBox ID="TxtScopeEnd" runat="server" Width="50px" />
                                            <pe:NumberValidator ID="ValeScopeBegin" ControlToValidate="TxtScopeBegin" Display="Dynamic"
                                                ErrorMessage="请填写整数！" runat="server" ValidationGroup="ListPaing2"></pe:NumberValidator>
                                            <pe:NumberValidator ID="ValeScopeEnd" ControlToValidate="TxtScopeEnd" Display="Dynamic"
                                                ErrorMessage="请填写整数！" runat="server" ValidationGroup="ListPaing2"></pe:NumberValidator>
                                            <br />
                                            <span style="color: Green">例： 1 ~ 9 或 9 ~ 1 升序或倒序采集</span>
                                            <br />
                                            <asp:Button ID="BtnPaingType2" runat="server" Text="测试一下" OnClick="BtnPaingType2_Click"
                                                ValidationGroup="ListPaing2" />
                                        </td>
                                    </tr>
                                    <tr class="tdbg" style="display: none" id="ListPaing3">
                                        <td style="height: 40%" valign="top">
                                            URL列表：↓<br />
                                            <asp:TextBox ID="TxtListPaing" runat="server" Height="120px" TextMode="MultiLine"
                                                Width="80%"></asp:TextBox>
                                            <br />
                                            <pe:RequiredFieldValidator ID="ValeListPaing" ControlToValidate="TxtListPaing" ErrorMessage="URL列表不能为空！"
                                                runat="server" ValidationGroup="ListPaing3"></pe:RequiredFieldValidator>
                                            <br />
                                            <span style="color: Green">注：一行写一个网页地址</span>
                                            <br />
                                            <asp:Button ID="BtnPaingType3" runat="server" Text="测试一下" OnClick="BtnPaingType3_Click"
                                                ValidationGroup="ListPaing3" />
                                        </td>
                                    </tr>
                                </tbody>
                                <tbody id="PnlFilter" visible="false" runat="server">
                                    <tr class="tdbg">
                                        <td style="height: 40%" valign="top">
                                            过滤选项↓<br />
                                            <asp:CheckBoxList ID="ChkFilterSelect" runat="server" RepeatDirection="Horizontal"
                                                CellPadding="3" RepeatColumns="3">
                                                <asp:ListItem Value="Iframe">过滤内联页</asp:ListItem>
                                                <asp:ListItem Value="Object">过滤Falsh广告,控件等</asp:ListItem>
                                                <asp:ListItem Value="Script">过滤js、vbs等脚本</asp:ListItem>
                                                <asp:ListItem Value="Style">过滤样式</asp:ListItem>
                                                <asp:ListItem Value="Div">过滤层</asp:ListItem>
                                                <asp:ListItem Value="Span">过滤行内元素Span容器</asp:ListItem>
                                                <asp:ListItem Value="Table">过滤表格</asp:ListItem>
                                                <asp:ListItem Value="Img">过滤图片</asp:ListItem>
                                                <asp:ListItem Value="Font">过滤字体(字留下字体去掉)</asp:ListItem>
                                                <asp:ListItem Value="A">过滤链接 (字留下链接去掉)</asp:ListItem>
                                                <asp:ListItem Value="Html">过滤html元素</asp:ListItem>
                                            </asp:CheckBoxList></td>
                                    </tr>
                                    <tr class="tdbg">
                                        <td style="height: 40%" valign="top">
                                            过滤指定代码↓<br />
                                            <asp:RadioButton ID="RadlFilterType1" Text="不过滤" GroupName="RadlPaingType" runat="server"
                                                Checked="true" />
                                            <asp:RadioButton ID="RadlFilterType2" Text="一般过滤" GroupName="RadlPaingType" runat="server" />
                                            <asp:RadioButton ID="RadlFilterType3" Text="高级过滤" GroupName="RadlPaingType" runat="server" />
                                        </td>
                                    </tr>
                                    <tr class="tdbg" id="filterType1" style="display: none">
                                        <td style="height: 40%" valign="top">
                                            要过滤的代码：↓<br />
                                            <asp:TextBox ID="TxtFilter" runat="server" Height="80px" TextMode="MultiLine" Width="80%"></asp:TextBox>
                                            <br />
                                            <pe:RequiredFieldValidator ID="ValeFilter" ControlToValidate="TxtFilter" ErrorMessage="要过滤的代码不能为空！"
                                                runat="server" ValidationGroup="Filter1"></pe:RequiredFieldValidator>
                                            <br />
                                            要替换的代码：↓<br />
                                            <asp:TextBox ID="TxtReplace" runat="server" Height="80px" TextMode="MultiLine" Width="80%"></asp:TextBox>
                                            <br />
                                            <asp:Button ID="BtnReplace1" runat="server" Text="测试一下" OnClick="BtnReplace1_Click"
                                                ValidationGroup="Filter1" />
                                        </td>
                                    </tr>
                                    <tr class="tdbg" id="filterType2" style="display: none">
                                        <td style="height: 40%" valign="top">
                                            要过滤的开始代码：↓<br />
                                            <asp:TextBox ID="TxtFilterBegin" runat="server" Height="80px" TextMode="MultiLine"
                                                Width="80%"></asp:TextBox>
                                            <br />
                                            <pe:RequiredFieldValidator ID="ValeFilterBegin" ControlToValidate="TxtFilterBegin"
                                                ErrorMessage="要过滤的开始代码不能为空！" runat="server" ValidationGroup="Filter2"></pe:RequiredFieldValidator>
                                            <br />
                                            要过滤的结束代码：↓<br />
                                            <asp:TextBox ID="TxtFilterEnd" runat="server" Height="80px" TextMode="MultiLine"
                                                Width="80%"></asp:TextBox>
                                            <br />
                                            <pe:RequiredFieldValidator ID="ValeFilterEnd" ControlToValidate="TxtFilterEnd" ErrorMessage="要过滤的结束代码不能为空！"
                                                runat="server" ValidationGroup="Filter2"></pe:RequiredFieldValidator>
                                            <br />
                                            要替换的代码：↓<br />
                                            <asp:TextBox ID="TxtReplace2" runat="server" Height="80px" TextMode="MultiLine" Width="80%"></asp:TextBox>
                                            <br />
                                            <asp:Button ID="BtnReplace2" runat="server" Text="测试一下" OnClick="BtnReplace2_Click"
                                                ValidationGroup="Filter2" />
                                        </td>
                                    </tr>
                                    <tr class="tdbg" id="filterType3">
                                        <td style="height: 40%" valign="top">
                                            <asp:Button ID="BtnReplace" runat="server" Text="测试一下" OnClick="BtnReplace_Click" />
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="tdbg">
            <td colspan="2">
                <span style="color: Green">注：</span>小技巧，选取左侧的代码按住 Ctrl 键不放，可以托放到右侧</td>
        </tr>
    </table>
    <br />
    <center>
        <br />
        <asp:Button ID="BtnSubmit" runat="server" Text="保存采集规则" OnClick="BtnSubmit_Click" />&nbsp;&nbsp;
        <asp:Button ID="BtnCancle" runat="server" Text="返回采集管理" ValidationGroup="BtnCancleValidationGroup"
            OnClick="BtnCancle_Click" />
        <asp:HiddenField ID="HdnCollectionType" runat="server" />
    </center>
</asp:Content>
