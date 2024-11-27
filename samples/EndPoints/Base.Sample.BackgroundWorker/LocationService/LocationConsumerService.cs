namespace Base.Sample.BackgroundWorker.LocationService;

public class LocationConsumerService(
    ILogger<KafkaConsumerService<string, int>> logger,
    IOptions<KafkaConfiguration> kafkaConfigurationOptions)
    : KafkaConsumerService<string,int>(logger, kafkaConfigurationOptions)
{
    protected override Task Consume(int value, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    protected override void Init()
    {

    }
}

