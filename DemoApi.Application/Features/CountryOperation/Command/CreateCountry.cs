using AutoMapper;
using DemoApi.Application.Repositories;
using DemoApi.Application.ViewModel;
using DemoApi.Domain.Extensions.Results;
using DemoApi.Domain.Model;
using FluentValidation;
using MediatR;

namespace DemoApi.Application.Features.CountryOperation.Command;

public record CreateCountry(CountryVm CountryVm):IRequest<CommandResult<CountryVm>>;

public class CreateCountryHandler : IRequestHandler<CreateCountry, CommandResult<CountryVm>>
{
    private readonly ICountryRepository _countryRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateCountry> _validator;

    public CreateCountryHandler(ICountryRepository countryRepository, IMapper mapper, IValidator<CreateCountry> validator)
    {
        _countryRepository = countryRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<CommandResult<CountryVm>> Handle(CreateCountry request, CancellationToken cancellationToken)
    {
        var validationResult= await _validator.ValidateAsync(request,cancellationToken);
        if(validationResult is not null)
        {
            var data = await _countryRepository.InsertAsync(_mapper.Map<Country>(request.CountryVm));
            return data switch
            {
                null => new CommandResult<CountryVm>(default, CommandResultTypeEnum.UnprocessableEntity),
                _ => new CommandResult<CountryVm>(data, CommandResultTypeEnum.Success),
            };
        }
        throw new ValidationException(validationResult.Errors);
    }
}

public class CreateCountryValidation : AbstractValidator<CreateCountry> {
    public CreateCountryValidation()
    {
        RuleFor(x => x.CountryVm.Name).NotNull().WithMessage("'{PropertyName}' information is required.");
    }
}
