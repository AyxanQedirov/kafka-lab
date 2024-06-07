using Confluent.Kafka;


string topic = "test-topic";

Console.WriteLine("Consumer 1");
ConsumerConfig config = new()
{
    BootstrapServers = "192.168.101.36:9092",
    GroupId = "gr1",
    AutoOffsetReset = AutoOffsetReset.Earliest,
};

using var consumer = new ConsumerBuilder<string, string>(config).Build();
consumer.Subscribe(topic);


while (true)
{
    ConsumeResult<string,string> result = consumer.Consume();
    Thread.Sleep(2);
    Console.WriteLine($"{result.Offset} | {result.Partition.Value} | {result.Value}");
}

