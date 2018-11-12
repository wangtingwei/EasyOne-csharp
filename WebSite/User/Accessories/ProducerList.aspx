<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.User.Accessories.ProducerList" Codebehind="ProducerList.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>选择厂商</title>
    <base target="_self" />
</head>
<% ProducerInput = Request.QueryString["OpenerText"];%>
<body>
    <form id="form2" runat="server">
        <div style="text-align: center">
            <table width="560" border="0" cellpadding="2" cellspacing="0" class="border">
                <tr class="title" style="height: 22">
                    <td align="left">
                        <b>已经选定的厂商：</b></td>
                    <td align="right" style="width: 177px">
                        <a href="javascript:window.close();">返回&gt;&gt;</a></td>
                </tr>
                <tr class="tdbg">
                    <td align="left">
                        <input type="text" id="ProducerList" size="60" maxlength="200" readonly="readonly"
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
                        <a href="ProducerList.aspx?OpenerText=<%= ProducerInput %>&ProducerType=-1">全部分类</a>
                        | <a href="ProducerList.aspx?OpenerText=<%= ProducerInput %>&ProducerType=1">大陆厂商</a>
                        | <a href="ProducerList.aspx?OpenerText=<%= ProducerInput %>&ProducerType=2">港台厂商</a>
                        | <a href="ProducerList.aspx?OpenerText=<%= ProducerInput %>&ProducerType=3">日韩厂商</a>
                        | <a href="ProducerList.aspx?OpenerText=<%= ProducerInput %>&ProducerType=4">欧美厂商</a>
                        | <a href="ProducerList.aspx?OpenerText=<%= ProducerInput %>&ProducerType=0">其它厂商</a>
                    </td>
                </tr>
            </table>
            <br />
            <table width="560" border="0" cellpadding="2" cellspacing="0" class="border">
                <tr class="title">
                    <td>
                        <b>厂商列表：</b></td>
                    <td align="right">
                        <input name="Producer" type="text" size="20" value="" class="inputtext" />&nbsp;&nbsp;<input
                            type="submit" value="查找" /></td>
                </tr>
                <tr>
                    <td valign="top" style="height: 100px" colspan="2">
                        <asp:Repeater ID="RepProducers" runat="server">
                            <HeaderTemplate>
                                <table width="550" border="0" cellspacing="1" cellpadding="1">
                                    <tr>
                                        <td style="width: 20%" align="center">
                                            厂商名称
                                        </td>
                                        <td style="width: 15%" align="center">
                                            厂商缩写
                                        </td>
                                        <td style="width: 70%" align="center">
                                            简介
                                        </td>
                                    </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr style="background-color: #f9f9f9">
                                    <td align="center">
                                        <a href="#" onclick="<%# "add('" + Eval("ProducerName") + "')"%>">
                                            <%# Eval("ProducerName")%>
                                        </a>
                                    </td>
                                    <td align="center">
                                        <%# Eval("ProducerShortName")%>
                                    </td>
                                    <td align="left">
                                        <%# Eval("ProducerIntro")%>
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
    document.getElementById('ProducerList').value = opener.document.getElementById('<%= ProducerInput %>').value;
    function add(obj)
    {
        if(obj==""){return false;}
        if(opener.document.getElementById('<%= ProducerInput %>').value=="")
        {
            opener.document.getElementById('<%= ProducerInput %>').value=obj;
            document.getElementById('ProducerList').value = opener.document.getElementById('<%= ProducerInput %>').value;
            return false;
        }
        var singleProducer=obj.split("|");
        var ignoreProducer="";
        for(i=0;i<singleProducer.length;i++)
        {
            if(checkProducer(opener.document.getElementById('<%= ProducerInput %>').value,singleProducer[i]))
            {
                ignoreProducer=ignoreProducer+singleProducer[i]+" ";
            }
            else
            {
                opener.document.getElementById('<%= ProducerInput %>').value = opener.document.getElementById('<%= ProducerInput %>').value + "|" + singleProducer[i];
                document.getElementById('ProducerList').value = opener.document.getElementById('<%= ProducerInput %>').value;
            }
        }
        if(ignoreProducer!="")
        {
            alert(ignoreProducer+" 厂商已经存在，此操作已经忽略！");
        }
    }
    function del(num)
    {
        if (num==0 || opener.document.getElementById('<%= ProducerInput %>').value=="" || opener.document.getElementById('<%= ProducerInput %>').value=="|")
        {
            opener.document.getElementById('<%= ProducerInput %>').value="";
            document.getElementById('ProducerList').value="";
            return false;
        }
    
        var strDel=opener.document.getElementById('<%= ProducerInput %>').value;
        var s=strDel.split("|");
        opener.document.getElementById('<%= ProducerInput %>').value = strDel.substring(0,strDel.length-s[s.length-1].length-1);
        document.getElementById('ProducerList').value = opener.document.getElementById('<%= ProducerInput %>').value;
    }
    
    function checkProducer(Producerlist,thisProducer)
    {
      if (Producerlist==thisProducer){
            return true;
      }
      else{
        var s=Producerlist.split("|");
        for (j=0;j<s.length;j++){
            if(s[j]==thisProducer)
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
