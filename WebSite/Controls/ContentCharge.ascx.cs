namespace EasyOne.WebSite.Controls
{
    using EasyOne.Common;
    using EasyOne.CommonModel;
    using EasyOne.Contents;
    using EasyOne.Model.Contents;
    using EasyOne.Model.UserManage;
    using EasyOne.UserManage;
    using EasyOne.Web.UI;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Web.UI.WebControls;

    public partial class ContentCharge : BaseUserControl
    {
        protected DataTable contentDataTable;

        protected void Page_Load(object sender, EventArgs e)
        {
            ContentChargeInfo info4;
            int generalId = BaseUserControl.RequestInt32("GeneralID");
            this.contentDataTable = ContentManage.GetContentDataById(generalId);
            if ((this.contentDataTable == null) || (this.contentDataTable.Rows.Count == 0))
            {
                BaseUserControl.WriteErrMsg("指定的信息不存在！");
            }
            if (ModelManager.GetModelInfoById(DataConverter.CLng(this.contentDataTable.Rows[0]["ModelID"].ToString())).IsNull)
            {
                BaseUserControl.WriteErrMsg("信息隶属模型不存在！");
            }
            IList<UserGroupsInfo> userGroupList = UserGroups.GetUserGroupList(0, 0);
            ContentPermissionInfo contentPermissionInfoById = PermissionContent.GetContentPermissionInfoById(generalId);
            if (!contentPermissionInfoById.IsNull)
            {
                switch (contentPermissionInfoById.PermissionType)
                {
                    case 0:
                        this.LitInfoPurview.Text = "继承栏目权限";
                        goto Label_0186;

                    case 1:
                        this.LitInfoPurview.Text = "所有会员";
                        goto Label_0186;
                }
                this.LitInfoPurview.Text = "指定会员组:";
                if (!string.IsNullOrEmpty(contentPermissionInfoById.ArrGroupId))
                {
                    string[] strArray = contentPermissionInfoById.ArrGroupId.Split(new char[] { ',' });
                    for (int i = 0; i < strArray.Length; i++)
                    {
                        foreach (UserGroupsInfo info3 in userGroupList)
                        {
                            if (info3.GroupId == DataConverter.CLng(strArray[i]))
                            {
                                this.LitInfoPurview.Text = this.LitInfoPurview.Text + " " + info3.GroupName;
                                break;
                            }
                        }
                    }
                }
            }
        Label_0186:
            info4 = EasyOne.Contents.ContentCharge.GetContentChargeInfoById(generalId);
            if (!info4.IsNull)
            {
                this.LblInfoPoint.Text = DataConverter.CLng(info4.InfoPoint).ToString();
                switch (info4.ChargeType)
                {
                    case 1:
                        this.LblChargeType.Text = "距离上次收费时间" + DataConverter.CLng(info4.PitchTime).ToString() + "小时后重新收费";
                        break;

                    case 2:
                        this.LblChargeType.Text = "会员重复阅读此文章" + DataConverter.CLng(info4.ReadTimes).ToString() + "次后重新收费";
                        break;

                    case 3:
                        this.LblChargeType.Text = "上述两者都满足时重新收费";
                        break;

                    case 4:
                        this.LblChargeType.Text = "上述两者任一个满足时就重新收费";
                        break;

                    case 5:
                        this.LblChargeType.Text = "每阅读一次就重复收费一次（建议不要使用）";
                        break;

                    default:
                        this.LblChargeType.Text = "不重复收费";
                        break;
                }
                this.LblDividePercent.Text = DataConverter.CLng(info4.DividePercent).ToString() + "%";
            }
        }
    }
}

