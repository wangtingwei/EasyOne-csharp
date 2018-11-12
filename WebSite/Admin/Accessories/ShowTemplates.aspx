<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Accessories.ShowTemplates" Codebehind="ShowTemplates.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>模板显示</title>
</head>
<body class="tdbg">
    <form id="form1" runat="server">
        <div style="width: 100%; height: 100%;">
            <asp:Repeater ID="RepFiles" runat="server" OnItemDataBound="RepFiles_ItemDataBound">
                <ItemTemplate>
                    <%#  System.Convert.ToInt32(Eval("type")) == 1 ? "<img src=\"../../Admin/Images/Node/closefolder.gif\" style=border-right: 0px; border-top: 0px;border-left: 0px; border-bottom: 0px />" : "<img src=\"../../Admin/Images/Node/singlepage.gif\" style=border-right: 0px; border-top: 0px;border-left: 0px; border-bottom: 0px />"%>
                    <a href="#" onclick="<%#  System.Convert.ToInt32(Eval("type")) == 1 ?"openfilesdir('" + Eval("Name") + "')":"add('" + Eval("Name") + "')"%>">
                        <%# Eval("Name")%>
                    </a>
                    <br />
                </ItemTemplate>
            </asp:Repeater>
            <asp:HiddenField ID="HdnFileText" runat="server" />
        </div>

        <script language="javascript" type="text/javascript">
    function add(obj)
    {
        if(obj==""){return false;}
        parent.document.getElementById('FileName').value = document.getElementById('HdnFileText').value + "/" + obj;
    }
    function openfilesdir(obj)
    {
        if(obj==""){return false;}
        var pathtext="<%= Server.UrlEncode(Request.QueryString["FilesDir"]) %>";//如果是

        parent.document.getElementById('ParentDirText').value = pathtext;
        var path="ShowTemplates.aspx?FilesDir="+pathtext+"/"+escape(obj);
        parent.document.getElementById('main_right').src=path;
    }
        </script>

    </form>
</body>
</html>
