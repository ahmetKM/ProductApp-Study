using Newtonsoft.Json;
using ProductApp.Core.Entities;
using ProductApp.Core.Repositories;
using ProductApp.Core.Services;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApp.Service.Services
{
    public class RabbitMQProducerService : IRabbitMQProducerService
    {
        private readonly IGenericRepository<User> _repository;

        public RabbitMQProducerService(IGenericRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task SendNewCategoryMailToQueue() 
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

                var users = _repository.GetAll();
                if (users != null && users.Count() > 0) 
                {
                    string json = JsonConvert.SerializeObject(users.ToList());
                    byte[] messageContent = Encoding.UTF8.GetBytes(json);

                    channel.BasicPublish
                    (
                        exchange: "",
                        routingKey: "yeni-kategori-bilgilendirme",
                        basicProperties: null,
                        body: messageContent
                    );
                }
                
            }
        }
    }
}
