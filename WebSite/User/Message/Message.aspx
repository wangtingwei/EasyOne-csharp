<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false"
    Inherits="EasyOne.WebSite.User.Message" Codebehind="Message.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>撰写短消息</title>
</head>
<body>
    <pe:UserNavigation ID="UserCenterNavigation" Tab="message" runat="server" />
    <pe:ExtendedSiteMapPath ID="YourPosition" SiteMapProvider="UserMapProvider" runat="server" />
    <form id="form1" runat="server">
        <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
            <tr align="center">
                <td colspan="2" class="spacingtitle">
                    <b>撰写短消息</b>
                </td>
            </tr>
            <tr class="tdbg">
                <td align="right" style="width: 25%">
                    收件人：
                </td>
                <td>
                    <asp:TextBox ID="TxtInceptUser" runat="server" Width="300" />
                    <asp:DropDownList ID="DropFriends" runat="server" />
                    <pe:RequiredFieldValidator ID="ValrInceptUser" runat="server" ControlToValidate="TxtInceptUser"
                        ErrorMessage="收件人不能为空" Display="Dynamic" />
                </td>
            </tr>
            <tr class="tdbg">
                <td align="right">
                    短消息主题：
                </td>
                <td>
                    <asp:TextBox ID="TxtTitle" runat="server" Width="575" MaxLength="50" />
                    <pe:RequiredFieldValidator ID="ValrTitle" runat="server" ControlToValidate="TxtTitle"
                        ErrorMessage="短消息主题不能为空" Display="Dynamic" />
                </td>
            </tr>
            <tr class="tdbg">
                <td align="right">
                    短消息内容：
                </td>
                <td>
                    <pe:PEeditor ID="EditorContent" runat="server" Width="580px" Height="300px" ToolbarSet="Basic" />
                    <pe:FckEditorValidator ID="ValrContent" runat="server" ControlToValidate="EditorContent"
                        ErrorMessage="短消息内容不能为空" Display="Dynamic" />
                </td>
            </tr>
            <tr class="tdbgbottom">
                <td colspan="2">
                    <asp:HiddenField ID="HdnMessageID" runat="server" />
                    <asp:Button ID="BtnSend" runat="server" Text="发送" CommandName="Send" OnClick="BtnSend_Click" />
                    <asp:Button ID="BtnSave" runat="server" Text="保存" CommandName="Save" OnClick="BtnSave_Click" />
                    <asp:Button ID="BtnReset" runat="server" Text="清除" OnClick="BtnReset_Click" />
                </td>
            </tr>
            <tr align="center" class="tdbg">
                <td colspan="2" style="text-align: left">
                    1、可以用英文状态下的逗号将用户名隔开实现群发，最多<b><asp:Label ID="LblMaxSendNum" runat="server" Text="" /></b>个用户。<br />
                    2、标题最多<b><%=TxtTitle.MaxLength.ToString()%></b>个字符，内容最多<b><%=MaxContentLength%></b>个字符</td>
            </tr>
        </table>

        <script type="text/javascript">
        
            var txtInceptUser = document.getElementById('<%=TxtInceptUser.ClientID %>');
            var dropFriends = document.getElementById('<%=DropFriends.ClientID %>');        
            function SelectFromFriend()
            {

                if(dropFriends.selectedIndex==0)
                {
                    return false;
                }
                var selectedFriend = dropFriends.value;
                
                if(txtInceptUser.value !='')
                {
                    var users = txtInceptUser.value.split(',');
                    for(var i=0;i<users.length;i++)
                    {
                        if(users[i]==selectedFriend)
                        {
                            return false;
                        }
                    }
                    txtInceptUser.value +=","+selectedFriend;
                }
                else
                {
                    txtInceptUser.value =selectedFriend;
                }
            }
        </script>

    </form>
</body>
</html>
