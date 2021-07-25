using Common.Extensions;
using Domain.Entities;
using NetDevPack.Specification;
using System;
using System.Linq.Expressions;

namespace Application.Users.Specifications
{
    public class ShopkeeperSpec : Specification<User>
    {
        public override Expression<Func<User, bool>> ToExpression()
        {
            return user => user.Document.IsCnpj();
        }
    }
}
