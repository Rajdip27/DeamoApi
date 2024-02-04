using DemoApi.App.Controllers.Base;
using DemoApi.Application.Features.StateOperation.Command;
using DemoApi.Application.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace DemoApi.App.Controllers;


public class StateController : ApiControllerBase
{
    public async Task<ActionResult<StateVm>> CreatState(StateVm model)=>
        await HandelCommandAsync(new CreateState(model));
   
}
