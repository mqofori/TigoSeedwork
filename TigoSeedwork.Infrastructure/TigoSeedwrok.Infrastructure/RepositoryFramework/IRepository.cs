using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using TigoSeedwork.Domain;
using TigoSeedwork.Domain.Specification;
using TigoSeedwork.Infrastructure.UnitOfWork;

namespace TigoSeedwork.Infrastructure.RepositoryFramework
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        #region IRepository

        /// <summary>
        /// Unit of work
        /// </summary>
        /// <param name="unitOfWork"></param>
       // void Inject(IUnitOfWork unitOfWork);
       

        /// <summary>
        /// Get element by entity key
        /// </summary>
        /// <param name="id">Entity key value</param>
        /// <returns></returns>
        TEntity FindBy(string key);

        TEntity FindBy(int id);

        /// <summary>
        /// Add item into repository
        /// </summary>
        /// <param name="item">Item to add to repository</param>
        void Add(TEntity item);


        /// <summary>
        /// Delete item 
        /// </summary>
        /// <param name="item">Item to delete</param>
        void Remove(TEntity item);

        /// <summary>
        /// Set item as modified
        /// </summary>
        /// <param name="item">Item to modify</param>
        void Modify(TEntity item);


        /// <summary>
        /// Get all elements of type TEntity in repository
        /// </summary>
        /// <returns>List of selected elements</returns>`
        IList<TEntity> FindAll();


        List<TEntity> GetByExample(TEntity exampleInstance, params string[] propertiesToExclude);
        TEntity GetUniqueByExample(TEntity exampleInstance, params string[] propertiesToExclude);

        void CommitChanges();

        #endregion



       

        #region NLayer

        ///// <summary>
        ///// Get all elements of type TEntity in repository
        ///// </summary>
        ///// <returns>List of selected elements</returns>
        //IEnumerable<TEntity> GetAll();


        ///// <summary>
        ///// Get all elements of type TEntity that matching a
        ///// Specification <paramref name="specification"/>
        ///// </summary>
        ///// <param name="specification">Specification that result meet</param>
        ///// <returns></returns>
        //IEnumerable<TEntity> AllMatching(ISpecification<TEntity> specification);

        ///// <summary>
        ///// Get all elements of type TEntity in repository
        ///// </summary>
        ///// <param name="pageIndex">Page index</param>
        ///// <param name="pageCount">Number of elements in each page</param>
        ///// <param name="orderByExpression">Order by expression for this query</param>
        ///// <param name="ascending">Specify if order is ascending</param>
        ///// <returns>List of selected elements</returns>
        //IEnumerable<TEntity> GetPaged<KProperty>(int pageIndex, int pageCount, Expression<Func<T, KProperty>> orderByExpression, bool ascending);
        //#endregion


        ///// <summary>
        ///// Get  elements of type TEntity in repository
        ///// </summary>
        ///// <param name="filter">Filter that each element do match</param>
        ///// <returns>List of selected elements</returns>
        //IEnumerable<TEntity> GetFiltered(Expression<Func<TEntity, bool>> filter);
       
        #endregion
    }
}
