using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common
{
    public abstract class TypedEntity : ITypedEntity
    {
        public abstract int EntityId { get; }

        public abstract string EntityUniqueIdentifier { get; }

        public abstract string Type { get; }

        public override string ToString()
        {
            return $"Id: {EntityId}, Tipo: {Type}, Identificador: {EntityUniqueIdentifier}";
        }
    }
}
