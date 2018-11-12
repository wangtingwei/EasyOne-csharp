namespace EasyOne.WebSite
{
    using EasyOne.Contents;
    using EasyOne.Model.Contents;
    using EasyOne.Web.UI;
    using System;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;

    public partial class GetHits : BasePage
    {
        private int dayHits;
        private int hits;
        private CommonModelInfo modelInfo;
        private int monthHits;
        private int weekHits;

        private int GetDays(DayOfWeek dy)
        {
            switch (dy)
            {
                case DayOfWeek.Sunday:
                    return 0;

                case DayOfWeek.Monday:
                    return 1;

                case DayOfWeek.Tuesday:
                    return 2;

                case DayOfWeek.Wednesday:
                    return 3;

                case DayOfWeek.Thursday:
                    return 4;

                case DayOfWeek.Friday:
                    return 5;

                case DayOfWeek.Saturday:
                    return 6;
            }
            return 0;
        }

        protected override void Render(HtmlTextWriter writer)
        {
            if (!this.Page.IsPostBack)
            {
                int generalId = BasePage.RequestInt32("id");
                string str = BasePage.RequestStringToLower("HitsType");
                this.modelInfo = ContentManage.GetCommonModelInfoById(generalId);
                if (string.IsNullOrEmpty(str))
                {
                    this.UpdateHits(generalId);
                }
                this.ShowHits(generalId, str);
            }
        }

        private void ShowHits(int id, string hitsType)
        {
            int hits = 0;
            string str = hitsType;
            if (str != null)
            {
                if (!(str == "dayhits"))
                {
                    if (str == "weekhits")
                    {
                        hits = this.modelInfo.WeekHits;
                        goto Label_0066;
                    }
                    if (str == "monthhits")
                    {
                        hits = this.modelInfo.MonthHits;
                        goto Label_0066;
                    }
                }
                else
                {
                    hits = this.modelInfo.DayHits;
                    goto Label_0066;
                }
            }
            hits = this.modelInfo.Hits;
        Label_0066:
            base.Response.Write("document.write('" + hits + "');");
        }

        private void UpdateHits(int id)
        {
            CommonModelInfo commonModelInfoById = ContentManage.GetCommonModelInfoById(id);
            if (!commonModelInfoById.IsNull)
            {
                DateTime time;
                if (!commonModelInfoById.LastHitTime.HasValue)
                {
                    time = DateTime.Now;
                }
                else
                {
                    time = commonModelInfoById.LastHitTime.Value;
                }
                this.hits = commonModelInfoById.Hits + 1;
                this.dayHits = commonModelInfoById.DayHits;
                this.weekHits = commonModelInfoById.WeekHits;
                this.monthHits = commonModelInfoById.MonthHits;
                DateTime now = DateTime.Now;
                if (string.Compare(time.ToShortDateString(), DateTime.Now.ToShortDateString(), StringComparison.OrdinalIgnoreCase) == 0)
                {
                    this.dayHits++;
                }
                else
                {
                    this.dayHits = 1;
                }
                DateTime time3 = now.AddDays((double) -this.GetDays(now.DayOfWeek));
                DateTime time4 = time3.AddDays(7.0);
                if ((DateTime.Compare(time, time3) >= 0) && (DateTime.Compare(time, time4) <= 0))
                {
                    this.weekHits++;
                }
                else
                {
                    this.weekHits = 1;
                }
                if ((string.Compare(time.Year.ToString(), now.Year.ToString(), StringComparison.Ordinal) == 0) && (string.Compare(time.Month.ToString(), now.Month.ToString(), StringComparison.Ordinal) == 0))
                {
                    this.monthHits++;
                }
                else
                {
                    this.monthHits = 1;
                }
                time = DateTime.Now;
                ContentManage.UpdateHits(id, this.hits, this.dayHits, this.weekHits, this.monthHits, time);
            }
        }
    }
}

