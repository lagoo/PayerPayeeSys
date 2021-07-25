using FluentValidation;
using FluentValidation.Internal;
using System.Collections.Generic;
using System.Linq;

namespace Domain.UnitTests.Core.Helpers
{
    public static class FluentValidationHelper
    {
        public static Dictionary<string, string[]> GetFluentValidationRules<TValidator>(TValidator validator) where TValidator : IValidator
        {
            Dictionary<string, string[]> result = new Dictionary<string, string[]>();
            IEnumerable<IValidationRule> rules = validator as IEnumerable<IValidationRule>;

            foreach (PropertyRule item in rules)
            {
                string[] messages = item.Validators.Select(e => e.Options.GetErrorMessageTemplate(null)).ToArray();

                string message = string.Join(", ", messages);

                result.Add(item.PropertyName, messages);
            }

            return result;
        }
    }
}
