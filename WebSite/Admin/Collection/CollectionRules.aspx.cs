namespace EasyOne.WebSite.Admin.Collection
{
    using EasyOne.Collection;
    using EasyOne.Common;
    using EasyOne.Controls;
    using EasyOne.Web.UI;
    using System;
    using System.Collections;
    using System.Text;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class CollectionRules : AdminPage
    {

        protected void BtnCancle_Click(object sender, EventArgs e)
        {
        }

        protected void BtnField_Click(object sender, EventArgs e)
        {
            CollectionCommon common = new CollectionCommon();
            Uri url = new Uri(this.TxtListAddress.Text);
            string coding = this.CodingType(this.RadlCodeType.SelectedValue);
            string httpPage = common.GetHttpPage(url, coding);
            string str3 = common.GetInterceptionString(httpPage, this.TxtFieldBegin.Text, this.TxtFieldEnd.Text);
            if (!string.IsNullOrEmpty(str3))
            {
                if (this.ChkDesignated.Checked)
                {
                    this.TxtShowCode.Text = this.TxtDesignated.Text;
                }
                else
                {
                    if (this.RadContent.Checked)
                    {
                        this.TxtShowCode.Text = str3;
                    }
                    if (this.RadKeyWord.Checked)
                    {
                        this.TxtShowCode.Text = CollectionCommon.CreateKeyWord(str3, DataConverter.CLng(this.TxtKeyWord.Text));
                    }
                    if (this.RadDate.Checked)
                    {
                        this.TxtShowCode.Text = DataConverter.CDate(str3).ToString();
                    }
                }
            }
        }

        protected void BtnPaingType1_Click(object sender, EventArgs e)
        {
            CollectionCommon common = new CollectionCommon();
            Uri url = new Uri(this.TxtListAddress.Text);
            string coding = this.CodingType(this.RadlCodeType.SelectedValue);
            string httpPage = common.GetHttpPage(url, coding);
            this.TxtShowCode.Text = common.ConvertToAbsluteUrl(common.GetPaing(httpPage, this.TxtPaingBegin.Text, this.TxtPaingEnd.Text), url.ToString());
        }

        protected void BtnPaingType2_Click(object sender, EventArgs e)
        {
            string str = this.TxtPaingAddress.Text.ToLower();
            if (string.IsNullOrEmpty(str))
            {
                AdminPage.WriteErrMsg("请填写要采集的分页地址！");
            }
            if (str.IndexOf("{$id}", StringComparison.OrdinalIgnoreCase) <= 0)
            {
                AdminPage.WriteErrMsg("URL字符串没有标签{$ID}");
            }
            int num = DataConverter.CLng(this.TxtScopeBegin.Text);
            int num2 = DataConverter.CLng(this.TxtScopeEnd.Text);
            StringBuilder builder = new StringBuilder();
            if (num2 < num)
            {
                for (int i = num; i >= num2; i--)
                {
                    builder.Append(str.Replace("{$id}", i.ToString()) + "\r\n");
                }
            }
            else
            {
                for (int j = num; j <= num2; j++)
                {
                    builder.Append(str.Replace("{$id}", j.ToString()) + "\r\n");
                }
            }
            this.TxtShowCode.Text = builder.ToString();
        }

        protected void BtnPaingType3_Click(object sender, EventArgs e)
        {
            this.TxtShowCode.Text = this.TxtListPaing.Text;
        }

        protected void BtnReplace_Click(object sender, EventArgs e)
        {
            string contentCode = this.GetContentCode();
            contentCode = this.FilterItem(contentCode);
            this.TxtShowCode.Text = contentCode;
        }

        protected void BtnReplace1_Click(object sender, EventArgs e)
        {
            string contentCode = this.GetContentCode();
            contentCode = this.FilterItem(contentCode);
            string text = this.TxtReplace.Text;
            if (string.IsNullOrEmpty(this.TxtReplace.Text))
            {
                text = "";
            }
            this.TxtShowCode.Text = contentCode.Replace(this.TxtFilter.Text, text);
        }

        protected void BtnReplace2_Click(object sender, EventArgs e)
        {
            string contentCode = this.GetContentCode();
            contentCode = this.FilterItem(contentCode);
            string text = this.TxtReplace.Text;
            if (string.IsNullOrEmpty(this.TxtReplace.Text))
            {
                text = "";
            }
            string oldValue = new CollectionCommon().GetInterceptionString(contentCode, this.TxtFilterBegin.Text, this.TxtFilterEnd.Text, true, true);
            this.TxtShowCode.Text = contentCode.Replace(oldValue, text);
        }

        protected void BtnShowCode_Click(object sender, EventArgs e)
        {
            this.TxtShowCode.Text = this.GetContentCode();
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
        }

        protected void BtnTestLink_Click(object sender, EventArgs e)
        {
            CollectionCommon common = new CollectionCommon();
            Uri url = new Uri(this.TxtListAddress.Text);
            string coding = this.CodingType(this.RadlCodeType.SelectedValue);
            string httpPage = common.GetHttpPage(url, coding);
            string code = common.GetInterceptionString(httpPage, this.TxtListBegin.Text, this.TxtListEnd.Text);
            ArrayList list = common.GetArray(code, this.TxtLinkBegin.Text, this.TxtLinkEnd.Text);
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < list.Count; i++)
            {
                builder.Append(common.ConvertToAbsluteUrl(list[i].ToString(), url.ToString()) + "\r\n");
            }
            this.TxtShowCode.Text = builder.ToString();
        }

        protected void BtnTestList_Click(object sender, EventArgs e)
        {
            CollectionCommon common = new CollectionCommon();
            string contentCode = this.GetContentCode();
            this.TxtShowCode.Text = common.GetInterceptionString(contentCode, this.TxtListBegin.Text, this.TxtListEnd.Text);
        }

        private string CodingType(string code)
        {
            string str = "gb2312";
            string str2 = code;
            if (str2 == null)
            {
                return str;
            }
            if (!(str2 == "0"))
            {
                if (str2 != "1")
                {
                    if (str2 != "2")
                    {
                        return str;
                    }
                    return "Big5";
                }
            }
            else
            {
                return "gb2312";
            }
            return "UTF-8";
        }

        private string FilterItem(string constr)
        {
            StringBuilder builder = new StringBuilder();
            foreach (ListItem item in this.ChkFilterSelect.Items)
            {
                if (item.Selected)
                {
                    if (builder.Length > 0)
                    {
                        builder.Append("," + item.Value);
                    }
                    else
                    {
                        builder.Append(item.Value);
                    }
                }
            }
            if (builder.Length > 0)
            {
                constr = StringHelper.FilterScript(constr, builder.ToString());
            }
            return constr;
        }

        private string GetContentCode()
        {
            CollectionCommon common = new CollectionCommon();
            Uri url = new Uri(this.TxtListAddress.Text);
            string coding = this.CodingType(this.RadlCodeType.SelectedValue);
            return common.GetHttpPage(url, coding);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            bool isPostBack = base.IsPostBack;
        }

        protected void RadList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string iD = ((RadioButton) sender).ID;
            this.PnlList.Visible = false;
            this.PnlField.Visible = false;
            this.PnlPaing.Visible = false;
            this.PnlFilter.Visible = false;
            string str2 = iD;
            if (str2 != null)
            {
                if (!(str2 == "RadList"))
                {
                    if (!(str2 == "RadField"))
                    {
                        if (!(str2 == "RadPaing"))
                        {
                            if (str2 == "RadFilter")
                            {
                                this.PnlFilter.Visible = true;
                                this.RadlFilterType1.Attributes.Add("onclick", "javascript:filterType1.style.display='none';filterType2.style.display='none';filterType3.style.display='';");
                                this.RadlFilterType2.Attributes.Add("onclick", "javascript:filterType1.style.display='';filterType2.style.display='none';filterType3.style.display='none';");
                                this.RadlFilterType3.Attributes.Add("onclick", "javascript:filterType1.style.display='none';filterType2.style.display='';filterType3.style.display='none';");
                            }
                            return;
                        }
                        this.PnlPaing.Visible = true;
                        this.RadlPaingType1.Attributes.Add("onclick", "javascript:ListPaing1.style.display='';ListPaing2.style.display='none';ListPaing3.style.display='none';");
                        this.RadlPaingType2.Attributes.Add("onclick", "javascript:ListPaing1.style.display='none';ListPaing2.style.display='';ListPaing3.style.display='none';");
                        this.RadlPaingType3.Attributes.Add("onclick", "javascript:ListPaing1.style.display='none';ListPaing2.style.display='none';ListPaing3.style.display='';");
                        return;
                    }
                }
                else
                {
                    this.PnlList.Visible = true;
                    return;
                }
                this.PnlField.Visible = true;
            }
        }
    }
}

