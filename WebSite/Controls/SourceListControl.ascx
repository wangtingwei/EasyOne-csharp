<%@ Control Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Controls.SourceListControl" Codebehind="SourceListControl.ascx.cs" %>
  <div style="text-align: center">
         <% openerInput = Request.QueryString["OpenerInput"];%>
            <table width="560" border="0" cellpadding="2" cellspacing="0" class="border">
                <tr class="title">
                    <td align="left">
                        <a href="SourceList.aspx?OpenerInput=<%= openerInput %>">全部分类</a>
                        <asp:Repeater ID="RptSourceType" runat="server" DataSourceID="OdsSourceType">
                            <ItemTemplate>
                                | <a href="SourceList.aspx?OpenerInput=<%= openerInput %>&type=<%# Eval("Name") %>">
                                    <%# Eval("Name") %>
                                </a>
                            </ItemTemplate>
                        </asp:Repeater>
                        <asp:ObjectDataSource ID="OdsSourceType" runat="server" SelectMethod="GetSourceTypeList"
                            TypeName="EasyOne.Accessories.Source"></asp:ObjectDataSource>
                    </td>
                </tr>
            </table>
            <br />
            <table width="560" border="0" style="text-align: center" cellpadding="2" cellspacing="0"
                class="border">
                <tr class="title">
                    <td align="left">
                        <b>列表：</b></td>
                    <td align="right">
                        <asp:TextBox ID="TxtSource" runat="server"></asp:TextBox>&nbsp;&nbsp;<asp:Button
                            ID="BtnSearch" runat="server" Text="查找" /></td>
                </tr>
                <tr>
                    <td valign="top" colspan="2">
                        <asp:Repeater ID="RptSource" runat="server">
                            <HeaderTemplate>
                                <table width="550" border="0" cellspacing="1" cellpadding="1" style="background-color: #f9f9f9">
                                    <tr align="center">
                                        <td style="width: 100">
                                            名称</td>
                                        <td style="width: 100">
                                            来源分类</td>
                                        <td>
                                            简介</td>
                                    </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td align="center">
                                        <a href="#" onclick="add(<%# "'" + Eval("name") + "'"%>)">
                                            <%# Eval("name")%>
                                        </a>
                                    </td>
                                    <td>
                                        <%# Eval("type")%>
                                    </td>
                                    <td>
                                        <%# Eval("intro")%>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </td>
                </tr>
            </table>
            <pe:AspNetPager ID="Pager" runat="server" OnPageChanged="Pager_PageChanged">
            </pe:AspNetPager>

            <script language="javascript" type="text/javascript">
    function add(obj)
    {
        if(obj==""){return false;}
        opener.document.getElementById("<%= openerInput %>").value=obj;
        window.close();
    }
            </script>

        </div>
