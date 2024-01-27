namespace DemoApi.Application.ViewModel;
public record StudentVm(
    long Id,
    long StudentId,
    string Name,
    string Email,
    string FatherName,
    string MotherName,
    string Address,
    string Phone,
    DateTime DateOfBirth
);

