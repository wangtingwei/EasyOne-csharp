namespace EasyOne.Logging
{
    using System;

    public class FileLog : LogEntry
    {
        public override void Add(LogInfo info)
        {
        }

        public override bool Delete(DateTime time)
        {
            return (time < DateTime.Now);
        }

        public override bool Delete(int id)
        {
            return (0 < id);
        }
    }
}

