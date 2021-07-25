using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Common.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException()
            : base("Ocorreu uma ou mais falhas de validação.")
        {
            Failures = new Dictionary<string, string[]>();
        }

        public ValidationException(string key, string[] erros) : this()
        {
            Failures.Add(key, erros);
        }

        public ValidationException(IReadOnlyDictionary<string, string[]> failures) : this()
        {
            foreach (var fail in failures)
                Failures.Add(fail.Key, fail.Value);
        }

        public ValidationException(List<ValidationFailure> failures)
            : this()
        {
            var propertyNames = failures
                .Select(e => e.PropertyName)
                .Distinct();

            foreach (var propertyName in propertyNames)
            {
                var propertyFailures = failures
                    .Where(e => e.PropertyName == propertyName)
                    .Select(e => e.ErrorMessage)
                    .ToArray();

                Failures.Add(propertyName, propertyFailures);
            }
        }

        public IDictionary<string, string[]> Failures { get; }
    }
}
