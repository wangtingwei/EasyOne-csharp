namespace EasyOne.Components.CacheDependency
{
    using System.Web.Caching;

    public interface ICacheDependency
    {
        AggregateCacheDependency Dependency();
    }
}

