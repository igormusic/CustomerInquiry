using RdKafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CustomerInquiry.PublishFakes
{
    public class Publisher
    {
        private Producer producer;
        private Topic topic;

        public Publisher(string url, string topicName) {

            producer = new Producer(url);
            topic = producer.Topic(topicName);
            
        }
        public async void Publish(string message)
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            DeliveryReport deliveryReport = await topic.Produce(data);
            Console.WriteLine($"Produced to Partition: {deliveryReport.Partition}, Offset: {deliveryReport.Offset}");
        }
    }
}
