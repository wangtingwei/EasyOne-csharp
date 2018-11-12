<%@ Control Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Controls.AuthorListControl" Codebehind="AuthorListControl.ascx.cs" %>
 <% AuthorInput = Request.QueryString["OpenerText"];%>
        <div style="text-align: center">
            <table width='560' border='0' cellpadding='2' cellspacing='0' class='border'>
                <tr class='title' style="height: 22">
                    <td valign='top'>
                        <b>已经选定的作者：</b></td>
                    <td align='right' style="width: 177px">
                        <a href='javascript:window.close();'>返回&gt;&gt;</a></td>
                </tr>
                <tr class='tdbg'>
                    <td>
                        <input type="text" id="AuthorList" size="60" maxlength="200" readonly="readonly"
                            class="inputtext" /></td>
                    <td align='center' style="width: 177px">
                        <input type="button" class="inputbutton" name="del1" onclick="del(1)" value="删除最后" />
                        <input type="button" class="inputbutton" name="del2" onclick="del(0)" value="删除全部" /></td>
                </tr>
            </table>
            <br />
            <table width="560" border="0" cellpadding="2" cellspacing="0" class="border">
                <tr class="title">
                    <td align="left">
                        <a href="AuthorList.aspx">全部分类</a>
                        <asp:Repeater ID="RptAuthorType" runat="server">
                            <ItemTemplate>
                                | <a onclick="OpenMainRightType('<%# Eval("DataTextField") %>')" href="#">
                                    <%# Eval("DataTextField")%>
                                </a>
                            </ItemTemplate>
                        </asp:Repeater>
                    </td>
                </tr>
            </table>
            <br />
            <table width="560" border="0" cellpadding="2" cellspacing="0" class="border">
                <tr class="title">
                    <td>
                        <b>作者列表：</b></td>
                    <td align="right">姓名：
                        <input id="HdnListType" type="hidden" />
                        <input name='Author' type='text' size='20' class="inputtext" />&nbsp;&nbsp;<input
                            type='submit' value='查找' /></td>
                </tr>
                <tr>
                    <td valign='top' style="height: 100px" colspan="2">
                        <asp:Repeater ID="RepAuthors" runat="server">
                            <HeaderTemplate>
                                <table width='550' border='0' cellspacing='1' cellpadding='1'>
                                    <tr>
                                        <td style="width: 20%" align='center'>
                                            姓名
                                        </td>
                                        <td style="width: 10%" align='center'>
                                            性别
                                        </td>
                                        <td style="width: 70%" align='center'>
                                            简介
                                        </td>
                                    </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr style="background-color: #f9f9f9">
                                    <td align='center'>
                                        <a href='#' onclick="<%# "add('" + Eval("Name") + "')"%>">
                                            <%# Eval("Name")%>
                                        </a>
                                    </td>
                                    <td align='center'>
                                        <%# (int)Eval("Sex") == 1 ? "男" : "女"%>
                                    </td>
                                    <td align="left">
                                        <%# Eval("Intro")%>
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
            <table border="0"  cellpadding="2" cellspacing="0">
                <tr>
                    <td align="center">
                        <pe:AspNetPager ID="Pager" runat="server" OnPageChanged="Pager_PageChanged">
                        </pe:AspNetPager>
                    </td>
                </tr>
            </table>

            <script language="javascript" type="text/javascript">
            
     function OpenMainRightType(value)
    {
        var url= "AuthorList.aspx?ListType=4&OpenerText=<%= AuthorInput %>&SearchType="+escape(value);
        document.getElementById("HdnListType").value=5;
        window.location =url; 
    }
          
           
    
    function add(obj)
    {
        if(obj==""){return alert('dfs');}
        if(opener.document.getElementById('<%= AuthorInput %>').value=="")
        {
            opener.document.getElementById('<%= AuthorInput %>').value=obj;
            document.getElementById('AuthorList').value = opener.document.getElementById('<%= AuthorInput %>').value;
            return false;
        }
        var singleAuthor=obj.split("|");
        var ignoreAuthor="";
        for(i=0;i<singleAuthor.length;i++)
        {
            if(checkAuthor(opener.document.getElementById('<%= AuthorInput %>').value,singleAuthor[i]))
            {
                ignoreAuthor=ignoreAuthor+singleAuthor[i]+" ";
            }
            else
            {
                opener.document.getElementById('<%= AuthorInput %>').value = opener.document.getElementById('<%= AuthorInput %>').value + "|" + singleAuthor[i];
                document.getElementById('AuthorList').value = opener.document.getElementById('<%= AuthorInput %>').value;
            }
        }
        if(ignoreAuthor!="")
        {
            alert(ignoreAuthor+" 作者已经存在，此操作已经忽略！");
        }
    }
    function del(num)
    {
        if (num==0 || opener.document.getElementById('<%= AuthorInput %>').value=="" || opener.document.getElementById('<%= AuthorInput %>').value=="|")
        {
            opener.document.getElementById('<%= AuthorInput %>').value="";
            document.getElementById('AuthorList').value="";
            return false;
        }
    
        var strDel=opener.document.getElementById('<%= AuthorInput %>').value;
        var s=strDel.split("|");
        opener.document.getElementById('<%= AuthorInput %>').value = strDel.substring(0,strDel.length-s[s.length-1].length-1);
        document.getElementById('AuthorList').value = opener.document.getElementById('<%= AuthorInput %>').value;
    }
    
    function checkAuthor(Authorlist,thisAuthor)
    {
      if (Authorlist==thisAuthor){
            return true;
      }
      else{
        var s=Authorlist.split("|");
        for (j=0;j<s.length;j++){
            if(s[j]==thisAuthor)
                return true;
        }
        return false;
      }
    }
            </script>

        </div>
