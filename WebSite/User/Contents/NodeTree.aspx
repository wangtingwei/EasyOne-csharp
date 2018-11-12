<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.User.Contents.NodeTree" Codebehind="NodeTree.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <script language="javascript" type="text/javascript">
<!--
    function going(type,id,nodeId,Iseshop)
    {
        switch (type)
        {    
            case "addcontent":
                if(Iseshop=="True")
                {
                    var url = "../Shop/Product.aspx?Action=add&NodeId=" + nodeId + "&ModelID=" + id;
                    JumpToMainRight(url);
                }
                else
                {
                    var url = "Content.aspx?Action=add&NodeId=" + nodeId + "&ModelID=" + id;
                    JumpToMainRight(url);
                }
                break;
            case "-1":
                var url = "ContentManage.aspx?Status=-1&NodeID=" + nodeId;
                JumpToMainRight(url);
                break;
            case "101":
                var url = "ContentManage.aspx?Status=101&NodeID=" + nodeId;
                JumpToMainRight(url);
                break;
            case "99":
                 var url = "ContentManage.aspx?Status=99&NodeID=" + nodeId;
                JumpToMainRight(url);
                break;
            case "-2":
                var url = "ContentManage.aspx?Status=-2&NodeID=" + nodeId;
                JumpToMainRight(url);
                break;
            case "favorite":
                var url = "favorite.aspx?NodeID=" + nodeId;
                JumpToMainRight(url);
                break;
            case "comment":
                var url = "CommentManage.aspx?NodeID=" + nodeId;
                JumpToMainRight(url);
                break;
        }
    }
    
    function JumpToMainRight(url) { parent.frames["main_right"].location = url; }
    
    function rightMenu(nodeId,arrModelId,arrModelName,event) {}
//-->
    </script>
    <pe:XLoadTree ID="XLoadNodeTree"  RootIcon="WebSite" runat="server">
                    </pe:XLoadTree>
    </div>
    </form>
</body>
</html>
