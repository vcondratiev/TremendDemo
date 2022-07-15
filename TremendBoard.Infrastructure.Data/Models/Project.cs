using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TremendBoard.Infrastructure.Data.Models.Identity;

namespace TremendBoard.Infrastructure.Data.Models
{
    public class Project: BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime CompletedDate { get; set; }

        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
        public virtual ICollection<ProjectTask> Tasks { get; set; }
    }
}
