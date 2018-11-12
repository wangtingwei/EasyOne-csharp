namespace EasyOne.WebSite.Admin.Accessories
{
    using EasyOne.Accessories;
    using EasyOne.Common;
    using EasyOne.Controls;
    using EasyOne.Model.Accessories;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.WebControls;

    public partial class KeyWord : AdminPage
    {


        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            string text = this.TxtKeywordText.Text;
            if (string.IsNullOrEmpty(text))
            {
                AdminPage.WriteErrMsg("<li>关键字不能为空！</li>");
            }
            if (string.IsNullOrEmpty(this.TxtPriority.Text))
            {
                AdminPage.WriteErrMsg("<li>关键字权重不能为空！</li>");
            }
            KeywordInfo keywordInfo = new KeywordInfo();
            if (this.ViewState["action"].ToString() == "Add")
            {
                if (!Keywords.Exists(text))
                {
                    keywordInfo.KeywordText = text;
                    keywordInfo.KeywordType = DataConverter.CLng(this.RadlKeywordType.SelectedValue);
                    keywordInfo.Priority = DataConverter.CLng(this.TxtPriority.Text);
                    if (Keywords.Add(keywordInfo))
                    {
                        AdminPage.WriteSuccessMsg("<li>添加关键字成功！</li>", "KeyWordManage.aspx");
                    }
                    else
                    {
                        AdminPage.WriteErrMsg("<li>添加关键字操作失败！</li>");
                    }
                }
                else
                {
                    AdminPage.WriteErrMsg("<li>数据库中已经存在此关键字！</li>");
                }
            }
            else
            {
                bool flag;
                if (text == this.HdnKeywordText.Value)
                {
                    flag = false;
                }
                else
                {
                    flag = Keywords.Exists(text);
                }
                if (!flag)
                {
                    keywordInfo.KeywordId = BasePage.RequestInt32("KeywordID");
                    keywordInfo.KeywordText = text;
                    keywordInfo.KeywordType = DataConverter.CLng(this.RadlKeywordType.SelectedValue);
                    keywordInfo.Priority = DataConverter.CLng(this.TxtPriority.Text);
                    if (Keywords.Update(keywordInfo))
                    {
                        AdminPage.WriteSuccessMsg("<li>修改关键字成功！</li>", "KeyWordManage.aspx");
                    }
                    else
                    {
                        AdminPage.WriteErrMsg("<li>修改关键字操作失败！</li>");
                    }
                }
                else
                {
                    AdminPage.WriteErrMsg("<li>数据库中已经存在此关键字！</li>");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                string str = BasePage.RequestString("Action", "Add");
                this.ViewState["action"] = str;
                if (str == "Modify")
                {
                    this.EBtnSubmit.Text = "修 改";
                    KeywordInfo keywordById = Keywords.GetKeywordById(BasePage.RequestInt32("KeywordID"));
                    if (!keywordById.IsNull)
                    {
                        this.TxtKeywordText.Text = keywordById.KeywordText;
                        this.TxtPriority.Text = keywordById.Priority.ToString();
                        this.RadlKeywordType.SelectedValue = keywordById.KeywordType.ToString();
                        this.HdnKeywordText.Value = keywordById.KeywordText.ToString();
                    }
                }
            }
        }
    }
}

