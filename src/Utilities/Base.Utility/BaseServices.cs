namespace Base.Utility;

public class BaseServices(IJsonSerializer serializer)
{
    public readonly IJsonSerializer Serializer = serializer;
}