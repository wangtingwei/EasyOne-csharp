<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Collection.ConfigStep3"
    MasterPageFile="~/Admin/MasterPage.master" Title="采集配置第三步" ValidateRequest="false" Codebehind="ConfigStep3.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td colspan="2" class="spacingtitle">
                <b>内容页采集设置</b>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" style="width: 15%" align="right">
                <strong>所属采集项目：</strong></td>
            <td>
                <asp:Label ID="LblItemName" runat="server" Text="" />
            </td>
        </tr>
    </table>
    <br />
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <asp:Repeater ID="RptModelList" runat="server" OnItemDataBound="RptModelList_ItemDataBound">
            <HeaderTemplate>
            </HeaderTemplate>
            <ItemTemplate>
                <asp:Label ID="LblList" runat="server" Text=""></asp:Label>
                <tr class="tdbg" onmouseout="this.className='tdbg'" onmouseover="this.className='tdbgmouseover'">
                    <td class="tdbgleft" style="width: 120px;">
                        <asp:Label ID="LblFieldName" runat="server" Text=""></asp:Label>
                    </td>
                    <td align="left">
                        <asp:RadioButton ID="RadDefault" runat="server" GroupName="" Checked="true" />
                        使用字段默认值
                        <asp:RadioButton ID="RadDesignated" runat="server" />
                        使用指定值
                        <asp:TextBox ID="TxtDesignated" runat="server" Width="200px" MaxLength ="100"></asp:TextBox>                           
                        <asp:DropDownList ID="DropStatusType" runat="server" Visible  ="false"></asp:DropDownList>  
                        <asp:RadioButton ID="RadSet" runat="server" />
 
                        使用采集规则
                        <pe:ExtendedLabel HtmlEncode="false" ID="LblSetField"  runat="server" Text=""></pe:ExtendedLabel>
                        <asp:HiddenField ID="HdnFieldName" runat="server" />
                        <asp:HiddenField ID="HdnFieldType" runat="server" />
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
            </FooterTemplate>
        </asp:Repeater>
    </table>
                            
    <br />
    <center>
        <asp:HiddenField ID="HdnAction" runat="server" />
        <asp:Button ID="BtnCancel1" runat="server" Text="上一步" OnClick="BtnCancel1_Click"  Visible ="false" ValidationGroup = "BtnCancel1"/>&nbsp;&nbsp;<asp:Button
                ID="BtnSubmit" Text="下一步" OnClick="EBtnSubmit_Click" runat="server" />&nbsp;&nbsp;
                <asp:Button ID="BtnCancel2" Text="返回采集管理" OnClick="BtnCancel2_Click" runat="server" />
    </center>    
    <script language="javascript" type="text/javascript"> 
    <!-- 
        function SetField(modelId,itemId,fieldName,fieldType,fieldAlias) { window.open ("FieldRule.aspx?ModelId=" + modelId + "&ItemId=" + itemId + "&FieldType=" + fieldType + "&FieldName=" + fieldName + "&FieldAlias=" + fieldAlias,"newwindow", "height=630px, width=900px, toolbar=no, menubar=no, scrollbars=yes, resizable=no, location=no, status=no") } 
    //--> 
    </script>

</asp:Content>
