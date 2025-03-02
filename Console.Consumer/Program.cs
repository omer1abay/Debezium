// See https://aka.ms/new-console-template for more information
using Confluent.Kafka;
using Console.Consumer;
using Newtonsoft.Json.Linq;
class Consumer
{
    static void Main(string[] args)
    {
        ConsumerConfig config = new()
        {
            BootstrapServers = "kafka:9092",
            GroupId = Guid.NewGuid().ToString(),
            AutoOffsetReset = AutoOffsetReset.Earliest, // Önemli: Earliest tüm mesajları en baştan okur
            EnableAutoCommit = false // İsteğe bağlı: Manuel commit için
        };
        using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
        consumer.Subscribe("datatransfer.public.product");
        while (true)
        {
            try
            {
                var consumeResult = consumer.Consume();
                var data = JObject.Parse(consumeResult.Message.Value);
                var product = System.Text.Json.JsonSerializer.Deserialize<ProductModel>(data["payload"].ToString());
                System.Console.WriteLine($"Consumed message '{product.Id}' at: '{consumeResult.TopicPartitionOffset}'.");
            }
            catch (ConsumeException e)
            {
                System.Console.WriteLine($"Error occurred: {e.Error.Reason}");
            }
        }
    }
}

