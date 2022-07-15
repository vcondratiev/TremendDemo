using TremendBoard.Infrastructure.Data.Context;
using TremendBoard.Infrastructure.Data.Models.Identity;
using TremendBoard.Infrastructure.Services.Interfaces;

namespace TremendBoard.Infrastructure.Services.Concrete
{
    public class UserRoleRepository : GenericRepository<ApplicationUserRole>, IUserRoleRepository
    {
        public UserRoleRepository(TremendBoardDbContext context) : base(context)
        {
        }
    }
}
