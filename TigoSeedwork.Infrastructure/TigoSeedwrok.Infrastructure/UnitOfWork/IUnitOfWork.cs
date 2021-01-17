using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TigoSeedwork.Domain;

namespace TigoSeedwork.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork
    {
        void RegisterAdded(IEntity entity, IUnitOfWorkRepository repository);
        void RegisterChanged(IEntity entity, IUnitOfWorkRepository repository);
        void RegisterRemoved(IEntity entity, IUnitOfWorkRepository repository);
        void Commit();


        #region Old 1
        //void Save(IEntity Entity);
        //void Delete(IEntity Entity);
        //void Commit();
        #endregion
       
    }
}
