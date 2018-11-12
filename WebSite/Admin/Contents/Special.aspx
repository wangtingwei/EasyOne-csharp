<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Contents.Specials"
    Title="添加专题" Codebehind="Special.aspx.cs" %>

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
        var arrTabTitle = new Array("TabTitle0","<%=TabTitle1.ClientID %>","TabTitle2","TabTitle3");
        var arrTabs = new Array("Tabs0","Tabs1","Tabs2","Tabs3");
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
                if(tempType==0) 
                {
                    document.getElementById(arrTabTitle[i].toString()).style.display = 'none';
                }
                else
                {
                    document.getElementById(arrTabTitle[i].toString()).style.display=displayValue;
                }
            }
        }
        function setFileFileds(num)
        {    
             var id = '<%=objFiles1.ClientID%>';
             id = id.replace("1","");
             for(var i=1,str='';i<=20;i++)
             {
                 document.getElementById(id + i.toString()).style.display = 'none';
             }
             for(var i=1,str='';i<=num;i++)
             {
                 document.getElementById(id + i.toString()).style.display='';
             }
        }
        function ChangeElementValue(elementId,Value)
        {
            if(Value != "-1")
            {
                document.getElementById(elementId).value = Value;
            }
        }

        function GetInitial()
        {
            if($get("<%= TxtSpecialName.ClientID %>").value != "")
            {
                EasyOne.WebSite.Admin.Contents.CategoryService.GetInitial($get("<%= TxtSpecialName.ClientID %>").value, onInitial);
            }
        }
        
        function onInitial(value)
        {
                $get("<%= TxtSpecialIdentifier.ClientID %>").value = value;
                $get("<%= TxtSpecialDir.ClientID %>").value = value;
        }
        
        
        function GetBatchInitial()
        {
            var specialNames = $get("<%= TxtSpecialNames.ClientID %>").value;
            if(specialNames.value != "")
            {
                EasyOne.WebSite.Admin.Contents.CategoryService.GetInitial(specialNames, onBatchInitial);
            }
        }
        
        function onBatchInitial(value)
        {
                $get("<%= TxtSpecialIdentifiers.ClientID %>").value = value;
                $get("<%= TxtSpecialDirs.ClientID %>").value = value;
        }

    </script>

    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr align="center">
            <td id="TabTitle0" class="titlemouseover" onclick="ShowTabs(0)">
                基本信息</td>
            <td id="TabTitle1" class="tabtitle" runat="server" onclick="ShowTabs(1)">
                权限设置</td>
            <td id="TabTitle2" class="tabtitle" onclick="ShowTabs(2)">
                生成选项</td>
            <td id="TabTitle3" class="tabtitle" onclick="ShowTabs(3)">
                自设内容</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr class="tdbg">
            <td valign="top" style="margin-top: 0px; margin-left: 0px;">
                <table width="100%" cellpadding="2" cellspacing="1" class="border">
                    <%-- 基本信息--%>
                    <tbody id="Tabs0">
                        <tr class="tdbg">
                            <td style="width: 300" class="tdbgleft">
                                <strong>所属专题类别：</strong></td>
                            <td>
                                <asp:DropDownList ID="DropSpecialCategory" Width="155px" DataValueField="SpecialCategoryId"
                                    DataTextField="SpecialCategoryName" runat="server" AppendDataBoundItems="true">
                                    <asp:ListItem Text="请选择专题类别" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr class="tdbg" runat="server" id="SpecialName">
                            <td style="width: 300px" class="tdbgleft">
                                <strong>专题名称：</strong></td>
                            <td>
                                <asp:TextBox ID="TxtSpecialName" Width="150px" runat="server" MaxLength="200"></asp:TextBox>
                                <pe:RequiredFieldValidator ID="ValrSpecialName" runat="server" ErrorMessage="专题名称必填！"
                                    ControlToValidate="TxtSpecialName" Display="Dynamic" SetFocusOnError="True"></pe:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr class="tdbg"  runat="server" id="SpecialIdentifier">
                            <td class="tdbgleft" style="width: 300">
                                <strong>专题标识符：</strong><br />
                                用于前台调用时可以直接用标识符取代ID</td>
                            <td>
                                <asp:TextBox ID="TxtSpecialIdentifier" Width="150px" runat="server"></asp:TextBox>
                                <pe:RequiredFieldValidator ID="ValrSpecialIdentifier" runat="server" ErrorMessage="标识符必填！"
                                    ControlToValidate="TxtSpecialIdentifier" Display="Dynamic" SetFocusOnError="True"></pe:RequiredFieldValidator></td>
                        </tr>
                        <tr class="tdbg" runat="server" id="SpecialDir">
                            <td style="width: 300" class="tdbgleft">
                                <strong>专题的目录名：</strong><br />
                            </td>
                            <td>
                                <asp:TextBox ID="TxtSpecialDir" Width="150px" MaxLength="20" runat="server"></asp:TextBox>
                                <pe:RequiredFieldValidator ID="RequiredFieldValidator1" Display="dynamic" ControlToValidate="TxtSpecialDir"
                                    runat="server" ErrorMessage="目录名不能为空"></pe:RequiredFieldValidator>
                                <span style="color: Blue">注意，目录名只能是字母、数字、下划线组成
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TxtSpecialDir"
                                        Display="Dynamic" ValidationExpression="[_a-zA-Z0-9]*" ErrorMessage="注意，目录名只能是字母、数字、下划线组成"
                                        SetFocusOnError="True"></asp:RegularExpressionValidator></span><br />
                            </td>
                        </tr>
                        
                        <tr class="tdbg" runat="server" id="BatchSpecial" visible="false">
                            <td style="width: 300" class="tdbgleft">
                                <strong>批量添加专题：</strong><br />
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td align="center">
                                            专题名称
                                        </td>
                                        <td align="center">
                                            专题标识符
                                        </td>
                                        <td align="center">
                                            专题的目录名
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="TxtSpecialNames" MaxLength="255" runat="server" Columns="50" Height="180px"
                                                TextMode="MultiLine" Width="135px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtSpecialIdentifiers" MaxLength="255" runat="server" Columns="50"
                                                Height="180px" TextMode="MultiLine" Width="135px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtSpecialDirs" MaxLength="255" runat="server" Columns="50" Height="180px"
                                                TextMode="MultiLine" Width="135px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <pe:RequiredFieldValidator ID="ValrSpecialNameTab1" runat="server" ErrorMessage="专题名称不能为空！"
                                    ControlToValidate="TxtSpecialNames" Display="Dynamic" SetFocusOnError="True" RequiredText=""></pe:RequiredFieldValidator>
                                <pe:RequiredFieldValidator ID="ValrSpecialIdentifierTab1" runat="server" ErrorMessage="标识符不能为空！"
                                    ControlToValidate="TxtSpecialIdentifiers" Display="Dynamic" SetFocusOnError="True"
                                    RequiredText=""></pe:RequiredFieldValidator>
                                <pe:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="TxtSpecialDirs"
                                    Display="Dynamic" ErrorMessage="专题的目录名不能为空！" SetFocusOnError="True" runat="server"
                                    RequiredText=""></pe:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegTxtSpecialDir1" runat="server" ControlToValidate="TxtSpecialDirs"
                                    Display="Dynamic" ValidationExpression="[_a-zA-Z\r\n][_a-zA-Z0-9\r\n]*" ErrorMessage="目录名格式错误"
                                    SetFocusOnError="True"></asp:RegularExpressionValidator>
                                <span style="color: Blue">注：一行填写一个专题信息，并且目录名只能是字母、数字、下划线组成，首字符不能是数字。</span>
                            </td>
                        </tr>
                        
                        <tr class="tdbg">
                            <td style="width: 300" class="tdbgleft">
                                <strong>专题图片地址：</strong><br />
                                用于在专题页显示指定的图片</td>
                            <td>
                                <asp:TextBox ID="TxtSpecialPic" MaxLength="255" Width="400px" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr class="tdbg">
                            <td style="width: 300" class="tdbgleft">
                                <strong>专题提示：</strong><br />
                                鼠标移至专题名称上时将显示设定的提示文字（不支持HTML）</td>
                            <td>
                                <asp:TextBox ID="TxtSpecialTips" runat="server" Columns="60" Height="30" Width="400px"
                                    Rows="2" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td style="width: 300" class="tdbgleft">
                                <strong>专题说明：</strong><br />
                                用于在专题页详细介绍专题信息，支持HTML</td>
                            <td>
                                <asp:TextBox ID="TxtDescription" runat="server" Columns="60" Height="30" Width="400px"
                                    Rows="2" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td style="width: 300" class="tdbgleft">
                                <strong>是否推荐：</strong></td>
                            <td>
                                <asp:RadioButtonList ID="RadlIsElite" runat="server" Height="3px" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="True" Selected="True">是</asp:ListItem>
                                    <asp:ListItem Value="False">否</asp:ListItem>
                                </asp:RadioButtonList></td>
                        </tr>
                        <tr class="tdbg">
                            <td style="width: 300" class="tdbgleft">
                                <strong>打开方式：</strong></td>
                            <td>
                                <asp:RadioButtonList ID="RadOpenType" runat="server" Height="3px" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="0" Selected="True">在原窗口打开</asp:ListItem>
                                    <asp:ListItem Value="1">在新窗口打开</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td style="width: 300" class="tdbgleft">
                                <strong>专题页模板：</strong>
                            </td>
                            <td>
                                <pe:TemplateSelectControl ID="FileCSpecialTemplatePath" Width="340px" runat="server"></pe:TemplateSelectControl>
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td style="width: 300" class="tdbgleft">
                                <strong>搜索页模板：</strong>
                            </td>
                            <td>
                                <pe:TemplateSelectControl ID="FileCSearchTemplatePath" Width="340px" runat="server"></pe:TemplateSelectControl>
                            </td>
                        </tr>
                    </tbody>
                    <tbody id="Tabs1" style="display: none">
                        <tr class="tdbg">
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
                                            <pe:ExtendedGridView ID="EgvPermissions" runat="server" AutoGenerateColumns="False"
                                                DataKeyNames="GroupId" OnRowDataBound="EgvPermissions_RowDataBound">
                                                <Columns>
                                                    <pe:BoundField HeaderText="会员组名" DataField="GroupName" />
                                                    <pe:TemplateField HeaderText="添加内容到专题" SortExpression="TreeLineType">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="ChkSpecialInput" runat="server"></asp:CheckBox>
                                                        </ItemTemplate>
                                                    </pe:TemplateField>
                                                </Columns>
                                            </pe:ExtendedGridView>
                                        </td>
                                        <td style="width: 60%" id="TdRolePermissions" runat="server">
                                            <pe:ExtendedGridView ID="EgvRoleView" runat="server" AutoGenerateColumns="False"
                                                DataKeyNames="RoleId" OnRowDataBound="EgvRoleView_RowDataBound">
                                                <Columns>
                                                    <pe:BoundField HeaderText="角色名" DataField="RoleName"/>
                                                    <pe:TemplateField HeaderText="添加内容到专题" SortExpression="TreeLineType">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="ChkSpecialInput" runat="server"></asp:CheckBox>
                                                        </ItemTemplate>
                                                    </pe:TemplateField>
                                                    <pe:TemplateField HeaderText="专题内容管理" SortExpression="TreeLineType">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="ChkSpecialManage" runat="server"></asp:CheckBox>
                                                        </ItemTemplate>
                                                    </pe:TemplateField>
                                                </Columns>
                                            </pe:ExtendedGridView>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                    <tbody id="Tabs2" style="display: none">
                        <tr class="tdbg">
                            <td style="width: 200" class="tdbgleft">
                                <strong>列表页是否生成HTML：</strong>
                            </td>
                            <td>
                                <asp:RadioButtonList ID="RadlCreatHtml" runat="server" Height="3px" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="True">是</asp:ListItem>
                                    <asp:ListItem Value="False" Selected="True">否</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td class="tdbgleft" style="width: 200px">
                                <strong>列表页文件的存放位置：</strong><br />
                            </td>
                            <td>
                                <asp:RadioButtonList ID="RadlListPageHtmlDirType" runat="server">
                                    <asp:ListItem Selected="true" Value="NodePath">列表文件分目录保存在所属专题类别下的所属专题的文件夹中<br/><span style="color: blue">例：NationalNews/NBA/index.html（专题首页）<br /> NationalNews/NBA/List_1.html<span>（专题列表第二页）</asp:ListItem>
                                    <asp:ListItem Value="ListPath">列表文件统一保存在所属专题类别下的指定的“List”文件夹中<br/><span style="color: blue">例：NationalNews/List/List_236.html（专题首页）<br /> NationalNews/List/List_236_1.html</span>（专题列表第二页）</asp:ListItem>
                                    <asp:ListItem Value="RootPath">列表文件统一保存在Special目录的文件夹中<br/><span style="color: blue">例：Special/List_236.html（专题首页）<br /> Special/List_236_1.html<span>（专题列表第二页）</asp:ListItem>
                                </asp:RadioButtonList></td>
                        </tr>
                        <tr class="tdbg">
                            <td class="tdbgleft">
                                <strong>列表页文件的扩展名：</strong><br />
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
                    </tbody>
                    <%--自设内容--%>
                    <tbody id="Tabs3" style="display: none">
                        <asp:Panel ID="PnlCustomFileds" runat="server">
                            <tr class="tdbg">
                                <td style="width: 150" class="tdbgleft">
                                    <strong>自设内容项目数：</strong></td>
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
                                    在列表模板页面插入<span style="color: Blue">{PE.Label id="专题自设内容" specialid="@Request_id" num="1"
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
                                    在列表模板页面插入<span style="color: Blue">{PE.Label id="专题自设内容" specialid="@Request_id" num="2"
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
                                    在列表模板页面插入<span style="color: Blue">{PE.Label id="专题自设内容" specialid="@Request_id" num="3"
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
                                    在列表模板页面插入<span style="color: Blue">{PE.Label id="专题自设内容" specialid="@Request_id" num="4"
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
                                    在列表模板页面插入<span style="color: Blue">{PE.Label id="专题自设内容" specialid="@Request_id" num="5"
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
                                    在列表模板页面插入<span style="color: Blue">{PE.Label id="专题自设内容" specialid="@Request_id" num="6"
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
                                    在列表模板页面插入<span style="color: Blue">{PE.Label id="专题自设内容" specialid="@Request_id" num="7"
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
                                    在列表模板页面插入<span style="color: Blue">{PE.Label id="专题自设内容" specialid="@Request_id" num="8"
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
                                    在列表模板页面插入<span style="color: Blue">{PE.Label id="专题自设内容" specialid="@Request_id" num="9"
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
                                    在列表模板页面插入<span style="color: Blue">{PE.Label id="专题自设内容" specialid="@Request_id" num="10"
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
                                    在列表模板页面插入<span style="color: Blue">{PE.Label id="专题自设内容" specialid="@Request_id" num="11"
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
                                    在列表模板页面插入<span style="color: Blue">{PE.Label id="专题自设内容" specialid="@Request_id" num="12"
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
                                    在列表模板页面插入<span style="color: Blue">{PE.Label id="专题自设内容" specialid="@Request_id" num="13"
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
                                    在列表模板页面插入<span style="color: Blue">{PE.Label id="专题自设内容" specialid="@Request_id" num="14"
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
                                    在列表模板页面插入<span style="color: Blue">{PE.Label id="专题自设内容" specialid="@Request_id" num="15"
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
                                    &nbsp;&nbsp;在列表模板页面插入<span style="color: Blue">{PE.Label id="专题自设内容" specialid="@Request_id"
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
                                    在列表模板页面插入<span style="color: Blue">{PE.Label id="专题自设内容" specialid="@Request_id" num="17"
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
                                    在列表模板页面插入<span style="color: Blue">{PE.Label id="专题自设内容" specialid="@Request_id" num="18"
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
                                    在列表模板页面插入<span style="color: Blue">{PE.Label id="专题自设内容" specialid="@Request_id" num="19"
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
                                    在列表模板页面插入<span style="color: Blue">{PE.Label id="专题自设内容" specialid="@Request_id" num="20"
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
                <asp:Button ID="EBtnAdd" Text="添加" Visible="false" OnClick="EBtnAdd_Click" runat="server" />&nbsp;&nbsp;
                <asp:Button ID="EBtnModify" Text="修改" Visible="false" OnClick="EBtnModify_Click"
                    runat="server" />&nbsp;&nbsp;
                <input name="Cancel" type="button" class="inputbutton" id="BtnCancel" value="取消"
                    onclick="Redirect('SpecialManage.aspx')" /></td>
        </tr>
    </table>
    <asp:HiddenField ID="HdnSpecialName" runat="server" />
    <asp:HiddenField ID="HdnSpecialDir" runat="server" />
    <asp:HiddenField ID="HdnOrderId" runat="server" />
</asp:Content>
