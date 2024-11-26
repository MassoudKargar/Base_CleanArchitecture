namespace Base.Application.BaseMediatR;

public enum GenericAction : byte
{
    GetAll = 0,
    GetById = 1,
    Insert = 2,
    Update = 3,
    Delete = 4,
}