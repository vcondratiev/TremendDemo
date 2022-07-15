using System.Collections.Generic;
using System.Linq;
using TremendBoard.Infrastructure.Data.Context;
using TremendBoard.Infrastructure.Data.Models;
using TremendBoard.Infrastructure.Data.Models.Identity;
using TremendBoard.Infrastructure.Services.Interfaces;

namespace TremendBoard.Infrastructure.Services.Concrete
{
    public class ProjectRepository : GenericRepository<Project>, IProjectRepository
    {
        public ProjectRepository(TremendBoardDbContext context) : base(context)
        {
        }

        IEnumerable<ApplicationUserRole> IProjectRepository.GetProjectUserRoles(string projectId)
        {
            return _context.UserRoles.Where(x => x.Project.Id == projectId);
        }
    }
}
