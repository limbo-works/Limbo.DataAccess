using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Limbo.EntityFramework.Repositories.Crud;
using Limbo.EntityFramework.Repositories;
using Limbo.EntityFramework.Models;
using Limbo.EntityFramework.Services.Models;
using System.Linq;
using Limbo.EntityFramework.Settings;
using Limbo.EntityFramework.UnitOfWorks;
using Microsoft.EntityFrameworkCore;

namespace Limbo.EntityFramework.Services.Crud {
    /// <inheritdoc/>
    public abstract class CrudServiceBase<TDomain, TRepository> : ServiceBase<TRepository>, ICrudServiceBase<TDomain, TRepository>
        where TDomain : class, IGenericId, new()
        where TRepository : IDbRepositoryBase<DbContext>, IDbCrudRepositoryBase<TDomain> {
        /// <summary>
        /// The repository
        /// </summary>
        protected readonly TRepository Repository;

        /// <summary>
        /// The settings for data access
        /// </summary>
        protected readonly EntityFrameworkSettings EntityFrameworkSettings;

        /// <inheritdoc/>
        protected CrudServiceBase(TRepository repository, ILogger<ServiceBase<TRepository>> logger, EntityFrameworkSettings entityFrameworkSettings, IUnitOfWork<TRepository> unitOfWork) : base(logger, unitOfWork, repository) {
            this.Repository = repository;
            this.EntityFrameworkSettings = entityFrameworkSettings;
        }

        /// <inheritdoc/>
        public virtual async Task<IServiceResponse<TDomain>> Add(TDomain entity, IsolationLevel isolationLevel) {
            return await ExecuteServiceTask(async () => {
                return await Repository.AddAsync(entity);
            }, HttpStatusCode.Created, isolationLevel);
        }

        /// <inheritdoc/>
        public virtual async Task<IServiceResponse<TDomain>> Add(TDomain entity) {
            return await Add(entity, EntityFrameworkSettings.DefaultIsolationLevel).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<IServiceResponse<TDomain>> DeleteById(int id, IsolationLevel isolationLevel) {
            return await ExecuteServiceTask<TDomain>(async () => {
                await Repository.DeleteByIdAsync(id);
                return new TDomain();
            }, HttpStatusCode.NoContent, isolationLevel);
        }

        /// <inheritdoc/>
        public virtual async Task<IServiceResponse<TDomain>> DeleteById(int id) {
            return await DeleteById(id, EntityFrameworkSettings.DefaultIsolationLevel).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<IServiceResponse<IEnumerable<TDomain>>> GetAll(IsolationLevel isolationLevel) {
            return await ExecuteServiceTask(async () => {
                return await Repository.GetAllAsync();
            }, HttpStatusCode.OK, isolationLevel);
        }

        /// <inheritdoc/>
        public virtual async Task<IServiceResponse<IEnumerable<TDomain>>> GetAll() {
            return await GetAll(EntityFrameworkSettings.DefaultIsolationLevel).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<IServiceResponse<TDomain>> GetById(int id, IsolationLevel isolationLevel) {
            return await ExecuteServiceTask(async () => {
                return await Repository.GetByIdAsync(id);
            }, HttpStatusCode.OK, isolationLevel);
        }

        /// <inheritdoc/>
        public virtual async Task<IServiceResponse<TDomain>> GetById(int id) {
            return await GetById(id, EntityFrameworkSettings.DefaultIsolationLevel).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<IServiceResponse<IQueryable<TDomain>>> QueryDbSet(IsolationLevel isolationLevel) {
            return await ExecuteServiceTask(async () => {
                return await Repository.QueryDbSet();
            }, HttpStatusCode.OK, isolationLevel);
        }

        /// <inheritdoc/>
        public virtual async Task<IServiceResponse<IQueryable<TDomain>>> QueryDbSet() {
            return await QueryDbSet(EntityFrameworkSettings.DefaultIsolationLevel).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<IServiceResponse<TDomain>> Update(TDomain entity, IsolationLevel isolationLevel) {
            return await ExecuteServiceTask(async () => {
                return await Task.Run(() => Repository.Update(entity));
            }, HttpStatusCode.OK, isolationLevel);
        }

        /// <inheritdoc/>
        public virtual async Task<IServiceResponse<TDomain>> Update(TDomain entity) {
            return await Update(entity, EntityFrameworkSettings.DefaultIsolationLevel).ConfigureAwait(false);
        }
    }
}
