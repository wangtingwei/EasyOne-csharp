<%@ Page Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Admin.Collection.Exclosion"
    MasterPageFile="~/Admin/MasterPage.master" Title="采集排除" ValidateRequest="false" Codebehind="Exclosion.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CphNavigation" runat="Server">
    <pe:ExtendedSiteMapPath ID="SmpNavigator" SiteMapProvider="AdminMapProvider" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CphContent" runat="Server">
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr align="center">
            <td colspan="2" class="spacingtitle">
                <b>
                    <pe:AlternateLiteral ID="LblTitle" Text="添加采集排除规则" AlternateText="修改采集排除规则" runat="Server" /></b>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" style="width: 15%" align="right">
                <strong>排除名称：</strong>
            </td>
            <td>
                <asp:TextBox ID="TxtExclosionName" runat="server" Width="200px" MaxLength="200"></asp:TextBox>
                <pe:RequiredFieldValidator ID="ValeTxtExclosionName" ControlToValidate="TxtExclosionName"
                    ErrorMessage="排除名称不能为空！" runat="server"></pe:RequiredFieldValidator>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" style="width: 15%" align="right">
                <strong>排除类型：</strong>
            </td>
            <td>
                <asp:DropDownList ID="DropExclosionType" runat="server" Width="60px">
                    <asp:ListItem Value="1">文本</asp:ListItem>
                    <asp:ListItem Value="2">时间</asp:ListItem>
                    <asp:ListItem Value="3">数字</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tbody id="Tabs0" runat="server">
            <tr class="tdbg">
                <td class="tdbgleft" style="width: 15%" align="right">
                    <strong>字符排除类型：</strong>
                </td>
                <td>
                    <asp:RadioButton ID="RadExclosionString1" Text ="采集不含有的下面字符的信息" GroupName ="RadExclosionString" runat="server" Checked="True" />
                    <asp:RadioButton ID="RadExclosionString2" Text = "采集只含有下面字符的信息" GroupName ="RadExclosionString" runat="server" />
                </td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft" style="width: 15%" align="right">
                </td>
                <td>
                    <asp:TextBox ID="TxtExclosionString" runat="server" Height="281px" TextMode="MultiLine"
                        Width="250px"></asp:TextBox>
                    <br />
                    <span style="color: Green">注： 一行一个或多个</span>
                </td>
            </tr>
        </tbody>
        <tbody id="Tabs1" runat="server" style="display: none;">
            <tr class="tdbg">
                <td class="tdbgleft" style="width: 15%" align="right">
                </td>
                <td>
                    <asp:CheckBox ID="ChkExclosionDateTime1" runat="server" Text="排除日期等于" />
                    <pe:DatePicker ID="ExclosionDateTime1" runat="server"></pe:DatePicker>的信息
                    <br />
                    <asp:CheckBox ID="ChkExclosionDateTime2" runat="server" Text="排除日期大于" />
                    <pe:DatePicker ID="ExclosionDateTime2" runat="server"></pe:DatePicker>的信息<br />
                    <asp:CheckBox ID="ChkExclosionDateTime3" runat="server" Text="排除日期小于" />
                    <pe:DatePicker ID="ExclosionDateTime3" runat="server"></pe:DatePicker>的信息
                </td>
            </tr>
        </tbody>
        <tbody id="Tabs2" runat="server" style="display: none;">
            <tr class="tdbg">
                <td class="tdbgleft" style="width: 15%" align="right">
                </td>
                <td>
                    <asp:CheckBox ID="ChkExclosionNumber1" runat="server" Text="排除数字等于" />
                    <asp:TextBox ID="TxtExclosionNumber1" Columns = "7" MaxLength ="7" runat="server"></asp:TextBox>的信息
                    <br />
                    <asp:CheckBox ID="ChkExclosionNumber2" runat="server" Text="排除数字大于" />
                    <asp:TextBox ID="TxtExclosionNumber2" Columns = "7" MaxLength ="7" runat="server"></asp:TextBox>的信息<br />
                    <asp:CheckBox ID="ChkExclosionNumber3" runat="server" Text="排除数字小于" />
                    <asp:TextBox ID="TxtExclosionNumber3" Columns = "7" MaxLength ="7" runat="server"></asp:TextBox>的信息
                </td>
            </tr>
        </tbody>
    </table>
    <br />
    <center>
        <asp:Button ID="BtnSubmit" Text=" 提 交 " OnClick="EBtnSubmit_Click" runat="server" />&nbsp;&nbsp;<input
            name="Cancel" type="button" class="inputbutton" id="Cancel2" value=" 返 回 " onclick="Redirect('ItemManage.aspx')" />
        <asp:HiddenField ID="HdnAction" runat="server" />
        <asp:HiddenField ID="HdnExclosionName" runat="server" />
        
    </center>
    
    <script type ="text/javascript" >
    function exclosionTpye(type){
        if (type == 1){
            document.getElementById("<%=Tabs0.ClientID%>").style.display="";
            document.getElementById("<%=Tabs1.ClientID%>").style.display="none";
            document.getElementById("<%=Tabs2.ClientID%>").style.display="none";
        }else if (type ==2){
            document.getElementById("<%=Tabs0.ClientID%>").style.display="none";
            document.getElementById("<%=Tabs1.ClientID%>").style.display="";
            document.getElementById("<%=Tabs2.ClientID%>").style.display="none";
        }else{
            document.getElementById("<%=Tabs0.ClientID%>").style.display="none";
            document.getElementById("<%=Tabs1.ClientID%>").style.display="none";
            document.getElementById("<%=Tabs2.ClientID%>").style.display="";
        }
    }
    </script>
</asp:Content>
