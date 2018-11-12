<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Analytics.ShowOnline" Codebehind="ShowOnline.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <pe:ExtendedGridView ID="EgvOnLine" runat="server" AutoGenerateColumns="False" CheckBoxFieldHeaderWidth="3%"
                DataSourceID="ObjOnline" SerialText="" OnRowDataBound="EgvOnLine_RowDataBound"
                AllowPaging="True" ItemName="在线用户" ItemUnit="个" Width="100%" IsHoldState="True">
                <Columns>
                    <asp:TemplateField HeaderText="编号">
                    </asp:TemplateField>
                    <asp:BoundField DataField="UserIP" HeaderText="访问者IP" SortExpression="UserIP" />
                    <asp:BoundField DataField="OnTime" HeaderText="上站时间" SortExpression="OnTime" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"
                        HtmlEncode="False" />
                    <asp:BoundField DataField="LastTime" HeaderText="最后刷新时间" SortExpression="LastTime"
                        DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HtmlEncode="False" />
                    <asp:TemplateField HeaderText="已停留时间">
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="用户当前浏览页">
                        <itemtemplate>
<asp:HyperLink runat="server" NavigateUrl='<%# Eval("UserPage") %>' Target="_blank" id="HlnkUrl"></asp:HyperLink>
</itemtemplate>
                    </asp:TemplateField>
                </Columns>
            </pe:ExtendedGridView>
        </div>
        <asp:ObjectDataSource ID="ObjOnline" runat="server" EnablePaging="True" SelectMethod="GetStatOnlineList"
            TypeName="EasyOne.Analytics.OtherReport" MaximumRowsParameterName="maxiNumRows"
            SelectCountMethod="GetTotalStatOnline" StartRowIndexParameterName="startRowIndexId">
            <SelectParameters>
                <asp:Parameter Name="startRowIndexId" Type="Int32" />
                <asp:Parameter Name="maxiNumRows" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        注意：<br />
        (1)此功能只有在启动了“在线人数统计”功能，才能正确显示在线用户信息。<br />
        (2)在超过指定的时间内，用户长时间无任何活动的，作离线处理。
    </form>
</body>
</html>
