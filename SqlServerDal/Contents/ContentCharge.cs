namespace EasyOne.SqlServerDal.Contents
{
    using EasyOne.IDal.Contents;
    using EasyOne.Model.Contents;
    using EasyOne.SqlServerDal;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;

    public class ContentCharge : IContentCharge
    {
        public bool Add(ContentChargeInfo contentChargeInfo)
        {
            Parameters parms = new Parameters();
            GetParameter(contentChargeInfo, parms);
            string strSql = "INSERT INTO PE_ContentCharge(GeneralId, ChargeType, InfoPoint, PitchTime, ReadTimes, DividePercent) VALUES (@GeneralId, @ChargeType, @InfoPoint, @PitchTime, @ReadTimes, @DividePercent)";
            return DBHelper.ExecuteSql(strSql, parms);
        }

        public bool BatchUpdate(ContentChargeInfo contentChargeInfo, string itemId, Dictionary<string, bool> checkItem)
        {
            StringBuilder builder = new StringBuilder();
            Parameters cmdParams = new Parameters();
            builder.Append("UPDATE PE_ContentCharge SET ");
            if (checkItem["InfoPoint"])
            {
                builder.Append("InfoPoint = @InfoPoint,");
                cmdParams.AddInParameter("@InfoPoint", DbType.Int32, contentChargeInfo.InfoPoint);
            }
            if (checkItem["ChargeType"])
            {
                builder.Append("ChargeType = @ChargeType,");
                cmdParams.AddInParameter("@ChargeType", DbType.Int32, contentChargeInfo.ChargeType);
                builder.Append("PitchTime = @PitchTime,");
                cmdParams.AddInParameter("@PitchTime", DbType.Int32, contentChargeInfo.PitchTime);
                builder.Append("ReadTimes = @ReadTimes,");
                cmdParams.AddInParameter("@ReadTimes", DbType.Int32, contentChargeInfo.ReadTimes);
            }
            if (checkItem["DividePercent"])
            {
                builder.Append("DividePercent = @DividePercent,");
                cmdParams.AddInParameter("@DividePercent", DbType.Int32, contentChargeInfo.DividePercent);
            }
            if (builder.Length <= 0x1c)
            {
                return true;
            }
            builder.Remove(builder.Length - 1, 1);
            builder.Append(" WHERE ");
            builder.Append(" GeneralID IN ( ");
            builder.Append(DBHelper.ToValidId(itemId));
            builder.Append(" )");
            return DBHelper.ExecuteSql(builder.ToString(), cmdParams);
        }

        private static ContentChargeInfo ContentChargeFromDataReader(NullableDataReader dr)
        {
            ContentChargeInfo info = new ContentChargeInfo();
            info.GeneralId = dr.GetInt32("GeneralId");
            info.ChargeType = dr.GetInt32("ChargeType");
            info.InfoPoint = dr.GetInt32("InfoPoint");
            info.PitchTime = dr.GetInt32("PitchTime");
            info.ReadTimes = dr.GetInt32("ReadTimes");
            info.DividePercent = dr.GetInt32("DividePercent");
            return info;
        }

        public void Delete(int generalId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@GeneralId", DbType.Int32, generalId);
            DBHelper.ExecuteSql("DELETE FROM PE_ContentCharge WHERE GeneralId = @GeneralId", cmdParams);
        }

        public void Delete(string generalId)
        {
            DBHelper.ExecuteSql("DELETE FROM PE_ContentCharge WHERE GeneralId IN(" + DBHelper.ToValidId(generalId) + ")");
        }

        public bool Exists(int generalId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@GeneralId", DbType.Int32, generalId);
            return DBHelper.ExistsSql("SELECT COUNT(*) FROM PE_ContentCharge WHERE GeneralId = @GeneralId", cmdParams);
        }

        public ContentChargeInfo GetContentChargeInfoById(int generalId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@GeneralId", DbType.Int32, generalId);
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql("SELECT * FROM PE_ContentCharge WHERE GeneralId = @GeneralId", cmdParams))
            {
                if (reader.Read())
                {
                    return ContentChargeFromDataReader(reader);
                }
                return new ContentChargeInfo(true);
            }
        }

        private static void GetParameter(ContentChargeInfo contentChargeInfo, Parameters parms)
        {
            parms.AddInParameter("@GeneralId", DbType.Int32, contentChargeInfo.GeneralId);
            parms.AddInParameter("@ChargeType", DbType.Int32, contentChargeInfo.ChargeType);
            parms.AddInParameter("@InfoPoint", DbType.Int32, contentChargeInfo.InfoPoint);
            parms.AddInParameter("@PitchTime", DbType.Int32, contentChargeInfo.PitchTime);
            parms.AddInParameter("@ReadTimes", DbType.Int32, contentChargeInfo.ReadTimes);
            parms.AddInParameter("@DividePercent", DbType.Int32, contentChargeInfo.DividePercent);
        }

        public bool Update(ContentChargeInfo contentChargeInfo)
        {
            Parameters parms = new Parameters();
            GetParameter(contentChargeInfo, parms);
            return DBHelper.ExecuteSql("UPDATE PE_ContentCharge SET GeneralId = @GeneralId, ChargeType = @ChargeType, InfoPoint = @InfoPoint, PitchTime = @PitchTime, ReadTimes = @ReadTimes, DividePercent = @DividePercent WHERE GeneralId = @GeneralId", parms);
        }
    }
}

