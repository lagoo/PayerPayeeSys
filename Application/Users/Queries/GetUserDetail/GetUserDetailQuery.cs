using Application.Common.Exceptions;
using Application.Common.Handlers;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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
                var entity = await _context.Users.Where(e => e.Id == request.UserId)
                                                 .Include(e => e.Wallet)
                                                 .ThenInclude(e => e.Transactions)
                                                 .FirstOrDefaultAsync();

                if (entity == null)
                    throw new NotFoundException(nameof(User), request.UserId);

                return _mapper.Map<UserDetailVm>(entity);
            }
        }
    }
}
