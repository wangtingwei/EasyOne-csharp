<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Accessories.TemplateList" Codebehind="TemplateList.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>�ļ�����</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table style="width: 100%; height: 100%" border="0" cellpadding="2" cellspacing="0"
                class="border">
                <tr style="height: 100%; width: 100%">
                    <td class="tdbg" align="center" valign="top" style="width: 30%;">
                        <iframe id="fileList" name="fileList" scrolling="yes" style="width: 100%; height: 100%"
                            src="TemplateTree.aspx" frameborder="0"  title=""></iframe>
                    </td>
                    <td class="tdbg" align="left" valign="top" colspan="2" style="width: 70%;">
                        <iframe id="main_right" name="main_right" scrolling="yes" style="width: 100%; height: 100%"
                            src="ShowTemplates.aspx" frameborder="0" marginheight="0" marginwidth="1"></iframe>
                    </td>
                </tr>
                <tr class="title" style="height: 22; width: 177px">
                    <td style="width: 103px" align="right">
                        �ļ����ƣ�</td>
                    <td align="left">
                        <input type="text" id="FileName" size="60" style="height:22px;" readonly="readOnly" class="inputtext" />
                        <input type="hidden" id="ParentDirText" />
                        <asp:HiddenField ID="HdnParentDir" runat="server" />
                    </td>
                    <td align="center" style="width: 177px">
                        <input type="button" class="inputbutton" id="BtnSubmit" value="��ȷ����" onclick="javascript:window.close();add()" />
                        <input type="button" class="inputbutton" id="BtnCancel" value="��ȡ����" onclick="javascript:window.close();" />
                    </td>
                </tr>
            </table>
        </div>
    </form>

    <script language="javascript" type="text/javascript">
    function add()
    {
        opener.document.getElementById('<%= FilePathInput %>').value = document.getElementById('FileName').value;
    }
    </script>

</body>
</html>
