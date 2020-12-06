using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;

namespace ConsumerApp
{
    class Program
    {
        static string myExchange = "ptt";
        static string myExchangeDirect = "matematik";

        private static int bekle = 1000;

        static void Main(string[] args)
        {
            Console.WriteLine("Consumer");
            //fanout();
            direct();
            
        }

        private static void fanout()
        {
            Console.WriteLine("Kuyruk adınız giriniz");
            var queueName = Console.ReadLine();

            Console.WriteLine($"Kuyruk : {queueName}");
            // bekle = int.Parse(args[0]);
            ConnectionFactory factory = new ConnectionFactory();
            factory.HostName = "localhost";

            using (IConnection connection = factory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(myExchange, type: ExchangeType.Fanout);

                    // string queueName = channel.QueueDeclare().QueueName;

                    // Console.WriteLine("queueName");
                    channel.QueueBind(queue: queueName, exchange: myExchange, routingKey: "");


                    channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

                    EventingBasicConsumer consumer = new EventingBasicConsumer(channel);

                    channel.BasicConsume(queueName, false, consumer);
                    consumer.Received += (sender, e) =>
                    {
                        Thread.Sleep(bekle);
                        Console.WriteLine($"{queueName} - {Encoding.UTF8.GetString(e.Body.ToArray())} alındı");
                        channel.BasicAck(e.DeliveryTag, false);
                    };

                    Console.Read();

                }
            }
        }

        private static void direct()
        {
            Console.WriteLine("Kuyruk adınız giriniz");
            var queueName = Console.ReadLine();

            //Console.WriteLine("Key adınız giriniz");
            //var myKey = Console.ReadLine();

            Console.WriteLine($"Kuyruk : {queueName}");
            // bekle = int.Parse(args[0]);
            ConnectionFactory factory = new ConnectionFactory();
            factory.HostName = "localhost";

            using (IConnection connection = factory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    //  channel.ExchangeDeclare(myExchange, type: ExchangeType.Fanout);

                    // string queueName = channel.QueueDeclare().QueueName;

                    // Console.WriteLine("queueName");
                    // channel.QueueBind(queueName, myExchangeDirect, myKey);


                  //  channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

                    EventingBasicConsumer consumer = new EventingBasicConsumer(channel);

                    channel.BasicConsume(queueName, true, consumer);
                    consumer.Received += (sender, e) =>
                    {
                        Thread.Sleep(bekle);
                        Console.WriteLine($"{queueName} - {Encoding.UTF8.GetString(e.Body.ToArray())} alındı");
                        channel.BasicAck(e.DeliveryTag, false);
                    };

                    Console.Read();

                }
            }
        }
    }
}
