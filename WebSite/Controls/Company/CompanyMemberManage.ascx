<%@ Control Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Controls.CompanyMemberManage" Codebehind="CompanyMemberManage.ascx.cs" %>
<pe:ExtendedGridView ID="EgvCompanyMember" Width="100%" AutoGenerateColumns="false" runat="server" OnRowCommand="EgvCompanyMember_RowCommand" OnRowDataBound="EgvCompanyMember_RowDataBound">
    <Columns>
         <pe:BoundField DataField="UserName" HeaderText="��Ա��" HeaderStyle-Width="10%"></pe:BoundField>
         <pe:BoundField DataField="TrueName" HeaderText="��ʵ����" HeaderStyle-Width="10%"></pe:BoundField>
         <pe:BoundField DataField="ZipCode" HeaderText="��������" HeaderStyle-Width="10%"></pe:BoundField>
         <pe:BoundField DataField="Address" HeaderText="��ϵ��ַ"></pe:BoundField>
         <pe:TemplateField HeaderText="״̬" HeaderStyle-Width="12%">
            <ItemTemplate>
                <%#GetUserTypeText(Convert.ToInt32(Eval("UserType")))%>
            </ItemTemplate>
         </pe:TemplateField>
         <pe:TemplateField HeaderStyle-Width="25%" HeaderText="����">
            <ItemTemplate>
                <asp:LinkButton ID="LbtnRemoveFromCompany" Visible="false" OnClientClick="javascript:return confirm('ȷ�Ͻ����û�����ҵ��ɾ����');"  runat="server" CommandArgument='<%#Eval("UserName") %>' CommandName="RemoveFromCompany">����ҵ��ɾ��</asp:LinkButton>
                <asp:LinkButton ID="LbtnAddToAdmin" Visible="false" runat="server" OnClientClick="javascript:return confirm('ȷ�Ͻ����û�����Ϊ����Ա��');" CommandArgument='<%#Eval("UserName") %>' CommandName="AddToAdmin">����Ϊ����Ա</asp:LinkButton>
                <asp:LinkButton ID="LbtnRemoveFromAdmin" Visible="false" runat="server" OnClientClick="javascript:return confirm('ȷ�Ͻ����û���Ϊ��ͨ��Ա��');" CommandArgument='<%#Eval("UserName") %>' CommandName="RemoveFromAdmin">��Ϊ��ͨ��Ա</asp:LinkButton>
                <asp:LinkButton ID="LbtnAgree" Visible="false" runat="server" OnClientClick="javascript:return confirm('ȷ����׼���û�������ҵ�У�');" CommandArgument='<%#Eval("UserName") %>' CommandName="Agree">��׼����</asp:LinkButton>
                <asp:LinkButton ID="LbtnReject" Visible="false" runat="server" OnClientClick="javascript:return confirm('ȷ�Ͼܾ����û�������ҵ�У�');" CommandArgument='<%#Eval("UserName") %>' CommandName="Reject">�ܾ�����</asp:LinkButton>
            </ItemTemplate>
         </pe:TemplateField>
    </Columns> 
</pe:ExtendedGridView>
<asp:HiddenField ID="HdnCompanyClientId" runat="server" />
<asp:HiddenField ID="HdnReturnUrl" runat="server" />
