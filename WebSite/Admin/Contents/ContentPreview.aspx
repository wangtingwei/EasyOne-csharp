<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Contents.ContentPreview" Codebehind="ContentPreview.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>内容预览</title>
</head>
<body>
    <form id="form1" runat="server">
    
<script language="javascript" type="text/javascript">
<!--
/* 改变图片大小 */
function resizepic(thispic)
{
if(thispic.width>550){thispic.height=thispic.height*550/thispic.width;thispic.width=550;} 
}

/* 无级缩放图片大小 */
function bbimg(o)
{
  return true;
}
//-->
</script>
    <pe:ExtendedLiteral ID="LitContent" HtmlEncode="false" runat="server"></pe:ExtendedLiteral>
    </form>
</body>
</html>
