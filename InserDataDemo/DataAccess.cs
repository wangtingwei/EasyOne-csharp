using System;
using System.Collections.Generic;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace InserDataDemo
{
    public class DataAccess
    {
        private string connStr;
        private SqlConnection conn;
        private SqlCommand comd = new SqlCommand();
        private SqlDataAdapter adapter;
        public DataAccess()
        {
            connStr = System.Configuration.ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
            conn = new SqlConnection(connStr);
            adapter = new SqlDataAdapter(comd);
            comd.Connection = conn;
        }

        //定义一个方法，接受一个班级名的参数，返回一张DataTable, 里面是该班级的所有学生的信息
        public DataTable GetStuInfoByBan(string banName)
        {
            DataTable dt = new DataTable();

            comd.CommandText = string.Format("select s_id,s_name from T_Student as s,T_Banji as b where b.b_name = '{0}' ",banName);
            try
            {
                conn.Open();
                adapter.Fill(dt);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return dt;
        }

        //定义一个方法，返回班级表中的数据
        public DataTable GetStuInfo()
        {
            DataTable dt = new DataTable();

            comd.CommandText = "select * from T_Banji where 1=1 ";
            try
            {
                conn.Open();
                adapter.Fill(dt);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return dt;
        }


        #region 插入操作
        public bool InserDt(int count)
        {
            bool isOk = false;
            
            
            
           
            try
            {
                conn.Open();
                SqlTransaction tran = conn.BeginTransaction();
                comd.Transaction = tran;
                int num = 0;
                for (int i = 1; i < 5001; i++)
                {
                    comd.CommandText = comTextFormat(i);
                    int a = comd.ExecuteNonQuery();
                    if (a >= 1)
                    {
                        num++;
                    }
                }
                if (num == 5000)
                {
                    tran.Commit();
                    isOk = true;
                }
                else
                {
                    tran.Rollback();
                }
            }
            catch (Exception )
            {

                throw;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            
            return isOk;
        }
        private string comTextFormat( int a)
        {
            string result = null;
            result = string.Format("insert into T_Student(s_name,b_id) values('名字{0}',1)", a);
            return result;
        }
        #endregion
    }
}
