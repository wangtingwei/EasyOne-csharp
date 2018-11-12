<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.Admin.Template.LabelUI" Title="��ǩ�༭" Codebehind="Label.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" />

    <script id="insertlabel" type="text/javascript">
<!--
function addclass(sourceid, tarid){
    var select=$get(sourceid);
    var tar=$get(tarid);
    for(i=0;i<select.length;i++){
        if(select[i].selected){
            tar.value=select[i].value;
        }
    }
}

function settext(){
    $get("<% =TxtTestStat.ClientID %>").innerHTML = "������...";
}
-->
    </script>

    <table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="border">
        <tr>
            <td class="spacingtitle" colspan="2" align="center">
                <span id="LblTitle" style="font-weight: bold;">��ǩ����</span></td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right" style="width: 105px">
                <strong>��ǩ���ƣ�&nbsp;</strong></td>
            <td class="tdbg" align="left">
                <asp:TextBox ID="TxtLabelName" runat="server" Width="288px" />
                <pe:RequiredFieldValidator runat="server" ID="NReq" ControlToValidate="TxtLabelName"
                    Display="Dynamic" ErrorMessage="�������ǩ����" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right" style="width: 105px">
                <strong>��ǩ���ࣺ&nbsp;</strong></td>
            <td class="tdbg" align="left">
                <asp:TextBox ID="TxtLabelType" runat="server" Width="216px"></asp:TextBox>
                <asp:DropDownList ID="DropLabelType" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right" style="width: 105px">
                <strong>�������ã�&nbsp;</strong></td>
            <td class="tdbg" align="left">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="RbtDataType" runat="server" OnSelectedIndexChanged="RbtDataType_SelectedIndexChanged"
                            AutoPostBack="True">
                            <asp:ListItem Value="static">��</asp:ListItem>
                            <asp:ListItem Value="sql_sysquery">ϵͳ���ݿ�SQL��ѯ</asp:ListItem>
                            <asp:ListItem Value="sql_sysstoredquery">ϵͳ���ݿ�洢���̲�ѯ</asp:ListItem>
                            <asp:ListItem Value="sql_outquery">�ⲿSQL��ѯ</asp:ListItem>
                            <asp:ListItem Value="mdb_read">Access����Դ</asp:ListItem>
                            <asp:ListItem Value="xsl_read">Excel����Դ</asp:ListItem>
                            <asp:ListItem Value="ole_read">OLE����Դ</asp:ListItem>
                            <asp:ListItem Value="odbc_read">ODBC����Դ</asp:ListItem>
                            <asp:ListItem Value="orc_read">Oracle����Դ</asp:ListItem>
                            <asp:ListItem Value="xml_read">XML����Դ</asp:ListItem>
                        </asp:DropDownList>
                        <table>
                            <tr>
                                <td>
                                    ��ǩģ�崦��ʽ��</td>
                                <td>
                                    <asp:RadioButtonList ID="RBLOutType" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="txt" Selected="True">TXT����</asp:ListItem>
                                        <asp:ListItem Value="sin">��XSLT����</asp:ListItem>
                                        <asp:ListItem Value="">�ɱ��XSLT����</asp:ListItem>
                                        <asp:ListItem Value="xml">ǿ�����XML�ṹ(��̬��ǩ��Ч)</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="PanelOutSide" runat="server">
                            <table>
                                <tr>
                                    <td>
                                        ����Դ�����ַ�����</td>
                                    <td>
                                        <asp:TextBox ID="TxtDataSource" runat="server" Width="315px"></asp:TextBox>&nbsp;
                                        <asp:Button ID="BtnTestDataSource" OnClientClick="settext()" OnClick="BtnTestDataSource_Click"
                                            runat="server" Text="����"></asp:Button><br /><asp:Label ID="TxtTestStat" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right" style="width: 105px">
                <strong>��ǩ˵����&nbsp;</strong></td>
            <td class="tdbg" align="left">
                <asp:TextBox ID="TxtLabelIntro" runat="server" Width="288px" Height="112px" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg">
            <td colspan="2" align="center">
                <asp:Button ID="BtnNext" runat="server" Text="��һ��" OnClick="BtnNext_Click" Style="cursor: pointer;
                    cursor: hand; width: 88px;" />&nbsp;&nbsp;<asp:Button ID="BtnSave" runat="server"
                        Text="������" OnClick="BtnSave_Click" Style="cursor: pointer; cursor: hand; width: 88px;"
                        Visible="False" />&nbsp;&nbsp;
                <input id="BtnCancel" type="button" class="inputbutton" value="ȡ����" onclick="Redirect('LabelManage.aspx')"
                    style="cursor: pointer; cursor: hand; width: 88px;" />&nbsp;&nbsp;
                <asp:DropDownList ID="LinkJump" runat="server" Visible="false" OnSelectedIndexChanged="JumpUrl">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <br />
    <table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="border">
        <tr>
            <td class="spacingtitle" colspan="2" align="center">
                <span id="Span1" style="font-weight: bold;">���˵��</span></td>
        </tr>
        <tr class="tdbg">
            <td>
                <ajaxToolkit:TabContainer runat="server" ID="Tabs" Height="150px">
                    <ajaxToolkit:TabPanel runat="Server" ID="TabPanel" HeaderText="ʹ��˵��">
                        <ContentTemplate>
                            <p>
                                ��ѡ������Դʱ����ǩΪ��̬ģʽ�������ٶ���죬�������٣�һ�����������ģ��ķָ����
                            </p>
                            <p>
                                ��ǩ��ѡ�����������ʽ������Ϊ��
                                <li>��XSLT��������ǩģ��ֻ֧��ʹ�ñ�׼��XSLT�﷨������Դ����һ�㣬�ٶȱȽϿ죬һ���ǩ��Ӧʹ�ô���ģʽ��</li>
                                <li>�ɱ��XSLT��������ǩģ��֧��ʹ��c#,vb,js�ȳ�����룬������߼��ı�ǩ������Դ������ߡ�</li>
                                <li>TXT���ݣ�ֻ���һ���ֶεĽ����ͨ�����ڵ�һ��ѯ��ϵͳ��ǩ����XSLT�������ܣ�Ϊ����ٵı�ǩ��<font color="red">��̬��ǩģʽǿ�ҽ���ʹ�ô�ѡ�</font></li>
                                <li>XML���ݣ���ģʽ������XSLT���ͣ�ֱ�����XML��ʽ�Ĳ�ѯ������ٶȷǳ��죬һ��ֻ�������ⳡ���µ����ݽ���ʹ�á�<font color="red">��ģʽ�ھ�̬��ǩ����Ч��</font></li>
                            </p>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel runat="Server" ID="TabPanel1" HeaderText="���ʽ���">
                        <ContentTemplate>
                            <p>
                                ��ǩ����:<br />
                                �����뱾��ǩ�����ƣ�������һ��ȷ��������ҳ�е��ø�ʽ��ȷ��Ϊ{$PE id="��ǩ��" ����=""/}��</p>
                            <p>
                                ��ǩ����:<br />
                                ������Ϊ��ǩѡ��һ�����࣬���û������Ҫ�ķ��࣬����ֱ���ڷ������������������Ҫ�ķ������ƣ��÷��ཫ���Զ�������������ַ���Ϊ�գ���ñ�ǩ�������κη��ࡣ</p>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel runat="Server" ID="TabPanel3" HeaderText="����Դ˵��">
                        <ContentTemplate>
                            <p>
                                ����Դ�����Ǹ�����ǩ�ṩ���ݵ���Դ��������ϵͳ���ݿ⣬���AC���ݿ⣬�������ӱ���ļ���XML�ļ���������վ���XML���ݵĵ�ַ�ȡ�</p>
                            <p>
                                �ⲿ����Դ��������˵����
                                <li>OLE����Դ,һ����������Access��Excel�����ݿ�</li>
                                <li>ODBC����Դ����;�ȽϹ㷺��������������Ŀ�֧��ODBC��ʽ�����ݿ�</li>
                                <li>Oracle����Դ��Oracleר�����ӷ�ʽ��Ч��Ҫ��ͨ��ODBC���Ӹߡ�</li>
                                <li>Xml����Դ���ȿ����Ǳ��ص�XML�ļ���Ҳ������Զ�̵�XML��ʽ����Դ��</li>
                            </p>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                </ajaxToolkit:TabContainer>
            </td>
        </tr>
    </table>
</asp:Content>
