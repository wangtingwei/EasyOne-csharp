<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Contents.Model"
    MasterPageFile="~/Admin/MasterPage.master" Title="ģ�͹���" ValidateRequest="false" Codebehind="Model.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table class="border" width="100%" cellpadding="2" cellspacing="1">
        <tr>
            <td class="spacingtitle" colspan="2" align="center">
                <pe:AlternateLiteral ID="AltrTitle" Text="�������ģ��" AlternateText="�޸�����ģ��" runat="Server" />
            </td>
        </tr>
        <tr class="tdbg" runat="server" id="trModelTemmpalteId">
            <td class="tdbgleft">
                <strong>��������ģ��ģ�壺</strong>
            </td>
            <td>
                <asp:DropDownList ID="DropModelTemplate" runat="server" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" style="width: 35%">
                <strong>����ģ�����ƣ�</strong>
            </td>
            <td>
                <asp:TextBox ID="TxtModelName" runat="server" Width="156" MaxLength="200" />
                <pe:RequiredFieldValidator ID="ValrModelName" ControlToValidate="TxtModelName" runat="server"
                    ErrorMessage="����ģ�����Ʋ���Ϊ�գ�" SetFocusOnError="true" Display="Dynamic" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>���������ݱ�����</strong>
                <pe:AlternateLiteral ID="TableNameText" Text="<br /><span style='color: red'>ע�⣺</span>��������޷��ٸ��ı�����"
                    runat="Server" />
            </td>
            <td>
                <asp:Label ID="LblTablePrefix" runat="server" Text="PE_U_" />
                <asp:TextBox ID="TxtTableName" runat="server" Width="120" MaxLength="50" />
                <pe:RequiredFieldValidator ID="ValrTableName" ControlToValidate="TxtTableName" runat="server"
                    ErrorMessage="���ݱ�������Ϊ��" SetFocusOnError="true" Display="Dynamic" />
                <asp:RegularExpressionValidator ID="ValeTableName" runat="server" ControlToValidate="TxtTableName"
                    ErrorMessage="ֻ����������ĸ�����ֻ��»���" ValidationExpression="^[\w_]+$" SetFocusOnError="true"
                    Display="Dynamic" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>��Ŀ���ƣ�</strong>
                <br />
                ���磺���¡������ͼƬ����Ʒ
            </td>
            <td>
                <asp:TextBox ID="TxtItemName" runat="server" Width="156" MaxLength="20" />
                <pe:RequiredFieldValidator ID="ValrItemName" ControlToValidate="TxtItemName" runat="server"
                    ErrorMessage="��Ŀ���Ʋ���Ϊ��" SetFocusOnError="true" Display="Dynamic" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>��Ŀ��λ��</strong>
                <br />
                ���磺ƪ��������
            </td>
            <td>
                <asp:TextBox ID="TxtItemUnit" runat="server" Width="156" MaxLength="20" />
                <pe:RequiredFieldValidator ID="ValrItemUnit" ControlToValidate="TxtItemUnit" runat="server"
                    ErrorMessage="��Ŀ��λ����Ϊ��" SetFocusOnError="true" Display="Dynamic" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>��Ŀͼ�꣺</strong>
                <br />
                ͼ������~/Images/ModelIcon/Ŀ¼��
            </td>
            <td>
                <asp:TextBox ID="TxtItemIcon" Text="Default.gif" runat="server" Width="156" MaxLength="20" />
                <asp:Image ID="ImgItemIcon" runat="server" ImageUrl="~/Images/ModelIcon/Default.gif" />
                <=<asp:DropDownList ID="DrpItemIcon" runat="server" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>ģ��������</strong>
            </td>
            <td>
                <asp:TextBox ID="TxtDescription" runat="server" TextMode="MultiLine" Width="365px"
                    Height="43px" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>Ĭ������ҳģ�壺</strong>
            </td>
            <td>
                <pe:TemplateSelectControl ID="FileCTemplate" runat="server" Width="300px" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>��ӡҳģ�壺</strong>
            </td>
            <td>
                <pe:TemplateSelectControl ID="TscPrintTemplate" runat="server" Width="300px" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>��Ŀ����ҳģ�壺</strong>
            </td>
            <td>
                <pe:TemplateSelectControl ID="TscSearchTemplate" runat="server" Width="300px" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>�߼�������ҳģ�壺</strong>
            </td>
            <td>
                <pe:TemplateSelectControl ID="TscAdvanceSearchFormTemplate" runat="server" Width="300px" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>�߼�����ҳģ�壺</strong>
            </td>
            <td>
                <pe:TemplateSelectControl ID="TscAdvanceSearchTemplate" runat="server" Width="300px" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>�鿴����ģ�壺</strong>
            </td>
            <td>
                <pe:TemplateSelectControl ID="TscCommentManageTemplate" runat="server" Width="300px" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>��Ϣ�����ļ���</strong>
            </td>
            <td>
                <asp:TextBox ID="TxtAddInfoFilePath" runat="server" Width="156" MaxLength="200" Text="Content.aspx" />
                <pe:RequiredFieldValidator ID="ValrAddInfoFilePath" ControlToValidate="TxtAddInfoFilePath"
                    runat="server" ErrorMessage="��Ϣ�����ļ�����Ϊ��" SetFocusOnError="true" Display="Dynamic" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>��Ϣ�����ļ���</strong>
            </td>
            <td>
                <asp:TextBox ID="TxtManageInfoFilePath" runat="server" Width="156" MaxLength="200"
                    Text="ContentManage.aspx" />
                <pe:RequiredFieldValidator ID="ValrManageInfoFilePath" ControlToValidate="TxtManageInfoFilePath"
                    runat="server" ErrorMessage="��Ϣ�����ļ�����Ϊ��" SetFocusOnError="true" Display="Dynamic" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>��ϢԤ���ļ���</strong>
            </td>
            <td>
                <asp:TextBox ID="TxtPreviewInfoFilePath" runat="server" Width="156" MaxLength="200"
                    Text="ContentView.aspx" />
                <pe:RequiredFieldValidator ID="ValrPreviewInfoFilePath" ControlToValidate="TxtPreviewInfoFilePath"
                    runat="server" ErrorMessage="��ϢԤ���ļ�����Ϊ��" SetFocusOnError="true" Display="Dynamic" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>��Ϣ���������ļ���</strong>
            </td>
            <td>
                <asp:TextBox ID="TxtBatchInfoFilePath" runat="server" Width="156" MaxLength="200"
                    Text="ContentBatch.aspx" />
                <pe:RequiredFieldValidator ID="ValrBatchInfoFilePath" ControlToValidate="TxtBatchInfoFilePath"
                    runat="server" ErrorMessage="��Ϣ�������ļ�����Ϊ��" SetFocusOnError="true" Display="Dynamic" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>�Զ������ݷ����͹���ҳע�����</strong>
            </td>
            <td style="width: 80%">
                �����������ʹ�����Զ���ĳ����ļ����뽫�ļ�����ڡ�~/Admin/Contents/��Ŀ¼�¡�
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>�Ƿ�ͳ�Ƶ������</strong>
            </td>
            <td>
                <asp:RadioButtonList ID="RadioIsCountHits" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True" Value="True">��</asp:ListItem>
                    <asp:ListItem Value="False">��</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>�Ƿ���ã�</strong>
            </td>
            <td>
                <asp:RadioButtonList ID="RadioDisabled" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal">
                    <asp:ListItem Value="True">��</asp:ListItem>
                    <asp:ListItem Value="False" Selected="True">��</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr class="tdbg" runat="server" id="EnableCharge">
            <td class="tdbgleft">
                <strong>�Ƿ������շѣ�</strong>
            </td>
            <td>
                <asp:RadioButtonList ID="RadioEnableCharge" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal">
                    <asp:ListItem Value="True">��</asp:ListItem>
                    <asp:ListItem Value="False" Selected="True">��</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
         <tr class="tdbg" runat="server" id="EnableChargeTips">
            <td class="tdbgleft">
                <strong>���ɾ�̬ҳʱ���շ���ʾ��</strong><br />
                ֧��HTML���룬�ر��ǩ�У�<br />
                {$ModelName} ��Ŀ����<br />
                {$FileName} �ļ�����<br />
                {$Id}     ��Ϣ��GeneralId<br />
                
            </td>
            <td>
                <asp:TextBox ID="TxtModelChargeTips" TextMode="MultiLine" MaxLength="255" Width="365px" Height="43px" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>�Ƿ�����ǩ�գ�</strong>
            </td>
            <td>
                <asp:RadioButtonList ID="RadioEnableSignin" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal">
                    <asp:ListItem Value="True">��</asp:ListItem>
                    <asp:ListItem Value="False" Selected="True">��</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>�Ƿ�����ͶƱ��</strong>
            </td>
            <td>
                <asp:RadioButtonList ID="RadVote" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal">
                    <asp:ListItem Value="True">��</asp:ListItem>
                    <asp:ListItem Value="False" Selected="True">��</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft">
                <strong>�û��ڴ�ģ���·������ݵ���������</strong>
                <br />
                ���Ϊ0��ʾû������
            </td>
            <td>
                <asp:TextBox ID="TxtMaxPerUser" runat="server" MaxLength="6" Text="0" Width="36" />������
            </td>
        </tr>
        <tr class="tdbgbottom">
            <td colspan="2">
                <asp:Button ID="EBtnSubmit" Text="����" OnClick="EBtnSubmit_Click" runat="server" />
                &nbsp;&nbsp;
                <input name="Cancel" type="button" class="inputbutton" id="Cancel" value="ȡ��" onclick="Redirect('ModelManage.aspx')" />
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HdnModelId" runat="server" />
    <asp:HiddenField ID="HdnModelName" runat="server" />
    <asp:HiddenField ID="HdnTableName" runat="server" />

    <script type="text/javascript">
    function ChangeImgItemIcon(icon)
    {
        document.getElementById("<%= ImgItemIcon.ClientID %>").src = "../../Images/ModelIcon/"+icon;
    }
    function ChangeTxtItemIcon(icon)
    {
        document.getElementById("<%= TxtItemIcon.ClientID %>").value = icon;
    }
    </script>

</asp:Content>
