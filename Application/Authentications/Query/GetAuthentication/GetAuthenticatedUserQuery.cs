using Application.Common.Handlers;
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Authentications.Query.GetAuthentication
{
    public class GetAuthenticationQuery : IRequest<bool>
    {
        public string Password { get; set; }

        public class Handler : QueryBaseHandler, IRequestHandler<GetAuthenticationQuery, bool>
        {
            public Handler(IApplicationContext context, IMapper mapper) : base(context, mapper) { }

            public Task<bool> Handle(GetAuthenticationQuery request, CancellationToken cancellationToken)
            {
                return Task.FromResult(request.Password == "123");
            }
        }
    }
}
