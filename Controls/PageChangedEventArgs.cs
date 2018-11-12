namespace EasyOne.Controls
{
    using System;

    public sealed class PageChangedEventArgs : EventArgs
    {
        private readonly int _newpageindex;

        public PageChangedEventArgs(int newPageIndex)
        {
            this._newpageindex = newPageIndex;
        }

        public int NewPageIndex
        {
            get
            {
                return this._newpageindex;
            }
        }
    }
}

