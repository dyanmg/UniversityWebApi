using Elastic.Ingest.Elasticsearch.DataStreams;
using Elastic.Serilog.Sinks;
using Elastic.Transport;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace Infra.Common
{
    public static class HostRegistration
    {
        public static IHostBuilder UseInfraLogging(this IHostBuilder host)
        {
            // Add serilog
            return host.UseSerilog((context, services, configuration) => configuration
                .ReadFrom.Configuration(context.Configuration)
                .WriteTo.Logger(l => l.MinimumLevel.Is(Enum.Parse<LogEventLevel>(context.Configuration["Elasticsearch:MinimumLevel"]!))
                    .WriteTo.Elasticsearch([new Uri(context.Configuration["Elasticsearch:NodeUri"]!)], opts =>
                    {
                        opts.DataStream = new DataStreamName("university-webapi", "aspnetcore", "training");
                    },
                    transport =>
                    {
                        transport.Authentication(new BasicAuthentication(context.Configuration["Elasticsearch:Username"]!, context.Configuration["Elasticsearch:Password"]!));
                    })));
        }
    }
}
