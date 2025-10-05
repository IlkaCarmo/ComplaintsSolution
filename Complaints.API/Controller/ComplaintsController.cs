using Complaints.Application.DTOS;
using Complaints.Application.Interfaces;
using Complaints.Application.RabbitMQ;
using Complaints.Infrastructure.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace Complaints.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplaintsController : ControllerBase
    {
        private readonly IRabbitMqPublisher _publisher;
        private readonly IS3Service _s3Service;
        private readonly ISqsPublisher _sqsPublisher;


        public ComplaintsController(IRabbitMqPublisher publisher, IS3Service s3Service, ISqsPublisher sqsPublisher)
        {
            _publisher = publisher;
            _s3Service = s3Service;
            _sqsPublisher = sqsPublisher;
        }


        //Canal digital
        [HttpPost("digital")]
        public async Task<IActionResult> SubmitComplaintWithOptionalAttachments( [FromForm] ComplaintInputDto input,[FromForm] List<IFormFile>? attachments)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var attachmentUrls = new List<string>();

            if (attachments != null && attachments.Any())
            {
                foreach (var file in attachments)
                {
                    var fileName = $"{Guid.NewGuid()}_{file.FileName}";
                    await _s3Service.UploadFileAsync(file, fileName);
                    await _sqsPublisher.PublishAsync(fileName);
                    attachmentUrls.Add(fileName);
                }
            }

            var complaint = new ComplaintDto
            {
                CustomerName = input.CustomerName,
                CPF = input.CPF,
                Description = input.Description,
                Channel = input.Channel,
                Status = input.Status,
                CreatedAt = input.CreatedAt,
                AttachmentUrls = attachmentUrls
            };

            _publisher.Publish(complaint);

            object anexos = attachmentUrls.Any() ? attachmentUrls : "Nenhum anexo enviado";

            return Accepted(new
            {
                message = "Reclamação recebida com sucesso.",
                anexos
            });
        }


        //Canal fisico
        [HttpPost("upload")]
        public async Task<IActionResult> UploadDocument(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Arquivo inválido.");

            var fileName = $"{Guid.NewGuid()}_{file.FileName}";
            await _s3Service.UploadFileAsync(file, fileName);
            await _sqsPublisher.PublishAsync(fileName);

            return Accepted(new { message = "Documento recebido com sucesso.", fileName });
        }
    }
}