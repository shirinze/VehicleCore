using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using VehicleCore.API.DTOs;
using VehicleCore.API.Features;
using VehicleCore.Application.Commands.Car.Create;
using VehicleCore.Application.Commands.Car.Delete;
using VehicleCore.Application.Commands.Car.ToggleActivation;
using VehicleCore.Application.Commands.Car.Update;
using VehicleCore.Application.Queries.Car.GetById;
using VehicleCore.Application.Queries.Car.GetList;
using VehicleCore.DomainService.BaseSpecifications;

namespace VehicleCore.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CarsController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrUpdateCarDto input,CancellationToken cancellationToken)
    {
        var command = new CreateCarCommand(input.Title, input.GearBox);
        await mediator.Send(command,cancellationToken);
        return Ok(BaseResult.Success());
    }


    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromBody] CreateOrUpdateCarDto input, [FromRoute] int id,CancellationToken cancellationToken)
    {
        var command = new UpdateCarCommand(id, input.Title, input.GearBox);
        await mediator.Send(command,cancellationToken);
        return Ok(BaseResult.Success());
    }


    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
    {
        var command = new DeleteCarCommand(id);
        await mediator.Send(command, cancellationToken);
        return Ok(BaseResult.Success());
    }


    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id,CancellationToken cancellationToken)
    {
        var query = new GetCarByIdQuery(id);
        var entity=await mediator.Send(query,cancellationToken);
        return Ok(BaseResult.Success(entity));
    }


    [HttpGet]
    public async Task<IActionResult> GetList
        (
        [FromQuery] string q,
        [FromQuery] OrderType orderType,
        [FromQuery] int pageSize,
        [FromQuery] int pageNumber,
        CancellationToken cancellationToken
        )
    {
        var query = new GetCarListQuery(q, orderType, pageSize, pageNumber);
        var entity = await mediator.Send(query, cancellationToken);
        return Ok(BaseResult.Success(entity));
    }


    [HttpPut("{id:int}/ToggleActivation")]
    public async Task<IActionResult> Activate([FromRoute] int id,CancellationToken cancellationToken)
    {
        var command = new ToggleActivationCarCommand(id);
        await mediator.Send(command, cancellationToken);
        return Ok(BaseResult.Success());
    }
}
