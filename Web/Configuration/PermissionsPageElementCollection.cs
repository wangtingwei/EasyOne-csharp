namespace EasyOne.Web.Configuration
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Diagnostics;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public sealed class PermissionsPageElementCollection : ConfigurationElementCollection, IEnumerable<PermissionsPageElement>, IEnumerable
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new PermissionsPageElement();
        }

        protected override ConfigurationElement CreateNewElement(string elementName)
        {
            return new PermissionsPageElement(elementName);
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((PermissionsPageElement)element).Pageurl;
        }

        public IEnumerator<PermissionsPageElement> GetEnumerator()
        {
            //<GetEnumerator>d__0 d__ = new <GetEnumerator>d__0(0);
            //d__.<>4__this = this;
            //return d__;
            for (int i = 0; i < base.Count;i++ )
            {
                yield return (PermissionsPageElement)base.BaseGet(i);
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

        public PermissionsPageElement this[string pageurl]
        {
            get
            {
                return (PermissionsPageElement)base.BaseGet(pageurl);
            }
        }

        public PermissionsPageElement this[int index]
        {
            get
            {
                return (PermissionsPageElement)base.BaseGet(index);
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

        
    }
}

