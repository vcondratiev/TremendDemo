using System.ComponentModel.DataAnnotations;

namespace TremendBoard.Mvc.Models.RoleViewModels
{
    public class ApplicationRoleDetailViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
        public string Description { get; set; }
        public string UserRoleName { get; set; }
        public string StatusMessage { get; set; }
    }
}
