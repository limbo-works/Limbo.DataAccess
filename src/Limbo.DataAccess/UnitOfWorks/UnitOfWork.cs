using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Limbo.DataAccess.Repositories;

namespace Limbo.DataAccess.UnitOfWorks {
    /// <inheritdoc/>
    public class UnitOfWork<TRepository> : IUnitOfWork<TRepository>
        where TRepository : IDbRepositoryBase {
        private readonly DbContext _context;
        private IDbContextTransaction _transaction;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository"></param>
        public UnitOfWork(TRepository repository) {
            _context = repository.GetDBContext();
        }

        /// <inheritdoc/>
        public async Task BeginUnitOfWorkAsync(IsolationLevel IsolationLevel) {
            _transaction = await _context.Database.BeginTransactionAsync(IsolationLevel);
        }

        /// <inheritdoc/>
        public Task BeginUnitOfWorkAsync() {
            return BeginUnitOfWorkAsync(IsolationLevel.Serializable);
        }

        /// <inheritdoc/>
        public async Task CommitUnitOfWorkAsync() {
            if (_transaction != null) {
                try {
                    await _context.SaveChangesAsync();
                    await _transaction.CommitAsync();
                } catch (Exception) {
                    await _transaction.RollbackAsync();
                    throw;
                }
            } else {
                throw new ArgumentNullException("_transtaction", "_transtaction cannot be null");
            }
        }
    }
}
