using Amazon.S3;
using Amazon.SQS;
using Amazon.SQS.Model;
using Complaints.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complaints.Infrastructure.Messaging
{
    public class SqsPublisher : ISqsPublisher
    {
        private readonly IAmazonSQS _sqsClient;
        private readonly string _queueUrl = "https://sqs.sa-east-1.amazonaws.com/123456789012/complaints-ocr";

        public SqsPublisher(IAmazonSQS sqsClient)
        {
            _sqsClient = sqsClient;
        }
        public async Task PublishAsync(string fileName)
        {
            var message = new SendMessageRequest
            {
                QueueUrl = _queueUrl,
                MessageBody = fileName
            };

            await _sqsClient.SendMessageAsync(message);
        }
    }
}
