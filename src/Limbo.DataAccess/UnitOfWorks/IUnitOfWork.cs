using System.Data;
using System.Threading.Tasks;
using Limbo.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Limbo.DataAccess.UnitOfWorks {
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
        /// <param name="IsolationLevel"></param>
        /// <returns></returns>
        Task BeginUnitOfWorkAsync(IsolationLevel IsolationLevel);

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
    }
}
