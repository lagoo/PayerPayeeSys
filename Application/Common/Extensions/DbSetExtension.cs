using Application.Common.Exceptions;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Application.Common.Extensions
{
    public static class DbSetExtension
    {
        public static EntityEntry<TEntity> AddWithValidate<TEntity>(this DbSet<TEntity> dbset, TEntity entity) where TEntity : class, IValidableEntity
        {
            if (!entity.IsValid())
                throw new ValidationException(entity.ValidationErros);

            return dbset.Add(entity);
        }
    }
}
