<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Template.Template_AddPage" Codebehind="Template_addPage.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>分页标签编辑器-<% =RequestString("n") %></title>
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="Expires" content="0" />
</head>
<body>
    <form runat="server" id="form1">
        <table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0">
        <tr height="30"><td bgcolor="#999999" valign="middle"><img src="../../Admin/Images/img_u.gif" alt="" /><span style="color: #00FF00;font-weight: bold;font-size: 18px;"><asp:Label ID="LabelName" runat="server"></asp:Label></asp:Label></span></td></tr>
        <tr><td align="center"><div style="height:185px; border:1px double #ffffff">
            <pe:ExtendedLabel HtmlEncode="false" ID="labelbody" runat="server"></pe:ExtendedLabel>
        </div>
        </td></tr>
        <tr height="30">
           <td bgcolor="#999999" color="#ffffff" align="center">
                <input type="button" class="inputbutton" value="确定" onclick="submitdate();" />&nbsp;&nbsp;&nbsp;&nbsp;<input
                    type="button" class="inputbutton" value="取消" onclick="window.close();" /></div>
           </td>
        </tr>
        </table>
        <script language="JavaScript" type="text/javascript">
    <!--
    function submitdate(){
        var spantype = document.getElementById("spantype").value;
        var cssname = document.getElementById("cssname").value;
        var returnstr = "{PE.Page id=\"" + document.getElementById("<% =LabelName.ClientID %>").innerHTML + "\"";   
        var oSelectArr = document.getElementById("datasource");
        if(oSelectArr != null)
        {  
            for(var j=0;j<oSelectArr.length;j++)
            {
                if(oSelectArr[j].selected==true)
                {
                    returnstr += " datasource=\"" + oSelectArr[j].value + "\"";
                }
            }
        }
  
        if(spantype != "")
        {
            returnstr += " span=\"" + spantype + "\"";
        }
  
        if(cssname != "")
        {
            returnstr += " class=\"" + cssname + "\"";
        }

        if(window.showModalDialog != null)
        {
            window.returnValue = returnstr + "/}";
        }
        else
        {
            var pre = window.opener.document.getElementById('ctl00_CphContent_TxtTemplate').value.substr(0, window.opener.start);
            var post = window.opener.document.getElementById('ctl00_CphContent_TxtTemplate').value.substr(window.opener.end);
            window.opener.document.getElementById('ctl00_CphContent_TxtTemplate').value = pre + returnstr + " /}" + post;
        }
        window.close();
    }
    -->
        </script>

    </form>
</body>
</html>
