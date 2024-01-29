using DemoApi.Application.Repositories;
using DemoApi.Application.ViewModel;
using DemoApi.Domain.Extensions.Results;
using FluentValidation;
using MediatR;

namespace DemoApi.Application.Features.StudentOperation.Command;

public record DeleteStudent(long Id) : IRequest<CommandResult<StudentVm>>;

public class DeleteStudentHandler : IRequestHandler<DeleteStudent, CommandResult<StudentVm>>
{
    private readonly IStudentRepository _studentRepository;
    private readonly IValidator<DeleteStudent> _validator;

    public DeleteStudentHandler(IStudentRepository studentRepository, IValidator<DeleteStudent> validator)
    {
        _studentRepository = studentRepository;
        _validator = validator;
    }

    public async Task<CommandResult<StudentVm>> Handle(DeleteStudent request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if(validationResult is not null)
        {
            var result = await _studentRepository.DeleteAsync(request.Id);
            return result switch
            {
                null => new CommandResult<StudentVm>(default, CommandResultTypeEnum.NotFound),
                _ => new CommandResult<StudentVm>(result, CommandResultTypeEnum.Success)
            };
        }
        throw new ValidationException(validationResult.Errors);
    }
}

public class DeleteStudentValidator : AbstractValidator<DeleteStudent>
{
    public DeleteStudentValidator()
    {
        RuleFor(x => x.Id).NotNull().WithMessage("'{PropertyName}' information is required.");
    }
}
