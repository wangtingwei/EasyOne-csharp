<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Install.Upgrade" Codebehind="Upgrade.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>���������Ʒ��������</title>
    <link rel="stylesheet" type="text/css" href="images/upgrade.css" />
</head>
<body>
    <div class="top">
        <form id="search" action="http://help.EasyOne.net/search.asp" method="post">
        <dl>
            <dt class="linking"><a href="http://www.EasyOne.net/" target="_blank" title="���ʶ��׹ٷ���վ">
                EasyOne.net</a> | <a href="http://EasyOne.net/soft/" target="_blank" title="������ض���ϵ�������Ʒ">
                    �������</a> | <a href="http://EasyOne.net/User/" target="_blank" title="���׹ٷ���վ�ͻ���������">
                        �ͻ���������</a> | <a href="http://bbs.EasyOne.net/" target="_blank" title="�������϶�����̳����">
                            ������̳</a></dt>
            <dt class="search"><span style="width: 320px; height: 22px; height: 26px; padding: 6px 0 0 0;
                padding: 2px 0 0 0; _padding: 4px 0 0 0; overflow: hidden; float: right;"><span style="float: right;
                    padding: 0px 0 0 10px; padding: 2px 0 0 10px;">
                    <input id="Submit" style="border: 0px; width: 47px; height: 20px;" type="image" src="Images/upgrade_search_but.gif"
                        name="Submit" />
                </span><span>
                    <input name="Keyword" id="Keyword" onclick="value=''" style="background: #ebf7ff"
                        onmouseover="this.style.backgroundColor='#ffffff'" onmouseout="this.style.backgroundColor='#ebf7ff'"
                        value="����ȫվ����" size="35" />
                    <input name="ModuleName" type="hidden" id="ModuleName" value="Article" />
                    <input id="Field" type="hidden" value="Title" name="Field" />
                </span></span></dt>
        </dl>
        </form>
    </div>
    <br />
    <h1>
        ׼������
        <%= ProductName %>
    </h1>
    <h2>
        ��ǰ���򼯰汾��<%= ProductVersion %></h2>
    <h2>
        ��ǰ���ݿ�汾��<%= dataBaseVersion %></h2>
    <h2 style="color:Red">
        ��ʼ����ǰ���ȱ������ݿ�����վ����ļ���</h2>
    <form id="form1" runat="server">
    <h3>
        ��ѡ�����ݿ�汾��<asp:DropDownList ID="DropSqlVersion" runat="server">
            <asp:ListItem Selected="True" Value="2000">Sql Server 2000</asp:ListItem>
            <asp:ListItem  Value="2005">Sql Server 2005</asp:ListItem>
        </asp:DropDownList><span style="color:Red">����ȷѡ�����ݿ�İ汾</span>
    </h3>
    <h2>
        <input id="BtnUpgrade" type="button" class="button_link" runat="server" onmouseover="this.className='button_over'"
            onmouseout="this.className='button_link'" value="��ʼ����" onserverclick="BtnUpgrade_Click" /></h2>
    </form>
</body>
</html>
