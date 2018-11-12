<%@ Control Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Controls.Purview" Codebehind="Purview.ascx.cs" %>
<table width='100%' border='0' cellpadding='0' cellspacing='0'>
    <tr align='center'>
        <td id='TabTitle0' runat="server" class='titlemouseover' onclick='ShowTabs(0)'>
            管理权限</td>
        <td id='TabTitle1' runat="server" class='tabtitle' onclick='ShowTabs(1)' style="display: none;">
            信息管理权限</td>
        <td id='TabTitle2' runat="server" class='tabtitle' onclick='ShowTabs(2)' style="display: none;">
            专题权限</td>
        <td id='TabTitle3' runat="server" class='tabtitle' onclick='ShowTabs(3)'>
            字段权限</td>
        <td id='TabTitle4' runat="server" class='tabtitle' onclick='ShowTabs(4)' style="display: none;">
            节点权限</td>
        <td id='TabTitle5' runat="server" class='tabtitle' onclick='ShowTabs(5)' style="display: none;">
            评论权限</td>
        <td>
            &nbsp;</td>
    </tr>
</table>
<table width='100%' border='0' cellpadding='5' cellspacing='1' class='border'>
    <tr class='tdbg'>
        <td style="height: 100px;" valign='top' colspan="2">
            <table width='100%' cellpadding='2' cellspacing='1' style="background-color: white;">
                <tbody id='Tabs0' runat="server">
                    <tr class='tdbg'>
                        <td align="left" class="tdbg" colspan="2" valign="top" style='padding-left: 20px;'>
                            <fieldset id="ModelPurview" style="border: 0px;">
                                <table width='100%' border='0' cellpadding='0' cellspacing='1'>
                                    <asp:Label ID="LblModelPurview" Text="" runat="server" />
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                </tbody>
                <tbody id='Tabs1' runat="server" style="display: none">
                    <tr class="tdbg">
                        <td align="right" colspan="2">
                            【<a onclick="javascript:ShowTabs(0)" href='#'><span style='color: blue'>返回管理权限</span></a>】
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%;" colspan="2" id="NodeList">
                            <!-- 显示栏目树开始 -->
                            <pe:ExtendedGridView ID="EgvContents" runat="server" DataSourceID="OdsEgvContents"
                                GridLines="None" Width="100%" AutoGenerateColumns="false" DataKeyNames="NodeID"
                                OnRowDataBound="EgvContents_RowDataBound">
                                <HeaderStyle BackColor="#449AE8" HorizontalAlign="Center" Font-Bold="True" ForeColor="White" />
                                <RowStyle CssClass="tdbg" />
                                <Columns>
                                    <pe:BoundField DataField="NodeID" HeaderText="ID">
                                        <ItemStyle Width="10%" />
                                    </pe:BoundField>
                                    <pe:TemplateField HeaderText="节点名" SortExpression="NodeName">
                                        <HeaderStyle Width="30%" />
                                        <ItemTemplate>
                                            <asp:Label ID="LabNodeShowTree" runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </pe:TemplateField>
                                    <pe:TemplateField HeaderText="查看">
                                        <HeaderStyle Width="10%" />
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ChkNodePreview" runat="server"></asp:CheckBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </pe:TemplateField>
                                    <pe:TemplateField HeaderText="录入">
                                        <HeaderStyle Width="10%" />
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ChkNodeInput" runat="server"></asp:CheckBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </pe:TemplateField>
                                    <pe:TemplateField HeaderText="审核">
                                        <HeaderStyle Width="10%" />
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ChkNodeCheck" runat="server"></asp:CheckBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </pe:TemplateField>
                                    <pe:TemplateField HeaderText="信息管理">
                                        <HeaderStyle Width="10%" />
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ChkContentManage" runat="server"></asp:CheckBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </pe:TemplateField>
                                </Columns>
                            </pe:ExtendedGridView>
                            <asp:ObjectDataSource ID="OdsEgvContents" runat="server" DataObjectTypeName="EasyOne.Model.Contents.NodeInfo"
                                DeleteMethod="Delete" InsertMethod="Insert" SelectMethod="GetNodeListExecptLinkType"
                                TypeName="EasyOne.Contents.Nodes" UpdateMethod="Update"></asp:ObjectDataSource>
                            <!-- 显示栏目树结束 -->
                        </td>
                    </tr>
                    <tr class="tdbg">
                        <td align="right" colspan="2">
                            【<a onclick="javascript:ShowTabs(0)" href='#'><span style='color: blue'>返回管理权限</span></a>】
                        </td>
                    </tr>
                </tbody>
                <tbody id="Tabs2" runat="server" style="display: none">
                    <tr class="tdbg">
                        <td align="right" colspan="2">
                            【<a onclick="javascript:ShowTabs(0)" href='#'><span style='color: blue'>返回管理权限</span></a>】</td>
                    </tr>
                    <tr class="tdbg">
                        <td align="left" class="tdbg" valign="top" style="width: 100%;">
                            <!-- 显示专题类别 -->
                            <pe:ExtendedGridView ID="EgvSpecial" DataSourceID="OdsSpecial" GridLines="None" runat="server"
                                Width="100%" AutoGenerateColumns="False" OnRowDataBound="EgvSpecial_RowDataBound"
                                CheckBoxFieldHeaderWidth="3%" SerialText="">
                                <HeaderStyle BackColor="#449AE8" HorizontalAlign="Center" Font-Bold="True" ForeColor="White" />
                                <Columns>
                                    <pe:BoundField DataField="Id" HeaderText="序号" SortExpression="Id" />
                                    <pe:TemplateField HeaderText="专题名" SortExpression="Name">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="LabName"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </pe:TemplateField>
                                    <pe:TemplateField HeaderText="添加内容到专题" SortExpression="TreeLineType">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ChkSpecialInput" runat="server"></asp:CheckBox>
                                        </ItemTemplate>
                                    </pe:TemplateField>
                                    <pe:TemplateField HeaderText="专题内容管理" SortExpression="TreeLineType">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ChkSpecialManage" runat="server"></asp:CheckBox>
                                            <asp:HiddenField ID="HdnSpecialId" runat="server" />
                                        </ItemTemplate>
                                    </pe:TemplateField>
                                </Columns>
                            </pe:ExtendedGridView>
                            <asp:ObjectDataSource ID="OdsSpecial" runat="server" SelectMethod="GetSpecialTree"
                                TypeName="EasyOne.Contents.Special"></asp:ObjectDataSource>
                        </td>
                    </tr>
                    <tr class="tdbg">
                        <td align="right" colspan="2">
                            【<a onclick="javascript:ShowTabs(0)" href='#'><span style='color: blue'>返回管理权限</span></a>】</td>
                    </tr>
                </tbody>
                <tbody id='Tabs3' runat="server" style="display: none">
                    <tr class='tdbg'>
                        <td align="left" class="tdbg" valign="top" style="width: 20%;">
                            <table width='100%' border='0' cellpadding='5' cellspacing='1'>
                                <!-- 显示模型树开始 -->
                                <asp:Repeater ID="RptModelList" runat="server" DataSourceID="OdsModelList" OnItemDataBound="RptModelList1_ItemDataBound">
                                    <HeaderTemplate>
                                        <tr class='title'>
                                            <td align="center">
                                                模型名</td>
                                        </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr id="ModelTr" runat="server">
                                            <td align="center">
                                                <asp:Label ID="modellist" runat="server"></asp:Label></td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </table>
                            <asp:ObjectDataSource ID="OdsModelList" runat="server" SelectMethod="GetModelList"
                                TypeName="EasyOne.CommonModel.ModelManager">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="0" Name="modelType" Type="int32" />
                                    <asp:Parameter DefaultValue="1" Name="showType" Type="int32" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </td>
                        <td class="tdbg" valign="top">
                            <!-- 显示模型树开始 -->
                            <asp:Repeater ID="RptModelList2" runat="server" DataSourceID="OdsModelList" OnItemDataBound="RptModelList2_ItemDataBound">
                                <HeaderTemplate>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table width='100%' border='0' cellpadding='5' cellspacing='1' id="model" runat="server">
                                        <tr>
                                            <td align="center">
                                                <table width='100%' border='0' cellpadding='2' cellspacing='1' class='border' style="background-color: white;">
                                                    <tr class='title'>
                                                        <td align="center" style="width: 20%; height: 18px">
                                                            字段名称
                                                        </td>
                                                        <td align="center" style="width: 20%;">
                                                            字段别名
                                                        </td>
                                                        <td align="center" style="width: 20%">
                                                            字段类型
                                                        </td>
                                                        <td align="center" style="width: 20%">
                                                            字段级别
                                                        </td>
                                                        <td align="center" style="width: 20%">
                                                            禁止设置值
                                                        </td>
                                                    </tr>
                                                    <asp:HiddenField ID="HdnModelId" runat="server" Value='<%# Eval("ModelId") %>' />
                                                    <asp:Repeater ID="RptFieldList" runat="server">
                                                        <ItemTemplate>
                                                            <tr class="tdbg" align="center" onmouseover="MouseOver(this,&quot;tdbgmouseover&quot;)"
                                                                onmouseout="MouseOut(this)">
                                                                <td align="center" style="width: 20%; height: 22px">
                                                                    <%# Eval("FieldName") %>
                                                                </td>
                                                                <td align="center" style="width: 20%; height: 22px">
                                                                    <%# Eval("FieldAlias") %>
                                                                </td>
                                                                <td align="center" style="width: 20%">
                                                                    <%# EasyOne.CommonModel.Field.GetFieldTypeName((int)Eval("FieldType"))%>
                                                                </td>
                                                                <td align="center" style="width: 20%">
                                                                    <%# (int)Eval("FieldLevel")==0 ? "<span style='color:Green'>系统</span>" : "自定义"%>
                                                                </td>
                                                                <td align="center" style="width: 20%">
                                                                    <asp:CheckBox ID="ChkFieldPurview" runat="server"></asp:CheckBox>
                                                                    <asp:HiddenField ID="HdnFieldName" runat="server" Value='<%# Eval("FieldName") %>' />
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                                <FooterTemplate>
                                </FooterTemplate>
                            </asp:Repeater>
                        </td>
                    </tr>
                </tbody>
                <tbody id="Tabs4" runat="server" style="display: none">
                    <tr class="tdbg">
                        <td align="right" colspan="2">
                            【<a onclick="javascript:ShowTabs(0)" href='#'><span style='color: blue'>返回管理权限</span></a>】</td>
                    </tr>
                    <tr>
                        <td style="width: 100%;" colspan="2" id="Td1">
                            <!-- 显示栏目树开始 -->
                            <pe:ExtendedGridView ID="EgvNodes" runat="server" DataSourceID="OdsEgvNodes" GridLines="None"
                                Width="100%" AutoGenerateColumns="false" DataKeyNames="NodeID" OnRowDataBound="EgvNodes_RowDataBound">
                                <HeaderStyle BackColor="#449AE8" HorizontalAlign="Center" Font-Bold="True" ForeColor="White" />
                                <RowStyle CssClass="tdbg" />
                                <Columns>
                                    <pe:BoundField DataField="NodeID" HeaderText="ID">
                                        <ItemStyle Width="3%" />
                                    </pe:BoundField>
                                    <pe:TemplateField HeaderText="节点名" SortExpression="NodeName">
                                        <HeaderStyle Width="30%" />
                                        <ItemTemplate>
                                            <asp:Label ID="LabNodeShowTrees" runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </pe:TemplateField>
                                    <pe:TemplateField HeaderText="设置当前节点">
                                        <HeaderStyle Width="8%" />
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ChkCurrentNodesManage" runat="server"></asp:CheckBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </pe:TemplateField>
                                    <pe:TemplateField HeaderText="子节点管理<br/>（可以添加、修改、删除、排序子节点）">
                                        <HeaderStyle Width="20%" />
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ChkChildNodeManage" runat="server"></asp:CheckBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </pe:TemplateField>
                                </Columns>
                            </pe:ExtendedGridView>
                            <asp:ObjectDataSource ID="OdsEgvNodes" runat="server" DataObjectTypeName="EasyOne.Model.Contents.NodeInfo"
                                DeleteMethod="Delete" InsertMethod="Insert" SelectMethod="GetNodesList" TypeName="EasyOne.Contents.Nodes"
                                UpdateMethod="Update"></asp:ObjectDataSource>
                            <!-- 显示栏目树结束 -->
                        </td>
                    </tr>
                    <tr class="tdbg">
                        <td align="right" colspan="2">
                            【<a onclick="javascript:ShowTabs(0)" href='#'><span style='color: blue'>返回管理权限</span></a>】
                        </td>
                    </tr>
                </tbody>
                <tbody id="Tabs5" runat="server" style="display: none">
                    <tr class="tdbg">
                        <td align="right" colspan="2">
                            【<a onclick="javascript:ShowTabs(0)" href='#'><span style='color: blue'>返回管理权限</span></a>】</td>
                    </tr>
                    <tr>
                        <td style="width: 100%;" colspan="2" id="Td2">
                            <pe:ExtendedGridView ID="EgvNodeComments" runat="server" DataSourceID="OdsEgvNodeComments"
                                GridLines="None" Width="100%" AutoGenerateColumns="false" DataKeyNames="NodeID"
                                OnRowDataBound="EgvNodeComments_RowDataBound">
                                <HeaderStyle BackColor="#449AE8" HorizontalAlign="Center" Font-Bold="True" ForeColor="White" />
                                <RowStyle CssClass="tdbg" />
                                <Columns>
                                    <pe:BoundField DataField="NodeID" HeaderText="ID">
                                        <ItemStyle Width="5%" />
                                    </pe:BoundField>
                                    <pe:TemplateField HeaderText="节点名" SortExpression="NodeName">
                                        <HeaderStyle Width="65%" />
                                        <ItemTemplate>
                                            <asp:Label ID="LabNodeShowTreeSSS" runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </pe:TemplateField>
                                    <pe:TemplateField HeaderText="回复">
                                        <HeaderStyle Width="10%" />
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ChkNodeCommentReply" runat="server"></asp:CheckBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </pe:TemplateField>
                                    <pe:TemplateField HeaderText="审核">
                                        <HeaderStyle Width="10%" />
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ChkNodeCommentCheck" runat="server"></asp:CheckBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </pe:TemplateField>
                                    <pe:TemplateField HeaderText="管理">
                                        <HeaderStyle Width="10%" />
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ChkNodeCommentManage" runat="server"></asp:CheckBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </pe:TemplateField>
                                </Columns>
                            </pe:ExtendedGridView>
                            <asp:ObjectDataSource ID="OdsEgvNodeComments" runat="server" DataObjectTypeName="EasyOne.Model.Contents.NodeInfo"
                                DeleteMethod="Delete" InsertMethod="Insert" SelectMethod="GetNodeListExecptLinkType"
                                TypeName="EasyOne.Contents.Nodes" UpdateMethod="Update"></asp:ObjectDataSource>
                        </td>
                    </tr>
                    <tr class="tdbg">
                        <td align="right" colspan="2">
                            【<a onclick="javascript:ShowTabs(0)" href='#'><span style='color: blue'>返回管理权限</span></a>】
                        </td>
                    </tr>
                </tbody>
            </table>
        </td>
    </tr>
