using Application.Users.Commands.CreateUser;
using Application.Users.Queries.GetUserDetail;
using Application.Users.Queries.GetUsersList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebAPI.Common.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IJwtHandler _jwtHandler;

        public UserController(IJwtHandler jwtHandler)
        {
            this._jwtHandler = jwtHandler;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize]
        public async Task<ActionResult<UserListVm>> GetAll()
        {
            var vm = await Mediator.Send(new GetUsersListQuery());

            return Ok(vm);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize]
        public async Task<ActionResult<UserDetailVm>> Get(int id)
        {
            var vm = await Mediator.Send(new GetUserDetailQuery(id));

            return Ok(vm);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesDefaultResponseType]        
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }
    }
}
