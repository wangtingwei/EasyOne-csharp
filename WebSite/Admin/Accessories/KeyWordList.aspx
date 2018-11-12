<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Accessories.KeyWordList" Codebehind="KeyWordList.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>关键字列表</title>
    <base target="_self" />
</head>
<% keywordInput = Request.QueryString["OpenerText"];%>
<body>
    <form id="form1" runat="server">
        <div style="text-align: center">
            <table width="560" border="0" cellpadding="2" cellspacing="0" class="border">
                <tr class="title" style="height: 22">
                    <td valign="top">
                        <b>已经选定的关键字：</b></td>
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
                        <asp:TextBox ID="TxtKeyWord" runat="server"></asp:TextBox>&nbsp;&nbsp;<asp:Button
                            ID="BtnSearch" runat="server" Text="查找" /></td>
                </tr>
                <tr>
                    <td valign="top" colspan="2">
                        <asp:Repeater ID="RepKeyWords" runat="server" OnItemDataBound="RepKeyWords_ItemDataBound">
                            <HeaderTemplate>
                                <table width="550" border="0" cellspacing="1" cellpadding="1" bgcolor="#f9f9f9">
                                    <tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <td align="center">
                                <pe:ExtendedLiteral ID="LitKeyword" runat="server"></pe:ExtendedLiteral>
                                </td>
                                <% 
                                    i++; %>
                                <% if (i % 5 == 0 && i > 1)
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
                        <a href="#" onclick="add('<%=allKeyword %>')">增加以上所有关键字</a></td>
                </tr>
            </table>
            <table border="0" cellpadding="2" cellspacing="0">
                <tr>
                    <td align="center">
                        <pe:AspNetPager ID="Pager" runat="server" OnPageChanged="Pager_PageChanged">
                        </pe:AspNetPager>
                    </td>
                </tr>
            </table>
        </div>

        <script language="javascript" type="text/javascript">
    document.getElementById("KeyList").value = opener.document.getElementById("<%= keywordInput %>").value;
    function add(obj)
    {
        if(obj==""){return false;}
        if(opener.document.getElementById("<%= keywordInput %>").value=="")
        {
            opener.document.getElementById("<%= keywordInput %>").value=obj;
            document.getElementById("KeyList").value = opener.document.getElementById("<%= keywordInput %>").value;
            return false;
        }
        var singleKey=obj.split(" ");
        var ignoreKey="";
        for(i=0;i<singleKey.length;i++)
        {
            if(checkKey(opener.document.getElementById("<%= keywordInput %>").value,singleKey[i]))
            {
                ignoreKey=ignoreKey+singleKey[i]+" ";
            }
            else
            {
                opener.document.getElementById("<%= keywordInput %>").value = opener.document.getElementById("<%= keywordInput %>").value + " " + singleKey[i];
                document.getElementById("KeyList").value = opener.document.getElementById("<%= keywordInput %>").value;
            }
        }
        if(ignoreKey!="")
        {
            alert(ignoreKey+" 关键字已经存在，此操作已经忽略！");
        }
    }
    function del(num)
    {
        if (num==0 || opener.document.getElementById("<%= keywordInput %>").value=="" || opener.document.getElementById("<%= keywordInput %>").value==" ")
        {
            opener.document.getElementById("<%= keywordInput %>").value="";
            document.getElementById("KeyList").value="";
            return false;
        }
    
        var strDel=opener.document.getElementById("<%= keywordInput %>").value;
        var s=strDel.split(" ");
        opener.document.getElementById("<%= keywordInput %>").value = strDel.substring(0,strDel.length-s[s.length-1].length-1);
        document.getElementById("KeyList").value = opener.document.getElementById("<%= keywordInput %>").value;
    }
    
    function checkKey(Keylist,thisKey)
    {
      if (Keylist==thisKey){
            return true;
      }
      else{
        var s=Keylist.split(" ");
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
