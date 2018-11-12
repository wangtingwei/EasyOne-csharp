<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" ValidateRequest="false" Inherits="EasyOne.WebSite.Admin.Template.LabelBatch" Title="标签批处理" Codebehind="LabelBatch.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="server">
    <table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="border">
        <tr>
            <td class="spacingtitle" colspan="2" align="center">
                <span id="LblTitle" style="font-weight: bold;">标签批量设置</span></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right" style="width: 105px">
                <strong>选定规则：&nbsp;</strong></td>
            <td class="tdbg" align="left">
                <asp:RadioButtonList ID="RbtSearType" runat="server" AutoPostBack="True" RepeatDirection="Horizontal" OnSelectedIndexChanged="RbtSearType_SelectedIndexChanged">
                    <asp:ListItem Value="all" Selected="True">全部标签</asp:ListItem>
                    <asp:ListItem Value="type">按分类选择</asp:ListItem>
                    <asp:ListItem Value="keyword">按搜索结果</asp:ListItem>
                </asp:RadioButtonList>
                <asp:DropDownList ID="RbtLabelType" runat="server" Visible="false">
                </asp:DropDownList>
                <asp:TextBox ID="KeyWord" runat="server" Text="关键字" Visible="false"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right" style="width: 105px">
                <strong>替换部位：&nbsp;</strong></td>
            <td class="tdbg" align="left">
                <asp:RadioButtonList ID="RadioButtonList2" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="all" Selected="True">全部替换</asp:ListItem>
                    <asp:ListItem Value="labelname">标签名称</asp:ListItem>
                    <asp:ListItem Value="labeltype">标签分类</asp:ListItem>
                    <asp:ListItem Value="labelintro">标签说明</asp:ListItem>
                    <asp:ListItem Value="sql">查询语句</asp:ListItem>
                    <asp:ListItem Value="template">标签模板</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right" style="width: 105px">
                <strong>替换内容：&nbsp;</strong></td>
            <td class="tdbg" align="left">
                替换目标：<asp:TextBox ID="ReplaceSource" runat="server"></asp:TextBox><br />
                替换结果：<asp:TextBox ID="ReplaceTarget" runat="server"></asp:TextBox><br />
            </td>
        </tr>
        <tr class="tdbg">
            <td colspan="2" align="center">
                <asp:Button ID="BtnFinal" runat="server" Text="开始替换" OnClick="BtnFinal_Click" />
                &nbsp;&nbsp;<asp:CheckBox ID="ChkAdd" runat="server" />添加为新标签
            </td>
        </tr>
    </table>    
</asp:Content>
