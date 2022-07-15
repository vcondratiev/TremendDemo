using System.Collections.Generic;
using TremendBoard.Infrastructure.Data.Models;

namespace TremendBoard.Infrastructure.Services.Interfaces
{
    public interface ITaskRepository: IGenericRepository<ProjectTask>
    {
        public IEnumerable<ProjectTask> GetProjectTasks(string projectId, string status);
    }
}
