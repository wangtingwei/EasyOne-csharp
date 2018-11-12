namespace EasyOne.Web.Configuration
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Diagnostics;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public sealed class SecurityCheckPageElementCollection : ConfigurationElementCollection, IEnumerable<SecurityCheckPageElement>, IEnumerable
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new SecurityCheckPageElement();
        }

        protected override ConfigurationElement CreateNewElement(string elementName)
        {
            return new SecurityCheckPageElement(elementName);
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((SecurityCheckPageElement) element).Pageurl;
        }

        public IEnumerator<SecurityCheckPageElement> GetEnumerator()
        {
            //return new <GetEnumerator>d_0(0){<>4_this=this};
            for (int i = 0; i < base.Count; i++)
            {
                yield return (SecurityCheckPageElement)base.BaseGet(i);
            }
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.BasicMap;
            }
        }

        protected override string ElementName
        {
            get
            {
                return "page";
            }
        }

        public SecurityCheckPageElement this[string pageurl]
        {
            get
            {
                return (SecurityCheckPageElement) base.BaseGet(pageurl);
            }
        }

        public SecurityCheckPageElement this[int index]
        {
            get
            {
                return (SecurityCheckPageElement) base.BaseGet(index);
            }
            set
            {
                if (base.BaseGet(index) != null)
                {
                    base.BaseRemoveAt(index);
                }
                this.BaseAdd(index, value);
            }
        }

        //[CompilerGenerated]
        //private sealed class <GetEnumerator>d__0 : IEnumerator<SecurityCheckPageElement>, IEnumerator, IDisposable
        //{
        //    private int <>1__state;
        //    private SecurityCheckPageElement <>2__current;
        //    public SecurityCheckPageElementCollection <>4__this;
        //    public IEnumerator<SecurityCheckPageElement> <>7__wrap2;
        //    public SecurityCheckPageElement <data>5__1;

        //    [DebuggerHidden]
        //    public <GetEnumerator>d__0(int <>1__state)
        //    {
        //        this.<>1__state = <>1__state;
        //    }

        //    private void <>m__Finally3()
        //    {
        //        this.<>1__state = -1;
        //        if (this.<>7__wrap2 != null)
        //        {
        //            this.<>7__wrap2.Dispose();
        //        }
        //    }

        //    private bool MoveNext()
        //    {
        //        bool flag;
        //        try
        //        {
        //            switch (this.<>1__state)
        //            {
        //                case 0:
        //                    this.<>1__state = -1;
        //                    this.<>7__wrap2 = this.<>4__this.GetEnumerator();
        //                    this.<>1__state = 1;
        //                    goto Label_006B;

        //                case 2:
        //                    this.<>1__state = 1;
        //                    goto Label_006B;

        //                default:
        //                    goto Label_007E;
        //            }
        //        Label_003C:
        //            this.<data>5__1 = this.<>7__wrap2.Current;
        //            this.<>2__current = this.<data>5__1;
        //            this.<>1__state = 2;
        //            return true;
        //        Label_006B:
        //            if (this.<>7__wrap2.MoveNext())
        //            {
        //                goto Label_003C;
        //            }
        //            this.<>m__Finally3();
        //        Label_007E:
        //            flag = false;
        //        }
        //        fault
        //        {
        //            this.System.IDisposable.Dispose();
        //        }
        //        return flag;
        //    }

        //    [DebuggerHidden]
        //    void IEnumerator.Reset()
        //    {
        //        throw new NotSupportedException();
        //    }

        //    void IDisposable.Dispose()
        //    {
        //        switch (this.<>1__state)
        //        {
        //            case 1:
        //            case 2:
        //                break;

        //            default:
        //                return;
        //                try
        //                {
        //                }
        //                finally
        //                {
        //                    this.<>m__Finally3();
        //                }
        //                break;
        //        }
        //    }

        //    SecurityCheckPageElement IEnumerator<SecurityCheckPageElement>.Current
        //    {
        //        [DebuggerHidden]
        //        get
        //        {
        //            return this.<>2__current;
        //        }
        //    }

        //    object IEnumerator.Current
        //    {
        //        [DebuggerHidden]
        //        get
        //        {
        //            return this.<>2__current;
        //        }
        //    }
        //}
    }
}

