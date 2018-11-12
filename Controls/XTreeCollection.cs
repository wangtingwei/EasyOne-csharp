namespace EasyOne.Controls
{
    using EasyOne.Common;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class XTreeCollection : List<XTreeItem>
    {
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>\n");
            builder.Append("<tree>");
            foreach (XTreeItem item in this)
            {
                builder.Append("<tree ");
                builder.Append("text=\"" + DataSecurity.XmlEncode(item.Text) + "\" ");
                builder.Append("title=\"" + DataSecurity.XmlEncode(item.Title) + "\" ");
                builder.Append("arrModelId=\"" + DataSecurity.XmlEncode(item.ArrModelId) + "\" ");
                builder.Append("arrModelName=\"" + DataSecurity.XmlEncode(item.ArrModelName) + "\" ");
                builder.Append("arrPurview=\"" + DataSecurity.XmlEncode(item.ArrPurview) + "\" ");
                builder.Append("nodeId=\"" + DataSecurity.XmlEncode(item.NodeId) + "\" ");
                builder.Append("target=\"" + DataSecurity.XmlEncode(item.Target) + "\" ");
                builder.Append("expand=\"" + DataSecurity.XmlEncode(item.Expand) + "\" ");
                builder.Append("action=\"" + DataSecurity.XmlEncode(item.Action) + "\" ");
                builder.Append("src=\"" + DataSecurity.XmlEncode(item.XmlSrc) + "\" ");
                builder.Append("anchorType=\"" + DataSecurity.XmlEncode(item.AnchorType) + "\" ");
                builder.Append("icon=\"" + DataSecurity.XmlEncode(item.Icon) + "\" ");
                builder.Append("nodeType=\"" + DataSecurity.XmlEncode(item.NodeType) + "\" ");
                builder.Append("enable=\"" + DataSecurity.XmlEncode(item.Enable) + "\" ");
                builder.Append(" />");
            }
            builder.Append("</tree>\n");
            return builder.ToString();
        }
    }
}

