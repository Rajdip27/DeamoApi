using AutoMapper;
using DemoApi.Application.Repositories.Base;
using DemoApi.Application.ViewModel;
using DemoApi.Domain.Model;
using DemoApi.Infrastructure.DatabaseContext;

namespace DemoApi.Application.Repositories;

public class StateRepository : BaseRepository<State, StateVm, long>, IStateRepository
{
	public StateRepository(IMapper mapper, ApplicationDbContext context) : base(mapper, context)
	{
	}
}
