<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.User.Contents.SpecialList" Codebehind="SpecialList.aspx.cs" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>选择专题</title>
    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <script language="javascript" type="text/javascript">
<!--
    var returnNodeName;
    var nnodeId;
    function going(nodeId)
    {
       CallTheServer(nodeId,"");
       nnodeId = nodeId;
    }
function category(){alert("你选择的是专题类别，请选择专题！");}
    
    function ReceiveServerData(rvalue)
    {
        returnNodeName = rvalue;
        var isMSIE= (navigator.appName == "Microsoft Internet Explorer");
        if(isMSIE)
        {
            window.returnValue =nnodeId + "$$$" + returnNodeName;
            window.close();
        }
        else
        {
            opener.UpdateSpecial(nnodeId + "$$$" + returnNodeName);
            window.close();
        }
    }
-->
            </script>

            <pe:XLoadTree ID="XLoadNodeTree" RootText="网站" RootAction="#" RootTarget="_self"
                runat="server">
            </pe:XLoadTree>
        </div>
        <br />
        <br />
        <div style="color: Red">
            只能添加到专题中，不能添加到专题类别中</div>
    </form>
</body>
</html>