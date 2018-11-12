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

    public partial class SpecialCategoryOrder : AdminPage
    {

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            BasePage.ResponseRedirect("SpecialCategoryOrder.aspx");
        }

        protected void EBtnSetOrderId_Click(object sender, EventArgs e)
        {
            string str = this.HdnList.Value;
            if (!string.IsNullOrEmpty(str))
            {
                List<SpecialCategoryInfo> list = new List<SpecialCategoryInfo>();
                string[] strArray = str.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < strArray.Length; i++)
                {
                    SpecialCategoryInfo specialCategoryInfoById = Special.GetSpecialCategoryInfoById(DataConverter.CLng(strArray[i]));
                    if (!specialCategoryInfoById.IsNull)
                    {
                        specialCategoryInfoById.OrderId = i + 1;
                        list.Add(specialCategoryInfoById);
                    }
                }
                Special.OrderSpecialCategory(list);
                this.Repeater1.DataBind();
                IncludeFile.CreateIncludeFileByAssociateType(AssociateType.Special);
                base.Response.Write("<script type='text/javascript'>parent.frames[\"left\"].location.reload();</script>");
                AdminPage.WriteSuccessMsg("专题类别排序成功！", "SpecialCategoryOrder.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RolePermissions.AccessCheck(OperateCode.SpecialManage);
        }
    }
}

