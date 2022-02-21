using Microsoft.EntityFrameworkCore;

namespace Limbo.DataAccess.Repositories {
    /// <summary>
    /// A base for a repository
    /// </summary>
    public interface IDbRepositoryBase {
        /// <summary>
        /// Gets the db context
        /// </summary>
        /// <returns></returns>
        DbContext GetDBContext();
    }
}