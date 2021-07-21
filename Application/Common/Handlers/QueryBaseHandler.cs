using Application.Common.Interfaces;
using AutoMapper;

namespace Application.Common.Handlers
{
    public abstract class QueryBaseHandler
    {
        protected readonly IApplicationContext _context;
        protected readonly IMapper _mapper;

        public QueryBaseHandler(IApplicationContext  context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }
    }
}
