namespace EasyOne.WebSite.Admin.Analytics
{
    using EasyOne.Analytics;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.WebControls;

    public partial class StatUserDataReport : AdminPage
    {
        protected const int BarWidth = 90;
        protected Label LblCount;
        protected float m_ItemSum;
        private StatName m_StatName;

        protected void ExtendedGridView1_DataBinding(object sender, EventArgs e)
        {
            int num = UserDataReport.Sum(this.m_StatName);
            this.m_ItemSum = Convert.ToSingle(num);
            this.LblCount.Text = num.ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string str = BasePage.RequestString("Action");
            if (Enum.IsDefined(typeof(StatName), str))
            {
                this.m_StatName = (StatName) Enum.Parse(typeof(StatName), BasePage.RequestString("Action"), true);
            }
            else
            {
                this.m_StatName = StatName.None;
            }
            string[] description = UserDataReport.GetDescription(this.m_StatName);
            this.SmpNavigator.AdditionalNode = description[1];
            this.ExtendedGridView1.Columns[0].HeaderText = description[0];
            this.OdsStat.SelectParameters["sn"].DefaultValue = this.m_StatName.ToString("D");
        }
    }
}

