using System.Security.AccessControl;

namespace Base.Samples.Core.Domain.People.Entities;

public class Person : BaseEntity<long>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public long StoreId { get; set; }
    public Store Store { get; set; }
}

public class Store : BaseEntity<long>
{
    public string Name { get; set; }
    public IQueryable<Person> Persons { get; set; }
}