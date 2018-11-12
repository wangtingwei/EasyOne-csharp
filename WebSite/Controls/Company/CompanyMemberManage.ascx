<%@ Control Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Controls.CompanyMemberManage" Codebehind="CompanyMemberManage.ascx.cs" %>
<pe:ExtendedGridView ID="EgvCompanyMember" Width="100%" AutoGenerateColumns="false" runat="server" OnRowCommand="EgvCompanyMember_RowCommand" OnRowDataBound="EgvCompanyMember_RowDataBound">
    <Columns>
         <pe:BoundField DataField="UserName" HeaderText="会员名" HeaderStyle-Width="10%"></pe:BoundField>
         <pe:BoundField DataField="TrueName" HeaderText="真实姓名" HeaderStyle-Width="10%"></pe:BoundField>
         <pe:BoundField DataField="ZipCode" HeaderText="邮政编码" HeaderStyle-Width="10%"></pe:BoundField>
         <pe:BoundField DataField="Address" HeaderText="联系地址"></pe:BoundField>
         <pe:TemplateField HeaderText="状态" HeaderStyle-Width="12%">
            <ItemTemplate>
                <%#GetUserTypeText(Convert.ToInt32(Eval("UserType")))%>
            </ItemTemplate>
         </pe:TemplateField>
         <pe:TemplateField HeaderStyle-Width="25%" HeaderText="操作">
            <ItemTemplate>
                <asp:LinkButton ID="LbtnRemoveFromCompany" Visible="false" OnClientClick="javascript:return confirm('确认将该用户从企业中删除？');"  runat="server" CommandArgument='<%#Eval("UserName") %>' CommandName="RemoveFromCompany">从企业中删除</asp:LinkButton>
                <asp:LinkButton ID="LbtnAddToAdmin" Visible="false" runat="server" OnClientClick="javascript:return confirm('确认将该用户升级为管理员？');" CommandArgument='<%#Eval("UserName") %>' CommandName="AddToAdmin">升级为管理员</asp:LinkButton>
                <asp:LinkButton ID="LbtnRemoveFromAdmin" Visible="false" runat="server" OnClientClick="javascript:return confirm('确认将该用户降为普通成员？');" CommandArgument='<%#Eval("UserName") %>' CommandName="RemoveFromAdmin">降为普通成员</asp:LinkButton>
                <asp:LinkButton ID="LbtnAgree" Visible="false" runat="server" OnClientClick="javascript:return confirm('确认批准该用户加入企业中？');" CommandArgument='<%#Eval("UserName") %>' CommandName="Agree">批准加入</asp:LinkButton>
                <asp:LinkButton ID="LbtnReject" Visible="false" runat="server" OnClientClick="javascript:return confirm('确认拒绝该用户加入企业中？');" CommandArgument='<%#Eval("UserName") %>' CommandName="Reject">拒绝加入</asp:LinkButton>
            </ItemTemplate>
         </pe:TemplateField>
    </Columns> 
</pe:ExtendedGridView>
<asp:HiddenField ID="HdnCompanyClientId" runat="server" />
<asp:HiddenField ID="HdnReturnUrl" runat="server" />
