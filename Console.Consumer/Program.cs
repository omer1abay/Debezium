// See https://aka.ms/new-console-template for more information
using System;
using Console;
using Confluent.Kafka;

class Consumer
{
    static void Main(string[] args)
    {
        ConsumerConfig config = new()
        {
            BootstrapServers = "localhost:9092",
            GroupId = Guid.NewGuid().ToString(),
        };
        using IConsumer<string, string> consumer
            = new ConsumerBuilder<string, string>(config).Build();
        try
        {
            TopicPartition topicPartition = new("purchases", new Partition(0));
            WatermarkOffsets watermarkOffsets
                = consumer.QueryWatermarkOffsets(topicPartition, TimeSpan.FromSeconds(3));
            TopicPartitionOffset topicPartitionOffset
                = new(topicPartition, new Offset(watermarkOffsets.High.Value - 1));
            consumer.Assign(topicPartitionOffset);
            ConsumeResult<string, string> consumeResult
                = consumer.Consume(TimeSpan.FromSeconds(3));
            Console.Write($"Last message value = {consumeResult.Message.Value}, " +
                $"position = {consumer.Position(topicPartition)}");
        }
        finally { consumer.Close(); }
    }
}

