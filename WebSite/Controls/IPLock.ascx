<%@ Control Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Controls.IPLock1" Codebehind="IPLock.ascx.cs" %>
<%@ Register Assembly="System.Web.Extensions" Namespace="System.Web.UI" TagPrefix="asp" %>
<div style="width: 420px">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <pe:ExtendedGridView ID="EgvIPLock" runat="server" Width="100%" SerialText="" OnRowEditing="EgvIPLock_RowEditing"
                OnRowDeleting="EgvIPLock_RowDeleting" CheckBoxFieldHeaderWidth="3%" AutoGenerateColumns="False">
                <Columns>
                    <pe:BoundField DataField="IPFrom" HeaderText="起始IP">
                        <HeaderStyle Width="40%" />
                    </pe:BoundField>
                    <pe:BoundField DataField="IPTo" HeaderText="结尾IP">
                        <HeaderStyle Width="40%" />
                    </pe:BoundField>
                    <pe:TemplateField HeaderText="操 作">
                        <HeaderStyle Width="20%" />
                        <ItemTemplate>
                            <asp:LinkButton ID="LbtnEdit" runat="server" CommandName="Edit" CausesValidation="False">编辑</asp:LinkButton>&nbsp;
                            <asp:LinkButton ID="LbtnDelete" runat="server" CommandName="Delete" CausesValidation="False">删除</asp:LinkButton>
                        </ItemTemplate>
                    </pe:TemplateField>
                </Columns>
            </pe:ExtendedGridView>
            <div style="text-align: center">
                起始IP：<asp:TextBox ID="TxtFrom" runat="server" Width="100px"></asp:TextBox>
                &nbsp;&nbsp; 结尾IP：<asp:TextBox ID="TxtTo" runat="server" Width="100px"></asp:TextBox>
                <asp:Button ID="BtnSave" OnClick="BtnSave_Click" runat="server" Text="添加"></asp:Button><br />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div style="text-align: center">
        <asp:RegularExpressionValidator ID="ValeIPFrom" runat="server" ControlToValidate="TxtFrom"
            Display="Dynamic" ErrorMessage="起始IP不是有效的IP地址！" SetFocusOnError="True" ValidationExpression="^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$"></asp:RegularExpressionValidator>
        <pe:RequiredFieldValidator ID="ValrIPFrom" runat="server" ControlToValidate="TxtFrom"
            Display="Dynamic" ErrorMessage="起始IP不能为空！" SetFocusOnError="True" ShowRequiredText="False"></pe:RequiredFieldValidator>
        <pe:RequiredFieldValidator ID="ValrIPTo" runat="server" ControlToValidate="TxtTo"
            Display="Dynamic" ErrorMessage="结尾IP不能为空！" SetFocusOnError="True" ShowRequiredText="False"></pe:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="ValeIPTo" runat="server" ControlToValidate="TxtTo"
            Display="Dynamic" ErrorMessage="结尾IP不是有效的IP地址！" SetFocusOnError="True" ValidationExpression="^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$"></asp:RegularExpressionValidator></div>
</div>
