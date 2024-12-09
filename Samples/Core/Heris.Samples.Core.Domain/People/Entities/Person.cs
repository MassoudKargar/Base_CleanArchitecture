namespace Heris.Samples.Core.Domain.People.Entities;

public class Person : BaseEntity<long>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}
