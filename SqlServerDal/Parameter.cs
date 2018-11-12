namespace EasyOne.SqlServerDal
{
    using System;
    using System.Data;

    public class Parameter
    {
        private DbType m_DBType;
        private ParameterDirection m_Direction;
        private string m_Name;
        private int m_Size;
        private object m_Value;

        public Parameter()
        {
        }

        public Parameter(string name, DbType type, object value) : this(ParameterDirection.Input, name, type, value, 0)
        {
        }

        public Parameter(ParameterDirection direction, string name, DbType type, object value, int size)
        {
            this.m_Direction = direction;
            this.m_Name = name;
            this.m_DBType = type;
            this.m_Value = value;
            this.m_Size = size;
        }

        public DbType DBType
        {
            get
            {
                return this.m_DBType;
            }
            set
            {
                this.m_DBType = value;
            }
        }

        public ParameterDirection Direction
        {
            get
            {
                return this.m_Direction;
            }
            set
            {
                this.m_Direction = value;
            }
        }

        public string Name
        {
            get
            {
                return this.m_Name;
            }
            set
            {
                this.m_Name = value;
            }
        }

        public int Size
        {
            get
            {
                return this.m_Size;
            }
            set
            {
                this.m_Size = value;
            }
        }

        public object Value
        {
            get
            {
                return this.m_Value;
            }
            set
            {
                this.m_Value = value;
            }
        }
    }
}

