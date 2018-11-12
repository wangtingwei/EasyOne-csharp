namespace EasyOne.SqlServerDal
{
    using System;
    using System.Data;
    using System.Reflection;

    public sealed class NullableDataReader : IDataReader, IDisposable, IDataRecord, INullableReader
    {
        private IDataReader reader;

        private NullableDataReader()
        {
        }

        public NullableDataReader(IDataReader dataReader)
        {
            this.reader = dataReader;
        }

        public void Close()
        {
            this.reader.Close();
        }

        public void Dispose()
        {
            if (this.reader != null)
            {
                this.reader.Dispose();
            }
        }

        public bool GetBoolean(int i)
        {
            return this.reader.GetBoolean(i);
        }

        public bool GetBoolean(string name)
        {
            bool boolean = false;
            if (!this.IsDBNull(name))
            {
                boolean = this.GetBoolean(this.reader.GetOrdinal(name));
            }
            return boolean;
        }

        public byte GetByte(int i)
        {
            return this.reader.GetByte(i);
        }

        public byte GetByte(string name)
        {
            return this.GetByte(this.reader.GetOrdinal(name));
        }

        public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            return this.reader.GetBytes(i, fieldOffset, buffer, bufferoffset, length);
        }

        public char GetChar(int i)
        {
            return this.reader.GetChar(i);
        }

        public char GetChar(string name)
        {
            return this.GetChar(this.reader.GetOrdinal(name));
        }

        public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
        {
            return this.reader.GetChars(i, fieldoffset, buffer, bufferoffset, length);
        }

        public IDataReader GetData(int i)
        {
            return this.reader.GetData(i);
        }

        public string GetDataTypeName(int i)
        {
            return this.reader.GetDataTypeName(i);
        }

        public string GetDataTypeName(string name)
        {
            return this.reader.GetDataTypeName(this.reader.GetOrdinal(name));
        }

        public DateTime GetDateTime(int i)
        {
            return this.reader.GetDateTime(i);
        }

        public DateTime GetDateTime(string name)
        {
            if (this.IsDBNull(name))
            {
                return DateTime.Now;
            }
            return this.reader.GetDateTime(this.reader.GetOrdinal(name));
        }

        public decimal GetDecimal(int i)
        {
            return this.reader.GetDecimal(i);
        }

        public decimal GetDecimal(string name)
        {
            decimal @decimal = 0M;
            if (!this.IsDBNull(name))
            {
                @decimal = this.reader.GetDecimal(this.reader.GetOrdinal(name));
            }
            return @decimal;
        }

        public double GetDouble(int i)
        {
            return this.reader.GetDouble(i);
        }

        public double GetDouble(string name)
        {
            double num = 0.0;
            if (!this.IsDBNull(name))
            {
                num = this.reader.GetDouble(this.reader.GetOrdinal(name));
            }
            return num;
        }

        public Type GetFieldType(int i)
        {
            return this.reader.GetFieldType(i);
        }

        public Type GetFieldType(string name)
        {
            return this.reader.GetFieldType(this.reader.GetOrdinal(name));
        }

        public float GetFloat(int i)
        {
            return this.reader.GetFloat(i);
        }

        public Guid GetGuid(int i)
        {
            return this.reader.GetGuid(i);
        }

        public Guid GetGuid(string name)
        {
            return this.reader.GetGuid(this.reader.GetOrdinal(name));
        }

        public short GetInt16(int i)
        {
            return this.reader.GetInt16(i);
        }

        public short GetInt16(string name)
        {
            if (this.IsDBNull(name))
            {
                return 0;
            }
            return this.reader.GetInt16(this.reader.GetOrdinal(name));
        }

        public int GetInt32(int i)
        {
            return this.reader.GetInt32(i);
        }

        public int GetInt32(string name)
        {
            if (this.IsDBNull(name))
            {
                return 0;
            }
            return this.reader.GetInt32(this.reader.GetOrdinal(name));
        }

        public long GetInt64(int i)
        {
            return this.reader.GetInt64(i);
        }

        public long GetInt64(string name)
        {
            if (this.IsDBNull(name))
            {
                return 0L;
            }
            return this.reader.GetInt64(this.reader.GetOrdinal(name));
        }

        public string GetName(int i)
        {
            return this.reader.GetName(i);
        }

        public DateTime GetNullableDateTime(string name)
        {
            if (this.IsDBNull(name))
            {
                return DateTime.Now;//null;
            }
            return this.reader.GetDateTime(this.reader.GetOrdinal(name));
        }

        public int GetOrdinal(string name)
        {
            return this.reader.GetOrdinal(name);
        }

        public DataTable GetSchemaTable()
        {
            return this.reader.GetSchemaTable();
        }

        public float GetSingle(string name)
        {
            float @float = 0f;
            if (!this.IsDBNull(name))
            {
                @float = this.reader.GetFloat(this.reader.GetOrdinal(name));
            }
            return @float;
        }

        public string GetString(int i)
        {
            return this.reader.GetString(i);
        }

        public string GetString(string name)
        {
            string str = string.Empty;
            if (!this.IsDBNull(name))
            {
                str = this.reader.GetString(this.reader.GetOrdinal(name));
            }
            return str;
        }

        public object GetValue(int i)
        {
            return this.reader.GetValue(i);
        }

        public object GetValue(string name)
        {
            return this.reader.GetValue(this.reader.GetOrdinal(name));
        }

        public int GetValues(object[] values)
        {
            return this.reader.GetValues(values);
        }

        public bool IsDBNull(int i)
        {
            return this.reader.IsDBNull(i);
        }

        public bool IsDBNull(string name)
        {
            return this.reader.IsDBNull(this.reader.GetOrdinal(name));
        }

        public bool NextResult()
        {
            return this.reader.NextResult();
        }

        public bool Read()
        {
            return this.reader.Read();
        }

        public int Depth
        {
            get
            {
                return this.reader.Depth;
            }
        }

        public int FieldCount
        {
            get
            {
                return this.reader.FieldCount;
            }
        }

        public bool IsClosed
        {
            get
            {
                return this.reader.IsClosed;
            }
        }

        public object this[string name]
        {
            get
            {
                return this.reader[name];
            }
        }

        public object this[int i]
        {
            get
            {
                return this.reader[i];
            }
        }

        public int RecordsAffected
        {
            get
            {
                return this.reader.RecordsAffected;
            }
        }
    }
}

