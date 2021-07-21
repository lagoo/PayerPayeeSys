namespace Domain.Interfaces
{
    public interface ITypedEntity
    {
        int EntityId { get; }
        string EntityUniqueIdentifier { get; }
        string Type { get; }

        virtual string ToString()
        {
            return $"Id: {EntityId}, Tipo: {Type}, Identificador: {EntityUniqueIdentifier}";
        }
    }
}
