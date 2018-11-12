<%@ Control Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Controls.FieldControl.NodeType" Codebehind="NodeType.ascx.cs" %>
<tr id='Tab' runat="server" class='tdbg'>
    <td class='tdbgleft' align='right' style="width: 20%;">
        <div class="DivWordBreak">
            <strong>
                <%= FieldAlias %>
                ：&nbsp;</strong><br />
            <%= Tips %>
        </div>
    </td>
    <td class='tdbg' align='left'>
        <asp:HiddenField ID="HdnNodeId" runat="server" />
        <asp:PlaceHolder ID="PnlChange" runat="server">
            <asp:Label ID="LblNavigation" runat="server"></asp:Label>
            <input type="button" class="button" onclick="ShowWindow()" value="更换节点" />
            <asp:PlaceHolder ID="PhAddInfo" runat="server">
                <input type="button" class="button" onclick="AddInfo()" value="添加到其它节点" /></asp:PlaceHolder>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="PnlSelect" runat="server">
            <asp:DropDownList ID="DrpNodeList" runat="server" DataTextField="NodeName" DataValueField="NodeId">
            </asp:DropDownList><pe:RequiredFieldValidator ID="ReqDrpNodeList" runat="server"
                ControlToValidate="DrpNodeList" SetFocusOnError="true" Display="Dynamic" ErrorMessage="请选择栏目"
                Visible="false"></pe:RequiredFieldValidator><asp:PlaceHolder ID="PhAddInfo2" runat="server">
                    <input type="button" class="button" onclick="AddInfo()" value="添加到其它节点" /></asp:PlaceHolder>
        </asp:PlaceHolder>
        <asp:HiddenField ID="HdnInfoIds" runat="server" />
        <div>
            <ul style="margin: 0; padding: 0;" id="SelectedNodes" runat="server">
            </ul>
        </div>
        <iframe marginwidth='0' marginheight='0' frameborder='0' id='UploadPath' width='0'
            height='0' src='<%=GetUploadPath%>'></iframe>
    </td>
</tr>

<script type="text/javascript">
<!--
var nodeIdClientId = "<%= HdnNodeId.ClientID %>";
//-->
</script>

