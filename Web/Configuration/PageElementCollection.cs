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
    /// 页面元素集合
    /// </summary>
    public class PageElementCollection : ConfigurationElementCollection, IEnumerable<PageElement>, IEnumerable
    {
        /// <summary>
        /// 当在派生的类中重写时，创建一个新的 ConfigurationElement
        /// </summary>
        /// <returns></returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new PageElement();
        }

        protected override ConfigurationElement CreateNewElement(string elementName)
        {
            return new PageElement(elementName);
        }
        /// <summary>
        /// 在派生类中重写时获取指定配置元素的元素键
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((PageElement)element).Pageurl;
        }

        public IEnumerator<PageElement> GetEnumerator()
        {
            //<GetEnumerator>d__0 d__ = new <GetEnumerator>d__0(0);
            //d__.<>4__this = this;
            //return d__;
            for (int i = 0; i < base.Count; i++)
            {
                yield return (PageElement)base.BaseGet(i);
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageurl"></param>
        /// <returns></returns>
        public PageElement this[string pageurl]
        {
            get
            {
                //return the configuration element  with the specified key
                return (PageElement)base.BaseGet(pageurl);
            }
        }
        /// <summary>
        /// 这里实现了一个属性索引器,可以把索引器用作某种默认的属性
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public PageElement this[int index]
        {
            get
            {//获取指定索引位置的 ConfigurationElement
                return (PageElement)base.BaseGet(index);
            }
            set
            {
                if (base.BaseGet(index) != null)
                {
                    //移除位于指定索引位置的 ConfigurationElement
                    base.BaseRemoveAt(index);
                }
                this.BaseAdd(index, value);
            }
        }
    }
}

