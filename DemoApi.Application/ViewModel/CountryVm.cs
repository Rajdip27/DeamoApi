using AutoMapper;
using DemoApi.Domain.Model;
using DemoApi.Domain.Model.BaseEntities;

namespace DemoApi.Application.ViewModel;
[AutoMap(typeof(Country), ReverseMap = true)]
public class CountryVm : BaseEntity
{
    public string Name { get; set; }
}

