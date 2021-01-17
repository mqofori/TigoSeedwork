using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TigoSeedwork.Domain;
//using TigoSeedwork.Infrastructure.UnitOfWork;

namespace TigoSeedwork.Infrastructure.RepositoryFramework
{
    public abstract class RepositoryBase<T>
        : IRepository<T> where T: IEntity  //IUnitOfWorkRepository  where T: IEntity
    {
        #region Private Fields


      //  private IUnitOfWork _unitOfWork;

        #endregion

        #region Constructors

        protected RepositoryBase()           
        {
        }

        

        #endregion

        #region IRepository<T> Members

        
        public abstract T FindBy(string key);


        public abstract T FindBy(int id);
        

        public abstract void Add(T item);


        public abstract void Remove(T item);


        public abstract void Modify(T item);
        public abstract IList<T> FindAll();

        public abstract List<T> GetByExample(T exampleInstance, params string[] propertiesToExclude);


        public abstract T GetUniqueByExample(T exampleInstance, params string[] propertiesToExclude);


        public abstract void CommitChanges();
        
        #endregion    
       
    }
}
