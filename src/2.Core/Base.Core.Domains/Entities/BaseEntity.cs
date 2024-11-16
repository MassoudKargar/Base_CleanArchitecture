namespace Base.Core.Domains.Entities;
/// <summary>
/// Base class for all entities in the system
/// </summary>
public class BaseEntity<TId> where TId : struct
{
    /// <summary>
    /// Entities numerical ID
    /// Be used only for saving in the database and simplicity of work.
    /// </summary>
    public TId Id { get; set; }

    /// <summary>
    /// Entities create data time
    /// </summary>
    public DateTime CreationDate { get; set; }

    /// <summary>
    /// Entities modify date time 
    /// </summary>
    public DateTime? ModifyDate { get; set; }

    /// <summary>
    /// Entities deleted status
    /// </summary>
    public bool IsDeleted { get; set; }

    /// <summary>
    /// The default constructor is defined as Protected.
    /// Given that this needs to be created when constructing the basic Entity properties, no object should be created without filling these properties.
    /// To prevent this, all Entities must have constructors defined that have an input value.
    /// In order to be able to use these entities for the process of storing and retrieving from the database with the help of ORMs, it is necessary to create a default constructor with a high access level such as Protected or Private.
    /// </summary>
    protected BaseEntity() { }


    #region Equality Check
    public bool Equals(BaseEntity<TId>? other) => this == other;
    public override bool Equals(object? obj) =>
         obj is BaseEntity<TId> otherObject && Id.Equals(otherObject.Id);

    public override int GetHashCode() => Id.GetHashCode();
    public static bool operator ==(BaseEntity<TId>? left, BaseEntity<TId>? right)
    {
        if (left is null && right is null)
            return true;

        if (left is null || right is null)
            return false;
        return left.Equals(right);
    }

    public static bool operator !=(BaseEntity<TId> left, BaseEntity<TId> right)
        => !(right == left);

    #endregion
}