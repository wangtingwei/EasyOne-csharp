namespace EasyOne.IDal
{
    using System.Web.Caching;

    public interface IItemCacheDependency
    {
        AggregateCacheDependency GetDependency();
    }
}

