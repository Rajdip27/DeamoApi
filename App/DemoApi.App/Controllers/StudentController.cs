using DemoApi.App.Controllers.Base;
using DemoApi.Application.Features.StudentOperation.Command;
using DemoApi.Application.Features.StudentOperation.Query;
using DemoApi.Application.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace DemoApi.App.Controllers;

public class StudentController : ApiControllerBase
{
    [HttpPost]
    public async Task<ActionResult<StudentVm>> InsertAsync([FromBody] StudentVm model) =>
        await HandelCommandAsync(new CreateStudent(model));

    [HttpGet]
    public async Task<ActionResult<StudentVm>> GetListAsync(int pageSize = 10, int pageIndex = 0, string searchText = null)=>
        await HandelQueryAsync(new GetAllStudentListAsync(pageSize, pageIndex, searchText));

    [HttpPut("{id:long}")]
    public async Task<ActionResult<StudentVm>>UpdateStudent( long id, [FromBody] StudentVm model) =>
        await HandelCommandAsync(new UpdateStudent(id,model));

    [HttpDelete("{id:long}")]
    public async Task<ActionResult<StudentVm>>Delete(long id)=>
        await HandelCommandAsync(new DeleteStudent(id));
    [HttpGet("{id:long}")]
    public async Task<ActionResult<StudentVm>> GetStudentById(long id) =>
        await HandelQueryAsync(new GetSingleStudentById(id));

    

    
    
}
