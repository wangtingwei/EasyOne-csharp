namespace EasyOne.WebSite.Admin.Contents
{
    using EasyOne.AccessManage;
    using EasyOne.Common;
    using EasyOne.Contents;
    using EasyOne.Controls;
    using EasyOne.Enumerations;
    using EasyOne.Model.Contents;
    using EasyOne.Templates;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.Web.UI.WebControls;

    public partial class SpecialOrder : AdminPage
    {

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("SpecialOrder.aspx?SpecialCategoryId=" + BasePage.RequestString("SpecialCategoryId"));
        }

        protected void EBtnSetOrderId_Click(object sender, EventArgs e)
        {
            string str = this.HdnList.Value;
            if (!string.IsNullOrEmpty(str))
            {
                string[] strArray = str.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                List<SpecialInfo> list = new List<SpecialInfo>();
                for (int i = 0; i < strArray.Length; i++)
                {
                    SpecialInfo specialInfoById = Special.GetSpecialInfoById(DataConverter.CLng(strArray[i]));
                    if (!specialInfoById.IsNull)
                    {
                        specialInfoById.OrderId = i + 1;
                        list.Add(specialInfoById);
                    }
                }
                Special.OrderSpecial(list);
                this.Repeater1.DataBind();
                IncludeFile.CreateIncludeFileByAssociateType(AssociateType.Special);
                base.Response.Write("<script type='text/javascript'>parent.frames[\"left\"].location.reload();</script>");
                AdminPage.WriteSuccessMsg("专题排序成功！", "SpecialOrder.aspx?SpecialCategoryId=" + BasePage.RequestString("SpecialCategoryId"));
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RolePermissions.BusinessAccessCheck(OperateCode.SpecialManage);
        }
    }
}

