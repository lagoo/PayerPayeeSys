using Application.Common.Interfaces;

namespace Application.Common.Services
{
    public class DefaultApplicationVersionService : IApplicationVersionService
    {
        public int AppVersion => 1;
    }
}
