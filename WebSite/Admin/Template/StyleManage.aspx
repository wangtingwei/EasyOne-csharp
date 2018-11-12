<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPage.master"
    Title="风格管理" Inherits="EasyOne.WebSite.Admin.Template.StyleManage" Codebehind="StyleManage.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <asp:ScriptManager ID="SmStyleManage" runat="server">
    </asp:ScriptManager>

    <script language="javascript" type="text/javascript">
          function ShowTabs(ID){
               for (i=0;i< 2;i++){
                    if(i == ID){
                        document.getElementById("TabTitle" + i).className="titlemouseover";
                        document.getElementById("Tabs" + i).style.display="";
                    }
                    else{
                        document.getElementById("TabTitle" + i).className="tabtitle";
                        document.getElementById("Tabs" + i).style.display="none";
                    }
               }
          } 
    </script>

    <table width="100%">
        <tr>
            <td>
                当前目录：<asp:Label ID="LblCurrentDir" runat="server"></asp:Label>
            </td>
            <td align="right">
                <pe:ExtendedLiteral HtmlEncode="false" ID="LitParentDirLink" runat="server"></pe:ExtendedLiteral></td>
        </tr>
    </table>
    <asp:Literal ID="LitMessageText" runat="server" Visible="false"></asp:Literal>
    <pe:ExtendedGridView ID="EgvFiles" runat="server" SerialText="" AutoGenerateColumns="False"
        AutoGenerateCheckBoxColumn="True" DataKeyNames="Name" OnRowCommand="EgvFiles_RowCommand"
        CheckBoxFieldHeaderWidth="3%" OnRowDataBound="EgvFiles_RowDataBound">
        <Columns>
            <pe:TemplateField HeaderText="名称">
                <ItemStyle HorizontalAlign="Left" />
                <ItemTemplate>
                    <img alt="" src=' <%# System.Convert.ToInt32(Eval("type")) == 1 ? "../../Admin/Images/Node/closefolder.gif" :"../../Admin/Images/Node/singlepage.gif" %>' />
                    <%# System.Convert.ToInt32(Eval("type")) == 1 ? "<a href=\"StyleManage.aspx?Dir=" + Server.UrlEncode(Request.QueryString["Dir"] + "/" + Eval("Name").ToString()) + "\">" + Eval("Name").ToString() + "</a>" : (IsEdit(Eval("type").ToString(), Eval("content_type").ToString()) ? "<a href=\"StyleSheets.aspx?Action=Modify&Dir=" + Server.UrlEncode(Request.QueryString["Dir"] + "/" + Eval("Name").ToString()) + "\">" + Eval("Name").ToString() + "</a>" : Eval("Name").ToString())%>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="大小">
                <HeaderStyle Width="60px" />
                <ItemTemplate>
                    <%# GetSize(Eval("size").ToString()) %>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Right" />
            </pe:TemplateField>
            <pe:TemplateField HeaderText="类型">
                <HeaderStyle Width="60px" />
                <ItemTemplate>
                    <asp:HiddenField ID="HdnFileType" Value='<%#Eval("type") %>' runat="server" />
                    <%# System.Convert.ToInt32(Eval("type")) == 1 ? "文件夹" : Eval("content_type").ToString() + "文件" %>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:BoundField DataField="lastWriteTime" HeaderText="最后修改时间" SortExpression="lastWriteTime"
                DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HtmlEncode="False">
                <HeaderStyle Width="120px" />
            </pe:BoundField>
            <pe:TemplateField HeaderText="操作">
                <HeaderStyle Width="150px" />
                <ItemTemplate>
                    <pe:ExtendedAnchor ID="EahTemplateEdit" IsChecked="true" OperateCode="TemplateManage"
                        href='<%# "StyleSheets.aspx?Action=Modify&Dir="+ Server.UrlEncode(Request.QueryString["Dir"] +"/"+ Eval("Name").ToString()) %>'
                        runat="server" visible='<%# IsEdit(Eval("type").ToString(),Eval("content_type").ToString()) %>'>编辑</pe:ExtendedAnchor>
                    <pe:ExtendedLinkButton ID="ELbtnDel" Text="删除" IsChecked="true" OperateCode="TemplateManage"
                        runat="server" CommandArgument='<%# Eval("Name").ToString()%>' CommandName='<%# System.Convert.ToInt32(Eval("type")) == 1 ? "DelDir":"DelFiles" %>'
                        OnClientClick="return confirm('确定要删除此文件夹或文件吗？');" />
                    <pe:ExtendedAnchor ID="EahReName" IsChecked="true" OperateCode="TemplateManage" href="#"
                        onclick='<%# "ReName(\"" + Eval("type").ToString()+"\",\""+ Eval("Name").ToString().Replace("\\","\\\\") + "\");"%>'
                        runat="server">重命名</pe:ExtendedAnchor>
                    <pe:ExtendedLinkButton ID="ELbtnCopy" Text="复制" IsChecked="true" OperateCode="TemplateManage"
                        runat="server" CommandName='<%# System.Convert.ToInt32(Eval("type")) == 1 ? "CopyDir":"CopyFiles" %>'
                        CommandArgument='<%# Eval("Name").ToString()%>' />
                </ItemTemplate>
            </pe:TemplateField>
        </Columns>
    </pe:ExtendedGridView>
    <table border="0" align="center" cellpadding="2" cellspacing="0">
        <tr>
            <td align="center">
                <pe:AspNetPager ID="Pager" PageSize="20" runat="server" OnPageChanged="Pager_PageChanged">
                </pe:AspNetPager>
            </td>
        </tr>
    </table>
    <br />
    <asp:Panel ID="PanButton" runat="server">
        <input id="ChkAll" onclick="javascript:CheckAll(this);" type="checkbox" /><label
            for="ChkAll">选中所有</label>
        <pe:ExtendedButton ID="EBtnBatchDel" OnClientClick="return confirm('确定要删除选中的文件夹和文件吗？');"
            IsChecked="true" OperateCode="TemplateManage" runat="server" Text="删除选中的文件或文件夹"
            OnClick="EBtnBatchDel_Click" CausesValidation="False" />
        <pe:ExtendedButton ID="EBtnCreateTemplate" runat="server" Text="新建风格" IsChecked="true"
            OperateCode="TemplateManage" OnClick="EBtnCreateTemplate_Click" />
        <input id="InputUploadTemplate" type="button" class="inputbutton" value="上传风格" onclick="javascript:window.open('StyleSheetsUpload.aspx?Dir=<%= Server.UrlEncode(Request.QueryString["Dir"]) %>','上传风格','width=600,height=450,resizable=0,scrollbars=yes');" />
        <input id="InputNewDir" type="button" class="inputbutton" value="新建目录" onclick="CreateDir()" />
        <pe:ExtendedLiteral HtmlEncode="false" ID="LitParentDirButton" runat="server"></pe:ExtendedLiteral>
        <asp:DropDownList ID="DrpMove" runat="server">
            <asp:ListItem Text="请选择目标文件夹" Value=""></asp:ListItem>
            <asp:ListItem Text="/" Value="/"></asp:ListItem>
        </asp:DropDownList><asp:Button ID="BtnMove" runat="server" Text="移动" OnClick="BtnMove_Click" /><br />
         <span style="color:Blue">注：对CSS文件的删除移动操作，请手动更新模板中的引用。</span>
    </asp:Panel>
    <asp:HiddenField ID="HdnType" runat="server" />
    <asp:HiddenField ID="HdnName" runat="server" />
    <asp:Panel ID="PnlFileRename" runat="server" Style="width: 300px; display: none;
        background-color: Gray; text-align: center; padding-top: 1px;">
        <table width="100%" border="0" cellpadding="1" cellspacing="1" class="border">
            <tr>
                <td class="spacingtitle">
                    重命风格文件名
                </td>
            </tr>
            <tr>
                <td class="tdbgleft">
                    <asp:TextBox ID="TxtFileName" ValidationGroup="TxtFileName" Width="290px" runat="server"></asp:TextBox>
                    <pe:RequiredFieldValidator ID="ValrTxtFileName" runat="server" ErrorMessage="风格名不能为空"
                        Display="Dynamic" ControlToValidate="TxtFileName" ValidationGroup="TxtFileName"></pe:RequiredFieldValidator><asp:RegularExpressionValidator
                            ID="ValeTxtFileName" ValidationGroup="TxtFileName" ControlToValidate="TxtFileName"
                            Display="Dynamic" runat="server" ErrorMessage='风格格式应为***.css，不能包含\/:*?"<>|.和空格等字符！'></asp:RegularExpressionValidator>
                    <br />
                    <pe:ExtendedButton ID="EBtnFileReName" runat="server" ValidationGroup="TxtFileName"
                        IsChecked="true" OperateCode="TemplateManage" Text="重命风格文件名" OnClick="EBtnModifyName_Click" /><asp:Button
                            ID="BtnHiddenFileRename" Text="取消" runat="server" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="PnlDirRename" runat="server" Style="width: 300px; display: none; background-color: Gray;
        text-align: center; padding-top: 1px;">
        <table width="100%" border="0" cellpadding="1" cellspacing="1" class="border">
            <tr>
                <td class="spacingtitle">
                    重命目录名
                </td>
            </tr>
            <tr>
                <td class="tdbgleft">
                    <asp:TextBox ID="TxtDirName" ValidationGroup="TxtDirName" runat="server" Width="290px"></asp:TextBox>
                    <pe:RequiredFieldValidator ID="ValrTxtDirName" runat="server" ErrorMessage="目录名不能为空"
                        Display="Dynamic" ControlToValidate="TxtDirName" ValidationGroup="TxtDirName"></pe:RequiredFieldValidator><asp:RegularExpressionValidator
                            ID="ValeTxtDirName" ValidationGroup="TxtDirName" Display="Dynamic" ControlToValidate="TxtDirName"
                            runat="server" ErrorMessage='目录格式不正确，不能包含\/:*?"<>|.和空格等字符！'></asp:RegularExpressionValidator>
                    <br />
                    <pe:ExtendedButton ID="EBtnDirReName" runat="server" ValidationGroup="TxtDirName"
                        IsChecked="true" OperateCode="TemplateManage" Text="重命目录名" OnClick="EBtnModifyName_Click" /><asp:Button
                            ID="BtnHiddenDirRename" Text="取消" runat="server" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="PnlCopyDir" runat="server" Style="width: 300px; display: none; background-color: Gray;
        text-align: center; padding-top: 1px;">
        <table width="100%" border="0" cellpadding="1" cellspacing="1" class="border">
            <tr>
                <td class="spacingtitle">
                    复制文件夹
                </td>
            </tr>
            <tr>
                <td class="tdbgleft">
                    复制目标文件夹<asp:Label ForeColor="red" ID="LblCopyDir" runat="server"></asp:Label>已经存在了，请重命名，如果不重命名，则覆盖现有文件夹
                    <asp:TextBox ID="TxtCopyDir" ValidationGroup="TxtCopyDir" runat="server"></asp:TextBox>
                    <pe:RequiredFieldValidator ID="ValrTxtCopyDir" runat="server" ErrorMessage="目录名不能为空"
                        Display="Dynamic" ControlToValidate="TxtCopyDir" ValidationGroup="TxtCopyDir"></pe:RequiredFieldValidator><asp:RegularExpressionValidator
                            ID="ValeTxtCopyDir" ValidationGroup="TxtCopyDir" Display="Dynamic" ControlToValidate="TxtCopyDir"
                            runat="server" ErrorMessage='目录格式不正确，不能包含\/:*?"<>|.和空格等字符！'></asp:RegularExpressionValidator>
                    <br />
                    <pe:ExtendedButton ID="EBtnCopyDir" runat="server" IsChecked="true" OperateCode="TemplateManage"
                        ValidationGroup="TxtCopyDir" Text="复制" OnClick="EBtnCopyDir_Click" /><asp:Button
                            ID="BtnHiddenCopyDir" runat="server" Text="取消" />
                    <asp:HiddenField ID="HdnCopyDir" runat="server" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="PnlCopyFile" runat="server" Style="width: 300px; display: none; background-color: Gray;
        text-align: center; padding-top: 1px;">
        <table width="100%" border="0" cellpadding="1" cellspacing="1" class="border">
            <tr>
                <td class="spacingtitle">
                    复制文件
                </td>
            </tr>
            <tr>
                <td class="tdbgleft">
                    复制目标文件<asp:Label ForeColor="red" ID="LblCopyFile" runat="server"></asp:Label>已经存在了，请重命名，如果不重命名，则覆盖现有文件
                    <asp:TextBox ID="TxtCopyFile" ValidationGroup="TxtCopyFile" runat="server"></asp:TextBox>
                    <pe:RequiredFieldValidator ID="ValrTxtCopyFile" runat="server" ErrorMessage="文件名不能为空"
                        Display="Dynamic" ControlToValidate="TxtCopyFile" ValidationGroup="TxtCopyFile"></pe:RequiredFieldValidator><asp:RegularExpressionValidator
                            ID="ValeTxtCopyFile" ValidationGroup="TxtCopyFile" Display="Dynamic" ControlToValidate="TxtCopyFile"
                            runat="server" ErrorMessage='文件格式应为***.css，不能包含\/:*?"<>|.和空格等字符！'></asp:RegularExpressionValidator>
                    <br />
                    <pe:ExtendedButton ID="EBtnCopyFile" runat="server" IsChecked="true" OperateCode="TemplateManage"
                        ValidationGroup="TxtCopyFile" Text="复制" OnClick="EBtnCopyFile_Click" /><asp:Button
                            ID="BtnHiddenCopyFile" runat="server" Text="取消" />
                    <asp:HiddenField ID="HdnCopyFile" runat="server" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="PnlCreateDir" runat="server" Style="width: 300px; display: none; background-color: Gray;
        text-align: center; padding-top: 1px;">
        <table width="100%" border="0" cellpadding="1" cellspacing="1" class="border">
            <tr>
                <td class="spacingtitle">
                    创建目录
                </td>
            </tr>
            <tr>
                <td class="tdbgleft">
                    目录名：
                    <asp:TextBox ID="TxtNewDir" runat="server" ValidationGroup="NewDir"></asp:TextBox><pe:RequiredFieldValidator
                        ID="ValrTxtNewDir" ControlToValidate="TxtNewDir" ValidationGroup="NewDir" Display="Dynamic"
                        runat="server" ErrorMessage="文件目录名不能为空！"></pe:RequiredFieldValidator><asp:RegularExpressionValidator
                            ID="ValeTxtNewDir" runat="server" ErrorMessage='目录名格式不正确，不能包含\/:*?"<>|.和空格等字符！' ControlToValidate="TxtNewDir"
                            ValidationGroup="NewDir" Display="Dynamic"></asp:RegularExpressionValidator><br />
                    <pe:ExtendedButton ID="EBtnNewDir" runat="server" IsChecked="true" OperateCode="TemplateManage"
                        Text="执行" ValidationGroup="NewDir" OnClick="EBtnNewDir_Click" /><asp:Button ID="BtnHiddenCreateDir"
                            Text="取消" runat="server" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:HiddenField ID="HdnStyleManage" runat="server" />
    <ajaxToolkit:ModalPopupExtender ID="MpeFileRename" runat="server" TargetControlID="HdnStyleManage"
        Y="60" PopupControlID="PnlFileRename" CancelControlID="BtnHiddenFileRename" BackgroundCssClass="modalBackground"
        DropShadow="true" Drag="true" />
    <ajaxToolkit:ModalPopupExtender ID="MpeDirRename" runat="server" TargetControlID="HdnStyleManage"
        Y="60" PopupControlID="PnlDirRename" CancelControlID="BtnHiddenDirRename" BackgroundCssClass="modalBackground"
        DropShadow="true" Drag="true" />
    <ajaxToolkit:ModalPopupExtender ID="MpeCreateDir" runat="server" TargetControlID="HdnStyleManage"
        Y="60" PopupControlID="PnlCreateDir" CancelControlID="BtnHiddenCreateDir" BackgroundCssClass="modalBackground"
        DropShadow="true" Drag="true" />
    <ajaxToolkit:ModalPopupExtender ID="MpeCopyDir" runat="server" TargetControlID="HdnStyleManage"
        Y="60" PopupControlID="PnlCopyDir" CancelControlID="BtnHiddenCopyDir" BackgroundCssClass="modalBackground"
        DropShadow="true" Drag="true" />
    <ajaxToolkit:ModalPopupExtender ID="MpeCopyFile" runat="server" TargetControlID="HdnStyleManage"
        Y="60" PopupControlID="PnlCopyFile" CancelControlID="BtnHiddenCopyFile" BackgroundCssClass="modalBackground"
        DropShadow="true" Drag="true" />

    <script language="javascript" type="text/javascript">
    <!--
    function ShowCopyDir()
    {
    var copyDirPopup = $find('<%= MpeCopyDir.ClientID %>');	
            copyDirPopup.show();
    }
    
    function ShowCopyFile()
    {
    var copyFilePopup = $find('<%= MpeCopyFile.ClientID %>');	
            copyFilePopup.show();
    }
    
    
    function CreateDir()
    {
        var createDirPopup = $find('<%= MpeCreateDir.ClientID %>');	
            createDirPopup.show();
    }
    
    function ReName(type,name)
    {
        var txtFileName = "<%= TxtFileName.ClientID %>";
        var txtDirName = "<%= TxtDirName.ClientID %>";
        document.getElementById('<%= HdnName.ClientID %>').value = name;
        document.getElementById('<%= HdnType.ClientID %>').value = type;
        if(type=="1")
        {
            document.getElementById(txtDirName).value = name;
            var showDirRenamePopup = $find('<%= MpeDirRename.ClientID %>');	
            showDirRenamePopup.show();
        }
        else
        {
            document.getElementById(txtFileName).value = name;
            var showFileRenamePopup = $find('<%= MpeFileRename.ClientID %>');	
            showFileRenamePopup.show();
        }
    }
    -->
    </script>

</asp:Content>
