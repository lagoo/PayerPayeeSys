using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class DomainEventService : IDomainEventService
    {
        private readonly ILogger<DomainEventService> _logger;
        private readonly IPublisher _mediator;
        private readonly List<Tuple<DomainEvent, EventTime>> _events;

        public DomainEventService(ILogger<DomainEventService> logger, IPublisher mediator)
        {
            _logger = logger;
            _mediator = mediator;
            _events ??= new List<Tuple<DomainEvent, EventTime>>();
        }

        public void AddEvent(DomainEvent domainEvent, EventTime eventTime)
        {
            if (domainEvent is null)
                throw new ArgumentNullException(nameof(domainEvent));

            _logger.LogInformation("Add new domain event. Event - {event}", domainEvent.GetType().Name);

            _events.Add(new Tuple<DomainEvent, EventTime>(domainEvent, eventTime));
        }

        public async Task Publish(DomainEvent domainEvent)
        {
            _logger.LogInformation("Publishing domain event. Event - {event}", domainEvent.GetType().Name);
            await _mediator.Publish(GetNotificationCorrespondingToDomainEvent(domainEvent));
        }

        public async Task Publish(EventTime eventTime)
        {
            List<DomainEvent> events = _events.Where(e => e.Item2 == eventTime && !e.Item1.IsPublished).Select(e => e.Item1).ToList();

            foreach (var domainEvent in events)
            {
                _logger.LogInformation("Publishing domain event. Event - {event}", domainEvent.GetType().Name);

                domainEvent.IsPublished = true;

                await _mediator.Publish(GetNotificationCorrespondingToDomainEvent(domainEvent));
            }
        }

        private INotification GetNotificationCorrespondingToDomainEvent(DomainEvent domainEvent)
        {
            return (INotification)Activator.CreateInstance(
                typeof(DomainEventNotification<>).MakeGenericType(domainEvent.GetType()), domainEvent);
        }
    }
}
