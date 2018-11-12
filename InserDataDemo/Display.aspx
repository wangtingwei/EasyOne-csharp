<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Display.aspx.cs" Inherits="InserDataDemo.Display" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <asp:Label ID="Label1" runat="server" Text=></asp:Label><br />
            <asp:Repeater ID="RepeaterInner" runat="server">
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("s_name") %>'></asp:Label>
                </ItemTemplate>
            </asp:Repeater>
        </ItemTemplate>
        </asp:Repeater>
    </div>
    </form>
</body>
</html>
