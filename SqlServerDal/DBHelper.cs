namespace EasyOne.SqlServerDal
{
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using EasyOne.Common;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Text.RegularExpressions;

    public abstract class DBHelper
    {
        protected DBHelper()
        {
        }

        public static void BuildDBParameter(Database db, DbCommand dbCommand, Parameters cmdParams)
        {
            foreach (Parameter parameter in cmdParams.Entries)
            {
                if (parameter.Direction == ParameterDirection.Input)
                {
                    db.AddInParameter(dbCommand, parameter.Name, parameter.DBType, parameter.Value);
                }
                else if (parameter.Direction == ParameterDirection.Output)
                {
                    db.AddOutParameter(dbCommand, parameter.Name, parameter.DBType, parameter.Size);
                }
            }
        }

        public static DataSet ExecuteDataSet(CommandType commandType, string strCommand, Parameters cmdParams)
        {
            DbCommand storedProcCommand;
            Database db = DatabaseFactory.CreateDatabase();
            if (commandType == CommandType.StoredProcedure)
            {
                storedProcCommand = db.GetStoredProcCommand(strCommand);
            }
            else
            {
                storedProcCommand = db.GetSqlStringCommand(strCommand);
            }
            if (cmdParams != null)
            {
                BuildDBParameter(db, storedProcCommand, cmdParams);
            }
            return db.ExecuteDataSet(storedProcCommand);
        }

        public static DataSet ExecuteDataSetProc(string storedProcName)
        {
            return ExecuteDataSet(CommandType.StoredProcedure, storedProcName, null);
        }

        public static DataSet ExecuteDataSetProc(string storedProcName, Parameters cmdParams)
        {
            return ExecuteDataSet(CommandType.StoredProcedure, storedProcName, cmdParams);
        }

        public static DataSet ExecuteDataSetSql(string strSql)
        {
            return ExecuteDataSet(CommandType.Text, strSql, null);
        }

        public static DataSet ExecuteDataSetSql(string strSql, Parameters cmdParams)
        {
            return ExecuteDataSet(CommandType.Text, strSql, cmdParams);
        }

        public static int ExecuteNonQuery(CommandType commandType, string strCommand, Parameters cmdParams)
        {
            DbCommand storedProcCommand;
            Database db = DatabaseFactory.CreateDatabase();
            if (commandType == CommandType.StoredProcedure)
            {
                storedProcCommand = db.GetStoredProcCommand(strCommand);
            }
            else
            {
                storedProcCommand = db.GetSqlStringCommand(strCommand);
            }
            if (cmdParams != null)
            {
                BuildDBParameter(db, storedProcCommand, cmdParams);
            }
            return db.ExecuteNonQuery(storedProcCommand);
        }

        public static int ExecuteNonQueryProc(string storedProcName)
        {
            return ExecuteNonQuery(CommandType.StoredProcedure, storedProcName, null);
        }

        public static int ExecuteNonQueryProc(string storedProcName, Parameters cmdParams)
        {
            return ExecuteNonQuery(CommandType.StoredProcedure, storedProcName, cmdParams);
        }

        public static int ExecuteNonQuerySql(string strSql)
        {
            return ExecuteNonQuery(CommandType.Text, strSql, null);
        }

        public static int ExecuteNonQuerySql(string strSql, Parameters cmdParams)
        {
            return ExecuteNonQuery(CommandType.Text, strSql, cmdParams);
        }

        public static bool ExecuteProc(string storedProcName)
        {
            return (ExecuteNonQueryProc(storedProcName) > 0);
        }

        public static bool ExecuteProc(string storedProcName, Parameters cmdParams)
        {
            return (ExecuteNonQueryProc(storedProcName, cmdParams) > 0);
        }

        public static NullableDataReader ExecuteReader(CommandType commandType, string strCommand, Parameters cmdParams)
        {
            DbCommand storedProcCommand;
            Database db = DatabaseFactory.CreateDatabase();
            if (commandType == CommandType.StoredProcedure)
            {
                storedProcCommand = db.GetStoredProcCommand(strCommand);
            }
            else
            {
                storedProcCommand = db.GetSqlStringCommand(strCommand);
            }
            if (cmdParams != null)
            {
                BuildDBParameter(db, storedProcCommand, cmdParams);
            }
            return new NullableDataReader(db.ExecuteReader(storedProcCommand));
        }

        public static NullableDataReader ExecuteReaderProc(string storedProcName)
        {
            return ExecuteReader(CommandType.StoredProcedure, storedProcName, null);
        }

        public static NullableDataReader ExecuteReaderProc(string storedProcName, Parameters cmdParams)
        {
            return ExecuteReader(CommandType.StoredProcedure, storedProcName, cmdParams);
        }

        public static NullableDataReader ExecuteReaderSql(string strSql)
        {
            return ExecuteReader(CommandType.Text, strSql, null);
        }

        public static NullableDataReader ExecuteReaderSql(string strSql, Parameters cmdParams)
        {
            return ExecuteReader(CommandType.Text, strSql, cmdParams);
        }

        public static object ExecuteScalar(CommandType commandType, string strCommand, Parameters cmdParams)
        {
            DbCommand storedProcCommand;
            Database db = DatabaseFactory.CreateDatabase();
            if (commandType == CommandType.StoredProcedure)
            {
                storedProcCommand = db.GetStoredProcCommand(strCommand);
            }
            else
            {
                storedProcCommand = db.GetSqlStringCommand(strCommand);
            }
            if (cmdParams != null)
            {
                BuildDBParameter(db, storedProcCommand, cmdParams);
            }
            object objA = db.ExecuteScalar(storedProcCommand);
            if (!object.Equals(objA, null) && !object.Equals(objA, DBNull.Value))
            {
                return objA;
            }
            return null;
        }

        public static object ExecuteScalarProc(string storedProcName)
        {
            return ExecuteScalar(CommandType.StoredProcedure, storedProcName, null);
        }

        public static object ExecuteScalarProc(string storedProcName, Parameters cmdParams)
        {
            return ExecuteScalar(CommandType.StoredProcedure, storedProcName, cmdParams);
        }

        public static object ExecuteScalarSql(string strSql)
        {
            return ExecuteScalar(CommandType.Text, strSql, null);
        }

        public static object ExecuteScalarSql(string strSql, Parameters cmdParams)
        {
            return ExecuteScalar(CommandType.Text, strSql, cmdParams);
        }

        public static bool ExecuteSql(string strSql)
        {
            return (ExecuteNonQuerySql(strSql) > 0);
        }

        public static bool ExecuteSql(string strSql, Parameters cmdParams)
        {
            return (ExecuteNonQuerySql(strSql, cmdParams) > 0);
        }

        public static bool Exists(CommandType commandType, string strCommand, Parameters cmdParams)
        {
            DbCommand storedProcCommand;
            Database db = DatabaseFactory.CreateDatabase();
            if (commandType == CommandType.StoredProcedure)
            {
                storedProcCommand = db.GetStoredProcCommand(strCommand);
            }
            else
            {
                storedProcCommand = db.GetSqlStringCommand(strCommand);
            }
            if (cmdParams != null)
            {
                BuildDBParameter(db, storedProcCommand, cmdParams);
            }
            if (ObjectToInt32(db.ExecuteScalar(storedProcCommand)) <= 0)
            {
                return false;
            }
            return true;
        }

        public static bool ExistsProc(string storedProcName)
        {
            return Exists(CommandType.StoredProcedure, storedProcName, null);
        }

        public static bool ExistsProc(string storedProcName, Parameters cmdParams)
        {
            return Exists(CommandType.StoredProcedure, storedProcName, cmdParams);
        }

        public static bool ExistsSql(string strSql)
        {
            return Exists(CommandType.Text, strSql, null);
        }

        public static bool ExistsSql(string strSql, Parameters cmdParams)
        {
            return Exists(CommandType.Text, strSql, cmdParams);
        }

        public static string FilterBadChar(string strchar)
        {
            if (string.IsNullOrEmpty(strchar))
            {
                return "";
            }
            return strchar.Replace("'", "");
        }

        public static int GetMaxId(string storedProcName)
        {
            return ObjectToInt32(ExecuteScalarProc(storedProcName));
        }

        public static int GetMaxId(string tableName, string fieldName)
        {
            string query = "SELECT MAX(" + fieldName + ") FROM " + tableName;
            Database database = DatabaseFactory.CreateDatabase();
            DbCommand sqlStringCommand = database.GetSqlStringCommand(query);
            return ObjectToInt32(database.ExecuteScalar(sqlStringCommand));
        }

        public static IList<T> GetModelListProc<T>(string storedProcName, Parameters cmdParams, DateReaderToModel<T> datareaderToModel)
        {
            List<T> list = new List<T>();
            using (NullableDataReader reader = ExecuteReaderProc(storedProcName, cmdParams))
            {
                if (reader.Read())
                {
                    list.Add(datareaderToModel(reader));
                }
            }
            return list;
        }

        public static IList<T> GetModelListSql<T>(string strSql, Parameters cmdParams, DateReaderToModel<T> datareaderToModel)
        {
            List<T> list = new List<T>();
            using (NullableDataReader reader = ExecuteReaderSql(strSql, cmdParams))
            {
                if (reader.Read())
                {
                    list.Add(datareaderToModel(reader));
                }
            }
            return list;
        }

        public static T GetModelProc<T>(string storedProcName, Parameters cmdParams, T model, DateReaderToModel<T> datareaderToModel)
        {
            using (NullableDataReader reader = ExecuteReaderProc(storedProcName, cmdParams))
            {
                if (reader.Read())
                {
                    model = datareaderToModel(reader);
                }
            }
            return model;
        }

        public static T GetModelSql<T>(string strSql, Parameters cmdParams, T model, DateReaderToModel<T> datareaderToModel)
        {
            using (NullableDataReader reader = ExecuteReaderSql(strSql, cmdParams))
            {
                if (reader.Read())
                {
                    model = datareaderToModel(reader);
                }
            }
            return model;
        }

        public static bool IsNumber(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }
            return DataValidator.IsNumberSign(input);
        }

        public static int ObjectToInt32(object value)
        {
            int result = 0;
            if ((!object.Equals(value, null) && !object.Equals(value, DBNull.Value)) && !int.TryParse(value.ToString(), out result))
            {
                result = 0;
            }
            return result;
        }

        public static string ToNumber(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return "0";
            }
            if (!Regex.IsMatch(input, "^[+-]?[0-9]+[.]?[0-9]*$"))
            {
                return "0";
            }
            return input;
        }

        public static string ToValidId(string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                string[] strArray = input.Split(new char[] { ',' });
                string str = "";
                for (int i = 0; i < strArray.GetLength(0); i++)
                {
                    if (IsNumber(strArray[i]))
                    {
                        str = str + strArray[i] + ",";
                    }
                }
                if (str.Length > 0)
                {
                    return str.Substring(0, str.Length - 1);
                }
            }
            return "0";
        }
    }
}

