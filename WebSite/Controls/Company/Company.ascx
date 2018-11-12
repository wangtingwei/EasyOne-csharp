<%@ Control Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Controls.CompanyControl" Codebehind="Company.ascx.cs" %>
<tr class='tdbg'>
    <td align='right' style="width: 20%;" class='tdbgleft'>
        企业名称：</td>
    <td align='left' colspan="3" style="width: 80%;">
        <asp:TextBox ID="TxtCompanyName" runat="server" Width="300px" MaxLength="255"></asp:TextBox>  
        <pe:RequiredFieldValidator ID="ValrCompanyName" runat="server" Display="dynamic"
            ControlToValidate="TxtCompanyName" ErrorMessage="企业名称不能为空！"></pe:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="ValeCompanyName" Display="Dynamic" ControlToValidate="TxtCompanyName"
            ValidationExpression="^[\w\W\u4e00-\u9fa5]{6,100}$" SetFocusOnError="true" runat="server" ErrorMessage="企业名称的长度不能小于6位大于100位"></asp:RegularExpressionValidator>
    </td>
</tr>
<tr class='tdbg'>
    <td align='right' class='tdbgleft'>
        通讯地址：</td>
    <td align='left' colspan="3">
        <pe:Region ID="Region1" runat="server" />
        <table border="0" cellpadding="2" cellspacing="1" width="100%"  style="width: 100%; background-color: white;">
            <tr class="tdbg">
                <td style="width: 20%" align="right" class="tdbgleft">
                    联系地址：</td>
                <td>
                    <asp:TextBox ID="TxtAddress" runat="server" Width="300px" MaxLength="255"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ValrAddress" ControlToValidate="TxtAddress" SetFocusOnError="true"
                        Display="dynamic" runat="server" ErrorMessage="请输入详细的联系地址"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="ValeAddress" Display="Dynamic" ControlToValidate="TxtAddress"
                        ValidationExpression="^[\w\W\u4e00-\u9fa5]{10,}$" SetFocusOnError="true" runat="server" ErrorMessage="联系地址的长度不能小于10"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr class="tdbg">
                <td align="right" class="tdbgleft">
                    邮政编码：</td>
                <td>
                    <asp:TextBox ID="TxtZipCode" runat="server" MaxLength="6" Width="200px"></asp:TextBox><pe:ZipCodeValidator
                        ID="VzipZipCode" ControlToValidate="TxtZipCode" SetFocusOnError="true" Display="dynamic"
                        runat="server"></pe:ZipCodeValidator>
                    <asp:RequiredFieldValidator ID="ValrZipCode" ControlToValidate="TxtZipCode" SetFocusOnError="true"
                        Display="dynamic" runat="server" ErrorMessage="请输入单位的邮政编码"></asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
    </td>
</tr>
<tr class='tdbg'>
    <td align='right' class='tdbgleft'>
        联系电话：</td>
    <td align='left'>
        <asp:TextBox ID="TxtPhone" runat="server" MaxLength="30" ></asp:TextBox>
    </td>
    <td align='right' class='tdbgleft'>
        传真号码：</td>
    <td align='left'>
        <asp:TextBox ID="TxtFax" runat="server" MaxLength="30" ></asp:TextBox>
    </td>
</tr>
<tr class='tdbg'>
    <td align='right' class='tdbgleft'>
        开户银行：</td>
    <td align='left'>
        <asp:TextBox ID="TxtBankOfDeposit" runat="server" MaxLength="255" ></asp:TextBox>
    </td>
    <td align='right' class='tdbgleft'>
        银行帐号：</td>
    <td align='left'>
        <asp:TextBox ID="TxtBankAccount" runat="server" MaxLength="255" ></asp:TextBox>
    </td>
</tr>
<tr class='tdbg'>
    <td align='right' class='tdbgleft'>
        税号：</td>
    <td align='left'>
        <asp:TextBox ID="TxtTaxNum" runat="server" MaxLength="20" ></asp:TextBox>
    </td>
    <td align='right' class='tdbgleft'>
        网址：</td>
    <td align='left'>
        <asp:TextBox ID="TxtHomepage" runat="server" MaxLength="100" ></asp:TextBox>
    </td>
</tr>
<tr class='tdbg'>
    <td align='right' class='tdbgleft'>
        行业地位：</td>
    <td align='left'>
        <asp:DropDownList ID="DropStatusInField" runat="server" DataTextField="DataTextField"
            DataValueField="DataValueField">
        </asp:DropDownList>
    </td>
    <td align='right' class='tdbgleft'>
        公司规模：</td>
    <td align='left'>
        <asp:DropDownList ID="DropCompanySize" runat="server" DataTextField="DataTextField"
            DataValueField="DataValueField">
        </asp:DropDownList>
    </td>
</tr>
<tr class='tdbg'>
    <td align='right' class='tdbgleft'>
        业务范围：</td>
    <td align='left'>
        <asp:TextBox ID="TxtBusinessScope" runat="server" MaxLength="255" ></asp:TextBox>
    </td>
    <td align='right' class='tdbgleft'>
        年销售额：</td>
    <td align='left'>
        <asp:TextBox ID="TxtAnnualSales" runat="server" MaxLength="20" ></asp:TextBox>万元
    </td>
</tr>
<tr class='tdbg'>
    <td align='right' class='tdbgleft'>
        经营状态：</td>
    <td align='left'>
        <asp:DropDownList ID="DropManagementForms" runat="server" DataTextField="DataTextField"
            DataValueField="DataValueField">
        </asp:DropDownList>
    </td>
    <td align='right' class='tdbgleft'>
        注册资本：</td>
    <td align='left'>
        <asp:TextBox ID="TxtRegisteredCapital" runat="server" MaxLength="20" ></asp:TextBox>万元
    </td>
</tr>
<tr class='tdbg'>
    <td align='right' class='tdbgleft'>
        公司照片：</td>
    <td align='left' colspan="3">
        <asp:TextBox ID="TxtCompanyPic" runat="server" MaxLength="255"  Width="250px"></asp:TextBox>
    </td>
</tr>
<tr class='tdbg'>
    <td align='right' class='tdbgleft'>
        公司简介：</td>
    <td align='left' colspan="3">
        <asp:TextBox ID="TxtCompanyIntro" TextMode="MultiLine" Height="120px" runat="server" Width="250px"></asp:TextBox>
    </td>
</tr>
<asp:HiddenField ID="HdnCompanyAction" runat="server" />      
