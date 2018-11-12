namespace EasyOne.WebSite.Admin.AD
{
    using EasyOne.AccessManage;
    using EasyOne.AD;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Model.AD;
    using EasyOne.Web.UI;
    using System;
    using System.Runtime.CompilerServices;
    using System.Web.UI.WebControls;

    public partial class ADZoneManage : AdminPage
    {

        private void DoDelegate(DeleteAndAtctiveAndPause action)
        {
            string str = this.GdvADZone.SelectList.ToString();
            if (!string.IsNullOrEmpty(str) && action(str))
            {
                BasePage.ResponseRedirect("ADZoneManage.aspx");
            }
        }

        protected void EBtnActive_Click(object sender, EventArgs e)
        {
            DeleteAndAtctiveAndPause action = new DeleteAndAtctiveAndPause(ADZone.ActiveADZone);
            this.DoDelegate(action);
        }

        protected void EBtnDelete_Click(object sender, EventArgs e)
        {
            DeleteAndAtctiveAndPause action = new DeleteAndAtctiveAndPause(ADZone.Delete);
            this.DoDelegate(action);
        }

        protected void EBtnPause_Click(object sender, EventArgs e)
        {
            DeleteAndAtctiveAndPause action = new DeleteAndAtctiveAndPause(ADZone.PauseADZone);
            this.DoDelegate(action);
        }

        protected void EBtnRefurbish_Click(object sender, EventArgs e)
        {
            ADZone.CreateJS(this.GdvADZone.SelectList.ToString());
        }

        protected void GdvADZone_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ADZoneInfo dataItem = (ADZoneInfo) e.Row.DataItem;
                Label label = (Label) e.Row.FindControl("LabZoneType");
                Label label2 = (Label) e.Row.FindControl("LabShowType");
                label.Text = BasePage.EnumToHtml<ADZoneType>(dataItem.ZoneType);
                label2.Text = GetADZoneShowType()[dataItem.ShowType];
            }
        }

        private static string[] GetADZoneShowType()
        {
            string[] strArray = new string[4];
            strArray[1] = "按权重随机显示";
            strArray[2] = "按权重优先显示";
            strArray[3] = "按顺序循环显示";
            return strArray;
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            RolePermissions.BusinessAccessCheck(OperateCode.AdManage);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string str = BasePage.RequestString("Action");
            if (!string.IsNullOrEmpty(str))
            {
                string id = BasePage.RequestString("ZoneId");
                string returnurl = "ADZoneManage.aspx";
                switch (str)
                {
                    case "Delete":
                        ADZone.Delete(id);
                        ADZone.DeleteJS(id);
                        return;

                    case "Copy":
                        if (!ADZone.CopyADZone(id))
                        {
                            AdminPage.WriteErrMsg("复制广告版位失败！", returnurl);
                            return;
                        }
                        AdminPage.WriteSuccessMsg("复制广告版位成功！", returnurl);
                        ADZone.CreateJS(id);
                        return;

                    case "Pause":
                        ADZone.PauseADZone(id);
                        ADZone.DeleteJS(id);
                        BasePage.ResponseRedirect(returnurl);
                        return;

                    case "Active":
                        ADZone.ActiveADZone(id);
                        ADZone.CreateJS(id);
                        BasePage.ResponseRedirect(returnurl);
                        return;

                    case "Clear":
                        ADZone.ClearADZone(id);
                        ADZone.DeleteJS(id);
                        BasePage.ResponseRedirect(returnurl);
                        return;

                    case "Refurbish":
                        ADZone.CreateJS(id);
                        AdminPage.WriteSuccessMsg("刷新成功！", "AdZoneManage.aspx");
                        return;

                    default:
                        return;
                }
            }
        }

        private delegate bool DeleteAndAtctiveAndPause(string id);
    }
}

