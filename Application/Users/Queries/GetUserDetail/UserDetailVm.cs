using Application.Common.Mappings;
using Domain.Entities;

namespace Application.Users.Queries.GetUserDetail
{
    public class UserDetailVm : IMapFrom<User>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Document { get; set; }
        public string Email { get; set; }

        public WalletLookupDto Wallet { get; set; }        
    }
}