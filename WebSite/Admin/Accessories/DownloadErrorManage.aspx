<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Accessories.DownloadErrorManage" Title="下载报错管理" Codebehind="DownloadErrorManage.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server"
        AdditionalNode="所有下载报错" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <pe:ExtendedGridView ID="EgvDownloadError" runat="server" AllowPaging="True" AutoGenerateColumns="False"
        AutoGenerateCheckBoxColumn="True" DataKeyNames="ErrorId" DataSourceID="OdsDownloadError"
        ItemName="地址" ItemUnit="个" OnRowCommand="EgvDownloadError_RowCommand">
        <Columns>
            <pe:BoundField DataField="InfoID" HeaderText="软件ID">
                <HeaderStyle Width="8%" />
            </pe:BoundField>
            <pe:TemplateField HeaderText="软件名" SortExpression="InfoID">
                <HeaderStyle Width="17%" />
                <ItemTemplate>
                    <%#GetSoftName(Convert.ToInt32(Eval("InfoID")))%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:BoundField DataField="ErrorUrl" HeaderText="下载地址">
            </pe:BoundField>
            <pe:BoundField DataField="ErrorTimes" HeaderText="报错人次">
                <HeaderStyle Width="10%" />
            </pe:BoundField>
            <pe:TemplateField HeaderText="操作">
                <HeaderStyle Width="15%" />
                <ItemTemplate>
                    <a id="EahDownloadErrorTest" href='<%#Eval("ErrorUrl")%>'
                        target="_blank" runat="server">测试</a>
                    <asp:LinkButton ID="ELbtnDel" Text="删除" runat="server" CommandArgument='<%# Bind("ErrorID") %>'
                        CausesValidation="False" CommandName="Delete" OnClientClick="return confirm('确定要删除吗？')" />
                </ItemTemplate>
            </pe:TemplateField>
        </Columns>
    </pe:ExtendedGridView>
    <br />
    <input id="ChkAll" onclick="javascript:CheckAll(this);" type="checkbox" />
    选中本页所有下载地址错误信息 &nbsp;&nbsp;&nbsp;&nbsp;
    <pe:ExtendedButton ID="EBtnDelete" Text="删除选中的下载地址错误信息" OnClick="EBtnDelete_Click" OnClientClick="return batchconfirm('确实要删除选中的下载地址错误信息？');"
        CausesValidation="False" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="EBtnClear" Text="删除所有下载地址错误信息" OnClick="EBtnClear_Click" OnClientClick="return confirm('确实要删除所有的下载地址错误信息？');"
        UseSubmitBehavior="True" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:ObjectDataSource ID="OdsDownloadError" runat="server" SelectCountMethod="GetTotalOfDownloadError"
        DeleteMethod="Delete" SelectMethod="GetDownloadErrorList" TypeName="EasyOne.Accessories.DownloadError"
        EnablePaging="True" StartRowIndexParameterName="startRowIndexId" MaximumRowsParameterName="maxiNumRows">
        <DeleteParameters>
            <asp:Parameter Name="errorId" Type="String" />
        </DeleteParameters>
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="" Name="searchType" QueryStringField="Field"
                Type="String" />
            <asp:QueryStringParameter DefaultValue="" Name="keyword" QueryStringField="KeyWord"
                Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
