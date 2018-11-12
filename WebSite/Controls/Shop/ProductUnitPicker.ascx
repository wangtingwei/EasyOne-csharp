<%@ Control Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Controls.Shop.ProductUnitPicker" Codebehind="ProductUnitPicker.ascx.cs" %>
<asp:TextBox ID="TxtUnit" runat="server" MaxLength="4" Width="80px"></asp:TextBox>
&lt;&lt;
<asp:DropDownList ID="DropProductUnit" runat="server" AppendDataBoundItems="true">
    <asp:ListItem Text="请选择" Value="-1"></asp:ListItem>
</asp:DropDownList>&nbsp;<pe:RequiredFieldValidator ID="ValrUnit" runat="server" ControlToValidate="TxtUnit"
    Display="Dynamic" ErrorMessage="单位不能为空！" SetFocusOnError="true"></pe:RequiredFieldValidator>
    
<script type="text/javascript">

var dropUnit = document.getElementById('<%=DropProductUnit.ClientID %>');
var txtUnit = document.getElementById('<%= TxtUnit.ClientID %>');

    function PickUnit()
    {
        if(dropUnit.selectedIndex !=0)
        {
            txtUnit.value = dropUnit.value;
        }
    }
</script>
