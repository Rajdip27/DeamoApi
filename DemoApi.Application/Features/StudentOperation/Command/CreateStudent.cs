using AutoMapper;
using DemoApi.Application.Repositories;
using DemoApi.Application.ViewModel;
using DemoApi.Domain.Extensions.Results;
using DemoApi.Domain.Model;
using FluentValidation;
using MediatR;

namespace DemoApi.Application.Features.StudentOperation.Command;

public record CreateStudent(StudentVm StudentVm) : IRequest<CommandResult<StudentVm>>;

public class CreateStudentHandler : IRequestHandler<CreateStudent, CommandResult<StudentVm>>
{
    private readonly IStudentRepository _studentRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<StudentVm> _validator;

    public CreateStudentHandler(IStudentRepository studentRepository, IMapper mapper, IValidator<StudentVm> validator)
    {
        _studentRepository = studentRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<CommandResult<StudentVm>> Handle(CreateStudent request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request.StudentVm, cancellationToken);
        if (validationResult != null)
        {
            var result = _mapper.Map<StudentVm>(await _studentRepository
                .InsertAsync(_mapper.Map<Student>(request.StudentVm)));

            if (result is not null)
            {
                return new CommandResult<StudentVm>(result, CommandResultTypeEnum.Success);
            }
            return new CommandResult<StudentVm>(null, CommandResultTypeEnum.UnprocessableEntity);
        }
        throw new ValidationException(validationResult.Errors);
    }
}


public class CreateStudentValidator : AbstractValidator<StudentVm>
{
    public CreateStudentValidator()
    {
        RuleFor(x => x.Name).NotNull().WithMessage("'{PropertyName}' information is required.");
        RuleFor(x => x.FatherName).NotNull().WithMessage("'{PropertyName}' information is required.");
        RuleFor(x => x.MotherName).NotNull().WithMessage("'{PropertyName}' information is required.");
        RuleFor(x => x.Email).NotNull().WithMessage("'{PropertyName}' information is required.");
        RuleFor(x => x.Phone).NotNull().WithMessage("'{PropertyName}' information is required.");
        RuleFor(x => x.Address).NotNull().WithMessage("'{PropertyName}' information is required.");
    }

}


