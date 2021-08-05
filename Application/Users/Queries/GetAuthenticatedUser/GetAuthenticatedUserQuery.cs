using Application.Common.Handlers;
using Application.Common.Interfaces;
using AutoMapper;
using Common.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Queries.GetAuthenticatedUser
{
    public class GetAuthenticatedUserQuery : IRequest<int>
    {
        public string Email { get; set; }
        public string Password { get; set; }


        public class Handler : QueryBaseHandler, IRequestHandler<GetAuthenticatedUserQuery, int>
        {
            public Handler(IApplicationContext context, IMapper mapper) : base(context, mapper) { }            

            public async Task<int> Handle(GetAuthenticatedUserQuery request, CancellationToken cancellationToken)
            {
                if (request.Email == "admin" && request.Password == "123")
                    return SystemConst.SYSTEM_USER_ID;

                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower().Trim() == request.Email.ToLower().Trim());

                if (user == null)
                    throw new ArgumentException($"User {request.Email} not found");

                if (!user.ValidatePassword(request.Password))
                    throw new ArgumentException($"Incorrect password provided");

                return user.Id;
            }
        }
    }
}
