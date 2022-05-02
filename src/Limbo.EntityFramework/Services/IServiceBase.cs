using Limbo.EntityFramework.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Limbo.EntityFramework.Services {
    /// <summary>
    /// A base for defining a service
    /// </summary>
    /// <typeparam name="TRepository"></typeparam>
    public interface IServiceBase<TRepository>
        where TRepository : IDbRepositoryBase<DbContext> {
    }
}
