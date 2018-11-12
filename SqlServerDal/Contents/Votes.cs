namespace EasyOne.SqlServerDal.Contents
{
    using EasyOne.IDal.Contents;
    using EasyOne.Model;
    using EasyOne.SqlServerDal;
    using System;
    using System.Data;

    public class Votes : IVotes
    {
        public void Add(VoteInfo voteInfo)
        {
            string strSql = "INSERT INTO PE_Vote(GeneralId, VoteTitle, IsAlive, VoteItem, StartTime, EndTime, VoteTotalNumber, ItemType) \r\n                              VALUES(@GeneralId, @VoteTitle, @IsAlive, @VoteItem, @StartTime, @EndTime, @VoteTotalNumber, @ItemType)";
            Parameters parms = GetParms(voteInfo);
            DBHelper.ExecuteNonQuerySql(strSql, parms);
        }

        public void Delete(string generalId)
        {
            DBHelper.ExecuteNonQuerySql("DELETE FROM PE_Vote WHERE GeneralId IN (" + generalId + ")");
        }

        private static Parameters GetParms(VoteInfo voteInfo)
        {
            Parameters parameters = new Parameters();
            parameters.AddInParameter("@GeneralId", DbType.Int32, voteInfo.GeneralId);
            parameters.AddInParameter("@VoteTitle", DbType.String, voteInfo.VoteTitle);
            parameters.AddInParameter("@IsAlive", DbType.Boolean, voteInfo.IsAlive);
            parameters.AddInParameter("@VoteItem", DbType.String, voteInfo.VoteItem);
            parameters.AddInParameter("@StartTime", DbType.DateTime, voteInfo.StartTime);
            parameters.AddInParameter("@EndTime ", DbType.DateTime, voteInfo.EndTime);
            parameters.AddInParameter("@VoteTotalNumber", DbType.Int32, voteInfo.VoteTotalNumber);
            parameters.AddInParameter("@ItemType", DbType.Int32, voteInfo.ItemType);
            return parameters;
        }

        public VoteInfo GetVoteInfoByGeneralId(int generalId)
        {
            string strSql = "SELECT * FROM PE_Vote WHERE GeneralId = @GeneralId";
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@GeneralId", DbType.Int32, generalId);
            using (NullableDataReader reader = DBHelper.ExecuteReaderSql(strSql, cmdParams))
            {
                if (reader.Read())
                {
                    VoteInfo info = new VoteInfo();
                    info.GeneralId = reader.GetInt32("GeneralId");
                    info.VoteTitle = reader.GetString("VoteTitle");
                    info.VoteItem = reader.GetString("VoteItem");
                    info.IsAlive = reader.GetBoolean("IsAlive");
                    info.StartTime = reader.GetDateTime("StartTime");
                    info.EndTime = reader.GetDateTime("EndTime");
                    info.VoteTotalNumber = reader.GetInt32("VoteTotalNumber");
                    info.ItemType = reader.GetInt32("ItemType");
                    return info;
                }
                return new VoteInfo(true);
            }
        }

        public void Update(VoteInfo voteInfo)
        {
            string strSql = "UPDATE PE_Vote \r\n                              SET VoteTitle = @VoteTitle, IsAlive = @IsAlive, VoteItem = @VoteItem, StartTime = @StartTime, EndTime = @EndTime, VoteTotalNumber = @VoteTotalNumber, ItemType = @ItemType \r\n                              WHERE GeneralId = @GeneralId";
            Parameters parms = GetParms(voteInfo);
            DBHelper.ExecuteNonQuerySql(strSql, parms);
        }
    }
}

