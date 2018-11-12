<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Controls.ValidateCodeImage" Codebehind="ValidateCodeImage.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <pe:ValidateCodeImage ID="ValidateCodeImage1" runat="server" ValidateCodeSessionName="ValidateCodeSession"
                ValidateCodeFontSize="10" ValidateCodeMaxLength="6" ValidateCodeMinLength="4"
                ValidateCodeLengthMode="static" />
        </div>
    </form>
</body>
</html>
