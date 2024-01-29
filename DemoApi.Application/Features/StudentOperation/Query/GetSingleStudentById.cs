using DemoApi.Application.Repositories;
using DemoApi.Application.ViewModel;
using DemoApi.Domain.Extensions.Results;
using FluentValidation;
using MediatR;

namespace DemoApi.Application.Features.StudentOperation.Query;

public record GetSingleStudentById(long id):IRequest<QueryResult<StudentVm>>;

public class GetSingleStudentByIdHandler : IRequestHandler<GetSingleStudentById, QueryResult<StudentVm>>
{
    private readonly IStudentRepository _studentRepository;
    private readonly IValidator<GetSingleStudentById> _validator;

    public GetSingleStudentByIdHandler(IStudentRepository studentRepository, IValidator<GetSingleStudentById> validator )
    {
        _studentRepository = studentRepository;
        _validator = validator;
    }

    public async Task<QueryResult<StudentVm>> Handle(GetSingleStudentById request, CancellationToken cancellationToken)
    {
       var validationResult=await _validator.ValidateAsync(request,cancellationToken);
        if(validationResult is not null)
        {
            var data = await _studentRepository.FirstOrDefaultAsync(request.id);
            return data switch 
            { 
                null=>new QueryResult<StudentVm>(default,QueryResultTypeEnum.NotFound),
                _=> new QueryResult<StudentVm>(data,QueryResultTypeEnum.Success),
            };

        }
        throw new ValidationException(validationResult.Errors);
    }
}
public class GetSingleStudentByIdValidation : AbstractValidator<GetSingleStudentById> 
{
    public GetSingleStudentByIdValidation()
    {
        RuleFor(x => x.id).NotNull().WithMessage("'{PropertyName}' information is required.");
    }
}


