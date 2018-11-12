namespace EasyOne.Controls.ExtendedUploadFile
{
    using System;
    using System.Collections;
    using System.Reflection;

    public class UploadFileCollection : ICollection, IEnumerable
    {
        private ArrayList filelist = new ArrayList();

        public void Add(UploadFile file)
        {
            this.filelist.Add(file);
        }

        public void CopyTo(Array array, int index)
        {
            this.filelist.CopyTo(array, index);
        }

        public IEnumerator GetEnumerator()
        {
            return this.filelist.GetEnumerator();
        }

        public int Count
        {
            get
            {
                return this.filelist.Count;
            }
        }

        public bool IsSynchronized
        {
            get
            {
                return this.filelist.IsSynchronized;
            }
        }

        public UploadFile this[int index]
        {
            get
            {
                return (UploadFile) this.filelist[index];
            }
        }

        public object SyncRoot
        {
            get
            {
                return this.filelist.SyncRoot;
            }
        }
    }
}

