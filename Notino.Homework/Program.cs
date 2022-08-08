using MediatR;
using Notino.Homework.Converters;
using Notino.Homework.EmailService;
using Notino.Homework.EmailService.Interfaces;
using Notino.Homework.EmailService.Model;
using Notino.Homework.Middlewares;
using Notino.Homework.Providers.TotalCommander;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var emailConfig = configuration.GetSection("EmailConfig")
                               .Get<EmailConfig>();

builder.Services.AddSingleton(emailConfig);
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddSingleton<IConversionProvider, ConversionProvider>();
builder.Services.AddSingleton<IFileManager, FileManager>();
builder.Services.AddHttpClient<FileManager>();

builder.Services.AddControllers().AddJsonOptions(o =>
{
    o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddMediatR(typeof(Program));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();


