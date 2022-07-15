using System.Collections.Generic;
using TremendBoard.Infrastructure.Data.Models.Identity;

namespace TremendBoard.Infrastructure.Services.Interfaces
{
    public interface IProjectRepository : IGenericRepository<Data.Models.Project>
    {
        IEnumerable<ApplicationUserRole> GetProjectUserRoles(string projectId);
    }
}
