namespace EasyOne.WebSite.Admin.Contents
{
    using EasyOne.Common;
    using EasyOne.Contents;
    using EasyOne.Controls;
    using EasyOne.Model.Contents;
    using EasyOne.Web.UI;
    using System;
    using System.Web;
    using System.Web.UI.HtmlControls;

    public partial class SpecialTreeXml : AdminPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            base.Response.Clear();
            base.Response.ContentType = "text/xml; charset=UTF-8";
            base.Response.CacheControl = "no-cache";
            XTreeCollection xTreeList = new XTreeCollection();
            SpecialXml(xTreeList);
            base.Response.Write(xTreeList.ToString());
            base.Response.End();
        }

        private static void SpecialXml(XTreeCollection xTreeList)
        {
            if (DataConverter.CLng(HttpContext.Current.Request.QueryString["SpecialCategoryID"]) <= 0)
            {
                foreach (SpecialCategoryInfo info in Special.GetSpecialCategoryList())
                {
                    string str = "SpecialManage.aspx?SpecialCategoryID=" + info.SpecialCategoryId;
                    string str2 = "";
                    string str3 = "2";
                    if (Special.ExistsSpecialCategoryIdInSpecials(info.SpecialCategoryId))
                    {
                        str3 = "1";
                        str2 = "SpecialTreeXml.aspx?SpecialCategoryID=" + info.SpecialCategoryId;
                    }
                    XTreeItem item = new XTreeItem();
                    item.Text = info.SpecialCategoryName;
                    item.ArrModelId = str3;
                    item.ArrModelName = "";
                    item.Icon = "Container";
                    item.NodeId = info.SpecialCategoryId.ToString();
                    item.Target = "main_right";
                    item.Expand = "0";
                    item.AnchorType = "2";
                    item.XmlSrc = str2;
                    item.Action = str;
                    item.Title = "小贴士：您可以在节点名称上点击鼠标右键，从弹出菜单中选择相关操作。";
                    xTreeList.Add(item);
                }
            }
            else
            {
                foreach (SpecialInfo info2 in Special.GetSpecialList(DataConverter.CLng(HttpContext.Current.Request.QueryString["SpecialCategoryID"])))
                {
                    string str4 = "Special.aspx?Action=Modify&amp;SpecialID=" + info2.SpecialId;
                    XTreeItem item2 = new XTreeItem();
                    item2.Text = info2.SpecialName;
                    item2.ArrModelId = "0";
                    item2.ArrModelName = "";
                    item2.NodeId = info2.SpecialId.ToString();
                    item2.Icon = "Container";
                    item2.Target = "main_right";
                    item2.Expand = "0";
                    item2.AnchorType = "2";
                    item2.XmlSrc = "";
                    item2.Action = str4;
                    item2.Title = "小贴士：您可以在节点名称上点击鼠标右键，从弹出菜单中选择相关操作。";
                    xTreeList.Add(item2);
                }
            }
        }
    }
}

