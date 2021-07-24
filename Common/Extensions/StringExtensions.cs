using NetDevPackBr.Documentos;
using System;
using System.Collections.Generic;
using System.Text;

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
