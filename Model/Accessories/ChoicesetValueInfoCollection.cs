namespace EasyOne.Model.Accessories
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;

    public class ChoicesetValueInfoCollection : ICollection<ChoicesetValueInfo>, IEnumerable<ChoicesetValueInfo>, IEnumerable
    {
        private IList<ChoicesetValueInfo> list = new List<ChoicesetValueInfo>();

        public void Add(ChoicesetValueInfo item)
        {
            this.list.Add(item);
        }

        public void Clear()
        {
            this.list.Clear();
        }

        public bool Contains(ChoicesetValueInfo item)
        {
            return this.list.Contains(item);
        }

        public void CopyTo(ChoicesetValueInfo[] array, int arrayIndex)
        {
            this.list.CopyTo(array, arrayIndex);
        }

        public IEnumerator<ChoicesetValueInfo> GetEnumerator()
        {
            return this.list.GetEnumerator();
        }

        public void Insert(int index, ChoicesetValueInfo item)
        {
            this.list.Insert(index, item);
        }

        public bool Remove(ChoicesetValueInfo item)
        {
            return this.list.Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public int Count
        {
            get
            {
                return this.list.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return this.list.IsReadOnly;
            }
        }

        public ChoicesetValueInfo this[int index]
        {
            get
            {
                if ((index >= 0) && (index < this.list.Count))
                {
                    return this.list[index];
                }
                return new ChoicesetValueInfo();
            }
        }
    }
}

