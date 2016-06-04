using RdKafka;
using System;
using System.CommandLine;

namespace CustomerInquiry.Consumer
{
    
    public class Program
    {
        public static void Main(string[] args)
        {
            string kafkaUrl = "localhost:9092";
            string groupId = "customerConsumer";
            string kafkaTopic = "customers";

            ArgumentSyntax.Parse(args, syntax =>
            {
                syntax.DefineOption("u|url", ref kafkaUrl, "Kafka url (IPAddress:PortNumber)");
                syntax.DefineOption("t|topic", ref kafkaTopic, "Kafka topic");
                syntax.DefineOption("g|group", ref groupId, "Kafka consumer group");
            });

            var consumer = new ConsoleLogConsumer(kafkaUrl, groupId, kafkaTopic);

            consumer.Start();

            Console.WriteLine("Started consumer, press enter to stop consuming");
            Console.ReadLine();
        }

       
    }
}
