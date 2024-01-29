using AutoMapper;
using DemoApi.Application.Repositories.Base;
using DemoApi.Application.ViewModel;
using DemoApi.Domain.Model;
using DemoApi.Infrastructure.DatabaseContext;

namespace DemoApi.Application.Repositories;

public class CountryRepository : BaseRepository<Country, CountryVm, long>,ICountryRepository
{
    public CountryRepository(IMapper mapper, ApplicationDbContext context) : base(mapper, context)
    {

    }
}
