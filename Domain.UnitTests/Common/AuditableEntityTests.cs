using Common.Constants;
using Common.Interface;
using Domain.UnitTests.Core.Models;
using Moq;
using Xunit;

namespace Domain.UnitTests.Common
{
    public class AuditableEntityTests
    {
        private readonly IDateTime dateTime;

        public AuditableEntityTests()
        {
            Mock<IDateTime> mock = new Mock<IDateTime>();
            mock.Setup(e => e.Now).Returns(SystemConst.GetDateDefault());

            dateTime = mock.Object;
        }

        [Fact(DisplayName = "Deve preencher propriedades Created da entidade com herança de AuditableEntity")]
        [Trait("Domain", "AuditableEntity")]
        public void AuditableEntity_MarkAsCreated_ShouldFillCreatedProperties()
        {
            // Arrange
            var entity = new AuditableEntityTesting();

            // Act
            entity.MarkAsCreated(SystemConst.SYSTEM_USER_ID, SystemConst.SYSTEM_USER_NAME, dateTime);

            // Assert
            Assert.Equal(SystemConst.SYSTEM_USER_ID, entity.CreatedById);
            Assert.Equal(SystemConst.SYSTEM_USER_NAME, entity.CreatedBy);
            Assert.Equal(SystemConst.GetDateDefault(), entity.CreatedOn.Date);
        }

        [Fact(DisplayName = "Deve preencher propriedades Modified da entidade com herança de AuditableEntity")]
        [Trait("Domain", "AuditableEntity")]
        public void AuditableEntity_MarkAsChanged_ShouldFillModifiedProperties()
        {
            // Arrange
            var entity = new AuditableEntityTesting();

            // Act
            entity.MarkAsChanged(SystemConst.SYSTEM_USER_ID, SystemConst.SYSTEM_USER_NAME, dateTime);

            // Assert
            Assert.Equal(SystemConst.SYSTEM_USER_ID, entity.ModifiedById);
            Assert.Equal(SystemConst.SYSTEM_USER_NAME, entity.ModifiedBy);
            Assert.Equal(SystemConst.GetDateDefault(), entity.ModifiedOn.Value.Date);
        }

        [Fact(DisplayName = "Deve preencher propriedades Deleted da entidade com herança de AuditableEntity")]
        [Trait("Domain", "AuditableEntity")]
        public void AuditableEntity_MarkAsDeleted_ShouldFillDeletedProperties()
        {
            // Arrange
            var entity = new AuditableEntityTesting();

            // Act
            entity.MarkAsDeleted(SystemConst.SYSTEM_USER_ID, SystemConst.SYSTEM_USER_NAME, dateTime);

            // Assert
            Assert.True(entity.Deleted);
            Assert.Equal(SystemConst.SYSTEM_USER_ID, entity.DeletedById);
            Assert.Equal(SystemConst.SYSTEM_USER_NAME, entity.DeletedBy);
            Assert.Equal(SystemConst.GetDateDefault(), entity.DeletedOn.Value.Date);
        }

        [Fact(DisplayName = "Deve alterar propriedade Deleted e preencher as propriedades Modified da entidade com herança de AuditableEntity")]
        [Trait("Domain", "AuditableEntity")]
        public void AuditableEntity_MarkAsUnDeleted_ShouldFillDeletedProperties()
        {
            // Arrange
            var entity = new AuditableEntityTesting();

            // Act
            entity.MarkAsUnDeleted(SystemConst.SYSTEM_USER_ID, SystemConst.SYSTEM_USER_NAME, dateTime);

            // Assert
            Assert.False(entity.Deleted);
            Assert.Equal(SystemConst.SYSTEM_USER_ID, entity.ModifiedById);
            Assert.Equal(SystemConst.SYSTEM_USER_NAME, entity.ModifiedBy);
            Assert.Equal(SystemConst.GetDateDefault(), entity.ModifiedOn.Value);
        }
    }
}
