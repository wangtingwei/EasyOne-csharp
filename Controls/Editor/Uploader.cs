namespace EasyOne.Controls.Editor
{
    using EasyOne.Common;
    using EasyOne.CommonModel;
    using EasyOne.Components;
    using EasyOne.Model.CommonModel;
    using EasyOne.Model.UserManage;
    using EasyOne.UserManage;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Web;

    public class Uploader : FileWorkerBase
    {
        private static string GetAllowSuffix(FieldInfo fieldInfo, string uploaderType)
        {
            string str2;
            string str3;
            string str4;
            string str = "";
            if (fieldInfo.Settings.Count > 9)
            {
                str2 = fieldInfo.Settings[6];
                str3 = fieldInfo.Settings[7];
                str4 = fieldInfo.Settings[8];
            }
            else
            {
                str2 = fieldInfo.Settings[2];
                str3 = fieldInfo.Settings[3];
                str4 = fieldInfo.Settings[4];
            }
            string str5 = uploaderType;
            if (str5 == null)
            {
                return str;
            }
            if (!(str5 == "Photo"))
            {
                if ((str5 == "Flash") || (str5 == "Media"))
                {
                    return str3;
                }
                if (str5 != "Link")
                {
                    return str;
                }
                return str4;
            }
            return str2;
        }

        protected override void OnLoad(EventArgs e)
        {
            HttpPostedFile file = base.Request.Files["NewFile"];
            string str = Path.GetExtension(file.FileName).ToLower();
            string uploaderType = base.Request.Form["UploaderType"];
            bool flag = DataConverter.CBoolean(base.Request.Form["IsWatermark"]);
            bool flag2 = DataConverter.CBoolean(base.Request.Form["IsThumb"]);
            int modelId = DataConverter.CLng(base.Request.Form["ModelId"]);
            string str3 = DataSecurity.FilterBadChar(base.Request.Form["FieldName"]);
            string allowSuffix = "";
            int uploadFileMaxSize = 0;
            int uploadSize = 0;
            string customMsg = "请检查网站信息配置是否设置允许的上传文件大小！";
            if (!SiteConfig.SiteOption.EnableUploadFiles)
            {
                this.SendResults(0xcc);
            }
            else
            {
                if (!PEContext.Current.Admin.Identity.IsAuthenticated)
                {
                    if (!PEContext.Current.User.Identity.IsAuthenticated)
                    {
                        UserGroupsInfo userGroupById = UserGroups.GetUserGroupById(-2);
                        if (string.IsNullOrEmpty(userGroupById.GroupSetting))
                        {
                            this.SendResults(1, "", "", "匿名会员组不存在！");
                            return;
                        }
                        UserPurviewInfo groupSetting = UserGroups.GetGroupSetting(userGroupById.GroupSetting);
                        if (groupSetting.IsNull)
                        {
                            this.SendResults(1, "", "", "匿名会员组没有进行权限设置！");
                            return;
                        }
                        if (!groupSetting.EnableUpload)
                        {
                            this.SendResults(1, "", "", "匿名会员组没有开启上传权限！");
                            return;
                        }
                        uploadSize = groupSetting.UploadSize;
                    }
                    else
                    {
                        if (!PEContext.Current.User.UserInfo.UserPurview.EnableUpload)
                        {
                            this.SendResults(1, "", "", "所属会员组没有开启上传权限！");
                            return;
                        }
                        uploadSize = PEContext.Current.User.UserInfo.UserPurview.UploadSize;
                    }
                }
                if ((file == null) || (file.ContentLength == 0))
                {
                    this.SendResults(0xca);
                }
                else
                {
                    if ((modelId == 0) || string.IsNullOrEmpty(str3))
                    {
                        if (!ConfigurationManager.AppSettings["EasyOne:DefaultUploadSuffix"].ToLower().Contains(str))
                        {
                            this.SendResults(1, "", "", "不允许上传动态页文件！");
                            return;
                        }
                        uploadFileMaxSize = SiteConfig.SiteOption.UploadFileMaxSize;
                    }
                    else
                    {
                        IList<FieldInfo> fieldListByModelId = ModelManager.GetFieldListByModelId(modelId);
                        if ((fieldListByModelId != null) && (fieldListByModelId.Count > 0))
                        {
                            foreach (FieldInfo info3 in fieldListByModelId)
                            {
                                if (string.CompareOrdinal(info3.FieldName, str3) == 0)
                                {
                                    allowSuffix = GetAllowSuffix(info3, uploaderType);
                                    if (info3.Settings.Count > 7)
                                    {
                                        uploadFileMaxSize = DataConverter.CLng(info3.Settings[7]);
                                    }
                                    break;
                                }
                            }
                        }
                        if (string.IsNullOrEmpty(allowSuffix))
                        {
                            this.SendResults(1, "", "", "字段内容控件没有填写允许上传的后缀！");
                            return;
                        }
                        if (!allowSuffix.Contains(str.Replace(".", "")))
                        {
                            this.SendResults(1, "", "", "这种文件类型不允许上传！只允许上传这几种文件类型：" + allowSuffix);
                            return;
                        }
                        customMsg = "请检查所属字段控件是否设置了允许上传文件大小！";
                    }
                    if (uploadFileMaxSize <= 0)
                    {
                        this.SendResults(1, "", "", customMsg);
                    }
                    else
                    {
                        if (!PEContext.Current.Admin.Identity.IsAuthenticated && (uploadFileMaxSize > uploadSize))
                        {
                            uploadFileMaxSize = uploadSize;
                        }
                        if (file.ContentLength > (uploadFileMaxSize * 0x400))
                        {
                            this.SendResults(1, "", "", "请上传小于" + uploadFileMaxSize.ToString() + "KB的文件！");
                        }
                        else
                        {
                            string str9;
                            int errorNumber = 0;
                            string fileUrl = "";
                            string str7 = DataSecurity.MakeFileRndName();
                            string str8 = str7 + str;
                            int num5 = 0;
                            while (true)
                            {
                                str9 = Path.Combine(base.UserFilesDirectory, str8);
                                if (!File.Exists(str9))
                                {
                                    break;
                                }
                                num5++;
                                str8 = string.Concat(new object[] { Path.GetFileNameWithoutExtension(file.FileName), "(", num5, ")", Path.GetExtension(file.FileName) });
                                errorNumber = 0xc9;
                            }
                            file.SaveAs(str9);
                            fileUrl = base.UserFilesPath + str8;
                            if (!string.IsNullOrEmpty(uploaderType) && (string.CompareOrdinal(uploaderType, "Photo") == 0))
                            {
                                string oldValue = "";
                                if (base.Request.ApplicationPath.EndsWith("/", StringComparison.Ordinal))
                                {
                                    oldValue = ("/" + SiteConfig.SiteOption.UploadDir + "/").Replace("//", "/");
                                }
                                else
                                {
                                    oldValue = base.Request.ApplicationPath + "/" + SiteConfig.SiteOption.UploadDir;
                                }
                                if (flag2)
                                {
                                    string str11 = base.UserFilesPath + str7 + "_S" + str;
                                    Thumbs.GetThumbsPath(fileUrl.Replace(oldValue, ""), str11.Replace(oldValue, ""));
                                }
                                if (flag)
                                {
                                    WaterMark.AddWaterMark(fileUrl.Replace(oldValue, ""));
                                }
                            }
                            this.SendResults(errorNumber, fileUrl, str8);
                        }
                    }
                }
            }
        }

        private void SendResults(int errorNumber)
        {
            this.SendResults(errorNumber, "", "", "");
        }

        private void SendResults(int errorNumber, string fileUrl, string fileName)
        {
            this.SendResults(errorNumber, fileUrl, fileName, "");
        }

        private void SendResults(int errorNumber, string fileUrl, string fileName, string customMsg)
        {
            base.Response.Clear();
            base.Response.Write("<script type=\"text/javascript\">");
            base.Response.Write(string.Concat(new object[] { "window.parent.OnUploadCompleted(", errorNumber, ",'", fileUrl.Replace("'", @"\'"), "','", fileName.Replace("'", @"\'"), "','", customMsg.Replace("'", @"\'"), "') ;" }));
            base.Response.Write("</script>");
            base.Response.End();
        }
    }
}

