namespace Base.Samples.Core.Domain.People.Entities;

public class Customer : BaseEntity<int>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public long StoreId { get; set; }
}