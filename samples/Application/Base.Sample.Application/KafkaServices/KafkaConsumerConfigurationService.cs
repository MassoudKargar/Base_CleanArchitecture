using Base.Extensions.BackgroundWorker.KafkaConsumer;
using Base.Extensions.DependencyInjection.Abstractions;
using Confluent.Kafka;
namespace Base.Sample.Application.KafkaServices;

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
