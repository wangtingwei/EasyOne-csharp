<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Template.TemplateManage"
    MasterPageFile="~/Admin/MasterPage.master" Title="ģ�����"
    ValidateRequest="false" Codebehind="TemplateManage.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <asp:ScriptManager ID="SmTemplateManage" runat="server">
    </asp:ScriptManager>

    <script language="javascript" type="text/javascript">
    <!--
    function setFileType(num)
    {
        if (num == 0){
            document.getElementById('EntiretyMatching').style.display = ''; 
        }
        else
        {
            document.getElementById('EntiretyMatching').style.display = 'none'; 
        }
    }
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
    
    -->
    
    </script>

    <table width="100%">
        <tr>
            <td>
                <pe:ExtendedLabel ID="LblNavigation" HtmlEncode="false" runat="server" Text="��ǰĿ¼��" /><asp:Label ID="LblCurrentDir"
                    runat="server"></asp:Label>
            </td>
            <td align="right">
                <pe:ExtendedLiteral HtmlEncode="false" ID="LitParentDirLink" runat="server"></pe:ExtendedLiteral>
            </td>
        </tr>
    </table>
    <asp:Literal ID="LitMessageText" runat="server" Visible="false"></asp:Literal>
    <pe:ExtendedGridView ID="EgvFiles" runat="server" SerialText="" AutoGenerateColumns="False"
        AutoGenerateCheckBoxColumn="True" DataKeyNames="Name" OnRowCommand="EgvFiles_RowCommand"
        CheckBoxFieldHeaderWidth="3%" OnRowDataBound="EgvFiles_RowDataBound">
        <Columns>
            <pe:TemplateField HeaderText="����">
                <ItemStyle HorizontalAlign="Left" />
                <ItemTemplate>
                    <img alt="" src=' <%# System.Convert.ToInt32(Eval("type")) == 1 ? "../../Admin/Images/Node/closefolder.gif" :"../../Admin/Images/Node/singlepage.gif" %>' />
                    <a href="<%# System.Convert.ToInt32(Eval("type")) == 1 ?  "TemplateManage.aspx?Dir=" + Server.UrlEncode(Request.QueryString["Dir"] +"/"+ Eval("Name").ToString()):"Template.aspx?Action=Modify&Dir="+ Server.UrlEncode(Request.QueryString["Dir"] +"/"+ Eval("Name").ToString()) %>">
                        <%# Eval("Name") %>
                    </a>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:TemplateField HeaderText="��С">
                <HeaderStyle Width="60px" />
                <ItemTemplate>
                    <%# GetSize(Eval("size").ToString()) %>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Right" />
            </pe:TemplateField>
            <pe:TemplateField HeaderText="����">
                <HeaderStyle Width="60px" />
                <ItemTemplate>
                    <asp:HiddenField ID="HdnFileType" Value='<%#Eval("type") %>' runat="server" />
                    <%# System.Convert.ToInt32(Eval("type")) == 1 ? "�ļ���" : Eval("content_type").ToString() + "�ļ�" %>
                </ItemTemplate>
            </pe:TemplateField>
            <pe:BoundField DataField="lastWriteTime" HeaderText="����޸�ʱ��" SortExpression="lastWriteTime"
                DataFormatString="{0:yyyy-MM-dd HH:mm:ss}" HtmlEncode="False">
                <HeaderStyle Width="120px" />
            </pe:BoundField>
            <pe:TemplateField HeaderText="����">
                <HeaderStyle Width="150px" />
                <ItemTemplate>
                    <pe:ExtendedAnchor ID="EahTemplateEdit" IsChecked="true" OperateCode="TemplateManage"
                        href='<%# "Template.aspx?Action=Modify&Dir="+ Server.UrlEncode(Request.QueryString["Dir"] +"/"+ Eval("Name").ToString()) %>'
                        runat="server" visible='<%# System.Convert.ToInt32(Eval("type")) == 1 ? false:true %>'>�༭</pe:ExtendedAnchor>
                    <pe:ExtendedLinkButton ID="ELbtnDel" Text="ɾ��" IsChecked="true" OperateCode="TemplateManage"
                        runat="server" CommandArgument='<%# Eval("Name").ToString()%>' CommandName='<%# System.Convert.ToInt32(Eval("type")) == 1 ? "DelDir":"DelFiles" %>'
                        OnClientClick="return confirm('ȷ��Ҫɾ�����ļ��л��ļ���');" />
                    <pe:ExtendedAnchor ID="EahReName" IsChecked="true" OperateCode="TemplateManage" href="#"
                        onclick='<%# "ReName(\"" + Eval("type").ToString()+"\",\""+ Eval("Name").ToString().Replace("\\","\\\\") + "\");"%>'
                        runat="server">������</pe:ExtendedAnchor>
                    <pe:ExtendedLinkButton ID="ELbtnCopy" Text="����" IsChecked="true" OperateCode="TemplateManage"
                        runat="server" CommandName='<%# System.Convert.ToInt32(Eval("type")) == 1 ? "CopyDir":"CopyFiles" %>'
                        CommandArgument='<%# Eval("Name").ToString()%>' />
                </ItemTemplate>
            </pe:TemplateField>
        </Columns>
    </pe:ExtendedGridView>
    <table border="0" cellpadding="2" cellspacing="0">
        <tr>
            <td align="center">
                <pe:AspNetPager ID="Pager" runat="server" PageSize="20" OnPageChanged="Pager_PageChanged">
                </pe:AspNetPager>
            </td>
        </tr>
    </table>
    <br />
    <asp:Panel ID="PanButton" runat="server">
        <table width="100%">
            <tr>
                <td style="width: 11%;">
                    <input id="ChkAll" onclick="javascript:CheckAll(this);" type="checkbox" /><label
                        for="ChkAll">ѡ������</label>
                </td>
                <td>
                    <pe:ExtendedButton ID="EBtnBatchDel" OnClientClick="return confirm('ȷ��Ҫɾ��ѡ�е��ļ��к��ļ���');"
                        IsChecked="true" OperateCode="TemplateManage" runat="server" Text="ɾ��ѡ�е��ļ����ļ���"
                        OnClick="EBtnBatchDel_Click" CausesValidation="False" />
                    <pe:ExtendedButton ID="EBtnCreateTemplate" runat="server" Text="�½�ģ��" IsChecked="true"
                        OperateCode="TemplateManage" OnClick="EBtnCreateTemplate_Click" />
                    <input id="InputUploadTemplate" type="button" class="inputbutton" value="�ϴ�ģ��" onclick="javascript:window.open('TemplateUpload.aspx?Dir=<%= Server.UrlEncode(Request.QueryString["Dir"]) %>','�ϴ�ģ��','width=600,height=450,resizable=0,scrollbars=yes');" />
                    <input id="InputNewDir" type="button" class="inputbutton" value="�½�Ŀ¼" onclick="CreateDir()" />
                    <pe:ExtendedButton ID="EBtnTemplateReplace" runat="server" Text="ģ�������滻" IsChecked="true"
                        OperateCode="TemplateManage" OnClick="EBtnTemplateManage_Click" />
                    <pe:ExtendedLiteral HtmlEncode="false" ID="LitParentDirButton" runat="server"></pe:ExtendedLiteral>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:Button ID="BtnMove" runat="server" Text="�ƶ��� >>" OnClick="BtnMove_Click" />
                    <asp:DropDownList ID="DrpMove" runat="server">
                        <asp:ListItem Text="��ѡ��Ŀ���ļ���" Value=""></asp:ListItem>
                        <asp:ListItem Text="/" Value="/"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:HiddenField ID="HdnType" runat="server" />
    <asp:HiddenField ID="HdnName" runat="server" />
    <asp:Panel ID="PnlFileRename" runat="server" Style="width: 300px; display: none;
        background-color: Gray; text-align: center; padding-top: 1px;">
        <table width="100%" border="0" cellpadding="1" cellspacing="1" class="border">
            <tr>
                <td class="spacingtitle">
                    ����ģ���ļ���
                </td>
            </tr>
            <tr>
                <td class="tdbgleft" align="left">
                    <asp:TextBox ID="TxtFileName" ValidationGroup="TxtFileName" Width="290px" runat="server"></asp:TextBox>
                    <pe:RequiredFieldValidator ID="ValrTxtFileName" runat="server" ErrorMessage="ģ��������Ϊ��"
                        Display="Dynamic" ControlToValidate="TxtFileName" ValidationGroup="TxtFileName"></pe:RequiredFieldValidator><asp:RegularExpressionValidator
                            ID="ValeTxtFileName" ValidationGroup="TxtFileName" ControlToValidate="TxtFileName"
                            Display="Dynamic" runat="server" ErrorMessage='ģ���ʽӦΪ***.html�����ܰ���\/:*?"<>|.�Ϳո���ַ���'></asp:RegularExpressionValidator>
                    <br />
                    <asp:CheckBox ID="ChkUpdateFileContactinformation" runat="server" Text="ͬʱ�������ø��ļ���������Ϣ"
                        Checked="true"></asp:CheckBox>
                    <br />
                    <center>
                        <pe:ExtendedButton ID="EBtnFileReName" runat="server" ValidationGroup="TxtFileName"
                            IsChecked="true" OperateCode="TemplateManage" Text="����ģ���ļ���" OnClick="EBtnModifyName_Click" /><asp:Button
                                ID="BtnHiddenFileRename" Text="ȡ��" runat="server" />
                    </center>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="PnlDirRename" runat="server" Style="width: 300px; display: none; background-color: Gray;
        text-align: center; padding-top: 1px;">
        <table width="100%" border="0" cellpadding="1" cellspacing="1" class="border">
            <tr>
                <td class="spacingtitle">
                    ����Ŀ¼��
                </td>
            </tr>
            <tr>
                <td class="tdbgleft" align="left">
                    <asp:TextBox ID="TxtDirName" ValidationGroup="TxtDirName" runat="server" Width="290px"></asp:TextBox>
                    <pe:RequiredFieldValidator ID="ValrTxtDirName" runat="server" ErrorMessage="Ŀ¼������Ϊ��"
                        Display="Dynamic" ControlToValidate="TxtDirName" ValidationGroup="TxtDirName"></pe:RequiredFieldValidator><asp:RegularExpressionValidator
                            ID="ValeTxtDirName" ValidationGroup="TxtDirName" Display="Dynamic" ControlToValidate="TxtDirName"
                            runat="server" ErrorMessage='Ŀ¼��ʽ����ȷ�����ܰ���\/:*?"<>|.�Ϳո���ַ���'></asp:RegularExpressionValidator>
                    <br />
                    <asp:CheckBox ID="ChkUpdateFolderContactinformation" runat="server" Text="ͬʱ�������ø��ļ��е�������Ϣ"
                        Checked="true"></asp:CheckBox>
                    <br />
                    <center>
                        <pe:ExtendedButton ID="EBtnDirReName" runat="server" ValidationGroup="TxtDirName"
                            IsChecked="true" OperateCode="TemplateManage" Text="����Ŀ¼��" OnClick="EBtnModifyName_Click" /><asp:Button
                                ID="BtnHiddenDirRename" Text="ȡ��" runat="server" />
                    </center>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="PnlCopyDir" runat="server" Style="width: 300px; display: none; background-color: Gray;
        text-align: center; padding-top: 1px;">
        <table width="100%" border="0" cellpadding="1" cellspacing="1" class="border">
            <tr>
                <td class="spacingtitle">
                    �����ļ���
                </td>
            </tr>
            <tr>
                <td class="tdbgleft">
                    ����Ŀ���ļ���<asp:Label ForeColor="red" ID="LblCopyDir" runat="server"></asp:Label>�Ѿ������ˣ�������������������������򸲸������ļ���
                    <asp:TextBox ID="TxtCopyDir" ValidationGroup="TxtCopyDir" runat="server"></asp:TextBox>
                    <pe:RequiredFieldValidator ID="ValrTxtCopyDir" runat="server" ErrorMessage="Ŀ¼������Ϊ��"
                        Display="Dynamic" ControlToValidate="TxtCopyDir" ValidationGroup="TxtCopyDir"></pe:RequiredFieldValidator><asp:RegularExpressionValidator
                            ID="ValeTxtCopyDir" ValidationGroup="TxtCopyDir" Display="Dynamic" ControlToValidate="TxtCopyDir"
                            runat="server" ErrorMessage='Ŀ¼��ʽ����ȷ�����ܰ���\/:*?"<>|.�Ϳո���ַ���'></asp:RegularExpressionValidator>
                    <br />
                    <pe:ExtendedButton ID="EBtnCopyDir" runat="server" IsChecked="true" OperateCode="TemplateManage"
                        ValidationGroup="TxtCopyDir" Text="����" OnClick="EBtnCopyDir_Click" /><asp:Button
                            ID="BtnHiddenCopyDir" runat="server" Text="ȡ��" />
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
                    �����ļ�
                </td>
            </tr>
            <tr>
                <td class="tdbgleft">
                    ����Ŀ���ļ�<asp:Label ForeColor="red" ID="LblCopyFile" runat="server"></asp:Label>�Ѿ������ˣ�������������������������򸲸������ļ�
                    <asp:TextBox ID="TxtCopyFile" ValidationGroup="TxtCopyFile" runat="server"></asp:TextBox>
                    <pe:RequiredFieldValidator ID="ValrTxtCopyFile" runat="server" ErrorMessage="�ļ�������Ϊ��"
                        Display="Dynamic" ControlToValidate="TxtCopyFile" ValidationGroup="TxtCopyFile"></pe:RequiredFieldValidator><asp:RegularExpressionValidator
                            ID="ValeTxtCopyFile" ValidationGroup="TxtCopyFile" Display="Dynamic" ControlToValidate="TxtCopyFile"
                            runat="server" ErrorMessage='�ļ���ʽӦΪ***.html�����ܰ���\/:*?"<>|.�Ϳո���ַ���'></asp:RegularExpressionValidator>
                    <br />
                    <pe:ExtendedButton ID="EBtnCopyFile" runat="server" IsChecked="true" OperateCode="TemplateManage"
                        ValidationGroup="TxtCopyFile" Text="����" OnClick="EBtnCopyFile_Click" /><asp:Button
                            ID="BtnHiddenCopyFile" runat="server" Text="ȡ��" />
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
                    ����Ŀ¼
                </td>
            </tr>
            <tr>
                <td class="tdbgleft">
                    Ŀ¼����
                    <asp:TextBox ID="TxtNewDir" runat="server" ValidationGroup="NewDir"></asp:TextBox><pe:RequiredFieldValidator
                        ID="ValrTxtNewDir" ControlToValidate="TxtNewDir" ValidationGroup="NewDir" Display="Dynamic"
                        runat="server" ErrorMessage="�ļ�Ŀ¼������Ϊ�գ�"></pe:RequiredFieldValidator><asp:RegularExpressionValidator
                            ID="ValeTxtNewDir" runat="server" ErrorMessage='Ŀ¼����ʽ����ȷ�����ܰ���\/:*?"<>|.�Ϳո���ַ���' ControlToValidate="TxtNewDir"
                            ValidationGroup="NewDir" Display="Dynamic"></asp:RegularExpressionValidator><br />
                    <pe:ExtendedButton ID="EBtnNewDir" runat="server" IsChecked="true" OperateCode="TemplateManage"
                        Text="ִ��" ValidationGroup="NewDir" OnClick="EBtnNewDir_Click" /><asp:Button ID="BtnHiddenCreateDir"
                            Text="ȡ��" runat="server" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:HiddenField ID="HdnTemplateManage" runat="server" />
    <ajaxToolkit:ModalPopupExtender ID="MpeFileRename" runat="server" TargetControlID="HdnTemplateManage"
        Y="60" PopupControlID="PnlFileRename" CancelControlID="BtnHiddenFileRename" BackgroundCssClass="modalBackground"
        DropShadow="true" Drag="true" />
    <ajaxToolkit:ModalPopupExtender ID="MpeDirRename" runat="server" TargetControlID="HdnTemplateManage"
        Y="60" PopupControlID="PnlDirRename" CancelControlID="BtnHiddenDirRename" BackgroundCssClass="modalBackground"
        DropShadow="true" Drag="true" />
    <ajaxToolkit:ModalPopupExtender ID="MpeCreateDir" runat="server" TargetControlID="HdnTemplateManage"
        Y="60" PopupControlID="PnlCreateDir" CancelControlID="BtnHiddenCreateDir" BackgroundCssClass="modalBackground"
        DropShadow="true" Drag="true" />
    <ajaxToolkit:ModalPopupExtender ID="MpeCopyDir" runat="server" TargetControlID="HdnTemplateManage"
        Y="60" PopupControlID="PnlCopyDir" CancelControlID="BtnHiddenCopyDir" BackgroundCssClass="modalBackground"
        DropShadow="true" Drag="true" />
    <ajaxToolkit:ModalPopupExtender ID="MpeCopyFile" runat="server" TargetControlID="HdnTemplateManage"
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
