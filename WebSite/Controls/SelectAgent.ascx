<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Controls.SelectAgent" Codebehind="SelectAgent.ascx.cs" %>

<script language='javascript' type="text/javascript">
      function SelectAgent()
      {
            window.open("<%=ManageDir %>Shop/AgentList.aspx","window","width=600,height=450");
      }
</script>

<asp:TextBox ID="TxtAgentName" ReadOnly="true" runat="server"></asp:TextBox>
<input type="button" class="inputbutton" onclick="SelectAgent()" value="..." />
<asp:HiddenField ID="HdnAgentName" runat="server" />
