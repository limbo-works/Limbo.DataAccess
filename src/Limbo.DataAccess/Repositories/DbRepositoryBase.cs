using System;
using Microsoft.EntityFrameworkCore;

namespace Limbo.DataAccess.Repositories {
    /// <inheritdoc/>
    public abstract class DbRepositoryBase<TDbContext> : IDbRepositoryBase<TDbContext>
        where TDbContext : DbContext {
        private readonly IDbContextFactory<TDbContext> _contextFactory;
        private TDbContext? _context;

        /// <inheritdoc/>
        protected DbRepositoryBase(IDbContextFactory<TDbContext> contextFactory) {
            _contextFactory = contextFactory;
        }

        /// <inheritdoc/>
        public virtual TDbContext GetDBContext() {
            if (_context == null) {
                _context = _contextFactory.CreateDbContext();
                if (_context == null) {
                    throw new NullReferenceException("DbContext wasn't created.");
                }
                return _context;
            } else {
                return _context;
            }
        }
    }
}
