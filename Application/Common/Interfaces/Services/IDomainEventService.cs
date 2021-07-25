using Domain.Common;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(EventTime eventTime);

        void AddEvent(DomainEvent domainEvent, EventTime eventTime = EventTime.PreSave);
    }

    public enum EventTime
    {
        PreSave = 1,
        PosSave = 2
    }
}
