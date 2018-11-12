<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Accessories.PayPlatformManage" Codebehind="PayPlatformManage.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server"
        AdditionalNode="所有在线支付平台" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <pe:ExtendedGridView ID="EgvPayPlatform" runat="server" AutoGenerateColumns="False"
        DataSourceID="OdsPayPlatform" DataKeyNames="PayPlatformId" OnRowCommand="EgvPayPlatform_RowCommand"
        ItemName="平台" ItemUnit="个" OnRowDataBound="EgvPayPlatform_RowDataBound" CheckBoxFieldHeaderWidth="3%"
        RowDblclickBoundField="PayPlatformId" RowDblclickUrl="PayPlatform.aspx?Action=Modify&amp;ID={$Field}"
        SerialText="">
        <Columns>
            <pe:BoundField DataField="PayPlatformId" HeaderText="ID" SortExpression="PayPlatformId">
                <HeaderStyle Width="4%" />
            </pe:BoundField>
            <pe:BoundField DataField="PayPlatformName" HeaderText="名称" SortExpression="PayPlatformName">
            </pe:BoundField>
            <pe:BoundField DataField="AccountsId" HeaderText="商户ID" SortExpression="AccountsId">
                <HeaderStyle Width="20%" />
            </pe:BoundField>
            <pe:TemplateField SortExpression="Rate" HeaderText="手续费率">
                <HeaderStyle Width="8%" />
                <ItemTemplate>
                    <%# Eval("Rate") + "%"%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="是否默认" SortExpression="Disabled">
                <HeaderStyle Width="7%" />
                <ItemTemplate>
                    <%# (bool)Eval("IsDefault") == true ? "√":""%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="已启用" SortExpression="Disabled">
                <HeaderStyle Width="6%" />
                <ItemTemplate>
                    <%# (bool)Eval("IsDisabled") == true ? "<font color=red>×</font>" : "<font color=green>√</font>"%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="常规操作">
                <HeaderStyle Width="23%" />
                <ItemTemplate>
                    <asp:HyperLink ID="HlnkApply" runat="server" Target="_blank">申请商户</asp:HyperLink>
                    <asp:LinkButton ID="LbtnSetDefault" runat="server" CommandName='<%# (bool)Eval("IsDefault") == false ? "SetDefault" : ""%>'
                        CommandArgument='<%# Eval("PayPlatformID") %>'>默认</asp:LinkButton>
                    <asp:LinkButton ID="LbtnDisabled" runat="server" CommandName='<%# (bool)Eval("IsDisabled")? "Enabled" : "Disabled"%>'
                        CommandArgument='<%# Eval("PayPlatformID") %>'><%# (bool)Eval("IsDisabled") == true ? "启用" : "禁用"%></asp:LinkButton>
                    <a href='<%# Eval("PayPlatformID","PayPlatform.aspx?Action=Modify&ID={0}") %>'>修改</a>
                    <asp:LinkButton ID="LbtnDel" CommandName="Del" CommandArgument='<%# Eval("PayPlatformId") %>'
                        runat="server">删除</asp:LinkButton>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="排序操作" SortExpression="Disabled">
                <HeaderStyle Width="8%" />
                <ItemTemplate>
                    &nbsp;<asp:DropDownList ID="DropOrderId" runat="server">
                    </asp:DropDownList>
                </ItemTemplate>
            </pe:TemplateField>
        </Columns>
    </pe:ExtendedGridView>
    <br />
    <asp:ObjectDataSource ID="OdsPayPlatform" runat="server" SelectCountMethod="Count"
        SelectMethod="GetList" TypeName="EasyOne.Accessories.PayPlatform"></asp:ObjectDataSource>
    <div style="text-align: center;">
        <asp:Button ID="BtnSaveSort" runat="server" OnClick="BtnSaveSort_Click" Text="保存排序" />
    </div>
    <br />
    <table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="border">
        <tr>
            <td class="spacingtitle" colspan="2" align="center">
                <strong>相关说明</strong>
            </td>
        </tr>
        <tr class="tdbg">
            <td>
                <ajaxToolkit:TabContainer runat="server" ID="Tabs" Height="180px">
                    <ajaxToolkit:TabPanel runat="Server" ID="TabPanel" HeaderText="删除支付接口说明">
                        <ContentTemplate>
                            <p>
                                系统内置的支付接口不允许删除，如果不使用的话，可以设置为“禁用”。“禁用”状态的支付接口，前台不会显示。
                            </p>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel runat="Server" ID="TabPanel1" HeaderText="添加新支付接口说明">
                        <ContentTemplate>
                            <p>
                                在支付平台管理中添加了新的支付平台后，还需要自己添加支付接口程序。</p>
                            <p>
                                添加新支付接口步骤说明：
                                <li>添加平台：在支付平台管理中添加新的支付平台，注意添加后的平台ID，这个ID在程序中判断是哪个平台时会用到。</li>
                                <li>添加发送接口程序：发送接口程序在PayOnline.aspx（网站目录/PayOnline/PayOnline.aspx）文件中，根据在线支付商提供的接口开发文档在该文件中添加对应的接口程序。</li>
                                <li>添加接收接口程序：接收接口需要在PayOnline目录中添加一个新文件，在新文件中添加上接收程序，里面调用的方法可以参照系统内置支付平台的接收文件，如：快钱的接收文件（PayResult99bill.aspx）。</li>
                            </p>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                </ajaxToolkit:TabContainer>
            </td>
        </tr>
    </table>
</asp:Content>
