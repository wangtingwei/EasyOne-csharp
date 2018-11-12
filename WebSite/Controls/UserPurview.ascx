<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Controls.UserPurview" Codebehind="UserPurview.ascx.cs" %>
<table width='100%' border='0' cellpadding='0' cellspacing='0'>
    <tr align='center'>
        <td id='TabTitle0' runat="server" class='titlemouseover' onclick='ShowTabs(0)'>
            �ڵ�Ȩ��</td>
        <td id='TabTitle1' runat="server" class='tabtitle' onclick='ShowTabs(1)'>
            ר��Ȩ��</td>
        <td id='TabTitle2' runat="server" class='tabtitle' onclick='ShowTabs(2)'>
            �ֶ�Ȩ��</td>
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
                            <!-- ��ʾ��Ŀ����ʼ -->
                            <pe:ExtendedGridView ID="EgvNodes" runat="server" DataSourceID="OdsEgvNodes" GridLines="None"
                                Width="100%" AutoGenerateColumns="false" DataKeyNames="NodeID" OnRowDataBound="EgvNodes_RowDataBound">
                                <HeaderStyle BackColor="#449AE8" HorizontalAlign="Center" Font-Bold="True" ForeColor="White" />
                                <RowStyle CssClass="tdbg" />
                                <Columns>
                                    <pe:BoundField DataField="NodeID" HeaderText="ID">
                                        <ItemStyle Width="10%" />
                                    </pe:BoundField>
                                    <pe:TemplateField HeaderText="�ڵ���" SortExpression="NodeName">
                                        <HeaderStyle Width="30%" />
                                        <ItemTemplate>
                                            <asp:Label ID="LabNodeShowTree" runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </pe:TemplateField>
                                    <pe:TemplateField HeaderText="���">
                                        <HeaderStyle Width="10%" />
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ChkNodeSkim" runat="server"></asp:CheckBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </pe:TemplateField>
                                    <pe:TemplateField HeaderText="�鿴">
                                        <HeaderStyle Width="10%" />
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ChkNodeShow" runat="server"></asp:CheckBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </pe:TemplateField>
                                    <pe:TemplateField HeaderText="¼��">
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
                            <!-- ��ʾ��Ŀ������ -->
                        </td>
                    </tr>
                    <tr class='tdbg'>
                        <td class='tdbgleft'>
                            <strong>�����</strong>�û����Բ鿴ָ���ڵ��µ������б������Կ�������
                        </td>
                    </tr>
                    <tr class='tdbg'>
                        <td class='tdbgleft'>
                            <strong>�鿴��</strong>����鿴�ýڵ��·������κ���Ϣ��
                        </td>
                    </tr>
                    <tr class='tdbg'>
                        <td class='tdbgleft'>
                            <strong>¼�룺</strong>������ýڵ�¼����Ϣ��
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
                                    <pe:BoundField DataField="Id" HeaderText="���" SortExpression="Id" />
                                    <pe:TemplateField HeaderText="ר����" SortExpression="Name">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="LabName"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </pe:TemplateField>
                                    <pe:TemplateField HeaderText="������ݵ�ר��" SortExpression="TreeLineType">
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
                            <strong>ר����Ϣ¼�룺</strong>���Զ�ָ����ר����¼�����ݵĲ���Ȩ�ޡ�
                        </td>
                    </tr>
                </tbody>
                <tbody id='Tabs2' runat="server" style="display: none">
                    <tr class='tdbg'>
                        <td align="left" class="tdbg" valign="top" style="width: 20%;">
                            <table width='100%' border='0' cellpadding='5' cellspacing='1'>
                                <!-- ��ʾģ������ʼ -->
                                <asp:Repeater ID="RptModelList" runat="server" DataSourceID="OdsModelList" OnItemDataBound="RptModelList1_ItemDataBound">
                                    <HeaderTemplate>
                                        <tr class='title'>
                                            <td align="center">
                                                ģ����</td>
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
                            <!-- ��ʾģ������ʼ -->
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
                                                            �ֶ�����
                                                        </td>
                                                        <td align="center" style="width: 20%;">
                                                            �ֶα���
                                                        </td>
                                                        <td align="center" style="width: 20%">
                                                            �ֶμ���
                                                        </td>
                                                        <td align="center" style="width: 20%">
                                                            ��ֹ����ֵ
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
                                                                    <%# (int)Eval("FieldLevel")==0 ? "<span style='color:Green'>ϵͳ</span>" : "�Զ���"%>
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
                            //�������TD�еĿؼ�����ѡ����
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
                        //�������TD�еĿؼ�����ѡ����
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

