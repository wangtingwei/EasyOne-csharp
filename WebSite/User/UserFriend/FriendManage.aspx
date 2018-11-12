<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.User.FriendManage" Codebehind="FriendManage.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>好友管理</title>
</head>
<body>
    <pe:UserNavigation ID="UserCenterNavigation" Tab="friend" runat="server" />
    <pe:ExtendedSiteMapPath ID="YourPosition" SiteMapProvider="UserMapProvider" runat="server" />
    <form id="form1" runat="server">
        <div>
            <asp:Repeater ID="RptFriendGroup" runat="server">
                <HeaderTemplate>
                    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
                        <tr class="title">
                            <td>
                    |&nbsp;
                </HeaderTemplate>
                <ItemTemplate>
                    <a href='<%#string.Format("FriendManage.aspx?GroupID={0}",Eval("FriendGroupID"))%>'>
                        <%#Request.QueryString["GroupID"]==Convert.ToString(Eval("FriendGroupID"))?"<font color=\"red\">" + Eval("FriendGroupName") + "</font>":Eval("FriendGroupName")%></a>&nbsp;|
                </ItemTemplate>
                <FooterTemplate>
                    </td></tr></table>
                </FooterTemplate>
            </asp:Repeater>
            <br />
            <pe:ExtendedGridView ID="EgvFriend" AutoGenerateCheckBoxColumn="true" DataKeyNames="ID"
                OnRowCommand="EgvFriend_RowCommand" DataSourceID="OdsFriend" AutoGenerateColumns="false"
                AllowPaging="true" ItemName="用户" ItemUnit="个" runat="server">
                <Columns>
                    <pe:BoundField DataField="FriendName" HeaderText="姓名" HeaderStyle-Width="13%">
                    </pe:BoundField>
                    <pe:TemplateField HeaderText="组别" HeaderStyle-Width="10%">
                        <ItemTemplate>
                            <%#GetGroupName(Convert.ToInt32(Eval("GroupID"))) %>
                        </ItemTemplate>
                    </pe:TemplateField>
                    <pe:BoundField HeaderText="邮件地址" HeaderStyle-Width="20%" DataField="Email">
                    </pe:BoundField>
                    <pe:BoundField HeaderText="主页" DataField="Homepage">
                    </pe:BoundField>
                    <pe:BoundField HeaderText="QQ" HeaderStyle-Width="12%" DataField="QQ">
                    </pe:BoundField>
                    <pe:TemplateField HeaderText="操作" HeaderStyle-Width="12%">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" OnClientClick="return confirm('确定删除该用户吗？');" CommandName="Del"
                                CommandArgument='<%#Eval("ID")%>'>删除</asp:LinkButton>
                            <asp:HyperLink ID="LnkSend" runat="server" Visible='<%# Convert.ToInt32(Eval("GroupID"))!=0 %>'
                                NavigateUrl='<%#string.Format("../Message/Message.aspx?inceptUser={0}",Eval("FriendName"))%>'>发短消息</asp:HyperLink>
                        </ItemTemplate>
                    </pe:TemplateField>
                </Columns>
            </pe:ExtendedGridView>
            <br />
            <asp:Button ID="BtnDelete" runat="server" Text="删除选定的用户" OnClientClick="return confirm('确定删除选定的用户吗？');"
                OnClick="BtnDelete_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="BtnMoveFriend" runat="server" Text="将选定的用户移动到->" OnClick="BtnMoveFriend_Click" />
            <asp:DropDownList ID="DropFriendGroup" DataTextField="FriendGroupName" AutoPostBack="false"
                DataValueField="FriendGroupID" runat="server">
            </asp:DropDownList>
            <asp:ObjectDataSource ID="OdsFriend" runat="server" SelectCountMethod="GetTotalOfFriend"
                SelectMethod="GetList" TypeName="EasyOne.UserManage.UserFriend" StartRowIndexParameterName="startRowIndexId"
                MaximumRowsParameterName="maxNumberRows" EnablePaging="True">
                <SelectParameters>
                    <asp:ControlParameter ControlID="HdnUserName" Name="userName" Type="string" />
                    <asp:QueryStringParameter Name="groupId" Type="Int32" QueryStringField="GroupID"
                        DefaultValue="-1" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:HiddenField ID="HdnUserName" runat="server" />
        </div>
    </form>
</body>
</html>
