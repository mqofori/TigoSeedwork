using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NHibernate;

namespace TigoSeedwork.Infrastructure
{
    public interface  ISessionProvider: IDisposable
    {
        ISession GetCurrentSession();
        void DisposeCurrentSesion();
        void ReplaceCurrentSession(); 
    }
}
