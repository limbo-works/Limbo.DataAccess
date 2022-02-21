using System;
using System.Data;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Limbo.DataAccess.UnitOfWorks;
using Limbo.DataAccess.Repositories;
using Limbo.DataAccess.Services.Models;

namespace Limbo.DataAccess.Services {
    /// <inheritdoc/>
    public abstract class ServiceBase<TRepository> : UnitOfWork<TRepository>, IServiceBase<TRepository>
        where TRepository : IDbRepositoryBase {
        /// <summary>
        /// The logger
        /// </summary>
        protected ILogger<ServiceBase<TRepository>> Logger { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="logger"></param>
        protected ServiceBase(TRepository repository, ILogger<ServiceBase<TRepository>> logger) : base(repository) {
            Logger = logger;
        }

        /// <summary>
        /// Executes an func that returns a value within a unit of work
        /// </summary>
        /// <typeparam name="TDomain"></typeparam>
        /// <param name="func"></param>
        /// <param name="statusCode"></param>
        /// <param name="IsolationLevel"></param>
        /// <returns></returns>
        protected virtual async Task<IServiceResponse<TDomain>> ExecuteServiceTask<TDomain>(Func<Task<TDomain>> func, HttpStatusCode statusCode, IsolationLevel IsolationLevel)
            where TDomain : class {
            try {
                await BeginUnitOfWorkAsync(IsolationLevel);
                var response = await func.Invoke().ConfigureAwait(false);
                await CommitUnitOfWorkAsync();
                return new ServiceResponse<TDomain>(statusCode, response);
            } catch (Exception ex) {
                Logger.LogError(ex, $"Task failed with {typeof(TDomain)}");
                return new ServiceResponse<TDomain>(HttpStatusCode.InternalServerError, null);
            }
        }
    }
}
