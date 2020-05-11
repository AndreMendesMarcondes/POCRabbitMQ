using Microsoft.AspNetCore.Mvc;
using POCRabbitMQ.Services.Interfaces;
using System.Collections.Generic;

namespace POCRabbitMQ.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RabbitController : ControllerBase
    {
        private IRabbitManager _manager;

        public RabbitController(IRabbitManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            var num = new System.Random().Next(9000);

            _manager.Publish(new
            {
                field1 = $"PocRabbitMQ-{num}",
                field2 = $"Mineiro-{num}"
            }, "demo.exchange.topic.dotnetcore", "topic", "*.queue.durable.dotnetcore.#");

            return new string[] { "value1", "value2" };
        }
    }
}