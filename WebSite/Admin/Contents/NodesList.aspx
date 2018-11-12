<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Contents.NodesList"
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
       var action = '<%= Request.QueryString["Action"] %>';
       if(action == 'SetNode')
       {
           opener.SetNode(nnodeId + "$$$" + returnNodeName);
           window.close();
       }
       if(action == 'AddInfo')
       {
           opener.UpdateTheInfoNodes(nnodeId + "$$$" + returnNodeName);
       }
    }
-->
            </script>

            <pe:XLoadTree ID="XLoadNodeTree" RootText="网站" RootAction="#" RootTarget="_self"
                CheckBox="true" runat="server">
            </pe:XLoadTree>
        </div>
        <br />
<div style="text-align:center"><input type="button" value="关闭窗口" onclick="javascript:window.close();" /></div>
        <script language="javascript" type="text/javascript">
<!--
function OverrideXmlFileLoaded(oXmlDoc, jsParentNode) {
  var action = '<%= Request.QueryString["Action"] %>';
  var nodeids = opener.document.getElementById("<%= Request.QueryString["ClientID"] %>").value;
  var nodeidsarr = nodeids.split(',');
	for(i=0;i<nodeidsarr.length;i++)
	{
	    if(document.getElementById("EasyOne2007_NodeId_" + nodeidsarr[i]))
	    {
	        document.getElementById("EasyOne2007_NodeId_" + nodeidsarr[i]).checked = true;
	    }
	}
}

webFXTreeHandler.check = function(obj,nodeid,nodename)
{
    if(obj.checked)
    {
        going(nodeid,nodename);
    }
    else
    {
        opener.Del(nodeid);
    }
}
-->
        </script>

    </form>
</body>
</html>
