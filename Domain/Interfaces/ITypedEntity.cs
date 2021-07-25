namespace Domain.Interfaces
{
    public interface ITypedEntity
    {
        int EntityId { get; }
        string EntityUniqueIdentifier { get; }
        string Type { get; }
    }
}
