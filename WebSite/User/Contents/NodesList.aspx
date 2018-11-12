<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.User.Contents.NodesList"
    Title="节点树" Codebehind="NodesList.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>节点树</title>
    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <script language="javascript" type="text/javascript">
            <!--
                var returnNodeName;
                var nnodeId;
                function going(nodeId,nodeName)
                {
                   CallTheServer(nodeId,"");
                   nnodeId = nodeId;
                }
                
                function ReceiveServerData(rvalue)
                {
                   returnNodeName = rvalue;
                   opener.SetNode(nnodeId + "$$$" + returnNodeName);
                   window.close();
                }
                
                function rightMenu(nodeId,arrModelId,arrModelName,event) {
                }
            -->
            </script>

            <pe:XLoadTree ID="XLoadNodeTree" RootText="网站" RootAction="#" RootTarget="_self"
                runat="server">
            </pe:XLoadTree>
        </div>
    </form>
</body>
</html>
