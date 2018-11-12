<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" Inherits="EasyOne.WebSite.User.Contents.AnonymousContent2" Codebehind="AnonymousContent2.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>匿名投稿</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
            <table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="border">
                <tr>
                    <td class="spacingtitle" align="center" colspan ="2">
                        <asp:Label id="LblTitle"   runat="server" Text="匿名投稿第二步"></asp:Label>
                    </td>
                </tr>
                <asp:Repeater ID="RepContentForm" runat="server" OnItemDataBound="RepContentForm_OnItemDataBound">
                    <ItemTemplate>
                        <pe:FieldControl ID="Field" runat="server" EnableNull='<%# (bool)Eval("EnableNull") %>'
                            FieldAlias='<%# Eval("FieldAlias")%>' Tips='<%# Eval("Tips") %>' FieldName='<%#Eval("FieldName")%>'
                            ControlType='<%# Eval("FieldType") %>' FieldLevel='<%# Eval("FieldLevel") %>' IsAdminManage="false"
                            Description='<%# Eval("Description")%>' Settings='<%# ((EasyOne.Model.CommonModel.FieldInfo)Container.DataItem).Settings %>'
                            Value='<%# Eval("DefaultValue") %>'>
                        </pe:FieldControl>
                    </ItemTemplate>
                </asp:Repeater>
                <tr class="tdbg">
                    <td colspan="2" align="center">
                       <asp:Button ID="BtnSave" runat="server" Text="提交" OnClick="BtnSave_Click" />&nbsp;&nbsp;
                       <asp:Button ID="BtnBack" runat="server" Text="返回第一步" OnClick="BtnBack_Click"  CausesValidation="False" />   
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
