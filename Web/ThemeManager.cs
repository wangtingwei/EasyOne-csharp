namespace EasyOne.Web
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Web;

    public sealed class ThemeManager
    {
        private ThemeManager()
        {
        }

        public static IList<Theme> AdminThemesList()
        {
            return GetThemesList("Admin");
        }

        private static IList<Theme> GetThemesList(string startsWith)
        {
            DirectoryInfo[] directories = new DirectoryInfo(HttpContext.Current.Server.MapPath("~/App_Themes")).GetDirectories();
            IList<Theme> list = new List<Theme>();
            foreach (DirectoryInfo info2 in directories)
            {
                if (string.IsNullOrEmpty(startsWith) || info2.Name.StartsWith(startsWith, StringComparison.CurrentCultureIgnoreCase))
                {
                    Theme item = new Theme(info2.Name);
                    list.Add(item);
                }
            }
            return list;
        }

        public static IList<Theme> ThemesList()
        {
            return GetThemesList(string.Empty);
        }

        public static IList<Theme> UserThemesList()
        {
            return GetThemesList("User");
        }
    }
}

