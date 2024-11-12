

namespace Base.Utility;

public class BaseServices(
    ILoggerFactory loggerFactory,
    IJsonSerializer serializer,
    IUserInfoService userInfoService,
    ITranslator translator,
    IMapperAdapter mapperFacade)
{
    public readonly ITranslator Translator = translator;
    public readonly IMapperAdapter MapperFacade = mapperFacade;
    public readonly ILoggerFactory LoggerFactory = loggerFactory;
    public readonly IJsonSerializer Serializer = serializer;
    public readonly IUserInfoService UserInfoService = userInfoService;

}