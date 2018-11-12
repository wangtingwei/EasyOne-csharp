<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Survey.AnswerList" Codebehind="AnswerList.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>问题列表</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table border="0" cellpadding="2" cellspacing="1" class="border" align="center" style="width: 80%;">
        <tr class="tdbg">
        <td style="width: 60%; text-align: center">
            <asp:Label ID="LblTitle" runat="server" Font-Bold="True" ForeColor="Blue"></asp:Label>
        </td>
        </tr>
        <tr>
        <td class="tdbg">
            <asp:Repeater ID="RptAnswerList" runat="server" OnItemDataBound="RptAnswerList_ItemDataBound">
            <ItemTemplate>
            <table width="100%" border="0" style="text-align: center" cellpadding="2" cellspacing="1"
                class="border">
                <tr class="tdbg">
                    <asp:Literal ID="LtrAnswerList" runat="server"></asp:Literal>
                </tr>
            </table>
            </ItemTemplate>
            </asp:Repeater>
            <div style="width: 100%; text-align: center">
                <br />
                <input id="BtnReturn" type="button" class="inputbutton" value="返回" onclick="javaScript:history.back();" />
                <input id="BtnClose" type="button" class="inputbutton" value="关闭" onclick="javascript:window.close();" />
            </div>
        </td>
        </tr>
    </table>
    
    </div>
    </form>
</body>
</html>
