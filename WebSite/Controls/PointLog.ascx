<%@ Control Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Controls.PointLog" Codebehind="PointLog.ascx.cs" %>
<pe:ExtendedGridView ID="EgvUserPoint" runat="server" AllowPaging="True" AutoGenerateColumns="False"
    DataKeyNames="LogId" DataSourceID="OdsUserPoint" ShowFooter="True" ItemName="记录"
    ItemUnit="条" OnRowDataBound="EgvUserPoint_RowDataBound" OnDataBound="EgvUserPoint_DataBound">
    <Columns>
        <pe:BoundField DataField="LogTime" HeaderText="消费时间" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"
            SortExpression="LogTime" HtmlEncode="False">
            <HeaderStyle Width="17%" />
        </pe:BoundField>
        <pe:TemplateField HeaderText="用户名" SortExpression="IncomePayOut">
            <HeaderStyle Width="10%" />
            <ItemTemplate>
                <asp:HyperLink runat="server" ID="HypUserName"></asp:HyperLink>
            </ItemTemplate>
        </pe:TemplateField>
        <pe:BoundField DataField="IP" HeaderText="IP地址" SortExpression="IP">
            <HeaderStyle Width="13%" />
        </pe:BoundField>
        <pe:TemplateField HeaderText="点券数" SortExpression="IncomePayOut">
            <HeaderStyle Width="6%" />
            <ItemTemplate>
                <pe:ExtendedLabel HtmlEncode="false" runat="server" ID="LblIncomePayOut">
                </pe:ExtendedLabel>
            </ItemTemplate>
        </pe:TemplateField>
        <pe:BoundField DataField="Times" HeaderText="重复次数" SortExpression="Times">
            <HeaderStyle Width="8%" />
        </pe:BoundField>
        <pe:BoundField DataField="Inputer" HeaderText="操作员" SortExpression="Inputer" >
            <HeaderStyle Width="10%" />
        </pe:BoundField>
        <pe:TemplateField HeaderText="备注/说明">
            <ItemStyle HorizontalAlign="Left" />
            <ItemTemplate>
                <%# EasyOne.Common.StringHelper.SubString(Eval("Remark").ToString(),30,"...") %>
            </ItemTemplate>
        </pe:TemplateField>
        <pe:TemplateField HeaderText="操作" HeaderStyle-Width="5%" Visible="true">
            <ItemTemplate>
                <a href='<%#string.Format("PointLogDetail.aspx?LogID={0}",Eval("LogId"))%>'>查看</a>
            </ItemTemplate>
        </pe:TemplateField>
    </Columns>
</pe:ExtendedGridView>
<asp:HiddenField ID="HdnSearchType" runat="server" />
<asp:HiddenField ID="HdnField" runat="server" />
<asp:HiddenField ID="HdnKeyword" runat="server" />
<asp:ObjectDataSource ID="OdsUserPoint" runat="server" SelectMethod="GetPointList"
    SelectCountMethod="GetNumberOfUsersOnline" TypeName="EasyOne.UserManage.UserPointLog"
    EnablePaging="True" StartRowIndexParameterName="startRowIndexId" MaximumRowsParameterName="maxNumberRows"
    OldValuesParameterFormatString="original_{0}">
    <SelectParameters>
        <asp:ControlParameter ControlID="HdnSearchType" DefaultValue="-1" Name="scopesType"
            Type="Int32" />
        <asp:ControlParameter ControlID="HdnField" DefaultValue="0" Name="field" Type="Int32" />
        <asp:ControlParameter ControlID="HdnKeyword" DefaultValue="" Name="keyword" Type="String" />
    </SelectParameters>
</asp:ObjectDataSource>
