using DemoApi.Domain.Model.BaseEntities;

namespace DemoApi.Domain.Model;

public class State: AuditableEntity
{
	public string Name { get; set; }
	public long CountryId { get; set; }
	public Country Country { get; set; }
}
