namespace EasyOne.WebSite.Controls.Shop
{
    using EasyOne.Shop;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.WebControls;

    public partial class DependentProduct : BaseUserControl
    {
        protected int m_ModelId;
        protected int m_ProductID;

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public void SetDependentProduct(string idList)
        {
            this.LstDependentProduct.DataSource = ProductCommon.GetProductList(idList);
            this.LstDependentProduct.DataBind();
            this.HdnDependent.Value = idList;
        }

        public void SetModelId(int modelId)
        {
            this.m_ModelId = modelId;
        }

        public void SetProductId(int productId)
        {
            this.m_ProductID = productId;
        }

        public string DependentProducts
        {
            get
            {
                return this.HdnDependent.Value;
            }
        }
    }
}

