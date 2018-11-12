namespace EasyOne.IDal.Analytics
{
    using System;

    public interface ITimeReport
    {
        int[] GetAllList();
        int[] GetList(string value);
    }
}

