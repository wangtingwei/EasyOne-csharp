<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.User.Contents.NavContent" Codebehind="NavContent.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
        <table style="width:99%; height:130px; margin: 0 auto;" cellpadding="2" cellspacing="1"
            class="border">
            <tr class='tdbg'>
                <td class="spacingtitle" colspan="2" align="center">
                    <pe:AlternateLiteral ID="AlternateLiteral1" Text="内容添加" runat="Server" />
                </td>
            </tr>
            <tr class='tdbg'>
                <td class='tdbgleft'>
                    请选择发表栏目：
                </td>
                <td>
                    <div id="NodeTree">
                    </div>
                </td>
            </tr>
            <tr class='tdbg'>
                <td class='tdbgleft'>
                    请选择添加的模型：
                </td>
                <td>
                    <div id="Model">
                    </div>
                </td>
            </tr>
            <tr class="tdbg">
                <td colspan="2" align="center">
                    <asp:Button ID="BtnSelectNode" runat="server" Text="下一步" OnClick="BtnSelectNode_Click" />
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="HdnNodeId" runat="server" />
        <asp:HiddenField ID="HdnModelId" runat="server" />

        <script language="javascript" type="text/javascript">
            var node=0;
           $(document).ready(function(){
                var xmlContent =GetNodeXml(null); 
            }
            );
            function GetNodeXml(parm)
            {
                var url= "Nodexml.aspx?action=content";
                
                if(parm!=null||parm!="")
                {
                    url=url+"&nodeid="+parm;
                }
                $.ajax({
                url:url,
                type: 'GET',
                dataType: 'xml',
                error: function(){
                        $("#NodeTree").html("加载节点树出错！");
                },
                success: function(xml){
                    node++;
                    ParseXml(xml);
                }
                });
            }
           
            function ParseXml(xmlContent)
            {
                if(xmlContent==null)
                {
                    $("#NodeTree").html("没有添加权限！");
                    return;
                }
                var xmlSelect;
                
                $(xmlContent).find('tree').find('tree').each(function(i)
                {
                   
                   if($(this).attr("icon")!="Forbid" || $(this).attr("childNumber")>0)
                   {    
                      
                        if($(this).attr("icon")!="Forbid"&&node>1&&i==0)
                        {
                            xmlSelect= xmlSelect+"<option value=-3 selected=\"true\">请选择</option>";
                        }
                        if(i==0)
                        {
                             if(node==1)
                             {
                                document.getElementById("<%=HdnNodeId.ClientID %>").value=$(this).attr("nodeId");
                             }
                           
                             if(parseInt($(this).attr("nodeId"))>0)
                             {
                                InitModelList($(this).attr("nodeId"))
                             }
                             if($(this).attr("childNumber")>0&&node==1)
                             {
                                xmlSelect= xmlSelect+"<option value=-3 selected=\"true\">请选择</option>";
                             }
                        }
                        xmlSelect=xmlSelect+"<option value="+$(this).attr("nodeId") +">"+$(this).attr("text")+"</option>";
                   }
                 }
                );
                CreateDom(xmlSelect);
            }
            function CreateDom(domContent)
            {
                if(String(domContent)!="undefined")
                {
                    $("<select id='Node"+String(node) +"' onchange=ChangeModule(this)>"+ domContent +"</select>").appendTo("#NodeTree");
                }
            }
            function ChangeModule(objSelect)
            {   
                var i=parseInt(objSelect.id.replace("Node",""));
                if(i>0)
                {
                    for(var m=i+1;m<=node;m++)
                    {
                        $("#Node"+m).remove();
                    }
                     node=i;
                     
                    //还需要根据nodeId判断是否前面加"请选择"
                    var nodeId=objSelect.options[objSelect.options.selectedIndex].value;
                    document.getElementById("<%=HdnNodeId.ClientID %>").value=nodeId;
                    $("#Model").empty();
                   
                    GetNodeXml(nodeId);
                     InitModelList(nodeId);
                }
               
            }
            function InitModelList(nodeId)
            {
                $.ajax({
                url:"Nodexml.aspx?action=nodeinfo&nodeid="+nodeId,
                type: 'GET',
                dataType: 'xml',
                timeout: 1000,
                error: function(){
                        $("#Model").html("获取模型出错！");
                },
                success: function(xml){
                    InitModelSelect(xml);
                }
                });
            }
            function InitModelSelect(content)
            {
                
                if(content==null)
                {
                    $("#Model").html("此节点下没有绑定模型！");
                    return;
                }
                var xmlSelect="";
                $("#Model").empty();
              
                $(content).find('tree ').find('tree ').each(function(i)
                {
                   
                    if(i==0&&String($(this).attr("modelId"))!="undefined")
                    {
                       
                        document.getElementById("<%=HdnModelId.ClientID %>").value=$(this).attr("modelId");
                    }
                   
                    xmlSelect=xmlSelect+"<option value="+$(this).attr("modelId") +">添加"+$(this).text()+"</option>";
                 }
                );
               $("#Model").html("<select id='ModelName' onchange=ChangeModel(this)>"+ xmlSelect +"</select>");
              
            }
            function ChangeModel(objSelect)
            {
                document.getElementById("<%=HdnModelId.ClientID %>").value=objSelect.options[objSelect.options.selectedIndex].value;
               
            }
            
           
        </script>

    </form>
</body>
</html>
