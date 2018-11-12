namespace EasyOne.Contents
{
    using EasyOne.Common;
    using EasyOne.IDal.Contents;
    using EasyOne.Model.Contents;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using EasyOne.DalFactory;

    public sealed class ContentCharge
    {
        private static readonly IContentCharge dal = DataAccess.CreateContentCharge();

        private ContentCharge()
        {
        }

        public static bool Add(ContentChargeInfo contentChargeInfo)
        {
            return dal.Add(contentChargeInfo);
        }

        public static bool BatchUpdate(ContentChargeInfo contentChargeInfo, string itemId, Dictionary<string, bool> checkItem, int batchType)
        {
            if (!DataValidator.IsValidId(itemId))
            {
                return false;
            }
            if (!checkItem.ContainsValue(true))
            {
                return false;
            }
            StringBuilder sb = new StringBuilder();
            int generalId = 0;
            if (batchType == 1)
            {
                foreach (CommonModelInfo info in ContentManage.GetCommonModelInfoList(itemId))
                {
                    if (!dal.Exists(info.GeneralId))
                    {
                        contentChargeInfo.GeneralId = info.GeneralId;
                        dal.Add(contentChargeInfo);
                    }
                    else
                    {
                        StringHelper.AppendString(sb, info.GeneralId.ToString());
                    }
                }
            }
            else
            {
                foreach (string str in itemId.Split(new char[] { ',' }))
                {
                    generalId = DataConverter.CLng(str);
                    if (generalId != 0)
                    {
                        if (!dal.Exists(generalId))
                        {
                            contentChargeInfo.GeneralId = generalId;
                            dal.Add(contentChargeInfo);
                        }
                        else
                        {
                            StringHelper.AppendString(sb, str);
                        }
                    }
                }
            }
            if (sb.Length > 0)
            {
                return dal.BatchUpdate(contentChargeInfo, sb.ToString(), checkItem);
            }
            return true;
        }

        public static void Delete(int generalId)
        {
            dal.Delete(generalId);
        }

        public static void Delete(string generalId)
        {
            if (DataValidator.IsValidId(generalId))
            {
                dal.Delete(generalId);
            }
        }

        public static bool Exists(int generalId)
        {
            return dal.Exists(generalId);
        }

        public static ContentChargeInfo GetContentChargeInfoById(int generalId)
        {
            return dal.GetContentChargeInfoById(generalId);
        }

        public static bool Update(ContentChargeInfo contentChargeInfo)
        {
            return dal.Update(contentChargeInfo);
        }
    }
}

