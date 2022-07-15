using System.Threading.Tasks;
using TremendBoard.Infrastructure.Services.Interfaces;

namespace TremendBoard.Infrastructure.Services.Services
{
    public class MailingService : IMailingService
    {
        public Task<bool> NotifyTaskStopAsync()
        {
            return Task.FromResult(true);
        }

        public Task<bool> SendAsync()
        {
            return Task.FromResult(true);
        }
    }
}
