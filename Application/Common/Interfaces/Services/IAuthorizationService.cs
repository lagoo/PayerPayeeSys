using System.Threading.Tasks;

namespace Application.Common.Interfaces.Services
{
    public interface IAuthorizationService
    {
        Task<bool> Authorize();
    }
}
