namespace EasyOne.SqlServerDal
{
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class Parameters
    {
        private IList<Parameter> m_Entries;

        public Parameters()
        {
            this.m_Entries = new List<Parameter>();
        }

        public Parameters(string name, DbType type, object value)
        {
            this.m_Entries = new List<Parameter>();
            this.m_Entries.Add(new Parameter(name, type, value));
        }

        public void AddInParameter(string name, DbType type, object value)
        {
            this.m_Entries.Add(new Parameter(name, type, value));
        }

        public void AddOutParameter(string name, DbType type, int size)
        {
            this.m_Entries.Add(new Parameter(ParameterDirection.Output, name, type, null, size));
        }

        public IList<Parameter> Entries
        {
            get
            {
                return this.m_Entries;
            }
            set
            {
                this.m_Entries = value;
            }
        }
    }
}

