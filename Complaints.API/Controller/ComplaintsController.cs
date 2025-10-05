using Complaints.Application.DTOS;
using Complaints.Application.RabbitMQ;
using Complaints.Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace Complaints.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplaintsController : ControllerBase
    {
        private readonly IRabbitMqPublisher _publisher;

        public ComplaintsController(IRabbitMqPublisher publisher)
        {
            _publisher = publisher;
        }

        [HttpPost]
        public IActionResult SubmitComplaint([FromBody] ComplaintDto complaint)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _publisher.Publish(complaint);
            return Accepted(new { message = "Reclamação recebida com sucesso." });
        }
    }
}