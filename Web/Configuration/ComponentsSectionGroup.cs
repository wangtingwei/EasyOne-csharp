namespace EasyOne.Web.Configuration
{
    using System.Configuration;

    public class ComponentsSectionGroup : ConfigurationSectionGroup
    {
        [ConfigurationProperty("queryStrings")]
        public QueryStringsSection QueryStrings
        {
            get
            {
                return (QueryStringsSection) base.Sections["queryStrings"];
            }
        }

        [ConfigurationProperty("security")]
        public SecuritySection Security
        {
            get
            {
                return (SecuritySection) base.Sections["Security"];
            }
        }
    }
}

