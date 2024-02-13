using AutoMapper;
using DemoApi.Domain.Model;
using DemoApi.Domain.Model.BaseEntities;

namespace DemoApi.Application.ViewModel;
[AutoMap(typeof(State),ReverseMap =true)]
public class StateVm :BaseEntity
{
    public string Name { get; set; }
    public long CountryId { get; set; }
    public CountryVm Country { get; set; }

}

