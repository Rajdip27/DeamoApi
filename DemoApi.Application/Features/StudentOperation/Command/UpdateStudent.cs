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
    private readonly IValidator<StudentVm> _validator;

    public UpdateStudentHandler(IStudentRepository studentRepository, IMapper mapper, IValidator<StudentVm> validator)
    {
        _studentRepository = studentRepository;
        _mapper = mapper;
        _validator = validator;
    }
    public async Task<CommandResult<StudentVm>> Handle(UpdateStudent request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request.StudentVm, cancellationToken);
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

public class UpdateStudentValidator : AbstractValidator<StudentVm> {
    public UpdateStudentValidator()
    {
        RuleFor(x => x.Name).NotNull().WithMessage("'{PropertyName}' information is required.");
        RuleFor(x => x.FatherName).NotNull().WithMessage("'{PropertyName}' information is required.");
        RuleFor(x => x.MotherName).NotNull().WithMessage("'{PropertyName}' information is required.");
        RuleFor(x => x.Email).NotNull().WithMessage("'{PropertyName}' information is required.");
        RuleFor(x => x.Phone).NotNull().WithMessage("'{PropertyName}' information is required.");
        RuleFor(x => x.Address).NotNull().WithMessage("'{PropertyName}' information is required.");
    }
}



