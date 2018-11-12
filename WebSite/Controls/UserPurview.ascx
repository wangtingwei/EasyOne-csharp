<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Controls.UserPurview" Codebehind="UserPurview.ascx.cs" %>
<table width='100%' border='0' cellpadding='0' cellspacing='0'>
    <tr align='center'>
        <td id='TabTitle0' runat="server" class='titlemouseover' onclick='ShowTabs(0)'>
            节点权限</td>
        <td id='TabTitle1' runat="server" class='tabtitle' onclick='ShowTabs(1)'>
            专题权限</td>
        <td id='TabTitle2' runat="server" class='tabtitle' onclick='ShowTabs(2)'>
            字段权限</td>
        <td>
            &nbsp;</td>
    </tr>
</table>
<table width='100%' border='0' cellpadding='5' cellspacing='1' class='border'>
    <tr class='tdbg'>
        <td style="height: 100px;" valign='top' colspan="2">
            <table width='100%' cellpadding='2' cellspacing='1' style="background-color: white;">
                <tbody id='Tabs0' runat="server">
                    <tr>
                        <td style="width: 100%;" colspan="2" id="NodeList">
                            <!-- 显示栏目树开始 -->
                            <pe:ExtendedGridView ID="EgvNodes" runat="server" DataSourceID="OdsEgvNodes" GridLines="None"
                                Width="100%" AutoGenerateColumns="false" DataKeyNames="NodeID" OnRowDataBound="EgvNodes_RowDataBound">
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
                                    <pe:TemplateField HeaderText="浏览">
                                        <HeaderStyle Width="10%" />
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ChkNodeSkim" runat="server"></asp:CheckBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </pe:TemplateField>
                                    <pe:TemplateField HeaderText="查看">
                                        <HeaderStyle Width="10%" />
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ChkNodeShow" runat="server"></asp:CheckBox>
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
                                </Columns>
                            </pe:ExtendedGridView>
                            <asp:ObjectDataSource ID="OdsEgvNodes" runat="server" DataObjectTypeName="EasyOne.Model.Contents.NodeInfo"
                                SelectMethod="GetNodeListExecptLinkType" TypeName="EasyOne.Contents.Nodes"></asp:ObjectDataSource>
                            <!-- 显示栏目树结束 -->
                        </td>
                    </tr>
                    <tr class='tdbg'>
                        <td class='tdbgleft'>
                            <strong>浏览：</strong>用户可以查看指定节点下的文章列表，不可以看到内容
                        </td>
                    </tr>
                    <tr class='tdbg'>
                        <td class='tdbgleft'>
                            <strong>查看：</strong>允许查看该节点下发布的任何信息。
                        </td>
                    </tr>
                    <tr class='tdbg'>
                        <td class='tdbgleft'>
                            <strong>录入：</strong>可以向该节点录入信息。
                        </td>
                    </tr>
                </tbody>
                <tbody id="Tabs1" runat="server" style="display: none">
                    <tr class="tdbg">
                        <td align="left" class="tdbg" colspan="2" valign="top">
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
                                            <asp:HiddenField ID="HdnSpecialId" runat="server" />
                                        </ItemTemplate>
                                    </pe:TemplateField>
                                </Columns>
                            </pe:ExtendedGridView>
                            <asp:ObjectDataSource ID="OdsSpecial" runat="server" SelectMethod="GetSpecialTree"
                                TypeName="EasyOne.Contents.Special"></asp:ObjectDataSource>
                        </td>
                    </tr>
                    <tr class='tdbg'>
                        <td class='tdbgleft'>
                            <strong>专题信息录入：</strong>可以对指定的专题有录入内容的操作权限。
                        </td>
                    </tr>
                </tbody>
                <tbody id='Tabs2' runat="server" style="display: none">
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
            </table>
        </td>
    </tr>
</table>

<script language='javascript' id="check" type="text/javascript">
        var arrTable = new Array(<%= m_ArrTable.ToString() %>);
        var arrTitle = new Array(<%= m_ArrTitle.ToString() %>);
        var arrTabs = new Array(<%= m_ArrTabs.ToString() %>);
        var arrModelTr = new Array(<%= m_ArrModelTr.ToString() %>);

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
        

function chkall(input1,input2,chkName)
{
    var objForm = document.forms[input1];
    var objLen = objForm.length;
    for (var iCount = 0; iCount < objLen; iCount++)
    {
        var s=objForm.elements[iCount].name;
      
        if (input2.checked == true)
        { 
            if (objForm.elements[iCount].type == "checkbox"&&(s.substr(s.length-chkName.length,chkName.length)==chkName))
            {
                objForm.elements[iCount].checked = true;
            }
        }
        else
        {
            if (objForm.elements[iCount].type == "checkbox"&&(s.substr(s.length-chkName.length,chkName.length)==chkName))
            {
                objForm.elements[iCount].checked = false;
            }
        }
    }
}       

   function ChkWipeOffNodeAll(clientId){
             clientId.checked = false;
        } 
       

 function ChkNodeAll(form,nodeItem,clientId){
			if (clientId.checked)
			{	
                var oSpanArr = document.getElementsByTagName('tbody');
                var j = oSpanArr.length;
                for ( var i=0; i<j; i++ ) 
                {
                    if (oSpanArr[i].id != ""){
                        if(oSpanArr[i].id == "ctl00_CphContent_UserPurview_Tabs0")
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
           
           function ChkSpecialAll(form,nodeItem,clientId){
		if (clientId.checked)
		{	
            var oSpanArr = document.getElementsByTagName('tbody');
            var j = oSpanArr.length;
            for ( var i=0; i<j; i++ ) 
            {
                if (oSpanArr[i].id != ""){
                    if(oSpanArr[i].id == "ctl00_CphContent_UserPurview_Tabs1")
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
        
function ShowTabs(ID){
    for (i=0;i< 3;i++){
        if(i == ID){
            document.getElementById(arrTitle[i].toString()).className="titlemouseover";
            document.getElementById(arrTabs[i].toString()).style.display="";
        }
        else{
            document.getElementById(arrTitle[i].toString()).className="tabtitle";
            document.getElementById(arrTabs[i].toString()).style.display="none";
        }
    }
}
</script>

