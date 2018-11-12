namespace EasyOne.IDal.Accessories
{
    using EasyOne.Model.Accessories;

    public interface IDataBaseHandle
    {
        DataBaseVersionInfo LastVersion();
    }
}

