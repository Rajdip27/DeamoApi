using DemoApi.Domain.Model.BaseEntities;

namespace DemoApi.Domain.Model;

public class Country: AuditableEntity
{
    public string Name { get; set; }
    public ICollection<State>  States { get; set;}=new HashSet<State>();    
}
