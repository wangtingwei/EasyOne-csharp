namespace EasyOne.WebSite.Admin.Collection
{
    using EasyOne.Collection;
    using EasyOne.Common;
    using EasyOne.Controls;
    using EasyOne.Model.Collection;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class Exclosion : AdminPage
    {

        protected void EBtnSubmit_Click(object sender, EventArgs e)
        {
            bool flag2;
            bool flag = false;
            CollectionExclosionInfo collectionExclosionInfo = new CollectionExclosionInfo();
            collectionExclosionInfo.ExclosionName = this.TxtExclosionName.Text;
            collectionExclosionInfo.ExclosionType = DataConverter.CLng(this.DropExclosionType.SelectedValue);
            collectionExclosionInfo.IsExclosionDesignatedNumber = this.ChkExclosionNumber1.Checked;
            collectionExclosionInfo.IsExclosionMaxNumber = this.ChkExclosionNumber2.Checked;
            collectionExclosionInfo.IsExclosionMinNumber = this.ChkExclosionNumber3.Checked;
            collectionExclosionInfo.IsExclosionDesignatedDateTime = this.ChkExclosionDateTime1.Checked;
            collectionExclosionInfo.IsExclosionMaxDateTime = this.ChkExclosionDateTime2.Checked;
            collectionExclosionInfo.IsExclosionMinDateTime = this.ChkExclosionDateTime3.Checked;
            if (this.HdnAction.Value == "Modify")
            {
                collectionExclosionInfo.ExclosionId = BasePage.RequestInt32("ExclosionID");
                if (collectionExclosionInfo.ExclosionName == this.HdnExclosionName.Value)
                {
                    flag = false;
                }
                else
                {
                    flag = CollectionExclosion.Exists(collectionExclosionInfo.ExclosionName);
                }
            }
            else
            {
                flag = CollectionExclosion.Exists(collectionExclosionInfo.ExclosionName);
            }
            if (flag)
            {
                AdminPage.WriteErrMsg("<li>数据库中已经存在此采集排除！</li>");
            }
            switch (collectionExclosionInfo.ExclosionType)
            {
                case 1:
                    if (!this.RadExclosionString1.Checked)
                    {
                        collectionExclosionInfo.ExclosionStringType = 2;
                        break;
                    }
                    collectionExclosionInfo.ExclosionStringType = 1;
                    break;

                case 2:
                    collectionExclosionInfo.ExclosionDesignatedDateTime = new DateTime?(this.ExclosionDateTime1.Date);
                    collectionExclosionInfo.ExclosionMaxDateTime = new DateTime?(this.ExclosionDateTime2.Date);
                    collectionExclosionInfo.ExclosionMinDateTime = new DateTime?(this.ExclosionDateTime3.Date);
                    if (collectionExclosionInfo.IsExclosionMaxDateTime && collectionExclosionInfo.IsExclosionMinDateTime)
                    {
                        DateTime? exclosionMinDateTime = collectionExclosionInfo.ExclosionMinDateTime;
                        DateTime? exclosionMaxDateTime = collectionExclosionInfo.ExclosionMaxDateTime;
                        if ((exclosionMinDateTime.HasValue & exclosionMaxDateTime.HasValue) ? (exclosionMinDateTime.GetValueOrDefault() > exclosionMaxDateTime.GetValueOrDefault()) : false)
                        {
                            AdminPage.WriteErrMsg("<li>排除最小日期不能大于排除最大日期！</li>");
                        }
                    }
                    goto Label_0268;

                case 3:
                    collectionExclosionInfo.ExclosionDesignatedNumber = DataConverter.CLng(this.TxtExclosionNumber1.Text);
                    collectionExclosionInfo.ExclosionMaxNumber = DataConverter.CLng(this.TxtExclosionNumber2.Text);
                    collectionExclosionInfo.ExclosionMinNumber = DataConverter.CLng(this.TxtExclosionNumber3.Text);
                    if ((collectionExclosionInfo.IsExclosionMaxNumber && collectionExclosionInfo.IsExclosionMinNumber) && (collectionExclosionInfo.ExclosionMinNumber > collectionExclosionInfo.ExclosionMaxNumber))
                    {
                        AdminPage.WriteErrMsg("<li>排除最小数字不能大于排除最大数字！</li>");
                    }
                    goto Label_0268;

                default:
                    AdminPage.WriteErrMsg("<li>没有选择排除类型！</li>");
                    goto Label_0268;
            }
            collectionExclosionInfo.ExclosionString = this.TxtExclosionString.Text;
        Label_0268:
            flag2 = false;
            if (this.HdnAction.Value == "Modify")
            {
                flag2 = CollectionExclosion.Update(collectionExclosionInfo);
            }
            else
            {
                flag2 = CollectionExclosion.Add(collectionExclosionInfo);
            }
            if (flag2)
            {
                AdminPage.WriteSuccessMsg("保存采集排除成功！", "ExclosionManage.aspx");
            }
            else
            {
                AdminPage.WriteErrMsg("保存采集排除失败！");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (base.IsPostBack)
            {
                return;
            }
            this.DropExclosionType.Attributes.Add("onchange", "javascript:exclosionTpye(this.options[this.selectedIndex].value);");
            string str = BasePage.RequestString("Action", "Add");
            this.HdnAction.Value = str;
            if (!(str == "Modify"))
            {
                return;
            }
            CollectionExclosionInfo infoById = new CollectionExclosionInfo();
            int id = BasePage.RequestInt32("ExclosionID");
            if (id == 0)
            {
                AdminPage.WriteErrMsg("<li>ExclosionID 为零 或没有参数！</li>");
            }
            infoById = CollectionExclosion.GetInfoById(id);
            if (infoById.IsNull)
            {
                AdminPage.WriteErrMsg("<li>所属采集排除项目不存在！</li>");
            }
            this.TxtExclosionName.Text = infoById.ExclosionName;
            this.DropExclosionType.SelectedValue = infoById.ExclosionType.ToString();
            this.HdnExclosionName.Value = infoById.ExclosionName;
            switch (infoById.ExclosionType)
            {
                case 1:
                    if (infoById.ExclosionStringType != 1)
                    {
                        this.RadExclosionString2.Checked = true;
                        break;
                    }
                    this.RadExclosionString1.Checked = true;
                    break;

                case 2:
                    if (infoById.IsExclosionDesignatedDateTime)
                    {
                        this.ChkExclosionDateTime1.Checked = true;
                        this.ExclosionDateTime1.Text = infoById.ExclosionDesignatedDateTime.ToString();
                    }
                    if (infoById.IsExclosionMaxDateTime)
                    {
                        this.ChkExclosionDateTime2.Checked = true;
                        this.ExclosionDateTime2.Text = infoById.ExclosionMaxDateTime.ToString();
                    }
                    if (infoById.IsExclosionMinDateTime)
                    {
                        this.ChkExclosionDateTime3.Checked = true;
                        this.ExclosionDateTime3.Text = infoById.ExclosionMinDateTime.ToString();
                    }
                    goto Label_024C;

                case 3:
                    if (infoById.IsExclosionDesignatedNumber)
                    {
                        this.ChkExclosionNumber1.Checked = true;
                        this.TxtExclosionNumber1.Text = infoById.ExclosionDesignatedNumber.ToString();
                    }
                    if (infoById.IsExclosionMaxNumber)
                    {
                        this.ChkExclosionNumber2.Checked = true;
                        this.TxtExclosionNumber2.Text = infoById.ExclosionMaxNumber.ToString();
                    }
                    if (infoById.IsExclosionMinNumber)
                    {
                        this.ChkExclosionNumber3.Checked = true;
                        this.TxtExclosionNumber3.Text = infoById.ExclosionMinNumber.ToString();
                    }
                    goto Label_024C;

                default:
                    goto Label_024C;
            }
            this.TxtExclosionString.Text = infoById.ExclosionString;
        Label_024C:
            this.Page.ClientScript.RegisterStartupScript(base.GetType(), "Init", "<script type='text/javascript'>exclosionTpye(" + infoById.ExclosionType.ToString() + ");</script>");
        }
    }
}

