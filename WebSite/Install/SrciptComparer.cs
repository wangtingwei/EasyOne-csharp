namespace EasyOne.WebSite.Install
{
    using System;
    using System.Collections;

    public class SrciptComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            int num = Convert.ToInt32(x.ToString().Replace(".", ""));
            int num2 = Convert.ToInt32(y.ToString().Replace(".", ""));
            return (num - num2);
        }
    }
}

