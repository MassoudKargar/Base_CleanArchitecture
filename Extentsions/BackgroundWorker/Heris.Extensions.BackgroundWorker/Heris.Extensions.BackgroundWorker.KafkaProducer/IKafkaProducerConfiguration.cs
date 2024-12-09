using Confluent.Kafka;

namespace Heris.Extensions.BackgroundWorker.KafkaProducer;

public interface IKafkaProducerConfiguration
{
    public string OutputTopic { get; set; }
    public ProducerConfig ProducerConfig { get; set; }
    public Task GetConfiguration();
}
