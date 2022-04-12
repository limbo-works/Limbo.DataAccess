using Limbo.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Limbo.DataAccess.Services {
    /// <summary>
    /// A base for defining a service
    /// </summary>
    /// <typeparam name="TRepository"></typeparam>
    public interface IServiceBase<TRepository>
        where TRepository : IDbRepositoryBase<DbContext> {
    }
}
