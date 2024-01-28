using DemoApi.Domain.Extensions.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DemoApi.App.Controllers.Base;

[Route("api/[controller]")]
[ApiController]
public class ApiControllerBase : ControllerBase
{
    private ISender _sender;
    protected ISender Mediator => _sender ??= HttpContext.RequestServices.GetService<ISender>();

    protected async Task<ActionResult> HandelCommandAsync<T>(IRequest<CommandResult<T>> command)
    {
        var result = await Mediator.Send(command);
        return result.Type switch
        {
            CommandResultTypeEnum.BadRequest => new BadRequestResult(),
            CommandResultTypeEnum.NotFound => new NotFoundResult(),
            CommandResultTypeEnum.Conflict => new ConflictResult(),
            CommandResultTypeEnum.Unauthorized => new UnauthorizedResult(),
            CommandResultTypeEnum.Created => new CreatedResult("", result.Result),
            _ => new OkObjectResult(result.Result)
        };
    }

    protected async Task<ActionResult> HandelQueryAsync<T>(IRequest<QueryResult<T>> query)
    {
        var result = await Mediator.Send(query);
        return result.Type switch
        {
            QueryResultTypeEnum.InvalidInput => new BadRequestResult(),
            QueryResultTypeEnum.NotFound => new NotFoundResult(),
            _ => new OkObjectResult(result.Result)
        };
    }
}
