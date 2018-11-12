namespace EasyOne.SqlServerDal
{
    using System;
    using System.Runtime.CompilerServices;

    public delegate T DateReaderToModel<T>(NullableDataReader rdr);
}

