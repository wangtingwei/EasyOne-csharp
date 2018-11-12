namespace EasyOne.WebSite.Admin.Collection
{
    using EasyOne.Collection;
    using EasyOne.Controls;
    using EasyOne.Model.Collection;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class CollectionFilter : AdminPage
    {

        protected void BtnReplace_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.TxtShowCode.Text))
            {
                AdminPage.WriteErrMsg("测试文本框不能为空！");
            }
            if (this.RadFilterType1.Checked)
            {
                this.TxtShowCode.Text = this.TxtShowCode.Text.Replace(this.TxtFilterBegin.Text, this.TxtReplace.Text);
            }
            else
            {
                string str = new CollectionCommon().GetInterceptionString(this.TxtShowCode.Text, this.TxtFilterBegin.Text, this.TxtFilterEnd.Text, true, true);
                if (!string.IsNullOrEmpty(str))
                {
                    this.TxtShowCode.Text = this.TxtShowCode.Text.Replace(str, this.TxtReplace.Text);
                }
                else
                {
                    this.TxtShowCode.Text = this.TxtReplace.Text;
                }
            }
        }

        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            bool flag;
            CollectionFilterRuleInfo collectionFilterRuleInfo = new CollectionFilterRuleInfo();
            collectionFilterRuleInfo.FilterName = this.TxtFilterName.Text;
            bool flag2 = false;
            if (this.HdnFilter.Value == "Modify")
            {
                collectionFilterRuleInfo.FilterRuleId = BasePage.RequestInt32("FilterRuleID");
                if (collectionFilterRuleInfo.FilterName == this.HdnFilterName.Value)
                {
                    flag = false;
                }
                else
                {
                    flag = CollectionFilterRules.Exists(collectionFilterRuleInfo.FilterName);
                }
            }
            else
            {
                flag = CollectionFilterRules.Exists(collectionFilterRuleInfo.FilterName);
            }
            if (flag)
            {
                AdminPage.WriteErrMsg("<li>数据库中已经存在此采集过滤！</li>");
            }
            collectionFilterRuleInfo.BeginCode = this.TxtFilterBegin.Text;
            if (this.RadFilterType1.Checked)
            {
                collectionFilterRuleInfo.FilterType = 1;
            }
            else
            {
                collectionFilterRuleInfo.FilterType = 2;
                collectionFilterRuleInfo.EndCode = this.TxtFilterEnd.Text;
            }
            collectionFilterRuleInfo.Replace = this.TxtReplace.Text;
            if (this.HdnFilter.Value == "Modify")
            {
                flag2 = CollectionFilterRules.Update(collectionFilterRuleInfo);
            }
            else
            {
                flag2 = CollectionFilterRules.Add(collectionFilterRuleInfo);
            }
            if (flag2)
            {
                AdminPage.WriteSuccessMsg("保存采集过滤成功！", "CollectionFilterManage.aspx");
            }
            else
            {
                AdminPage.WriteErrMsg("保存采集过滤失败！");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                string str = BasePage.RequestString("Action", "Add");
                if (str == "Modify")
                {
                    CollectionFilterRuleInfo infoById = new CollectionFilterRuleInfo();
                    infoById = CollectionFilterRules.GetInfoById(BasePage.RequestInt32("FilterRuleID"));
                    this.TxtFilterName.Text = infoById.FilterName;
                    switch (infoById.FilterType)
                    {
                        case 1:
                            this.PnlFilterEnd.Visible = false;
                            this.RadFilterType1.Checked = true;
                            this.TxtFilterBegin.Text = infoById.BeginCode;
                            this.TxtReplace.Text = infoById.Replace;
                            break;

                        case 2:
                            this.PnlFilterEnd.Visible = true;
                            this.RadFilterType2.Checked = true;
                            this.TxtFilterBegin.Text = infoById.BeginCode;
                            this.TxtFilterEnd.Text = infoById.EndCode;
                            this.TxtReplace.Text = infoById.Replace;
                            break;
                    }
                    this.HdnFilter.Value = str;
                    this.HdnFilterName.Value = infoById.FilterName;
                }
            }
        }

        protected void RadFilterType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.RadFilterType1.Checked)
            {
                this.PnlFilterEnd.Visible = false;
                this.LblFilterBegin.Text = "要过滤的代码：";
                this.ValeFilterBegin.ErrorMessage = "要过滤的代码不能为空！";
            }
            else
            {
                this.PnlFilterEnd.Visible = true;
                this.LblFilterBegin.Text = "要过滤的开始代码：";
                this.ValeFilterBegin.ErrorMessage = "要过滤的开始代码不能为空！";
            }
        }
    }
}

