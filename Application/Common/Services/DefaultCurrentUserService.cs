using Application.Common.Interfaces;
using Common.Constants;

namespace Application.Common.Services
{
    public class DefaultCurrentUserService : ICurrentUserService
    {
        public int UserId => SystemConst.SYSTEM_USER_ID;

        public string UserName => SystemConst.SYSTEM_USER_NAME;
    }
}
