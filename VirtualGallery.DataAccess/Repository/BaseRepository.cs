using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using VirtualGallery.BusinessLogic;
using VirtualGallery.BusinessLogic.Filtering;

namespace VirtualGallery.DataAccess.Repository
{
    public class BaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey>
        where TEntity : class
    {
        private readonly DbSet<TEntity> _dbSet;

        private IList<TEntity> _all;

        public BaseRepository(IDbContextProvider dbContextProvider)
        {
            DbContextProvider = dbContextProvider;
            _dbSet = DbContext.Set<TEntity>();
        }

        protected VirtualGalleryDbContext DbContext
        {
            get
            {
                return DbContextProvider.GetDbContext();
            }
        }

        protected virtual IQueryable<TEntity> GetAllActiveQuery()
        {
            return _dbSet;
        }

        protected IDbContextProvider DbContextProvider { get; set; }

        /// <summary>
        /// Adds the specified entity
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Add(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            _dbSet.Add(entity);
        }

        /// <summary>
        /// Adds the specified entities range
        /// </summary>
        /// <param name="entities"></param>
        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException("entities");
            }

            _dbSet.AddRange(entities);
        }

        /// <summary>
        /// Deletes the specified entity
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        /// <summary>
        /// Deletes the specified entities range
        /// </summary>
        /// <param name="entities"></param>
        public virtual void DeleteRange(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        /// <summary>
        ///  Gets all entities
        /// </summary>
        /// <returns></returns>
        public virtual IList<TEntity> GetAll()
        {
            return GetAllActiveQuery().ToList();
        }

        /// <summary>
        ///  Gets all entities
        /// </summary>
        /// <returns></returns>
        public virtual IList<TEntity> GetAll(GenericFilter<TEntity> filter)
        {
            return ApplyFilter(GetAllActiveQuery(), filter).ToList();
        }

        /// <summary>
        ///  Gets entity by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual TEntity GetById(TKey id)
        {
            return _dbSet.Find(id);
        }

        /// <summary>
        /// Updates the specified entity
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            var entry = DbContext.Entry(entity);
            if (entry.State != EntityState.Modified)
            {
                if (entry.State == EntityState.Detached)
                {
                    _dbSet.Attach(entity);
                    entry.State = EntityState.Modified;
                }
            }
        }

        /// <summary>
        /// Updates the specified entities range
        /// </summary>
        /// <param name="entities"></param>
        public virtual void UpdateRange(IEnumerable<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException("entities");
            }

            var entitiesList = entities.ToList();
            var entries = DbContext.ChangeTracker.Entries<TEntity>().Where(e => entitiesList.Any(ent => ent == e.Entity)).ToList();
            foreach (var entity in entitiesList)
            {
                var entry = entries.FirstOrDefault(e => e.Entity == entity);

                if (entry != null && entry.State != EntityState.Detached)
                    continue;

                entry = DbContext.Entry(entity);
                _dbSet.Attach(entity);
                entry.State = EntityState.Modified;
            }
        }

        /// <summary>
        /// Apply Generic filter to query
        /// </summary>
        /// <param name="query"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        protected IQueryable<TEntityType> ApplyFilter<TEntityType>(
            IQueryable<TEntityType> query, GenericFilter<TEntityType> filter)
        {
            var tagretQuery = query;
            tagretQuery = ApplySpecification(tagretQuery, filter);
            tagretQuery = ApplySorting(tagretQuery, filter);
            tagretQuery = ApplyPaging(tagretQuery, filter);
            return tagretQuery;
        }

        /// <summary>
        /// Apply Paging filter to query
        /// </summary>
        /// <param name="query"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        protected IQueryable<TEntityType> ApplyPaging<TEntityType>(IQueryable<TEntityType> query, GenericFilter<TEntityType> filter)
        {
            var tagretQuery = query;

            if (filter != null)
            {
                if (filter.Skip.HasValue)
                    tagretQuery = tagretQuery.Skip(filter.Skip.Value);
                if (filter.Take.HasValue)
                    tagretQuery = tagretQuery.Take(filter.Take.Value);
            }

            return tagretQuery;
        }

        /// <summary>
        /// Apply Sorting filter to query
        /// </summary>
        /// <param name="query"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        protected IQueryable<TEntityType> ApplySorting<TEntityType>(
            IQueryable<TEntityType> query, GenericFilter<TEntityType> filter)
        {
            var tagretQuery = query;
            if (filter != null && filter.Sorting != null)
            {
                tagretQuery = filter.Sorting.ApplySorting(tagretQuery);
            }

            return tagretQuery;
        }

        /// <summary>
        /// Apply Specification filter to query
        /// </summary>
        /// <param name="query"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        protected IQueryable<TEntityType> ApplySpecification<TEntityType>(
            IQueryable<TEntityType> query, GenericFilter<TEntityType> filter)
        {
            var tagretQuery = query;
            if (filter != null && filter.Specification != null)
            {
                tagretQuery = filter.Specification.SatisfyingEntitiesFrom(query);
            }

            return tagretQuery;
        }

        /// <summary>
        ///  Count entities with the specified criteria
        /// </summary>
        /// <returns></returns>
        protected int Count(Expression<Func<TEntity, bool>> predicate = null)
        {
            return predicate != null ? GetAllActiveQuery().Where(predicate).Count() : GetAllActiveQuery().Count();
        }

        /// <summary>
        ///  Finds a entity with the specified criteria
        /// </summary>
        /// <returns></returns>
        protected IList<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAllActiveQuery().Where(predicate).ToList();
        }

        /// <summary>
        /// The first entity matching the specified criteria
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        protected TEntity GetSingle(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAllActiveQuery().FirstOrDefault(predicate);
        }

        /// <summary>
        ///  Check has entities with the specified criteria
        /// </summary>
        /// <returns></returns>
        protected bool HasAny(Expression<Func<TEntity, bool>> predicate = null)
        {
            // Don't use Any() instead of Count() > 0
            // Entity Framework generates inefficient SQL for Any()
            // http://connect.microsoft.com/VisualStudio/feedback/details/695744/entity-framework-generates-inefficient-sql-for-any
            return Count(predicate) > 0;
        }
    }
}