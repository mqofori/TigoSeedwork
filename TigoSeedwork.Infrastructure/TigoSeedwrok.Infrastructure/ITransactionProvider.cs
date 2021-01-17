using System;

using NHibernate;

namespace TigoSeedwork.Infrastructure
{
    public interface ITransactionProvider: IDisposable
    {
        ITransaction BeginTransaction();
    }
}
