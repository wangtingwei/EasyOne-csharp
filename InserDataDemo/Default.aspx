<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="InserDataDemo._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <style type="text/css">
        #Add
        {
            width: 68px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <p align="center">
        <asp:Button ID="Button1" runat="server" Text="添加" OnClick="OnAddClick" />
    </p>
    
    </div>
    </form>
</body>
</html>
