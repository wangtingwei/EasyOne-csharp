<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false"
    Inherits="EasyOne.WebSite.User.Contents.AnonymousContent" Codebehind="AnonymousContent.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>匿名投稿</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="border">
                <tr class ="tdbg">
                    <td class="spacingtitle" align="center">
                        <asp:Label ID="LblTitle" runat="server" Text="匿名投稿第一步"></asp:Label>
                    </td>
                </tr>
                <tr class ="tdbg">
                    <td style="height: 5px">
                        选择栏目：
                        <asp:DropDownList ID="DropNodeId" DataValueField="NodeId" DataTextField="NodeName"
                            runat="server" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="DropNode_SelectedIndexChanged">
                        </asp:DropDownList>
                        <pe:RequiredFieldValidator ID="ValeNodeId" ControlToValidate="DropNodeId" ErrorMessage="栏目不能为空！"
                            runat="server"></pe:RequiredFieldValidator>
                    </td>
                </tr>
                <tr class ="tdbg">
                    <td style="height: 5px">
                        选择模型：
                        <asp:DropDownList ID="DropModelId" runat="server" DataValueField="ModelId" DataTextField="ModelName">
                        </asp:DropDownList>
                        <pe:RequiredFieldValidator ID="ValeModelId" ControlToValidate="DropModelId" ErrorMessage="模型不能为空！"
                            runat="server"></pe:RequiredFieldValidator>
                    </td>
                </tr>
                <tr class="tdbg">
                    <td colspan="2" align="center">
                        <asp:Button ID="BtnSave" runat="server" Text="下一步" OnClick="BtnSave_Click" />&nbsp;&nbsp;
                        <asp:Button ID="BtnBack" runat="server" Text="返回首页" OnClick="BtnBack_Click"  />   
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
