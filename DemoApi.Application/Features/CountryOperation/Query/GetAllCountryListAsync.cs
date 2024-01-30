using AutoMapper;
using DemoApi.Application.Repositories;
using DemoApi.Application.ViewModel;
using DemoApi.Domain.Extensions.Pagging;
using DemoApi.Domain.Extensions.Results;
using DemoApi.Domain.Model;
using MediatR;

namespace DemoApi.Application.Features.CountryOperation.Query;

public record GetAllCountryListAsync(int PageSize, int PageIndex, string SearchText) :IRequest<QueryResult<Paging<CountryVm>>>;

public class GetAllCountryListAsyncHandler : IRequestHandler<GetAllCountryListAsync, QueryResult<Paging<CountryVm>>>
{
    private readonly ICountryRepository _countryRepository;
    private readonly IMapper _mapper;

    public GetAllCountryListAsyncHandler(ICountryRepository countryRepository, IMapper mapper)
    {
        _countryRepository = countryRepository;
        _mapper = mapper;
    }

    public async Task<QueryResult<Paging<CountryVm>>> Handle(GetAllCountryListAsync request, CancellationToken cancellationToken)
    {
        var result = await _countryRepository.GetPageAsync(request.PageIndex, request.PageSize,
       p => (string.IsNullOrEmpty(request.SearchText) | p.Name.Contains(request.SearchText)),
       o => o.OrderBy(o => o.Name),
       se => se);
        var data = result.ToPagingModel<Country, CountryVm>(_mapper);
        return data switch
        {
            null => new QueryResult<Paging<CountryVm>>(default, QueryResultTypeEnum.NotFound),
            _ => new QueryResult<Paging<CountryVm>>(data, QueryResultTypeEnum.Success)
        };
    }
}

