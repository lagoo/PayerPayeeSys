using Common.Constants;
using Domain.UnitTests.Core.Models;
using Xunit;

namespace Domain.UnitTests.Common
{
    public class TypedEntityTests
    {
        [Fact(DisplayName = "Deve validar ToString de entidades com herança de TypedEntity")]
        [Trait("Domain", "TypedEntity")]
        public void TypedEntity_ToString_ShouldReturnToStringCorrectly()
        {
            // Arrange
            var entity = new TypedEntityTesting(SystemConst.SYSTEM_USER_ID, SystemConst.SYSTEM_USER_NAME);

            // Act
            var result = entity.ToString();

            // Assert
            Assert.Equal($"Id: {SystemConst.SYSTEM_USER_ID}, Tipo: Classe de Teste, Identificador: {SystemConst.SYSTEM_USER_NAME}", result, true);
        }

    }
}
