using Domain.Common;

namespace Domain.UnitTests.Core.Models
{
    public class TypedEntityTesting : TypedEntity
    {
        public TypedEntityTesting(int id, string identifier)
        {
            Id = id;
            Identifier = identifier;
        }

        public int Id { get; }
        public string Identifier { get; }


        public override int EntityId => Id;
        public override string EntityUniqueIdentifier => Identifier;
        public override string Type => "Classe de Teste";
    }
}
