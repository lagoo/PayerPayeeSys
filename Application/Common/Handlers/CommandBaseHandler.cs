using Application.Common.Interfaces;

namespace Application.Common.Handlers
{
    public abstract class CommandBaseHandler
    {
        protected readonly IApplicationContext _context;
        protected readonly ICurrentUserService _currentUserService;

        public CommandBaseHandler(IApplicationContext context, ICurrentUserService currentUserService)
        {
            this._context = context;
            this._currentUserService = currentUserService;
        }
    }
}
