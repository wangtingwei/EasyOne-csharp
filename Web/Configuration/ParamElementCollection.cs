namespace EasyOne.Web.Configuration
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Diagnostics;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    /// <summary>
    /// 配置文件中的：参数元数集合
    /// </summary>
    public class ParamElementCollection : ConfigurationElementCollection, IEnumerable<ParamElement>, IEnumerable
    {
        public bool Contains(string elementName)
        {
            return (this[elementName] != null);
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new ParamElement();
        }

        protected override ConfigurationElement CreateNewElement(string elementName)
        {
            return new ParamElement(elementName);
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ParamElement)element).Name;
        }
        /// <summary>
        /// 实现迭代器
        /// </summary>
        /// <returns></returns>
        public IEnumerator<ParamElement> GetEnumerator()
        {
            //<GetEnumerator>d__0 d__ = new <GetEnumerator>d__0(0);
            //d__.<>4__this = this;
            //return d__;for
            for (int i = 0; i <base.Count;i++ )
            {
                yield return (ParamElement)base.BaseGet(i);
            }
            
        }
        /// <summary>
        /// 指示指定的 ConfigurationElement 是否存在于 ConfigurationElementCollection 中,重写此方法可实现自定义行为
        /// </summary>
        /// <param name="elementName"></param>
        /// <returns></returns>
        protected override bool IsElementName(string elementName)
        {
            return (elementName == "param");
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
                return "param";
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ParamElement this[string name]
        {
            get
            {
                if (string.IsNullOrEmpty(name))
                {
                    return null;
                }
                return (base.BaseGet(name) as ParamElement);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public ParamElement this[int index]
        {
            get
            {
                return (ParamElement)base.BaseGet(index);
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
        /// <summary>
        /// 
        /// </summary>
        protected override bool ThrowOnDuplicate
        {
            get
            {
                return true;
            }
        }

        //[CompilerGenerated]
        //private sealed class <GetEnumerator>d__0 : IEnumerator<ParamElement>, IEnumerator, IDisposable
        //{
        //    private int <>1__state;
        //    private ParamElement <>2__current;
        //    public ParamElementCollection <>4__this;
        //    public IEnumerator<ParamElement> <>7__wrap2;
        //    public ParamElement <data>5__1;

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

        //private bool MoveNext()
        //{
        //    bool flag;
        //    try
        //    {
        //        switch (this.<>1__state)
        //        {
        //            case 0:
        //                this.<>1__state = -1;
        //                this.<>7__wrap2 = this.<>4__this.GetEnumerator();
        //                this.<>1__state = 1;
        //                goto Label_006B;

        //            case 2:
        //                this.<>1__state = 1;
        //                goto Label_006B;

        //            default:
        //                goto Label_007E;
        //        }
        //    Label_003C:
        //        this.<data>5__1 = this.<>7__wrap2.Current;
        //        this.<>2__current = this.<data>5__1;
        //        this.<>1__state = 2;
        //        return true;
        //    Label_006B:
        //        if (this.<>7__wrap2.MoveNext())
        //        {
        //            goto Label_003C;
        //        }
        //        this.<>m__Finally3();
        //    Label_007E:
        //        flag = false;
        //    }
        //    fault
        //    {
        //        this.System.IDisposable.Dispose();
        //    }
        //    return flag;
        //}

        //[DebuggerHidden]
        //void IEnumerator.Reset()
        //{
        //    throw new NotSupportedException();
        //}

        //void IDisposable.Dispose()
        //{
        //    switch (this.<>1__state)
        //    {
        //        case 1:
        //        case 2:
        //            break;

        //        default:
        //            return;
        //            try
        //            {
        //            }
        //            finally
        //            {
        //                this.<>m__Finally3();
        //            }
        //            break;
        //    }
        //}

        //ParamElement IEnumerator<ParamElement>.Current
        //{
        //    [DebuggerHidden]
        //    get
        //    {
        //        return this.<>2__current;
        //    }
        //}

        //object IEnumerator.Current
        //{
        //    [DebuggerHidden]
        //    get
        //    {
        //        return this.<>2__current;
        //    }
        //}
        //}
    }
}

