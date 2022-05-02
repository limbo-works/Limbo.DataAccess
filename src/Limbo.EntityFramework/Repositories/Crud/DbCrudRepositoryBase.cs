using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Limbo.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Limbo.EntityFramework.Repositories.Crud {
    /// <inheritdoc/>
    public class DbCrudRepositoryBase<TDomain> : DbRepositoryBase<DbContext>, IDbCrudRepositoryBase<TDomain>
        where TDomain : class, GenericId, new() {

        /// <summary>
        /// The logger
        /// </summary>
        protected readonly ILogger<DbCrudRepositoryBase<TDomain>> logger;

        /// <summary>
        /// The DbSet
        /// </summary>
        protected readonly DbSet<TDomain> dbSet;

        /// <inheritdoc/>
        protected DbCrudRepositoryBase(IDbContextFactory<DbContext> contextFactory, ILogger<DbCrudRepositoryBase<TDomain>> logger) : base(contextFactory) {
            dbSet = GetDBContext().Set<TDomain>();
            this.logger = logger;
        }

        /// <inheritdoc/>
        public virtual async Task<TDomain> AddAsync(TDomain entity) {
            try {
                dbSet.Attach(entity);
                var createdEntity = await dbSet.AddAsync(entity).ConfigureAwait(false);
                return createdEntity.Entity;
            } catch (Exception e) {
                logger.LogError(e, $"Failed while adding {typeof(TDomain)}");
                throw new TaskCanceledException("Task failed", e);
            }
        }

        /// <inheritdoc/>
        public virtual async Task DeleteByIdAsync(int id) {
            try {
                var entity = await GetByIdAsync(id).ConfigureAwait(false);
                if (entity != null) {
                    dbSet.Remove(entity);
                }
            } catch (Exception e) {
                logger.LogError(e, $"Failed on Delete with {typeof(TDomain)}");
                throw new TaskCanceledException("Task failed", e);
            }
        }

        /// <inheritdoc/>
        public virtual async Task<IEnumerable<TDomain>> GetAllAsync() {
            try {
                return await dbSet.ToListAsync().ConfigureAwait(false);
            } catch (Exception e) {
                logger.LogError(e, $"Failed getting all {typeof(TDomain)}");
                throw new TaskCanceledException("Task failed", e);
            }
        }

        /// <inheritdoc/>
        public virtual async Task<TDomain?> GetByIdAsync(int id) {
            try {
                return await dbSet.FindAsync(id).ConfigureAwait(false);
            } catch (Exception e) {
                logger.LogError(e, $"Failed on GetById with {typeof(TDomain)} with id {id}");
                throw new TaskCanceledException("Task failed", e);
            }
        }

        /// <inheritdoc/>
        public virtual async Task<IQueryable<TDomain>> QueryDbSet() {
            try {
                return await Task.FromResult(dbSet).ConfigureAwait(false);
            } catch (Exception e) {
                logger.LogError(e, $"Failed query {typeof(TDomain)}");
                throw new TaskCanceledException("Task failed", e);
            }
        }

        /// <inheritdoc/>
        public virtual TDomain Update(TDomain entity) {
            try {
                dbSet.Attach(entity);
                var updatedEntity = dbSet.Update(entity);
                return updatedEntity.Entity;
            } catch (Exception e) {
                logger.LogError(e, $"Failed on Update with {typeof(TDomain)}");
                throw new ArgumentException("Failed updating entity", e);
            }
        }

        /// <summary>
        /// Adds a collection of items to a many to many releationship
        /// </summary>
        /// <typeparam name="TCollectionItemType"></typeparam>
        /// <param name="id"></param>
        /// <param name="collectionIds"></param>
        /// <param name="collectionKeySelector"></param>
        /// <returns></returns>
        /// <exception cref="TaskCanceledException"></exception>
        protected virtual async Task<TDomain> AddToCollection<TCollectionItemType>(int id, int[] collectionIds, Expression<Func<TDomain, List<TCollectionItemType>>> collectionKeySelector)
            where TCollectionItemType : class, GenericId, new() {
            try {
                Func<TDomain, List<TCollectionItemType>> compiledCollectionKeySelector = collectionKeySelector.Compile();
                var domain = await dbSet.Include(collectionKeySelector).FirstOrDefaultAsync(item => item.Id == id);
                if (domain == null) {
                    throw new ArgumentException("Id must reference a valid entity", nameof(id));
                }
                var collection = collectionIds.Where(itemId => !compiledCollectionKeySelector(domain).Any(item => item.Id == itemId));
                var loadedCollection = GetDBContext().Set<TCollectionItemType>().Where(item => collection.Any(id => id == item.Id));
                compiledCollectionKeySelector(domain).AddRange(loadedCollection);
                return domain;
            } catch (Exception e) {
                logger.LogError(e, $"Failed while adding collection {typeof(List<TCollectionItemType>)} to {typeof(TDomain)}");
                throw new TaskCanceledException("Task failed", e);
            }
        }

        /// <summary>
        /// Removes a collection of item from a many to many relationship
        /// </summary>
        /// <typeparam name="TCollectionItemType"></typeparam>
        /// <param name="id"></param>
        /// <param name="collectionIds"></param>
        /// <param name="collectionKeySelector"></param>
        /// <returns></returns>
        /// <exception cref="TaskCanceledException"></exception>
        protected virtual async Task<TDomain> RemoveFromCollection<TCollectionItemType>(int id, int[] collectionIds, Expression<Func<TDomain, List<TCollectionItemType>>> collectionKeySelector)
            where TCollectionItemType : class, GenericId, new() {
            try {
                Func<TDomain, List<TCollectionItemType>> compiledCollectionKeySelector = collectionKeySelector.Compile();
                var domain = await dbSet.Include(collectionKeySelector).FirstOrDefaultAsync(item => item.Id == id);
                if (domain == null) {
                    throw new ArgumentException("Id must reference a valid entity", nameof(id));
                }
                var collection = collectionIds.Select(itemId => new TCollectionItemType { Id = itemId });
                compiledCollectionKeySelector(domain).RemoveAll(collectionItem => collection.Any(c => c.Id == collectionItem.Id));
                return domain;
            } catch (Exception e) {
                logger.LogError(e, $"Failed while removing collection {typeof(List<TCollectionItemType>)} from {typeof(TDomain)}");
                throw new TaskCanceledException("Task failed", e);
            }
        }
    }
}
