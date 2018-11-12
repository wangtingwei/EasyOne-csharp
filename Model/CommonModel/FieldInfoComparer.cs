namespace EasyOne.Model.CommonModel
{
    using System;
    using System.Collections.Generic;

    public class FieldInfoComparer : IComparer<FieldInfo>
    {
        public int Compare(FieldInfo x, FieldInfo y)
        {
            return x.OrderId.CompareTo(y.OrderId);
        }
    }
}

