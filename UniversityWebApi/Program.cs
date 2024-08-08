using Application;
using Elastic.Ingest.Elasticsearch.DataStreams;
using Elastic.Serilog.Sinks;
using Elastic.Transport;
using Persistence;
using RestClient;
using Serilog;
using Serilog.Events;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

var builder = WebApplication.CreateBuilder(args);

// Add serilog

builder.Host.UseSerilog((context, services, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
    .WriteTo.Logger(l => l.MinimumLevel.Is(Enum.Parse<LogEventLevel>(context.Configuration["Elasticsearch:MinimumLevel"]))
        .WriteTo.Elasticsearch([new Uri(context.Configuration["Elasticsearch:NodeUri"])], opts =>
        {
            opts.DataStream = new DataStreamName("university-webapi", "aspnetcore", "training");
        },
        transport =>
        {
            transport.Authentication(new BasicAuthentication(context.Configuration["Elasticsearch:Username"], context.Configuration["Elasticsearch:Password"]));
        })));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationLayer();
builder.Services.AddPersistenceLayer(builder.Configuration);
builder.Services.AddRestClient();

var app = builder.Build();

app.Migrate();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
