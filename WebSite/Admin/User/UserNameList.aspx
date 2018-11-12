<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.User.UserNameList" Codebehind="UserNameList.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>选择对话框</title>
    <style type="text/css">
        li a:link { background-color:#eee}
        li a:hover{ background-color:#EEE; color:fff}
    </style>
    <base target="_self" />
</head>
<body>
    <form id="myform" runat="server">
        <div>
            <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
                <tr align="center">
                    <td class="title" align="left">
                        <b>已选定的用户名：</b></td>
                </tr>
                <tr class="tdbg">
                    <td colspan="2" class="tdbg">
                        <asp:TextBox ID="TxtUserName" runat="server" Width="420px"></asp:TextBox><br />
                        &nbsp;<input type="button" name="BtnCutLast" id="BtnCutLast" onclick="cutLastString()"
                            value="删除最后" />
                        <input type="button" name="BtnCutAll" id="BtnCutAll" onclick="Javascript:myform.TxtUserName.value=''"
                            value="删除全部" />
                        <input type="button" name="BtnReturn" id="BtnReturn" value=" 确 定 " onclick="ReturnValue()" />
                    </td>
                </tr>
            </table>
        </div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
                    <tr align="center">
                        <td class="title" align="left" style="height: 23px; vertical-align: middle;">
                            <asp:DataList ID="DlstGroup" runat="server" DataSourceID="OdsGroup" RepeatDirection="Horizontal"
                                RepeatLayout="Flow" Width="100%" OnItemCommand="DlstGroup_ItemCommand">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LbtnGroup" runat="server" Text='  <%# string.Format("|{0}",Eval("GroupName"))  %>'
                                        CommandArgument='<%# Eval("GroupId") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:DataList>
                        </td>
                    </tr>
                </table>
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                    <tr>
                        <td style="width: 50%; height: 23px;">
                            会员列表：</td>
                        <td style="width: 50%; text-align: right; height: 23px;">
                            <asp:TextBox ID="TxtSearchUser" runat="server"></asp:TextBox><asp:Button ID="BtnSearch"
                                runat="server" Text="查找" OnClick="BtnSearch_Click" /></td>
                    </tr>
                </table>
                <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
                    <tr align="center">
                        <td class="title" align="left">
                        </td>
                    </tr>
                </table>
                <div style="width: 99%; height: 140px;" class="border tdbg">
                    <asp:PlaceHolder ID="PlhUserList" runat="server"></asp:PlaceHolder>
                </div>
                <div style="width: 100%; text-align: center">
                    当前页：<asp:Label ID="LblCurrenPage" runat="server"></asp:Label>
                    &nbsp; 共&nbsp;<asp:Label ID="LblMaxPage" runat="server"></asp:Label>
                    页 &nbsp;&nbsp;
                    <asp:LinkButton ID="LbtnFirst" runat="server" OnClick="LbtnFirst_Click">首页</asp:LinkButton>&nbsp;
                    <asp:LinkButton ID="LbtnPrevious" runat="server" OnClick="LbtnPrevious_Click">上一页</asp:LinkButton>&nbsp;
                    <asp:LinkButton ID="LbtnNext" runat="server" OnClick="LbtnNext_Click">下一页</asp:LinkButton>&nbsp;
                    <asp:LinkButton ID="LbtnLast" runat="server" OnClick="LbtnLast_Click">尾页</asp:LinkButton>
                    &nbsp; &nbsp;
                    <asp:TextBox ID="TxtUserNameNum" runat="server" Width="22px" AutoPostBack="True">50</asp:TextBox>个用户名/页
                    &nbsp;&nbsp;转到第<asp:TextBox ID="TxtPage" runat="server" Width="20px" OnTextChanged="TxtPage_TextChanged"></asp:TextBox>页
                </div>
                <asp:ObjectDataSource ID="OdsGroup" runat="server" SelectMethod="GetUserGroupList"
                    TypeName="EasyOne.UserManage.UserGroups">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="0" Name="startRowIndexId" Type="Int32" />
                        <asp:Parameter DefaultValue="1000" Name="maxNumberRows" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:HiddenField ID="HdnSearchType" runat="server" />
                <asp:HiddenField ID="HiddenField2" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
        <div>
            &nbsp;</div>
        <br />
        &nbsp;&nbsp;
    </form>
</body>
</html>

<script language="javascript" type="text/javascript">
    document.myform.TxtUserName.value = <%="'"+ Request.QueryString["Default"] +"'"%>; 
    var oldUser='';
    function cutLastString()
    {
        var obj = document.getElementById('<%=TxtUserName.ClientID%>');
        obj.value =obj.value.substring(0,obj.value.lastIndexOf(","));
    }
    
    function add(obj,select)
    {
        if(obj==''){return false;}
        var txtUserName = document.getElementById('<%=TxtUserName.ClientID%>');
        if(txtUserName.value=='')
        {
            txtUserName.value=obj;
            window.returnValue=txtUserName.value;
            return false;
        }
        if(select==1)
        {
            var singleUser=obj.split(',');
            var ignoreUser='';
            for(i=0;i<singleUser.length;i++)
            {
                if(checkUser(txtUserName.value,singleUser[i]))
                {
                    ignoreUser=ignoreUser+singleUser[i]+" "
                }
                else
                {
                    txtUserName.value=txtUserName.value+','+singleUser[i];
                }
            }
            if(ignoreUser!='')
            {
                alert(ignoreUser+'已经存在，此操作已经忽略！');
            }
        }
        else
        {
            txtUserName.value=obj;
        }

        window.returnValue=txtUserName.value;
    }
    
  function checkUser(TxtUserName,thisUser)
    {
      if (TxtUserName==thisUser)
      {
            return true;
      }
      else
      {
        var s=TxtUserName.split(',');
        for (j=0;j<s.length;j++)
        {
            if(s[j]==thisUser)
            return true;
        }
        return false;
      }
    }
    
    function ReturnValue()
    {
        if(window.opener)
        {
            window.opener.DoPostBack(document.getElementById('<%=TxtUserName.ClientID%>').value);
            window.opener.focus();            
            window.close();
        }
        else
        {
            window.returnValue=document.getElementById('<%=TxtUserName.ClientID%>').value;window.close();
        }
    }
</script>

