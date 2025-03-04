using System.Net;
using System.Threading.Tasks;
using Cart.Application.Commands;
using Cart.Application.Dtos;
using Cart.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cart.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CartController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}", Name ="GetCartById")]
        [ProducesResponseType(typeof(CartShoppingDTO) , (int) HttpStatusCode.OK)]
        public async Task<IActionResult> GetCart(string id)
        {
            var query = new GetCartByIdQuery(id);
            var result = await _mediator.Send(query);
            return Ok(result);
            }

        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(typeof(CartShoppingDTO), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateCart([FromBody] CartShoppingDTO cartShoppingDTO)
        {
            if(cartShoppingDTO == null)
            {
                return BadRequest();
            }
            var command = new CreateCartCommand(cartShoppingDTO);
            var result = await _mediator.Send(command);
            if(result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> DeleteCart(string id)
        {
            var command = new DeleteCartCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
