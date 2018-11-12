<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.User.Contents.CommentTree" Codebehind="CommentTree.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat ="server">
    <title>评论树</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <script language="javascript" type="text/javascript">
     <!--
        function going(type,id,specialId,Iseshop)
        {
        }
        function Reflash_main_right(t)
        {
            var url = "CommentManage.aspx";
            JumpToMainRight(url);
        }
        
        function JumpToMainRight(url) { parent.frames["main_right"].location = url; }
    //-->
    </script>
    <pe:XLoadTree ID="XLoadNodeTree"  RootIcon="WebSite" runat="server"></pe:XLoadTree>
    </div>
    </form>
</body>
</html>

