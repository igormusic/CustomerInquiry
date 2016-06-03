using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerInquiry.PublishFakes
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var numberOfCustomers =10;
            string kafkaUrl = "192.168.0.126:9092";
            string kafkaTopic ="customers";

            ArgumentSyntax.Parse(args, syntax =>
            {
                syntax.DefineOption("n|number", ref numberOfCustomers, "Number of customers to generate");
                syntax.DefineOption("u|url", ref kafkaUrl, "Kafka url (IPAddress:PortNumber)");
                syntax.DefineOption("t|topic", ref kafkaTopic, "Kafka topic");
            });


            var faker = FakerFactory.CreateCustomerFaker();
            var publisher = new Publisher(kafkaUrl, kafkaTopic);

            for (var i = 1; i < numberOfCustomers; i++) {
                var customer = faker.Generate();
                var customerJSON = JsonConvert.SerializeObject(customer);
                publisher.Publish(customerJSON);
            }

           
            Console.ReadLine();
        }
    }
}
