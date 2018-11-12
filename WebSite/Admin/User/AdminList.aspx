<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.User.AdminList" Codebehind="AdminList.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>选择对话框</title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="width: 100%; text-align: center;">
            <table width="100%" border="0" style="text-align: center" cellpadding="2" cellspacing="0"
                class="border">
                <tr class="title">
                    <td style="text-align: right">
                        查找管理员：
                        <asp:TextBox ID="TxtKeyWord" runat="server" />&nbsp;<asp:Button ID="BtnSearch" runat="server"
                            Text="查找" OnClick="BtnSearch_Click" />
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:DataList ID="DlstAdmin" runat="server" CssClass="tdbg" RepeatColumns="5" RepeatDirection="Horizontal"
                            Width="100%">
                            <ItemStyle HorizontalAlign="Center" Width="20%" />
                            <ItemTemplate>
<%--                                <a href="#" onclick="<%#string.Format("window.returnValue='{0}$$${1}';window.close();",Eval("AdminName"),Eval("AdminId"))%>">
                                    <%#Eval("AdminName")%>
                                </a>--%>                            
                                <a href="#" onclick='<%#string.Format("window.opener." + m_JsFunctionName + "(\"{0}$$${1}\");window.close();",Eval("AdminName"),Eval("AdminId"))  %>'><%#Eval("AdminName")%></a>
</ItemTemplate>
                        </asp:DataList>
                    </td>
                </tr>
            </table>
        </div>
        <div style="text-align: center;">
            <pe:AspNetPager ID="Pager" runat="server" OnPageChanged="Pager_PageChanged">
            </pe:AspNetPager>
        </div>
    </form>
</body>
</html>
