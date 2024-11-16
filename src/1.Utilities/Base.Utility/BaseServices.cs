namespace Base.Utility;

public class BaseServices(
    ILoggerFactory loggerFactory,
    IJsonSerializer serializer,
    IUserInfoService userInfoService,
    ICacheAdapter cashRepository,
    IMapperAdapter mapperFacade)
{
    public readonly IMapperAdapter MapperFacade = mapperFacade;
    public readonly ILoggerFactory LoggerFactory = loggerFactory;
    public readonly IJsonSerializer Serializer = serializer;
    public readonly IUserInfoService UserInfoService = userInfoService;
    public readonly ICacheAdapter cashRepository = cashRepository;
}