using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TremendBoard.Infrastructure.Services.Interfaces;

namespace TremendBoard.Infrastructure.Services.HostedServices
{
    public class SendEmailBackgroundService : BackgroundService
    {
        private const int MS = 1000;
        private const int SEC = 1 * MS;
        private const int MIN = 60 * SEC;
        private const int INTERVAL_MIN = 60 * MIN;

        private readonly IMailingService _mailingService;
        private readonly IProjectRepository _projectRepository;

        public SendEmailBackgroundService(
            IMailingService mailingService,
            IProjectRepository projectRepository)
        {
            _mailingService = mailingService;
            _projectRepository = projectRepository;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Run until cancellation is requested.
            while (!stoppingToken.IsCancellationRequested)
            {
                // We can do here anything that we would like.
                // Start an interval and execute code periodically.
                // Execute the code only once and exit.

                var projects = await _projectRepository.GetAllAsync();
                if (projects.Count() > 0)
                {
                    await _mailingService.SendAsync();
                }

                // Wait X time then repeat the execution
                await Task.Delay(INTERVAL_MIN);
            }
        }
    }
}
