using DemoApi.Application.Common;
using DemoApi.Application.Repositories;
using DemoApi.Application.ViewModel;
using DemoApi.Domain.Extensions.Dropdown;
using DemoApi.Domain.Extensions.Results;
using MediatR;

namespace DemoApi.Application.Features.StateOperation.Query;

public record GetStateDropdownAsync(long? CountryId = null, string SearchText = null, int Size = CommonVariables.DropdownSize) : IRequest<QueryResult<Dropdown<CountryVm>>>;
public class GetStateDropdownAsyncHandler : IRequestHandler<GetStateDropdownAsync, QueryResult<Dropdown<CountryVm>>>
{
    private readonly IStateRepository _stateRepository;

    public GetStateDropdownAsyncHandler(IStateRepository stateRepository)
    {
        _stateRepository = stateRepository;
    }

    public async Task<QueryResult<Dropdown<CountryVm>>> Handle(GetStateDropdownAsync request, CancellationToken cancellationToken)
    {
        var data = await _stateRepository.GetDropdownAsync(p => (string.IsNullOrEmpty(request.SearchText) | p.Name.Contains(request.SearchText) && ),
            op => op.OrderBy(x => x.Id),
            o => new StateVm { Id = o.Id, Name = o.Name },
            request.Size
            );
        return data switch 
        {
            null=> new QueryResult<Dropdown<CountryVm>>(null,QueryResultTypeEnum.NotFound),
            _=> new QueryResult<Dropdown<CountryVm>>(data,QueryResultTypeEnum.Success)
        
        };

    }
} 

