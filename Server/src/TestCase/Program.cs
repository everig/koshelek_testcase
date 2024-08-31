using Infrastructure.DB.Repositories;
using Application.Common.Interfaces;
using Application.Common.Mapping;
using AutoMapper;
using System.Reflection;
using Application.Common.Mappings;
using TestCase.SignalR;
using Serilog;
using MediatR;
using Application.Common.Logging;
using Microsoft.AspNetCore.Http.Connections;


Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
var assemblies = Assembly.Load("Application");


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(config=>
{
    var file = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var path = Path.Combine(AppContext.BaseDirectory, file);
    config.IncludeXmlComments(path);
});

builder.Services.AddSingleton(Log.Logger);

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<MappingProfile>();
    cfg.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
});

builder.Host.UseSerilog((context, loggerConfiguration) =>
{
    loggerConfiguration.WriteTo.Console();
    loggerConfiguration.ReadFrom.Configuration(context.Configuration);
});

builder.WebHost.ConfigureKestrel(serveroptions =>
{
    serveroptions.ListenAnyIP(8080);
});

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(assemblies);
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
});

builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

builder.Services.AddSingleton<IMessageRepository>(x => new MessageRepository(builder.Configuration.GetConnectionString("DefaultConnection"), x.GetService<IMapper>()));

builder.Services.AddCors(options => options.AddPolicy("CorsPolicy",
    builder =>
    {
        builder
        .AllowAnyHeader()
        .AllowAnyMethod()
        .SetIsOriginAllowed((host) => true)
        .AllowCredentials();
    }));

builder.Services.AddSignalR();
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapHub<MessageHub>("/messagehub", options =>
{
    options.Transports =
        HttpTransportType.WebSockets |
        HttpTransportType.ServerSentEvents |
        HttpTransportType.LongPolling;
});

app.MapControllers();

app.Run();
