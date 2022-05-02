using System.Data;
using System.Threading.Tasks;
using Limbo.EntityFramework.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Limbo.EntityFramework.UnitOfWorks {
    /// <summary>
    /// A unit of work
    /// </summary>
    /// <typeparam name="TRepository"></typeparam>
    public interface IUnitOfWork<TRepository>
        where TRepository : IDbRepositoryBase<DbContext> {
        /// <summary>
        /// Begins a unit of work opreation
        /// </summary>
        /// <returns></returns>
        Task BeginUnitOfWorkAsync();

        /// <summary>
        /// Begins a unit of work opreation
        /// </summary>
        /// <param name="isolationLevel"></param>
        /// <returns></returns>
        Task BeginUnitOfWorkAsync(IsolationLevel isolationLevel);

        /// <summary>
        /// Closes a unit of work in case of a faulty execution
        /// </summary>
        /// <returns></returns>
        Task CloseUnitOfWork();

        /// <summary>
        /// Commits a unit of work opreation
        /// </summary>
        /// <returns></returns>
        Task CommitUnitOfWorkAsync();

        /// <summary>
        /// Sets the dbContext of the unit of work
        /// </summary>
        /// <param name="repository"></param>
        void SetDbContext(TRepository repository);
    }
}
