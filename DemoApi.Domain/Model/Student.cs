using DemoApi.Domain.Model.BaseEntities;

namespace DemoApi.Domain.Model;
public class Student: AuditableEntity
{
    public long StudentId { get; set; }
    public string Name { get; set;}
    public string Email { get; set; }
    public string FatherName { get; set; }
    public string MotherName { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public DateTime DateOfBirth { get; set; }   
}
