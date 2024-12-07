//using Base.Extensions.BackgroundWorker.KafkaConsumer;

//namespace Base.Sample.BackgroundWorker.LocationService;

//public class LocationConsumerService : KafkaConsumerService<string,int>
//{
//    public override Task StartAsync(CancellationToken cancellationToken)
//    {
//        return base.StartAsync(cancellationToken);
//    }
//    public LocationConsumerService(IKafkaConsumerConfiguration configurationKafka, ILogger<KafkaConsumerService<string, int>> logger) : base(configurationKafka, logger)
//    {
//    }

//    protected override async Task Consume(ConsumeResult<string, int> consumeResult, CancellationToken cancellationToken)
//    {
//    }
//}
