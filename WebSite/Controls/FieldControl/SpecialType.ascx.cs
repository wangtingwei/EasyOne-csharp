namespace EasyOne.WebSite.Controls.FieldControl
{
    using EasyOne.Collection;
    using EasyOne.Common;
    using EasyOne.Components;
    using EasyOne.Contents;
    using EasyOne.Model.Contents;
    using System;
    using System.Text;
    using System.Web;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public partial class SpecialType : BaseFieldControl
    {
        private int m_SpecialInfoType = 1;
        protected string path = "";

        private void InitSpecial()
        {
            StringBuilder builder = new StringBuilder();
            string specialInfoIds = "";
            switch (this.m_SpecialInfoType)
            {
                case 1:
                {
                    int generalId = DataConverter.CLng(HttpContext.Current.Request["GeneralId"]);
                    if (generalId > 0)
                    {
                        specialInfoIds = Special.GetSpecialInfoIds(generalId);
                    }
                    break;
                }
                case 2:
                {
                    int id = DataConverter.CLng(HttpContext.Current.Request["ItemId"]);
                    if (id > 0)
                    {
                        specialInfoIds = CollectionItem.GetInfoById(id).SpecialId;
                    }
                    break;
                }
            }
            if (!string.IsNullOrEmpty(specialInfoIds))
            {
                this.HdnSpecial.Value = specialInfoIds;
                string[] strArray = specialInfoIds.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < strArray.Length; i++)
                {
                    SpecialInfo specialInfoById = Special.GetSpecialInfoById(DataConverter.CLng(strArray[i]));
                    SpecialCategoryInfo specialCategoryInfoById = Special.GetSpecialCategoryInfoById(specialInfoById.SpecialCategoryId);
                    builder.Append("\n<span id='SpecialSpanId" + specialInfoById.SpecialId + "'>");
                    builder.Append(specialCategoryInfoById.SpecialCategoryName + ">>" + specialInfoById.SpecialName);
                    builder.Append(" <input type='button' class='button' onclick=\"javascript:DelSpecial('" + specialInfoById.SpecialId + "')\" value='删除此专题' /><br /></span>");
                }
            }
            if (builder.Length <= 0)
            {
                builder.Append("<span id='SpecialSpanId0'>无专题<br /></span>");
            }
            this.UlSpecial.InnerHtml = builder.ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.path = base.BasePath;
            this.path = this.Page.Request.Url.Scheme + "://" + this.Page.Request.Url.Authority + this.path;
            if (base.IsAdminManage)
            {
                this.path = this.path + SiteConfig.SiteOption.ManageDir;
            }
            else
            {
                this.path = this.path + "User";
            }
            if (!base.IsPostBack)
            {
                this.InitSpecial();
            }
        }

        public override string FieldValue
        {
            get
            {
                return this.HdnSpecial.Value;
            }
            set
            {
                base.FieldValue = value;
            }
        }

        public int SpecialInfoType
        {
            get
            {
                return this.m_SpecialInfoType;
            }
            set
            {
                this.m_SpecialInfoType = value;
            }
        }
    }
}

