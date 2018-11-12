<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.ShowError" Codebehind="ShowError.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>错误提示信息</title>
    <link href="../Prompt/prompts.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="Showms">
            <dl class="top">
            </dl>
            <dl class="content">
                <dd class="content1">
                    <div class="Pic">
                        <asp:Image ID="ImgWrong" ImageUrl="~/Prompt/images/P_Wrong.gif" runat="server" /></div>
                    <div class="MS">
                        <dl>
                            <dt class="titWrong">错误信息</dt>
                            <dd>
                                产生错误的可能原因：<br />
                                <pe:ExtendedLiteral HtmlEncode="false" ID="LtrErrorMessage" runat="server"></pe:ExtendedLiteral></dd>
                        </dl>
                    </div>
                    <div class="clearbox">
                    </div>
                    <div class="BUT">
                        <asp:HyperLink ID="LnkReturnUrl" runat="server"><< 返回上一页</asp:HyperLink></div>
                </dd>
            </dl>
            <dl class="bottom">
            </dl>
            <dl class="Shadow">
            </dl>
        </div>
        <div class="clearbox">
        </div>
    </form>
</body>
</html>
