using Domain.Common;
using System;

namespace Domain.UnitTests.Core.Models
{
    public class AuditableEntityTesting : AuditableEntity
    {
        public override int EntityId => throw new NotImplementedException();
        public override string EntityUniqueIdentifier => throw new NotImplementedException();
        public override string Type => throw new NotImplementedException();


        public override bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
