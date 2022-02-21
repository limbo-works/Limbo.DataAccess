using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Limbo.DataAccess.Models;
using Limbo.DataAccess.Repositories;
using Limbo.DataAccess.Repositories.Crud;
using Limbo.DataAccess.Services.Models;

namespace Limbo.DataAccess.Services.Crud {
    /// <summary>
    /// A generic base for a CRUD service
    /// </summary>
    /// <typeparam name="TDomain"></typeparam>
    /// <typeparam name="TRepository"></typeparam>
    public interface ICrudServiceBase<TDomain, TRepository> : IServiceBase<TRepository>
        where TDomain : class, GenericId, new()
        where TRepository : IDbRepositoryBase, IDbCrudRepositoryBase<TDomain> {
        /// <summary>
        /// Queries the dbset
        /// </summary>
        /// <returns></returns>
        Task<IServiceResponse<IQueryable<TDomain>>> QueryDbSet(IsolationLevel isolationLevel = IsolationLevel.Snapshot);

        /// <summary>
        /// Gets all entities from the database
        /// </summary>
        /// <returns></returns>
        Task<IServiceResponse<IEnumerable<TDomain>>> GetAll();

        /// <summary>
        /// Gets an entity by an id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IServiceResponse<TDomain>> GetById(int id);

        /// <summary>
        /// Deletes an entity from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IServiceResponse<TDomain>> DeleteById(int id);

        /// <summary>
        /// Updates a entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<IServiceResponse<TDomain>> Update(TDomain entity);

        /// <summary>
        /// Adds an entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<IServiceResponse<TDomain>> Add(TDomain entity);
    }
}
