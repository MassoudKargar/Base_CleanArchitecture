using Base.Extensions.BackgroundWorker.KafkaConsumer;
using Confluent.Kafka;

namespace Base.Sample.Application.KafkaServices
{
    internal class KafkaConsumerConfigurationService : IKafkaConsumerConfiguration
    {
        public string InputTopic { get; set; }
        public ConsumerConfig ConsumerConfig { get; set; }

        public Task GetConfiguration()
        {
            InputTopic = "Location";
            ConsumerConfig = new ConsumerConfig()
            {
                BootstrapServers = "localhost:9092", // Replace with your Kafka broker address
                GroupId = "state-detector-consumer-group",
                AutoOffsetReset = AutoOffsetReset.Earliest,
            };
            throw new NotImplementedException();
        }
    }
}
