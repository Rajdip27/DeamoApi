using DemoApi.Application.Repositories;
using DemoApi.Application.ViewModel;
using DemoApi.Domain.Extensions.Results;
using FluentValidation;
using MediatR;

namespace DemoApi.Application.Features.CountryOperation.Command;

public record DeleteCountry(long id):IRequest<CommandResult<CountryVm>>;
public class DeleteCountryHandler : IRequestHandler<DeleteCountry, CommandResult<CountryVm>>
{
    private readonly ICountryRepository _countryRepository;
    private readonly IValidator<DeleteCountry> _validator;

    public DeleteCountryHandler(ICountryRepository countryRepository, IValidator<DeleteCountry> validator)
    {
        _countryRepository = countryRepository;
        _validator = validator;
    }

    public async Task<CommandResult<CountryVm>> Handle(DeleteCountry request, CancellationToken cancellationToken)
    {
       var validationResult= await _validator.ValidateAsync(request, cancellationToken);
        if (validationResult is not null)
        {
            var result = await _countryRepository.DeleteAsync(request.id);
            return result switch
            {
                null => new CommandResult<CountryVm>(default, CommandResultTypeEnum.NotFound),
                _ => new CommandResult<CountryVm>(result, CommandResultTypeEnum.Success)
            };
        }
        throw new ValidationException(validationResult.Errors);
    }
}

public class DeleteCountryValidation : AbstractValidator<DeleteCountry> {
    public DeleteCountryValidation()
    {
        RuleFor(x=>x.id).NotNull().WithMessage("'{PropertyName}' information is required.");
    }
}

