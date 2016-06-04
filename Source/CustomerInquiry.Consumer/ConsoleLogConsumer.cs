using RdKafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerInquiry.Consumer
{
    public class ConsoleLogConsumer
    {
        private string topicName;
        private EventConsumer eventConsumer;
        private Topic topic;

        public ConsoleLogConsumer(string url, string groupId, string topicName)
        {
            var config = new Config() { EnableAutoCommit = true, GroupId = groupId };
            eventConsumer = new EventConsumer(config, url);

            eventConsumer.OnMessage += EventConsumer_OnMessage;
            eventConsumer.OnConsumerError += EventConsumer_OnConsumerError;
            eventConsumer.OnError += EventConsumer_OnError;
            //eventConsumer.OnPartitionsAssigned +=

            this.topicName = topicName;
        }

        private void EventConsumer_OnError(object sender, Handle.ErrorArgs e)
        {
            Console.WriteLine($"Error code: {e.ErrorCode} Error Reason: {e.Reason}");
        }

        private void EventConsumer_OnConsumerError(object sender, ErrorCode errorCode)
        {
            Console.WriteLine($"Consumer Error: {errorCode.ToString()}");
        }

        public void Start() {
            eventConsumer.Subscribe(new List<String>(new[] { topicName }));
        }

        private void EventConsumer_OnMessage(object sender, Message msg)
        {
            string text = Encoding.UTF8.GetString(msg.Payload, 0, msg.Payload.Length);
            Console.WriteLine($"Topic: {msg.Topic} Partition: {msg.Partition} Offset: {msg.Offset} {text}");
        }  
    }
}
