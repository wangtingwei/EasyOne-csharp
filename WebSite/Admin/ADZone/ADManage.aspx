<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.AD.ADManage" Title="网站广告管理" Codebehind="ADManage.aspx.cs" %>

<%@ Import Namespace="EasyOne.AD" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <pe:ExtendedGridView ID="GdvAD" runat="server" AllowPaging="True" AutoGenerateColumns="False"
        DataSourceID="OdsAd" ItemName="广告" ItemUnit="个" AutoGenerateCheckBoxColumn="True"
        DataKeyNames="AdID" OnRowDataBound="GdvAD_RowDataBound"
        RowDblclickBoundField="ADId" 
        RowDblclickUrl="Advertisement.aspx?Action=Modify&amp;ADId={$Field}">
        <Columns>
            <pe:BoundField DataField="ADId" HeaderText="序号">
                <HeaderStyle Width="5%" />
            </pe:BoundField>
            <pe:TemplateField HeaderText="预览">
                <HeaderStyle Width="5%" />
                <ItemTemplate>
                    <asp:HyperLink ID="HypPreview" runat="server" Text="预览">
                    </asp:HyperLink>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="广告名称">
            <HeaderStyle />
                <ItemTemplate>
                    <a href="Advertisement.aspx?Action=Modify&ADId=<%#Eval("ADId")%>">
                        <%# DataBinder.Eval(Container.DataItem,"ADName")%>
                    </a>
                </ItemTemplate>
                 <ItemStyle HorizontalAlign="Left" />
            </pe:TemplateField>
            <pe:TemplateField HeaderText="类型">
                <HeaderStyle Width="5%" />
                <ItemTemplate>
                    <%#Advertisement.GetADType()[Convert.ToInt32(DataBinder.Eval(Container.DataItem, "ADType"))-1].ToString()%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:BoundField DataField="Priority" HeaderText="权重">
                <HeaderStyle Width="5%" />
            </pe:BoundField>
            <pe:TemplateField HeaderText="点击数">
                <HeaderStyle Width="6%" />
                <ItemTemplate>
                    <pe:ExtendedLabel HtmlEncode="false" runat="server" ID="LabClicks"></pe:ExtendedLabel>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="浏览数">
                <HeaderStyle Width="6%" />
                <ItemTemplate>
                    <pe:ExtendedLabel HtmlEncode="false" ID="LabViews" runat="server"></pe:ExtendedLabel>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="剩余天数">
                <HeaderStyle Width="8%" />
                <ItemTemplate>
                    <pe:ExtendedLabel HtmlEncode="false" ID="LabDays" runat="server"></pe:ExtendedLabel>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="点击率">
                <HeaderStyle Width="6%" />
                <ItemTemplate>
                    <asp:Label ID="LabRate" runat="server"></asp:Label>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="已审核">
                <HeaderStyle Width="6%" />
                <ItemTemplate>
                    <%# (bool)Eval("Passed") == false ? "<span style=\"color: #ff0033\">×</span>" : "√"%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="操作">
                <HeaderStyle Width="19%" />
                <ItemTemplate>
                    <a href='<%# "Advertisement.aspx?Action=Modify&ADId=" + Eval("ADId")%>'>修改</a> <a
                        href='<%# AppendSecurityCode("ADManage.aspx?Action=Copy&ADId=" + Eval("ADId"))%>'>
                        复制</a> <a href='<%# AppendSecurityCode("ADManage.aspx?Action=Delete&ADId=" + Eval("ADId"))%>'
                            onclick="return confirm('确定要删除此广告吗？');">删除</a> <a href="ADManage.aspx?Action=<%# (bool)Eval("Passed") == false ? "Passed" : "CancelPassed"%>&ADId=<%#Eval("ADId")%>">
                                <%# (bool)Eval("Passed") == false ? "通过审核" : "取消审核"%>
                            </a>
                </ItemTemplate>
            </pe:TemplateField>
        </Columns>
    </pe:ExtendedGridView>
    <br />
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td>
            </td>
            <td>
                <asp:Button ID="EBtnDelete" runat="server" Text="删除选定广告" OnClientClick="return confirm('确定要删除选中的广告吗？')"
                    OnClick="EBtnDelete_Click" />
                <asp:Button ID="EBtnPassed" runat="server" Text="审核通过选定广告" OnClick="EBtnPassed_Click" />
                <asp:Button ID="EBtnCancelPased" runat="server" Text="取消审核选定广告" OnClick="EBtnCancelPased_Click" />
            </td>
        </tr>
    </table>
    <br />
    <asp:ObjectDataSource ID="OdsAd" runat="server" SelectMethod="GetAdvertisementList"
        TypeName="EasyOne.AD.Advertisement" EnablePaging="True" SelectCountMethod="GetTotalOfAdvertisements">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="0" Name="zoneId" QueryStringField="ZoneId"
                Type="Int32" />
            <asp:QueryStringParameter DefaultValue="0" Name="listType" QueryStringField="ListType"
                Type="Int32" />
            <asp:QueryStringParameter DefaultValue="" Name="keyword" QueryStringField="Keyword"
                Type="string" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <br />
    <div id="dHTMLADPreview" style='z-index: 1000; left: 0px; visibility: hidden; width: 10px;
        position: absolute; top: 0px; height: 10px'>
    </div>

    <script src="<%=BasePath %>Admin/JS/Popup.js" language="javascript" type="text/javascript"></script>

</asp:Content>
