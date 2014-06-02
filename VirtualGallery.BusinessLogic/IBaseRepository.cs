using System.Collections.Generic;
using VirtualGallery.BusinessLogic.Filtering;

namespace VirtualGallery.BusinessLogic
{
    public interface IBaseRepository<TEntity, TKey>
        where TEntity : class
    {
        /// <summary>
        ///  Gets all entities
        /// </summary>
        /// <returns></returns>
        IList<TEntity> GetAll();

        /// <summary>
        ///  Gets all entities
        /// </summary>
        /// <returns></returns>
        IList<TEntity> GetAll(GenericFilter<TEntity> filter);

        /// <summary>
        ///  Gets entity by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity GetById(TKey id);

        /// <summary>
        /// Adds the specified entity
        /// </summary>
        /// <param name="entity"></param>
        void Add(TEntity entity);

        /// <summary>
        /// Updates the specified entity
        /// </summary>
        /// <param name="entity"></param>
        void Update(TEntity entity);

        /// <summary>
        /// Deletes the specified entity
        /// </summary>
        /// <param name="entity"></param>
        void Delete(TEntity entity);
    }
}