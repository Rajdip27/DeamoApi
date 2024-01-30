using AutoMapper;
using DemoApi.Application.Repositories;
using DemoApi.Application.ViewModel;
using DemoApi.Domain.Extensions.Results;
using DemoApi.Domain.Model;
using FluentValidation;
using MediatR;

namespace DemoApi.Application.Features.CountryOperation.Command;

public record UpdateCountry(long id, CountryVm countryVm):IRequest< CommandResult<CountryVm>>;
public class UpdateCountryHandler : IRequestHandler<UpdateCountry, CommandResult<CountryVm>>
{
    private readonly ICountryRepository _countryRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<UpdateCountry> _validator;

    public UpdateCountryHandler(ICountryRepository countryRepository, IMapper mapper, IValidator<UpdateCountry> validator)
    {
        _countryRepository = countryRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<CommandResult<CountryVm>> Handle(UpdateCountry request, CancellationToken cancellationToken)
    {
       var validationResult= await _validator.ValidateAsync(request, cancellationToken);
        if(validationResult is not null)
        {
            var data= await _countryRepository.UpdateAsync(request.id,_mapper.Map<Country>(request.countryVm));
            return data switch
            {
                null=> new CommandResult<CountryVm>(default,CommandResultTypeEnum.UnprocessableEntity),
                _=> new CommandResult<CountryVm>(data,CommandResultTypeEnum.Success)

            };
        }
        throw new ValidationException(validationResult.Errors);
    }
}

public class UpdateCountryValidation : AbstractValidator<UpdateCountry> {
    public UpdateCountryValidation()
    {
        RuleFor(x=>x.countryVm.Name).NotNull().WithMessage("'{PropertyName}' information is required.");
    }
}

