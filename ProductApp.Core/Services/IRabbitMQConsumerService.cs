using ProductApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApp.Core.Services
{
    public interface IRabbitMQConsumerService
    {
        public Task SendNewCategoryMail();
    }
}
