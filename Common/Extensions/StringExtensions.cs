using NetDevPackBr.Documentos;
using System;

namespace Common.Extensions
{
    public static class StringExtensions
    {
        public static bool IsCnpj(this string document)
        {
            try
            {
                return new Cnpj(document).EstaValido();
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
