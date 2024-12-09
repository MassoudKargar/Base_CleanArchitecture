namespace Heris.Sample.BackgroundWorker.KafkaServices;

public class KafkaConsumerConfigurationService : IKafkaConsumerConfiguration, ISingletonLifetime
{
    public string InputTopic { get; set; }
    public ConsumerConfig ConsumerConfig { get; set; }

    public virtual Task GetConfiguration()
    {
        InputTopic = "Location";
        ConsumerConfig = new ConsumerConfig()
        {
            BootstrapServers = "localhost:9092", // Replace with your Kafka broker address
            GroupId = "state-detector-consumer-group",
            AutoOffsetReset = AutoOffsetReset.Earliest,
        };
        return Task.CompletedTask;
        //throw new NotImplementedException();
    }
}
