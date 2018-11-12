<%@ Control Language="C#" AutoEventWireup="true" Inherits="EasyOne.WebSite.Controls.Shop.DependentProduct" Codebehind="DependentProduct.ascx.cs" %>
<table>
    <tr>
        <td rowspan="2" style="width: 100px">
            <asp:ListBox Width="200" Height="70" DataTextField="Value" DataValueField="Key" ID="LstDependentProduct"
                runat="server" SelectionMode="Multiple"></asp:ListBox>
            <asp:HiddenField ID="HdnDependent" runat="server" />
        </td>
        <td style="width: 100px">
            <input type="button" class="inputbutton" onclick="AddDependentProduct()" value="添加" />
        </td>
    </tr>
    <tr>
        <td style="width: 100px">
            <input type="button" class="inputbutton" onclick='DelDependentProduct()' value="删除" />
        </td>
    </tr>
    <tr>
        <td>
            如果不指定从属商品，则可以与任何非促销礼品捆绑销售。</td>
    </tr>
</table>
<script language="javascript" type="text/javascript">      
      function AddDependentProduct()
      {
         window.open("../Shop/ProductList.aspx?ProductType=0,1,2,3&ProductID=<%=m_ProductID%>&ShowProperty=False&ModelID=" + <%=m_ModelId%>,"ProductList","width=670,height=400,resizable=0,scrollbars=yes");
      }
      function DoProductListPostBack(arr)
      {
            var list = document.getElementById('<%=LstDependentProduct.ClientID %>')
            var hdn = document.getElementById('<%=HdnDependent.ClientID %>');
            if (arr != null)
            {
                var ss=arr.split('$$$');
                if(checkName(list,ss[1]))
                {
                    return;
                }
                else
                {    
                    list.options[list.length] = new Option(ss[0],ss[1]);
                    if(hdn.value == "")
                    {
                        hdn.value = ss[1];
                    }
                    else
                    {
                        hdn.value = hdn.value + "," + ss[1];
                    }
                }
            }
      }
      
    function checkName(listControl,checkName)
    {
        for(i=0;i<listControl.length;i++)
        {
            if(listControl.options[i].value == checkName)
            {
                return true;
            }
        }
        return false;
    }
      
      function DelDependentProduct()
      {
        var list = document.getElementById('<%=LstDependentProduct.ClientID %>')
        if(list.length==0 || list.selectedIndex==-1){return false;} 
        
            var hdn = document.getElementById('<%=HdnDependent.ClientID %>');
            var newValue ="";
          for(i=0;i<list.length;i++)
          {
                while(list.options[i] != null && list.options[i].selected)
                {
                   list.options[i] = null;
                }
          }
          for(i=0;i<list.length;i++)
          {
                if(i!=(list.length-1))
                {
                    newValue += list.options[i].value +",";
                }
                else
                {
                    newValue += list.options[i].value;
                }
          }
            hdn.value = newValue;        
      }
      
</script>