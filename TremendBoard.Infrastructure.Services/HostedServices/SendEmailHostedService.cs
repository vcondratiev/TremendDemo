using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TremendBoard.Infrastructure.Services.Interfaces;

namespace TremendBoard.Infrastructure.Services.HostedServices
{
    public class SendEmailHostedService : IHostedService
    {
        private const int MS = 1000;
        private const int SEC = 1 * MS;
        private const int MIN = 60 * SEC;
        private const int INTERVAL_MIN = 60 * MIN;

        private readonly IMailingService _mailingService;
        private readonly IProjectRepository _projectRepository;

        private Timer _timer;

        public SendEmailHostedService(
            IMailingService mailingService,
            IProjectRepository projectRepository)
        {
            _mailingService = mailingService;
            _projectRepository = projectRepository;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(ExecuteAsync, null, 0, INTERVAL_MIN);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public async void ExecuteAsync(object state)
        {
            var projects = await _projectRepository.GetAllAsync();
            if (projects.Count() > 0)
            {
                await _mailingService.SendAsync();
            }
        }
    }
}
