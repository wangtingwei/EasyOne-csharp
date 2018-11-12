<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Contents.Single" ValidateRequest="false" Title="单页添加" Codebehind="Single.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">

    <script language="JavaScript" type="text/javascript">
    <!-- 
     function ChangeElementValue(elementId,Value)
        {
            if(Value != "-1")
            {
                document.getElementById(elementId).value = Value;
            }
        }
    //-->
    </script>

    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border" style="margin: 0 auto;">
        <tr align="center">
            <td class="spacingtitle" colspan="2">
                <asp:Label ID="LblTitle" runat="server" Text="添加单页节点" />
            </td>
        </tr>
        <tr id="TrNodeId" class="tdbg" runat="server" visible="false">
            <td class="tdbgleft">
                <strong>节点ID：</strong>
            </td>
            <td>
                <span style="color: Red">
                    <asp:Literal runat="server" ID="LitNodeId"></asp:Literal></span>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>所属节点：</strong>
            </td>
            <td>
                <asp:DropDownList ID="DropParentNode" DataValueField="NodeId" DataTextField="NodeName"
                    runat="server" AppendDataBoundItems="true">
                    <asp:ListItem Text="根节点" Value="0" />
                </asp:DropDownList>
                <asp:Label ID="LblNodeName" runat="server" Text="" />
                <asp:Label ID="LblNodePermissions" runat="server" Text="" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>单页名称：</strong>
            </td>
            <td>
                <asp:TextBox ID="TxtNodeName" runat="server" />
                <pe:RequiredFieldValidator ID="ValrNodeName" runat="server" ErrorMessage="单页名称不能为空！"
                    ControlToValidate="TxtNodeName" Display="Dynamic" SetFocusOnError="True" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>单页标识符：</strong><br />
                用于前台调用时可以直接用标识符取代ID
            </td>
            <td>
                <asp:TextBox ID="TxtNodeIdentifier" runat="server" />
                <pe:RequiredFieldValidator ID="ValrNodeIdentifier" runat="server" ErrorMessage="标识符不能为空！"
                    ControlToValidate="TxtNodeIdentifier" Display="Dynamic" SetFocusOnError="True" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>是否生成HTML：</strong><br />
            </td>
            <td>
                <asp:RadioButtonList ID="RadlIsCreate" RepeatLayout="Flow" RepeatDirection="Horizontal"
                    runat="server">
                    <asp:ListItem Text="是" Selected="true" Value="True" />
                    <asp:ListItem Text="否" Value="False" />
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>打开方式：</strong>
            </td>
            <td>
                <asp:RadioButton ID="RadOpenType0" Checked="true" GroupName="OpenType" runat="server" />在原窗口打开
                <asp:RadioButton ID="RadOpenType1" GroupName="OpenType" runat="server" />在新窗口打开
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>是否在顶部菜单处显示：</strong><br />
                此选项只对一级栏目有效。
            </td>
            <td>
                <asp:RadioButtonList ID="RadlShowOnMenu" RepeatLayout="Flow" RepeatDirection="Horizontal"
                    runat="server">
                    <asp:ListItem Text="是" Selected="True" Value="True" />
                    <asp:ListItem Text="否" Value="False" />
                </asp:RadioButtonList>
            </td>
        </tr>
        <%--    <tr class="tdbg">
            <td class="tdbgleft">
                <strong>是否位置导航处显示：</strong></td>
            <td>
                <asp:RadioButtonList ID="RadlShowOnPath" RepeatLayout="Flow" RepeatDirection="Horizontal"
                    runat="server">
                    <asp:ListItem Text="是" Selected="True" Value="True" />
                    <asp:ListItem Text="否" Value="False" />
                </asp:RadioButtonList></td>
        </tr>--%>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong><span>栏目列表文件名及后缀：</span></strong><br />
            </td>
            <td align="left">
                <table cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td>
                            <asp:Label ID="LblPageHtmlDir" runat="server" Text="" />
                        </td>
                        <td>
                            <asp:TextBox ID="TxtPageHtmlDir" runat="server" />
                        </td>
                        <td>
                            .
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
                        <td>
                            <pe:RequiredFieldValidator ID="ValrPageHtmlDir" ControlToValidate="TxtPageHtmlDir"
                                runat="server" ErrorMessage="单页名称不能为空" Display="Dynamic" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>指定单页模板：</strong>
            </td>
            <td align="left">
                <pe:TemplateSelectControl ID="FileCdefaultListTmeplate" Width="300px" runat="server" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>单页图片地址：</strong><br />
                用于在单页页显示指定的图片
            </td>
            <td>
                <asp:TextBox ID="TxtNodePicUrl" MaxLength="255" runat="server" Width="360px" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>单页提示：</strong><br />
                鼠标移至单页名称上时将显示设定的提示文字（不支持HTML）
            </td>
            <td>
                <asp:TextBox ID="TxtTips" runat="server" Columns="60" Height="65px" Width="360px"
                    Rows="2" TextMode="MultiLine" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>单页说明：</strong><br />
                用于在单页页详细介绍单页信息，支持HTML
            </td>
            <td>
                <asp:TextBox ID="TxtDescription" runat="server" Columns="60" Height="65px" Width="360px"
                    Rows="2" TextMode="MultiLine" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>单页META关键词：</strong><br />
                针对搜索引擎设置的关键词<br />
            </td>
            <td>
                <asp:TextBox ID="TxtMetaKeywords" runat="server" Height="65px" Width="360px" Columns="60"
                    Rows="4" TextMode="MultiLine" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>单页META网页描述：</strong><br />
                针对搜索引擎设置的网页描述<br />
            </td>
            <td>
                <asp:TextBox ID="TxtMetaDescription" runat="server" Height="65px" Width="360px" Columns="60"
                    Rows="4" TextMode="MultiLine" />
            </td>
        </tr>
        <tr class="tdbgbottom">
            <td colspan="2">
                <asp:Button ID="EBtnSubmit" Text="保存" OnClick="EBtnSubmit_Click" runat="server" />&nbsp;&nbsp;&nbsp;
                <input name="Cancel" type="button" class="inputbutton" id="BtnCancel" value="取消"
                    onclick="Redirect('CategoryManage.aspx')" />
            </td>
        </tr>
    </table>
</asp:Content>
