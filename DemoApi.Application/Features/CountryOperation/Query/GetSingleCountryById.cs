using DemoApi.Application.Repositories;
using DemoApi.Application.ViewModel;
using DemoApi.Domain.Extensions.Results;
using FluentValidation;
using MediatR;

namespace DemoApi.Application.Features.CountryOperation.Query;

public record GetSingleCountryById(long id):IRequest<QueryResult<CountryVm>>;
public class GetSingleCountryByIdHandler : IRequestHandler<GetSingleCountryById, QueryResult<CountryVm>>
{
    private readonly ICountryRepository _countryRepository;
    private readonly IValidator<GetSingleCountryById> _validator;

    public GetSingleCountryByIdHandler(ICountryRepository countryRepository, IValidator<GetSingleCountryById> validator)
    {
        _countryRepository = countryRepository;
        _validator = validator;
    }

    public async Task<QueryResult<CountryVm>> Handle(GetSingleCountryById request, CancellationToken cancellationToken)
    {
        var validationResult= await _validator.ValidateAsync(request, cancellationToken);
        if(validationResult is not null)
        {
            var data = await _countryRepository.FirstOrDefaultAsync(request.id);
            return data switch
            {
                null=>new QueryResult<CountryVm>(default,QueryResultTypeEnum.NotFound),
                _=> new QueryResult<CountryVm>(data,QueryResultTypeEnum.Success)
            };
        }
        throw new ValidationException(validationResult.Errors);
       
    }
}

public class GetSingleCountryByIdValidation : AbstractValidator<GetSingleCountryById> {
    public GetSingleCountryByIdValidation()
    {
        RuleFor(x => x.id).NotNull().WithMessage("'{PropertyName}' information is required.");
    }
}


