<%@ Page Language="C#" AutoEventWireup="true"
    Inherits="EasyOne.WebSite.ShowDownloadError" StylesheetTheme="" EnableTheming="false" Codebehind="ShowDownloadError.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>下载错误地址</title>
</head>
<body>
    <form id="ShowDownloadError" runat="server">
        <div id="content">
            <pe:ExtendedGridView ID="EgvDownloadError" runat="server" AllowPaging="False" AutoGenerateColumns="False"
                AutoGenerateCheckBoxColumn="True" DataKeyNames="urlId" ItemName="地址" ItemUnit="个">
                <Columns>
                <pe:TemplateField>
                <ItemTemplate><asp:HiddenField ID="HdnUrlID" runat="server" Value='<%# Eval("urlId") %>' /><asp:HiddenField ID="HdnServerID" runat="server" Value='<%# Eval("serverId") %>' /></ItemTemplate>
                </pe:TemplateField>
                    <pe:BoundField DataField="infoId" HeaderText="ID">
                        <HeaderStyle Width="15%" />
                    </pe:BoundField>
                    <pe:BoundField DataField="urlname" HeaderText="下载地址">
                    </pe:BoundField>
                </Columns>
            </pe:ExtendedGridView>
            <br />
            <input id="ChkAll" onclick="javascript:CheckAll(this);" type="checkbox" />
            选中本页所有下载地址错误信息 &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="EBtnSubmit" Text=" 报错 " OnClick="EBtnSubmit_Click" UseSubmitBehavior="True"
                runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="EBtnCancle" Text=" 取消 " OnClick="EBtnCancle_Click" UseSubmitBehavior="True"
                runat="server" />
        </div>
    </form>
</body>
</html>
