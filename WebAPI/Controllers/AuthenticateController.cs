using Application.Authentications.Query.GetAuthentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebAPI.Common.Interfaces;

namespace WebAPI.Controllers
{
    public class AuthenticateController : BaseController
    {
        private readonly IJwtHandler _jwtHandler;

        public AuthenticateController(IJwtHandler jwtHandler)
        {
            this._jwtHandler = jwtHandler;
        }

        [HttpPost("auth")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] GetAuthenticationQuery request)
        {
            var id = await Mediator.Send(request);

            var response = _jwtHandler?.Create();

            return Ok(response);
        }
    }
}
