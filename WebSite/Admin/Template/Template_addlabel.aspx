<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Template.Template_Addlabel" Codebehind="Template_addlabel.aspx.cs" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>标签编辑器-<% =RequestString("n") %></title>
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="Expires" content="0" />
</head>
<body>
    <form runat="server" id="form1">
    <table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0">
        <tr height="30">
            <td bgcolor="#999999" valign="middle">
                <img src="../../Admin/Images/img_u.gif" alt="" /><span style="color: #0000FF; font-weight: bold;
                    font-size: 18px;"><asp:Label ID="LabelName" runat="server"></asp:Label></span>
            </td>
        </tr>
        <tr>
            <td>
                <div style="height: 100%; overflow: auto; border: 1px double #ffffff">
                    <div style="width: 100%; text-align: center; background: #9999bb">
                        <asp:Label ID="labelintro" runat="server">无标签说明</asp:Label></div>
                    <div style="width: 95%; text-align: center">
                        <pe:ExtendedLabel HtmlEncode="false" ID="labelbody" runat="server"></pe:ExtendedLabel></div>
                </div>
            </td>
        </tr>
        <tr height="40">
            <td bgcolor="#999999" color="#ffffff" align="center">
                <input id="InstertAllParm" type="checkbox" /><label for="InstertAllParm">插入所有参数</label><span style="color:Red">不选择则等于参数默认值的参数不插入 。</span><br />
                <input type="button" class="inputbutton" value="添加为标签" onclick="submitdate();" />&nbsp;&nbsp;<input
                    type="button" class="inputbutton" value="添加为数据源" onclick="submitdate(0);" />&nbsp;&nbsp;<input
                        type="button" class="inputbutton" value="取消" onclick="window.close();" />
            </td>
        </tr>
    </table>

    <script language="JavaScript" type="text/javascript">
    <!--
    function submitdate(type){
        var returnstr;
        if(type == 0)
        {
            returnstr = "{PE.DataSource datasource=\"" + document.getElementById("<% =LabelName.ClientID %>").innerHTML + "\" id=\"" + document.getElementById("<% =LabelName.ClientID %>").innerHTML + "\"";
        }
        else
        {
            returnstr = "{PE.Label id=\"" + document.getElementById("<% =LabelName.ClientID %>").innerHTML + "\"";
        }
        var oSpanArr = document.getElementsByTagName('SPAN');
        var isInstertAllParm = document.getElementById("InstertAllParm").checked;
        for ( var i=0; i<oSpanArr.length; i++ ) 
        {
            var defaultvalue = oSpanArr[i].getAttribute("defaultvalue");
            switch (oSpanArr[i].getAttribute("stype"))
            {
            case "0":
                var pvalue = document.getElementById(oSpanArr[i].getAttribute("sid")).value;
                if(pvalue != "" && (pvalue != defaultvalue || isInstertAllParm) && i > 0)
                    returnstr += " " + oSpanArr[i].getAttribute("sid") + "=\"" + pvalue + "\"";
                break;
            case "1":
                var oSelectArr=document.getElementById(oSpanArr[i].getAttribute("sid"));
                for(var j=0;j<oSelectArr.length;j++)
                {
                    if(oSelectArr[j].selected==true && (j != defaultvalue || isInstertAllParm))
                    {
                        returnstr += " " + oSpanArr[i].getAttribute("sid") + "=\"" + j + "\"";
                    }
                }
                break;
            case "2":
                if(document.getElementById(oSpanArr[i].getAttribute("sid")).checked)
                {
                    if(defaultvalue != "true" || isInstertAllParm)
                    {
                    returnstr += " " + oSpanArr[i].getAttribute("sid") + "=\"true\"";
                    }
                }
                else
                {
                    if(defaultvalue != "false" || isInstertAllParm)
                    {
                    returnstr += " " + oSpanArr[i].getAttribute("sid") + "=\"false\"";
                    }
                }
                break;
            case "3":
                var oSelectArr=document.getElementById(oSpanArr[i].getAttribute("sid"));
                var numlist = "";
                for(var j=0;j<oSelectArr.length;j++)
                {
                    if(oSelectArr[j].selected==true)
                    {
                        if(numlist != "")
                        {
                            numlist += ",";
                        }
                        numlist += oSelectArr[j].value;
                    }
                }
                if(numlist != "")
                {
                if(defaultvalue != numlist || isInstertAllParm)
                {
                    returnstr += " " + oSpanArr[i].getAttribute("sid") + "=\"" + numlist + "\"";
                    }
                }
                break;
            }
        }
        
        if(document.getElementById('page') != null)
        {
            if(document.getElementById('page').checked == true)
            {
                returnstr += " page=\"true\"";
                returnstr += " pagesize=\"" + document.getElementById("pagesize").value + "\"";
                if(document.getElementById('urlpage').checked == true)
                {
                    returnstr += " urlpage=\"true\"";
                }
            }
        }
        
        if(document.getElementById('cachetime') != null)
        {
            if(document.getElementById("cachetime").value != "0")
            {
                returnstr += " cachetime=\"" + document.getElementById("cachetime").value + "\"";
            }
        }
        
        if(document.getElementById('nolabelproc').checked == true)
        {
            returnstr += " noprocinlabel=\"true\"";
        }
        
        if(document.getElementById("spantype").value != "")
        {
            returnstr += " span=\"" + document.getElementById("spantype").value + "\"";
        }
        
        if(document.getElementById("cssname").value != "")
        {
            returnstr += " class=\"" + document.getElementById("cssname").value + "\"";
        }

        if(window.showModalDialog != null)
        {
            window.returnValue = returnstr + " /}";
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
