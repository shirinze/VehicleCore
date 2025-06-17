using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using VehicleCore.API.DTOs;
using VehicleCore.Application.Commands.Car.Create;
using VehicleCore.Application.Commands.Car.Delete;
using VehicleCore.Application.Commands.Car.Update;

namespace VehicleCore.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CarsController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrUpdateCarDto input,CancellationToken cancellationToken)
    {
        var command = new CreateCarCommand(input.Title, input.GearBox);
        await mediator.Send(command);
        return Ok("create success");
    }
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromBody] CreateOrUpdateCarDto input, [FromRoute] int id,CancellationToken cancellationToken)
    {
        var command = new UpdateCarCommand(id, input.Title, input.GearBox);
        await mediator.Send(command);
        return Ok("update was success");
    }
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
    {
        var command = new DeleteCarCommand(id);
        await mediator.Send(command);
        return Ok();
    }
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id,CancellationToken cancellationToken)
    {
        var comm
    }
}
