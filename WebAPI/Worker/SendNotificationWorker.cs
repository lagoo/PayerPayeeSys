using Application.TransactionNotifications.Commands.SendTransactionNotifications;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace WebAPI.Worker
{
    public class SendNotificationWorker : BackgroundService
    {        
        private readonly ILogger<SendNotificationWorker> _sendNotificationlogger;
        private readonly IServiceProvider _serviceProvider;


        public SendNotificationWorker(ILogger<SendNotificationWorker> logger, IServiceProvider serviceProvider)
        {            
            _sendNotificationlogger = logger;
            this._serviceProvider = serviceProvider;
        } 

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _sendNotificationlogger.LogTrace("Starting to send transaction notification worker");
            stoppingToken.Register(() => _sendNotificationlogger.LogTrace("Demo Service is stopping."));

            while (!stoppingToken.IsCancellationRequested)
            {
                _sendNotificationlogger.LogTrace("sending notification in background");
                using (IServiceScope scope = _serviceProvider.CreateScope())
                {
                    IMediator mediator = scope.ServiceProvider.GetService<IMediator>();

                    await mediator.Send(new SendTransactionNotificationsCommand(),stoppingToken);

                    await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
                };                
            }

            _sendNotificationlogger.LogTrace("notifications sended");
        }
    }
}
