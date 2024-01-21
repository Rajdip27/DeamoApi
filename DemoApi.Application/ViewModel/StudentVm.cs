using DemoApi.Domain.Model.BaseEntities;

namespace DemoApi.Application.ViewModel;
public class StudentVm: BaseEntity
{
    public required long StudentId { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string FatherName { get; set; }
    public required string MotherName { get; set; }
    public required string Address { get; set; }
    public required string Phone { get; set; }
    public required DateOnly DateOfBirth { get; set; }
}
