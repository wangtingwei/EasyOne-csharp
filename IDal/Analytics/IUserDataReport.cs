namespace EasyOne.IDal.Analytics
{
    using System;
    using System.Collections.Generic;

    public interface IUserDataReport
    {
        Dictionary<string, int> GetList(int startRowIndexId, int maxiNumRows);

        int Count { get; }

        string MaxValue { get; }

        int Sum { get; }
    }
}

