using AutoMapper;
using DemoApi.Application.Repositories;
using DemoApi.Application.ViewModel;
using DemoApi.Domain.Extensions.Dropdown;
using DemoApi.Domain.Extensions.Results;
using MediatR;

namespace DemoApi.Application.Features.CountryOperation.Query;

public record GetCountryDropdownAsync(string SearchText, int Size) : IRequest<QueryResult<Dropdown<CountryVm>>>;
public class GetCountryDropdownAsyncHandler : IRequestHandler<GetCountryDropdownAsync, QueryResult<Dropdown<CountryVm>>>
{
    private readonly ICountryRepository _countryRepository;
    public GetCountryDropdownAsyncHandler(ICountryRepository countryRepository)
    {
        _countryRepository = countryRepository;
    }

    public async Task<QueryResult<Dropdown<CountryVm>>> Handle(GetCountryDropdownAsync request, CancellationToken cancellationToken)
    {
        var result = await _countryRepository.GetDropdownAsync(
          p => (string.IsNullOrEmpty(request.SearchText) | p.Name.Contains(request.SearchText)),
          o => o.OrderBy(ob => ob.Name),
          se => new CountryVm { Id = se.Id, Name = se.Name },
          request.Size);
        return result switch
        {
            null=>new QueryResult<Dropdown<CountryVm>>(default,QueryResultTypeEnum.NotFound),
            _=>new QueryResult<Dropdown<CountryVm>>(result,QueryResultTypeEnum.Success)
        };
    }
}

