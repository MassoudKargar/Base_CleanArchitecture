namespace Base.Sample.BackgroundWorker.LocationService;

public class LocationConsumerService : AbstractBackgroundWorker
{
    private readonly ILogger<LocationConsumerService> _logger;
    private readonly KafkaConfiguration _kafkaConfiguration;
    private IConsumer<string, int> _consumer;

    public LocationConsumerService(IOptions<KafkaConfiguration> kafkaConfigurationOptions, ILogger<LocationConsumerService> logger)
    {
        _logger = logger;
        _kafkaConfiguration = kafkaConfigurationOptions?.Value ?? throw new ArgumentException(nameof(kafkaConfigurationOptions));
        var consumerConfig = new ConsumerConfig
        {
            BootstrapServers = _kafkaConfiguration.Brokers, // Replace with your Kafka broker address
            GroupId = _kafkaConfiguration.ConsumerGroup,
            AutoOffsetReset = AutoOffsetReset.Earliest,

        };
        _consumer = new ConsumerBuilder<string, int>(consumerConfig).Build();
    }

    public override Task StartAsync(CancellationToken cancellationToken)
    {
        _consumer.Subscribe(_kafkaConfiguration.InputTopic);
        return base.StartAsync(cancellationToken);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                _logger.LogInformation("Kafka Consumer Service has started.");
                _consumer.Subscribe(_kafkaConfiguration.InputTopic);
                var consumeResult = _consumer.Consume(stoppingToken);
                if (consumeResult?.Message == null) continue;
                if (consumeResult.Topic.Equals(_kafkaConfiguration.InputTopic))
                {
                    await Consume(consumeResult.Message.Value, stoppingToken);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }
    }

    private async Task Consume(int messageValue, CancellationToken stoppingToken)
    {
        throw new NotImplementedException();
    }
}

