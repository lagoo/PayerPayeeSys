using Application.Common.Exceptions;
using Application.Common.Handlers;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Queries.GetUserDetail
{
    public class GetUserDetailQuery : IRequest<UserDetailVm>
    {
        public GetUserDetailQuery(int userId)
        {
            UserId = userId;
        }

        public int UserId { get; }

        public class Handler : QueryBaseHandler, IRequestHandler<GetUserDetailQuery, UserDetailVm>
        {
            public Handler(IApplicationContext context, IMapper mapper) : base(context, mapper)
            {
            }

            public async Task<UserDetailVm> Handle(GetUserDetailQuery request, CancellationToken cancellationToken)
            {
                var entity = await _context.Users
                                           .FindAsync(request.UserId);

                if (entity == null)                
                    throw new NotFoundException(nameof(User), request.UserId);                

                return _mapper.Map<UserDetailVm>(entity);
            }
        }
    }
}
