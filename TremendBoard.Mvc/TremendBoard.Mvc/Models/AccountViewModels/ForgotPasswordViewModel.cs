using System.ComponentModel.DataAnnotations;

namespace TremendBoard.Mvc.Models.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
