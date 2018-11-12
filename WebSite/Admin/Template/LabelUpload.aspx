<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Template.LabelUpload" Codebehind="LabelUpload.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>上传标签</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table width="560" border="0" cellpadding="2" cellspacing="0" class="border">
                <tr class="title" style="height: 22">
                    <td valign="top">
                        <b>请选择符合规范的标签文件</b></td>
                    <td align="right">
                        <a href="javascript:window.close();">返回&gt;&gt;</a></td>
                </tr>
                <tr class="tdbg">
                    <td colspan="2">
                        <asp:FileUpload ID="FileUpload1" Width="400" runat="server" /><pe:RequiredFieldValidator
                            ID="ValrFileUpload" ControlToValidate="FileUpload1" runat="server" ErrorMessage="请选择文件"
                            Display="Dynamic"></pe:RequiredFieldValidator>
                    </td>
                </tr>
                <tr class="tdbg">
                    <td colspan="2">
                        <pe:ExtendedButton ID="EBtnUpload" runat="server" Text="上传" OnClick="EBtnUpload_Click" /><input
                            type="button" class="inputbutton" onclick="window.close();" value="取消" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
