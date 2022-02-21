using Limbo.DataAccess.Repositories;
using Limbo.DataAccess.UnitOfWorks;

namespace Limbo.DataAccess.Services {
    /// <summary>
    /// A base for defining a service
    /// </summary>
    /// <typeparam name="TRepository"></typeparam>
    public interface IServiceBase<TRepository> : IUnitOfWork<TRepository>
        where TRepository : IDbRepositoryBase {
    }
}
