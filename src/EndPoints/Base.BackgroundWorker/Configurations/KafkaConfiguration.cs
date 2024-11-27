namespace Base.Application.Configurations;

public class KafkaConfiguration
{
    public string Brokers { get; set; }
    public string InputTopic { get; set; }
    public string OutputTopic { get; set; }
    public string ConsumerGroup { get; set; }
}