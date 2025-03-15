using System.Net;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using User.Application.Commands;
using User.Application.Queries;
using User.Application.Responses;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace User.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UsersController(IMediator mediator) => _mediator = mediator;


        [HttpGet("GetAllUsers")]
        [ProducesResponseType(typeof(IEnumerable<UserResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllUsers()
        {
            var response = await _mediator.Send(new GetAllUsersQuery());
            return Ok(response);
        }

        [HttpGet("GetAllActiveUsers")]
        [ProducesResponseType(typeof(IEnumerable<UserResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllActiveUsers()
        {
            var response = await _mediator.Send(new GetAllActiveUsersQuery());
            return Ok(response);
        }

        [HttpGet("GetAllInactiveUsers")]
        [ProducesResponseType(typeof(IEnumerable<UserResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllInactiveUsers()
        {
            var response = await _mediator.Send(new GetAllInactiveUsersQuery());
            return Ok(response);
        }


        [HttpGet("GetUserById/{id}", Name = "GetUserById")]
        [ProducesResponseType(typeof(UserResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetUserById(int id)
        {
            var response = await _mediator.Send(new GetUserByIdQuery(id));
            if (response == null)
                return NotFound(new { message = "User not found" });
            return Ok(response);
        }

        [HttpGet("GetUserByEmail/{email}", Name = "GetUserByEmail")]
        [ProducesResponseType(typeof(UserResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var response = await _mediator.Send(new GetUserByEmailQuery(email));
            if (response == null)
                return NotFound(new { message = "User not found" });
            return Ok(response);
        }


        [HttpPost("CreateUser")]
        [ProducesResponseType(typeof(UserResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)] // 400 Bad Request nếu lỗi
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand userCommand)
        {
            if (userCommand == null || !ModelState.IsValid)
                return BadRequest();
            var response = await _mediator.Send(userCommand);
            return Ok(response);
        }

        [HttpPut("UpdateUser")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommand userCommand)
        {
            if (userCommand == null || !ModelState.IsValid)
                return BadRequest(ModelState);
            var response = await _mediator.Send(userCommand);
            return Ok(response);
        }

        [HttpDelete("{id}", Name = "DeleteUser")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var query = new DeleteUserCommand(id);
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpPut("RestoreUser/{id}", Name = "RestoreUser")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> RestoreUser(int id)
        {
            var query = new RestoreUserCommand(id);
            var response = await _mediator.Send(query);
            return Ok(response);
        }
    }
}
