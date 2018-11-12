namespace EasyOne.Logging
{
    using System;

    public interface ILog
    {
        void Add(LogInfo info);
        bool Delete(DateTime time);
        bool Delete(int id);
    }
}

