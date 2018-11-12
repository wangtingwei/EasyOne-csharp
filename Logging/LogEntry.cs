namespace EasyOne.Logging
{
    using System;

    public abstract class LogEntry : ILog
    {
        protected LogEntry()
        {
        }

        public abstract void Add(LogInfo info);
        public abstract bool Delete(DateTime time);
        public abstract bool Delete(int id);
    }
}

