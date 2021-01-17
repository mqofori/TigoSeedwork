using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using  TigoSeedwork.Domain;
using System.Transactions;
using TigoSeedwork.Infrastructure;

namespace TigoSeedwork.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork 
    {
        private Dictionary<IEntity, IUnitOfWorkRepository> _addedEntities;
        private Dictionary<IEntity, IUnitOfWorkRepository> _changedEntities;
        private Dictionary<IEntity, IUnitOfWorkRepository> _deletedEntities;
        
        public UnitOfWork()
        {
            this._addedEntities = new Dictionary<IEntity, IUnitOfWorkRepository>();
            this._changedEntities = new Dictionary<IEntity, IUnitOfWorkRepository>();
            this._deletedEntities = new Dictionary<IEntity, IUnitOfWorkRepository>();
        }

        #region IUnitOfWork Members         

        public void RegisterAdded(IEntity entity, IUnitOfWorkRepository repository)
        {
           this._addedEntities.Add(entity, repository);
        }

        public void RegisterChanged(IEntity entity, IUnitOfWorkRepository repository)
        {
            this._changedEntities.Add(entity, repository);
        }

        public void RegisterRemoved(Domain.IEntity entity, IUnitOfWorkRepository repository)
        {
            this._deletedEntities.Add(entity, repository);
        }

        public void Commit()
        {
           // throw new NotImplementedException();
            using (TransactionScope scope = new TransactionScope())
            {
                foreach (IEntity entity in this._deletedEntities.Keys)
                {
                    this._deletedEntities[entity].PersistDeletedItem(entity);
                }

                foreach (IEntity entity in this._addedEntities.Keys)
                {
                    this._addedEntities[entity].PersistDeletedItem(entity);
                }

                foreach (IEntity entity in this._changedEntities.Keys)
                {
                    this._changedEntities[entity].PersistDeletedItem(entity);
                }

                this._addedEntities.Clear();
                this._changedEntities.Clear();
                this._deletedEntities.Clear();
            }
        }
        #endregion
    }
}
