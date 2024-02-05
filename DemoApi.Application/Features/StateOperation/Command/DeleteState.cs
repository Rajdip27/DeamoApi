using DemoApi.Application.Repositories;
using DemoApi.Application.ViewModel;
using DemoApi.Domain.Extensions.Results;
using FluentValidation;
using MediatR;

namespace DemoApi.Application.Features.StateOperation.Command;

public record DeleteState(long id):IRequest<CommandResult<StateVm>>;
public class DeleteStateHandler : IRequestHandler<DeleteState, CommandResult<StateVm>>
{
    private readonly IStateRepository stateRepository;
    private readonly IValidator<DeleteState> validator;

    public DeleteStateHandler(IStateRepository stateRepository, IValidator<DeleteState> validator)
    {
        this.stateRepository = stateRepository;
        this.validator = validator;
    }

    public async Task<CommandResult<StateVm>> Handle(DeleteState request, CancellationToken cancellationToken)
    {
        var validationResult= await validator.ValidateAsync(request, cancellationToken);
        if(validationResult is not null)
        {
            var data = await stateRepository.DeleteAsync(request.id);
            return data switch
            {
                null => new CommandResult<StateVm>(null,CommandResultTypeEnum.NotFound),
                _=> new CommandResult<StateVm>(data,CommandResultTypeEnum.Success)

            };
        }
        throw new ValidationException(validationResult.Errors);
    }
}
public class DeleteStateValidation : AbstractValidator<DeleteState>
{
    public DeleteStateValidation()
    {
        RuleFor(x=>x.id).NotNull().WithMessage("'{PropertyName}' information is required.");
    }
}

