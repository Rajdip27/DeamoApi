using DemoApi.App.Controllers.Base;
using DemoApi.Application.Features.StateOperation.Command;
using DemoApi.Application.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace DemoApi.App.Controllers;


public class StateController : ApiControllerBase
{
    [HttpPost]
    public async Task<ActionResult<StateVm>> CreatState(StateVm model)=>
        await HandelCommandAsync(new CreateState(model));

    [HttpPut("{id:long}")]
    public async Task<ActionResult<StateVm>>UpdateState(long id, StateVm model)=>
        await HandelCommandAsync(new  UpdateState(id, model));
    [HttpDelete("{id:long}")]
    public async Task<ActionResult> DeleteState(long id) =>
        await HandelCommandAsync(new DeleteState(id));
   
   
}
