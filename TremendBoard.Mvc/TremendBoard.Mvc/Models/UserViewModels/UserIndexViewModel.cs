using System.Collections.Generic;

namespace TremendBoard.Mvc.Models.UserViewModels
{
    public class UserIndexViewModel
    {
        public IEnumerable<UserDetailViewModel> Users { get; set; }
    }
}
