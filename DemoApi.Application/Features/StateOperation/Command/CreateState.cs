using AutoMapper;
using DemoApi.Application.Repositories;
using DemoApi.Application.ViewModel;
using DemoApi.Domain.Extensions.Results;
using DemoApi.Domain.Model;
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

	public async Task<CommandResult<StateVm>> Handle(CreateState request, CancellationToken cancellationToken)
	{
		var result = await _validator.ValidateAsync(request, cancellationToken);
		if(result is not null)
		{
			var data =await _stateRepository.InsertAsync(_mapper.Map<State>(result));
			return data switch 
			{ 
				null=>new CommandResult<StateVm>(default,CommandResultTypeEnum.UnprocessableEntity),
				_=>new CommandResult<StateVm>(data,CommandResultTypeEnum.Success)
			
			};

		}
		throw new ValidationException(result.Errors);
	}
}
public class CreateStateHandlerValidation : AbstractValidator<CreateState> {
    public CreateStateHandlerValidation()
    {
		RuleFor(x => x.model.Name).NotNull().WithMessage("'{PropertyName}' information is required.");
		RuleFor(x => x.model.CountryId).NotNull().WithMessage("'{PropertyName}' information is required.");
	}
}

