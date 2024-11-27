namespace Base.BackgroundWorker.BaseServices;

public abstract class KafkaProducerService<TKey, TValue>
{
    private IProducer<TKey, TValue> _producer;
    private readonly KafkaConfiguration _kafkaConfiguration;
    private ProducerConfig config;
    protected KafkaProducerService(IOptions<KafkaConfiguration> kafkaConfigurationOptions)
    {
        _kafkaConfiguration = kafkaConfigurationOptions.Value;
        config = new ProducerConfig()
        {
            BootstrapServers = _kafkaConfiguration.Brokers
        };
        _producer = new ProducerBuilder<TKey, TValue>(config).Build();
    }
    public async Task ExecuteAsync(TKey key, TValue value, CancellationToken stoppingToken)
    {
        if (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var msg = new Message<TKey, TValue>
                {
                    Key = key,
                    Value = value
                };
                await _producer.ProduceAsync(_kafkaConfiguration.OutputTopic, msg, stoppingToken);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
