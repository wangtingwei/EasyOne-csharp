<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" validateRequest="false" 
    Inherits="EasyOne.WebSite.Admin.Template.LabelSqlBuild" Title="SQL语句编辑" Codebehind="LabelSqlBuild.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <style type="text/css">
    <!-- 
    .dragspandiv{
    	background-color: #ff99cc;
    	FILTER: alpha(opacity=50);
        border: 1px dashed #F6B9D6;
        padding: 5px 5px 5px 5px; 
        width: 80px;
        height: 20px;
        float: left;
        text-align: center;
        margin: 10px;
        overflow:hidden;
    }
    .spanfixdiv{
    	background-color: #FFFBF5;
        border: 1px solid #F6B9D6;
        padding: 5px 5px 5px 5px; 
        width: 80px;
        height: 20px;
        float: left;
        text-align: center;
        margin: 5px;
        overflow:hidden;
        cursor: hand;
    }
   .selectlist{
        background-color: #e6efff;
        border: 1px dashed #2F4F4F;
        padding: 5px 5px 5px 5px; 
        width: 90%;
    }       
    .plist{
        background-color: #e6efff;
        border: 1px dashed #2F4F4F;
        padding: 5px 5px 5px 5px; 
       width: 90%;
        vertical-align: middle;
    }
    .fielddiv{
        background-color: #e6efff;
        border: 1px dashed #2F4F4F;
        padding: 5px 5px 5px 5px; 
        overflow: auto;
        float: left;
        width: 90%;
        height: 100%;
        text-align: left
    }
    -->
</style>
    <asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server" />

    <script id="pejs" type="text/javascript">
<!--
var dragspan;
var src;

function dragstart()
{
    window.drag = 1;
    window.event.returnValue = false;
    
    dragspan = document.createElement('div');
    dragspan.style.position = "absolute";
    dragspan.className = "dragspandiv";
    var mousePos = mouseCoords(window.event);
    dragspan.style.left = mousePos.x + 10;
    dragspan.style.top = mousePos.y + 8;
            
    dragspan.appendChild(document.createTextNode(window.event.srcElement.innerText));
    src = window.event.srcElement.innerText;

    document.body.appendChild(dragspan);
}

function dragend(type)
{
    if(window.drag)
    {
        document.body.removeChild(dragspan);
        var target = window.event.srcElement;
        if(type == 1)
        {
            target.focus();
            var tarobj = document.selection.createRange();
            tarobj.text = "@" + src;
        }
        else
        {
            target.value = "@" + src;
        }
        window.drag = 0;
        window.event.returnValue = true;
    }
    
}

