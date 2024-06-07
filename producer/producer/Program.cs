using Confluent.Kafka;
using Confluent.Kafka.Admin;

string topic = "test-topic";

ProducerConfig config = new()
{
    BootstrapServers = "192.168.101.36:9092"
};


using var producer = new ProducerBuilder<Null, string>(config).Build();

try
{
    while (true)
    {
        Console.Write("Enter your message: ");
        var messageValue = Console.ReadLine();

        TopicPartition topicPartition = new TopicPartition(topic, new Partition(new Random().Next(3)));

        Message<Null, string> message = new()
        {
            Value = messageValue
        };
        var produceResult = await producer.ProduceAsync(topicPartition, message);

        Console.WriteLine($"Sended to {topic} topic");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Exception occured: {ex.Message}");
}



async Task CreateTopic(string name, int partitioCount)
{

    using var adminClient = new AdminClientBuilder(config).Build();
    TopicSpecification topicSettings = new()
    {
        Name = name,
        NumPartitions = partitioCount,
        ReplicationFactor = 1
    };

    await adminClient.CreateTopicsAsync(new List<TopicSpecification> { topicSettings });
}

