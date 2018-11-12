<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" EnableEventValidation="false"  ValidateRequest="false" Inherits="EasyOne.WebSite.Admin.Contents.ContentAdvancedSearch"
    Title="内容高级查询" Codebehind="ContentAdvancedSearch.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="server">
    <asp:ScriptManager ID="ScriptManageContent" runat="server">
        <Services>
            <asp:ServiceReference Path="../../WebServices/CategoryService.asmx" />
        </Services>
    </asp:ScriptManager>
    <table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td colspan="3" class="spacingtitle">
                内容高级查询
            </td>
        </tr>
        <tr valign="top" class="tdbg">
            <td style="width: 208px;">
                <strong>选择节点：</strong>
                <br />
                不指定节点则为所有节点
                <asp:ListBox ID="ListNode" SelectionMode="multiple" runat="server" Width="200px"
                    Height="230px"></asp:ListBox>
                <pe:RequiredFieldValidator ID="ValrNodeName" runat="server" ErrorMessage="栏目名称不能为空！"
                    ControlToValidate="ListModelField" Display="Dynamic" SetFocusOnError="True"></pe:RequiredFieldValidator>
            </td>
            <td style="width: 208px;">
                <strong>选择模型：</strong><br />
                <asp:DropDownList ID="DropModel" AppendDataBoundItems="true" runat="server" Width="200px">
                    <asp:ListItem Selected="true" Value="-1" Text="请选择模型">
                
                    </asp:ListItem>
                </asp:DropDownList>
                <br />
                <strong>选择查询字段：</strong><br />
                <asp:ListBox ID="ListModelField" Rows="2" SelectionMode="multiple" runat="server"
                    Width="200px" Height="188px"></asp:ListBox>
                <br /><br />
                <input value ="增加查询字段" type ="button" onclick="AddSearchField(aspnetForm.<%=ListModelField.ClientID %>)" />
            </td>
            <td id ="searchValueList">
            </td>
        </tr>
        <tr class="tdbg">
            <td colspan="3" align="center" >
                <asp:HiddenField ID="HdnSearchValue" runat="server" />
                <asp:Button ID="BtnAdvancedSearch" runat="server" Text="开始查询" OnClick="BtnAdvancedSearch_Click" OnClientClick ="return BtnAdvancedSearch()" />
            </td>
        </tr>
    </table>

    <script language="javascript" type="text/javascript">
        var loadModel = false;
        
        function GetServices(value)
        {
            EasyOne.WebSite.Admin.Contents.CategoryService.GetFieldList(value,getFieldList);  //这里 WebServices 在加载下拉框时间不固定，在火狐浏览器下难以获取 火狐浏览器在WebServices 延迟函数很多失效 不得不采用全局变量解决此问题。
            $get("<%=HdnSearchValue.ClientID %>").value= "";
            loadModel = false;
        }
        function getFieldList(value)
        {
           var fieldList=$get("<%=ListModelField.ClientID %>");
           fieldList.innerHTML="";
            for(var i=0;i<value.length;i++)
            {
                if(value[i].FieldName!=null&&value[i].FieldAlias!=null)
                {                
                    fieldList.options[fieldList.options.length] = new Option(value[i].FieldAlias, value[i].FieldName, 0, 0);
                }
            }
        }

        function createField(ItemList)
        {
            var searchValue = "";
            var searchName = "";
                
            for(var x=0;x < ItemList.length;x++)
            {
                var opt = ItemList.options[x];
                if (searchValue == ""){
                    searchValue = opt.value;
                    searchName = opt.text;
                }
                else
                {
                    searchValue += "," + opt.value;
                    searchName += "," + opt.text;
                }
            }

            if (searchValue == ""){
                return;
            }
            var searchinnetHtml = " <table style=\"width: 100%;\" cellpadding=\"2\" cellspacing=\"1\" class=\"border\">";

            if (searchValue.indexOf(',') > 0){
                var arrSearchValue =  searchValue.split(',');
                var arrSearchName =  searchName.split(',');
                for (var i=0;i < arrSearchValue.length;i++){
                    searchinnetHtml += "<tr class ='tdbg' id = 'h_"+ arrSearchValue[i] +"' style='display:none;'><td class='tdbgleft' width='25%'>" + arrSearchName[i] + " 查询值： </td><td class='tdbg' ><input size='30' maxlength='30' type='text' name= '" + arrSearchValue[i] +"'/> <a href='#' onclick=\"JavaScript:delItem('h_" + arrSearchValue[i] + "','" + arrSearchName[i] +"')\"><img src=\"<%= BasePath%>admin/Images/Del.gif\"  BORDER='0'   ALT='删除" + arrSearchName[i] +"' /></a></td></tr>";
                }
            }
            else 
            {
                searchinnetHtml += "<tr class ='tdbg' id = 'h_"+ searchValue +"' style='display:none;'><td class='tdbgleft' width='25%'>" + searchName + " 查询值： </td><td class='tdbg'><input size='30' maxlength='30' type='text' name= '" + searchValue +"'/> <a href='#' onclick=\"JavaScript:delItem('h_" + searchValue + "','" + searchName +"')\"><img src=\"<%= BasePath%>admin/Images/Del.gif\"  BORDER='0'   ALT='删除" + searchName +"' /></a></td></tr>";
            }

            searchinnetHtml += "</table>";
            $get("searchValueList").innerHTML= searchinnetHtml;      
        }
        
        function AddSearchField(ItemList){            
            if (!loadModel) {
                createField($get("<%=ListModelField.ClientID %>"));
                loadModel = true;
            }
            
            var hdnSearchValue =  $get("<%=HdnSearchValue.ClientID %>").value;
            for(var x=ItemList.length-1;x>=0;x--)
            {
                var opt = ItemList.options[x];
                if (opt.selected)
                {
                    $get("h_" + opt.value).style.display="";
                    if (hdnSearchValue != ""){
                        hdnSearchValue += "," + opt.value;
                    }else{
                        hdnSearchValue = opt.value;
                    }
                    ItemList.options[x] = null;
                }
            }
            
            $get("<%=HdnSearchValue.ClientID %>").value= hdnSearchValue;
        }
        
        function delItem(value,name){
            var searchValue = "";
            var iSearchValue = value.replace('h_','');
            var hSearchValue = $get("<%=HdnSearchValue.ClientID %>").value;
            
            if (hSearchValue == ""){
                return;
            } 
            
            if (hSearchValue.indexOf(',') > 0){
                arrSearchValue = hSearchValue.split(',');
                for (var i= 0;i < arrSearchValue.length; i++) {
                    if ( iSearchValue != arrSearchValue[i]){
                        if (searchValue == "") {
                            searchValue = arrSearchValue[i];
                        }
                        else{
                            searchValue += "," + arrSearchValue[i];
                        }
                    }
                }
            }
            else
            {
                if (iSearchValue != hSearchValue){
                    searchValue = hSearchValue;
                }
            }
            
            var fieldList=$get("<%=ListModelField.ClientID %>");
            fieldList.options[fieldList.options.length] = new Option(name, iSearchValue, 0, 0);
            $get(value).style.display="none";
            $get("<%=HdnSearchValue.ClientID %>").value = searchValue; 
        }

        function BtnAdvancedSearch(){
            var isSelectedNodeId = "";
            var nodeList=$get("<%=ListNode.ClientID %>");
            
            for(var x=nodeList.length-1;x>=0;x--)
            {
	            var opt = nodeList.options[x];
	            if (opt.selected)
	            {
	               isSelectedNodeId= "true";
	               break;
	            }
            }
            
            if (isSelectedNodeId == ""){
	            alert("没有选择要查询的节点！");
	            return false ;
            }      
            
            var searchValue = $get("<%=HdnSearchValue.ClientID %>").value;
            if (searchValue == ""){
	            alert("没有选择要查询的字段！");
	            return false ;
            }
            if (searchValue.indexOf(",") > 0 ) {
                var arrSearchValue = searchValue.split(',');
                for (var i = 0; i < arrSearchValue.length; i++){
                    if ($get(arrSearchValue[i]).value == ""){
                        alert(arrSearchValue[i] + " 字段查询值不能为空！");
                        return false ;
                    }
                }
            }
            else
            {
                if ($get(searchValue).value == ""){
                    alert(searchValue + " 字段查询值不能为空！");
                    return false ;
                }
            }
        }
    </script>

</asp:Content>
