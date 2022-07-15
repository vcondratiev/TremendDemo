using System.Collections.Generic;
using TremendBoard.Infrastructure.Data.Models;

namespace TremendBoard.Mvc.Models.ScrumViewModels
{
    public class BoardViewModel
    {
        public string ProjectId { get; set; }
        public string ProjectName { get; set; }

        public IEnumerable<ProjectTask> BacklogTasks { get; set; }
        public IEnumerable<ProjectTask> InProgressTasks { get; set; }
        public IEnumerable<ProjectTask> InTestTasks { get; set; }
        public IEnumerable<ProjectTask> CompletedTasks { get; set; }

    }
}
