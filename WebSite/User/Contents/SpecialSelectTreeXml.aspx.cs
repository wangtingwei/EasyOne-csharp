namespace EasyOne.WebSite.User.Contents
{
    using EasyOne.AccessManage;
    using EasyOne.Common;
    using EasyOne.Contents;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Model.Contents;
    using EasyOne.Web.UI;
    using System;
    using System.Web;
    using System.Web.UI.HtmlControls;

    public partial class SpecialSelectTreeXml : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            base.Response.Clear();
            base.Response.ContentType = "text/xml; charset=UTF-8";
            base.Response.CacheControl = "no-cache";
            XTreeCollection xTreeList = new XTreeCollection();
            SpecialSelectXml(xTreeList);
            base.Response.Write(xTreeList.ToString());
            base.Response.End();
        }

        private static void SpecialSelectXml(XTreeCollection xTreeList)
        {
            if (DataConverter.CLng(HttpContext.Current.Request.QueryString["SpecialCategoryID"]) <= 0)
            {
                foreach (SpecialCategoryInfo info in Special.GetSpecialCategoryList())
                {
                    string str = "javascript:category();";
                    string str2 = "";
                    if (Special.ExistsSpecialCategoryIdInSpecials(info.SpecialCategoryId))
                    {
                        str2 = "SpecialSelectTreeXml.aspx?Action=SpecialSelect&SpecialCategoryID=" + info.SpecialCategoryId;
                    }
                    XTreeItem item = new XTreeItem();
                    item.Text = "[专题类别]" + info.SpecialCategoryName;
                    item.ArrModelId = "1";
                    item.ArrModelName = "";
                    item.Icon = "Container";
                    item.NodeId = info.SpecialCategoryId.ToString();
                    item.Target = "";
                    item.Expand = "0";
                    item.AnchorType = "1";
                    item.XmlSrc = str2;
                    item.Action = str;
                    xTreeList.Add(item);
                }
            }
            else
            {
                foreach (SpecialInfo info2 in Special.GetSpecialList(DataConverter.CLng(HttpContext.Current.Request.QueryString["SpecialCategoryID"])))
                {
                    string str3 = "javascript:going(" + info2.SpecialId.ToString() + ");";
                    XTreeItem item2 = new XTreeItem();
                    item2.Text = info2.SpecialName;
                    item2.ArrModelId = "0";
                    item2.ArrModelName = "";
                    item2.NodeId = info2.SpecialId.ToString();
                    item2.Icon = "Container";
                    item2.Target = "";
                    item2.Expand = "0";
                    if (UserPermissions.AccessCheckSpecial(OperateCode.SpecialContentInput, info2.SpecialId))
                    {
                        item2.AnchorType = "1";
                    }
                    else
                    {
                        item2.AnchorType = "0";
                    }
                    item2.XmlSrc = "";
                    item2.Action = str3;
                    xTreeList.Add(item2);
                }
            }
        }
    }
}

