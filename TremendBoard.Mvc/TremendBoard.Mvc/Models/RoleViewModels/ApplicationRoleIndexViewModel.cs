using System.Collections.Generic;

namespace TremendBoard.Mvc.Models.RoleViewModels
{
    public class ApplicationRoleIndexViewModel
    {
        public IEnumerable<ApplicationRoleDetailViewModel> Roles { get; set; }
    }
}
