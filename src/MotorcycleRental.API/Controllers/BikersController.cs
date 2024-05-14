using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MotorcycleRental.Application.Bikers.Commands.SendCHNImage;
using MotorcycleRental.Application.Bikers.Queries.GetRegister;
using MotorcycleRental.Domain.Constants;
using System.Runtime.CompilerServices;

namespace MotorcycleRental.API.Controllers
{
    [Authorize(Roles = UserRoles.Biker)]
    [Route("api/bikers")]
    [ApiController]    
    public class BikersController(IMediator mediator) : ControllerBase
    {
        [HttpPost("uploadCNH")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UploadImage([FromForm] SendCNHImageCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("bikerInfo")]
        public async Task<IActionResult> GetRegister()
        {
            var biker = await mediator.Send(new GetRegisterQuery());

            return Ok(biker);
        }
    }
}
