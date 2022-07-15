using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace TremendBoard.Infrastructure.Data.Models.Identity
{
    public class ApplicationRole : IdentityRole
    {
        public string Description { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
        public virtual ICollection<ApplicationRoleClaim> RoleClaims { get; set; }
    }
}
