using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IMessageService
    {
        Task<bool> SendMessage();
    }
}
