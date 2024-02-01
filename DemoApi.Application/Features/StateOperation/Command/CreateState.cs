using AutoMapper;
using DemoApi.Application.Repositories;
using DemoApi.Application.ViewModel;
using DemoApi.Domain.Extensions.Results;
using FluentValidation;
using MediatR;

namespace DemoApi.Application.Features.StateOperation.Command;

public record CreateState(StateVm model):IRequest<CommandResult<StateVm>>;
public class CreateStateHandler : IRequestHandler<CreateState, CommandResult<StateVm>>
{
	private readonly IStateRepository _stateRepository;
	private readonly IMapper _mapper;
	private readonly IValidator<CreateState> _validator;

	public CreateStateHandler(IStateRepository stateRepository, IMapper mapper, IValidator<CreateState> validator)
	{
		_stateRepository = stateRepository;
		_mapper = mapper;
		_validator = validator;
	}

	public Task<CommandResult<StateVm>> Handle(CreateState request, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}
public class CreateStateHandlerValidation : AbstractValidator<CreateState> {
    public CreateStateHandlerValidation()
    {
		RuleFor(x => x.model.Name).NotNull().WithMessage("'{PropertyName}' information is required.");
		RuleFor(x => x.model.CountryId).NotNull().WithMessage("'{PropertyName}' information is required.");
	}
}

