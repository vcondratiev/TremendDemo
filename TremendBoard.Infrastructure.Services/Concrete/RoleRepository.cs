using TremendBoard.Infrastructure.Data.Context;
using TremendBoard.Infrastructure.Data.Models.Identity;
using TremendBoard.Infrastructure.Services.Interfaces;

namespace TremendBoard.Infrastructure.Services.Concrete
{
    public class RoleRepository : GenericRepository<ApplicationRole>, IRoleRepository
    {
        public RoleRepository(TremendBoardDbContext context) : base(context)
        {
        }
    }
}
