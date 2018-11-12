<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.CommonModel.Field"
    ValidateRequest="false" Title="添加字段" Codebehind="Field.aspx.cs" %>

<asp:Content ID="Content3" ContentPlaceHolderID="CphNavigation" runat="Server">
    <table style="width: 100%; margin: 0 auto;" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
            </td>
            <td align="right">
                <pe:ExtendedLabel HtmlEncode="false" ID="LblModelName" runat="server" Text=""></pe:ExtendedLabel>
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CphContent" runat="Server">

    <script type="text/javascript">
function SetLitFieldName(value)
{
    document.getElementById("<%= LblFieldName.ClientID %>").innerHTML = value;
}
    </script>

    <table cellpadding="2" cellspacing="1" border="0" width="100%" class="border">
        <tr>
            <td class="title" colspan="2" align="center">
                <asp:Label ID="LblTitle" runat="server" Text="添加字段" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>字段名称：</strong>
            </td>
            <td align="left">
                <asp:TextBox ID="TxtFieldName" runat="server" MaxLength="21"></asp:TextBox>
                <pe:RequiredFieldValidator ID="ValrFieldName" ControlToValidate="TxtFieldName" runat="server"
                    ErrorMessage="字段名称不能为空" Display="Dynamic"></pe:RequiredFieldValidator>
                <pe:FieldValidator ControlToValidate="TxtFieldName" ID="FieldValidator1" runat="server"></pe:FieldValidator>
                <br />
                <span style="color: Blue">注：字段名由字母、数字、下划线组成，并且仅能字母开头，不以下划线结尾。 例如：Content</span>
                <br />
                可以在模板中用数据源标签调用该字段内容，调用形式例如：<br />
                {PE.Field ID="数据源标签ID" fieldname="<asp:Label ID="LblFieldName" Style="color: Red;"
                    runat="server"></asp:Label>" /}
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>字段别名：</strong>
            </td>
            <td align="left">
                <asp:TextBox ID="TxtFieldAliax" runat="server"></asp:TextBox>
                <br />
                <span style="color: blue">例如：文章内容</span>
                <pe:RequiredFieldValidator ID="ValrFieldAliax" ControlToValidate="TxtFieldAliax"
                    runat="server" ErrorMessage="字段别名不能为空" Display="Dynamic"></pe:RequiredFieldValidator>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>字段提示：</strong>
            </td>
            <td align="left">
                <asp:TextBox ID="TxtTips" runat="server"></asp:TextBox>
                <span style="color: Blue">显示在字段别名下方作为重要提示的文字</span>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>字段描述：</strong>
            </td>
            <td align="left">
                <asp:TextBox ID="TxtDescription" runat="server" Height="43px" TextMode="MultiLine"
                    Width="208px"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>是否必填：</strong>
            </td>
            <td align="left">
                <asp:RadioButtonList ID="RadlEnableNull" runat="server" Height="3px" RepeatDirection="Horizontal">
                    <asp:ListItem Value="True">是</asp:ListItem>
                    <asp:ListItem Value="False" Selected="True">否</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>是否在搜索表单显示：</strong>
            </td>
            <td align="left">
                <asp:RadioButtonList ID="RadlEnableShowOnSearchForm" runat="server" Height="3px"
                    RepeatDirection="Horizontal">
                    <asp:ListItem Value="True">是</asp:ListItem>
                    <asp:ListItem Value="False" Selected="True">否</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>此字段 的类型：</strong>
            </td>
            <td id="TdFieldType" runat="server" align="left">
                <table>
                    <tr>
                        <td>
                            <strong>基本字段</strong>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButton ID="RadTextType" Checked="true" Text="单行文本" AutoPostBack="true"
                                GroupName="RadFieldType" runat="server" OnCheckedChanged="RadlFieldType_SelectedIndexChanged" />
                        </td>
                        <td>
                            <asp:RadioButton ID="RadMultipleTextType" AutoPostBack="true" Text="多行文本（不支持HTML）"
                                GroupName="RadFieldType" runat="server" OnCheckedChanged="RadlFieldType_SelectedIndexChanged" />
                        </td>
                        <td>
                            <asp:RadioButton ID="RadMultipleHtmlTextType" AutoPostBack="true" Text="多行文本（支持HTML）"
                                GroupName="RadFieldType" runat="server" OnCheckedChanged="RadlFieldType_SelectedIndexChanged" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButton ID="RadListBoxType" AutoPostBack="true" Text="选项" GroupName="RadFieldType"
                                runat="server" OnCheckedChanged="RadlFieldType_SelectedIndexChanged" />
                        </td>
                        <td>
                            <asp:RadioButton ID="RadNumberType" AutoPostBack="true" Text="数字" GroupName="RadFieldType"
                                runat="server" OnCheckedChanged="RadlFieldType_SelectedIndexChanged" />
                        </td>
                        <td>
                            <asp:RadioButton ID="RadMoneyType" AutoPostBack="true" Text="货币" GroupName="RadFieldType"
                                runat="server" OnCheckedChanged="RadlFieldType_SelectedIndexChanged" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButton ID="RadDateTimeType" AutoPostBack="true" Text="日期和时间" GroupName="RadFieldType"
                                runat="server" OnCheckedChanged="RadlFieldType_SelectedIndexChanged" />
                        </td>
                        <td>
                            <asp:RadioButton ID="RadLookType" AutoPostBack="true" Text="查阅项" GroupName="RadFieldType"
                                runat="server" OnCheckedChanged="RadlFieldType_SelectedIndexChanged" />
                        </td>
                        <td>
                            <asp:RadioButton ID="RadLinkType" AutoPostBack="true" Text="超链接" GroupName="RadFieldType"
                                runat="server" OnCheckedChanged="RadlFieldType_SelectedIndexChanged" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButton ID="RadBoolType" AutoPostBack="true" Text="是/否（复选框）" GroupName="RadFieldType"
                                runat="server" OnCheckedChanged="RadlFieldType_SelectedIndexChanged" />
                        </td>
                        <td>
                            <asp:RadioButton ID="RadPictureType" AutoPostBack="true" Text="图片" GroupName="RadFieldType"
                                runat="server" OnCheckedChanged="RadlFieldType_SelectedIndexChanged" />
                            <%--<asp:RadioButton ID="RadCountType" AutoPostBack="true" Text="计算值（根据其他字段的计算）" GroupName="RadFieldType"
                                runat="server" OnCheckedChanged="RadlFieldType_SelectedIndexChanged" />--%>
                        </td>
                        <td>
                            <asp:RadioButton ID="RadFileType" AutoPostBack="true" Text="文件" GroupName="RadFieldType"
                                runat="server" OnCheckedChanged="RadlFieldType_SelectedIndexChanged" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButton ID="RadColorType" AutoPostBack="true" Text="颜色代码" GroupName="RadFieldType"
                                runat="server" OnCheckedChanged="RadlFieldType_SelectedIndexChanged" />
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>系统预定义字段</strong>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButton ID="RadOperatingType" AutoPostBack="true" Text="运行平台" GroupName="RadFieldType"
                                runat="server" OnCheckedChanged="RadlFieldType_SelectedIndexChanged" />
                        </td>
                        <td>
                            <asp:RadioButton ID="RadProducer" AutoPostBack="true" Text="厂商" GroupName="RadFieldType"
                                runat="server" OnCheckedChanged="RadlFieldType_SelectedIndexChanged" />
                        </td>
                        <td>
                            <asp:RadioButton ID="RadKeywordType" AutoPostBack="true" Text="关键字" GroupName="RadFieldType"
                                runat="server" OnCheckedChanged="RadlFieldType_SelectedIndexChanged" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButton ID="RadContentType" AutoPostBack="true" Text="内容" GroupName="RadFieldType"
                                runat="server" OnCheckedChanged="RadlFieldType_SelectedIndexChanged" />
                        </td>
                        <td>
                            <asp:RadioButton ID="RadMultiplePhotoType" runat="server" AutoPostBack="true" GroupName="RadFieldType"
                                OnCheckedChanged="RadlFieldType_SelectedIndexChanged" Text="多图片" />
                        </td>
                        <td>
                            <asp:RadioButton ID="RadTrademark" AutoPostBack="true" Text="品牌" GroupName="RadFieldType"
                                runat="server" OnCheckedChanged="RadlFieldType_SelectedIndexChanged" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButton ID="RadAuthorType" AutoPostBack="true" Text="作者" GroupName="RadFieldType"
                                runat="server" OnCheckedChanged="RadlFieldType_SelectedIndexChanged" />
                        </td>
                        <td>
                            <asp:RadioButton ID="RadSourceType" AutoPostBack="true" Text="来源" GroupName="RadFieldType"
                                runat="server" OnCheckedChanged="RadlFieldType_SelectedIndexChanged" />
                        </td>
                        <td>
                            <asp:RadioButton ID="RadDownServerType" AutoPostBack="true" Text="下载服务器" GroupName="RadFieldType"
                                runat="server" OnCheckedChanged="RadlFieldType_SelectedIndexChanged" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButton ID="RadSpecialType" AutoPostBack="true" Text="专题" GroupName="RadFieldType"
                                runat="server" OnCheckedChanged="RadlFieldType_SelectedIndexChanged" />
                        </td>
                        <td>
                            <asp:RadioButton ID="RadSkinType" AutoPostBack="true" Text="风格" GroupName="RadFieldType"
                                runat="server" OnCheckedChanged="RadlFieldType_SelectedIndexChanged" />
                        </td>
                        <td>
                            <asp:RadioButton ID="RadTemplateType" AutoPostBack="true" Text="模板" GroupName="RadFieldType"
                                runat="server" OnCheckedChanged="RadlFieldType_SelectedIndexChanged" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButton ID="RadInfoType" AutoPostBack="true" Text="虚链接" GroupName="RadFieldType"
                                runat="server" OnCheckedChanged="RadlFieldType_SelectedIndexChanged" />
                        </td>
                        <td>
                            <asp:RadioButton ID="RadStatusType" AutoPostBack="true" Text="状态" GroupName="RadFieldType"
                                runat="server" OnCheckedChanged="RadlFieldType_SelectedIndexChanged" />
                        </td>
                        <td>
                            <asp:RadioButton ID="RadNodeType" AutoPostBack="true" Text="节点" GroupName="RadFieldType"
                                runat="server" OnCheckedChanged="RadlFieldType_SelectedIndexChanged" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButton ID="RadTitleType" AutoPostBack="true" Text="标题" GroupName="RadFieldType"
                                runat="server" OnCheckedChanged="RadlFieldType_SelectedIndexChanged" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButton ID="RadProperty" AutoPostBack="true" Text="商品属性" GroupName="RadFieldType"
                                runat="server" OnCheckedChanged="RadlFieldType_SelectedIndexChanged" />
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tbody id="PnlText" visible="false" runat="server">
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>最大字符数：</strong>
                </td>
                <td align="left">
                    <asp:TextBox ID="TxtTextMaxLength" Text="200" runat="server" Columns="5"></asp:TextBox>
                    <pe:RequiredFieldValidator ID="ValrTextMaxLength" ControlToValidate="TxtTextMaxLength"
                        runat="server" ErrorMessage="最大字符数不能为空" Display="Dynamic"></pe:RequiredFieldValidator>
                    <asp:RangeValidator ID="ValgTextMaxLength" ControlToValidate="TxtTextMaxLength" runat="server"
                        ErrorMessage="超出数据范围！" Type="Integer" Display="Dynamic" MaximumValue="255" MinimumValue="1"></asp:RangeValidator>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>文本框长度：</strong>
                </td>
                <td align="left">
                    <asp:TextBox ID="TxtTextSize" Text="30" runat="server" Columns="5"></asp:TextBox>
                    <pe:RequiredFieldValidator ID="ValrTextSize" ControlToValidate="TxtTextSize" runat="server"
                        ErrorMessage="文本框长度不能为空" Display="Dynamic"></pe:RequiredFieldValidator>
                    <asp:RangeValidator ID="ValgTextSize" ControlToValidate="TxtTextSize" Type="Integer"
                        runat="server" ErrorMessage="超出数据范围！" Display="Dynamic" MaximumValue="250" MinimumValue="1"></asp:RangeValidator>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>默认值：</strong>
                </td>
                <td align="left">
                    <asp:TextBox ID="TxtTextDefaultValue" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>输入法设置：</strong>
                </td>
                <td align="left">
                    <asp:DropDownList ID="DropTextIMEMode" runat="server">
                        <asp:ListItem Value="0" Selected="True">默认</asp:ListItem>
                        <asp:ListItem Value="1">禁用</asp:ListItem>
                        <asp:ListItem Value="2">启用</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <pe:AttachFieldControl ID="AttachSingle" runat="server"></pe:AttachFieldControl>
        </tbody>
        <tbody id="PnlMultiText" visible="false" runat="server">
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>显示的宽度：</strong>
                </td>
                <td align="left">
                    <asp:TextBox ID="TxtMultiTextWidth" Text="500" runat="server" Columns="5"></asp:TextBox>
                    <span style="color: Blue">px &nbsp;例如：500px</span>
                    <pe:RequiredFieldValidator ID="ValrMultiTextWidth" runat="server" ControlToValidate="TxtMultiTextWidth"
                        Display="Dynamic" ErrorMessage="显示宽度不能为空"></pe:RequiredFieldValidator>
                    <pe:NumberValidator ID="NumberValidator10" ControlToValidate="TxtMultiTextWidth"
                        runat="server">
                    </pe:NumberValidator>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>显示的高度：</strong>
                </td>
                <td align="left">
                    <asp:TextBox ID="TxtMultiTextRow" Text="80" runat="server" Columns="5"></asp:TextBox>
                    <span style="color: Blue">px &nbsp;例如：80px</span>
                    <pe:RequiredFieldValidator ID="ValrMultiTextRow" runat="server" ControlToValidate="TxtMultiTextRow"
                        Display="Dynamic" ErrorMessage="显示高度不能为空"></pe:RequiredFieldValidator>
                    <pe:NumberValidator ID="NumberValidator9" ControlToValidate="TxtMultiTextRow" runat="server">
                    </pe:NumberValidator>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>默认值：</strong>
                </td>
                <td align="left">
                    <asp:TextBox ID="TxtMultiDefault" runat="server"></asp:TextBox>
                </td>
            </tr>
            <pe:AttachFieldControl ID="AttachMulit" runat="server"></pe:AttachFieldControl>
        </tbody>
        <tbody id="PnlEditor" visible="false" runat="server">
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>编辑器类型：</strong>
                </td>
                <td align="left">
                    <asp:DropDownList ID="DropEditorType" runat="server">
                        <asp:ListItem Value="1" Selected="True">简洁型编辑器</asp:ListItem>
                        <asp:ListItem Value="2">标准型编辑器</asp:ListItem>
                        <asp:ListItem Value="3">增强型编辑器</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>编辑器大小：</strong>
                </td>
                <td align="left">
                    宽
                    <asp:TextBox ID="TxtEditorWidth" Text="600" runat="server" Columns="5"></asp:TextBox>
                    <span style="color: Blue">px&nbsp;例如：600px</span>
                    <pe:RequiredFieldValidator ID="ValrEditorWidth" runat="server" ControlToValidate="TxtEditorWidth"
                        Display="Dynamic" ErrorMessage="显示宽度不能为空"></pe:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="ValeEditorWidth" runat="server" ControlToValidate="TxtEditorWidth"
                        ErrorMessage="只能输入字母数字百分号" ValidationExpression="^[a-zA-Z%0-9]*$" Display="Dynamic"></asp:RegularExpressionValidator>
                    <br />
                    高
                    <asp:TextBox ID="TxtEditorHight" Text="500" runat="server" Columns="5"></asp:TextBox>
                    <span style="color: Blue">px&nbsp;例如：350px</span>
                    <pe:RequiredFieldValidator ID="ValrEditorHight" runat="server" ControlToValidate="TxtEditorHight"
                        Display="Dynamic" ErrorMessage="显示高度不能为空"></pe:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="ValeEditorHight" runat="server" ControlToValidate="TxtEditorHight"
                        ErrorMessage="只能输入字母数字百分号" ValidationExpression="^[a-zA-Z%0-9]*$" Display="Dynamic"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>默认值：</strong>
                </td>
                <td align="left">
                    <asp:TextBox ID="TxtMulitHtmlDefault" runat="server"></asp:TextBox>
                </td>
            </tr>
            <pe:AttachFieldControl ID="AttachMulitHtml" runat="server"></pe:AttachFieldControl>
        </tbody>
        <tbody id="PnlChoice" visible="false" runat="server">
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>每个选项：</strong>
                </td>
                <td align="left">

                    <script type="text/javascript">
                    function AddUrl(){
                      var thisurl='选项名称'+(document.getElementById("<%= ChoiceUrl.ClientID %>").length+1)+'|选项值'; 
                      var url=prompt('请输入选项名称和值，中间用“|”隔开：',thisurl);
                      if(url!=null&&url!=''){document.getElementById("<%= ChoiceUrl.ClientID %>").options[document.getElementById("<%= ChoiceUrl.ClientID %>").length]=new Option(url,url);}
                    }
                    function ModifyUrl(){
                      if(document.getElementById("<%= ChoiceUrl.ClientID %>").length==0) return false;
                      var thisurl=document.getElementById("<%= ChoiceUrl.ClientID %>").value; 
                      if (thisurl=='') {alert('请先选择一个选项，再点修改按钮！');return false;}
                      var url=prompt('请输入选项名称和值，中间用“|”隔开：',thisurl);
                      if(url!=thisurl&&url!=null&&url!=''){document.getElementById("<%= ChoiceUrl.ClientID %>").options[document.getElementById("<%= ChoiceUrl.ClientID %>").selectedIndex]=new Option(url,url);}
                    }
                    function DelUrl(){
                      if(document.getElementById("<%= ChoiceUrl.ClientID %>").length==0) return false;
                      var thisurl=document.getElementById("<%= ChoiceUrl.ClientID %>").value; 
                      if (thisurl=='') {alert('请先选择一个选项，再点删除按钮！');return false;}
                      document.getElementById("<%= ChoiceUrl.ClientID %>").options[document.getElementById("<%= ChoiceUrl.ClientID %>").selectedIndex]=null;
                    }
                    
                    function ChangeHiddenFieldValue()
                    {
                        var obj = document.getElementById("<%= HdnChoiceUrls.ClientID %>");
                        var choiceUrl = document.getElementById("<%= ChoiceUrl.ClientID %>");
                        var Default=document.getElementById('<%=TxtChoiceDefaultValue.ClientID %>').value;
                        var bFoundMatchWithDefault = false;
                                
                        var value = "";
                        if(choiceUrl.length < 1){
                            alert("请添加选项！");
                            return false;
                        }
                        
                        for(i=0;i<choiceUrl.length;i++)
                        {
                            if(value!="")
                            {
                                value = value+ "$$$";
                            }
                            value = value + choiceUrl.options[i].value;

                            if (Default == choiceUrl.options[i].value.split("|")[1])
                            {
                                bFoundMatchWithDefault = true;              
                            }
            
                        }
                        obj.value = value;
                        
                        if(Default && !bFoundMatchWithDefault)
                        {
                            alert("“选项”域的默认值必须从指定的选项中选择。请再试一次。");
                            return false;
                        }
                        else
                        {
                            obj.value = value;
                            return true;
                        }
        
                        return true;
                    }

                    </script>

                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td align="left" style="width: 400px;">
                                <asp:HiddenField ID="HdnChoiceUrls" runat="server" />
                                <select id="ChoiceUrl" runat="server" style="width: 350px; height: 100px" size="2"
                                    ondblclick="return ModifyUrl();">
                                </select>
                            </td>
                            <td align="left">
                                <input type="button" name="addurl" value="添加选项    " onclick="AddUrl();" /><br />
                                <input type="button" name="modifyurl" value="修改当前选项" onclick="return ModifyUrl();" /><br />
                                <input type="button" name="delurl" value="删除当前选项" onclick="DelUrl();" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <span style="color: Green">注</span>：显示数据|保存数据，如果添加空数据可写 “无|”
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>显示选项使用：</strong>
                </td>
                <td align="left">
                    <asp:RadioButtonList ID="RadlChoiceType" runat="server">
                        <asp:ListItem Value="1" Selected="True">单选下拉列表框</asp:ListItem>
                        <asp:ListItem Value="2">多选列表框</asp:ListItem>
                        <asp:ListItem Value="3">单选按钮</asp:ListItem>
                        <asp:ListItem Value="4">复选框</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>允许“填充” 选项：</strong>
                </td>
                <td align="left">
                    <asp:RadioButtonList ID="RadlEnableFill" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="True">是</asp:ListItem>
                        <asp:ListItem Value="False" Selected="True">否</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>默认值：</strong>
                </td>
                <td align="left">
                    <asp:TextBox ID="TxtChoiceDefaultValue" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>每行显示项数：</strong>
                </td>
                <td align="left">
                    <asp:TextBox ID="TxtRepeatColumns" Text="1" runat="server"></asp:TextBox><asp:CompareValidator
                        ID="CompareValTxtRepeatColumns" ControlToValidate="TxtRepeatColumns" Display="Dynamic"
                        Operator="greaterThanEqual" Type="Integer" ValueToCompare="1" runat="server"
                        ErrorMessage="必须填写大于1的数字"></asp:CompareValidator><br />
                    <span style="color: Blue">只当类型为单选按钮或复选框时起作用</span>
                </td>
            </tr>
        </tbody>
        <tbody id="PnlNumber" visible="false" runat="server">
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>最小值：</strong>
                </td>
                <td align="left">
                    <asp:TextBox ID="TxtNumberMinLength" runat="server" Text=""></asp:TextBox>
                    <pe:NumberValidator ID="NumberValidator6" ControlToValidate="TxtNumberMinLength"
                        runat="server" Display="Dynamic">
                    </pe:NumberValidator>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>最大值：</strong>
                </td>
                <td align="left">
                    <asp:TextBox ID="TxtNumberMaxlength" runat="server" Text=""></asp:TextBox>
                    <asp:CompareValidator ID="ValcNumber" runat="server" ControlToCompare="TxtNumberMinLength"
                        ControlToValidate="TxtNumberMaxlength" ErrorMessage="CompareValidator" Operator="GreaterThan"
                        Type="Double" Display="Dynamic">最大值不能小于或等于最小值</asp:CompareValidator>
                    <pe:NumberValidator ID="NumberValidator5" ControlToValidate="TxtNumberMaxlength"
                        runat="server" Display="Dynamic">
                    </pe:NumberValidator>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>小数位数：</strong>
                </td>
                <td align="left">
                    <asp:DropDownList ID="DropNumberDecimals" runat="server">
                        <asp:ListItem Value="-1" Selected="True">自动</asp:ListItem>
                        <asp:ListItem>0</asp:ListItem>
                        <asp:ListItem>1</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>默认值：</strong>
                </td>
                <td align="left">
                    <asp:TextBox ID="TxtNumberDefaultValue" runat="server"></asp:TextBox>
                    <pe:NumberValidator ID="NumberValidator7" ControlToValidate="TxtNumberDefaultValue"
                        runat="server" Display="Dynamic"></pe:NumberValidator>
                    <br />
                    <asp:CheckBox ID="ChkNumberPercent" runat="server" />以百分比显示(例如：50%)
                </td>
            </tr>
        </tbody>
        <tbody id="PnlCurrency" visible="false" runat="server">
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>最小值：</strong>
                </td>
                <td align="left">
                    <asp:TextBox ID="TxtCurrencyMinLength" runat="server"></asp:TextBox>
                    <span style="color: Blue">例如：1.00</span>
                    <asp:RegularExpressionValidator ID="ValeCurrencyMinLength" runat="server" ControlToValidate="TxtCurrencyMinLength"
                        ErrorMessage="只能输入货币字符" ValidationExpression="^-?[0-9]+(\.?[0-9]{1,4})?" Display="Dynamic"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>最大值：</strong>
                </td>
                <td align="left">
                    <asp:TextBox ID="TxtCurrencyMaxLength" runat="server"></asp:TextBox>
                    <asp:CompareValidator ID="ValCurrency" runat="server" ErrorMessage="CompareValidator"
                        ControlToCompare="TxtCurrencyMinLength" ControlToValidate="TxtCurrencyMaxLength"
                        Operator="GreaterThan" Type="Double" Display="Dynamic">最大值不能小于或等于最小值</asp:CompareValidator>
                    <asp:RegularExpressionValidator ID="ValeCurrencyMaxLength" runat="server" ControlToValidate="TxtCurrencyMaxLength"
                        ErrorMessage="只能输入货币字符" ValidationExpression="^-?[0-9]+(\.?[0-9]{1,4})?" Display="Dynamic"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>默认值：</strong>
                </td>
                <td align="left">
                    <asp:TextBox ID="TxtCurrencyDefaultValue" runat="server"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="ValeCurrencyDefaultValue" runat="server" ControlToValidate="TxtCurrencyDefaultValue"
                        ErrorMessage="只能输入货币字符" ValidationExpression="^-?[0-9]+(\.?[0-9]{1,2})?" Display="Dynamic"></asp:RegularExpressionValidator>
                </td>
            </tr>
        </tbody>
        <tbody id="PnlDateTime" visible="false" runat="server">
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>日期和时间格式：</strong>
                </td>
                <td align="left">
                    <asp:RadioButtonList ID="RadioDateTimeType" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="yyyy-MM-dd" Selected="True">仅日期</asp:ListItem>
                        <asp:ListItem Value="yyyy-MM-dd HH:mm:ss">日期和时间</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>默认值：</strong>
                </td>
                <td align="left">
                    <table cellpadding="0" cellspacing="0">
                        <tr>
                            <td align="left">
                                <asp:RadioButtonList ID="RadlDateTimeDefaultType" runat="server">
                                    <asp:ListItem Value="0" Selected="True">无</asp:ListItem>
                                    <asp:ListItem Value="1">当前日期</asp:ListItem>
                                    <asp:ListItem Value="2">指定日期</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td align="right">
                                <br />
                                <br />
                                <br />
                                <pe:DatePicker ID="DpkDateTimeInputDefaultValue" runat="server"></pe:DatePicker>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </tbody>
        <tbody id="PnlLookup" visible="false" runat="server">
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>获取信息来自模型：</strong>
                </td>
                <td align="left">
                    <asp:DropDownList ID="DropLookupTable" runat="server" OnSelectedIndexChanged="DropLookupTable_SelectedIndexChanged"
                        AutoPostBack="True">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>在此模型对应信息表的字段：</strong>
                </td>
                <td align="left">
                    <asp:DropDownList ID="DropLookupField" runat="server">
                    </asp:DropDownList>
                    <br />
                    <span style="color: Red">注：查阅项的字段只能是存在选定的模型对应表里面的单行文本字段</span>
                </td>
            </tr>
        </tbody>
        <tbody id="PnlURL" visible="false" runat="server">
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>最大字符数：</strong>
                </td>
                <td align="left">
                    <asp:TextBox ID="TxtURLMaxLength" runat="server" Columns="5"></asp:TextBox>
                    <pe:RequiredFieldValidator ID="ValeTxtURLMaxLength" ControlToValidate="TxtURLMaxLength"
                        runat="server" ErrorMessage="最大字符数不能为空" Display="Dynamic"></pe:RequiredFieldValidator>
                    <asp:RangeValidator ID="ValgURLMaxLength" Type="Integer" ControlToValidate="TxtURLMaxLength"
                        runat="server" ErrorMessage="超出数据范围！" Display="Dynamic" MaximumValue="255" MinimumValue="1"></asp:RangeValidator>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>文本框长度：</strong>
                </td>
                <td align="left">
                    <asp:TextBox ID="TxtURLSize" Text="80" runat="server" Columns="5"></asp:TextBox>
                    <pe:RequiredFieldValidator ID="ValeTxtURLSize" ControlToValidate="TxtURLSize" runat="server"
                        ErrorMessage="文本框长度不能为空" Display="Dynamic"></pe:RequiredFieldValidator>
                    <asp:RangeValidator ID="ValigURLSize" Type="Integer" ControlToValidate="TxtURLMaxLength"
                        runat="server" ErrorMessage="超出数据范围！" Display="Dynamic" MaximumValue="255" MinimumValue="1"></asp:RangeValidator>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>默认值：</strong>
                </td>
                <td align="left">
                    <asp:TextBox ID="TxtURLDefaultValue" runat="server"></asp:TextBox>
                    <span style="color: green">例：http://127.0.0.1/ </span>
                    <pe:UrlValidator ID="VurlURLDefaultValue" ControlToValidate="TxtURLDefaultValue"
                        runat="server"></pe:UrlValidator>
                </td>
            </tr>
        </tbody>
        <tbody id="PnlBoolean" visible="false" runat="server">
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>默认值：</strong>
                </td>
                <td align="left">
                    <asp:DropDownList ID="DropBoolean" runat="server">
                        <asp:ListItem Value="True" Selected="True">是</asp:ListItem>
                        <asp:ListItem Value="False">否</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </tbody>
        <tbody id="PnlImage" visible="false" runat="server">
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>图片文本框长度：</strong>
                </td>
                <td align="left">
                    <asp:TextBox ID="TxtImageTextLength" Text="30" runat="server" Columns="5"></asp:TextBox>
                    <pe:RequiredFieldValidator ID="ValrImageTextLength" ControlToValidate="TxtImageTextLength"
                        runat="server" ErrorMessage="文本框长度不能为空" Display="Dynamic"></pe:RequiredFieldValidator>
                    <asp:RangeValidator ID="ValgImageTextLength" ControlToValidate="TxtImageTextLength"
                        Type="Integer" runat="server" ErrorMessage="超出数据范围！" Display="Dynamic" MaximumValue="250"
                        MinimumValue="1"></asp:RangeValidator>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>是否启用上传：</strong>
                </td>
                <td align="left">
                    <asp:RadioButtonList ID="RadlIsUpload" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="True" Selected="True">是</asp:ListItem>
                        <asp:ListItem Value="False">否</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr class="tdbg" id="ImageSize">
                <td class="tdbgleft">
                    <strong>允许的图片大小：</strong>
                </td>
                <td align="left">
                    <asp:TextBox ID="TxtImageSize" Text="1024" runat="server" Columns="6" MaxLength="6"></asp:TextBox>
                    KB <span style="color: Blue">提示：1 KB = 1024 Byte，1 MB = 1024 KB</span>
                    <pe:RequiredFieldValidator ID="ValrImageSize" runat="server" ControlToValidate="TxtImageSize"
                        Display="Dynamic" ErrorMessage="允许的图片大小不能为空"></pe:RequiredFieldValidator>
                    <pe:NumberValidator ID="NumberValidator2" ControlToValidate="TxtImageSize" runat="server"
                        Display="Dynamic">
                    </pe:NumberValidator>
                </td>
            </tr>
            <tr class="tdbg" id="ImageType">
                <td class="tdbgleft">
                    <strong>允许的图片类型：</strong>
                </td>
                <td align="left">
                    <asp:TextBox ID="TextImageType" runat="server" Columns="30"></asp:TextBox>
                    <span style="color: Blue">注：允许多个类型请用“|”号分割，如：jpg|gif|bmp等等</span>
                    <pe:RequiredFieldValidator ID="ValrTextImageType" runat="server" ControlToValidate="TextImageType"
                        Display="Dynamic" ErrorMessage="图片类型不能为空"></pe:RequiredFieldValidator>
                </td>
            </tr>
            <tr class="tdbg" id="IsFromSelected">
                <td class="tdbgleft">
                    <strong>是否从系统已上传图片中选择：</strong>
                </td>
                <td align="left">
                    <asp:RadioButtonList ID="RadlIsFromSelected" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="True">是</asp:ListItem>
                        <asp:ListItem Value="False" Selected="True">否</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr class="tdbg" id="UploadFiles" runat="server" style="display: none;">
                <td class="tdbgleft">
                    <strong>是否从信息已上传图片中选择：</strong>
                </td>
                <td align="left">
                    <asp:RadioButtonList ID="RadlUploadFiles" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="True">是</asp:ListItem>
                        <asp:ListItem Value="False" Selected="True">否</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr class="tdbg" id="WsImage">
                <td class="tdbgleft">
                    <strong>图片是否加水印：</strong>
                </td>
                <td align="left">
                    <asp:RadioButtonList ID="RadlWsImage" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="True">是</asp:ListItem>
                        <asp:ListItem Value="False" Selected="True">否</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr class="tdbg" id="Thumb">
                <td class="tdbgleft">
                    <strong>是否上传缩略图：</strong>
                </td>
                <td align="left">
                    <asp:RadioButtonList ID="RadlThumb" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="True">是</asp:ListItem>
                        <asp:ListItem Value="False" Selected="True">否</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr class="tdbg" id="PicDefaultUrl">
                <td class="tdbgleft">
                    <strong>默认图片：</strong>
                </td>
                <td align="left">
                    <asp:TextBox ID="TxtPicDefaultUrl" runat="server"></asp:TextBox>
                </td>
            </tr>
        </tbody>
        <tbody id="PnlFile" visible="false" runat="server">
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>是否为下载频道控件：</strong>
                </td>
                <td align="left">
                    <asp:CheckBox ID="ChkDownLoadUrl" Checked="true" runat="server" />
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>保存文件大小设置：</strong>
                </td>
                <td align="left">
                    是否保存文件大小：<asp:CheckBox ID="ChkSoftSize" Checked="true" runat="server" Text="" /><br />
                    保存字段的名称：<asp:TextBox ID="TxtFileSizeField" runat="server" Width="80px" MaxLength="30"></asp:TextBox>
                    <pe:RequiredFieldValidator ID="ValrFileSizeField" runat="server" ControlToValidate="TxtFileSizeField"
                        Display="Dynamic" ErrorMessage="保存字段的名称不能为空"></pe:RequiredFieldValidator>
                    <pe:FieldValidator ControlToValidate="TxtFileSizeField" Display="Dynamic" ID="FieldValidator4"
                        runat="server"></pe:FieldValidator>
                    <asp:CompareValidator ID="ValcFileSizeField" runat="server" Display="Dynamic" ErrorMessage="保存文件大小的字段名不能与主字段名重复！"
                        ControlToValidate="TxtFileSizeField" ControlToCompare="TxtFieldName" Operator="NotEqual"
                        SetFocusOnError="True"></asp:CompareValidator>
                    <br />
                    <span style="color: Blue">注：字段名由字母、数字、下划线组成，并且仅能字母开头，不以下划线结尾。 例如：Content</span>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>允许的文件大小：</strong>
                </td>
                <td align="left">
                    <asp:TextBox ID="TxtFileSize" Text="1024" runat="server" Columns="6" MaxLength="6"></asp:TextBox>
                    KB <span style="color: Blue">提示：1 KB = 1024 Byte，1 MB = 1024 KB</span>
                    <pe:RequiredFieldValidator ID="ValrFileSize" runat="server" ControlToValidate="TxtFileSize"
                        Display="Dynamic" ErrorMessage="文件大小不能为空"></pe:RequiredFieldValidator>
                    <pe:NumberValidator ID="NumberValidator1" ControlToValidate="TxtFileSize" runat="server"
                        Display="Dynamic">
                    </pe:NumberValidator>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>允许的文件类型：</strong>
                </td>
                <td align="left">
                    <asp:TextBox ID="TextFileType" runat="server"></asp:TextBox>
                    <pe:RequiredFieldValidator ID="ValrFileType" runat="server" ControlToValidate="TextFileType"
                        Display="Dynamic" ErrorMessage="文件类型不能为空"></pe:RequiredFieldValidator>
                    <span style="color: Blue">注：允许多个类型请用“|”号分割，如：jpg|mp3|gif|rm|rmvb等等</span>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>默认值：</strong>
                </td>
                <td align="left">
                    <asp:TextBox ID="TxtFileDefaultValue" runat="server" Width="349px"></asp:TextBox>
                    <br />
                    <span style="color: Green">例：下载地址1|http://127.0.0.1/UploadFile/Soft/2007/2/moivename.rmvb</span>
                </td>
            </tr>
        </tbody>
        <tbody id="PnlColor" visible="false" runat="server">
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>默认颜色值：</strong>
                </td>
                <td align="left">
                    <pe:ColorPicker ID="CpkColorDefault" runat="server"></pe:ColorPicker>
                </td>
            </tr>
        </tbody>
        <tbody id="PnlAuthor" visible="false" runat="server">
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>文本框长度：</strong>
                </td>
                <td align="left">
                    <asp:TextBox ID="TxtAuthorSize" Text="50" runat="server" Columns="5"></asp:TextBox>
                    <pe:RequiredFieldValidator ID="RequiredFieldValidatorAuthor" ControlToValidate="TxtAuthorSize"
                        runat="server" ErrorMessage="文本框长度不能为空" Display="Dynamic"></pe:RequiredFieldValidator>
                    <pe:NumberValidator ID="NumberValidatorAuthor" ControlToValidate="TxtAuthorSize"
                        runat="server" Display="Dynamic"></pe:NumberValidator>
                    <asp:RangeValidator ID="ValrAuthorSize" ControlToValidate="TxtAuthorSize" Type="Integer"
                        runat="server" ErrorMessage="不能超过200字符长度！" Display="Dynamic" MaximumValue="200"
                        MinimumValue="1"></asp:RangeValidator>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>默认值：</strong>
                </td>
                <td align="left">
                    <asp:TextBox ID="TxtAuthorDefaultValue" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>是否保存上次记录：</strong>
                </td>
                <td align="left">
                    <asp:RadioButtonList ID="RadAuthorIsPSession" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="True">是</asp:ListItem>
                        <asp:ListItem Value="False" Selected="True">否</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
        </tbody>
        <tbody id="PnlSource" visible="false" runat="server">
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>文本框长度：</strong>
                </td>
                <td align="left">
                    <asp:TextBox ID="TxtSourceSize" Text="50" runat="server" Columns="5"></asp:TextBox>
                    <pe:RequiredFieldValidator ID="RequiredFieldValidatorSource" ControlToValidate="TxtSourceSize"
                        runat="server" ErrorMessage="文本框长度不能为空" Display="Dynamic"></pe:RequiredFieldValidator>
                    <pe:NumberValidator ID="NumberValidatorSource" ControlToValidate="TxtSourceSize"
                        runat="server" Display="Dynamic"></pe:NumberValidator>
                    <asp:RangeValidator ID="ValrSourceSize" ControlToValidate="TxtSourceSize" Type="Integer"
                        runat="server" ErrorMessage="不能超过200字符长度！" Display="Dynamic" MaximumValue="200"
                        MinimumValue="1"></asp:RangeValidator>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>默认值：</strong>
                </td>
                <td align="left">
                    <asp:TextBox ID="TxtSourceDefaultValue" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>是否保存上次记录：</strong>
                </td>
                <td align="left">
                    <asp:RadioButtonList ID="RadSourceIsPSession" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="True">是</asp:ListItem>
                        <asp:ListItem Value="False" Selected="True">否</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
        </tbody>
        <tbody id="PnlKeyword" visible="false" runat="server">
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>文本框长度：</strong>
                </td>
                <td align="left">
                    <asp:TextBox ID="TxtKeywordSize" Text="50" runat="server" Columns="5"></asp:TextBox>
                    <pe:RequiredFieldValidator ID="RequiredFieldValidatorKeyword" ControlToValidate="TxtKeywordSize"
                        runat="server" ErrorMessage="文本框长度不能为空" Display="Dynamic"></pe:RequiredFieldValidator>
                    <pe:NumberValidator ID="NumberValidatorKeyword" ControlToValidate="TxtKeywordSize"
                        runat="server" Display="Dynamic"></pe:NumberValidator>
                    <asp:RangeValidator ID="ValrKeywordSize" ControlToValidate="TxtKeywordSize" Type="Integer"
                        runat="server" ErrorMessage="不能超过200字符长度！" Display="Dynamic" MaximumValue="200"
                        MinimumValue="1"></asp:RangeValidator>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>默认值：</strong>
                </td>
                <td align="left">
                    <asp:TextBox ID="TxtKeywordDefaultValue" runat="server"></asp:TextBox>
                </td>
            </tr>
        </tbody>
        <tbody id="PnlOperatingSystem" visible="false" runat="server">
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>分行键入&nbsp;&nbsp;<br />
                        每个平台选项：</strong>
                </td>
                <td align="left">
                    <asp:TextBox ID="TxtOperatingSystemSelectItem" runat="server" Height="100px" TextMode="MultiLine"
                        Width="300px" Wrap="false"></asp:TextBox>
                    <pe:RequiredFieldValidator ID="RequiredFieldValidatorOperatingSystem" runat="server"
                        ControlToValidate="TxtOperatingSystemSelectItem" Display="Dynamic" ErrorMessage="分行默认项不能为空"></pe:RequiredFieldValidator>
                    <span style="color: blue">注：一行一个默认项</span>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>文本框长度：</strong>
                </td>
                <td align="left">
                    <asp:TextBox ID="TxtOperatingSystemSize" Text="50" runat="server" Columns="5"></asp:TextBox>
                    <pe:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="TxtOperatingSystemSize"
                        runat="server" ErrorMessage="文本框长度不能为空" Display="Dynamic"></pe:RequiredFieldValidator>
                    <pe:NumberValidator ID="NumberValidatorOperatingSystem" ControlToValidate="TxtOperatingSystemSize"
                        runat="server" Display="Dynamic"></pe:NumberValidator>
                    <asp:RangeValidator ID="ValrOperatingSystemSize" ControlToValidate="TxtOperatingSystemSize"
                        Type="Integer" runat="server" ErrorMessage="不能超过200字符长度！" Display="Dynamic" MaximumValue="200"
                        MinimumValue="1"></asp:RangeValidator>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>默认值：</strong>
                </td>
                <td align="left">
                    <asp:TextBox ID="TxtOperatingSystemDefaultValue" runat="server"></asp:TextBox>
                    <span style="color: green">例：Win2000/XP/Win2003</span>
                </td>
            </tr>
        </tbody>
        <tbody id="PnlProducer" visible="false" runat="server">
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>文本框长度：</strong>
                </td>
                <td align="left">
                    <asp:TextBox ID="TxtProducerSize" Text="50" runat="server" Columns="5"></asp:TextBox>
                    <pe:RequiredFieldValidator ID="RequiredFieldValidatorProducer" ControlToValidate="TxtProducerSize"
                        runat="server" ErrorMessage="文本框长度不能为空" Display="Dynamic"></pe:RequiredFieldValidator>
                    <pe:NumberValidator ID="NumberValidatorProducer" ControlToValidate="TxtProducerSize"
                        runat="server" Display="Dynamic"></pe:NumberValidator>
                    <asp:RangeValidator ID="RangeValidatorProducer" ControlToValidate="TxtProducerSize"
                        Type="Integer" runat="server" ErrorMessage="不能超过200字符长度！" Display="Dynamic" MaximumValue="200"
                        MinimumValue="1"></asp:RangeValidator>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>默认值：</strong>
                </td>
                <td align="left">
                    <asp:TextBox ID="TxtProducerDefaultValue" runat="server"></asp:TextBox>
                </td>
            </tr>
        </tbody>
        <tbody id="PnlTrademark" visible="false" runat="server">
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>文本框长度：</strong>
                </td>
                <td align="left">
                    <asp:TextBox ID="TxtTrademarkSize" Text="50" runat="server" Columns="5"></asp:TextBox>
                    <pe:RequiredFieldValidator ID="RequiredFieldValidatorTrademark" ControlToValidate="TxtTrademarkSize"
                        runat="server" ErrorMessage="文本框长度不能为空" Display="Dynamic"></pe:RequiredFieldValidator>
                    <pe:NumberValidator ID="NumberValidatorTrademark" ControlToValidate="TxtTrademarkSize"
                        runat="server" Display="Dynamic"></pe:NumberValidator>
                    <asp:RangeValidator ID="RangeValidatorTrademark" ControlToValidate="TxtTrademarkSize"
                        Type="Integer" runat="server" ErrorMessage="不能超过200字符长度！" Display="Dynamic" MaximumValue="200"
                        MinimumValue="1"></asp:RangeValidator>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>默认值：</strong>
                </td>
                <td align="left">
                    <asp:TextBox ID="TxtTrademarkDefaultValue" runat="server"></asp:TextBox>
                </td>
            </tr>
        </tbody>
        <tbody id="PnlContent" visible="false" runat="server">
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>编辑器大小：</strong>
                </td>
                <td align="left">
                    宽
                    <asp:TextBox ID="TxtContentEditorWidth" Text="600" runat="server" Columns="5"></asp:TextBox>
                    <span style="color: Blue">px&nbsp;例如：600px</span>
                    <pe:RequiredFieldValidator ID="ValrContentEditorWidth" runat="server" ControlToValidate="TxtContentEditorWidth"
                        Display="Dynamic" ErrorMessage="显示宽度不能为空"></pe:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TxtContentEditorWidth"
                        ErrorMessage="只能输入字母数字百分号" ValidationExpression="^[a-zA-Z%0-9]*$" Display="Dynamic"></asp:RegularExpressionValidator>
                    <br />
                    高
                    <asp:TextBox ID="TxtContentEditorHight" Text="500" runat="server" Columns="5"></asp:TextBox>
                    <span style="color: Blue">px&nbsp;例如：350px</span>
                    <pe:RequiredFieldValidator ID="ValrContentEditorHight" runat="server" ControlToValidate="TxtContentEditorHight"
                        Display="Dynamic" ErrorMessage="显示高度不能为空"></pe:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="TxtContentEditorHight"
                        ErrorMessage="只能输入字母数字百分号" ValidationExpression="^[a-zA-Z%0-9]*$" Display="Dynamic"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>允许的文件大小：</strong>
                </td>
                <td align="left">
                    <asp:TextBox ID="TxtUploadSize" Text="" runat="server" Columns="6" MaxLength="6"></asp:TextBox>
                    KB <span style="color: Blue">提示：1 KB = 1024 Byte，1 MB = 1024 KB</span>
                    <pe:RequiredFieldValidator ID="ValrTxtUploadSize" runat="server" ControlToValidate="TxtUploadSize"
                        Display="Dynamic" ErrorMessage="文件大小不能为空"></pe:RequiredFieldValidator>
                    <pe:NumberValidator ID="NumberValidator4" ControlToValidate="TxtUploadSize" runat="server"
                        Display="Dynamic">
                    </pe:NumberValidator>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>允许上传的图片类型：</strong>
                </td>
                <td align="left">
                    <asp:TextBox ID="TextUploadImpType" runat="server" Text="jpg|gif|jpeg|png|bmp"></asp:TextBox>
                    <pe:RequiredFieldValidator ID="ValrUploadImpType" runat="server" ControlToValidate="TextUploadImpType"
                        Display="Dynamic" ErrorMessage="图片类型不能为空"></pe:RequiredFieldValidator>
                    <span style="color: Blue">注：允许多个类型请用“|”号分割，如：jpg|gif|jpeg|png|bmp等等</span>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>允许上传的媒体类型：</strong>
                </td>
                <td align="left">
                    <asp:TextBox ID="TextUploadFlashType" runat="server" Text="swf|fla|rm|rmvb|mp3|mpeg|avi|mpeg2|wmv|midi"></asp:TextBox>
                    <pe:RequiredFieldValidator ID="ValrUploadFlashType" runat="server" ControlToValidate="TextUploadFlashType"
                        Display="Dynamic" ErrorMessage="Flash类型不能为空"></pe:RequiredFieldValidator>
                    <span style="color: Blue">注：允许多个类型请用“|”号分割，如：swf|fla|rm|rmvb|mp3|mpeg|avi|mpeg2|wmv|midi等等</span>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>允许上传的附件类型：</strong>
                </td>
                <td align="left">
                    <asp:TextBox ID="TextUploadAnnexType" runat="server" Text="txt|doc|rar|zip"></asp:TextBox>
                    <pe:RequiredFieldValidator ID="ValrUploadAnnexType" runat="server" ControlToValidate="TextUploadAnnexType"
                        Display="Dynamic" ErrorMessage="附件类型不能为空"></pe:RequiredFieldValidator>
                    <span style="color: Blue">注：允许多个类型请用“|”号分割，如：txt|doc|rar|zip等等</span>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>图片是否加水印：</strong>
                </td>
                <td align="left">
                    <asp:RadioButtonList ID="RadlContentWsImage" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="True">是</asp:ListItem>
                        <asp:ListItem Value="False" Selected="True">否</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>是否上传缩略图：</strong>
                </td>
                <td align="left">
                    <asp:RadioButtonList ID="RadlContentThumb" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="True">是</asp:ListItem>
                        <asp:ListItem Value="False" Selected="True">否</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
        </tbody>
        <tbody id="PnlVirtualLink" visible="false" runat="server">
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>显示的宽度：</strong>
                </td>
                <td align="left">
                    <asp:TextBox ID="TxtVirtualLinkWidth" Text="500" runat="server" Columns="5"></asp:TextBox>
                    <span style="color: Blue">px &nbsp;例如：500px</span>
                    <pe:RequiredFieldValidator ID="RequiredFieldValidatorVirtualLinkWidth" runat="server"
                        ControlToValidate="TxtVirtualLinkWidth" Display="Dynamic" ErrorMessage="显示宽度不能为空"></pe:RequiredFieldValidator>
                    <pe:NumberValidator ID="NumberValidatorVirtualLinkWidth" ControlToValidate="TxtVirtualLinkWidth"
                        runat="server">
                    </pe:NumberValidator>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>显示的高度：</strong>
                </td>
                <td align="left">
                    <asp:TextBox ID="TxtVirtualLinkRow" Text="80" runat="server" Columns="5"></asp:TextBox>
                    <span style="color: Blue">px &nbsp;例如：80px</span>
                    <pe:RequiredFieldValidator ID="RequiredFieldValidatorVirtualLinkRow" runat="server"
                        ControlToValidate="TxtVirtualLinkRow" Display="Dynamic" ErrorMessage="显示高度不能为空"></pe:RequiredFieldValidator>
                    <pe:NumberValidator ID="NumberValidatorVirtualLinkRow" ControlToValidate="TxtVirtualLinkRow"
                        runat="server">
                    </pe:NumberValidator>
                </td>
            </tr>
        </tbody>
        <tbody id="PnlDownServer" visible="false" runat="server">
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>文本框长度：</strong>
                </td>
                <td align="left">
                    <asp:TextBox ID="TxtDownServerWidth" Text="50" runat="server" Columns="5"></asp:TextBox>
                    <pe:RequiredFieldValidator ID="RequiredFieldValidatorDownServerWidth" ControlToValidate="TxtDownServerWidth"
                        runat="server" ErrorMessage="文本框长度不能为空" Display="Dynamic"></pe:RequiredFieldValidator>
                    <pe:NumberValidator ID="NumberValidatorDownServerWidth" ControlToValidate="TxtDownServerWidth"
                        runat="server" Display="Dynamic"></pe:NumberValidator>
                    <asp:RangeValidator ID="RangeValidatorDownServerWidth" ControlToValidate="TxtDownServerWidth"
                        Type="Integer" runat="server" ErrorMessage="不能超过100字符长度！" Display="Dynamic" MaximumValue="1000"
                        MinimumValue="1"></asp:RangeValidator>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>默认值：</strong>
                </td>
                <td align="left">
                    <asp:TextBox ID="TxtDownServerDefaultValue" runat="server" Width="276px"></asp:TextBox>
                    <span style="color: green">例：$$$下载服务器名|下载服务器ID</span>
                </td>
            </tr>
        </tbody>
        <tbody id="PnlSpecial" visible="false" runat="server">
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>显示的宽度：</strong>
                </td>
                <td align="left">
                    <asp:TextBox ID="TxtSpecialWidth" Text="500" runat="server" Columns="5"></asp:TextBox>
                    <span style="color: Blue">px &nbsp;例如：500px</span>
                    <pe:RequiredFieldValidator ID="RequiredFieldValidatorSpecialWidth" runat="server"
                        ControlToValidate="TxtSpecialWidth" Display="Dynamic" ErrorMessage="显示宽度不能为空"></pe:RequiredFieldValidator>
                    <pe:NumberValidator ID="NumberValidatorSpecialWidth" ControlToValidate="TxtSpecialWidth"
                        runat="server">
                    </pe:NumberValidator>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>显示的高度：</strong>
                </td>
                <td align="left">
                    <asp:TextBox ID="TxtSpecialRow" Text="80" runat="server" Columns="5"></asp:TextBox>
                    <span style="color: Blue">px &nbsp;例如：80px</span>
                    <pe:RequiredFieldValidator ID="RequiredFieldValidatorSpecialRow" runat="server" ControlToValidate="TxtSpecialRow"
                        Display="Dynamic" ErrorMessage="显示高度不能为空"></pe:RequiredFieldValidator>
                    <pe:NumberValidator ID="NumberValidatorSpecialRow" ControlToValidate="TxtSpecialRow"
                        runat="server">
                    </pe:NumberValidator>
                </td>
            </tr>
        </tbody>
        <tbody id="PnlTitle" visible="false" runat="server">
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>最大字符数：</strong>
                </td>
                <td align="left">
                    <asp:TextBox ID="TxtTitleMaxLength" Text="200" runat="server" Columns="5"></asp:TextBox>
                    <pe:RequiredFieldValidator ID="RequiredFieldValidatorTitleMaxLength" ControlToValidate="TxtTitleMaxLength"
                        runat="server" ErrorMessage="最大字符数不能为空" Display="Dynamic"></pe:RequiredFieldValidator>
                    <asp:RangeValidator ID="RangeValidatorTitleMaxLength" ControlToValidate="TxtTitleMaxLength"
                        runat="server" ErrorMessage="超出数据范围！" Type="Integer" Display="Dynamic" MaximumValue="255"
                        MinimumValue="1"></asp:RangeValidator>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>文本框长度：</strong>
                </td>
                <td align="left">
                    <asp:TextBox ID="TxtTitleSize" Text="30" runat="server" Columns="5"></asp:TextBox>
                    <pe:RequiredFieldValidator ID="RequiredFieldValidatorTitleSize" ControlToValidate="TxtTitleSize"
                        runat="server" ErrorMessage="文本框长度不能为空" Display="Dynamic"></pe:RequiredFieldValidator>
                    <asp:RangeValidator ID="RangeValidatorTitleSize" ControlToValidate="TxtTitleSize"
                        Type="Integer" runat="server" ErrorMessage="超出数据范围！" Display="Dynamic" MaximumValue="250"
                        MinimumValue="1"></asp:RangeValidator>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>默认值：</strong>
                </td>
                <td align="left">
                    <asp:TextBox ID="TxtTitleDefaultValue" runat="server" Width="276px"></asp:TextBox>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>是否启用检测重复值：</strong>
                </td>
                <td align="left">
                    <asp:RadioButtonList ID="RadCheckTitleValue" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="True">是</asp:ListItem>
                        <asp:ListItem Value="False" Selected="True">否</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>是否自动生成拼音标题：</strong>
                </td>
                <td align="left">
                    <asp:RadioButtonList ID="RadCreatePinyinTitle" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="True">是</asp:ListItem>
                        <asp:ListItem Value="False" Selected="True">否</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
        </tbody>
        <tbody id="PnlMultiplePhoto" visible="false" runat="server">
           <%-- <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>是否启用独立的缩略图字段：</strong>
                </td>
                <td align="left">
                    <asp:CheckBox ID="ChkIsSaveThumb" runat="server" Text="" />
                </td>
            </tr>
            <tr class="tdbg" id="tThumbField" runat="server" style="display: none;">
                <td class="tdbgleft">
                    <strong>保存缩略图地址字段：</strong>
                </td>
                <td align="left">
                    <asp:TextBox ID="TxtThumbField" runat="server"></asp:TextBox><asp:CompareValidator
                        ID="CompareValidator4" runat="server" Display="Dynamic" ErrorMessage="保存缩略图地址字段名不能与主字段名重复！"
                        ControlToValidate="TxtThumbField" ControlToCompare="TxtFieldName" Operator="NotEqual"
                        SetFocusOnError="True"></asp:CompareValidator>
                    <asp:CustomValidator ID="ValxThumbField" ClientValidationFunction="ValxThumbField_ClientValidate"
                        Display="dynamic" ValidateEmptyText="true" SetFocusOnError="true" runat="server"
                        ErrorMessage="缩略图地址字段名不能为空" ControlToValidate="TxtThumbField"></asp:CustomValidator>
                </td>
            </tr>--%>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>图片是否加水印：</strong>
                </td>
                <td align="left">
                    <asp:RadioButtonList ID="RadlMWsImage" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="True">是</asp:ListItem>
                        <asp:ListItem Value="False" Selected="True">否</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>允许的文件大小：</strong>
                </td>
                <td align="left">
                    <asp:TextBox ID="TxtMultiPhotoSize" Text="1024" runat="server" Columns="6" MaxLength="6"></asp:TextBox>
                    KB <span style="color: Blue">提示：1 KB = 1024 Byte，1 MB = 1024 KB</span>
                    <pe:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxtMultiPhotoSize"
                        Display="Dynamic" ErrorMessage="文件大小不能为空"></pe:RequiredFieldValidator>
                    <pe:NumberValidator ID="NumberValidator3" ControlToValidate="TxtMultiPhotoSize" runat="server"
                        Display="Dynamic">
                    </pe:NumberValidator>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>允许的文件类型：</strong>
                </td>
                <td align="left">
                    <asp:TextBox ID="TxtMultiPhotoExt" runat="server"></asp:TextBox>
                    <pe:RequiredFieldValidator ID="ReqTxtMultiPhotoExt" runat="server" ControlToValidate="TxtMultiPhotoExt"
                        Display="Dynamic" ErrorMessage="文件类型不能为空"></pe:RequiredFieldValidator>
                    <span style="color: Blue">注：允许多个类型请用“|”号分割，如：jpg|mp3|gif|rm|rmvb等等</span>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>默认值：</strong>
                </td>
                <td align="left">
                    <asp:TextBox ID="TxtMultiPhotoDefaultValue" runat="server" Width="281px"></asp:TextBox><br />
                    <span style="color: Green">例：图片地址1|Photo/2007/12/2007122209084503640.jpg</span>
                </td>
            </tr>
        </tbody>
        <tbody id="PnlProperty" visible="false" runat="server">
            <tr class="tdbg">
                <td align="right" class="tdbgleft" style="width: 180px">
                    <strong>商品属性值：</strong>&nbsp;
                </td>
                <td align="left" class="tdbg">
                    <asp:TextBox ID="TxtProperty" runat="server" Height="100px" TextMode="MultiLine"
                        Width="300px" Wrap="false"></asp:TextBox>
                    <pe:RequiredFieldValidator ID="ValrProperty" runat="server" ControlToValidate="TxtProperty"
                        Display="Dynamic" ErrorMessage="商品属性值不能为空"></pe:RequiredFieldValidator>
                    <span style="color: #0000ff">注：商品属性值之间以“回车”分隔。</span>
                </td>
            </tr>
        </tbody>
        <tbody runat="server" id="PnlRoleList">
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>禁止设置该字段值的会员组：</strong>
                </td>
                <td align="left">
                    <pe:ExtendedCheckBoxList ID="EChkGroupList" RepeatColumns="5" runat="server">
                    </pe:ExtendedCheckBoxList>
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">
                    <strong>禁止设置该字段值的角色：</strong>
                </td>
                <td align="left">
                    <pe:ExtendedCheckBoxList ID="EChkRoleList" RepeatColumns="5" runat="server">
                    </pe:ExtendedCheckBoxList>
                </td>
            </tr>
        </tbody>
        <tr class="tdbgbottom">
            <td colspan="2">
                <asp:Button ID="EBtnSubmit" Text="保存字段" OnClick="EBtnSubmit_Click" runat="server" />&nbsp;&nbsp;
                <asp:Button ID="BtnCancel" runat="server" Text="返回字段管理" OnClick="BtnCancel_Click"
                    CausesValidation="False" />
                <asp:HiddenField ID="HdnFieldLevel" runat="server" />
                <asp:HiddenField ID="HdnFieldType" runat="server" />
                <asp:HiddenField ID="HdnOrderId" runat="server" />
                <asp:HiddenField ID="HdnDisabled" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
