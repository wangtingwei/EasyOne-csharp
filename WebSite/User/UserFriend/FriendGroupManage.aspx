<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.User.FriendGroupManage" Codebehind="FriendGroupManage.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>好友组管理</title>
</head>
<body>
    <pe:UserNavigation ID="UserCenterNavigation" Tab="friend" runat="server" />
    <pe:ExtendedSiteMapPath ID="YourPosition" SiteMapProvider="UserMapProvider" runat="server" />
    <form id="form1" runat="server">
        <div>
            <pe:ExtendedGridView ID="EgvFriendGroup" runat="server" SerialText="ID" DataKeyNames="FriendGroupID"
                OnRowCommand="EgvFriendGroup_RowCommand" AutoGenerateSerialColumn="true" AutoGenerateColumns="false"
                OnRowDataBound="EgvFriendGroup_RowDataBound">
                <Columns>
                    <pe:BoundField HeaderText="成员组名" DataField="FriendGroupName" HeaderStyle-Width="20%">
                    </pe:BoundField>
                    <pe:BoundField HeaderText="成员数量" HeaderStyle-Width="12%">
                    </pe:BoundField>
                    <pe:TemplateField HeaderText="操 作">
                        <ItemTemplate>
                            <asp:HyperLink Enabled='<%#Convert.ToInt32(Eval("FriendGroupID"))==0?false:true %>'
                                ID="HyperLink1" NavigateUrl='<%#string.Format("FriendGroup.aspx?Action=Modify&GroupID={0}&GroupName={1}",Eval("FriendGroupID"), Server.UrlEncode(Convert.ToString(Eval("FriendGroupName"))) )%>'
                                runat="server">修改</asp:HyperLink>
                            <asp:LinkButton ID="LinkButton1" OnClientClick="if(!disabled)return confirm('删除该分组后，该分组下面的好友也会删除，确定要删除吗');"
                                Enabled='<%#Convert.ToInt32(Eval("FriendGroupID"))==0||Convert.ToInt32(Eval("FriendGroupID"))==1?false:true %>'
                                CommandName="Del" CommandArgument='<%#Eval("FriendGroupID") %>' runat="server">删除</asp:LinkButton>
                            <asp:HyperLink ID="HyperLink2" NavigateUrl='<%#string.Format("FriendManage.aspx?GroupID={0}",Eval("FriendGroupID")) %>'
                                runat="server">列出成员名单</asp:HyperLink>
                        </ItemTemplate>
                    </pe:TemplateField>
                </Columns>
            </pe:ExtendedGridView>
            <br />
            注意： 默认组黑名单，拒收所有来自黑名单的短消息。
        </div>
    </form>
</body>
</html>
