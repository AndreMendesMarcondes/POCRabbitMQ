using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System;
using System.Text;

namespace Publisher.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private const string UName = "admin";
        private const string PWD = "admin";
        private const string HName = "localhost";

        [HttpPost]
        public IActionResult Post(string message)
        {
            SendMessage(message);
            return StatusCode(201);
        }

        private void SendMessage(string message)
        {
            var connectionFactory = new ConnectionFactory()
            {
                UserName = UName,
                Password = PWD,
                HostName = HName,
                Port = 5672
            };

            var connection = connectionFactory.CreateConnection();
            var model = connection.CreateModel();
            var properties = model.CreateBasicProperties();
            properties.Persistent = false;

            byte[] messagebuffer = Encoding.Default.GetBytes(message);
            model.BasicPublish("topic.exchange", "Message.Bombay.Email", properties, messagebuffer);
            Console.WriteLine("Message Sent From: topic.exchange ");
            Console.WriteLine("Routing Key: Message.Bombay.Email");
            Console.WriteLine("Message Sent");
        }
    }
}
