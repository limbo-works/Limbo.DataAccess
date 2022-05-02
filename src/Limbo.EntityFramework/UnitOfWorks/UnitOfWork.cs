using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Limbo.EntityFramework.Repositories;
using Microsoft.Extensions.Logging;

namespace Limbo.EntityFramework.UnitOfWorks {
    /// <inheritdoc/>
    public class UnitOfWork<TRepository> : IUnitOfWork<TRepository>
        where TRepository : IDbRepositoryBase<DbContext> {

        private DbContext? _context;
        private IDbContextTransaction? _transaction;
        private readonly ILogger<UnitOfWork<TRepository>> _logger;

        /// <inheritdoc/>
        public UnitOfWork(ILogger<UnitOfWork<TRepository>> logger) {
            _logger = logger;
        }

        /// <inheritdoc/>
        public virtual void SetDbContext(TRepository repository) {
            _context = repository.GetDbContext();
        }

        /// <inheritdoc/>
        public virtual async Task BeginUnitOfWorkAsync(IsolationLevel isolationLevel) {
            if (_transaction != null) {
                var exception = new InvalidOperationException("Cannot open new transaction while current transaction is not closed");
                _logger.LogError(exception, "Cannot open new transaction while current transaction is not closed");
                throw exception;
            } else {
                if (_context == null) {
                    throw new NullReferenceException("DbContext cannot be null");
                }
                _transaction = await _context.Database.BeginTransactionAsync(isolationLevel);
            }
        }

        /// <inheritdoc/>
        public virtual Task BeginUnitOfWorkAsync() {
            return BeginUnitOfWorkAsync(IsolationLevel.Serializable);
        }

        /// <inheritdoc/>
        public virtual async Task CommitUnitOfWorkAsync() {
            if (_transaction != null) {
                try {
                    if (_context == null) {
                        throw new NullReferenceException("DbContext cannot be null");
                    }
                    _context.SaveChanges();
                    await _transaction.CommitAsync();
                } catch (Exception ex) {
                    _logger.LogError(ex, "Failed to commit transaction");
                    await _transaction.RollbackAsync();
                    throw;
                }
                await _transaction.DisposeAsync();
                _transaction = null;
            } else {
                throw new NullReferenceException("Transtaction cannot be null");
            }
        }

        /// <inheritdoc/>
        public virtual async Task CloseUnitOfWork() {
            if (_transaction != null) {
                try {
                    await _transaction.RollbackAsync();
                } catch (Exception ex) {
                    _logger.LogError(ex, "Failed closing Unit of Work");
                }
                _transaction.Dispose();
                _transaction = null;
            }
        }
    }
}
