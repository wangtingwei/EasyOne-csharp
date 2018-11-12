namespace EasyOne.WebSite.Controls
{
    using EasyOne.Common;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI.WebControls;

    public partial class ChargeTypeSetting : BaseUserControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            bool isPostBack = this.Page.IsPostBack;
        }

        public int ChargeType
        {
            get
            {
                for (int i = 0; i < 6; i++)
                {
                    RadioButton button = (RadioButton) this.FindControl("RadChargeType" + i.ToString());
                    if (button.Checked)
                    {
                        return i;
                    }
                }
                return 0;
            }
            set
            {
                switch (value)
                {
                    case 0:
                        this.RadChargeType0.Checked = true;
                        return;

                    case 1:
                        this.RadChargeType1.Checked = true;
                        return;

                    case 2:
                        this.RadChargeType2.Checked = true;
                        return;

                    case 3:
                        this.RadChargeType3.Checked = true;
                        return;

                    case 4:
                        this.RadChargeType4.Checked = true;
                        return;

                    case 5:
                        this.RadChargeType5.Checked = true;
                        return;
                }
            }
        }

        public int PitchTime
        {
            get
            {
                return DataConverter.CLng(this.TxtPitchTime.Text);
            }
            set
            {
                this.TxtPitchTime.Text = value.ToString();
            }
        }

        public int ReadTimes
        {
            get
            {
                return DataConverter.CLng(this.TxtReadTimes.Text);
            }
            set
            {
                this.TxtReadTimes.Text = value.ToString();
            }
        }
    }
}

