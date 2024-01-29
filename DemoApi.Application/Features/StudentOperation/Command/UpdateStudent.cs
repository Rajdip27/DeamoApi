using AutoMapper;
using DemoApi.Application.Repositories;
using DemoApi.Application.ViewModel;
using DemoApi.Domain.Extensions.Results;
using DemoApi.Domain.Model;
using FluentValidation;
using MediatR;

namespace DemoApi.Application.Features.StudentOperation.Command;

public record UpdateStudent(long id,StudentVm StudentVm):IRequest<CommandResult<StudentVm>>;

public class UpdateStudentHandler : IRequestHandler<UpdateStudent, CommandResult<StudentVm>>
{
    private readonly IStudentRepository _studentRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<UpdateStudent> _validator;

    public UpdateStudentHandler(IStudentRepository studentRepository, IMapper mapper, IValidator<UpdateStudent> validator)
    {
        _studentRepository = studentRepository;
        _mapper = mapper;
        _validator = validator;
    }
    public async Task<CommandResult<StudentVm>> Handle(UpdateStudent request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (validationResult is not null)
        {
            var result= _mapper.Map<StudentVm>(await _studentRepository.UpdateAsync(request.id,_mapper.Map<Student>(request.StudentVm)));
            if(result is not null)
            {
                return new CommandResult<StudentVm> ( result ,CommandResultTypeEnum.Success);
            }
            return new CommandResult<StudentVm>(default, CommandResultTypeEnum.UnprocessableEntity);
        }
        throw new ValidationException(validationResult.Errors);
    }
}
public class UpdateStudentValidator : AbstractValidator<UpdateStudent> {
    public UpdateStudentValidator()
    {
        RuleFor(x => x.StudentVm.Name).NotNull().WithMessage("'{PropertyName}' information is required.");
        RuleFor(x => x.StudentVm.FatherName).NotNull().WithMessage("'{PropertyName}' information is required.");
        RuleFor(x => x.StudentVm.MotherName).NotNull().WithMessage("'{PropertyName}' information is required.");
        RuleFor(x => x.StudentVm.Email).NotNull().WithMessage("'{PropertyName}' information is required.");
        RuleFor(x => x.StudentVm.Phone).NotNull().WithMessage("'{PropertyName}' information is required.");
        RuleFor(x => x.StudentVm.Address).NotNull().WithMessage("'{PropertyName}' information is required.");
        RuleFor(x => x.StudentVm.Id).NotNull().WithMessage("'{PropertyName}' information is required.");
        RuleFor(x => x.id).NotNull().WithMessage("'{PropertyName}' information is required.");
    }
}