</table>
<br />

<script language='javascript' id="check" type="text/javascript">
        var arrTitle = new Array(<%= m_ArrTitle.ToString() %>);
        var arrTable = new Array(<%= m_ArrTable.ToString() %>);
        var arrTabs = new Array(<%= m_ArrTabs.ToString() %>);
        var arrModelTr = new Array(<%= m_ArrModelTr.ToString() %>);
        var arrSpecialTable = new Array(<%= m_ArrSpecialTable.ToString() %>);
        var arrSpecialCategoryTr = new Array(<%= m_ArrSpecialCategoryTr.ToString() %>);

        var tID=0;
        function Hidd(ID)
        {
        if(ID!=tID)
           {
               document.getElementById(arrTable[tID].toString()).style.display = "none";
               document.getElementById(arrTable[ID].toString()).style.display = "";
               document.getElementById(arrModelTr[tID].toString()).className = "tdbg";
               document.getElementById(arrModelTr[ID].toString()).className = "title";                   
               tID=ID;
            }
        }
        
        function HiddSpecial(ID)
        {
        if(ID!=tID)
           {
               document.getElementById(arrSpecialTable[tID].toString()).style.display = "none";
               document.getElementById(arrSpecialTable[ID].toString()).style.display = "";
               document.getElementById(arrSpecialCategoryTr[tID].toString()).className = "tdbg";
               document.getElementById(arrSpecialCategoryTr[ID].toString()).className = "title";                   
               tID=ID;
            }
        }
           
        function ShowTabs(ID){
            for (i=0;i< 6;i++){
                if(i == ID){
                    document.getElementById(arrTitle[i].toString()).className="titlemouseover";
                    document.getElementById(arrTabs[i].toString()).style.display="";
                }
                else{
                    document.getElementById(arrTitle[i].toString()).className="tabtitle";
                    document.getElementById(arrTabs[i].toString()).style.display="none";
                }
                
                if(ID == 1 || ID == 2){
                    document.getElementById(arrTitle[i].toString()).style.display = "none";
                }
                else
                {
                    if(i == 0 || i == 3){
                        document.getElementById(arrTitle[i].toString()).style.display = "";
                    }
                }
            }
        }
        

    function ChkNodeAll(form,nodeItem,clientId,Tabs){
		if (clientId.checked)
		{	
            var oSpanArr = document.getElementsByTagName('tbody');
            var j = oSpanArr.length;
            for ( var i=0; i<j; i++ ) 
            {
                if (oSpanArr[i].id != ""){
                    if(oSpanArr[i].id == Tabs)
                    {
                        //在这里给TD中的控件进行选择处理
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
            }
			clientId.checked = true;  
		}
	}
		
    function ChkWipeOffNodeAll(clientId){
         clientId.checked = false;
    } 
    
    function ChkSpecialAll(form,nodeItem,clientId){
		if (clientId.checked)
		{	
            var oSpanArr = document.getElementsByTagName('tbody');
            var j = oSpanArr.length;
            for ( var i=0; i<j; i++ ) 
            {
                if (oSpanArr[i].id != ""){
                    if(oSpanArr[i].id == "ctl00_CphContent_Purview_Tabs2")
                    {
                        //在这里给TD中的控件进行选择处理
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
            }
			clientId.checked = true;  
		}
	}
		
    function ChkWipeOffSpecialAll(clientId){
         clientId.checked = false;
    } 
            
    function CheckModel(obj){
    
	    var oSpanArr = document.getElementsByTagName('fieldset');
	    var j = oSpanArr.length;
		
	    //更改所有所有子节点的状态
	    for ( var i=0; i<j; i++ ) 
	    {    
		    if (oSpanArr[i].id != ""){       
			    var inputArr = oSpanArr[i].getElementsByTagName('input');
			    var m = inputArr.length
			    for ( var r=0; r< m; r++ ) 
			    {
				    var t = inputArr[r];
				    if (t.id){
				       if (t.id.substr(0,obj.id.length+1) == obj.id + '_')
				       t.checked = obj.checked;
				    }  
			    }
		    }
	    }
		    
		//开始更改父节点的状态
		if(obj.checked==true){
			CheckParentModel(obj.id);
		}
		else{
			ChangeParentModel(obj.id);
		}
	}
	
	function CheckParentModel(objID){
		if(objID.indexOf("_") > 0)
		{
		    var parentid=objID.substr(0,objID.lastIndexOf("_"));
		    document.getElementById(parentid).checked=true;
		    CheckParentModel(parentid);
		}
	}
	function ChangeParentModel(objID){
		if(objID.indexOf("_") > 0)
		{
            var oSpanArr = document.getElementsByTagName('fieldset');
		    var j = oSpanArr.length;
		    var parentid=objID.substr(0,objID.lastIndexOf("_"));
		    document.getElementById(parentid).checked = false;

		    for ( var i=0; i<j; i++ ) 
		    {    
			    if (oSpanArr[i].id != ""){       
				    var inputArr = oSpanArr[i].getElementsByTagName('input');
				    var m = inputArr.length
				    
				    for ( var r=0; r< m; r++ ) 
				    {
					    var t = inputArr[r];
					    if (t.id){
					       if (t.id.substr(0,parentid.length+1) == parentid + '_'){
						       if(t.checked == true){
							       document.getElementById(parentid).checked = true;
							       break;
						       }
						    }
					    }  
				    }
			    }
		    }
		    ChangeParentModel(parentid);
		}
	}

</script>

