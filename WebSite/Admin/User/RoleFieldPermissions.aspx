<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.User.RoleFieldPermissions"
    Title="字段权限管理" Codebehind="RoleFieldPermissions.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="server">
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td colspan="2" class="spacingtitle">
                <b>角色权限设置</b>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right" style="width: 150px;">
                <strong>角色名：</strong></td>
            <td>
                <asp:Label ID="LblRoleName" runat="server" Text="" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right" style="height: 79px">
                <strong>角色描述：</strong></td>
            <td style="height: 79px">
                <asp:Label ID="LblDescription" runat="server" Text="" />
            </td>
        </tr>
    </table>
    <br />
    <table width="100%" border="0" cellpadding="5" cellspacing="1" class="border"><tr class="tdbg"><tr class="tdbg"><td align="left" class="tdbg" valign="top" style="width: 20%;"><table width="100%" border="0" cellpadding="5" cellspacing="1"><!-- 显示模型树开始 --><asp:Repeater ID="RptModelList" runat="server" DataSourceID="OdsModelList" OnItemDataBound="RptModelList1_ItemDataBound"><HeaderTemplate><tr class="title"><td align="center">模型名</td></tr></HeaderTemplate><ItemTemplate><tr id="ModelTr" runat="server"><td align="center"><pe:ExtendedLabel HtmlEncode="false"  ID="modellist" runat="server"></pe:ExtendedLabel></td></tr></ItemTemplate><FooterTemplate></FooterTemplate></asp:Repeater></table><asp:ObjectDataSource ID="OdsModelList" runat="server" SelectMethod="GetModelList"　TypeName="EasyOne.CommonModel.ModelManager"><SelectParameters><asp:Parameter DefaultValue="1" Name="modelType" Type="Int32" /><asp:Parameter DefaultValue="1" Name="showType" Type="Int32" /></SelectParameters></asp:ObjectDataSource></td><td class="tdbg" valign="top"><!-- 显示模型树开始 --><asp:Repeater ID="RptModelList2" runat="server" DataSourceID="OdsModelList" OnItemDataBound="RptModelList2_ItemDataBound"><HeaderTemplate></HeaderTemplate><ItemTemplate><table width="100%" border="0" cellpadding="5" cellspacing="1" id="model" runat="server"><tr><td align="center"><table width="100%" border="0" cellpadding="2" cellspacing="1" class="border" style="background-color: white;"><tr class="title"><td align="center" style="width: 20%; height: 18px">字段名称</td><td align="center" style="width: 20%;">字段别名</td><td align="center" style="width: 20%">字段类型</td><td align="center" style="width: 20%">字段级别</td><td align="center" style="width: 20%">禁止设置值</td></tr><asp:HiddenField ID="HdnModelId" runat="server" Value='<%# Eval("ModelId") %>' /><asp:Repeater ID="RptFieldList" runat="server"><ItemTemplate><tr class="tdbg" align="center"><td align="center" style="width: 20%; height: 22px"><%# Eval("FieldName") %></td><td align="center" style="width: 20%; height: 22px"><%# Eval("FieldAlias") %></td><td align="center" style="width: 20%"><%# EasyOne.CommonModel.Field.GetFieldTypeName((int)Eval("FieldType"))%></td><td align="center" style="width: 20%"><%# (int)Eval("FieldLevel") == 0 ? "<span style=\"color:Green\">系统</span>" : "自定义"%></td><td align="center" style="width: 20%"><asp:CheckBox ID="ChkFieldPurview" runat="server"></asp:CheckBox><asp:HiddenField ID="HdnFieldName" runat="server" Value='<%# Eval("FieldName") %>' /></td></tr></ItemTemplate></asp:Repeater>
                </table>
            </td>
        </tr>
    </table></ItemTemplate><FooterTemplate></FooterTemplate></asp:Repeater></td></tr></table>
    <br />
    <center>
        <br />
        <asp:Button ID="BtnSubmit" runat="server" Text="保存返回角色管理" OnClick="BtnSubmit_Click" />&nbsp;&nbsp;
        <asp:Button ID="BtnCancle" runat="server" Text="返回角色管理" OnClick="BtnCancle_Click" />
    </center>

    <script language="JavaScript" type="text/javascript">
    <!--
    var arrTable = new Array(<%= m_ArrTable.ToString() %>);
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
        
    function windowclose(){
        window.close();
    }
    //-->
    </script>

</asp:Content>
