using System.Threading.Tasks;

namespace TremendBoard.Infrastructure.Services.Interfaces
{
    public interface IMailingService
    {
        Task<bool> SendAsync();
        Task<bool> NotifyTaskStopAsync();
    }
}
