using System;
using System.ComponentModel.DataAnnotations;
using TremendBoard.Infrastructure.Data.Models.Identity;

namespace TremendBoard.Infrastructure.Data.Models
{
    public class ProjectTask : BaseEntity
    {
        [Required]
        public string Content { get; set; }
        public string Status { get; set; }

        public TimeSpan Duration { get; set; }

        public virtual Project Project { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
