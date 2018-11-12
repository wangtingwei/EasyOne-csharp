<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false"
    ValidateRequest="false" Inherits="EasyOne.WebSite.User.Contents.Content" Codebehind="Content.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr tdbg>
                    <td class="spacingtitle" align="center">
                        <pe:AlternateLiteral ID="LblTitle" Text="内容添加" AlternateText="修改内容" runat="Server" />
                    </td>
                </tr>
                <tr>
                    <td style="height: 5px">
                    </td>
                </tr>
            </table>
            <table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="border">
                <asp:Repeater ID="RepContentForm" runat="server" OnItemDataBound="RepContentForm_OnItemDataBound">
                    <ItemTemplate>
                        <pe:FieldControl ID="Field" runat="server" EnableNull='<%# (bool)Eval("EnableNull") %>'
                            FieldAlias='<%# Eval("FieldAlias")%>' Tips='<%# Eval("Tips") %>' FieldName='<%#Eval("FieldName")%>'
                            ControlType='<%# Eval("FieldType") %>' FieldLevel='<%# Eval("FieldLevel") %>'
                            IsAdminManage="false" Description='<%# Eval("Description")%>' Settings='<%# ((EasyOne.Model.CommonModel.FieldInfo)Container.DataItem).Settings %>'
                            Value='<%# Eval("DefaultValue") %>'>
                        </pe:FieldControl>
                    </ItemTemplate>
                </asp:Repeater>
                <tr class="tdbg">
                    <td colspan="2" align="center">
                        <asp:Button ID="BtnSave" runat="server" Text="提交" OnClick="BtnSave_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
