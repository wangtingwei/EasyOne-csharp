<%@ Control Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Controls.ValidLog" Codebehind="ValidLog.ascx.cs" %>
<pe:ExtendedGridView ID="EgvUserValid" runat="server" AllowPaging="True" AutoGenerateColumns="False"
    DataKeyNames="LogId" DataSourceID="OdsUserValid" ItemName="记录" ItemUnit="条" OnRowDataBound="EgvUserValid_RowDataBound">
    <Columns>
        <pe:BoundField DataField="LogTime" HeaderText="时间" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"
            SortExpression="LogTime" HtmlEncode="False">
            <HeaderStyle Width="17%" />
        </pe:BoundField>
        <pe:TemplateField HeaderText="用户名">
            <HeaderStyle Width="10%" />
            <ItemTemplate>
                <asp:HyperLink runat="server" ID="HypUserName"></asp:HyperLink>
            </ItemTemplate>
        </pe:TemplateField>
        <pe:BoundField DataField="IP" HeaderText="IP地址" SortExpression="IP">
            <HeaderStyle Width="13%" />
        </pe:BoundField>
        <pe:TemplateField HeaderText="有效期" SortExpression="IncomePayout">
            <HeaderStyle Width="8%" />
            <ItemTemplate>
                <pe:ExtendedLabel HtmlEncode="false" runat="server" Text='<%#GetIncomePayout(Convert.ToInt32(DataBinder.Eval(Container.DataItem,"IncomePayout")),Convert.ToInt32(DataBinder.Eval(Container.DataItem,"ValidNum")))%>'
                    ID="LblIncomePayOut">
                </pe:ExtendedLabel>
            </ItemTemplate>
        </pe:TemplateField>
        <pe:BoundField DataField="Inputer" HeaderText="操作员" SortExpression="Inputer" >
            <HeaderStyle Width="10%" />
        </pe:BoundField>
        <pe:TemplateField HeaderText="备注/说明">
            <ItemStyle HorizontalAlign="Left" />
            <ItemTemplate>
                <%# EasyOne.Common.StringHelper.SubString(Eval("Remark").ToString(),43,"...") %>
            </ItemTemplate>
        </pe:TemplateField>
        <pe:TemplateField HeaderText="操作" HeaderStyle-Width="5%">
            <ItemTemplate>
                <a href='<%#string.Format("ValidLogDetail.aspx?LogID={0}",Eval("LogId"))%>'>查看</a>
            </ItemTemplate>
        </pe:TemplateField>
    </Columns>
</pe:ExtendedGridView>
<asp:HiddenField ID="HdnSearchType" runat="server" />
<asp:HiddenField ID="HdnField" runat="server" />
<asp:HiddenField ID="HdnKeyword" runat="server" />
<asp:ObjectDataSource ID="OdsUserValid" runat="server" SelectMethod="GetValidList"
    SelectCountMethod="GetNumberOfUsersOnline" TypeName="EasyOne.UserManage.UserValidLog"
    EnablePaging="True" StartRowIndexParameterName="startRowIndexId" MaximumRowsParameterName="maxNumberRows">
    <SelectParameters>
        <asp:ControlParameter ControlID="HdnSearchType" DefaultValue="-1" Name="scopesType"
            Type="Int32" />
        <asp:ControlParameter ControlID="HdnField" DefaultValue="0" Name="field" Type="Int32" />
        <asp:ControlParameter ControlID="HdnKeyword" DefaultValue="" Name="keyword" Type="String" />
    </SelectParameters>
</asp:ObjectDataSource>
