using System.Collections.Generic;

namespace Application.Users.Queries.GetUsersList
{
    public class UserListVm
    {
        public UserListVm()
        {
            Users = new List<UserLookupDto>();
        }

        public IList<UserLookupDto> Users { get; set; }
    }
}