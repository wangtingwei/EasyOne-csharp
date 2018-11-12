namespace EasyOne.SqlServerDal
{
    using System;
    using System.Data;

    public class CommonListParameters : Parameters
    {
        private string m_Filter;
        private int m_PageSize;
        private string m_SortColumn;
        private EasyOne.SqlServerDal.Sorts m_Sorts;
        private int m_StartRows;
        private string m_StrColumn;
        private string m_TableName;
        private int m_Total;

        public CommonListParameters() : this(1, 20, "", "*", "", EasyOne.SqlServerDal.Sorts.Desc, "")
        {
        }

        public CommonListParameters(int startRows, int pageSize) : this(startRows, pageSize, "", "*", "", EasyOne.SqlServerDal.Sorts.Desc, "")
        {
        }

        public CommonListParameters(int startRows, int pageSize, string tableName, string strColumn, string sortColumn, EasyOne.SqlServerDal.Sorts sorts, string filter)
        {
            this.m_StartRows = startRows;
            this.m_PageSize = pageSize;
            this.m_TableName = tableName;
            this.m_StrColumn = strColumn;
            this.m_SortColumn = sortColumn;
            this.m_Sorts = sorts;
            this.m_Filter = filter;
        }

        public void CreateParameter()
        {
            base.Entries.Add(new Parameter("@StartRows", DbType.Int32, this.m_StartRows));
            base.Entries.Add(new Parameter("@PageSize", DbType.Int32, this.m_PageSize));
            base.Entries.Add(new Parameter("@TableName", DbType.String, this.m_TableName));
            base.Entries.Add(new Parameter("@StrColumn", DbType.String, this.m_StrColumn));
            base.Entries.Add(new Parameter("@SortColumn", DbType.String, this.m_SortColumn));
            if (this.m_Sorts == EasyOne.SqlServerDal.Sorts.Desc)
            {
                base.Entries.Add(new Parameter("@Sorts", DbType.String, "DESC"));
            }
            else
            {
                base.Entries.Add(new Parameter("@Sorts", DbType.String, "ASC"));
            }
            base.Entries.Add(new Parameter("@Filter", DbType.String, this.m_Filter));
            base.Entries.Add(new Parameter(ParameterDirection.Output, "@Total", DbType.Int32, null, 10));
        }

        public string Filter
        {
            get
            {
                return this.m_Filter;
            }
            set
            {
                this.m_Filter = value;
            }
        }

        public int PageSize
        {
            get
            {
                return this.m_PageSize;
            }
            set
            {
                this.m_PageSize = value;
            }
        }

        public string SortColumn
        {
            get
            {
                return this.m_SortColumn;
            }
            set
            {
                this.m_SortColumn = value;
            }
        }

        public EasyOne.SqlServerDal.Sorts Sorts
        {
            get
            {
                return this.m_Sorts;
            }
            set
            {
                this.m_Sorts = value;
            }
        }

        public int StartRows
        {
            get
            {
                return this.m_StartRows;
            }
            set
            {
                this.m_StartRows = value;
            }
        }

        public string StrColumn
        {
            get
            {
                return this.m_StrColumn;
            }
            set
            {
                this.m_StrColumn = value;
            }
        }

        public string TableName
        {
            get
            {
                return this.m_TableName;
            }
            set
            {
                this.m_TableName = value;
            }
        }

        public int Total
        {
            get
            {
                return this.m_Total;
            }
            set
            {
                this.m_Total = value;
            }
        }
    }
}

