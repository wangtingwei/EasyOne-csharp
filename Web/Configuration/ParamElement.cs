namespace EasyOne.Web.Configuration
{
    using System;
    using System.Configuration;
    using System.Globalization;

    public sealed class ParamElement : ConfigurationElement
    {
        private static readonly ConfigurationProperty _Casesensitive = new ConfigurationProperty("casesensitive", typeof(bool), false);
        private static readonly ConfigurationProperty _DataType = new ConfigurationProperty("datatype", typeof(ParamType), null, ConfigurationPropertyOptions.IsRequired);
        private static readonly ConfigurationProperty _Length = new ConfigurationProperty("length", typeof(int), 0, null, _LengthValidator, ConfigurationPropertyOptions.None);
        private static readonly IntegerValidator _LengthValidator = new IntegerValidator(1, 100);
        private static readonly ConfigurationProperty _Name = new ConfigurationProperty("name", typeof(string), "", ConfigurationPropertyOptions.IsKey | ConfigurationPropertyOptions.IsRequired);
        private static readonly ConfigurationProperty _Optional = new ConfigurationProperty("optional", typeof(bool), false);
        private static ConfigurationPropertyCollection _Properties = new ConfigurationPropertyCollection();

        public ParamElement()
        {
            _Properties.Add(_Name);
            _Properties.Add(_DataType);
            _Properties.Add(_Optional);
            _Properties.Add(_Casesensitive);
            _Properties.Add(_Length);
        }

        public ParamElement(string elementName)
        {
            this.Name = elementName;
        }

        [ConfigurationProperty("casesensitive", DefaultValue=false)]
        public bool Casesensitive
        {
            get
            {
                return (bool) base[_Casesensitive];
            }
            set
            {
                base[_Casesensitive] = value;
            }
        }

        [ConfigurationProperty("type", IsRequired=true)]
        public ParamType DataType
        {
            get
            {
                return (ParamType) base[_DataType];
            }
            set
            {
                base[_DataType] = value;
            }
        }

        [ConfigurationProperty("length")]
        public int Length
        {
            get
            {
                return (int) base[_Length];
            }
            set
            {
                base[_Length] = value;
            }
        }

        [ConfigurationProperty("name", IsKey=true, IsRequired=true)]
        public string Name
        {
            get
            {
                if (!this.Casesensitive)
                {
                    return ((string) base[_Name]).ToLower(CultureInfo.CurrentCulture);
                }
                return (string) base[_Name];
            }
            set
            {
                base[_Name] = value;
            }
        }

        [ConfigurationProperty("optional", DefaultValue=false)]
        public bool Optional
        {
            get
            {
                return (bool) base[_Optional];
            }
            set
            {
                base[_Optional] = value;
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

