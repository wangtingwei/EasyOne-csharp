<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.User.NodePermissions"
    Title="栏目权限管理" EnableViewState="false" Codebehind="NodePermissions.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="server">
    <base target="_self" />
    <table width="100%" border="0" cellpadding="5" cellspacing="1" class="border">
        <tr>
            <td style="width: 100%;" colspan="2" id="NodeList">
                <!-- 显示栏目树开始 -->
                <pe:ExtendedGridView ID="EgvNodes" runat="server" DataSourceID="OdsEgvNodes" GridLines="None"
                    Width="100%" AutoGenerateColumns="false" EnableViewState="false" DataKeyNames="NodeID"
                    OnRowDataBound="EgvNodes_RowDataBound">
                    <HeaderStyle BackColor="#449AE8" HorizontalAlign="Center" Font-Bold="True" ForeColor="White" />
                    <RowStyle CssClass="tdbg" />
                    <Columns>
                        <pe:BoundField DataField="NodeID" HeaderText="ID">
                            <ItemStyle Width="10%" />
                        </pe:BoundField>
                        <pe:TemplateField HeaderText="节点名" SortExpression="NodeName">
                            <HeaderStyle Width="30%" />
                            <ItemTemplate>
                                <pe:ExtendedLabel ID="LabNodeShowTree" runat="server"></pe:ExtendedLabel>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </pe:TemplateField>
                        <pe:TemplateField HeaderText="浏览" Visible="false">
                            <HeaderStyle Width="10%" />
                            <ItemTemplate>
                                <asp:CheckBox ID="ChkNodeSkim" runat="server"></asp:CheckBox>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </pe:TemplateField>
                        <pe:TemplateField HeaderText="查看" Visible="false">
                            <HeaderStyle Width="10%" />
                            <ItemTemplate>
                                <asp:CheckBox ID="ChkNodePreview" runat="server"></asp:CheckBox>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </pe:TemplateField>
                        <pe:TemplateField HeaderText="录入" Visible="false">
                            <HeaderStyle Width="10%" />
                            <ItemTemplate>
                                <asp:CheckBox ID="ChkNodeInput" runat="server"></asp:CheckBox>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </pe:TemplateField>
                        <pe:TemplateField HeaderText="审核" Visible="false">
                            <HeaderStyle Width="10%" />
                            <ItemTemplate>
                                <asp:CheckBox ID="ChkNodeCheck" runat="server"></asp:CheckBox>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </pe:TemplateField>
                        <pe:TemplateField HeaderText="信息管理" Visible="false">
                            <HeaderStyle Width="10%" />
                            <ItemTemplate>
                                <asp:CheckBox ID="ChkContentManage" runat="server"></asp:CheckBox>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </pe:TemplateField>
                        <pe:TemplateField HeaderText="设置当前节点" Visible="false">
                            <HeaderStyle Width="8%" />
                            <ItemTemplate>
                                <asp:CheckBox ID="ChkCurrentNodesManage" runat="server"></asp:CheckBox>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </pe:TemplateField>
                        <pe:TemplateField HeaderText="子节点管理<br/>（可以添加、修改、删除、排序子节点）" Visible="false">
                            <HeaderStyle Width="20%" />
                            <ItemTemplate>
                                <asp:CheckBox ID="ChkChildNodeManage" runat="server"></asp:CheckBox>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </pe:TemplateField>
                        <pe:TemplateField HeaderText="回复" Visible="false">
                            <HeaderStyle Width="10%" />
                            <ItemTemplate>
                                <asp:CheckBox ID="ChkNodeCommentReply" runat="server"></asp:CheckBox>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </pe:TemplateField>
                        <pe:TemplateField HeaderText="审核" Visible="false">
                            <HeaderStyle Width="10%" />
                            <ItemTemplate>
                                <asp:CheckBox ID="ChkNodeCommentCheck" runat="server"></asp:CheckBox>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </pe:TemplateField>
                        <pe:TemplateField HeaderText="管理" Visible="false">
                            <HeaderStyle Width="10%" />
                            <ItemTemplate>
                                <asp:CheckBox ID="ChkNodeCommentManage" runat="server"></asp:CheckBox>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </pe:TemplateField>
                    </Columns>
                </pe:ExtendedGridView>
                <asp:ObjectDataSource ID="OdsEgvNodes" runat="server" DataObjectTypeName="EasyOne.Model.Contents.NodeInfo"
                    DeleteMethod="Delete" InsertMethod="Insert" SelectMethod="GetNodeListExecptLinkType"
                    TypeName="EasyOne.Contents.Nodes" UpdateMethod="Update"></asp:ObjectDataSource>
                <!-- 显示栏目树结束 -->
            </td>
        </tr>
    </table>
    <br />
    <center>
        <br />
        <asp:Button ID="BtnSubmit" runat="server" Text="保存返回权限设置" OnClick="BtnSubmit_Click" />&nbsp;&nbsp;
        <input type="button" value="取消返回权限设置" onclick="window.close();" />
    </center>

    <script language="JavaScript" type="text/javascript">
    <!--
    function ChkNodeAll(form,nodeItem,clientId){
		if (clientId.checked)
		{	
            var oSpanArr = document.getElementsByTagName('table');
            var j = oSpanArr.length;
            for ( var i=0; i<j; i++ ) 
            {
                if (oSpanArr[i].id != ""){
                    var inputArr = oSpanArr[i].getElementsByTagName('input');
                    var m = inputArr.length
                    for ( var r=0; r< m; r++ ) 
                    {
                        var t = inputArr[r];
			            if (t.name){
				            if (t.name.indexOf(nodeItem) != -1 && t.disabled==false)
				            {
					            t.checked = false;  
				            }
			            }  
                    }
                    
                }   
            }
			clientId.checked = true;  
		}
	}
		
    function ChkWipeOffNodeAll(clientId){
         clientId.checked = false;
    } 
    
    
    function ChkNodeAll2(nodeItem,clientId,nodeManage){
		if (clientId.checked)
		{
            var oSpanArr = document.getElementsByTagName('table');
            var j = oSpanArr.length;
            for ( var i=0; i<j; i++ ) 
            {
                if (oSpanArr[i].id != ""){
                    var inputArr = oSpanArr[i].getElementsByTagName('input');
                    var m = inputArr.length
                    for ( var r=0; r< m; r++ ) 
                    {
                        var t = inputArr[r];
			            if (t.name){
				            if (t.name.indexOf(nodeItem) != -1 && t.disabled==false)
				            {
					            t.checked = false;  
				            }
			            }  
                    }
                    
                }   
            }
			clientId.checked = true;  
		}
		else
		{
			var s = nodeManage.id.substring(nodeManage.id.lastIndexOf("_")+1,nodeManage.id.length);	
		    if(!nodeManage.checked){
		        var oSpanArr = document.getElementsByTagName('table');
                var j = oSpanArr.length;
                for ( var i=0; i<j; i++ ) 
                {
                    if (oSpanArr[i].id != ""){
                        var inputArr = oSpanArr[i].getElementsByTagName('input');
                        var m = inputArr.length
                        for ( var r=0; r< m; r++ ) 
                        {
                            var t = inputArr[r];
			                if (t.name){
				                if (t.name.indexOf(s) != -1 && t.disabled==false)
				                {
				                    if (t.checked)
						            {
				                       var oCell = document.getElementById(t.id.substring(0,t.id.lastIndexOf("_")+1) + nodeItem);
									   oCell.checked = true;
						            }
						        }
			                }  
                        }
                    }   
                }
            }
			else
			{
				if (nodeManage.checked)
				{
					 clientId.checked = true;
					 alert("节点已经拥有信息管理权限，信息管理拥有查看、录入、审核权限，若取消此权限请先取消信息管理权限！");
					 return;
				}
			}
		}
	}
    function ChkWipeOffNodePermissionsAll(fatherclientId,nodeItem,clientId){
	    var oCell = document.getElementById(nodeItem);
		if (!clientId.checked)
	    {
			if (oCell.checked)
			{
				 clientId.checked = true;
				 alert("节点已经拥有信息管理权限，信息管理拥有查看、录入、审核权限，若取消此权限请先取消信息管理权限！");
				 return;
			}
	    }
		var s = oCell.id.substring(oCell.id.lastIndexOf("_")+1,oCell.id.length);	
		var oSpanArr = document.getElementsByTagName('table');
		var j = oSpanArr.length;
		for ( var i=0; i<j; i++ ) 
		{
			if (oSpanArr[i].id != ""){
				var inputArr = oSpanArr[i].getElementsByTagName('input');
				var m = inputArr.length
				for ( var r=0; r< m; r++ ) 
				{
					var t = inputArr[r];
					if (t.name){
						if (t.name.indexOf(s) != -1 && t.disabled==false)
						{
							if (t.checked)
							{
							   var oCell = document.getElementById(t.id.substring(0,t.id.lastIndexOf("_")+1) + clientId.id.substring(oCell.id.lastIndexOf("_")+1,oCell.id.length));
							   oCell.checked = true;
							}
						}
					}  
				}
			}   
		}
        fatherclientId.checked = false;
    } 
    
    function ChkNodeManageAll(form,nodeItem,clientId){
		if (clientId.checked)
		{	
			var currentlyNodeId = clientId.id;
			currentlyNodeId = currentlyNodeId.substring(currentlyNodeId.indexOf("_"), currentlyNodeId.lastIndexOf("_"));
			var oSpanArr = document.getElementsByTagName('table');
			var j = oSpanArr.length;
			for ( var i=0; i<j; i++ ) 
			{
				if (oSpanArr[i].id != ""){
					var inputArr = oSpanArr[i].getElementsByTagName('input');
					var m = inputArr.length
					for ( var r=0; r< m; r++ ) 
					{
						var t = inputArr[r];
						if (t.id){
							if (t.id.indexOf(currentlyNodeId) > 0 && t.disabled==false)
							{
								t.checked = true;  
							}
							else{
								t.checked = false;  
							}
						}
					}
				}   
			}
		}
	}
    function ChkWipeOffNodeManageAll(clientId,nodeItem,nodePreviewId,nodeInputId,nodeCheckId){
		clientId.checked = false;
		if (nodeItem == "" || nodeItem.indexOf("_") == 0)
		{ 
		   return;
		} 
		var oCell = document.getElementById(nodeItem);
		if (!oCell.checked)
		{
			 return;
		}

		nodeItem = nodeItem.substring(nodeItem.indexOf("_"), nodeItem.lastIndexOf("_"));
		var oSpanArr = document.getElementsByTagName('table');
		var j = oSpanArr.length;
		for ( var i=0; i<j; i++ ) 
		{
			if (oSpanArr[i].id != ""){
				var inputArr = oSpanArr[i].getElementsByTagName('input');
				var m = inputArr.length
				for ( var r=0; r< m; r++ ) 
				{
					var t = inputArr[r];
					if (t.id){
						if (t.id.indexOf(nodeItem) > 0 && t.disabled==false)
						{
						    var n = t.id.substring(t.id.lastIndexOf("_")+1,t.id.length);

						    if ((nodePreviewId.checked && nodePreviewId.id.indexOf(n) > 0) || (nodeInputId.checked && nodeInputId.id.indexOf(n) > 0) || (nodeCheckId.checked && nodeCheckId.id.indexOf(n) > 0))
						    {
				                t.checked = false;  
						    }
						    else
						    {
						        t.checked = true;  
						    }
						}
					}  
				}
			}   
		}
    }
  //-->
    </script>

</asp:Content>
