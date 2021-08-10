using WebAPI.Common.Models;

namespace WebAPI.Common.Interfaces
{
    public interface IJwtHandler
    {
        JsonWebToken Create();
    }
}
