namespace EasyOne.Components
{
    using EasyOne.Model.Accessories;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Web.Caching;

    public sealed class SiteCache
    {
        public const int DayFactor = 0x4380;
        private static int factor = 5;
        public const int HourFactor = 720;
        public const int MinuteFactor = 12;
        private static readonly Cache s_Cache = InitSiteCache();
        public const double SecondFactor = 0.2;

        private SiteCache()
        {
        }

        public static IList<CacheInfo> AcquireCurrentCacheList(int cacheType)
        {
            string str;
            switch (cacheType)
            {
                case 1:
                    str = @"CK_Content_NodeInfo_\S*";
                    break;

                case 2:
                    str = @"CK_CommonModel_\S*";
                    break;

                case 3:
                    str = @"CK_Label_\S*";
                    break;

                case 4:
                    str = @"CK_Page_Category_\S*";
                    break;

                default:
                    str = @"CK_\S*";
                    break;
            }
            Regex regex = new Regex(str, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            IDictionaryEnumerator enumerator = s_Cache.GetEnumerator();
            List<CacheInfo> list = new List<CacheInfo>();
            while (enumerator.MoveNext())
            {
                if (regex.IsMatch(enumerator.Key.ToString()))
                {
                    CacheInfo item = new CacheInfo();
                    item.CacheName = enumerator.Key.ToString();
                    string str2 = enumerator.Value.ToString();
                    if (str2.Length > 100)
                    {
                        str2 = str2.Substring(0, 100);
                    }
                    item.CacheValue = str2;
                    list.Add(item);
                }
            }
            return list;
        }

        public static void Clear()
        {
            IDictionaryEnumerator enumerator = s_Cache.GetEnumerator();
            ArrayList list = new ArrayList();
            while (enumerator.MoveNext())
            {
                list.Add(enumerator.Key);
            }
            foreach (string str in list)
            {
                s_Cache.Remove(str);
            }
        }

        public static object Get(string key)
        {
            return s_Cache[key];
        }

        private static Cache InitSiteCache()
        {
            HttpContext current = HttpContext.Current;
            if (current != null)
            {
                return current.Cache;
            }
            return HttpRuntime.Cache;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void Insert(string key, object value)
        {
            Insert(key, value, null, 1);
        }

        public static void Insert(string key, object value, int seconds)
        {
            Insert(key, value, null, seconds);
        }

        public static void Insert(string key,object value,System.Web.Caching.CacheDependency dep)
        {
            s_Cache.Insert(key, value, dep, DateTime.Now.AddSeconds((double)(Factor * 0x21c0)), TimeSpan.Zero, CacheItemPriority.NotRemovable, null);
        }//此方法是新加的
        public static void Insert(string key, object value, System.Web.Caching.SqlCacheDependency dep)
        {
            Insert(key, value, dep, 0x21c0);
        }

        public static void Insert(string key, object value, int seconds, CacheItemPriority priority)
        {
            Insert(key, value, null, seconds, priority);
        }

        public static void Insert(string key, object value, System.Web.Caching.SqlCacheDependency dep, int seconds)
        {
            Insert(key, value, dep, seconds, CacheItemPriority.NotRemovable);
        }
        /// <summary>
        /// 向 Cache 对象中插入对象，后者具有依赖项、过期和优先级策略以及一个委托（可用于在从 Cache 移除插入项时通知应用程序）。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="dep">在存储于 ASP.NET 应用程序的 Cache 对象中的项与文件、缓存键、文件或缓存键的数组或另一个 CacheDependency 对象之间建立依附性关系。CacheDependency 类监视依附性关系，以便在任何这些对象更改时，该缓存项都会自动移除。</param>
        /// <param name="seconds"></param>
        /// <param name="priority"></param>
        public static void Insert(string key, object value, System.Web.Caching.SqlCacheDependency dep, int seconds, CacheItemPriority priority)
        {
            if (value != null)
            {
                s_Cache.Insert(key, value, dep, DateTime.Now.AddSeconds((double)(Factor * seconds)), TimeSpan.Zero, priority, null);
            }
        }

        public static void Max(string key, object value)
        {
            Max(key, value, null);
        }

        public static void Max(string key, object value, System.Web.Caching.SqlCacheDependency dep)
        {
            if (value != null)
            {
                s_Cache.Insert(key, value, dep, DateTime.MaxValue, TimeSpan.Zero, CacheItemPriority.Normal, null);
            }
        }

        public static void MicroInsert(string key, object value, int secondFactor)
        {
            if (value != null)
            {
                s_Cache.Insert(key, value, null, DateTime.Now.AddSeconds((double) (Factor * secondFactor)), TimeSpan.Zero);
            }
        }
        /// <summary>
        /// 清除指定名称的Cache对象
        /// </summary>
        /// <param name="key"></param>
        public static void Remove(string key)
        {
            s_Cache.Remove(key);
        }

        public static void RemoveByPattern(string pattern)
        {
            IDictionaryEnumerator enumerator = s_Cache.GetEnumerator();
            Regex regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            ArrayList list = new ArrayList();
            while (enumerator.MoveNext())
            {
                if (regex.IsMatch(enumerator.Key.ToString()))
                {
                    list.Add(enumerator.Key);
                }
            }
            foreach (string str in list)
            {
                s_Cache.Remove(str);
            }
        }
        /// <summary>
        /// 属性表示当前Cache列表
        /// </summary>
        public static IList<CacheInfo> CurrentCacheList
        {
            get
            {
                IDictionaryEnumerator enumerator = s_Cache.GetEnumerator();
                IList<CacheInfo> list = new List<CacheInfo>();
                while (enumerator.MoveNext())
                {
                    CacheInfo item = new CacheInfo();
                    item.CacheName = enumerator.Key.ToString();
                    string str = enumerator.Value.ToString();
                    if (str.Length > 100)
                    {
                        str = str.Substring(0, 100);
                    }
                    item.CacheValue = str;
                    list.Add(item);
                }
                return list;
            }
        }
        /// <summary>
        /// 因素
        /// </summary>
        public static int Factor
        {
            get
            {
                return factor;
            }
            set
            {
                factor = value;
            }
        }
    }
}

