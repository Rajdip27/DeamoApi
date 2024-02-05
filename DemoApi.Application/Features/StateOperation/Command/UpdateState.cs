using AutoMapper;
using DemoApi.Application.Repositories;
using DemoApi.Application.ViewModel;
using DemoApi.Domain.Extensions.Results;
using DemoApi.Domain.Model;
using FluentValidation;
using MediatR;

namespace DemoApi.Application.Features.StateOperation.Command;

public record UpdateState(long id,StateVm StateVm):IRequest<CommandResult<StateVm>>;
public class UpdateStateHandler : IRequestHandler<UpdateState, CommandResult<StateVm>>
{
    private readonly IStateRepository stateRepository;
    private readonly IMapper mapper;
    private readonly IValidator<UpdateState> validator;

    public UpdateStateHandler(IStateRepository stateRepository, IMapper mapper, IValidator<UpdateState> validator)
    {
        this.stateRepository = stateRepository;
        this.mapper = mapper;
        this.validator = validator;
    }

    public async Task<CommandResult<StateVm>> Handle(UpdateState request, CancellationToken cancellationToken)
    {
        var validationResult= await validator.ValidateAsync(request, cancellationToken);
        if(validationResult is not null)
        {
            var data= await stateRepository.UpdateAsync(request.id,mapper.Map<State>(request));
            return data switch 
            {
                null=> new CommandResult<StateVm>(null,CommandResultTypeEnum.UnprocessableEntity),
                _=> new CommandResult<StateVm>(data,CommandResultTypeEnum.Success)
            };

        }
        throw new ValidationException(validationResult.Errors);
    }
}

public class UpdateStateValidation : AbstractValidator<UpdateState>
{
    public UpdateStateValidation()
    {
        RuleFor(x => x.id).NotNull().WithMessage("'{PropertyName}' information is required.");
        RuleFor(x => x.StateVm.Name).NotNull().WithMessage("'{PropertyName}' information is required.");
        RuleFor(x => x.StateVm.CountryId).NotNull().WithMessage("'{PropertyName}' information is required.");
    }

}

