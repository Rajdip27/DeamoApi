using AutoMapper;
using DemoApi.Application.Repositories.Base;
using DemoApi.Application.ViewModel;
using DemoApi.Domain.Model;
using DemoApi.Infrastructure.DatabaseContext;

namespace DemoApi.Application.Repositories;

public class StudentRepository : BaseRepository<Student, StudentVm, long>, IStudentRepository
{
    public StudentRepository(IMapper mapper, ApplicationDbContext context) : base(mapper, context)
    {
    }
}
