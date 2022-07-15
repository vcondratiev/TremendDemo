using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;
using System.Transactions;
using TremendBoard.Infrastructure.Data.Context;
using TremendBoard.Infrastructure.Services.Interfaces;

namespace TremendBoard.Infrastructure.Services.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TremendBoardDbContext _context;

        private IDbContextTransaction _transaction;

        public UnitOfWork(TremendBoardDbContext context)
        {
            _context = context;
            
            Project = new ProjectRepository(_context);
            ProjectTask = new TaskRepository(_context);
            UserRole = new UserRoleRepository(_context);
            User = new UserRepository(_context);
            Role = new RoleRepository(_context);
        }

        public async Task StartTransactionAsync()
        {
            _transaction?.Dispose();
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
            }
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<int> SaveChangesAsync(bool ensureAutoHistory = false, params IUnitOfWork[] unitOfWorks)
        {
            using var ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            var count = 0;
            foreach (var unitOfWork in unitOfWorks)
            {
                count++;
                await unitOfWork.SaveAsync().ConfigureAwait(false);
            }

            count += await SaveChangesAsync(ensureAutoHistory);

            ts.Complete();

            return count;
        }

        public IProjectRepository Project
        {
            get;
            private set;
        }

        public ITaskRepository ProjectTask
        {
            get;
            private set;
        }

        public IUserRoleRepository UserRole
        {
            get;
            private set;
        }

        public IUserRepository User
        {
            get;
            private set;
        }

        public IRoleRepository Role
        {
            get;
            private set;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}
