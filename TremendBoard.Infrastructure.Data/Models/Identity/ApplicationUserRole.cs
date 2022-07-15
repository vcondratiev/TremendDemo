using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TremendBoard.Infrastructure.Data.Models.Identity
{
    public partial class ApplicationUserRole : IdentityUserRole<string>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public virtual Project Project { get; set; }
    }
}
