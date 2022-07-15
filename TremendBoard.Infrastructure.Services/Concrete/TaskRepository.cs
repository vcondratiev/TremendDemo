using System.Collections.Generic;
using System.Linq;
using TremendBoard.Infrastructure.Data.Context;
using TremendBoard.Infrastructure.Data.Models;
using TremendBoard.Infrastructure.Services.Interfaces;

namespace TremendBoard.Infrastructure.Services.Concrete
{
    public class TaskRepository : GenericRepository<ProjectTask>, ITaskRepository
    {
        public TaskRepository(TremendBoardDbContext context) : base(context)
        {
        }

        public IEnumerable<ProjectTask> GetProjectTasks(string projectId, string status)
        {
            return _context
                .ProjectTasks
                .Where(x => x.Project.Id == projectId && x.Status == status);
        }
    }
}
