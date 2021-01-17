using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NHibernate;

namespace TigoSeedwork.Infrastructure
{
    public class TransactionProvider: ITransactionProvider
    {
        private readonly ISessionProvider _sessionProvider;

        public TransactionProvider(ISessionProvider sessionProvider)
        {
            _sessionProvider = sessionProvider;
        }

        #region ITransactionProvider
        public ITransaction BeginTransaction()
        {
            var session = _sessionProvider.GetCurrentSession();
            return session.BeginTransaction();
        }

        public void Dispose()
        {
            _sessionProvider.Dispose();
        }
        #endregion
    }
}
