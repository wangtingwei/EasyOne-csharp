<%@ Control Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Controls.CompanyControl" Codebehind="Company.ascx.cs" %>
<tr class='tdbg'>
    <td align='right' style="width: 20%;" class='tdbgleft'>
        ��ҵ���ƣ�</td>
    <td align='left' colspan="3" style="width: 80%;">
        <asp:TextBox ID="TxtCompanyName" runat="server" Width="300px" MaxLength="255"></asp:TextBox>  
        <pe:RequiredFieldValidator ID="ValrCompanyName" runat="server" Display="dynamic"
            ControlToValidate="TxtCompanyName" ErrorMessage="��ҵ���Ʋ���Ϊ�գ�"></pe:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="ValeCompanyName" Display="Dynamic" ControlToValidate="TxtCompanyName"
            ValidationExpression="^[\w\W\u4e00-\u9fa5]{6,100}$" SetFocusOnError="true" runat="server" ErrorMessage="��ҵ���Ƶĳ��Ȳ���С��6λ����100λ"></asp:RegularExpressionValidator>
    </td>
</tr>
<tr class='tdbg'>
    <td align='right' class='tdbgleft'>
        ͨѶ��ַ��</td>
    <td align='left' colspan="3">
        <pe:Region ID="Region1" runat="server" />
        <table border="0" cellpadding="2" cellspacing="1" width="100%"  style="width: 100%; background-color: white;">
            <tr class="tdbg">
                <td style="width: 20%" align="right" class="tdbgleft">
                    ��ϵ��ַ��</td>
                <td>
                    <asp:TextBox ID="TxtAddress" runat="server" Width="300px" MaxLength="255"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ValrAddress" ControlToValidate="TxtAddress" SetFocusOnError="true"
                        Display="dynamic" runat="server" ErrorMessage="��������ϸ����ϵ��ַ"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="ValeAddress" Display="Dynamic" ControlToValidate="TxtAddress"
                        ValidationExpression="^[\w\W\u4e00-\u9fa5]{10,}$" SetFocusOnError="true" runat="server" ErrorMessage="��ϵ��ַ�ĳ��Ȳ���С��10"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr class="tdbg">
                <td align="right" class="tdbgleft">
                    �������룺</td>
                <td>
                    <asp:TextBox ID="TxtZipCode" runat="server" MaxLength="6" Width="200px"></asp:TextBox><pe:ZipCodeValidator
                        ID="VzipZipCode" ControlToValidate="TxtZipCode" SetFocusOnError="true" Display="dynamic"
                        runat="server"></pe:ZipCodeValidator>
                    <asp:RequiredFieldValidator ID="ValrZipCode" ControlToValidate="TxtZipCode" SetFocusOnError="true"
                        Display="dynamic" runat="server" ErrorMessage="�����뵥λ����������"></asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
    </td>
</tr>
<tr class='tdbg'>
    <td align='right' class='tdbgleft'>
        ��ϵ�绰��</td>
    <td align='left'>
        <asp:TextBox ID="TxtPhone" runat="server" MaxLength="30" ></asp:TextBox>
    </td>
    <td align='right' class='tdbgleft'>
        ������룺</td>
    <td align='left'>
        <asp:TextBox ID="TxtFax" runat="server" MaxLength="30" ></asp:TextBox>
    </td>
</tr>
<tr class='tdbg'>
    <td align='right' class='tdbgleft'>
        �������У�</td>
    <td align='left'>
        <asp:TextBox ID="TxtBankOfDeposit" runat="server" MaxLength="255" ></asp:TextBox>
    </td>
    <td align='right' class='tdbgleft'>
        �����ʺţ�</td>
    <td align='left'>
        <asp:TextBox ID="TxtBankAccount" runat="server" MaxLength="255" ></asp:TextBox>
    </td>
</tr>
<tr class='tdbg'>
    <td align='right' class='tdbgleft'>
        ˰�ţ�</td>
    <td align='left'>
        <asp:TextBox ID="TxtTaxNum" runat="server" MaxLength="20" ></asp:TextBox>
    </td>
    <td align='right' class='tdbgleft'>
        ��ַ��</td>
    <td align='left'>
        <asp:TextBox ID="TxtHomepage" runat="server" MaxLength="100" ></asp:TextBox>
    </td>
</tr>
<tr class='tdbg'>
    <td align='right' class='tdbgleft'>
        ��ҵ��λ��</td>
    <td align='left'>
        <asp:DropDownList ID="DropStatusInField" runat="server" DataTextField="DataTextField"
            DataValueField="DataValueField">
        </asp:DropDownList>
    </td>
    <td align='right' class='tdbgleft'>
        ��˾��ģ��</td>
    <td align='left'>
        <asp:DropDownList ID="DropCompanySize" runat="server" DataTextField="DataTextField"
            DataValueField="DataValueField">
        </asp:DropDownList>
    </td>
</tr>
<tr class='tdbg'>
    <td align='right' class='tdbgleft'>
        ҵ��Χ��</td>
    <td align='left'>
        <asp:TextBox ID="TxtBusinessScope" runat="server" MaxLength="255" ></asp:TextBox>
    </td>
    <td align='right' class='tdbgleft'>
        �����۶</td>
    <td align='left'>
        <asp:TextBox ID="TxtAnnualSales" runat="server" MaxLength="20" ></asp:TextBox>��Ԫ
    </td>
</tr>
<tr class='tdbg'>
    <td align='right' class='tdbgleft'>
        ��Ӫ״̬��</td>
    <td align='left'>
        <asp:DropDownList ID="DropManagementForms" runat="server" DataTextField="DataTextField"
            DataValueField="DataValueField">
        </asp:DropDownList>
    </td>
    <td align='right' class='tdbgleft'>
        ע���ʱ���</td>
    <td align='left'>
        <asp:TextBox ID="TxtRegisteredCapital" runat="server" MaxLength="20" ></asp:TextBox>��Ԫ
    </td>
</tr>
<tr class='tdbg'>
    <td align='right' class='tdbgleft'>
        ��˾��Ƭ��</td>
    <td align='left' colspan="3">
        <asp:TextBox ID="TxtCompanyPic" runat="server" MaxLength="255"  Width="250px"></asp:TextBox>
    </td>
</tr>
<tr class='tdbg'>
    <td align='right' class='tdbgleft'>
        ��˾��飺</td>
    <td align='left' colspan="3">
        <asp:TextBox ID="TxtCompanyIntro" TextMode="MultiLine" Height="120px" runat="server" Width="250px"></asp:TextBox>
    </td>
</tr>
<asp:HiddenField ID="HdnCompanyAction" runat="server" />      
