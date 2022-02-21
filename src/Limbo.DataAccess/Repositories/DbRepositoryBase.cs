using Limbo.DataAccess.Contexts.Models;
using Microsoft.EntityFrameworkCore;

namespace Limbo.DataAccess.Repositories {
    /// <inheritdoc/>
    public abstract class DbRepositoryBase : IDbRepositoryBase {
        private readonly DbContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        protected DbRepositoryBase(IDbContext context) {
            _context = context.Context;
        }

        /// <inheritdoc/>
        public virtual DbContext GetDBContext() {
            return _context;
        }
    }
}
