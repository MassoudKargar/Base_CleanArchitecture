namespace Heris.Sample.BackgroundWorker.LocationService;

public class LocationConsumerService(
    IKafkaConsumerConfiguration configurationKafka,
    ILogger<LocationConsumerService> logger)
    : KafkaConsumerService<string, int>(configurationKafka, logger)
{
    public override Task StartAsync(CancellationToken cancellationToken)
    {
        return base.StartAsync(cancellationToken);
    }

    protected override async Task Consume(ConsumeResult<string, int> consumeResult, CancellationToken cancellationToken)
    {
    }
}