function dragmove()
{
    if(window.drag)
    {
        var ev = ev || window.event;
        var mousePos = mouseCoords(ev);

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
-->
    </script>

    <table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="border">
        <tr>
            <td class="spacingtitle" colspan="2" align="center">
                <b>标签查询设置</b></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbg" align="left">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <table style="width: 100%; margin: 0 auto;">
                            <tr>
                                <td class="tdbgleft" style="width: 105px; text-align: right;">
                                    输出数量：</td>
                                <td colspan="3">
                                    <asp:TextBox ID="TxtOutNum" runat="server" Text="10" Width="120" Style="text-align: center" />
                                    <ajaxToolkit:NumericUpDownExtender ID="NumericUpDownExtender1" runat="server" TargetControlID="TxtOutNum"
                                        Width="120" RefValues="" ServiceDownMethod="" ServiceUpMethod="" TargetButtonDownID=""
                                        TargetButtonUpID="" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tdbgleft" style="width: 105px; text-align: right;">
                                    选择数据：</td>
                                <td class="tdbg" colspan="3">
                                    <div id="selectdatediv" class="selectlist">
                                        <table>
                                            <tr>
                                                <td style="width: 100px; text-align: right;">
                                                    主表：</td>
                                                <td>
                                                    <asp:DropDownList ID="DbTableDownList" runat="server" Width="166px" AutoPostBack="True"
                                                        OnSelectedIndexChanged="DBTableDownList_SelectedIndexChanged" /></td>
                                                <td style="width: 100px; text-align: right;">
                                                    从表：</td>
                                                <td>
                                                    <asp:DropDownList ID="DbTableDownList2" runat="server" Width="166px" AutoPostBack="True"
                                                        OnSelectedIndexChanged="DBTableDownList2_SelectedIndexChanged" /></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 100px; text-align: right;">
                                                    输出字段：</td>
                                                <td>
                                                    <asp:ListBox ID="DbFieldDownList" runat="server" Height="220px" Width="166px" SelectionMode="Multiple" /></td>
                                                <td style="width: 100px; text-align: right;">
                                                    输出字段：</td>
                                                <td>
                                                    <asp:ListBox ID="DbFieldDownList2" runat="server" Height="220px" Width="166px" SelectionMode="Multiple" /></td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <span id="Span1" runat="server" visible="false">
                                <tbody>
                                    <tr>
                                        <td class="tdbgleft" style="width: 105px; text-align: right;">
                                            约束字段：</td>
                                        <td colspan="3">
                                            <div id="ycdiv" class="selectlist">
                                                <asp:DropDownList ID="Dbtj" runat="server">
                                                    <asp:ListItem>InnerJoin</asp:ListItem>
                                                    <asp:ListItem>LeftJoin</asp:ListItem>
                                                    <asp:ListItem>OuterJoin</asp:ListItem>
                                                    <asp:ListItem>RightJoin</asp:ListItem>
                                                </asp:DropDownList><asp:DropDownList ID="DbFieldList" runat="server" Width="166px" /><asp:DropDownList
                                                    ID="Dbys" runat="server">
                                                    <asp:ListItem>=</asp:ListItem>
                                                    <asp:ListItem>Like</asp:ListItem>
                                                </asp:DropDownList><asp:DropDownList ID="DbFieldList2" runat="server" Width="166px" />
                                            </div>
                                        </td>
                                    </tr>
                                </tbody>
                            </span>
                            <tr style="height: 180px;">
                                <td class="tdbgleft" style="width: 105px; text-align: right;">
                                    <asp:Button ID="BtnClausebilud" runat="server" Style="cursor: pointer; cursor: hand;
                                        width: 88px;" Text="生成查询条件" OnClick="BtnClausebilud_Click" /></td>
                                <td colspan="3">
                                    <div id="gridviewclause" class="fielddiv">
                                        <asp:GridView ID="GridView_Clause" AutoGenerateColumns="false" runat="server">
                                            <Columns>
                                                <pe:BoundField DataField="ColumnName" HeaderText="字段名" SortExpression="ColumnName"
                                                     />
                                                <pe:BoundField DataField="DataType" HeaderText="字段类型" SortExpression="DataType" />
                                                <pe:TemplateField HeaderText="条件约束">
                                                    <itemtemplate>
                                                        <asp:DropDownList ID="dropdowntj2" runat="server">
                                                            <asp:ListItem Value="">-----请选择------</asp:ListItem>
                                                            <asp:ListItem Value="1">等于</asp:ListItem>
                                                            <asp:ListItem Value="2">大于</asp:ListItem>
                                                            <asp:ListItem Value="3">小于</asp:ListItem>
                                                            <asp:ListItem Value="4">大于等于</asp:ListItem>
                                                            <asp:ListItem Value="5">小于等于</asp:ListItem>
                                                            <asp:ListItem Value="6">不等于</asp:ListItem>
                                                            <asp:ListItem Value="7">在</asp:ListItem>
                                                            <asp:ListItem Value="8">象</asp:ListItem>
                                                            <asp:ListItem Value="9">不在</asp:ListItem>
                                                            <asp:ListItem Value="10">不象</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </itemtemplate>
                                                </pe:TemplateField>
                                                <pe:TemplateField HeaderText="条件对象">
                                                    <itemtemplate>
                                                        <asp:TextBox ID="txtbox1" runat="server"></asp:TextBox>
                                                    </itemtemplate>
                                                </pe:TemplateField>
                                                <pe:TemplateField HeaderText="条件层次">
                                                    <itemtemplate>
                                                        <asp:DropDownList ID="dropdowntj1" runat="server">
                                                            <asp:ListItem>0</asp:ListItem>
                                                            <asp:ListItem>1</asp:ListItem>
                                                            <asp:ListItem>2</asp:ListItem>
                                                            <asp:ListItem>3</asp:ListItem>
                                                            <asp:ListItem>4</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </itemtemplate>
                                                </pe:TemplateField>
                                                <pe:TemplateField HeaderText="启用排序">
                                                    <itemtemplate>
                                                        <asp:DropDownList ID="dropdownorder" runat="server">
                                                            <asp:ListItem Value="">不排序</asp:ListItem>
                                                            <asp:ListItem Value="up">升序</asp:ListItem>
                                                            <asp:ListItem Value="down">降序</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </itemtemplate>
                                                </pe:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdbgleft" style="width: 105px; text-align: right;">
                                    自定义参数：<br />
                                    （拖放添加）</td>
                                <td colspan="3">
                                    <div id="plist" class="plist">
                                        <pe:ExtendedLabel HtmlEncode="false" ID="attlist" runat="server"></pe:ExtendedLabel>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdbgleft" style="width: 105px; text-align: right;">
                                    <asp:Button ID="BtnSqlbilud" runat="server" Style="cursor: pointer; cursor: hand;
                                        width: 88px;" Text="生成SQL语句" OnClick="BtnSqlbilud_Click" /></td>
                                <td colspan="3">
                                    <asp:TextBox ID="TxtSqlstr" runat="server" Width="90%" Height="79px" Rows="5" TextMode="MultiLine" /></td>
                            </tr>
                            <tr>
                                <td class="tdbgleft" style="width: 105px; text-align: right;">
                                    <asp:CheckBox ID="ChkPage" runat="server" OnCheckedChanged="ChkPage_CheckedChanged"
                                        AutoPostBack="True" />启用分页</td>
                                <td colspan="3">
                                    <span id="CountShow" runat="server" visible="false">查询统计语句↓<br />
                                        <asp:TextBox ID="TxtSqlCount" runat="server" Width="90%" Height="50px" Text="select count(id) from database"
                                            Rows="5" TextMode="MultiLine"></asp:TextBox>
                                    </span><span id="PageShow" runat="server" visible="false">
                                        <br />
                                        查询分页语句↓<br />
                                        <asp:TextBox ID="TxtSqlPage" Width="90%" runat="server" Height="150px" Rows="5" TextMode="MultiLine"></asp:TextBox>
                                    </span>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr class="tdbg">
            <td align="center">
                <asp:Button ID="BtnPrv" OnClick="BtnPrv_Click" runat="server" Style="cursor: pointer;
                    cursor: hand; width: 88px;" Text="上一步" />&nbsp;&nbsp;<asp:Button ID="BtnNext" OnClick="BtnNext_Click"
                        runat="server" Style="cursor: pointer; cursor: hand; width: 88px;" Text="下一步" />&nbsp;&nbsp;<asp:Button
                            ID="BtnSave" runat="server" Text="保　存" OnClick="BtnSave_Click" Style="cursor: pointer;
                            cursor: hand; width: 88px;" Visible="False" />&nbsp;&nbsp;<input id="BtnCancel" type="button"
                                class="inputbutton" value="取　消" onclick="Redirect('LabelManage.aspx')" style="cursor: pointer;
                                cursor: hand; width: 88px;" /><br />
            </td>
        </tr>
    </table>
    <br />
    <table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="border">
        <tr>
            <td class="spacingtitle" colspan="2" align="center">
                <b>相关说明</b></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbg" align="left">
                <ajaxToolkit:TabContainer runat="server" ID="Tabs" Height="150px">
                    <ajaxToolkit:TabPanel runat="Server" ID="TabPanel" HeaderText="标签查询">
                        <ContentTemplate>
                            <p>
                                根据需要，构造您自己的SQL语句，如选择分页，请您给出该标签的总数查询语句，与分页查询语句，以便于系统查询取得对应页面的数据。</p>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel runat="Server" ID="TabPanel1" HeaderText="SQL语句">
                        <ContentTemplate>
                            <p>
                                SQL语句就是数据库查询语句，基本查询语句如下：<span style="color: Green">select top 输出条数 字段列表 from 数据库名 where
                                    约束条件 order by 排序条件。</span>其中，输出条数为数字，字段列表为您需要输出的字段名，如有多个则由,号分割。</p>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel runat="Server" ID="TabPanel2" HeaderText="存储过程">
                        <ContentTemplate>
                            <p>
                                存储过程是一个SQL语句的集合，它可以通过一条语句来调用。</p>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                </ajaxToolkit:TabContainer>
            </td>
        </tr>
    </table>
</asp:Content>
