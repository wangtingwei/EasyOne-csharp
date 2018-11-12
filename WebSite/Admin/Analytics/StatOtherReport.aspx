<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Analytics.StatOtherReport" Title="网站统计管理" Codebehind="StatOtherReport.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td style="width: 80%">
                <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
            </td>
            <td style="width: 20%; text-align: right">
                有效统计：<asp:Label ID="LblCount" runat="server" ForeColor="Red"></asp:Label>&nbsp;</td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <asp:PlaceHolder ID="PlhStat" runat="server" EnableViewState="False"></asp:PlaceHolder>
    <pe:ExtendedGridView ID="ExtendedGridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False">
        <Columns>
            <pe:BoundField DataField="Key" HeaderText="Key">
                <ItemStyle HorizontalAlign="Left" Width="30%" />
                <HeaderStyle HorizontalAlign="Left" Width="30%" />
            </pe:BoundField>
            <pe:BoundField DataField="Value" HeaderText="访问人数">
                <ItemStyle HorizontalAlign="Left" Width="20%" />
                <HeaderStyle HorizontalAlign="Left" Width="20%" />
            </pe:BoundField>
            <pe:TemplateField HeaderText="百分比">
                <ItemStyle HorizontalAlign="Left" Width="20%" />
                <HeaderStyle HorizontalAlign="Left" Width="20%" />
                <ItemTemplate>
                    <%# string.Format("{0:p}",Convert.ToSingle(Eval("Value"))/itemSum) %>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="图示">
                <ItemStyle HorizontalAlign="Left" Width="30%" />
                <HeaderStyle HorizontalAlign="Left" Width="30%" />
                <ItemTemplate>
                    <%# string.Format("<div class='StatBar' style='width:{0}%'>", (Convert.ToSingle(Eval("Value")) / itemSum) * BarWidth)%>
                </ItemTemplate>
            </pe:TemplateField>
        </Columns>
    </pe:ExtendedGridView>
    <asp:ObjectDataSource ID="OdsStat" runat="server" EnablePaging="True" MaximumRowsParameterName="maxiNumRows"
        StartRowIndexParameterName="startRowIndexId" TypeName="EasyOne.Analytics.OtherReport">
    </asp:ObjectDataSource>
</asp:Content>
