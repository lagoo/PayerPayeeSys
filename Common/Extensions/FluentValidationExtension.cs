using FluentValidation;
using FluentValidation.Results;
using NetDevPackBr.Documentos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Extensions
{
    public static class FluentValidationExtension
    {
        public static Dictionary<string, string[]> ToDictionaryOfErrors(this ValidationResult validationResult)
        {
            return validationResult.Errors.GroupBy(e => e.PropertyName)
                                          .ToDictionary(e => e.Key, e => e.Select(a => a.ErrorMessage).ToArray());
        }

        public static IRuleBuilderOptions<T, string> ValidDocument<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must(input =>
            {
                if (string.IsNullOrEmpty(input))
                    return false;

                try
                {
                    if (input.Length == 11)
                        return new Cpf(input).EstaValido();

                    if (input.Length == 14)
                        return new Cnpj(input).EstaValido();

                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
            });
        }
    }
}