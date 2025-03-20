using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Order.Application.Services;

namespace Order.Infrastructure.ExternalServices
{
    public class OrderCleanupBackgroudService : BackgroundService
    {
        private readonly ILogger<OrderCleanupBackgroudService> _logger;
        private readonly CleanupExpiredOrdersUseCase _cleanupUseCase;

        public OrderCleanupBackgroudService(ILogger<OrderCleanupBackgroudService> logger, CleanupExpiredOrdersUseCase cleanupUseCase)
        {
            _logger = logger;
            _cleanupUseCase = cleanupUseCase;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation("Cleaning up expired orders");
                    await _cleanupUseCase.ExecuteAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error while cleaning up expired orders");
                }
                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
            }
        }
    }
}

