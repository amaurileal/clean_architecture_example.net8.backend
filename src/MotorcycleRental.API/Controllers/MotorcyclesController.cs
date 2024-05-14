using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotorcycleRental.Application.Motorcycles.Commands.CreateMotorcycle;
using MotorcycleRental.Application.Motorcycles.Commands.DeleteMotorcycle;
using MotorcycleRental.Application.Motorcycles.Commands.UpdateMotorcycle;
using MotorcycleRental.Application.Motorcycles.Queries.GetAllMotorcycleActives;
using MotorcycleRental.Application.Motorcycles.Queries.GetAllMotorcycles;
using MotorcycleRental.Application.Motorcycles.Queries.GetMotorcycleById;
using MotorcycleRental.Domain.Constants;

namespace MotorcycleRental.API.Controllers
{
    [Authorize]
    [Route("api/motorcycles")]
    [ApiController]
    public class MotorcyclesController(IMediator mediator) : ControllerBase
    {

        // GET: api/<MotorcycleController>
        [HttpGet]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> GetAll([FromQuery] GetAllMotorcyclesQuery query)
        {

            var motorcycles = await mediator.Send(query);

            return Ok(motorcycles);
        }

        // GET api/<MotorcycleController>/5
        [HttpGet("{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult> GetById(int id)
        {
            var motorcycle = await mediator.Send(new GetMotorcycleByIdQuery()
            {
                Id = id
            }
            );

            return Ok(motorcycle);
        }

        // POST api/<MotorcycleController>
        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Post([FromBody] CreateMotorcycleCommand command)
        {
            int id = await mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        // PUT api/<MotorcycleController>/5
        [HttpPatch("{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateMotorcycleCommand command)
        {
            command.Id = id;
            await mediator.Send(command);

            return NoContent();
        }

        // DELETE api/<MotorcycleController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Delete(int id)
        {
            await mediator.Send(new DeleteMotorcycleCommand(id));

            return NoContent();
        }

        [HttpGet("getActives")]
        [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.Biker}")]
        public async Task<ActionResult> GetActives([FromQuery] GetAllActivesMotorcyclesQuery query)
        {
            var motorcycle = await mediator.Send(query);            

            return Ok(motorcycle);
        }
    }
}
