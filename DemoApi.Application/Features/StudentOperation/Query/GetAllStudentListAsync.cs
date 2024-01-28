using AutoMapper;
using DemoApi.Application.Repositories;
using DemoApi.Application.ViewModel;
using DemoApi.Domain.Extensions.Pagging;
using DemoApi.Domain.Extensions.Results;
using DemoApi.Domain.Model;
using MediatR;

namespace DemoApi.Application.Features.StudentOperation.Query;

public record GetAllStudentListAsync(int PageSize, int PageIndex, string SearchText):IRequest<QueryResult<Paging<StudentVm>>>;

public class GetAllStudentListAsyncHandler : IRequestHandler<GetAllStudentListAsync, QueryResult<Paging<StudentVm>>>
{
    private readonly IStudentRepository _studentRepository;
    private readonly IMapper _mapper;

    public GetAllStudentListAsyncHandler(IStudentRepository studentRepository, IMapper mapper)
    {
        _studentRepository = studentRepository;
        _mapper = mapper;
    }

    public async Task<QueryResult<Paging<StudentVm>>> Handle(GetAllStudentListAsync request, CancellationToken cancellationToken)
    {
      var result = await _studentRepository.GetPageAsync(request.PageIndex, request.PageSize,
      p => (string.IsNullOrEmpty(request.SearchText) | p.Name.Contains(request.SearchText)),
      o => o.OrderBy(o => o.Id),
      se => se);

        var data = result.ToPagingModel<Student, StudentVm>(_mapper);

        return data switch
        {
            not null => new QueryResult<Paging<StudentVm>>(data, QueryResultTypeEnum.Success),
            _ => new QueryResult<Paging<StudentVm>>(null, QueryResultTypeEnum.NotFound)
        };
    }
}
