<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.User.Contents.SignIn" Codebehind="Signin.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>我签收的文章</title>
</head>
<body>
    <pe:UserNavigation ID="UserCenterNavigation" Tab="user" runat="server" />
    <pe:ExtendedSiteMapPath ID="YourPosition" SiteMapProvider="UserMapProvider" runat="server" />
    <form id="form1" runat="server">
        <div>
            <pe:ExtendedGridView ID="EgvContentSignIn" runat="server" DataSourceID="OdsContents"
                SerialText="" AutoGenerateCheckBoxColumn="True" AutoGenerateColumns="False" AllowPaging="True"
                OnRowDataBound="EgvContentSignIn_RowDataBound" OnRowCommand="EgvContentSignIn_RowCommand"
                DataKeyNames="GeneralId" CheckBoxFieldHeaderWidth="3%">
                <Columns>
                    <pe:BoundField DataField="GeneralId" HeaderText="ID" SortExpression="GeneralId">
                        <HeaderStyle Width="5%" />
                    </pe:BoundField>
                    <pe:TemplateField HeaderText="标题" SortExpression="Title">
                        <HeaderStyle Width="30%" />
                        <ItemStyle HorizontalAlign="Left" />
                        <ItemTemplate>
                            <pe:LinkImage ID="LinkImageModel" runat="server">
                            <pe:ExtendedHyperLink ID="LnkNodeLink" runat="server" />
                            <asp:HyperLink ID="HypTitle" NavigateUrl='<%# FullBasePath %>Item/<%#Eval("GeneralId") %>.aspx' Text='<%# Eval("Title").ToString().Length <= 20 ? Eval("Title") : Eval("Title").ToString().Substring(0, 20) + ".."%>' runat="server" />
                            </pe:LinkImage>
                        </ItemTemplate>
                    </pe:TemplateField>
                    <pe:BoundField DataField="Inputer" HeaderText="录入者" SortExpression="Inputer">
                        <HeaderStyle Width="8%" />
                    </pe:BoundField>
                    <pe:BoundField DataField="Hits" HeaderText="点击数" SortExpression="Hits">
                        <HeaderStyle Width="8%" />
                    </pe:BoundField>
                    <pe:BoundField DataField="EliteLevel" HeaderText="推荐级别" SortExpression="EliteLevel">
                        <HeaderStyle Width="10%" />
                    </pe:BoundField>
                    <pe:BoundField DataField="Priority" HeaderText="优先级" SortExpression="Priority">
                        <HeaderStyle Width="8%" />
                    </pe:BoundField>
                    <pe:TemplateField HeaderText="签收状态" SortExpression="Status">
                        <HeaderStyle Width="8%" />
                        <ItemTemplate>
                            <pe:ExtendedLabel HtmlEncode="false" ID="LblSignInStatus" runat="Server"></pe:ExtendedLabel>
                        </ItemTemplate>
                    </pe:TemplateField>
                    <pe:TemplateField HeaderText="签收操作" SortExpression="Disabled">
                        <HeaderStyle Width="15%" />
                        <ItemTemplate>
                            <%--                            <pe:ExtendedNodeAnchor ID="EahContentView" IsChecked="true" NodeId='<%# RequestInt32("NodeID")%>'
                                OperateCode="NodeContentPreview" href='' runat="server">查看文件</pe:ExtendedNodeAnchor>--%>
                            <pe:ExtendedNodeLinkButton ID="ELbtnContentSignIn" Text="签收文件" IsChecked="false"
                                runat="server" CommandArgument='<%# Eval("GeneralId")%>' CommandName="SignIn" />
                        </ItemTemplate>
                    </pe:TemplateField>
                </Columns>
            </pe:ExtendedGridView>
            <asp:HiddenField ID="HdnListType" runat="server" Value="-1" />
            <asp:ObjectDataSource ID="OdsContents" runat="server" SelectMethod="GetCommonModelInfoListBySignInLog"
                SelectCountMethod="GetTotalOfCommonModelInfoBySignInLog" TypeName="EasyOne.Contents.ContentManage"
                EnablePaging="True" MaximumRowsParameterName="maxNumberRows" StartRowIndexParameterName="startRowIndexId">
                <SelectParameters>
                    <asp:Parameter Name="userName" Type="String" DefaultValue="" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <br />
            <input id="ChkAll" onclick="javascript:CheckAll(this);" type="checkbox" />
            <label for="ChkAll">
                选中本页显示的所有项目</label>
            &nbsp;&nbsp;
            <pe:ExtendedNodeButton ID="EBtnSignIn" Text="签收选定的项" IsChecked="true" NodeId='<%# RequestInt32("NodeID")%>'
                OperateCode="NodeContentPreview" runat="server" OnClick="EBtnSignIn_Click" />
        </div>
    </form>
</body>
</html>
