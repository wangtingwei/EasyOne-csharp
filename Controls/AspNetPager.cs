namespace EasyOne.Controls
{
    using System;
    using System.Collections;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    [PersistChildren(false), Description("专用于ASP.Net Web应用程序的分页控件"), ToolboxData("<{0}:AspNetPager runat=server></{0}:AspNetPager>"), Designer(typeof(PagerDesigner)), DefaultProperty("PageSize"), DefaultEvent("PageChanged"), ParseChildren(false)]
    public class AspNetPager : Panel, INamingContainer, IPostBackEventHandler, IPostBackDataHandler
    {
        private string cssClassName;
        private string currentUrl;
        private string inputPageIndex;
        private string inputPageSize;
        private string urlPageIndexName;
        private bool urlPaging;
        private NameValueCollection urlParams;

        public event EventHandler<PageChangedEventArgs> PageChanged;

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            if (this.Page != null)
            {
                this.Page.VerifyRenderingInServerForm(this);
            }
            base.AddAttributesToRender(writer);
        }

        private void AddToolTip(HtmlTextWriter writer, int pageIndex)
        {
            if (this.ShowNavigationToolTip)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Title, string.Format(this.NavigationToolTipTextFormatString, pageIndex));
            }
        }

        private string BuildUrlString(NameValueCollection col)
        {
            int num;
            string str = "";
            if ((this.urlParams == null) || (this.urlParams.Count <= 0))
            {
                for (num = 0; num < col.Count; num++)
                {
                    str = str + ("&" + col.Keys[num] + "=" + col[num]);
                }
                return (this.currentUrl + "?" + str.Substring(1));
            }
            NameValueCollection values = new NameValueCollection(this.urlParams);
            string[] allKeys = values.AllKeys;
            for (num = 0; num < allKeys.Length; num++)
            {
                allKeys[num] = allKeys[num].ToLower();
            }
            for (num = 0; num < col.Count; num++)
            {
                if (Array.IndexOf<string>(allKeys, col.Keys[num].ToLower()) < 0)
                {
                    values.Add(col.Keys[num], col[num]);
                }
                else
                {
                    values[col.Keys[num]] = col[num];
                }
            }
            StringBuilder builder = new StringBuilder();
            for (num = 0; num < values.Count; num++)
            {
                builder.Append("&");
                builder.Append(values.Keys[num]);
                builder.Append("=");
                builder.Append(values[num]);
            }
            return (this.currentUrl + "?" + builder.ToString().Substring(1));
        }

        private void CreateInputBox(HtmlTextWriter writer)
        {
            if ((this.ShowInputBox == EasyOne.Controls.ShowInputBox.Always) || ((this.ShowInputBox == EasyOne.Controls.ShowInputBox.Auto) && (this.PageCount >= this.ShowBoxThreshold)))
            {
                writer.Write("&nbsp;&nbsp;&nbsp;&nbsp;");
                if (this.TextBeforeInputBox != null)
                {
                    writer.Write(this.TextBeforeInputBox);
                }
                writer.AddAttribute(HtmlTextWriterAttribute.Type, "text");
                writer.AddStyleAttribute(HtmlTextWriterStyle.Height, "12px");
                writer.AddStyleAttribute(HtmlTextWriterStyle.Width, "20px");
                writer.AddAttribute(HtmlTextWriterAttribute.Value, this.CurrentPageIndex.ToString());
                if ((this.InputBoxStyle != null) && (this.InputBoxStyle.Trim().Length > 0))
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Style, this.InputBoxStyle);
                }
                if ((this.InputBoxClass != null) && (this.InputBoxClass.Trim().Length > 0))
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, this.InputBoxClass);
                }
                if ((this.PageCount <= 1) && this.AlwaysShow)
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.ReadOnly, "true");
                }
                writer.AddAttribute(HtmlTextWriterAttribute.Name, this.UniqueID + "_input");
                writer.AddAttribute(HtmlTextWriterAttribute.Id, this.UniqueID + "_input");
                string str = "doCheck(document.getElementById('" + this.UniqueID + "_input'))";
                string str2 = "if(event.keyCode==13){if(" + str + ")__doPostBack('" + this.UniqueID + "',document.getElementById('" + this.UniqueID + "_input').value);else{event.returnValue=false;}}";
                string str3 = "if(event.keyCode==13){if(" + str + "){event.returnValue=false;document.all['" + this.UniqueID + "'][1].click();}else{event.returnValue=false;}}";
                string str4 = "if(" + str + "){location.href=BuildUrlString('" + this.urlPageIndexName + "',document.getElementById('" + this.UniqueID + "_input').value)}";
                writer.AddAttribute("onkeypress", this.urlPaging ? str3 : str2);
                writer.AddAttribute("onchange", this.urlPaging ? str3 : str2);
                writer.RenderBeginTag(HtmlTextWriterTag.Input);
                writer.RenderEndTag();
                if (this.TextAfterInputBox != null)
                {
                    writer.Write(this.TextAfterInputBox);
                }
                writer.AddAttribute(HtmlTextWriterAttribute.Type, this.urlPaging ? "Button" : "Submit");
                writer.AddAttribute(HtmlTextWriterAttribute.Name, this.UniqueID);
                writer.AddAttribute(HtmlTextWriterAttribute.Value, this.SubmitButtonText);
                if ((this.SubmitButtonClass != null) && (this.SubmitButtonClass.Trim().Length > 0))
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, this.SubmitButtonClass);
                }
                if ((this.SubmitButtonStyle != null) && (this.SubmitButtonStyle.Trim().Length > 0))
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Style, this.SubmitButtonStyle);
                }
                if ((this.PageCount <= 1) && this.AlwaysShow)
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "true");
                }
                writer.AddAttribute(HtmlTextWriterAttribute.Onclick, this.urlPaging ? str4 : ("return " + str));
                writer.RenderBeginTag(HtmlTextWriterTag.Input);
                writer.RenderEndTag();
            }
        }

        private void CreateMoreButton(HtmlTextWriter writer, int pageIndex)
        {
            this.WriteCssClass(writer);
            writer.AddAttribute(HtmlTextWriterAttribute.Href, this.GetHrefString(pageIndex));
            this.AddToolTip(writer, pageIndex);
            writer.RenderBeginTag(HtmlTextWriterTag.A);
            if ((this.PagingButtonType == EasyOne.Controls.PagingButtonType.Image) && (this.MoreButtonType == EasyOne.Controls.PagingButtonType.Image))
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Src, this.ImagePath + "more" + this.ButtonImageNameExtension + this.ButtonImageExtension);
                writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");
                writer.AddAttribute(HtmlTextWriterAttribute.Align, this.ButtonImageAlign.ToString());
                writer.RenderBeginTag(HtmlTextWriterTag.Img);
                writer.RenderEndTag();
            }
            else
            {
                writer.Write("...");
            }
            writer.RenderEndTag();
            writer.AddStyleAttribute(HtmlTextWriterStyle.Width, this.PagingButtonSpacing.ToString());
            writer.RenderBeginTag(HtmlTextWriterTag.Span);
            writer.RenderEndTag();
        }

        private void CreateNavigationButton(HtmlTextWriter writer, string btnname)
        {
            if ((this.ShowFirstLast || ((btnname != "first") && (btnname != "last"))) && (this.ShowPrevNext || ((btnname != "prev") && (btnname != "next"))))
            {
                bool flag;
                int num;
                string str = "";
                bool flag2 = (this.PagingButtonType == EasyOne.Controls.PagingButtonType.Image) && (this.NavigationButtonType == EasyOne.Controls.PagingButtonType.Image);
                if ((btnname == "prev") || (btnname == "first"))
                {
                    flag = this.CurrentPageIndex <= 1;
                    if (!this.ShowDisabledButtons && flag)
                    {
                        return;
                    }
                    num = (btnname == "first") ? 1 : (this.CurrentPageIndex - 1);
                    if (flag2)
                    {
                        if (!flag)
                        {
                            writer.AddAttribute(HtmlTextWriterAttribute.Href, this.GetHrefString(num));
                            this.AddToolTip(writer, num);
                            writer.RenderBeginTag(HtmlTextWriterTag.A);
                            writer.AddAttribute(HtmlTextWriterAttribute.Src, this.ImagePath + btnname + this.ButtonImageNameExtension + this.ButtonImageExtension);
                            writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");
                            writer.AddAttribute(HtmlTextWriterAttribute.Align, this.ButtonImageAlign.ToString());
                            writer.RenderBeginTag(HtmlTextWriterTag.Img);
                            writer.RenderEndTag();
                            writer.RenderEndTag();
                        }
                        else
                        {
                            writer.AddAttribute(HtmlTextWriterAttribute.Src, this.ImagePath + btnname + this.DisabledButtonImageNameExtension + this.ButtonImageExtension);
                            writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");
                            writer.AddAttribute(HtmlTextWriterAttribute.Align, this.ButtonImageAlign.ToString());
                            writer.RenderBeginTag(HtmlTextWriterTag.Img);
                            writer.RenderEndTag();
                        }
                    }
                    else
                    {
                        str = (btnname == "prev") ? this.PrevPageText : this.FirstPageText;
                        if (flag)
                        {
                            writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled");
                        }
                        else
                        {
                            this.WriteCssClass(writer);
                            this.AddToolTip(writer, num);
                            writer.AddAttribute(HtmlTextWriterAttribute.Href, this.GetHrefString(num));
                        }
                        writer.RenderBeginTag(HtmlTextWriterTag.A);
                        writer.Write(str);
                        writer.RenderEndTag();
                    }
                }
                else
                {
                    flag = this.CurrentPageIndex >= this.PageCount;
                    if (!this.ShowDisabledButtons && flag)
                    {
                        return;
                    }
                    num = (btnname == "last") ? this.PageCount : (this.CurrentPageIndex + 1);
                    if (flag2)
                    {
                        if (!flag)
                        {
                            writer.AddAttribute(HtmlTextWriterAttribute.Href, this.GetHrefString(num));
                            this.AddToolTip(writer, num);
                            writer.RenderBeginTag(HtmlTextWriterTag.A);
                            writer.AddAttribute(HtmlTextWriterAttribute.Src, this.ImagePath + btnname + this.ButtonImageNameExtension + this.ButtonImageExtension);
                            writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");
                            writer.AddAttribute(HtmlTextWriterAttribute.Align, this.ButtonImageAlign.ToString());
                            writer.RenderBeginTag(HtmlTextWriterTag.Img);
                            writer.RenderEndTag();
                            writer.RenderEndTag();
                        }
                        else
                        {
                            writer.AddAttribute(HtmlTextWriterAttribute.Src, this.ImagePath + btnname + this.DisabledButtonImageNameExtension + this.ButtonImageExtension);
                            writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");
                            writer.AddAttribute(HtmlTextWriterAttribute.Align, this.ButtonImageAlign.ToString());
                            writer.RenderBeginTag(HtmlTextWriterTag.Img);
                            writer.RenderEndTag();
                        }
                    }
                    else
                    {
                        str = (btnname == "next") ? this.NextPageText : this.LastPageText;
                        if (flag)
                        {
                            writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled");
                        }
                        else
                        {
                            this.WriteCssClass(writer);
                            this.AddToolTip(writer, num);
                            writer.AddAttribute(HtmlTextWriterAttribute.Href, this.GetHrefString(num));
                        }
                        writer.RenderBeginTag(HtmlTextWriterTag.A);
                        writer.Write(str);
                        writer.RenderEndTag();
                    }
                }
                this.WriteButtonSpace(writer);
            }
        }

        private void CreateNumericButton(HtmlTextWriter writer, int index)
        {
            bool isCurrent = index == this.CurrentPageIndex;
            if ((this.PagingButtonType == EasyOne.Controls.PagingButtonType.Image) && (this.NumericButtonType == EasyOne.Controls.PagingButtonType.Image))
            {
                if (!isCurrent)
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Href, this.GetHrefString(index));
                    this.AddToolTip(writer, index);
                    writer.RenderBeginTag(HtmlTextWriterTag.A);
                    this.CreateNumericImages(writer, index, isCurrent);
                    writer.RenderEndTag();
                }
                else
                {
                    this.CreateNumericImages(writer, index, isCurrent);
                }
            }
            else if (isCurrent)
            {
                writer.AddStyleAttribute(HtmlTextWriterStyle.FontWeight, "Bold");
                writer.AddStyleAttribute(HtmlTextWriterStyle.Color, "red");
                writer.RenderBeginTag(HtmlTextWriterTag.Font);
                if (this.NumericButtonTextFormatString.Length > 0)
                {
                    writer.Write(string.Format(this.NumericButtonTextFormatString, this.ChinesePageIndex ? GetChinesePageIndex(index) : index.ToString()));
                }
                else
                {
                    writer.Write(this.ChinesePageIndex ? GetChinesePageIndex(index) : index.ToString());
                }
                writer.RenderEndTag();
            }
            else
            {
                this.WriteCssClass(writer);
                this.AddToolTip(writer, index);
                writer.AddAttribute(HtmlTextWriterAttribute.Href, this.GetHrefString(index));
                writer.RenderBeginTag(HtmlTextWriterTag.A);
                if (this.NumericButtonTextFormatString.Length > 0)
                {
                    writer.Write(string.Format(this.NumericButtonTextFormatString, this.ChinesePageIndex ? GetChinesePageIndex(index) : index.ToString()));
                }
                else
                {
                    writer.Write(this.ChinesePageIndex ? GetChinesePageIndex(index) : index.ToString());
                }
                writer.RenderEndTag();
            }
            this.WriteButtonSpace(writer);
        }

        private void CreateNumericImages(HtmlTextWriter writer, int index, bool isCurrent)
        {
            string str = index.ToString();
            for (int i = 0; i < str.Length; i++)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Src, string.Concat(new object[] { this.ImagePath, str[i], isCurrent ? this.CpiButtonImageNameExtension : this.ButtonImageNameExtension, this.ButtonImageExtension }));
                writer.AddAttribute(HtmlTextWriterAttribute.Align, this.ButtonImageAlign.ToString());
                writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");
                writer.RenderBeginTag(HtmlTextWriterTag.Img);
                writer.RenderEndTag();
            }
        }

        private void CreatePageSize(HtmlTextWriter writer)
        {
            if (this.ShowPageSize)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Name, this.UniqueID + "_inputPageSize");
                writer.AddAttribute(HtmlTextWriterAttribute.Id, this.UniqueID + "_inputPageSize");
                writer.AddAttribute(HtmlTextWriterAttribute.Value, this.PageSize.ToString());
                writer.AddStyleAttribute(HtmlTextWriterStyle.Height, "12px");
                writer.AddStyleAttribute(HtmlTextWriterStyle.Width, "20px");
                this.Page.ClientScript.GetPostBackClientHyperlink(this, "");
                string str = "if(doCheckPageSize(document.getElementById('" + this.UniqueID + "_inputPageSize')))__doPostBack('" + this.UniqueID + "','');else{event.returnValue=false;}";
                writer.AddAttribute("onchange", str);
                writer.RenderBeginTag(HtmlTextWriterTag.Input);
                writer.RenderEndTag();
                writer.Write("个/页");
                writer.Write("&nbsp;&nbsp;");
            }
        }

        private static string GetChinesePageIndex(int index)
        {
            Hashtable hashtable = new Hashtable();
            hashtable.Add("0", "零");
            hashtable.Add("1", "一");
            hashtable.Add("2", "二");
            hashtable.Add("3", "三");
            hashtable.Add("4", "四");
            hashtable.Add("5", "五");
            hashtable.Add("6", "六");
            hashtable.Add("7", "七");
            hashtable.Add("8", "八");
            hashtable.Add("9", "九");
            string str = index.ToString();
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {
                char ch = str[i];
                builder.Append(hashtable[ch.ToString()]);
            }
            return builder.ToString();
        }

        private string GetHrefString(int pageIndex)
        {
            if (this.urlPaging)
            {
                NameValueCollection col = new NameValueCollection();
                col.Add(this.urlPageIndexName, pageIndex.ToString());
                return this.BuildUrlString(col);
            }
            return this.Page.ClientScript.GetPostBackClientHyperlink(this, pageIndex.ToString());
        }

        private string GetStyleString()
        {
            if (base.Style.Count <= 0)
            {
                return null;
            }
            string[] array = new string[base.Style.Count];
            base.Style.Keys.CopyTo(array, 0);
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < array.Length; i++)
            {
                builder.Append(array[i] + ":" + base.Style[array[i]] + ";");
            }
            return builder.ToString();
        }

        public virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
        {
            string s = postCollection[this.UniqueID + "_input"];
            if ((s != null) && (s.Trim().Length > 0))
            {
                try
                {
                    if (int.Parse(s) > 0)
                    {
                        this.inputPageIndex = s;
                        this.Page.RegisterRequiresRaiseEvent(this);
                    }
                }
                catch
                {
                }
            }
            this.Page.RegisterRequiresRaiseEvent(this);
            return false;
        }

        protected override void OnLoad(EventArgs e)
        {
            if (this.urlPaging)
            {
                this.currentUrl = this.Page.Request.Path;
                this.urlParams = this.Page.Request.QueryString;
                string s = this.Page.Request.QueryString[this.urlPageIndexName];
                int newPageIndex = 1;
                try
                {
                    newPageIndex = int.Parse(s);
                }
                catch
                {
                }
                this.inputPageSize = this.Page.Request.Form[this.UniqueID + "_inputPageSize"];
                if (!string.IsNullOrEmpty(this.Page.Request.Form[this.UniqueID + "_inputPageSize"]))
                {
                    this.PageSize = int.Parse(this.Page.Request.Form[this.UniqueID + "_inputPageSize"]);
                }
                this.OnPageChanged(new PageChangedEventArgs(newPageIndex));
            }
            else
            {
                this.inputPageIndex = this.Page.Request.Form[this.UniqueID + "_input"];
                this.inputPageSize = this.Page.Request.Form[this.UniqueID + "_inputPageSize"];
            }
            base.OnLoad(e);
        }

        protected virtual void OnPageChanged(PageChangedEventArgs e)
        {
            if (this.PageChanged != null)
            {
                this.PageChanged(this, e);
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            if (this.PageCount >= 1)
            {
                string script = "<script language=\"Javascript\">function doCheck(el){var r=new RegExp(\"^\\\\s*(\\\\d+)\\\\s*$\");if(r.test(el.value)){if(RegExp.$1<1||RegExp.$1>" + this.PageCount.ToString() + "){alert(\"" + this.PageIndexOutOfRangeErrorString + "\");return false;}return true;}alert(\"" + this.InvalidPageIndexErrorString + "\");return false;}function doCheckPageSize(el){var r=new RegExp(\"^\\\\s*(\\\\d+)\\\\s*$\");if(r.test(el.value)){if(RegExp.$1<1||RegExp.$1>500){alert(\"一页显示记录数不得超过500\");return false;}return true;}alert(\"请输入正确的字符\");return false;}</script>";
                if ((this.ShowInputBox == EasyOne.Controls.ShowInputBox.Always) || ((this.ShowInputBox == EasyOne.Controls.ShowInputBox.Auto) && (this.PageCount >= this.ShowBoxThreshold)))
                {
                    if (!this.Page.ClientScript.IsClientScriptBlockRegistered("AspNetPager_checkinput"))
                    {
                        this.Page.ClientScript.RegisterClientScriptBlock(base.GetType(), "AspNetPager_checkinput", script);
                    }
                    string str2 = "<script language=\"javascript\" > <!-- \nfunction BuildUrlString(key,value){ var _key=key.toLowerCase(); var prms=location.search; if(prms.length==0) return location.pathname+\"?\"+_key+\"=\"+value; var params=prms.substring(1).split(\"&\"); var newparam=\"\"; var found=false; for(i=0;i<params.length;i++){ if(params[i].split(\"=\")[0].toLowerCase()==_key){ params[i]=_key+\"=\"+value; found=true; break; } } if(found) return location.pathname+\"?\"+params.join(\"&\"); else return location+\"&\"+_key+\"=\"+value; }\n//--> </script>";
                    if (!this.Page.ClientScript.IsClientScriptBlockRegistered("AspNetPager_BuildUrlScript"))
                    {
                        this.Page.ClientScript.RegisterClientScriptBlock(base.GetType(), "AspNetPager_BuildUrlScript", str2);
                    }
                }
            }
            base.OnPreRender(e);
        }

        public void RaisePostBackEvent(string eventArgument)
        {
            int currentPageIndex = this.CurrentPageIndex;
            try
            {
                if (!string.IsNullOrEmpty(this.inputPageSize) && (this.PageSize != int.Parse(this.inputPageSize)))
                {
                    this.PageSize = int.Parse(this.inputPageSize);
                    this.CurrentPageIndex = 1;
                    currentPageIndex = 1;
                }
                if (string.IsNullOrEmpty(eventArgument))
                {
                    eventArgument = this.inputPageIndex;
                }
                currentPageIndex = int.Parse(eventArgument);
            }
            catch
            {
            }
            this.OnPageChanged(new PageChangedEventArgs(currentPageIndex));
        }

        public virtual void RaisePostDataChangedEvent()
        {
        }

        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            bool flag = (this.PageCount > 1) || ((this.PageCount <= 1) && this.AlwaysShow);
            writer.WriteLine();
            base.RenderBeginTag(writer);
            if (((this.ShowCustomInfoSection == EasyOne.Controls.ShowCustomInfoSection.Left) || (this.ShowCustomInfoSection == EasyOne.Controls.ShowCustomInfoSection.Right)) && flag)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Width, "100%");
                writer.AddAttribute(HtmlTextWriterAttribute.Style, this.GetStyleString());
                if (this.Height != Unit.Empty)
                {
                    writer.AddStyleAttribute(HtmlTextWriterStyle.Height, this.Height.ToString());
                }
                writer.AddAttribute(HtmlTextWriterAttribute.Border, "0");
                writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
                writer.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
                writer.RenderBeginTag(HtmlTextWriterTag.Table);
                writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                writer.AddAttribute(HtmlTextWriterAttribute.Align, "center");
                writer.RenderBeginTag(HtmlTextWriterTag.Td);
            }
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            if ((this.PageCount >= 0) || this.AlwaysShow)
            {
                if (this.ShowCustomInfoSection == EasyOne.Controls.ShowCustomInfoSection.Left)
                {
                    writer.Write(this.CustomInfoText);
                    writer.RenderEndTag();
                    this.WriteCellAttributes(writer, false);
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, this.CssClass);
                    writer.RenderBeginTag(HtmlTextWriterTag.Td);
                }
                int num = (this.CurrentPageIndex - 1) / this.NumericButtonCount;
                int pageIndex = num * this.NumericButtonCount;
                int num3 = ((pageIndex + this.NumericButtonCount) > this.PageCount) ? this.PageCount : (pageIndex + this.NumericButtonCount);
                writer.Write("总共<font color='green'>" + this.RecordCount.ToString() + "</font>条记录");
                writer.Write("&nbsp;&nbsp;");
                this.CreateNavigationButton(writer, "first");
                writer.Write("&nbsp;&nbsp;");
                this.CreateNavigationButton(writer, "prev");
                writer.Write("&nbsp;&nbsp;");
                if (this.ShowPageIndex)
                {
                    if (this.CurrentPageIndex > this.NumericButtonCount)
                    {
                        this.CreateMoreButton(writer, pageIndex);
                    }
                    for (int i = pageIndex + 1; i <= num3; i++)
                    {
                        this.CreateNumericButton(writer, i);
                    }
                    if ((this.PageCount > this.NumericButtonCount) && (num3 < this.PageCount))
                    {
                        this.CreateMoreButton(writer, num3 + 1);
                    }
                }
                this.CreateNavigationButton(writer, "next");
                writer.Write("&nbsp;&nbsp;");
                this.CreateNavigationButton(writer, "last");
                writer.Write("&nbsp;&nbsp;");
                this.CreatePageSize(writer);
                writer.Write(string.Concat(new object[] { "页次：<font color='red'>", this.CurrentPageIndex.ToString(), "</font>/", this.PageCount, "页" }));
                this.CreateInputBox(writer);
                if (this.ShowCustomInfoSection == EasyOne.Controls.ShowCustomInfoSection.Right)
                {
                    writer.RenderEndTag();
                    this.WriteCellAttributes(writer, false);
                    writer.RenderBeginTag(HtmlTextWriterTag.Td);
                    writer.Write(this.CustomInfoText);
                }
            }
        }

        public override void RenderEndTag(HtmlTextWriter writer)
        {
            if (((this.ShowCustomInfoSection == EasyOne.Controls.ShowCustomInfoSection.Left) || (this.ShowCustomInfoSection == EasyOne.Controls.ShowCustomInfoSection.Right)) && ((this.PageCount > 1) || ((this.PageCount <= 1) && this.AlwaysShow)))
            {
                writer.RenderEndTag();
                writer.RenderEndTag();
                writer.RenderEndTag();
            }
            base.RenderEndTag(writer);
            writer.WriteLine();
        }

        private void WriteButtonSpace(HtmlTextWriter writer)
        {
            if (this.PagingButtonSpacing.Value > 0.0)
            {
                writer.AddStyleAttribute(HtmlTextWriterStyle.Width, this.PagingButtonSpacing.ToString());
                writer.RenderBeginTag(HtmlTextWriterTag.Span);
                writer.RenderEndTag();
            }
        }

        private void WriteCellAttributes(HtmlTextWriter writer, bool leftCell)
        {
            string str = this.CustomInfoSectionWidth.ToString();
            if (((this.ShowCustomInfoSection == EasyOne.Controls.ShowCustomInfoSection.Left) && leftCell) || ((this.ShowCustomInfoSection == EasyOne.Controls.ShowCustomInfoSection.Right) && !leftCell))
            {
                if ((this.CustomInfoClass != null) && (this.CustomInfoClass.Trim().Length > 0))
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, this.CustomInfoClass);
                }
                if ((this.CustomInfoStyle != null) && (this.CustomInfoStyle.Trim().Length > 0))
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Style, this.CustomInfoStyle);
                }
                writer.AddAttribute(HtmlTextWriterAttribute.Valign, "bottom");
                writer.AddStyleAttribute(HtmlTextWriterStyle.Width, str);
                writer.AddAttribute(HtmlTextWriterAttribute.Align, this.CustomInfoTextAlign.ToString().ToLower());
            }
            else
            {
                if (this.CustomInfoSectionWidth.Type == UnitType.Percentage)
                {
                    str = Unit.Percentage(100.0 - this.CustomInfoSectionWidth.Value).ToString();
                    writer.AddStyleAttribute(HtmlTextWriterStyle.Width, str);
                }
                writer.AddAttribute(HtmlTextWriterAttribute.Valign, "bottom");
            }
            writer.AddAttribute(HtmlTextWriterAttribute.Nowrap, "true");
        }

        private void WriteCssClass(HtmlTextWriter writer)
        {
            if ((this.cssClassName != null) && (this.cssClassName.Trim().Length > 0))
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, this.cssClassName);
            }
        }

        [Category("Behavior"), DefaultValue(false), Description("总是显示分页控件，即使要分页的数据只要一页"), Browsable(true)]
        public bool AlwaysShow
        {
            get
            {
                object obj2 = this.ViewState["AlwaysShow"];
                return ((obj2 != null) && ((bool) obj2));
            }
            set
            {
                this.ViewState["AlwaysShow"] = value;
            }
        }

        [Category("图片按钮"), DefaultValue(3), Description("指定当使用图片按钮时，图片的对齐方式"), Browsable(true)]
        public ImageAlign ButtonImageAlign
        {
            get
            {
                object obj2 = this.ViewState["ButtonImageAlign"];
                if (obj2 != null)
                {
                    return (ImageAlign) obj2;
                }
                return ImageAlign.Baseline;
            }
            set
            {
                this.ViewState["ButtonImageAlign"] = value;
            }
        }

        [Browsable(true), DefaultValue(".gif"), Category("图片按钮"), Description("当使用图片按钮时，图片的类型，如gif或jpg，该值即图片文件的后缀名")]
        public string ButtonImageExtension
        {
            get
            {
                object obj2 = this.ViewState["ButtonImageExtension"];
                if (obj2 != null)
                {
                    return (string) obj2;
                }
                return ".gif";
            }
            set
            {
                string str = value.Trim();
                this.ViewState["ButtonImageExtension"] = str.StartsWith(".", StringComparison.Ordinal) ? str : ("." + str);
            }
        }

        [Category("图片按钮"), DefaultValue((string) null), Description("自定义图片文件名的后缀字符串（非文件后缀名），如图片“1f.gif”的ButtonImageNameExtension即为“f”"), Browsable(true)]
        public string ButtonImageNameExtension
        {
            get
            {
                return (string) this.ViewState["ButtonImageNameExtension"];
            }
            set
            {
                this.ViewState["ButtonImageNameExtension"] = value;
            }
        }

        [Description("是否将页索引数值按钮用中文数字一、二、三等代替"), DefaultValue(false), Browsable(true), Category("导航按钮")]
        public bool ChinesePageIndex
        {
            get
            {
                object obj2 = this.ViewState["ChinesePageIndex"];
                return ((obj2 != null) && ((bool) obj2));
            }
            set
            {
                this.ViewState["ChinesePageIndex"] = value;
            }
        }

        [DefaultValue((string) null), Description("当前页索引按钮的图片名后缀字符串"), Category("图片按钮"), Browsable(true)]
        public string CpiButtonImageNameExtension
        {
            get
            {
                object obj2 = this.ViewState["CpiButtonImageNameExtension"];
                if (obj2 != null)
                {
                    return (string) obj2;
                }
                return this.ButtonImageNameExtension;
            }
            set
            {
                this.ViewState["CpiButtonImageNameExtension"] = value;
            }
        }

        [Browsable(true), Category("Appearance"), DefaultValue((string) null), Description("应用于控件的CSS类名")]
        public override string CssClass
        {
            get
            {
                return base.CssClass;
            }
            set
            {
                base.CssClass = value;
                this.cssClassName = value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), ReadOnly(true), Browsable(false), Description("当前显示页的索引"), Category("分页"), DefaultValue(1)]
        public int CurrentPageIndex
        {
            get
            {
                object obj2 = this.ViewState["CurrentPageIndex"];
                if ((this.Page != null) && (this.Page.Session != null))
                {
                    string str = this.Page.GetType().Name + "_" + this.UniqueID + "_CurrentPageIndex";
                    if (this.Page.Session[str] != null)
                    {
                        obj2 = this.Page.Session[str];
                    }
                }
                int num = (obj2 == null) ? 1 : ((int) obj2);
                if ((num > this.PageCount) && (this.PageCount > 0))
                {
                    return this.PageCount;
                }
                if (num < 1)
                {
                    return 1;
                }
                return num;
            }
            set
            {
                int pageCount = value;
                if (pageCount < 1)
                {
                    pageCount = 1;
                }
                else if (pageCount > this.PageCount)
                {
                    pageCount = this.PageCount;
                }
                if ((this.Page != null) && (this.Page.Session != null))
                {
                    string str = this.Page.GetType().Name + "_" + this.UniqueID + "_CurrentPageIndex";
                    this.Page.Session[str] = value;
                }
                else
                {
                    this.ViewState["CurrentPageIndex"] = pageCount;
                }
            }
        }

        [Category("自定义信息区"), DefaultValue((string) null), Browsable(true), Description("应用于用户自定义信息区的级联样式表类名")]
        public string CustomInfoClass
        {
            get
            {
                object obj2 = this.ViewState["CustomInfoClass"];
                if (obj2 != null)
                {
                    return (string) obj2;
                }
                return this.CssClass;
            }
            set
            {
                this.ViewState["CustomInfoClass"] = value;
            }
        }

        [Category("自定义信息区"), Description("用户自定义信息区的宽度"), Browsable(true), DefaultValue(typeof(Unit), "40%")]
        public Unit CustomInfoSectionWidth
        {
            get
            {
                object obj2 = this.ViewState["CustomInfoSectionWidth"];
                if (obj2 != null)
                {
                    return (Unit) obj2;
                }
                return Unit.Percentage(40.0);
            }
            set
            {
                this.ViewState["CustomInfoSectionWidth"] = value;
            }
        }

        [Category("自定义信息区"), Description("应用于用户自定义信息区的CSS样式文本"), DefaultValue((string) null), Browsable(true)]
        public string CustomInfoStyle
        {
            get
            {
                object obj2 = this.ViewState["CustomInfoStyle"];
                if (obj2 != null)
                {
                    return (string) obj2;
                }
                return this.GetStyleString();
            }
            set
            {
                this.ViewState["CustomInfoStyle"] = value;
            }
        }

        [Description("要显示在用户自定义信息区的用户自定义信息文本"), Category("自定义信息区"), Browsable(true), DefaultValue((string) null)]
        public string CustomInfoText
        {
            get
            {
                return (string) this.ViewState["CustomInfoText"];
            }
            set
            {
                this.ViewState["CustomInfoText"] = value;
            }
        }

        [DefaultValue(1), Category("自定义信息区"), Browsable(true), Description("用户自定义信息区文本的对齐方式")]
        public HorizontalAlign CustomInfoTextAlign
        {
            get
            {
                object obj2 = this.ViewState["CustomInfoTextAlign"];
                if (obj2 != null)
                {
                    return (HorizontalAlign) obj2;
                }
                return HorizontalAlign.Left;
            }
            set
            {
                this.ViewState["CustomInfoTextAlign"] = value;
            }
        }

        [Browsable(true), DefaultValue((string) null), Description("已禁用的页导航按钮的图片名后缀字符串"), Category("图片按钮")]
        public string DisabledButtonImageNameExtension
        {
            get
            {
                object obj2 = this.ViewState["DisabledButtonImageNameExtension"];
                if (obj2 != null)
                {
                    return (string) obj2;
                }
                return this.ButtonImageNameExtension;
            }
            set
            {
                this.ViewState["DisabledButtonImageNameExtension"] = value;
            }
        }

        [DefaultValue(true), Category("Behavior"), Browsable(false), Description("是否启用控件的视图状态，该属性的值必须为true，不允许用户设置。")]
        public override bool EnableViewState
        {
            get
            {
                return base.EnableViewState;
            }
            set
            {
                base.EnableViewState = true;
            }
        }

        [Description("第一页按钮上显示的文本"), Category("导航按钮"), DefaultValue("<font face=\"webdings\">9</font>首页"), Browsable(true)]
        public string FirstPageText
        {
            get
            {
                object obj2 = this.ViewState["FirstPageText"];
                if (obj2 != null)
                {
                    return (string) obj2;
                }
                return "首页";
            }
            set
            {
                this.ViewState["FirstPageText"] = value;
            }
        }

        [Browsable(true), Category("图片按钮"), Description("当使用图片按钮时，指定图片文件的路径"), DefaultValue((string) null)]
        public string ImagePath
        {
            get
            {
                string relativeUrl = (string) this.ViewState["ImagePath"];
                if (relativeUrl != null)
                {
                    relativeUrl = base.ResolveUrl(relativeUrl);
                }
                return relativeUrl;
            }
            set
            {
                string str = value.Trim().Replace(@"\", "/");
                this.ViewState["ImagePath"] = str.EndsWith("/", StringComparison.Ordinal) ? str : (str + "/");
            }
        }

        [Browsable(true), Category("文本框及提交按钮"), DefaultValue((string) null), Description("应用于页索引输入文本框的CSS类名")]
        public string InputBoxClass
        {
            get
            {
                return (string) this.ViewState["InputBoxClass"];
            }
            set
            {
                if (value.Trim().Length > 0)
                {
                    this.ViewState["InputBoxClass"] = value;
                }
            }
        }

        [DefaultValue((string) null), Category("文本框及提交按钮"), Description("应用于页索引输入文本框的CSS样式文本"), Browsable(true)]
        public string InputBoxStyle
        {
            get
            {
                return (string) this.ViewState["InputBoxStyle"];
            }
            set
            {
                if (value.Trim().Length > 0)
                {
                    this.ViewState["InputBoxStyle"] = value;
                }
            }
        }

        [DefaultValue("页索引无效！"), Browsable(true), Description("当用户输入无效的页索引（负值或非数字）时在客户端显示的错误信息。"), Category("Data")]
        public string InvalidPageIndexErrorString
        {
            get
            {
                object obj2 = this.ViewState["InvalidPageIndexErrorString"];
                if (obj2 != null)
                {
                    return (string) obj2;
                }
                return "页索引无效！";
            }
            set
            {
                this.ViewState["InvalidPageIndexErrorString"] = value;
            }
        }

        [Browsable(true), Description("最后一页按钮上显示的文本"), DefaultValue("<font face=\"webdings\">:</font>尾页"), Category("导航按钮")]
        public string LastPageText
        {
            get
            {
                object obj2 = this.ViewState["LastPageText"];
                if (obj2 != null)
                {
                    return (string) obj2;
                }
                return "尾页";
            }
            set
            {
                this.ViewState["LastPageText"] = value;
            }
        }

        [Description("“更多页”（...）按钮的类型"), Browsable(true), Category("导航按钮"), DefaultValue(0)]
        public EasyOne.Controls.PagingButtonType MoreButtonType
        {
            get
            {
                object obj2 = this.ViewState["MoreButtonType"];
                if (obj2 != null)
                {
                    return (EasyOne.Controls.PagingButtonType) obj2;
                }
                return this.PagingButtonType;
            }
            set
            {
                this.ViewState["MoreButtonType"] = value;
            }
        }

        [Category("导航按钮"), Description("第一页、上一页、下一页和最后一页按钮的类型"), Browsable(true), DefaultValue(0)]
        public EasyOne.Controls.PagingButtonType NavigationButtonType
        {
            get
            {
                object obj2 = this.ViewState["NavigationButtonType"];
                if (obj2 != null)
                {
                    return (EasyOne.Controls.PagingButtonType) obj2;
                }
                return this.PagingButtonType;
            }
            set
            {
                this.ViewState["NavigationButtonType"] = value;
            }
        }

        [DefaultValue("转到第{0}页"), Description("页导航按钮工具提示文本的格式"), Browsable(true), Category("导航按钮")]
        public string NavigationToolTipTextFormatString
        {
            get
            {
                object obj2 = this.ViewState["NavigationToolTipTextFormatString"];
                if (obj2 != null)
                {
                    return (string) obj2;
                }
                return "转到第{0}页";
            }
            set
            {
                string str = value;
                if ((str.Trim().Length < 1) && (str.IndexOf("{0}", StringComparison.Ordinal) < 0))
                {
                    str = "{0}";
                }
                this.ViewState["NavigationToolTipTextFormatString"] = str;
            }
        }

        [Category("导航按钮"), Description("下一页按钮上显示的文本"), DefaultValue("<font face=\"webdings\">4</font>下一页"), Browsable(true)]
        public string NextPageText
        {
            get
            {
                object obj2 = this.ViewState["NextPageText"];
                if (obj2 != null)
                {
                    return (string) obj2;
                }
                return "下一页";
            }
            set
            {
                this.ViewState["NextPageText"] = value;
            }
        }

        [DefaultValue(10), Category("导航按钮"), Browsable(true), Description("要显示的页索引数值按钮的数目")]
        public int NumericButtonCount
        {
            get
            {
                object obj2 = this.ViewState["NumericButtonCount"];
                if (obj2 != null)
                {
                    return (int) obj2;
                }
                return 10;
            }
            set
            {
                this.ViewState["NumericButtonCount"] = value;
            }
        }

        [Description("页索引数值按钮上文字的显示格式"), Category("导航按钮"), Browsable(true), DefaultValue("")]
        public string NumericButtonTextFormatString
        {
            get
            {
                object obj2 = this.ViewState["NumericButtonTextFormatString"];
                if (obj2 != null)
                {
                    return (string) obj2;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["NumericButtonTextFormatString"] = value;
            }
        }

        [DefaultValue(0), Category("导航按钮"), Description("页导航数值按钮的类型"), Browsable(true)]
        public EasyOne.Controls.PagingButtonType NumericButtonType
        {
            get
            {
                object obj2 = this.ViewState["NumericButtonType"];
                if (obj2 != null)
                {
                    return (EasyOne.Controls.PagingButtonType) obj2;
                }
                return this.PagingButtonType;
            }
            set
            {
                this.ViewState["NumericButtonType"] = value;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int PageCount
        {
            get
            {
                return (int) Math.Ceiling((double) (((double) this.RecordCount) / ((double) this.PageSize)));
            }
        }

        [Category("Data"), Description("当用户输入的页索引超出范围（大于最大页索引或小于最小页索引）时在客户端显示的错误信息。"), DefaultValue("页数超出范围！"), Browsable(true)]
        public string PageIndexOutOfRangeErrorString
        {
            get
            {
                object obj2 = this.ViewState["PageIndexOutOfRangeErrorString"];
                if (obj2 != null)
                {
                    return (string) obj2;
                }
                return "页数超出范围！";
            }
            set
            {
                this.ViewState["PageIndexOutOfRangeErrorString"] = value;
            }
        }

        [Category("分页"), DefaultValue(20), Description("每页显示的记录数"), Browsable(true)]
        public int PageSize
        {
            get
            {
                if ((this.Page != null) && (this.Page.Session != null))
                {
                    string str = this.Page.GetType().Name + "_" + this.UniqueID + "_PageSize";
                    if (this.Page.Session[str] != null)
                    {
                        return (int) this.Page.Session[str];
                    }
                }
                object obj2 = this.ViewState["PageSize"];
                if (obj2 != null)
                {
                    return (int) obj2;
                }
                return 10;
            }
            set
            {
                if ((this.Page != null) && (this.Page.Session != null))
                {
                    string str = this.Page.GetType().Name + "_" + this.UniqueID + "_PageSize";
                    if (this.Page.Session != null)
                    {
                        this.Page.Session[str] = value;
                    }
                    else
                    {
                        this.ViewState["PageSize"] = value;
                    }
                }
                else
                {
                    this.ViewState["PageSize"] = value;
                }
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int PagesRemain
        {
            get
            {
                return (this.PageCount - this.CurrentPageIndex);
            }
        }

        [Browsable(true), Description("分页导航按钮之间的间距"), Category("导航按钮"), DefaultValue(typeof(Unit), "10px")]
        public Unit PagingButtonSpacing
        {
            get
            {
                object obj2 = this.ViewState["PagingButtonSpacing"];
                if (obj2 != null)
                {
                    return Unit.Parse(obj2.ToString());
                }
                return Unit.Pixel(10);
            }
            set
            {
                this.ViewState["PagingButtonSpacing"] = value;
            }
        }

        [DefaultValue(0), Description("分页导航按钮的类型，是使用文字还是图片"), Browsable(true), Category("导航按钮")]
        public EasyOne.Controls.PagingButtonType PagingButtonType
        {
            get
            {
                object obj2 = this.ViewState["PagingButtonType"];
                if (obj2 != null)
                {
                    return (EasyOne.Controls.PagingButtonType) obj2;
                }
                return EasyOne.Controls.PagingButtonType.Text;
            }
            set
            {
                this.ViewState["PagingButtonType"] = value;
            }
        }

        [DefaultValue("<font face=\"webdings\">3</font>上一页"), Category("导航按钮"), Description("上一页按钮上显示的文本"), Browsable(true)]
        public string PrevPageText
        {
            get
            {
                object obj2 = this.ViewState["PrevPageText"];
                if (obj2 != null)
                {
                    return (string) obj2;
                }
                return "上一页";
            }
            set
            {
                this.ViewState["PrevPageText"] = value;
            }
        }

        [DefaultValue(0xe1), Description("要分页的所有记录的总数，该值须在程序运行时设置，默认值为225是为设计时支持而设置的参照值。"), Category("Data"), Browsable(false)]
        public int RecordCount
        {
            get
            {
                object obj2 = this.ViewState["Recordcount"];
                if (obj2 != null)
                {
                    return (int) obj2;
                }
                return 0;
            }
            set
            {
                this.ViewState["Recordcount"] = value;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public int RecordsRemain
        {
            get
            {
                if (this.CurrentPageIndex < this.PageCount)
                {
                    return (this.RecordCount - (this.CurrentPageIndex * this.PageSize));
                }
                return 0;
            }
        }

        [Description("指定当ShowInputBox设为ShowInputBox.Auto时，当总页数达到多少时才显示页索引输入文本框"), Category("文本框及提交按钮"), DefaultValue(30), Browsable(true)]
        public int ShowBoxThreshold
        {
            get
            {
                object obj2 = this.ViewState["ShowBoxThreshold"];
                if (obj2 != null)
                {
                    return (int) obj2;
                }
                return 30;
            }
            set
            {
                this.ViewState["ShowBoxThreshold"] = value;
            }
        }

        [DefaultValue(0), Category("自定义信息区"), Description("显示当前页和总页数信息，默认值为不显示，值为ShowCustomInfoSection.Left时将显示在页索引前，为ShowCustomInfoSection.Right时将显示在页索引后"), Browsable(true)]
        public EasyOne.Controls.ShowCustomInfoSection ShowCustomInfoSection
        {
            get
            {
                object obj2 = this.ViewState["ShowCustomInfoSection"];
                if (obj2 != null)
                {
                    return (EasyOne.Controls.ShowCustomInfoSection) obj2;
                }
                return EasyOne.Controls.ShowCustomInfoSection.Never;
            }
            set
            {
                this.ViewState["ShowCustomInfoSection"] = value;
            }
        }

        [Category("导航按钮"), Browsable(true), Description("是否显示已禁用的按钮"), DefaultValue(true)]
        public bool ShowDisabledButtons
        {
            get
            {
                object obj2 = this.ViewState["ShowDisabledButtons"];
                if (obj2 != null)
                {
                    return (bool) obj2;
                }
                return true;
            }
            set
            {
                this.ViewState["ShowDisabledButtons"] = value;
            }
        }

        [DefaultValue(true), Browsable(true), Description("是否在页导航元素中显示第一页和最后一页按钮"), Category("导航按钮")]
        public bool ShowFirstLast
        {
            get
            {
                object obj2 = this.ViewState["ShowFirstLast"];
                if (obj2 != null)
                {
                    return (bool) obj2;
                }
                return true;
            }
            set
            {
                this.ViewState["ShowFirstLast"] = value;
            }
        }

        [DefaultValue(1), Browsable(true), Category("文本框及提交按钮"), Description("指定页索引文本框的显示方式")]
        public EasyOne.Controls.ShowInputBox ShowInputBox
        {
            get
            {
                object obj2 = this.ViewState["ShowInputBox"];
                if (obj2 != null)
                {
                    return (EasyOne.Controls.ShowInputBox) obj2;
                }
                return EasyOne.Controls.ShowInputBox.Always;
            }
            set
            {
                this.ViewState["ShowInputBox"] = value;
            }
        }

        [DefaultValue(true), Description("指定当鼠标停留在导航按钮上时，是否显示工具提示"), Category("导航按钮"), Browsable(true)]
        public bool ShowNavigationToolTip
        {
            get
            {
                object obj2 = this.ViewState["ShowNavigationToolTip"];
                if (obj2 != null)
                {
                    return (bool) obj2;
                }
                return true;
            }
            set
            {
                this.ViewState["ShowNavigationToolTip"] = value;
            }
        }

        [Category("导航按钮"), DefaultValue(true), Description("是否在页导航元素中显示数值按钮"), Browsable(true)]
        public bool ShowPageIndex
        {
            get
            {
                object obj2 = this.ViewState["ShowPageIndex"];
                return ((obj2 != null) && ((bool) obj2));
            }
            set
            {
                this.ViewState["ShowPageIndex"] = value;
            }
        }

        [Browsable(true), DefaultValue(true), Description("是否显示每页的总数"), Category("导航按钮")]
        public bool ShowPageSize
        {
            get
            {
                object obj2 = this.ViewState["ShowPageSize"];
                if (obj2 != null)
                {
                    return (bool) obj2;
                }
                return true;
            }
            set
            {
                this.ViewState["ShowPageSize"] = value;
            }
        }

        [Description("是否在页导航元素中显示上一页和下一页按钮"), Category("导航按钮"), DefaultValue(true), Browsable(true)]
        public bool ShowPrevNext
        {
            get
            {
                object obj2 = this.ViewState["ShowPrevNext"];
                if (obj2 != null)
                {
                    return (bool) obj2;
                }
                return true;
            }
            set
            {
                this.ViewState["ShowPrevNext"] = value;
            }
        }

        [Description("应用于提交按钮的CSS类名"), Browsable(true), Category("文本框及提交按钮"), DefaultValue((string) null)]
        public string SubmitButtonClass
        {
            get
            {
                return (string) this.ViewState["SubmitButtonClass"];
            }
            set
            {
                this.ViewState["SubmitButtonClass"] = value;
            }
        }

        [Category("文本框及提交按钮"), Description("应用于提交按钮的CSS样式"), DefaultValue((string) null), Browsable(true)]
        public string SubmitButtonStyle
        {
            get
            {
                return (string) this.ViewState["SubmitButtonStyle"];
            }
            set
            {
                this.ViewState["SubmitButtonStyle"] = value;
            }
        }

        [Browsable(true), DefaultValue("go"), Description("提交按钮上的文本"), Category("文本框及提交按钮")]
        public string SubmitButtonText
        {
            get
            {
                object obj2 = this.ViewState["SubmitButtonText"];
                if (obj2 != null)
                {
                    return (string) obj2;
                }
                return "转到";
            }
            set
            {
                if (value.Trim().Length > 0)
                {
                    this.ViewState["SubmitButtonText"] = value;
                }
            }
        }

        [Category("文本框及提交按钮"), Browsable(true), DefaultValue((string) null), Description("页索引输入文本框后的文本内容字符串")]
        public string TextAfterInputBox
        {
            get
            {
                return (string) this.ViewState["TextAfterInputBox"];
            }
            set
            {
                this.ViewState["TextAfterInputBox"] = value;
            }
        }

        [Category("文本框及提交按钮"), DefaultValue((string) null), Description("页索引输入文本框前的文本内容字符串"), Browsable(true)]
        public string TextBeforeInputBox
        {
            get
            {
                return (string) this.ViewState["TextBeforeInputBox"];
            }
            set
            {
                this.ViewState["TextBeforeInputBox"] = value;
            }
        }

        [DefaultValue("page"), Browsable(true), Description("当启用Url分页方式时，显示在url中表示要传递的页索引的参数的名称"), Category("分页")]
        public string UrlPageIndexName
        {
            get
            {
                return this.urlPageIndexName;
            }
            set
            {
                this.urlPageIndexName = value;
            }
        }

        [Description("是否使用url传递分页信息的方式来分页"), Browsable(true), DefaultValue(false), Category("分页")]
        public bool UrlPaging
        {
            get
            {
                return this.urlPaging;
            }
            set
            {
                this.urlPaging = value;
            }
        }
    }
}

