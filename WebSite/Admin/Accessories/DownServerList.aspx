<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Accessories.DownServerList" Codebehind="DownServerList.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>下载服务器列表</title>
    <base target="_self" />
</head>
<% downserverInput = Request.QueryString["OpenerText"];%>
<body>
    <form id="form1" runat="server">
        <div style="text-align: center">
            <table width="560" border="0" cellpadding="2" cellspacing="0" class="border">
                <tr class="title" style="height: 22">
                    <td valign="top">
                        <b>已经选定的下载服务器：</b></td>
                    <td align="right">
                        <a href="javascript:window.close();">返回&gt;&gt;</a></td>
                </tr>
                <tr class="tdbg">
                    <td>
                        <input type="text" id="KeyList" size="60" maxlength="200" readonly="readonly" class="inputtext" /></td>
                    <td align="center">
                        <input type="button" class="inputbutton" name="del1" onclick="del(1)" value="删除最后" />
                        <input type="button" class="inputbutton" name="del2" onclick="del(0)" value="删除全部" /></td>
                </tr>
            </table>
            <br />
            <table width="560" border="0" cellpadding="2" cellspacing="0" class="border">
                <tr class="title">
                    <td>
                        <b><font color="red"></font>列表：</b></td>
                    <td align="right">
                        <asp:TextBox ID="TxtDownServer" runat="server"></asp:TextBox>&nbsp;&nbsp;<asp:Button
                            ID="BtnSearch" runat="server" Text="查找" /></td>
                </tr>
                <tr>
                    <td valign="top" colspan="2">
                        <asp:Repeater ID="RepDownServers" runat="server" OnItemDataBound="RepDownServers_ItemDataBound">
                            <HeaderTemplate>
                                <table width="550" border="0" cellspacing="1" cellpadding="1" bgcolor="#f9f9f9">
                                    <tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <td align="center">
                                    <pe:ExtendedLabel HtmlEncode="false" ID="LblServerName" runat="server" Text=""></pe:ExtendedLabel>
                                </td>
                                <% 
                                    i++; %>
                                <% if (i % 4 == 0 && i > 1)
                                   {%>
                                </tr><tr>
                                    <%} %>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tr></table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </td>
                </tr>
                <tr class="tdbg">
                    <td align="center" colspan="2">
                        <a href="#" onclick="addall('<%=allDownServer %>')">增加以上所有下载服务器</a></td>
                </tr>
            </table>
        </div>

        <script language="javascript" type="text/javascript">
    document.getElementById("KeyList").value = opener.document.getElementById("<%= downserverInput %>").value;
    function add(obj)
    {
        if(obj==""){return false;}
        if(opener.document.getElementById("<%= downserverInput %>").value=="")
        {
            opener.document.getElementById("<%= downserverInput %>").value=obj;
            document.getElementById("KeyList").value = opener.document.getElementById("<%= downserverInput %>").value;
            return false;
        }
        var singleKey=obj.split("|");
        var ignoreKey="";
        for(i=0;i<singleKey.length;i++)
        {
            if(checkKey(opener.document.getElementById("<%= downserverInput %>").value,singleKey[i]))
            {
                ignoreKey=ignoreKey+singleKey[i]+" ";
            }
            else
            {
                opener.document.getElementById("<%= downserverInput %>").value = opener.document.getElementById("<%= downserverInput %>").value + "|" + singleKey[i];
                document.getElementById("KeyList").value = opener.document.getElementById("<%= downserverInput %>").value;
            }
        }
        if(ignoreKey!="")
        {
            alert(" 该下载服务器已经存在，此操作已经忽略！");
        }
    }
    function addall(obj)
    {
        document.getElementById("KeyList").value = obj;
        opener.document.getElementById("<%= downserverInput %>").value=obj;
    }
    function del(num)
    {
        if (num==0 || opener.document.getElementById("<%= downserverInput %>").value=="" || opener.document.getElementById("<%= downserverInput %>").value=="|")
        {
            opener.document.getElementById("<%= downserverInput %>").value="";
            document.getElementById("KeyList").value="";
            return false;
        }
    
        var strDel=opener.document.getElementById("<%= downserverInput %>").value;
        var s=strDel.split("|");
        opener.document.getElementById("<%= downserverInput %>").value = strDel.substring(0,strDel.length-s[s.length-1].length-1);
        document.getElementById("KeyList").value = opener.document.getElementById("<%= downserverInput %>").value;
    }
    
    function checkKey(Keylist,thisKey)
    {
      if (Keylist==thisKey){
            return true;
      }
      else{
        var s=Keylist.split("|");
        for (j=0;j<s.length;j++){
            if(s[j]==thisKey)
                return true;
        }
        return false;
      }
    }
        </script>

    </form>
</body>
</html>
