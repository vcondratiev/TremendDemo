using System;
using System.Threading.Tasks;

namespace TremendBoard.Infrastructure.Services.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IProjectRepository Project
        {
            get;
        }
        ITaskRepository ProjectTask
        {
            get;
        }

        IUserRoleRepository UserRole
        {
            get;
        }

        IUserRepository User
        {
            get;
        }

        IRoleRepository Role
        {
            get;
        }

        Task<int> SaveAsync();
    }
}
