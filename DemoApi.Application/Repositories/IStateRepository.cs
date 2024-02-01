using DemoApi.Application.Repositories.Base;
using DemoApi.Application.ViewModel;
using DemoApi.Domain.Model;

namespace DemoApi.Application.Repositories;

public interface IStateRepository:IBaseRepository<State,StateVm,long>
{
}
