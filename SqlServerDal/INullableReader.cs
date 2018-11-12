namespace EasyOne.SqlServerDal
{
    using System;

    public interface INullableReader
    {
        bool GetBoolean(string name);
        byte GetByte(string name);
        char GetChar(string name);
        DateTime GetDateTime(string name);
        decimal GetDecimal(string name);
        double GetDouble(string name);
        Guid GetGuid(string name);
        short GetInt16(string name);
        int GetInt32(string name);
        long GetInt64(string name);
        float GetSingle(string name);
        string GetString(string name);
        object GetValue(string name);
        bool IsDBNull(string name);
    }
}

