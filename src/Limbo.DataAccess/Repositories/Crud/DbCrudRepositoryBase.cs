using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Limbo.DataAccess.Contexts.Models;
using Limbo.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Limbo.DataAccess.Repositories.Crud {
    /// <inheritdoc/>
    public class DbCrudRepositoryBase<TDomain> : DbRepositoryBase, IDbCrudRepositoryBase<TDomain>
        where TDomain : class, GenericId, new() {
        /// <summary>
        /// The logger
        /// </summary>
        protected readonly ILogger<DbCrudRepositoryBase<TDomain>> logger;

        /// <summary>
        /// The DbSet
        /// </summary>
        protected readonly DbSet<TDomain> dbSet;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="logger"></param>
        protected DbCrudRepositoryBase(IDbContext dbContext, ILogger<DbCrudRepositoryBase<TDomain>> logger) : base(dbContext) {
            dbSet = dbContext.Context.Set<TDomain>();
            this.logger = logger;
        }

        /// <inheritdoc/>
        public virtual async Task<TDomain> AddAsync(TDomain entity) {
            try {
                dbSet.Attach(entity);
                var createdEntity = await dbSet.AddAsync(entity);
                return createdEntity.Entity;
            } catch (Exception e) {
                logger.LogError(e, $"Failed while adding {typeof(TDomain)}");
                throw new TaskCanceledException("Task failed");
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
                throw new TaskCanceledException("Task failed");
            }
        }

        /// <inheritdoc/>
        public virtual async Task<IEnumerable<TDomain>> GetAllAsync() {
            try {
                return await dbSet.ToListAsync();
            } catch (Exception e) {
                logger.LogError(e, $"Failed getting all {typeof(TDomain)}");
                throw new TaskCanceledException("Task failed");
            }
        }

        /// <inheritdoc/>
        public virtual async Task<TDomain?> GetByIdAsync(int id) {
            try {
                return await dbSet.FindAsync(id);
            } catch (Exception e) {
                logger.LogError(e, $"Failed on GetById with {typeof(TDomain)} with id {id}");
                throw new TaskCanceledException("Task failed");
            }
        }

        /// <inheritdoc/>
        public virtual async Task<IQueryable<TDomain>> QueryDbSet() {
            try {
                return await Task.FromResult(dbSet).ConfigureAwait(false);
            } catch (Exception e) {
                logger.LogError(e, $"Failed query {typeof(TDomain)}");
                throw new TaskCanceledException("Task failed");
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
                throw new ArgumentException("Failed updating entity");
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
        protected virtual async Task<TDomain> AddToCollection<TCollectionItemType>(int id, int[] collectionIds, Func<TDomain, List<TCollectionItemType>> collectionKeySelector)
            where TCollectionItemType : class, GenericId, new() {
            try {
                var domain = await GetByIdAsync(id).ConfigureAwait(false);
                if (domain == null) {
                    throw new ArgumentException("Id must reference a valid entity", nameof(id));
                }
                var collection = collectionIds.Where(itemId => !collectionKeySelector(domain).Any(item => item.Id == itemId)).Select(itemId => new TCollectionItemType { Id = itemId });
                collectionKeySelector(domain).AddRange(collection);
                return domain;
            } catch (Exception e) {
                logger.LogError(e, $"Failed while adding collection {typeof(List<TCollectionItemType>)} to {typeof(TDomain)}");
                throw new TaskCanceledException("Task failed");
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
        protected virtual async Task<TDomain> RemoveFromCollection<TCollectionItemType>(int id, int[] collectionIds, Func<TDomain, List<TCollectionItemType>> collectionKeySelector)
            where TCollectionItemType : class, GenericId, new() {
            try {
                var domain = await GetByIdAsync(id).ConfigureAwait(false);
                if (domain == null) {
                    throw new ArgumentException("Id must reference a valid entity", nameof(id));
                }
                var collection = collectionIds.Select(itemId => new TCollectionItemType { Id = itemId });
                collectionKeySelector(domain).RemoveAll(collectionItem => collection.Any(c => c.Id == collectionItem.Id));
                return domain;
            } catch (Exception e) {
                logger.LogError(e, $"Failed while removing collection {typeof(List<TCollectionItemType>)} from {typeof(TDomain)}");
                throw new TaskCanceledException("Task failed");
            }
        }
    }
}
