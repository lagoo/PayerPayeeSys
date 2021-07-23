using Application.Common.Handlers;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Queries.GetUsersList
{
    public class GetUsersListQuery : IRequest<UserListVm>
    {

        public class Handler : QueryBaseHandler, IRequestHandler<GetUsersListQuery, UserListVm>
        {
            public Handler(IApplicationContext context, IMapper mapper) : base(context, mapper)
            {
            }

            public async Task<UserListVm> Handle(GetUsersListQuery request, CancellationToken cancellationToken)
            {
                var users = await _context.Users
                                          .ProjectTo<UserLookupDto>(_mapper.ConfigurationProvider)
                                          .ToListAsync(cancellationToken);

                var vm = new UserListVm
                {
                    Users = users
                };

                return vm;
            }
        }
    }
}
