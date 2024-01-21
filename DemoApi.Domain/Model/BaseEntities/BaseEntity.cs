using System.Security.Cryptography;

namespace DemoApi.Domain.Model.BaseEntities;
public abstract class BaseEntity<TId>
{
    public TId Id { get; set; }
}
public abstract class BaseEntity:BaseEntity<long> { }
