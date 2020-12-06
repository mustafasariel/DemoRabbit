
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsumerApp
{
    //class Class1
    //{
    //    static void Main(string[] args)
    //    {
    //        ConnectionFactory factory = new ConnectionFactory();
    //        factory.Uri = new Uri("amqp://hkhjerrt:hcqiavAqll6-co4abXnSqUBh_hHifz-Z@hornet.rmq.cloudamqp.com/hkhjerrt");

    //        using (IConnection connection = factory.CreateConnection())
    //        using (IModel channel = connection.CreateModel())
    //        {
    //            channel.ExchangeDeclare("directexchange", type: ExchangeType.Direct);
    //            for (int i = 1; i <= 100; i++)
    //            {
    //                byte[] bytemessage = Encoding.UTF8.GetBytes($"sayı - {i}");

    //                IBasicProperties properties = channel.CreateBasicProperties();
    //                properties.Persistent = true;
    //                if (i % 2 != 0)
    //                    channel.BasicPublish(exchange: "directexchange", routingKey: "ciftsayilar", basicProperties: properties, body: bytemessage);
    //                else
    //                    channel.BasicPublish(exchange: "directexchange", routingKey: "teksayilar", basicProperties: properties, body: bytemessage);
    //            }
    //        }
    //    }
    //}
}
