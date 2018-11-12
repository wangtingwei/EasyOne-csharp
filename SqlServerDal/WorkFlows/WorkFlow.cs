namespace EasyOne.SqlServerDal.WorkFlows
{
    using EasyOne.IDal.WorkFlow;
    using EasyOne.Model.WorkFlow;
    using EasyOne.SqlServerDal;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public sealed class WorkFlow : IWorkFlows
    {
        public bool Add(WorkFlowsInfo workFlowsInfo)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@FlowID", DbType.Int32, this.GetMaxId() + 1);
            cmdParams.AddInParameter("@FlowName", DbType.String, workFlowsInfo.FlowName);
            cmdParams.AddInParameter("@Description", DbType.String, workFlowsInfo.Description);
            string strSql = "INSERT INTO PE_WorkFlows(FlowID, FlowName, Description) VALUES (@FlowID, @FlowName, @Description)";
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }

        public bool Delete(int flowId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@FlowId", DbType.Int32, flowId);
            string strSql = "DELETE FROM PE_WorkFlows WHERE FlowId = @FlowId";
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }

        public bool Exists(string flowName)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@FlowName", DbType.String, flowName);
            string strSql = "SELECT COUNT(*) FROM PE_WorkFlows WHERE FlowName = @FlowName";
            return DBHelper.ExistsSql(strSql, cmdParams);
        }

        public int GetMaxId()
        {
            return DBHelper.GetMaxId("PE_WorkFlows", "FlowID");
        }

        public WorkFlowsInfo GetWorkFlowsById(int flowId)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@FlowID", DbType.Int32, flowId);
            string strCommand = "SELECT FlowID, FlowName, Description FROM PE_WorkFlows WHERE FlowID = @FlowID";
            using (NullableDataReader reader = DBHelper.ExecuteReader(CommandType.Text, strCommand, cmdParams))
            {
                if (reader.Read())
                {
                    return WorkFlowsFromrdr(reader);
                }
                return new WorkFlowsInfo(true);
            }
        }

        public IList<WorkFlowsInfo> GetWorkFlowsList()
        {
            IList<WorkFlowsInfo> list = new List<WorkFlowsInfo>();
            Parameters cmdParams = new Parameters();
            string strCommand = "SELECT FlowID, FlowName, Description FROM PE_WorkFlows ORDER BY FlowID ASC";
            using (NullableDataReader reader = DBHelper.ExecuteReader(CommandType.Text, strCommand, cmdParams))
            {
                while (reader.Read())
                {
                    list.Add(WorkFlowsInfoFromrdataReader(reader));
                }
            }
            return list;
        }

        public bool Update(WorkFlowsInfo workFlowsInfo)
        {
            Parameters cmdParams = new Parameters();
            cmdParams.AddInParameter("@FlowId", DbType.Int32, workFlowsInfo.FlowId);
            cmdParams.AddInParameter("@FlowName", DbType.String, workFlowsInfo.FlowName);
            cmdParams.AddInParameter("@Description", DbType.String, workFlowsInfo.Description);
            string strSql = "UPDATE PE_WorkFlows SET FlowName = @FlowName, Description = @Description WHERE FlowId = @FlowId";
            return DBHelper.ExecuteSql(strSql, cmdParams);
        }

        private static WorkFlowsInfo WorkFlowsFromrdr(NullableDataReader rdr)
        {
            WorkFlowsInfo info = new WorkFlowsInfo();
            info.FlowId = rdr.GetInt32("FlowId");
            info.FlowName = rdr.GetString("FlowName");
            info.Description = rdr.GetString("Description");
            return info;
        }

        private static WorkFlowsInfo WorkFlowsInfoFromrdataReader(NullableDataReader rdr)
        {
            WorkFlowsInfo info = new WorkFlowsInfo();
            info.FlowId = rdr.GetInt32("FlowId");
            info.FlowName = rdr.GetString("FlowName");
            info.Description = rdr.GetString("Description");
            return info;
        }
    }
}

