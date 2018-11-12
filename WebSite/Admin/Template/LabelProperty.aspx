<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Template.LabelProperty" Title="Untitled Page" Codebehind="LabelProperty.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <asp:ScriptManager ID="SmProperty" EnablePartialRendering="true" runat="server">
    </asp:ScriptManager>
    <table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="border">
        <tr>
            <td class="spacingtitle" colspan="2" align="center">
                <b>设置标签参数</b></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbg" align="left">
                <asp:UpdatePanel ID="UpPropertys" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="GdvPropertys" runat="server" DataSourceID="OdsPropertys" AutoGenerateColumns="False"
                            OnRowCommand="GdvPropertys_RowCommand" OnRowUpdating="GdvProperty_RowUpdating" Style="width: 100%;">
                            <Columns>
                                <pe:BoundField DataField="AttributeName" SortExpression="AttributeName" HeaderText="参数名称">
                                    <headerstyle horizontalalign="Center" width="30%" />
                                </pe:BoundField>
                                <pe:BoundField DataField="DefaultValue" SortExpression="DefaultValue" HeaderText="默认值">
                                    <headerstyle horizontalalign="Center" width="30%" />
                                </pe:BoundField>
                                <pe:BoundField DataField="Intro" SortExpression="Intro" HeaderText="参数说明" >
                                    <headerstyle horizontalalign="Center" width="30%" />
                                </pe:BoundField>
                                <pe:TemplateField HeaderText="操作">
                                    <headerstyle horizontalalign="Center" width="10%" />
                                    <itemtemplate>
                                        <asp:LinkButton ID="LbtnEdit" runat="server" CausesValidation="False" CommandName="Edit"
                                            Text="编辑"></asp:LinkButton>
                                        <asp:LinkButton ID="LbtnDelete" runat="server" CausesValidation="False" CommandName="Delete"
                                            CommandArgument='<%# Bind("AttributeName") %>'>删除</asp:LinkButton>
                                    </itemtemplate>
                                    <edititemtemplate>
                                        <asp:LinkButton ID="LbtnUpdate" runat="server" CausesValidation="False" CommandName="Update">修改</asp:LinkButton>
                                        <asp:LinkButton ID="LbtnCancel" runat="server" CausesValidation="False" CommandName="Cancel"
                                            Text="取消"></asp:LinkButton>
                                    </edititemtemplate>
                                </pe:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:ObjectDataSource ID="OdsPropertys" runat="server" TypeName="EasyOne.Templates.LabelManage"
                            SelectMethod="GetAttributeList" UpdateMethod="UpdateAttribute" DeleteMethod="DeleteAttribute">
                            <SelectParameters>
                                <asp:Parameter Name="xmlfilepath" Type="String" />
                            </SelectParameters>
                            <UpdateParameters>
                                <asp:Parameter Name="xmlfilepath" Type="String" />
                                <asp:Parameter Name="oldAttributeName" Type="String" />
                                <asp:Parameter Name="attributename" Type="String" />
                                <asp:Parameter Name="defaultvalue" Type="String" />
                                <asp:Parameter Name="intro" Type="String" />
                            </UpdateParameters>
                            <DeleteParameters>
                                <asp:Parameter Name="xmlfilepath" Type="String" />
                                <asp:Parameter Name="attributename" Type="String" />
                            </DeleteParameters>
                        </asp:ObjectDataSource>
                        <table cellspacing="0" border="1" id="ctl00_CphContent_GridView2" style="width: 100%;border-collapse: collapse;">
                            <tr>
                                <td style="width: 30%;">
                                    <asp:TextBox ID="TxtAttributeName" runat="server" Width="180px" Text="参数名称"></asp:TextBox></td>
                                <td style="width: 30%;">
                                    <asp:TextBox ID="TxtDefaultValue" runat="server" Width="180px" Text="默认值"></asp:TextBox></td>
                                <td style="width: 30%;">
                                    <asp:TextBox ID="TxtIntro" runat="server" Width="180px" Text="参数说明"></asp:TextBox></td>
                                <td style="width: 10%;">
                                    <asp:Button ID="BtnAddProperty" OnClick="BtnAddProperty_Click" runat="server" Text="添加" EnableViewState="False">
                                    </asp:Button></td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr class="tdbg">
            <td align="center">
                <asp:Button ID="BtnPrv" OnClick="BtnPrv_Click" runat="server" Style="cursor: pointer;
                    cursor: hand; width: 88px;" Text="上一步" />&nbsp;&nbsp;<asp:Button ID="BtnNext" runat="server"
                        Text="下一步" OnClick="BtnNext_Click" Style="cursor: pointer; cursor: hand; width: 88px;" />&nbsp;&nbsp;<asp:Button
                            ID="BtnSave" runat="server" Text="保　存" OnClick="BtnSave_Click" Style="cursor: pointer;
                            cursor: hand; width: 88px;" Visible="False" />&nbsp;&nbsp;<input id="BtnCancel" type="button"
                                class="inputbutton" value="取　消" onclick="Redirect('LabelManage.aspx')" style="cursor: pointer;
                                cursor: hand; width: 88px;" />
            </td>
        </tr>
    </table>
    <br />
    <table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="border">
        <tr>
            <td class="spacingtitle" colspan="2" align="center">
                <b>相关说明</b></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbg" align="left">
                <ajaxToolkit:TabContainer runat="server" ID="Tabs" Height="150px">
                    <ajaxToolkit:TabPanel runat="Server" ID="TabPanel" HeaderText="标签参数">
                        <ContentTemplate>
                            <p>
                                标签参数，就是标签中可自由定义的变量，您可以自由针对标签增加参数，增加后的参数名在标签调用时以{$PE id="标签名" 参数名="输入值"/}的方式调用，系统支持多参数，如果定义了参数又没有在模板编辑中编辑标签时给出参数值，则以属性默认值进行数据显示。</p>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel runat="Server" ID="TabPanel1" HeaderText="参数用途">
                        <ContentTemplate>
                            <p>
                                标签参数变量可用于SQL查询语句和模板中，用于SQL语句时的对应关系为，假设定义了参数：selectnum，则在SQL语句中使用如下方法调用：select top
                                @selectnum from database<br />
                                如果是用于标签模板，调用方式则为&lt;xsl:value-of select=&quot;selectnum&quot;/&gt;。</p>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel runat="Server" ID="TabPanel2" HeaderText="参数枚举">
                        <ContentTemplate>
                            <p>
                                参数的默认值支持枚举方法，多个枚举值请使用"|||"符号分割开，比如：p1|||p2|||p3，一但定义为枚举，则调用方法为{$PE id="标签名" 参数名="枚举位置"/}，其中枚举位置为您定义的枚举列表中排列顺序决定，从0开始到枚举的最大一位结束。例如：{$PE
                                id="标签名" 参数名="1"/}，则该参数得到的数据即为"p2"。<br />
                                <br />
                                注意：一旦定义为枚举类型，您将不能自由设置该标签的输入值，任何超越标准的输入值都将被重定向为0</p>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                </ajaxToolkit:TabContainer>
            </td>
        </tr>
    </table>
</asp:Content>
