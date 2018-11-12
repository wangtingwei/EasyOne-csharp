namespace EasyOne.WebSite.Controls.Shop
{
    using EasyOne.ExtendedControls;
    using EasyOne.Shop;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class ProductNumBuilder : BaseUserControl
    {

        protected void BtnCheckProductNum_Click(object sender, EventArgs e)
        {
            if (Product.IsExistSameProductNum(this.TxtProductNum.Text))
            {
                this.LblNotes.Text = "<span style='color:Red'>此商品编号已存在，请更换！</span>";
            }
            else
            {
                this.LblNotes.Text = "<span style='color:Green'>此商品编号有效！</span>";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack && string.IsNullOrEmpty(this.TxtProductNum.Text))
            {
                Random random = new Random();
                this.TxtProductNum.Text = DateTime.Now.ToString("yyyyMMddHHmmss") + random.Next().ToString();
            }
        }

        public string ProductNum
        {
            get
            {
                return this.TxtProductNum.Text;
            }
            set
            {
                this.TxtProductNum.Text = value;
            }
        }
    }
}

