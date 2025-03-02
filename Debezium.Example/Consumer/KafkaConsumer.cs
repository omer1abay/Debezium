using Confluent.Kafka;

namespace Debezium.Example.Consumer;

public class KafkaConsumer
{
    private readonly ILogger<KafkaConsumer> _logger;
    private readonly IConfiguration _configuration;
    public KafkaConsumer(ILogger<KafkaConsumer> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }
    public void Consume()
    {
        ConsumerConfig config = new()
        {
            BootstrapServers = "localhost:9092",
            GroupId = Guid.NewGuid().ToString(),
        };
        using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
        consumer.Subscribe("datatransfer.public.Product");
        while (true)
        {
            try
            {
                var consumeResult = consumer.Consume();
                Console.WriteLine($"Consumed message '{consumeResult.Message.Value}' at: '{consumeResult.TopicPartitionOffset}'.");
            }
            catch (ConsumeException e)
            {
                _logger.LogError($"Error occurred: {e.Error.Reason}");
            }
        }
    }
}
