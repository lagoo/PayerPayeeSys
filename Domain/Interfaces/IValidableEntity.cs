using System.Collections.Generic;

namespace Domain.Interfaces
{
    public interface IValidableEntity
    {
        IReadOnlyDictionary<string, string[]> ValidationErros { get; }

        bool IsValid();
    }
}
