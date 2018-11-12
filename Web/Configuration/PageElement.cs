namespace EasyOne.Web.Configuration
{
    using System;
    using System.Configuration;
    using System.Globalization;

    public sealed class PageElement : ConfigurationElement//表示配置文件中的页面配置元素
    {
        private static readonly ConfigurationProperty _AbortOnError = new ConfigurationProperty("abortOnError", typeof(bool), true);
        private static readonly ConfigurationProperty _Pageurl = new ConfigurationProperty("url", typeof(string), "", ConfigurationPropertyOptions.IsKey | ConfigurationPropertyOptions.IsRequired);
        private static readonly ConfigurationProperty _Param = new ConfigurationProperty(null, typeof(ParamElementCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);
        //表示配置元素属性的集合。
        private static ConfigurationPropertyCollection _Properties = new ConfigurationPropertyCollection();

        public PageElement()
        {//将配置属性添加到集合
            _Properties.Add(_Pageurl);
            _Properties.Add(_AbortOnError);
            _Properties.Add(_Param);
        }

        public PageElement(string elementName)
        {
            this.Pageurl = elementName;
        }
        /// <summary>
        /// Web.Config的自定义节点
        /// </summary>
        [ConfigurationProperty("abortOnError", DefaultValue=true, IsRequired=true)]
        public bool AbortOnError
        {
            get
            {
                return (bool) base[_AbortOnError];
            }
            set
            {
                base[_AbortOnError] = value;
            }
        }
        /// <summary>
        /// 属性节点：页面的URL地址
        /// </summary>
        [ConfigurationProperty("url", IsKey=true, IsRequired=true)]
        public string Pageurl
        {
            get
            {
                return ((string) base[_Pageurl]).ToLower(CultureInfo.CurrentCulture);
            }
            set
            {
                base[_Pageurl] = value;
            }
        }
        /// <summary>
        /// 列表属性节点：页面的参数para
        /// </summary>
        [ConfigurationProperty("param", IsDefaultCollection=true)]
        public ParamElementCollection Param
        {
            get
            {
                return (ParamElementCollection) base[_Param];
            }
        }

        protected override ConfigurationPropertyCollection Properties
        {
            get
            {
                return _Properties;
            }
        }
    }
}

