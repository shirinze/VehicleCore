using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VehicleCore.API.DTOs;
using VehicleCore.API.Features;
using VehicleCore.Application.Commands.Car.ToggleActivation;
using VehicleCore.Application.Commands.Motorcycle.Create;
using VehicleCore.Application.Commands.Motorcycle.Delete;
using VehicleCore.Application.Commands.Motorcycle.Update;
using VehicleCore.Application.Queries.Motorcycle.GetById;
using VehicleCore.Application.Queries.Motorcycle.GetList;
using VehicleCore.DomainService.BaseSpecifications;

namespace VehicleCore.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MotorcyclesController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrUpdateMotorcycleDto input,CancellationToken cancellationToken)
    {
        var command = new CreateMotorcycleCommand(input.Title, input.Fuel);
        await mediator.Send(command, cancellationToken);
        return Ok(BaseResult.Success());
    }


    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromBody] CreateOrUpdateMotorcycleDto input, [FromRoute] int id,CancellationToken cancellationToken)
    {
        var command = new UpdateMotorcycleCommand(id, input.Title, input.Fuel);
        await mediator.Send(command, cancellationToken);
        return Ok(BaseResult.Success());
    }


    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromBody] int id,CancellationToken cancellationToken)
    {
        var command = new DeleteMotorcycleCommand(id);
        await mediator.Send(command, cancellationToken);
        return Ok(BaseResult.Success());
    }


    [HttpPut("{id:int}/ToggleActivation")]
    public async Task<IActionResult> Activate([FromBody] int id, CancellationToken cancellationToken)
    {
        var command = new ToggleActivationCarCommand(id);
        await mediator.Send(command, cancellationToken);
        return Ok(BaseResult.Success());
    }


    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id,CancellationToken cancellationToken)
    {
        var query = new GetMotorcycleByIdQuery(id);
        var entity = await mediator.Send(query, cancellationToken);
        return Ok(BaseResult.Success(entity));
    }


    [HttpGet]
    public async Task<IActionResult> GetList
        (
        [FromQuery] string? q,
        [FromQuery] OrderType? orderType,
        [FromQuery] int? pageSize,
        [FromQuery] int? pageNumber,
        CancellationToken cancellationToken
        )
    {
        var query = new GetMotorcycleListQuery(q, orderType, pageSize, pageNumber);
        var entity = await mediator.Send(query, cancellationToken);
        return Ok(BaseResult.Success(entity));
    }
}
