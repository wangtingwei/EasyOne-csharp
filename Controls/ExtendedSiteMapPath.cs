namespace EasyOne.Controls
{
    using EasyOne.Components;
    using System;
    using System.ComponentModel;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [ToolboxData("<{0}:ExtendedSiteMapPath ID=\"Esp\" runat=\"server\" />"), Themeable(true)]
    public class ExtendedSiteMapPath : SiteMapPath
    {
        private SiteMapNode tempNode;

        private void AddLiteralAfterCurrentNode(SiteMapNodeItem item)
        {
            if (item.SiteMapNode.ChildNodes != null)
            {
                SiteMapNodeItem item2 = new SiteMapNodeItem(item.ItemIndex, SiteMapNodeItemType.PathSeparator);
                SiteMapNodeItemEventArgs e = new SiteMapNodeItemEventArgs(item2);
                this.InitializeItem(item2);
                this.OnItemCreated(e);
                item.Controls.Add(item2);
                Literal child = new Literal {
                    Mode = LiteralMode.PassThrough,
                    Text = this.AdditionalNode
                };
                item.Controls.Add(child);
            }
        }

        protected override void InitializeItem(SiteMapNodeItem item)
        {
            base.InitializeItem(item);
            if ((item.ItemType == SiteMapNodeItemType.Current) && !string.IsNullOrEmpty(this.AdditionalNode))
            {
                this.AddLiteralAfterCurrentNode(item);
            }
        }

        protected override void OnItemCreated(SiteMapNodeItemEventArgs e)
        {
            base.OnItemCreated(e);
            if (e.Item.ItemType == SiteMapNodeItemType.Root)
            {
                if (e.Item.Controls[0] is Literal)
                {
                    Literal literal = e.Item.Controls[0] as Literal;
                    literal.Mode = LiteralMode.PassThrough;
                    literal.Text = literal.Text.Replace("{SiteName}", SiteConfig.SiteInfo.SiteName);
                }
                else if (e.Item.Controls[0] is HyperLink)
                {
                    HyperLink link = e.Item.Controls[0] as HyperLink;
                    link.NavigateUrl = SiteConfig.SiteInfo.SiteUrl;
                    link.ToolTip = link.ToolTip.Replace("{SiteName}", SiteConfig.SiteInfo.SiteName);
                    link.Text = link.Text.Replace("{SiteName}", SiteConfig.SiteInfo.SiteName);
                }
            }
            if (e.Item.ItemType == SiteMapNodeItemType.Current)
            {
                this.tempNode = e.Item.SiteMapNode;
                if (e.Item.Controls[0] is Literal)
                {
                    Literal literal2 = e.Item.Controls[0] as Literal;
                    literal2.Mode = LiteralMode.PassThrough;
                    literal2.Text = Regex.Replace(literal2.Text, @"\{\$[^}]+\}", new MatchEvaluator(this.UrlVarMatch));
                    literal2.Text = Regex.Replace(literal2.Text, @"\{\@[^}]+\}", new MatchEvaluator(this.UrlTypeMatch));
                }
                else if (e.Item.Controls[0] is HyperLink)
                {
                    HyperLink link2 = e.Item.Controls[0] as HyperLink;
                    link2.NavigateUrl = null;
                    link2.ToolTip = null;
                    link2.Text = Regex.Replace(link2.Text, @"\{\$[^}]+\}", new MatchEvaluator(this.UrlVarMatch));
                    link2.Text = Regex.Replace(link2.Text, @"\{\@[^}]+\}", new MatchEvaluator(this.UrlTypeMatch));
                    link2.Text = HttpContext.Current.Server.HtmlDecode(link2.Text);
                }
                this.tempNode = null;
            }
            if (!string.IsNullOrEmpty(this.CurrentNode) && (e.Item.ItemType == SiteMapNodeItemType.Current))
            {
                if (e.Item.Controls[0] is Literal)
                {
                    Literal literal3 = e.Item.Controls[0] as Literal;
                    literal3.Mode = LiteralMode.PassThrough;
                    literal3.Text = this.CurrentNode;
                }
                else if (e.Item.Controls[0] is HyperLink)
                {
                    HyperLink link3 = e.Item.Controls[0] as HyperLink;
                    link3.NavigateUrl = null;
                    link3.ToolTip = null;
                    link3.Text = this.CurrentNode;
                }
            }
            if ((!string.IsNullOrEmpty(this.ParentNodeUrl) && (e.Item.ItemType == SiteMapNodeItemType.Parent)) && (e.Item.Controls[0] is HyperLink))
            {
                HyperLink link4 = e.Item.Controls[0] as HyperLink;
                link4.NavigateUrl = this.ParentNodeUrl;
            }
        }

        private string UrlTypeMatch(Match match)
        {
            if (this.tempNode != null)
            {
                string str = match.ToString().Replace("{@", "").Replace("}", "");
                string str2 = HttpContext.Current.Request.QueryString[str];
                if (str2 == null)
                {
                    return "";
                }
                string str3 = "PE" + str;
                if (this.tempNode[str3] == null)
                {
                    return "";
                }
                foreach (string str4 in this.tempNode[str3].Split(new char[] { ',' }))
                {
                    string[] strArray2 = str4.Split(new char[] { '|' });
                    if ((strArray2.Length >= 2) && strArray2[0].Equals(str2, StringComparison.InvariantCultureIgnoreCase))
                    {
                        return strArray2[1];
                    }
                }
            }
            return "";
        }

        private string UrlVarMatch(Match match)
        {
            string str = match.ToString().Replace("{$", "").Replace("}", "");
            string str2 = HttpContext.Current.Request.QueryString[str];
            if (str2 != null)
            {
                return str2;
            }
            return "";
        }

        [Bindable(true), Localizable(true), Category("自定义"), DefaultValue(""), Description("当前节点后附加的HTML节点")]
        public string AdditionalNode
        {
            get
            {
                string str = (string) this.ViewState["AdditionalNode"];
                if (str != null)
                {
                    return str;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["AdditionalNode"] = value;
            }
        }

        [Category("自定义"), Description("当前节点的HTML节点"), Localizable(true), Bindable(true), DefaultValue("")]
        public string CurrentNode
        {
            get
            {
                string str = (string) this.ViewState["CurrentNode"];
                if (str != null)
                {
                    return str;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["CurrentNode"] = value;
            }
        }

        [Category("自定义"), Bindable(true), Localizable(true), DefaultValue(""), Description("当前节点的父节点的URL地址")]
        public string ParentNodeUrl
        {
            get
            {
                string str = (string) this.ViewState["ParentNodeUrl"];
                if (str != null)
                {
                    return str;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["ParentNodeUrl"] = value;
            }
        }
    }
}

