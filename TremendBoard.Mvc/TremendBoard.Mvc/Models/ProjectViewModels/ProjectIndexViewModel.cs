using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TremendBoard.Mvc.Models.ProjectViewModels
{
    public class ProjectIndexViewModel
    {
        public IEnumerable<ProjectDetailViewModel> Projects { get; set; }
    }
}
