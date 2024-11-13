namespace Base.Samples.Core.Domain.People.Entities;

public class Person : BaseEntity<long>, IDbSet
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
} 