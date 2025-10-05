using Complaints.Application.DTOS;
using Complaints.Application.RabbitMQ;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace Complaints.Infrastructure.RabbitMQ
{
    public class RabbitMqPublisher : IRabbitMqPublisher
    {
        public void Publish<ComplaintDto>(ComplaintDto complaint)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "complaints", durable: false, exclusive: false, autoDelete: false, arguments: null);

            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(complaint));
            channel.BasicPublish(exchange: "", routingKey: "complaints", basicProperties: null, body: body);
        }
    }
}
