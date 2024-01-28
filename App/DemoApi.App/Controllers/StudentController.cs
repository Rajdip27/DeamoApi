using DemoApi.App.Controllers.Base;
using DemoApi.Application.Features.StudentOperation.Command;
using DemoApi.Application.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace DemoApi.App.Controllers;

public class StudentController : ApiControllerBase
{
    [HttpPost]
    public async Task<ActionResult<StudentVm>> InsertAsync([FromBody] StudentVm model) => await HandelCommandAsync(new CreateStudent(model));
}
