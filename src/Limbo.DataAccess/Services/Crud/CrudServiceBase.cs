using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Limbo.DataAccess.Repositories.Crud;
using Limbo.DataAccess.Repositories;
using Limbo.DataAccess.Models;
using Limbo.DataAccess.Services.Models;
using System.Linq;
using Limbo.DataAccess.Settings;
using Limbo.DataAccess.UnitOfWorks;
using Microsoft.EntityFrameworkCore;

namespace Limbo.DataAccess.Services.Crud {
    /// <inheritdoc/>
    public abstract class CrudServiceBase<TDomain, TRepository> : ServiceBase<TRepository>, ICrudServiceBase<TDomain, TRepository>
        where TDomain : class, GenericId, new()
        where TRepository : IDbRepositoryBase<DbContext>, IDbCrudRepositoryBase<TDomain> {
        /// <summary>
        /// The repository
        /// </summary>
        protected readonly TRepository repository;

        /// <summary>
        /// The settings for data access
        /// </summary>
        protected readonly DataAccessSettings dataAccessSettings;

        /// <inheritdoc/>
        protected CrudServiceBase(TRepository repository, ILogger<ServiceBase<TRepository>> logger, DataAccessSettings dataAccessSettings, IUnitOfWork<TRepository> unitOfWork) : base(logger, unitOfWork, repository) {
            this.repository = repository;
            this.dataAccessSettings = dataAccessSettings;
        }

        /// <inheritdoc/>
        public virtual async Task<IServiceResponse<TDomain>> Add(TDomain entity, IsolationLevel isolationLevel) {
            return await ExecuteServiceTask(async () => {
                return await repository.AddAsync(entity);
            }, HttpStatusCode.Created, isolationLevel);
        }

        /// <inheritdoc/>
        public virtual async Task<IServiceResponse<TDomain>> Add(TDomain entity) {
            return await Add(entity, dataAccessSettings.DefaultIsolationLevel).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<IServiceResponse<TDomain>> DeleteById(int id, IsolationLevel isolationLevel) {
            return await ExecuteServiceTask<TDomain>(async () => {
                await repository.DeleteByIdAsync(id);
                return new TDomain();
            }, HttpStatusCode.NoContent, isolationLevel);
        }

        /// <inheritdoc/>
        public virtual async Task<IServiceResponse<TDomain>> DeleteById(int id) {
            return await DeleteById(id, dataAccessSettings.DefaultIsolationLevel).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<IServiceResponse<IEnumerable<TDomain>>> GetAll(IsolationLevel isolationLevel) {
            return await ExecuteServiceTask(async () => {
                return await repository.GetAllAsync();
            }, HttpStatusCode.OK, isolationLevel);
        }

        /// <inheritdoc/>
        public virtual async Task<IServiceResponse<IEnumerable<TDomain>>> GetAll() {
            return await GetAll(dataAccessSettings.DefaultIsolationLevel).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<IServiceResponse<TDomain>> GetById(int id, IsolationLevel isolationLevel) {
            return await ExecuteServiceTask(async () => {
                return await repository.GetByIdAsync(id);
            }, HttpStatusCode.OK, isolationLevel);
        }

        /// <inheritdoc/>
        public virtual async Task<IServiceResponse<TDomain>> GetById(int id) {
            return await GetById(id, dataAccessSettings.DefaultIsolationLevel).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<IServiceResponse<IQueryable<TDomain>>> QueryDbSet(IsolationLevel isolationLevel) {
            return await ExecuteServiceTask(async () => {
                return await repository.QueryDbSet();
            }, HttpStatusCode.OK, isolationLevel);
        }

        /// <inheritdoc/>
        public virtual async Task<IServiceResponse<IQueryable<TDomain>>> QueryDbSet() {
            return await QueryDbSet(dataAccessSettings.DefaultIsolationLevel).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<IServiceResponse<TDomain>> Update(TDomain entity, IsolationLevel isolationLevel) {
            return await ExecuteServiceTask(async () => {
                return await Task.Run(() => repository.Update(entity));
            }, HttpStatusCode.OK, isolationLevel);
        }

        /// <inheritdoc/>
        public virtual async Task<IServiceResponse<TDomain>> Update(TDomain entity) {
            return await Update(entity, dataAccessSettings.DefaultIsolationLevel).ConfigureAwait(false);
        }
    }
}
