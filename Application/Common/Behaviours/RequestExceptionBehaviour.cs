using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Common.Interface;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Behaviours
{
    public class RequestExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ICurrentUserService currentUserService;
        private readonly IApplicationContext context;
        private readonly IDateTime dateTime;

        public RequestExceptionBehaviour(ICurrentUserService currentUserService, IApplicationContext context, IDateTime dateTime)
        {
            this.currentUserService = currentUserService;
            this.context = context;
            this.dateTime = dateTime;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                return await next();
            }
            catch (FluentValidation.ValidationException ex)
            {
                throw ex;
            }                        
            catch (WarningException ex)
            {                
                throw ex;
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }
    }
}
