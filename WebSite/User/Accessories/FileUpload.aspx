<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.User.Accessories.FileUpload" Codebehind="FileUpload.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>�ϴ��ļ�</title>
</head>
<body class="tdbg">
    <form id="form1" runat="server" enctype="multipart/form-data">
        <table style="height: 100%; border: 0; width: 100%">
            <tr class="tdbg">
                <td valign="top">
                    <asp:FileUpload ID="FupFile" runat="server" /><asp:Button ID="BtnUpload" runat="server"
                        Text="�ϴ�" OnClick="BtnUpload_Click" /><pe:RequiredFieldValidator ID="ValFile" runat="server" RequiredText=""
                            ErrorMessage="��ѡ���ϴ�·��" ControlToValidate="FupFile"></pe:RequiredFieldValidator><asp:Label
                                ID="LblMessage" runat="server" ForeColor="red" Text=""></asp:Label></td>
            </tr>
        </table>
    </form>
</body>
</html>
