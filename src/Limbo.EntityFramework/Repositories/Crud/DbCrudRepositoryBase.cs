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
        where TDomain : class, IGenericId, new() {

        /// <summary>
        /// The logger
        /// </summary>
        protected readonly ILogger<DbCrudRepositoryBase<TDomain>> Logger;

        /// <summary>
        /// The DbSet
        /// </summary>
        protected readonly DbSet<TDomain> DbSet;

        /// <inheritdoc/>
        protected DbCrudRepositoryBase(IDbContextFactory<DbContext> contextFactory, ILogger<DbCrudRepositoryBase<TDomain>> logger) : base(contextFactory) {
            DbSet = GetDbContext().Set<TDomain>();
            this.Logger = logger;
        }

        /// <inheritdoc/>
        public virtual async Task<TDomain> AddAsync(TDomain entity) {
            try {
                DbSet.Attach(entity);
                var createdEntity = await DbSet.AddAsync(entity).ConfigureAwait(false);
                return createdEntity.Entity;
            } catch (Exception e) {
                Logger.LogError(e, $"Failed while adding {typeof(TDomain)}");
                throw new TaskCanceledException("Task failed", e);
            }
        }

        /// <inheritdoc/>
        public virtual async Task DeleteByIdAsync(int id) {
            try {
                var entity = await GetByIdAsync(id).ConfigureAwait(false);
                if (entity != null) {
                    DbSet.Remove(entity);
                }
            } catch (Exception e) {
                Logger.LogError(e, $"Failed on Delete with {typeof(TDomain)}");
                throw new TaskCanceledException("Task failed", e);
            }
        }

        /// <inheritdoc/>
        public virtual async Task<IEnumerable<TDomain>> GetAllAsync() {
            try {
                return await DbSet.ToListAsync().ConfigureAwait(false);
            } catch (Exception e) {
                Logger.LogError(e, $"Failed getting all {typeof(TDomain)}");
                throw new TaskCanceledException("Task failed", e);
            }
        }

        /// <inheritdoc/>
        public virtual async Task<TDomain?> GetByIdAsync(int id) {
            try {
                return await DbSet.FindAsync(id).ConfigureAwait(false);
            } catch (Exception e) {
                Logger.LogError(e, $"Failed on GetById with {typeof(TDomain)} with id {id}");
                throw new TaskCanceledException("Task failed", e);
            }
        }

        /// <inheritdoc/>
        public virtual async Task<IQueryable<TDomain>> QueryDbSet() {
            try {
                return await Task.FromResult(DbSet).ConfigureAwait(false);
            } catch (Exception e) {
                Logger.LogError(e, $"Failed query {typeof(TDomain)}");
                throw new TaskCanceledException("Task failed", e);
            }
        }

        /// <inheritdoc/>
        public virtual TDomain Update(TDomain entity) {
            try {
                DbSet.Attach(entity);
                var updatedEntity = DbSet.Update(entity);
                return updatedEntity.Entity;
            } catch (Exception e) {
                Logger.LogError(e, $"Failed on Update with {typeof(TDomain)}");
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
            where TCollectionItemType : class, IGenericId, new() {
            try {
                Func<TDomain, List<TCollectionItemType>> compiledCollectionKeySelector = collectionKeySelector.Compile();
                var domain = await DbSet.Include(collectionKeySelector).FirstOrDefaultAsync(item => item.Id == id);
                if (domain == null) {
                    throw new ArgumentException("Id must reference a valid entity", nameof(id));
                }
                var collection = collectionIds.Where(itemId => !compiledCollectionKeySelector(domain).Any(item => item.Id == itemId));
                var loadedCollection = GetDbContext().Set<TCollectionItemType>().Where(item => collection.Any(id => id == item.Id));
                compiledCollectionKeySelector(domain).AddRange(loadedCollection);
                return domain;
            } catch (Exception e) {
                Logger.LogError(e, $"Failed while adding collection {typeof(List<TCollectionItemType>)} to {typeof(TDomain)}");
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
            where TCollectionItemType : class, IGenericId, new() {
            try {
                Func<TDomain, List<TCollectionItemType>> compiledCollectionKeySelector = collectionKeySelector.Compile();
                var domain = await DbSet.Include(collectionKeySelector).FirstOrDefaultAsync(item => item.Id == id);
                if (domain == null) {
                    throw new ArgumentException("Id must reference a valid entity", nameof(id));
                }
                var collection = collectionIds.Select(itemId => new TCollectionItemType { Id = itemId });
                compiledCollectionKeySelector(domain).RemoveAll(collectionItem => collection.Any(c => c.Id == collectionItem.Id));
                return domain;
            } catch (Exception e) {
                Logger.LogError(e, $"Failed while removing collection {typeof(List<TCollectionItemType>)} from {typeof(TDomain)}");
                throw new TaskCanceledException("Task failed", e);
            }
        }
    }
}
