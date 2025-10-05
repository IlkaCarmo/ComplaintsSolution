using Complaints.Application.Interfaces;
using Complaints.Application.RabbitMQ;
using Complaints.Infrastructure.Messaging;
using Complaints.Infrastructure.RabbitMQ;
using Complaints.Infrastructure.Storage;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IRabbitMqPublisher, RabbitMqPublisher>();
builder.Services.AddScoped<IS3Service, S3Service>();
builder.Services.AddScoped<ISqsPublisher, SqsPublisher>();
builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());
builder.Services.TryAddAWSService<Amazon.S3.IAmazonS3>();
builder.Services.TryAddAWSService<Amazon.SQS.IAmazonSQS>();
builder.Services.AddControllers();



// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Complaints API",
        Version = "v1",
        Description = "API para recebimento e rastreabilidade de reclamações com anexos",
        Contact = new OpenApiContact
        {
            Name = "Ilka Dev",
            Email = "ilka@example.com"
        }
    });
});

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
 
var app = builder.Build();

// Pipeline HTTP
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Complaints API v1");
    c.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

