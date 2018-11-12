<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.User.Accessories.TrademarkList" Codebehind="TrademarkList.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>选择品牌</title>
    <base target="_self" />
</head>
<% TrademarkInput = Request.QueryString["OpenerText"];%>
<body>
    <form id="form2" runat="server">
        <div style="text-align: center">
            <table width="560" border="0" cellpadding="2" cellspacing="0" class="border">
                <tr class="title" style="height: 22">
                    <td align="left">
                        <b>已经选定的品牌：</b></td>
                    <td align="right" style="width: 177px">
                        <a href="javascript:window.close();">返回&gt;&gt;</a></td>
                </tr>
                <tr class="tdbg">
                    <td align="left">
                        <input type="text" id="TrademarkList" size="60" maxlength="200" readonly="readonly"
                            class="inputtext" /></td>
                    <td align="center" style="width: 177px">
                        <input type="button" class="inputbutton" name="del1" onclick="del(1)" value="删除最后" />
                        <input type="button" class="inputbutton" name="del2" onclick="del(0)" value="删除全部" /></td>
                </tr>
            </table>
            <br />
            <table width="560" border="0" cellpadding="2" cellspacing="0" class="border">
                <tr class="title">
                    <td align="left">
                        <a href="TrademarkList.aspx?OpenerText=<%= TrademarkInput %>&TrademarkType=-1">全部分类</a>
                        | <a href="TrademarkList.aspx?OpenerText=<%= TrademarkInput %>&TrademarkType=1">大陆品牌</a>
                        | <a href="TrademarkList.aspx?OpenerText=<%= TrademarkInput %>&TrademarkType=2">港台品牌</a>
                        | <a href="TrademarkList.aspx?OpenerText=<%= TrademarkInput %>&TrademarkType=3">日韩品牌</a>
                        | <a href="TrademarkList.aspx?OpenerText=<%= TrademarkInput %>&TrademarkType=4">欧美品牌</a>
                        | <a href="TrademarkList.aspx?OpenerText=<%= TrademarkInput %>&TrademarkType=0">其它品牌</a>
                    </td>
                </tr>
            </table>
            <br />
            <table width="560" border="0" cellpadding="2" cellspacing="0" class="border">
                <tr class="title">
                    <td>
                        <b>品牌列表：</b></td>
                    <td align="right">
                        <input name="Trademark" type="text" size="20" value="" class="inputtext" />&nbsp;&nbsp;<input
                            type="submit" value="查找" /></td>
                </tr>
                <tr>
                    <td valign="top" style="height: 100px" colspan="2">
                        <asp:Repeater ID="RepTrademarks" runat="server">
                            <HeaderTemplate>
                                <table width="550" border="0" cellspacing="1" cellpadding="1">
                                    <tr>
                                        <td style="width: 30%" align="center">
                                            品牌名称
                                        </td>
                                        <td style="width: 70%" align="center">
                                            简介
                                        </td>
                                    </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr style="background-color: #f9f9f9">
                                    <td align="center">
                                        <a href="#" onclick="<%# "add('" + Eval("TrademarkName") + "')"%>">
                                            <%# Eval("TrademarkName")%>
                                        </a>
                                    </td>
                                    <td align="left">
                                        <%# Eval("TrademarkIntro")%>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </td>
                </tr>
            </table>
            <table border="0" align="center" cellpadding="2" cellspacing="0">
                <tr>
                    <td align="center">
                        <pe:AspNetPager ID="Pager" runat="server" OnPageChanged="Pager_PageChanged">
                        </pe:AspNetPager>
                    </td>
                </tr>
            </table>

            <script language="javascript" type="text/javascript">
    document.getElementById('TrademarkList').value = opener.document.getElementById('<%= TrademarkInput %>').value;
    function add(obj)
    {
        if(obj==""){return false;}
        if(opener.document.getElementById('<%= TrademarkInput %>').value=="")
        {
            opener.document.getElementById('<%= TrademarkInput %>').value=obj;
            document.getElementById('TrademarkList').value = opener.document.getElementById('<%= TrademarkInput %>').value;
            return false;
        }
        var singleTrademark=obj.split("|");
        var ignoreTrademark="";
        for(i=0;i<singleTrademark.length;i++)
        {
            if(checkTrademark(opener.document.getElementById('<%= TrademarkInput %>').value,singleTrademark[i]))
            {
                ignoreTrademark=ignoreTrademark+singleTrademark[i]+" ";
            }
            else
            {
                opener.document.getElementById('<%= TrademarkInput %>').value = opener.document.getElementById('<%= TrademarkInput %>').value + "|" + singleTrademark[i];
                document.getElementById('TrademarkList').value = opener.document.getElementById('<%= TrademarkInput %>').value;
            }
        }
        if(ignoreTrademark!="")
        {
            alert(ignoreTrademark+" 品牌已经存在，此操作已经忽略！");
        }
    }
    function del(num)
    {
        if (num==0 || opener.document.getElementById('<%= TrademarkInput %>').value=="" || opener.document.getElementById('<%= TrademarkInput %>').value=="|")
        {
            opener.document.getElementById('<%= TrademarkInput %>').value="";
            document.getElementById('TrademarkList').value="";
            return false;
        }
    
        var strDel=opener.document.getElementById('<%= TrademarkInput %>').value;
        var s=strDel.split("|");
        opener.document.getElementById('<%= TrademarkInput %>').value = strDel.substring(0,strDel.length-s[s.length-1].length-1);
        document.getElementById('TrademarkList').value = opener.document.getElementById('<%= TrademarkInput %>').value;
    }
    
    function checkTrademark(Trademarklist,thisTrademark)
    {
      if (Trademarklist==thisTrademark){
            return true;
      }
      else{
        var s=Trademarklist.split("|");
        for (j=0;j<s.length;j++){
            if(s[j]==thisTrademark)
                return true;
        }
        return false;
      }
    }
            </script>

        </div>
    </form>
</body>
</html>
