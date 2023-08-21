using Newtonsoft.Json;
using ProductApp.Core.Entities;
using ProductApp.Core.Repositories;
using ProductApp.Core.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApp.Service.Services
{
    public class RabbitMQConsumerService : IRabbitMQConsumerService
    {
        private readonly IGenericRepository<User> _repository;

        public RabbitMQConsumerService(IGenericRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task SendNewCategoryMail() 
        {
            ConnectionFactory connectionConfig = new ConnectionFactory()
            {
                HostName = "localhost",
                Port = 5672,
                UserName = "guest",
                Password = "guest"
            };

            using (var conneciton = connectionConfig.CreateConnection())
            using (var channel = conneciton.CreateModel())
            {
                channel.QueueDeclare
                (
                    queue: "yeni-kategori-bilgilendirme",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                );

                var queues = new EventingBasicConsumer(channel);
                string mailList = string.Empty;

                queues.Received += (model, mq) =>
                {
                    var messageBody = mq.Body;
                    mailList = Encoding.UTF8.GetString(messageBody.ToArray());
                };

                channel.BasicConsume
                (
                    queue: "yeni-kategori-bilgilendirme",
                    autoAck: false, 
                    consumer: queues
                );

                SendMail(mailList);
            }


        }

        private void SendMail(string mailList)
        {
            var list = JsonConvert.DeserializeObject<List<User>>(mailList);

            foreach (var mailAddress in list)
            {
                Console.WriteLine("Mail sent successfully to " + mailAddress);
            }
        }
    }
}
