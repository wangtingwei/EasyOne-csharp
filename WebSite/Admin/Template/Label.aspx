<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Template.LabelUI" Title="标签编辑" Codebehind="Label.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" />

    <script id="insertlabel" type="text/javascript">
<!--
function addclass(sourceid, tarid){
    var select=$get(sourceid);
    var tar=$get(tarid);
    for(i=0;i<select.length;i++){
        if(select[i].selected){
            tar.value=select[i].value;
        }
    }
}

function settext(){
    $get("<% =TxtTestStat.ClientID %>").innerHTML = "测试中...";
}
-->
    </script>

    <table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="border">
        <tr>
            <td class="spacingtitle" colspan="2" align="center">
                <span id="LblTitle" style="font-weight: bold;">标签设置</span></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right" style="width: 105px">
                <strong>标签名称：&nbsp;</strong></td>
            <td class="tdbg" align="left">
                <asp:TextBox ID="TxtLabelName" runat="server" Width="288px" />
                <pe:RequiredFieldValidator runat="server" ID="NReq" ControlToValidate="TxtLabelName"
                    Display="Dynamic" ErrorMessage="请输入标签名称" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right" style="width: 105px">
                <strong>标签分类：&nbsp;</strong></td>
            <td class="tdbg" align="left">
                <asp:TextBox ID="TxtLabelType" runat="server" Width="216px"></asp:TextBox>
                <asp:DropDownList ID="DropLabelType" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right" style="width: 105px">
                <strong>数据设置：&nbsp;</strong></td>
            <td class="tdbg" align="left">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="RbtDataType" runat="server" OnSelectedIndexChanged="RbtDataType_SelectedIndexChanged"
                            AutoPostBack="True">
                            <asp:ListItem Value="static">无</asp:ListItem>
                            <asp:ListItem Value="sql_sysquery">系统数据库SQL查询</asp:ListItem>
                            <asp:ListItem Value="sql_sysstoredquery">系统数据库存储过程查询</asp:ListItem>
                            <asp:ListItem Value="sql_outquery">外部SQL查询</asp:ListItem>
                            <asp:ListItem Value="mdb_read">Access数据源</asp:ListItem>
                            <asp:ListItem Value="xsl_read">Excel数据源</asp:ListItem>
                            <asp:ListItem Value="ole_read">OLE数据源</asp:ListItem>
                            <asp:ListItem Value="odbc_read">ODBC数据源</asp:ListItem>
                            <asp:ListItem Value="orc_read">Oracle数据源</asp:ListItem>
                            <asp:ListItem Value="xml_read">XML数据源</asp:ListItem>
                        </asp:DropDownList>
                        <table>
                            <tr>
                                <td>
                                    标签模板处理方式：</td>
                                <td>
                                    <asp:RadioButtonList ID="RBLOutType" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="txt" Selected="True">TXT数据</asp:ListItem>
                                        <asp:ListItem Value="sin">简单XSLT解析</asp:ListItem>
                                        <asp:ListItem Value="">可编程XSLT解析</asp:ListItem>
                                        <asp:ListItem Value="xml">强制输出XML结构(静态标签无效)</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="PanelOutSide" runat="server">
                            <table>
                                <tr>
                                    <td>
                                        数据源连接字符串：</td>
                                    <td>
                                        <asp:TextBox ID="TxtDataSource" runat="server" Width="315px"></asp:TextBox>&nbsp;
                                        <asp:Button ID="BtnTestDataSource" OnClientClick="settext()" OnClick="BtnTestDataSource_Click"
                                            runat="server" Text="测试"></asp:Button><br /><asp:Label ID="TxtTestStat" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right" style="width: 105px">
                <strong>标签说明：&nbsp;</strong></td>
            <td class="tdbg" align="left">
                <asp:TextBox ID="TxtLabelIntro" runat="server" Width="288px" Height="112px" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg">
            <td colspan="2" align="center">
                <asp:Button ID="BtnNext" runat="server" Text="下一步" OnClick="BtnNext_Click" Style="cursor: pointer;
                    cursor: hand; width: 88px;" />&nbsp;&nbsp;<asp:Button ID="BtnSave" runat="server"
                        Text="保　存" OnClick="BtnSave_Click" Style="cursor: pointer; cursor: hand; width: 88px;"
                        Visible="False" />&nbsp;&nbsp;
                <input id="BtnCancel" type="button" class="inputbutton" value="取　消" onclick="Redirect('LabelManage.aspx')"
                    style="cursor: pointer; cursor: hand; width: 88px;" />&nbsp;&nbsp;
                <asp:DropDownList ID="LinkJump" runat="server" Visible="false" OnSelectedIndexChanged="JumpUrl">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <br />
    <table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="border">
        <tr>
            <td class="spacingtitle" colspan="2" align="center">
                <span id="Span1" style="font-weight: bold;">相关说明</span></td>
        </tr>
        <tr class="tdbg">
            <td>
                <ajaxToolkit:TabContainer runat="server" ID="Tabs" Height="150px">
                    <ajaxToolkit:TabPanel runat="Server" ID="TabPanel" HeaderText="使用说明">
                        <ContentTemplate>
                            <p>
                                不选择数据源时，标签为静态模式工作，速度最快，功能最少，一般仅用来进行模板的分割管理。
                            </p>
                            <p>
                                标签可选择解析工作方式，依次为：
                                <li>简单XSLT解析：标签模板只支持使用标准的XSLT语法，但资源消耗一般，速度比较快，一般标签都应使用此种模式。</li>
                                <li>可编程XSLT解析：标签模板支持使用c#,vb,js等程序代码，属于最高级的标签，但资源消耗最高。</li>
                                <li>TXT数据：只输出一个字段的结果，通常用于单一查询的系统标签，无XSLT解析功能，为最高速的标签，<font color="red">静态标签模式强烈建议使用此选项。</font></li>
                                <li>XML数据：本模式不进行XSLT解释，直接输出XML格式的查询结果，速度非常快，一般只用于特殊场所下的数据交流使用。<font color="red">本模式在静态标签下无效。</font></li>
                            </p>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel runat="Server" ID="TabPanel1" HeaderText="名词解释">
                        <ContentTemplate>
                            <p>
                                标签名称:<br />
                                请输入本标签的名称，该名称一旦确定后，在网页中调用格式即确定为{$PE id="标签名" 属性=""/}。</p>
                            <p>
                                标签分类:<br />
                                您可以为标签选择一个分类，如果没有您需要的分类，可以直接在分类输入框中输入您需要的分类名称，该分类将会自动创建，如果保持分类为空，则该标签不属于任何分类。</p>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel runat="Server" ID="TabPanel3" HeaderText="数据源说明">
                        <ContentTemplate>
                            <p>
                                数据源，就是给本标签提供数据的来源，可以是系统数据库，外挂AC数据库，甚至电子表格文件，XML文件，其他网站输出XML数据的地址等。</p>
                            <p>
                                外部数据源连接类型说明：
                                <li>OLE数据源,一般用于连接Access，Excel等数据库</li>
                                <li>ODBC数据源，用途比较广泛，可以连接任意的可支持ODBC方式的数据库</li>
                                <li>Oracle数据源，Oracle专用连接方式，效率要比通过ODBC连接高。</li>
                                <li>Xml数据源，既可以是本地的XML文件，也可以是远程的XML格式数据源。</li>
                            </p>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                </ajaxToolkit:TabContainer>
            </td>
        </tr>
    </table>
</asp:Content>
