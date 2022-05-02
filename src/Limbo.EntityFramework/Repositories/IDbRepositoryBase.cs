using Microsoft.EntityFrameworkCore;

namespace Limbo.EntityFramework.Repositories {
    /// <summary>
    /// A base for a repository
    /// </summary>
    public interface IDbRepositoryBase<TDbContext>
        where TDbContext : DbContext {
        /// <summary>
        /// Gets the db context
        /// </summary>
        /// <returns></returns>
        TDbContext GetDBContext();
    }
}