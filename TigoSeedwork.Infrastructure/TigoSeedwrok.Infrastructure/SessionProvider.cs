
using NHibernate;

namespace TigoSeedwork.Infrastructure
{
    public class SessionProvider : ISessionProvider
    {
        private readonly ISessionFactory _sessionFactory;
        private ISession _currentSession;


        public SessionProvider()
            : this(null)
        {
            // Console.WriteLine("Building session provider");

        }

        public SessionProvider(ISessionFactory sessionFactory)
        {
            // Console.WriteLine("Building session provider");
            //  sessionFactory = FluentHelper.SessionFactory ;
            _sessionFactory = sessionFactory;
        }

        public void Dispose()
        {
            if (_currentSession != null)
                _currentSession.Dispose();

            _currentSession = null;
        }


        #region ISessionProvider Implementation

        public ISession GetCurrentSession()
        {
            if (null == _currentSession)
            {
                _currentSession = _sessionFactory.OpenSession();
            }
            return _currentSession;
        }

        public void ReplaceCurrentSession()
        {
            _currentSession.Dispose();
            _currentSession = null;
        }

        public void DisposeCurrentSession()
        {
            _currentSession.Dispose();
            _currentSession = null;
        }
        #endregion 
    }
}
