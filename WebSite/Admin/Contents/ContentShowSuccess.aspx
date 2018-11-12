<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="True"
    EnableEventValidation="false" Inherits="EasyOne.WebSite.Admin.Contents.ContentShowSuccess" Codebehind="ContentShowSuccess.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <center>
        <table class="border" border="0" cellpadding="2" cellspacing="1">
            <tr class="title">
                <td align="center" colspan="2">
                    <asp:Label ID="LblType" runat="server" Text="" />
                </td>
            </tr>
            <tr class="tdbg">
                <td style="width: 100px;" align="right" class="tdbgleft">
                    <strong>所属栏目：</strong></td>
                <td style="width: 400px;" align="left">
                    <asp:Label ID="LblNode" runat="server" Text="" />
                </td>
            </tr>
            <tr class="tdbg">
                <td align="right" class="tdbgleft">
                    <strong>标题：</strong></td>
                <td align="left">
                    <asp:Label ID="LblTitle" runat="server" Text="" />
                </td>
            </tr>
            <tr class="tdbg" id="Author" runat="server">
                <td align="right" class="tdbgleft">
                    <strong>作者：</strong></td>
                <td align="left">
                    <asp:Label ID="LblAuthor" runat="server" Text="" />
                </td>
            </tr>
            <tr class="tdbg" id="CopyFrom" runat="server">
                <td align="right" class="tdbgleft">
                    <strong>来源：</strong></td>
                <td align="left">
                    <asp:Label ID="LblCopyFrom" runat="server" Text="" />
                </td>
            </tr>
            <tr class="tdbg" id="Keylord" runat="server">
                <td align="right" class="tdbgleft">
                    <strong>关键字：</strong></td>
                <td align="left">
                    <asp:Label ID="LblKeylord" runat="server" Text="" />
                </td>
            </tr>
            <tr class="tdbg">
                <td align="right" class="tdbgleft">
                    <strong>状态：</strong></td>
                <td align="left">
                    <asp:Label ID="LblStatus" runat="server" Text="" />
                </td>
            </tr>
            <tr class="tdbg" align="center">
                <td style="height: 30px;" colspan="2">
                    【<a href="Content.aspx?Action=modify&GeneralID=<%=m_GeneralId%>&NodeID=<%=m_NodeId%>&ModelID=<%=m_ModelId%>">重新修改</a>】&nbsp;
                    【<a href="Content.aspx?Action=add&NodeID=<%=m_NodeId%>&ModelID=<%=m_ModelId%>">继续添加</a>】&nbsp;
                    【<a href="ContentManage.aspx?NodeId=<%=m_NodeId%>&ModelID=<%=m_ModelId%>">管理</a>】&nbsp;
                    【<pe:ExtendedLiteral HtmlEncode="false" ID="ShowContentPreview" runat="server" Text=""></pe:ExtendedLiteral>】
                </td>
            </tr>
        </table>
    </center>
</asp:Content>
