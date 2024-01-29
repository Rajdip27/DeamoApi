using AutoMapper;
using DemoApi.Domain.Model;

namespace DemoApi.Application.ViewModel;
[AutoMap(typeof(Country),ReverseMap =true)]
public record CountryVm(
    long Id,
    string Name
);

