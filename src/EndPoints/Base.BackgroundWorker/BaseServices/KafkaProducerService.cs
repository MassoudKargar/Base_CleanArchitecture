using System.Text;
using System.Threading;

namespace Base.BackgroundWorker.BaseServices;

internal abstract class KafkaProducerService<TValue> : BackgroundService
{ 
    private IProducer<string, TValue> _producer;
    private readonly ILogger<KafkaProducerService<TValue>> _logger;
    private readonly KafkaConfiguration _kafkaConfiguration;
    protected KafkaProducerService(ILogger<KafkaProducerService<TValue>> logger, IOptions<KafkaConfiguration> kafkaConfigurationOptions)
    {
        _logger = logger ?? throw new ArgumentException(nameof(logger));
        _kafkaConfiguration = kafkaConfigurationOptions?.Value ?? throw new ArgumentException(nameof(kafkaConfigurationOptions));

        Init();
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                _logger.LogInformation("Kafka Producer Service has started.");
                var value = await Produce(stoppingToken);
                var msg = new Message<string, TValue>
                {
                    Key = _kafkaConfiguration.Key,
                    Value = value
                };
                await _producer.ProduceAsync(_kafkaConfiguration.Topic, msg, stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }
    }
    protected abstract Task<TValue> Produce(CancellationToken cancellationToken);
    protected abstract void Init();
}