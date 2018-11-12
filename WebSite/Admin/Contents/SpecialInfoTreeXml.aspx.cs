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

    public partial class SpecialInfoTreeXml : AdminPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            base.Response.Clear();
            base.Response.ContentType = "text/xml; charset=UTF-8";
            base.Response.CacheControl = "no-cache";
            XTreeCollection xTreeList = new XTreeCollection();
            SpecialInfoXml(xTreeList);
            base.Response.Write(xTreeList.ToString());
            base.Response.End();
        }

        public static void SpecialInfoXml(XTreeCollection xTreeList)
        {
            if (DataConverter.CLng(HttpContext.Current.Request.QueryString["SpecialCategoryID"]) <= 0)
            {
                foreach (SpecialCategoryInfo info in Special.GetSpecialCategoryList())
                {
                    XTreeItem item = new XTreeItem();
                    string str = "";
                    if (Special.ExistsSpecialCategoryIdInSpecials(info.SpecialCategoryId))
                    {
                        str = "SpecialInfoTreeXml.aspx?SpecialCategoryID=" + info.SpecialCategoryId;
                    }
                    item.Text = info.SpecialCategoryName;
                    item.ArrModelId = "1";
                    item.ArrModelName = "";
                    item.Icon = "Container";
                    item.NodeId = info.SpecialCategoryId.ToString();
                    item.Target = "main_right";
                    item.Expand = "0";
                    item.AnchorType = "2";
                    item.XmlSrc = str;
                    item.Action = "SpecialInfosManage.aspx?SpecialCategoryID=" + info.SpecialCategoryId;
                    xTreeList.Add(item);
                }
            }
            else
            {
                foreach (SpecialInfo info2 in Special.GetSpecialList(DataConverter.CLng(HttpContext.Current.Request.QueryString["SpecialCategoryID"])))
                {
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
                    item2.Action = string.Concat(new object[] { "SpecialInfosManage.aspx?SpecialCategoryID=", info2.SpecialCategoryId, "&SpecialID=", info2.SpecialId, "&SpecialName=", HttpContext.Current.Server.UrlEncode(info2.SpecialName) });
                    xTreeList.Add(item2);
                }
            }
        }
    }
}

