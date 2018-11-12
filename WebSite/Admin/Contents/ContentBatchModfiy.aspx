<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"  EnableEventValidation="false" ValidateRequest="false" Inherits="EasyOne.WebSite.Admin.Contents.ContentBatchModfiy"
    Title="无标题页" Codebehind="ContentBatchModfiy.aspx.cs" %>

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
                批量替换信息内容
            </td>
        </tr>
        <tr valign="top" class="tdbg">
            <td style="width: 208px;">
                <strong>选择节点：</strong>
                <br />不指定节点则为所有节点
                <asp:ListBox ID="ListNode" SelectionMode="multiple" runat="server" Width="200px"
                    Height="230px"></asp:ListBox>
            </td>
            <td style="width: 208px;">
                <strong>选择模型：</strong><br />
                <asp:DropDownList ID="DropModel" AppendDataBoundItems="true" runat="server" Width="200px">
                <asp:ListItem Selected="true" Value="-1" Text="请选择模型">
                
                </asp:ListItem>
                </asp:DropDownList><br />
                <strong>选择替换字段：</strong><br />
                <asp:ListBox ID="ListModelField" Rows="2" SelectionMode="multiple" runat="server"
                    Width="200px" Height="188px"></asp:ListBox><br />
                    <pe:RequiredFieldValidator ID="ValrNodeName" runat="server" ErrorMessage="栏目名称不能为空！"
                                    ControlToValidate="ListModelField" Display="Dynamic" SetFocusOnError="True"></pe:RequiredFieldValidator>
            </td>
            <td>
                <strong>要替换的内容：</strong><br />
                <asp:TextBox ID="TxtTargetContent" TextMode="multiline" runat="server" Height="86px" Width="385px"></asp:TextBox>
                <br />
                <br />
                <strong>替换后的内容：</strong>
                <br />
                <asp:TextBox ID="TxtNewContent" TextMode="multiline" runat="server" Height="86px" Width="385px"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg">
            <td colspan="3" align="center">
                <asp:Button ID="BtnBatch" runat="server" Text="批量替换" OnClientClick="BOX_show('RegUser');" OnClick="BtnBatch_Click" /></td>
        </tr>
    </table>
    <div id="BOX_overlay" style="display:none;">
        <div id="RegUser">
            <div>
                <label><font color="#FF0000">数据正在更新中……</font></label><br />
                <img alt="" src="<%=BasePath %>admin/Images/progressbar.gif" />
            </div>
        </div>
    </div>
<script src="<%=BasePath %>admin/JS/ModalPopup.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
    //GetServices(1);
    function GetServices(value)
    {
        EasyOne.WebSite.Admin.Contents.CategoryService.GetFieldList(value,getFieldList);
    }
        function getFieldList(value)
        {
           
            var fieldList=$get("<%=ListModelField.ClientID %>");
           fieldList.innerHTML="";
            for(var i=0;i<value.length;i++)
            {
                if(value[i].FieldType==1||value[i].FieldType==2||value[i].FieldType==3||value[i].FieldType==30||value[i].FieldType==29)
                {
                    if(value[i].FieldName!=null&&value[i].FieldAlias!=null)
                    {
                    var opt=new Option();
                    opt.value=value[i].FieldName;
                    opt.text=value[i].FieldAlias;
                    fieldList.add(opt);
                    }
                }   
            }
        }
    </script>
</asp:Content>
