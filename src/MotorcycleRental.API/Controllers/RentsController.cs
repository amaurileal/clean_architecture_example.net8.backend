using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotorcycleRental.Application.Rents.Commands.CreateRent;
using MotorcycleRental.Application.Rents.Queries.CheckRentValue;
using MotorcycleRental.Application.Rents.Queries.GetRentById;
using MotorcycleRental.Domain.Constants;

namespace MotorcycleRental.API.Controllers
{
    [Authorize(Roles = UserRoles.Biker)]    
    [Route("api/rents")]
    [ApiController]
    public class RentsController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRentCommand commad) { 
            var id = await mediator.Send(commad);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        
        [HttpGet("checkRentValue/{previewDate}")]
        public async Task<ActionResult> CheckValue([FromRoute] DateOnly previewDate)
        {
            var result = await mediator.Send(new CheckRentValueQuery(previewDate));

            return Ok(result);
        }

        [HttpGet("{id}")]        
        public async Task<ActionResult> GetById(int id)
        {
            var rent = await mediator.Send(new GetRentByIdQuery(id));

                return Ok(rent);
        }
    }
}
