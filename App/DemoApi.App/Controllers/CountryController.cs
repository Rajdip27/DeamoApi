using DemoApi.App.Controllers.Base;
using DemoApi.Application.Features.CountryOperation.Command;
using DemoApi.Application.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace DemoApi.App.Controllers;


public class CountryController : ApiControllerBase 
{
    [HttpPost]
    public async Task<ActionResult<CountryVm>> CreateCountry([FromBody] CountryVm countryVm) =>
        await  HandelCommandAsync(new CreateCountry(countryVm));

    [HttpPut]
    public async Task<ActionResult<CountryVm>> UpdateCountry(long id , [FromBody],CountryVm model)=>
        await HandelCommandAsync(new UpdateCountry(id, model));
    



}


