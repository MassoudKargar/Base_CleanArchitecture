namespace Base.BackgroundWorker.BaseServices;

internal abstract class KafkaConsumerService<TKey,TValue> : BackgroundService
{
    private readonly ILogger<KafkaConsumerService<TKey, TValue>> _logger;
    private readonly KafkaConfiguration _kafkaConfiguration;
    private IConsumer<TKey, TValue> _consumer;

    protected KafkaConsumerService(ILogger<KafkaConsumerService<TKey, TValue>> logger, IOptions<KafkaConfiguration> kafkaConfigurationOptions)
    {
        _logger = logger ?? throw new ArgumentException(nameof(logger));
        _kafkaConfiguration = kafkaConfigurationOptions?.Value ?? throw new ArgumentException(nameof(kafkaConfigurationOptions));
        Init();
    }

    public override Task StartAsync(CancellationToken cancellationToken)
    {
        _consumer.Subscribe(_kafkaConfiguration.Topic);
        return base.StartAsync(cancellationToken);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                _logger.LogInformation("Kafka Consumer Service has started.");
                _consumer.Subscribe(_kafkaConfiguration.Topic);
                var consumeResult = _consumer.Consume(stoppingToken);
                if (consumeResult?.Message == null) continue;
                if (consumeResult.Topic.Equals(_kafkaConfiguration.Topic))
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

    protected abstract Task Consume(TValue value, CancellationToken cancellationToken);
    protected abstract void Init();
    
    public override void Dispose()
    {
        _consumer.Dispose();
    }
    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Kafka Consumer Service is stopping.");
        _consumer.Close();
        return base.StopAsync(cancellationToken);
    }

}