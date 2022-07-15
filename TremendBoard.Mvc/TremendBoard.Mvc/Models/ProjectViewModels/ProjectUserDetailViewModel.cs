using System.ComponentModel.DataAnnotations;

namespace TremendBoard.Mvc.Models.ProjectViewModels
{
    public class ProjectUserDetailViewModel
    {
        [Required]
        public string ProjectId { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public string RoleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserRoleName { get; set; }

    }
}
