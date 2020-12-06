using RabbitMQ.Client;
using System;
using System.Text;
using System.Threading;

namespace PublisherApp
{
    class Program
    {
        static string myExchange = "ptt";

        static string myExchangeDirect = "Matematik";

      
        static void Main(string[] args)
        {
            // fanout();
            Console.WriteLine("Publisher");

            direct();


        }

        private static void direct()
        {
            var factory = new ConnectionFactory();
            factory.HostName = "localhost";

            using (IConnection conn = factory.CreateConnection())
            {
                using (IModel channel = conn.CreateModel())
                {
                    channel.ExchangeDeclare(myExchangeDirect, ExchangeType.Direct);

                    for (int i = 0; i < 100; i++)
                    {
                        var str = $"sayi :{i}";
                        byte[] msg = Encoding.UTF8.GetBytes(str);
                        IBasicProperties properties = channel.CreateBasicProperties();

                        
                     
                        properties.Persistent = true;

                        if (i % 2 == 0)
                        {
                            channel.BasicPublish(myExchangeDirect, "ciftsayilar", properties, msg);
                        
                        }
                        else
                        {
                            channel.BasicPublish(myExchangeDirect, "teksayilar", properties, msg);
                        }
                        Console.WriteLine(str);
                        Thread.Sleep(1000);
                    }
                }
            }

        }

        private static void fanout()
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.HostName = "localhost";
            using (IConnection conn = factory.CreateConnection())
            {
                using (IModel channel = conn.CreateModel())
                {
                    channel.ExchangeDeclare(myExchange, type: ExchangeType.Fanout);

                    for (int i = 100; i < 200; i++)
                    {
                        Thread.Sleep(1000);

                        var sendMessage = $"iş - {i}";
                        byte[] bytemessage = Encoding.UTF8.GetBytes(sendMessage);

                        IBasicProperties basicProperties = channel.CreateBasicProperties();
                        basicProperties.Persistent = true;

                        channel.BasicPublish(exchange: myExchange, routingKey: "", basicProperties, body: bytemessage);

                        Console.WriteLine($"Gönderildi: {sendMessage}");
                    }
                }
            }
        }
    }
}
